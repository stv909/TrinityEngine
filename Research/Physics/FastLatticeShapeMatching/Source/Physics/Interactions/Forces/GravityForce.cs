using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
	public class GravityForce : IEnvironmentForce
	{
		[Controllable(Type = ControllableAttribute.ControllerType.Slider, Caption = "Gravity", Min = 0.0, Max = 1.0)]
		public static double gravity = 0.0;

		public virtual void ApplyForce(IEnumerable particles)
		{
			if (gravity != 0.0)
			{
				foreach (Particle t in particles)
				{
					t.fExt += new Vector2(0, -gravity);
				}
			}
		}
	}
}
