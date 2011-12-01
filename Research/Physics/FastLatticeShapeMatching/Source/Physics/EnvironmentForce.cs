using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
	public abstract class EnvironmentForce
	{
		public abstract void ApplyForce(IEnumerable particles);
		public virtual void Update() { }
        public virtual void PostUpdate() { }
    }
}
