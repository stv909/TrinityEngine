using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PhysicsTestbed
{
	public class LatticePoint
	{
		public Point index;
		public Particle particle;
		public SmoothingRegion smoothingRegion;
	}
	
	public class LsmBody
	{
		public bool[,] blueprint;

		public int width, height;
		public LatticePoint[,] lattice;
		public List<Particle> particles = new List<Particle>();
		public List<SmoothingRegion> smoothingRegions = new List<SmoothingRegion>();
		public List<Chunk> chunks = new List<Chunk>();

		Point spacing = new Point(25, 25);
		Vector2 offset = new Vector2(200, 200);
        Color3 color = new Color3(0, 1, 0);
        public Color3 Color { get { return color; } }

		protected static int w = 3;
		[Controllable(Type = ControllableAttribute.ControllerType.Textbox, Caption = "w", Min = 0, Max = 100, Integral = true)]
		public static int W
		{
			get
			{
				return w;
			}
			set
			{
				w = value;
				foreach(LsmBody b in World.Singleton.bodies)
					b.ChangeW();
			}
		}
		[Controllable(Type = ControllableAttribute.ControllerType.Slider, Caption = "Inertia bleeding", Min = 0.0, Max = 1.0)]
		public static double kInertiaBleeding = 0.0;
		[Controllable(Type = ControllableAttribute.ControllerType.Slider, Caption="Chunk damping", Min = 0.0, Max = 1.0)]
        public static double kChunkDamping = 0.0;
        [Controllable(Type = ControllableAttribute.ControllerType.Slider, Caption = "Region damping", Min = 0.0, Max = 1.0)]
		public static double kRegionDamping = 0.5;
		[Controllable(Type = ControllableAttribute.ControllerType.Slider, Caption = "Chunk smoothing", Min = 0.0, Max = 1.0)]
		public static double kChunkSmoothing = 0.0;

		[Controllable(Type = ControllableAttribute.ControllerType.Checkbox, Caption = "Fracturing")]
		public static bool fracturing = false;
		[Controllable(Type = ControllableAttribute.ControllerType.Checkbox, Caption = "Pause on fracture")]
		public static bool pauseOnFracture = false;
		[Controllable(Type = ControllableAttribute.ControllerType.Slider, Caption = "Fracture length tolerance", Min = 0.0, Max = 1.0)]
		public static double fractureLengthTolerance = 0.66;
		[Controllable(Type = ControllableAttribute.ControllerType.Slider, Caption = "Fracture angle tolerance", Min = 0.0, Max = 1.0)]
		public static double fractureAngleTolerance = 0.63;

        public LsmBody(Vector2 offset, Color3 color)
        {
            this.offset = offset;
            this.color = color;
        }

		public bool InBounds(int x, int y)
		{
			return (x >= 0 && y >= 0 && x < width && y < height);
		}

		public void GenerateFromBlueprint(bool[,] blueprint)
		{
			this.blueprint = blueprint;

			width = blueprint.GetLength(0);
			height = blueprint.GetLength(1);

			lattice = new LatticePoint[width, height];

			int x, y;

			for (x = 0; x < width; x++)
			{
				for (y = 0; y < height; y++)
				{
					lattice[x, y] = new LatticePoint();
					lattice[x,y].index = new Point(x,y);

					if (blueprint[x, y] == true)
					{
						// Generate particle
						Particle p = new Particle();
						lattice[x, y].particle = p;
						p.body = this;
						p.x0 = new Vector2(spacing.X * x, spacing.Y * y);
						p.x = offset + p.x0;
						p.latticePoint = lattice[x, y];
						p.goal = p.x;
						particles.Add(p);
					}
					else
					{
					}
				}
			}

			// Set up the neighbors
			for (x = 0; x < width; x++)
			{
				for (y = 0; y < height; y++)
				{
					if (blueprint[x, y] == true)
					{
						if (InBounds(x + 1, y) && blueprint[x + 1, y] == true)
						{
							lattice[x, y].particle.xPos = lattice[x + 1, y].particle;
							lattice[x + 1, y].particle.xNeg = lattice[x, y].particle;
						}
						if (InBounds(x, y + 1) && blueprint[x, y + 1] == true)
						{
							lattice[x, y].particle.yPos = lattice[x, y + 1].particle;
							lattice[x, y + 1].particle.yNeg = lattice[x, y].particle;
						}
					}
				}
			}

			// Create the smoothing regions and have them generate themselves
			foreach (Particle p in particles)
			{
				SmoothingRegion s = new SmoothingRegion(w);
				s.body = this;
				s.latticePoint = p.latticePoint;
				s.latticePoint.smoothingRegion = s;
				s.RegenerateRegion();
				smoothingRegions.Add(s);
			}

			// Create one chunk with all the particles in it
			Chunk c = new Chunk();
			foreach (Particle p in particles)
			{
				c.particles.Add(p);
				p.chunk = c;
			}
			c.CalculateInvariants();
			chunks.Add(c);

			// Setup the regions
			foreach (SmoothingRegion s in smoothingRegions)
			{
				s.CalculateInvariants();
			}
		}

		public void ChangeW()
		{
			foreach (SmoothingRegion s in smoothingRegions)
			{
				s.w = w;
			}
			RegenerateRegions();
		}

		public void Smooth()
		{
			// Region smoothing
            foreach (Particle p in particles)
            {
                p.goal = Vector2.ZERO;
				p.R = Matrix2x2.ZERO;
            }

			foreach (SmoothingRegion s in smoothingRegions)
			{
				s.ShapeMatch();
			}

			foreach (Particle p in particles)
			{
				p.goal /= p.mass;
				p.R /= p.mass;
			}

			// Chunk smoothing (smooths all particles in each chunk - a chunk is a maximally connected subgraph)
			PerformChunkSmoothing();
		}

        public void UpdateParticlesVelocity()
        {
			// Bleed off inertia, if the user wants to (usually not)
			foreach (Particle particle in particles)
			{
				particle.v *= (1 - kInertiaBleeding);
			}

            // Calculate velocity
            foreach(Particle p in particles)
            {
                if (p.parentRegions.Count != 0)
                {
                    p.v = p.v + (p.goal - p.x) + p.fExt / p.mass;		// Eqn. (1), h = 1.0
                }
                else
                {
					// We have no parent regions, so just move about as an independent particle
					p.goal = p.x;
                    p.v += p.fExt / p.mass;
                }
				p.fExt = Vector2.ZERO;
            }

            // Damp velocity
            PerformRegionDamping();
			PerformChunkDamping();
        }

        public void UpdateParticlesPosition()
        {
            // Apply velocity
            foreach (Particle p in particles)
            {
                if (p.locked == false)
                {
                    if (p.timeCoefPreCollision > 0.0)
                    {
                        p.x += p.vPreCollision * p.timeCoefPreCollision;
                        p.x += p.v * (1.0 - p.timeCoefPreCollision);

                        /*
                        // Debug metrics
                        Testbed.PostMessage(
                            System.Drawing.Color.Green, "Collision check: " + ( (p.vPreCollision * p.timeCoefPreCollision).Length() + (p.v * (1.0 - p.timeCoefPreCollision)).Length() ) + " =?= " + p.v.Length()
                        );
                        */
                    }
                    else
                    {
                        p.x += p.v;
                    }
                }
                else
                {
                    p.v = Vector2.ZERO;
                }
            }
        }

		public void PerformChunkSmoothing()
		{
			if (kChunkSmoothing != 0.0)
			{
				foreach (Chunk chunk in chunks)
				{
					chunk.ShapeMatch();
				}
			}
		}

        public void PerformChunkDamping()
		{
			if (kChunkDamping != 0.0)
			{
				// Damp velocity -- per chunk
				// Does it once, for each whole chunk - causes the rigid-body look

				foreach (Particle particle in particles)
				{
					particle.dv = Vector2.ZERO;
				}

				foreach (Chunk chunk in chunks)
				{
					chunk.CalculateDampedVelocities();
				}

				foreach (Particle particle in particles)
				{
					particle.dv /= particle.mass;
					particle.v = particle.v + kChunkDamping * particle.dv;
				}
			}
        }

        public void PerformRegionDamping()
        {
            if (kRegionDamping != 0.0)
            {
		        // Damp velocity -- per region

				foreach (Particle particle in particles)
				{
					particle.dv = Vector2.ZERO;
				}

                foreach(SmoothingRegion r in smoothingRegions)
                {
					r.CalculateDampedVelocities();
		        }

                foreach(Particle particle in particles)
                {
					particle.dv /= particle.mass;
					particle.v = particle.v + kRegionDamping * particle.dv;
                }
	        }
        }

		public void RegenerateRegions()
		{
			foreach (SmoothingRegion s in smoothingRegions)
				s.RegenerateRegion();
			CalculateInvariants();
		}

		public void CalculateInvariants()
		{
			foreach (SmoothingRegion s in smoothingRegions)
				s.CalculateInvariants();
			foreach (Chunk chunk in chunks)
				chunk.CalculateInvariants();
		}

		public void DoFracture()
		{
			if (!fracturing)
				return;

			bool somethingBroke = false;
			foreach (Particle p in particles)
			{
				somethingBroke = somethingBroke || p.DoFracture();
			}
			if (somethingBroke)
			{
				RegenerateRegions();
				if(pauseOnFracture)
					Testbed.Paused = true;
			}
		}
	}
}
