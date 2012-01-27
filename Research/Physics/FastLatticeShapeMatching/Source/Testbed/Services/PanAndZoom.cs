using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PhysicsTestbed
{
    public class PanAndZoom : MouseForce // TODO: use MouseService instead of MouseForce base class
    {
        public override void ApplyForce(IEnumerable particles)
        { 
        }

        double zoomMultiplier = 1.33;

        public override void Update()
        {
            if (wheelPositive || wheelNegative)
            {
                double priorZoom = Testbed.screenZoom;
                if (wheelPositive)
                    Testbed.screenZoom /= zoomMultiplier;
                else
                    Testbed.screenZoom *= zoomMultiplier;
                Testbed.screenTranslate -= (Testbed.screenZoom - priorZoom) * new Vector2(mouse.X, mouse.Y);
            }
            if (mmbDown)
            {
                Testbed.screenTranslate -= Testbed.screenZoom * new Vector2(mouse.X - oldMouse.X, mouse.Y - oldMouse.Y);
            }

            base.Update();
        }
    }
}
