using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    public class BodyImpulse : EnvironmentImpulse
    {
        public LsmBody body = null;

        public BodyImpulse(LsmBody body)
        {
            this.body = body;
        }

        public override void ApplyImpulse(System.Collections.IEnumerable particles)
        {
            // avoid self-collision
            if (particles.Equals(body.particles))
                return;

            bool collisionDetected = false;
            foreach (Particle t in particles)
            {
                foreach (Particle bt in body.particles)
                {
                    if ((t.x - bt.x).Length() < 10.0)
                    {
                        collisionDetected = true;
                        //Testbed.PostMessage(System.Drawing.Color.Green, "Collision detected!"); // DEBUG
                    }
                }
            }
            if (collisionDetected)
            {
                //Testbed.PostMessage(System.Drawing.Color.Green, "------------------------"); // DEBUG
            }
        }
    }
}
