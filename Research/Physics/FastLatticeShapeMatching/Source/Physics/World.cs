using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
	public class World
	{
        static World singleton = new World();
        public static World Singleton { get { return singleton; } }

		public List<LsmBody> bodies = new List<LsmBody>();
        public BodyRepulse bodyBodyRepulse = new BodyRepulse();
        public WallRepulse bodyWallRepulse = new WallRepulse(9999, 9999);

        // groups
        public List<IEnvironmentForce> environmentForces = new List<IEnvironmentForce>();
        public List<IUpdatable> forceServices = new List<IUpdatable>();

		public void Update()
		{
            // update all services
            foreach (IUpdatable updatable in forceServices)
            {
                updatable.Update();
            }

            // update internal processes in bodies, find velocities
			foreach (LsmBody b in bodies)
			{
                if (!b.Frozen)
                {
                    foreach (IEnvironmentForce force in environmentForces)
                    {
                        if (force is WallForce && !b.UseWallForce) // HACK to apply custom forces // TODO: make correct system
                            continue;
                        force.ApplyForce(b.particles);
                    }
                    b.Smooth();
                    b.DoFracture();
                    b.UpdateParticlesVelocity();
                }
                else
                {
                    b.UpdateFrozenParticlesVelocity();
                }
            }

            // iterate subframes for collision and integration system of bodies
            List<CollisionSubframeBuffer> collisionBuffer = new List<CollisionSubframeBuffer>();
            int iterationsCounter = 0; // to prevent deadlocks
            const int maxIterations = 64; // to prevent deadlocks
            double timeCoefficientPrediction = 1.0;
            bool collisionFound = false;
            do
            {
                for (int i = 0; i < bodies.Count; ++i )
                {
                    LsmBody bLeft = bodies[i];
                    if (!bLeft.Frozen && !bLeft.UseWallForce)
                    {
                        bLeft.CollideWithWall(timeCoefficientPrediction, ref collisionBuffer); // TODO: refactor to remove 'ref collisionBuffer'
                    }
                    for (int j = i + 1; j < bodies.Count; ++j) // TODO: implement self-collisions for j == i.
                    {
                        LsmBody bRight = bodies[j];
                        LsmBody.CollideBodies(bLeft, bRight, timeCoefficientPrediction, ref collisionBuffer); // TODO: refactor to remove 'ref collisionBuffer'
                        LsmBody.CollideBodies(bRight, bLeft, timeCoefficientPrediction, ref collisionBuffer); // TODO: refactor to remove 'ref collisionBuffer'
                    }
                }

                double timeCoefficientIntegrate = 0.0;
                if (collisionBuffer.Count > 0)
                {
                    CollisionSubframeBuffer subframe = LsmBody.GetEarliestSubframe(collisionBuffer);	// WARNING: now we assume that in 1 time moment we have maximum 1 collision in subframe.
																									    // TODO: make system ready to handle multi-collision subframes. For example, we'll need to accumulate velocity deltas for every particle and then make summation - not just direct assign of values.
                    timeCoefficientIntegrate = subframe.timeCoefficient;
                    timeCoefficientPrediction -= subframe.timeCoefficient;

                    subframe.particle.v = subframe.vParticle;
                    if (subframe.edge != null)
                    {
                        subframe.edge.start.v = subframe.vEdgeStart;
                        subframe.edge.end.v = subframe.vEdgeEnd;
                    }

                    collisionBuffer.Clear();
                    collisionFound = true;
                }
                else
                {
                    timeCoefficientIntegrate = timeCoefficientPrediction;
                    collisionFound = false;
                }

                if (timeCoefficientIntegrate > 0.0)
                {
                    foreach (LsmBody b in bodies)
                    {
                        if (!b.Frozen)
                        {
                            b.UpdateParticlesPosition(timeCoefficientIntegrate);
                        }
                        else
                        {
                            b.UpdateFrozenParticlesPosition();
                        }
                    }
                }
                else
                {
                    Testbed.PostMessage(System.Drawing.Color.Yellow, "Timestep <= 0.0 while subframes iterating!"); // DEBUG
                }

                if (++iterationsCounter >= maxIterations) // to prevent deadlocks
                {
                    Testbed.PostMessage(System.Drawing.Color.Red, "Deadlock detected in HandleCollisions!"); // DEBUG
                    if (LsmBody.pauseOnDeadlock) Testbed.Paused = true;
                    break;
                }
            }
            while (collisionFound);

            // postupdate all services
            foreach (IUpdatable updatable in forceServices)
            {
                updatable.PostUpdate();
            }
		}
	}
}
