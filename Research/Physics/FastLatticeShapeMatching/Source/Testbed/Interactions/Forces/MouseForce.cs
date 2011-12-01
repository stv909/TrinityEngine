using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PhysicsTestbed
{
	public abstract class MouseForce : EnvironmentForce
	{
		protected bool lmbDown = false, rmbDown = false;
		protected bool oldLmbDown, oldRmbDown;
		protected bool lmbJustPressed, rmbJustPressed;
		public Point mouse;

		public virtual void LmbDown()
		{
			lmbDown = true;
		}

		public virtual void LmbUp()
		{
			lmbDown = false;
		}

		public virtual void RmbDown()
		{
			rmbDown = true;
		}

		public virtual void RmbUp()
		{
			rmbDown = false;
		}

		public virtual void MouseMove(int x, int y)
		{
			mouse.X = x;
			mouse.Y = y;
		}

		public override void Update()
		{
			base.Update();

			lmbJustPressed = (lmbDown && !oldLmbDown);
			rmbJustPressed = (rmbDown && !oldRmbDown);

			oldLmbDown = lmbDown;
			oldRmbDown = rmbDown;
		}
	}
}
