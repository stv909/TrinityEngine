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
		public List<EnvironmentForce> environmentForces = new List<EnvironmentForce>();
		public List<EnvironmentImpulse> environmentImpulses = new List<EnvironmentImpulse>();
        public List<EnvironmentForce> interactionServices = new List<EnvironmentForce>();

        public List<MouseForce> mouseForces = new List<MouseForce>();

        // Simulation parameters
        //[Controllable(Type=ControllableAttribute.ControllerType.Slider, Min=0.0, Max=1.0, Caption="Alpha")]
        public static double alpha = 1.0;

        public LsmBody bodyActiveDebug = null;
        public LsmBody bodyPassiveDebug = null;

		public void Update()
		{
            // update all external forces
			foreach (EnvironmentForce e in environmentForces)
			{
				e.Update();
			}

            // update internal processes in bodies, find velocities
			foreach (LsmBody b in bodies)
			{
                if (!b.frozen)
                {
                    foreach (EnvironmentForce e in environmentForces)
                    {
                        if (
                            e is WallForce && !b.pureForces
                            && !b.Equals(bodyPassiveDebug) // DEBUG wallForce influence passive body anyway
                        )
                            continue;
                        e.ApplyForce(b.particles);
                    }
                    b.Smooth();
                    b.DoFracture();
                    b.UpdateParticlesVelocity();
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
                    if (!bLeft.frozen && !bLeft.pureForces)
                    {
                        if (bLeft.Equals(bodyActiveDebug)) // DEBUG no collisions for passive body
                        {
                            bLeft.CollideWithWall(timeCoefficientPrediction, ref collisionBuffer); // TODO: refactor to remove 'ref collisionBuffer'
                            for (int j = i + 1; j < bodies.Count; ++j) // TODO: implement self-collisions for j == i.
                            {
                                LsmBody bRight = bodies[j];
                                if (bRight.Equals(bodyPassiveDebug)) // DEBUG no collisions for passive body
                                {
                                    bLeft.CollideWith(bRight, timeCoefficientPrediction, ref collisionBuffer); // TODO: refactor to remove 'ref collisionBuffer'
                                }
                            }
                        }
                    }
                }

                double timeCoefficientIntegrate = 0.0;
                if (collisionBuffer.Count > 0)
                {
                    CollisionSubframeBuffer subframe = LsmBody.GetEarliestSubframe(collisionBuffer);
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
                        if (!b.frozen)
                        {
                            b.UpdateParticlesPosition(timeCoefficientIntegrate);
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

            // postupdate external forces
            foreach (EnvironmentForce e in environmentForces)
            {
                e.PostUpdate();
            }
		}
	}
}
