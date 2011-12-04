using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PhysicsTestbed
{
	public class DragForce : MouseForce
	{
		public Particle selected;
		new bool oldLmbDown = false;
        bool dragBodyFound = false;

		public override void ApplyForce(IEnumerable particles)
		{
            if (dragBodyFound)
                return;

			const int CUTOFF = 50;
			const float PAUSED_MULT = 2f;
			Vector2 mouseVec = new Vector2(mouse.X, mouse.Y);

			if (lmbDown == true && oldLmbDown == false)
			{
				selected = null;
				double closestDist = CUTOFF;

				foreach (Particle t in particles)
				{
					double dist = (t.goal - mouseVec).Length();
					if (dist < closestDist)
					{
						closestDist = dist;
						selected = t;
					}
				}
			}

			if (selected != null)
			{
				if (selected.locked == false)
				{
					if (!Testbed.Paused && lmbDown)
					{
						selected.fExt += (mouseVec - selected.x) * selected.mass;
						selected.v = Vector2.ZERO;
					}
					else if (Testbed.Paused && !lmbDown && oldLmbDown == true)
					{
						selected.fExt += (mouseVec - selected.x) * PAUSED_MULT * selected.mass;
					}
				}
				else if(lmbDown)
				{
					selected.x = mouseVec;
                    selected.v = Vector2.ZERO;
				}
                dragBodyFound = true;
			}
		}

        public override void PostUpdate()
        {
            base.Update();

            oldLmbDown = lmbDown;
            dragBodyFound = false;
        }

		public bool Dragging
		{
			get 
			{
				return (lmbDown && selected != null);
			}
		}
	}
}
