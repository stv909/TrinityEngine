using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
	public class ParticleAndDepth
	{
		public Particle particle;
		public int depth;

		public ParticleAndDepth(Particle t, int depth)
		{
			this.particle = t;
			this.depth = depth;
		}
	}

	public class SmoothingRegion
	{
		// Static properties
		public LsmBody body;
		public LatticePoint latticePoint;
		public int w;									// Region half-width
		public ManagedList<Particle> particles = new ManagedList<Particle>();

		// Physical properties
		public double M;								// The sum of all the particle masses of the particles in the region
		public Vector2 c0;								// The weighted average of all the particles' material positions

		// For smoothing
		public Vector2 o = new Vector2();				// Part of Tr - (cr - Rr cr0)) - the best fit translation
		public Matrix2x2 R = new Matrix2x2();			// Best fit orientation

		public void AddParticle(Particle p)
		{
			particles.Add(p);
			p.parentRegions.Add(this);
		}

		public void RemoveParticle(Particle p)
		{
			p.parentRegions.Remove(this);
			particles.Remove(p);
		}

		public bool ContainsParticle(Particle p)
		{
			return (particles.Contains(p));
		}

		public void RegenerateRegion()
		{
			foreach (Particle p in particles)
			{
				RemoveParticle(p);
			}

			Particle center = latticePoint.particle;
			Queue<ParticleAndDepth> toAdd = new Queue<ParticleAndDepth>();
			toAdd.Enqueue(new ParticleAndDepth(center, 0));
			while (toAdd.Count > 0)
			{
				ParticleAndDepth t = toAdd.Dequeue();
				if (t.particle != null && ContainsParticle(t.particle) == false && t.depth <= w)
				{
					AddParticle(t.particle);

					toAdd.Enqueue(new ParticleAndDepth(t.particle.xPos, t.depth + 1));
					toAdd.Enqueue(new ParticleAndDepth(t.particle.xNeg, t.depth + 1));
					toAdd.Enqueue(new ParticleAndDepth(t.particle.yPos, t.depth + 1));
					toAdd.Enqueue(new ParticleAndDepth(t.particle.yNeg, t.depth + 1));
				}
			}

			// Can't calculate invariants yet because we don't yet know the particles' per-region masses
		}

		public SmoothingRegion(int w)
		{
			this.w = w;
		}

		public void CalculateInvariants()
		{
			M = 0;
			c0 = new Vector2();
			foreach (Particle p in particles)
			{
				M += p.PerRegionMass;
				c0 += p.x0 * p.PerRegionMass;
			}
			c0 /= M;
		}

		// NOTE: This does NOT reuse sub-summations, and therefore is O(w^3) rather than O(1) as can be attained (see Readme.txt)
		public void ShapeMatch()
		{
			if (M == 0)
				return;

			// Calculate center of mass
			Vector2 c = new Vector2();
			foreach (Particle p in particles)
			{
				c += p.PerRegionMass * p.x;
			}
			c /= M;

			// Calculate A = Sum( m~ (xi - cr)(xi0 - cr0)^T ) - Eqn. 10
            Matrix2x2 A = Matrix2x2.ZERO;
            foreach (Particle p in particles)
            {
                A += p.PerRegionMass * Matrix2x2.MultiplyWithTranspose(p.x - c, p.x0 - c0);
            }

			// Polar decompose
            Matrix2x2 S = new Matrix2x2();
            R = A.ExtractRotation();

			if (double.IsNaN(R[0, 0]))
				R = Matrix2x2.IDENTITY;

			// Check for and fix inverted shape matching
            if (R.Determinant() < 0)
                R = R * -1;

			// Calculate o, the remaining part of Tr
            o = c + R * (-c0);

			// Add our influence to the particles' goal positions
			Vector2 sumAppliedForces = Vector2.ZERO;
			foreach (Particle p in particles)
			{
				// Figure out the goal position according to this region, Tr * p.x0
				Vector2 particleGoalPosition = o + R * p.x0;

				p.goal += p.PerRegionMass * particleGoalPosition;
				p.R += p.PerRegionMass * R;

                // For checking only
				sumAppliedForces += p.PerRegionMass * (particleGoalPosition - p.x);
			}

			// Error check
            if (sumAppliedForces.Length() > 0.001)
            {
				Testbed.PostMessage(Color.Red, "Shape matching region's forces did not sum to zero!");
            }
		}

		public void CalculateDampedVelocities()
		{
			// From Mueller et al., Position Based Dynamics
			if (particles.Count > 1)
			{
				double L = 0;
				double I = 0;

				Vector2 v = Vector2.ZERO;
				Vector2 c = Vector2.ZERO;
				foreach (Particle p in particles)
				{
					c += p.PerRegionMass * p.x;
					v += p.PerRegionMass * p.v;
				}
				c /= M;
				v /= M;

				foreach (Particle p in particles)
				{
					Vector2 ri = p.x - c;
					L += ri.CrossProduct(p.PerRegionMass * p.v);

					I += p.PerRegionMass * ri.LengthSq();
				}
				double w;
				w = L / I;

				foreach (Particle p in particles)
				{
					Vector2 ri = p.x - c;

					Vector2 dv;
					dv = v + new Vector2(-w * ri.Y, w * ri.X) - p.v;
					p.dv += p.PerRegionMass * dv;
				}
			}
		}
	}
}
