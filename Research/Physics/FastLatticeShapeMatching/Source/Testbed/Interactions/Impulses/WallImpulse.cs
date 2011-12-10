using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

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

        public override void ApplyImpulse(LsmBody applyBody, Particle applyParticle, LsmBody otherBody, Vector2 pos, Vector2 posNext, Vector2 velocity, ref List<CollisionSubframeBuffer> collisionBuffer, double accumulatedSubframeTime)
		{
            Debug.Assert(otherBody == null);
            float left = this.left + border, right = this.right - border, bottom = this.bottom + border, top = this.top - border;

            if (posNext.X < left)
            {
                collisionBuffer.Add(new CollisionSubframeBuffer(applyParticle, new Vector2(-velocity.X, velocity.Y), null, new Vector2(0.0, 0.0), new Vector2(0.0, 0.0), (left - pos.X) / velocity.X));
            }
            if (posNext.X > right)
            {
                collisionBuffer.Add(new CollisionSubframeBuffer(applyParticle, new Vector2(-velocity.X, velocity.Y), null, new Vector2(0.0, 0.0), new Vector2(0.0, 0.0), (right - pos.X) / velocity.X));
            }
            if (posNext.Y < bottom)
            {
                collisionBuffer.Add(new CollisionSubframeBuffer(applyParticle, new Vector2(velocity.X, -velocity.Y), null, new Vector2(0.0, 0.0), new Vector2(0.0, 0.0), (bottom - pos.Y) / velocity.Y));
            }
            if (posNext.Y > top)
            {
                collisionBuffer.Add(new CollisionSubframeBuffer(applyParticle, new Vector2(velocity.X, -velocity.Y), null, new Vector2(0.0, 0.0), new Vector2(0.0, 0.0), (top - pos.Y) / velocity.Y));
            }
        }
	}
}
