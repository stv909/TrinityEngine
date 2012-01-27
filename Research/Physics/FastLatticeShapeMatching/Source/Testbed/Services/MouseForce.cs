using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace PhysicsTestbed
{
	public abstract class MouseForce : MouseService, EnvironmentForce
	{
        public virtual void ApplyForce(IEnumerable particles)
        {
        }
	}
}
