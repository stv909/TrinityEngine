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

                b.UpdateParticles();
			}

            foreach (EnvironmentForce e in environmentForces)
            {
                e.PostUpdate();
            }
		}
	}
}
