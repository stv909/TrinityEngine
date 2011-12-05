using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PhysicsTestbed
{
	public class PanAndZoom : MouseForce
    {
        public override void ApplyForce(IEnumerable particles)
        { 
        }

        public override void Update()
        {
            if (wheelPositive)
            {
                Testbed.screenZoom /= 1.33;
            }
            if (wheelNegative)
            {
                Testbed.screenZoom *= 1.33;
            }
            if (mmbDown)
            {
                Testbed.screenTranslate -= new Vector2(mouse.X - oldMouse.X, mouse.Y - oldMouse.Y);
            }

            base.Update();
        }
    }
}
