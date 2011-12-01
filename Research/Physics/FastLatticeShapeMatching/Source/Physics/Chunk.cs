using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PhysicsTestbed
{
	public class Chunk
	{
		public List<Particle> particles = new List<Particle>();

		// Physical properties
		public double M;								// The sum of all the particle masses of the particles in the region
		public Vector2 c0;								// The weighted average of all the particles' material positions

		// For smoothing
		public Vector2 o = new Vector2();				// Part of Tr - (cr - Rr cr0)) - the best fit translation
		public Matrix2x2 R = new Matrix2x2();			// Best fit orientation

		// Chunks have the same shape-matching and velocity-damping functions as smoothing regions,
		//  with the only difference being that they do not take per-region mass into account

		public void CalculateInvariants()
		{
			M = 0;
			c0 = Vector2.ZERO;
			foreach (Particle particle in particles)
			{
				M += particle.mass;
				c0 += particle.mass * particle.x0;
			}
			c0 /= M;
		}

		public void ShapeMatch()
		{
			if (M == 0)
				return;

			// Calculate center of mass
			Vector2 c = new Vector2();
			foreach (Particle p in particles)
			{
				c += p.mass * p.x;
			}
			c /= M;

			// Calculate A = Sum( m~ (xi - cr)(xi0 - cr0)^T ) - Eqn. 10
			Matrix2x2 A = Matrix2x2.ZERO;
			foreach (Particle p in particles)
			{
				A += p.mass * Matrix2x2.MultiplyWithTranspose(p.x - c, p.x0 - c0);
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

				p.goal = (1 - LsmBody.kChunkSmoothing) * p.goal + LsmBody.kChunkSmoothing * particleGoalPosition;	// Blend

				// For checking only
				sumAppliedForces += p.mass * (particleGoalPosition - p.x);
			}

			// Error check
			if (sumAppliedForces.Length() > 0.001)
			{
				Testbed.PostMessage(Color.Red, "Chunk's forces did not sum to zero!");
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
				M = 0;
				foreach (Particle p in particles)
				{
					c += p.mass * p.x;
					v += p.mass * p.v;
					M += p.mass;
				}
				c /= M;
				v /= M;

				foreach (Particle p in particles)
				{
					Vector2 ri = p.x - c;
					L += ri.CrossProduct(p.mass * p.v);

					I += p.mass * ri.LengthSq();
				}
				double w;
				w = L / I;

				foreach (Particle p in particles)
				{
					Vector2 ri = p.x - c;

					Vector2 dv;
					dv = v + new Vector2(-w * ri.Y, w * ri.X) - p.v;
					p.dv += p.mass * dv;
				}
			}
		}
	}
}
