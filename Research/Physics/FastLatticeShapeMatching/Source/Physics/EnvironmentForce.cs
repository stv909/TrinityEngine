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
}
