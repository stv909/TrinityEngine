using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
	public class LockForce : MouseForce
	{
		public override void ApplyForce(IEnumerable particles)
		{
			Vector2 mouseVec = new Vector2(mouse.X, mouse.Y);

			if (rmbJustPressed && Program.window.rmbLockRadioButton.Checked)
			{
				double minDist = 50;
				Particle closestParticle = null;

				foreach (Particle t in particles)
				{
					double dist = (t.goal - mouseVec).Length();
					if (dist < minDist)
					{
						minDist = dist;
						closestParticle = t;
					}
				}

				if (closestParticle != null)
				{
					closestParticle.locked = !closestParticle.locked;
					if (closestParticle.locked)
						closestParticle.mass = 1000;
					else
						closestParticle.mass = 1;
					closestParticle.body.CalculateInvariants();
				}
			}
		}
	}
}
