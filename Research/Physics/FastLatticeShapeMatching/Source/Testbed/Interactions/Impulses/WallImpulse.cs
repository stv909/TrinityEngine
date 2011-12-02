using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    public class WallImpulse : EnvironmentImpulse
	{
        public float border = 45;
        public float left = 0, right, bottom = 0, top;

		public WallImpulse(float width, float height)
		{
			this.right = width;
			this.top = height;
		}

		public override void ApplyImpulse(System.Collections.IEnumerable particles)
		{
            float left = this.left + border, right = this.right - border, bottom = this.bottom + border, top = this.top - border;

			foreach(Particle t in particles)
			{
                // impulse & offset collision method
                t.vHistory.Clear();
                Vector2 pos = t.x;
                Vector2 posNext = pos + t.v;
                if (posNext.X < left)
                {
                    t.vHistory.Add(new PreCollisionHistory(t.v, (left - pos.X) / t.v.X));
                    t.v.X = -t.v.X;
                }
                else if (posNext.X > right)
                {
                    t.vHistory.Add(new PreCollisionHistory(t.v, (right - pos.X) / t.v.X));
                    t.v.X = -t.v.X;
                }
                else if (posNext.Y < bottom)
                {
                    t.vHistory.Add(new PreCollisionHistory(t.v, (bottom - pos.Y) / t.v.Y));
                    t.v.Y = -t.v.Y;
                }
                else if (posNext.Y > top)
                {
                    t.vHistory.Add(new PreCollisionHistory(t.v, (top - pos.Y) / t.v.Y));
                    t.v.Y = -t.v.Y;
                }
                // WARNING: now we handle only 1 collision case and ignore all others - because we are using else statement between collision checks
                // TODO: handle all accured collisions iterativly
            }
		}
	}
}
