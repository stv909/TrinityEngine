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
            LsmBody applyBody, Particle applyParticle, LsmBody otherBody, // HACK // TODO: try to don't use such information for collisions or formilize this ussage
            double timeCoefficientPrediction, ref List<CollisionSubframeBuffer> collisionBuffer // HACK // TODO: remove ref List<>
        );

        public abstract void ApplyImpulse_FrozenToDynamic( // HACK // TODO: implement it in elegant way
            LsmBody otherBody, Particle otherParticle, LsmBody applyBody, // HACK // TODO: try to don't use such information for collisions or formilize this ussage
            double timeCoefficientPrediction, ref List<CollisionSubframeBuffer> collisionBuffer // HACK // TODO: remove ref List<>
        );

        public abstract void ApplyImpulse_DynamicToFrozen( // HACK // TODO: implement it in elegant way
            LsmBody applyBody, Particle applyParticle, LsmBody otherBody, // HACK // TODO: try to don't use such information for collisions or formilize this ussage
            double timeCoefficientPrediction, ref List<CollisionSubframeBuffer> collisionBuffer // HACK // TODO: remove ref List<>
        );
    }


}
