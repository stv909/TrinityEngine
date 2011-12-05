using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    public abstract class Updatable
    {
        public virtual void Update() { }
        public virtual void PostUpdate() { }
    }

    public abstract class EnvironmentForce : Updatable
	{
		public abstract void ApplyForce(IEnumerable particles);
    }

    public abstract class EnvironmentImpulse
    {
        public abstract void ApplyImpulse(
            LsmBody applyBody, Particle applyParticle, // HACK // TODO: try to don't use such information for collisions or formilize this ussage
            Vector2 pos, Vector2 posNext, Vector2 velocity, ref List<CollisionSubframeBuffer> collisionBuffer, double accumulatedSubframeTime
        );
    }


}
