using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace PhysicsTestbed
{
    public class CollisionSubframe
    {
        public Particle particle;               // Particle in collision
        public Vector2 vParticle;   	        //  Velocity after collision
        public Edge edge;                       // Edge in collision
        public Vector2 vEdgeStart;		        //  Velocity after collision
        public Vector2 vEdgeEnd;		        //  Velocity after collision
        public double timeCoefficient = 0.0;    // Part of time step before collision - [0, 1]

        public CollisionSubframe() 
        {
        }

        public CollisionSubframe(Particle particle, Vector2 vParticle, Edge edge, Vector2 vEdgeStart, Vector2 vEdgeEnd, double timeCoefficient)
        {
            //Debug.Assert(timeCoefficient > 0.0);
            this.particle = particle;
            this.vParticle = vParticle;
            this.edge = edge;
            this.vEdgeStart = vEdgeStart;
            this.vEdgeEnd = vEdgeEnd;
            this.timeCoefficient = timeCoefficient;
        }
    }

    public class CollisionSubframeBuffer : CollisionSubframe
    {
        public CollisionSubframeBuffer(Particle particle, Vector2 vParticle, Edge edge, Vector2 vEdgeStart, Vector2 vEdgeEnd, double timeCoefficient)
        {
            Debug.Assert(!double.IsInfinity(timeCoefficient) && !double.IsNaN(timeCoefficient));
            this.particle = particle;
            this.vParticle = vParticle;
            this.edge = edge;
            this.vEdgeStart = vEdgeStart;
            this.vEdgeEnd = vEdgeEnd;
            this.timeCoefficient = timeCoefficient;
        }
    }

    // WARNING: not full functional class. it supports only constructor, Add and Clear.
    // TODO: move it to more appropriate file
    public class CollisionSubframeList
    {
        double currentTime = 0.0;
        public double CurrentTime { get { return currentTime; } }

        List<CollisionSubframe> buffer = new List<CollisionSubframe>();
        public List<CollisionSubframe> Buffer { get { return buffer; } }

        public void Add(CollisionSubframe item)
        {
            buffer.Add(item);
            currentTime += item.timeCoefficient;
        }

        public void Clear()
        {
            buffer.Clear();
            currentTime = 0.0;
        }
    }

    public class Edge
    {
        public Particle start;
        public Particle end;

        public Edge(Particle start, Particle end)
        {
            this.start = start;
            this.end = end;
        }
    }

	public class Particle
	{
        public LsmBody body;
		public LatticePoint latticePoint;
		public Chunk chunk;
        public bool locked = false;					// Whether I have been locked in place
        public Particle xPos, yPos, xNeg, yNeg;		// Neighbors
		public ManagedList<SmoothingRegion> parentRegions = new ManagedList<SmoothingRegion>();

		// Physical properties
        public double mass = 1.0;
        public Vector2 x;							// Position
		public Vector2 v;							// Velocity
        public Vector2 x0;							// Material position
        public Vector2 fExt;						// External force
        public Vector2 xPrior;                      // Position from prior frame

        public class CCDDebugInfo
        {
            public Vector2 point;
            public LineSegment edge;
            public double coordinateOfPointOnEdge;
            public CCDDebugInfo(Vector2 point, LineSegment edge)
            { 
                this.point = point;
                this.edge = edge;
                this.coordinateOfPointOnEdge = (point - edge.start).Dot(edge.end - edge.start) / (edge.end - edge.start).LengthSq();
            }
        }
        public List<CCDDebugInfo> ccdDebugInfos = new List<CCDDebugInfo>(); // DEBUG

		// Shape matching
        public Vector2 goal;
        public Matrix2x2 R;							// Average of all parent regions' Rs - used in fracturing

		// Damping
		public Vector2 dv;

		public double PerRegionMass					// m~ in [Rivers and James 2007]
		{
			get { return mass / parentRegions.Count; }
		}

		public bool DoFracture()
		{
			bool somethingBroke = false;
			if(xPos != null)
				somethingBroke = somethingBroke || DoFracture(ref xPos, ref xPos.xNeg);
			if(yPos != null)
				somethingBroke = somethingBroke || DoFracture(ref yPos, ref yPos.yNeg);
			return somethingBroke;
		}

		public bool DoFracture(ref Particle other, ref Particle me)
		{
			bool somethingBroke = false;

			double len = (other.goal - goal).Length();
			double rest = (other.x0 - x0).Length();
			double off = Math.Abs((len / rest) - 1.0);
			if (off > LsmBody.fractureLengthTolerance)
			{
				somethingBroke = true;
				Testbed.PostMessage("Length fracture: Rest = " + rest + ", actual = " + len);
			}

			if (!somethingBroke)
			{
				Vector2 a = new Vector2(other.R[0, 0], other.R[1, 0]);
				Vector2 b = new Vector2(R[0, 0], R[1, 0]);
				a.Normalize();
				b.Normalize();
				double angleDifference = Math.Acos(a.Dot(b));
				if (angleDifference > LsmBody.fractureAngleTolerance)
				{
					somethingBroke = true;
					Testbed.PostMessage("Angle fracture: angle difference = " + angleDifference);
				}
			}

			if (somethingBroke)
			{
				Particle saved = other;
				me = null;
				other = null;

				// Check if the chunks are still connected
				Queue<Particle> edge = new Queue<Particle>();
				List<Particle> found = new List<Particle>();
				edge.Enqueue(this);
				bool connected = false;
				while (edge.Count > 0)
				{
					Particle p = edge.Dequeue();
					if (!found.Contains(p))
					{
						found.Add(p);
						if (p == saved)
						{
							// Connected
							connected = true;
							break;
						}
						if (p.xPos != null)
							edge.Enqueue(p.xPos);
						if (p.xNeg != null)
							edge.Enqueue(p.xNeg);
						if (p.yPos != null)
							edge.Enqueue(p.yPos);
						if (p.yNeg != null)
							edge.Enqueue(p.yNeg);
					}
				}
				if (connected == false)
				{
					// The chunks broke - there are now two separate chunks (maximally connected subgraphs)
					chunk.particles.Clear();
					chunk.particles.AddRange(found);
					chunk.CalculateInvariants();

					Chunk newChunk = new Chunk();

					edge.Clear();
					found.Clear();
					edge.Enqueue(saved);
					while (edge.Count > 0)
					{
						Particle p = edge.Dequeue();
						if (!found.Contains(p))
						{
							found.Add(p);
							p.chunk = newChunk;
							if (p.xPos != null)
								edge.Enqueue(p.xPos);
							if (p.xNeg != null)
								edge.Enqueue(p.xNeg);
							if (p.yPos != null)
								edge.Enqueue(p.yPos);
							if (p.yNeg != null)
								edge.Enqueue(p.yNeg);
						}
					}

					newChunk.particles.AddRange(found);
					newChunk.CalculateInvariants();
					body.chunks.Add(newChunk);

					Testbed.PostMessage("Chunk broken: the original chunk now has " + chunk.particles.Count + " particles, the new chunk has " + newChunk.particles.Count + " particles.");
					Testbed.PostMessage("Number of chunks / particles: " + body.chunks.Count + " / " + body.particles.Count);
				}
			}

			return somethingBroke;
		}
	}
}
