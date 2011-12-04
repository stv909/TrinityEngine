using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    public class WallForce : EnvironmentForce
    {
        public float border = 30;
        public float left = 0, right, bottom = 0, top;

        public WallForce(float width, float height)
        {
            this.right = width;
            this.top = height;
        }

        public override void ApplyForce(System.Collections.IEnumerable particles)
        {
            float left = this.left + border, right = this.right - border, bottom = this.bottom + border, top = this.top - border;

            foreach (Particle t in particles)
            {
                Vector2 pos = t.x;
                // force method
                if (pos.X < left)
                {
                    t.fExt += new Vector2(left - pos.X, 0);
                }
                if (pos.Y < bottom)
                {
                    t.fExt += new Vector2(0, bottom - pos.Y);
                }
                if (pos.X > right)
                {
                    t.fExt += new Vector2(right - pos.X, 0);
                }
                if (pos.Y > top)
                {
                    t.fExt += new Vector2(0, top - pos.Y);
                }
            }
        }
    }
}
