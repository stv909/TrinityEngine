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

        public override void ApplyImpulse(LsmBody applyBody, Vector2 pos, Vector2 posNext, Vector2 velocity, ref List<CollisionSubframe> collisionBuffer)
		{
            float left = this.left + border, right = this.right - border, bottom = this.bottom + border, top = this.top - border;

            if (posNext.X < left)
            {
                collisionBuffer.Add(new CollisionSubframe(new Vector2(-velocity.X, velocity.Y), (left - pos.X) / velocity.X));
            }
            if (posNext.X > right)
            {
                collisionBuffer.Add(new CollisionSubframe(new Vector2(-velocity.X, velocity.Y), (right - pos.X) / velocity.X));
            }
            if (posNext.Y < bottom)
            {
                collisionBuffer.Add(new CollisionSubframe(new Vector2(velocity.X, -velocity.Y), (bottom - pos.Y) / velocity.Y));
            }
            if (posNext.Y > top)
            {
                collisionBuffer.Add(new CollisionSubframe(new Vector2(velocity.X, -velocity.Y), (top - pos.Y) / velocity.Y));
            }
        }
	}
}
