using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
	public class PushParticleGroup : MouseForce
	{
		const float PUSH_DIST = 35f;

		public bool Pushing
		{
			get { return rmbDown && Program.testbedWindow.rmbPushRadioButton.Checked; }
		}

		public Point PushPosition
		{
			get { return mouse; }
		}

		public float PushDistance
		{
			get { return PUSH_DIST; }
		}

		public override void ApplyForce(IEnumerable particles)
		{
			Vector2 mouseVec = new Vector2(mouse.X, mouse.Y);

			if (Pushing)
			{
				foreach (Particle t in particles)
				{
					double dist = (t.goal - mouseVec).Length();
					if (dist < PUSH_DIST)
					{
						if(!Testbed.Paused)
						{
							Vector2 v = (t.goal - mouseVec);
							v.Normalize();
							t.fExt += 2 * (mouseVec + v * PUSH_DIST - t.x);
						}
					}
				}
			}
		}
	}
}
