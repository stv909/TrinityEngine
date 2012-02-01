using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace PhysicsTestbed
{
    public class MouseService : IService
	{
		protected bool lmbDown = false, mmbDown = false, rmbDown = false;
		protected bool oldLmbDown, oldMmbDown, oldRmbDown;
		protected bool lmbJustPressed, mmbJustPressed, rmbJustPressed;
        protected bool wheelPositive = false;
        protected bool wheelNegative = false;
        public Point mouse;
        protected Point oldMouse;

        public virtual void MouseDown(MouseButtons button)
        {
            if (button == MouseButtons.Left)
                lmbDown = true;
            else if (button == MouseButtons.Right)
                rmbDown = true;
            else if (button == MouseButtons.Middle)
                mmbDown = true;
        }

        public virtual void MouseUp(MouseButtons button)
        {
            if (button == MouseButtons.Left)
                lmbDown = false;
            else if (button == MouseButtons.Right)
                rmbDown = false;
            else if (button == MouseButtons.Middle)
                mmbDown = false;
        }

		public virtual void MouseMove(int x, int y)
		{
			mouse.X = x;
			mouse.Y = y;
		}

        public virtual void MouseWheel(int delta)
        {
            Debug.Assert(delta != 0);
            wheelPositive = delta > 0;
            wheelNegative = delta < 0;
        }

		public virtual void PreUpdate()
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
