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
            SetDimensions(width, height);
		}

        public void SetDimensions(float width, float height)
        {
            this.right = width;
            this.top = height;
        }

        public override void ApplyImpulse(LsmBody applyBody, Particle applyParticle, LsmBody otherBody, double timeCoefficientPrediction, ref List<CollisionSubframeBuffer> collisionBuffer)
		{
            Debug.Assert(otherBody == null);
            float left = this.left + border, right = this.right - border, bottom = this.bottom + border, top = this.top - border;
            if (left < 0.0 || right < 0.0 || bottom < 0.0 || top < 0.0) // to prevent computation for incorrect case
                return;

            Vector2 pos = applyParticle.x;
            Vector2 velocity = applyParticle.v;
            Vector2 posNext = pos + velocity * timeCoefficientPrediction;

            if (posNext.X < left)
            {
                collisionBuffer.Add(new CollisionSubframeBuffer(applyParticle, new Vector2(-velocity.X * BodyImpulse.coefficientElasticity, velocity.Y), null, Vector2.ZERO, Vector2.ZERO, (left - pos.X) / velocity.X));
            }
            if (posNext.X > right)
            {
                collisionBuffer.Add(new CollisionSubframeBuffer(applyParticle, new Vector2(-velocity.X * BodyImpulse.coefficientElasticity, velocity.Y), null, Vector2.ZERO, Vector2.ZERO, (right - pos.X) / velocity.X));
            }
            if (posNext.Y < bottom)
            {
                collisionBuffer.Add(new CollisionSubframeBuffer(applyParticle, new Vector2(velocity.X, -velocity.Y * BodyImpulse.coefficientElasticity), null, Vector2.ZERO, Vector2.ZERO, (bottom - pos.Y) / velocity.Y));
            }
            if (posNext.Y > top)
            {
                collisionBuffer.Add(new CollisionSubframeBuffer(applyParticle, new Vector2(velocity.X, -velocity.Y * BodyImpulse.coefficientElasticity), null, Vector2.ZERO, Vector2.ZERO, (top - pos.Y) / velocity.Y));
            }
        }

        public override void ApplyImpulse_DynamicToFrozen(LsmBody applyBody, Particle applyParticle, LsmBody otherBody, double timeCoefficientPrediction, ref List<CollisionSubframeBuffer> collisionBuffer)
        {
            Debug.Assert(false);
        }

        public override void ApplyImpulse_FrozenToDynamic(LsmBody otherBody, Particle otherParticle, LsmBody applyBody, double timeCoefficientPrediction, ref List<CollisionSubframeBuffer> collisionBuffer)
        {
            Debug.Assert(false);
        }

	}
}
