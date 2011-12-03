using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PhysicsTestbed
{
	public partial class TestWindow : Form
	{
		int pauseStepFrame = 0;

		Vector2 centerOfMass;

		Vector2 velocity = new Vector2(0f, 0f);

		public TestWindow()
		{
			InitializeComponent();
			Testbed.wallImpulse.right = renderBox.Width;
			Testbed.wallImpulse.top = renderBox.Height;
            Testbed.wallForce.right = renderBox.Width;
            Testbed.wallForce.top = renderBox.Height;

			statusBox.PostMessage(Color.Green, "Simulation started");
		}

		private void TimerTick(object sender, EventArgs e)
		{
			pauseStepFrame++;

			Testbed.paused = true;

			if (pauseStepButton.Capture && pauseStepFrame % 20 == 0 && pauseStepFrame > 0)
			{
				Testbed.Update();
			}
			else if (runButton.Capture || runCheckBox.Checked)
			{
				Testbed.paused = false;
				Testbed.Update();
			}
			else
			{
				// Apply the drag force, even if we are paused
				foreach (LsmBody body in Testbed.world.bodies)
				{
					Testbed.dragForce.ApplyForce(body.particles);
				}
			}

			this.Render();
			UpdatePanels();
		}

		private void UpdatePanels()
		{
			LsmBody b;
			centerOfMass = new Vector2(0f, 0f);
			Vector2 momentum = new Vector2(0f, 0f);
			Vector2 oldCenterOfMass = new Vector2(0f, 0f);
			double kineticEnergy = 0;
			double angularMomentum = 0;
			double momentumMag;

            b = Testbed.world.bodies[0];

			foreach (Particle t in b.particles)
			{
				centerOfMass += t.x;
				kineticEnergy += 0.5 * 1 * t.v.LengthSq();
				momentum += t.v;
			}
			centerOfMass *= 1f / b.particles.Count;
			momentum *= 1f / b.particles.Count;
			momentumMag = momentum.Length();

			foreach (Particle p in b.particles)
			{
				angularMomentum += p.x.CrossProduct(p.v);
			}

			centerOfMassX.Text = "" + centerOfMass.X;
			centerOfMassY.Text = "" + centerOfMass.Y;
			linearMomentumX.Text = "" + Math.Round(momentum.X, 3);
			linearMomentumY.Text = "" + Math.Round(momentum.Y, 3);
			this.angularMomentum.Text = "" + Math.Round(angularMomentum,3);
		}

		private void renderBox_MouseDown(object sender, MouseEventArgs e)
		{
			foreach (EnvironmentForce ef in Testbed.world.environmentForces)
			{
				if (ef is MouseForce)
				{
					MouseForce d = (MouseForce)ef;
					if (e.Button == MouseButtons.Left)
						d.LmbDown();
					else if (e.Button == MouseButtons.Right)
						d.RmbDown();
				}
			}
		}

		private void renderBox_MouseUp(object sender, MouseEventArgs e)
		{
			foreach (EnvironmentForce ef in Testbed.world.environmentForces)
			{
				if (ef is MouseForce)
				{
					MouseForce d = (MouseForce)ef;
					if (e.Button == MouseButtons.Left)
						d.LmbUp();
					else if (e.Button == MouseButtons.Right)
						d.RmbUp();
				}
			}
		}

		private void renderBox_MouseMove(object sender, MouseEventArgs e)
		{
			foreach (EnvironmentForce ef in Testbed.world.environmentForces)
			{
				if (ef is MouseForce)
				{
					MouseForce d = (MouseForce)ef;
					d.MouseMove(e.X, this.renderBox.Height - e.Y);
				}
			}
		}

		private void runButton_Click(object sender, EventArgs e)
		{
			Testbed.paused = false;
		}

		private void pauseStepButton_MouseDown(object sender, MouseEventArgs e)
		{
			pauseStepFrame = 0;
			//Program.instance.testbed.PauseStep();
			//statusBox.PostMessage(Color.Blue, "Step");
			Testbed.Update();
		}

		private void renderBox_Resize(object sender, EventArgs e)
		{
			Testbed.wallImpulse.right = renderBox.Width;
			Testbed.wallImpulse.top = renderBox.Height;

            Testbed.wallForce.right = renderBox.Width;
            Testbed.wallForce.top = renderBox.Height;
        }

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			Testbed.Reset();
		}

		private void model11Button_Click(object sender, EventArgs e)
		{
			Testbed.SetModel(0, 1);
		}

		private void model12Button_Click(object sender, EventArgs e)
		{
            Testbed.SetModel(0, 2);
		}

		private void model13Button_Click(object sender, EventArgs e)
		{
            Testbed.SetModel(0, 3);
		}

		private void model14Button_Click(object sender, EventArgs e)
		{
            Testbed.SetModel(0, 4);
		}

        private void model15Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(0, 5);
        }

        private void model21Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(1, 1);
        }

        private void model22Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(1, 2);
        }

        private void model23Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(1, 3);
        }

        private void model24Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(1, 4);
        }

        private void model25Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(1, 5);
        }

        private void model02DisableCollisions_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Testbed.world.bodies[1].pureForces = cb.Checked;
        }
        
        private void model01DisableCollisions_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Testbed.world.bodies[0].pureForces = cb.Checked;
        }

        private void model02Freeze_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Testbed.world.bodies[1].frozen = cb.Checked;
        }

        private void model01Freeze_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Testbed.world.bodies[0].frozen = cb.Checked;
        }
    }
}