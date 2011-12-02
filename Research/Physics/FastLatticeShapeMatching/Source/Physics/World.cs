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

        // Simulation parameters
        //[Controllable(Type=ControllableAttribute.ControllerType.Slider, Min=0.0, Max=1.0, Caption="Alpha")]
        public static double alpha = 1.0;

		public void Update()
		{
			foreach (EnvironmentForce e in environmentForces)
			{
				e.Update();
			}

			foreach (LsmBody b in bodies)
			{
				foreach (EnvironmentForce e in environmentForces)
				{
					e.ApplyForce(b.particles);
				}
				b.Smooth();
				b.DoFracture();
                b.UpdateParticlesVelocity();
                b.HandleCollisions(environmentImpulses);
                b.UpdateParticlesPosition();
            }

            foreach (EnvironmentForce e in environmentForces)
            {
                e.PostUpdate();
            }
		}
	}
}
