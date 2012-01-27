using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace PhysicsTestbed
{
    public abstract class MouseService : Updatable
	{
		protected bool lmbDown = false, mmbDown = false, rmbDown = false;
		protected bool oldLmbDown, oldMmbDown, oldRmbDown;
		protected bool lmbJustPressed, mmbJustPressed, rmbJustPressed;
        protected bool wheelPositive = false;
        protected bool wheelNegative = false;
        public Point mouse;
        protected Point oldMouse;

		public virtual void LmbDown()
		{
			lmbDown = true;
		}

		public virtual void LmbUp()
		{
			lmbDown = false;
		}

        public virtual void MmbDown()
        {
            mmbDown = true;
        }

        public virtual void MmbUp()
        {
            mmbDown = false;
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

        public virtual void Wheel(int delta)
        {
            Debug.Assert(delta != 0);
            wheelPositive = delta > 0;
            wheelNegative = delta < 0;
        }

		public virtual void Update()
		{
			lmbJustPressed = (lmbDown && !oldLmbDown);
            mmbJustPressed = (mmbDown && !oldMmbDown);
            rmbJustPressed = (rmbDown && !oldRmbDown);

			oldLmbDown = lmbDown;
            oldMmbDown = mmbDown;
            oldRmbDown = rmbDown;
            oldMouse = mouse;

            wheelPositive = false;
            wheelNegative = false;
		}

        public virtual void PostUpdate()
        {
        }
	}
}
