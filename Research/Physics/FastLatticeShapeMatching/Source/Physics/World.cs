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
            foreach (LsmBody b in bodies)
            {
                if (!b.frozen)
                {
                    if (b.Equals(bodyActiveDebug)) // DEBUG no collisions for passive body
                    {
                        b.HandleCollisions(environmentImpulses);
                    }
                }
            }

            foreach (LsmBody b in bodies)
            {
                if (!b.frozen)
                {
                    b.UpdateParticlesPosition();
                }
            }

            // postupdate external forces
            foreach (EnvironmentForce e in environmentForces)
            {
                e.PostUpdate();
            }
		}
	}
}
