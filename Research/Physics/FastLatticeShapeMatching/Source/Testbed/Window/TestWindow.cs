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

            Testbed.world.bodyWallRepulse.SetDimensions(renderBox.Width, renderBox.Height);
            Testbed.wallForce.SetDimensions(renderBox.Width, renderBox.Height);

            for (int i = 0; i < Testbed.world.bodies.Count; ++i)
            {
                Testbed.MakeDataBindingForBody(this, Testbed.world.bodies[i], i);
            }

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

            // Update interaction services
            foreach (EnvironmentForce srv in Testbed.world.interactionServices)
            {
                srv.Update();
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
            foreach (MouseForce d in Testbed.world.mouseForces)
			{
                if (e.Button == MouseButtons.Left)
                    d.LmbDown();
                else if (e.Button == MouseButtons.Right)
                    d.RmbDown();
                else if (e.Button == MouseButtons.Middle)
                    d.MmbDown();
			}
		}

		private void renderBox_MouseUp(object sender, MouseEventArgs e)
		{
            foreach (MouseForce d in Testbed.world.mouseForces)
			{
				if (e.Button == MouseButtons.Left)
					d.LmbUp();
				else if (e.Button == MouseButtons.Right)
					d.RmbUp();
                else if (e.Button == MouseButtons.Middle)
                    d.MmbUp();
			}
		}

		private void renderBox_MouseMove(object sender, MouseEventArgs e)
		{
            foreach (MouseForce d in Testbed.world.mouseForces)
			{
    			d.MouseMove(e.X, this.renderBox.Height - e.Y);
			}
		}

        private void renderBox_MouseWheel(object sender, MouseEventArgs e)
        {
            foreach (MouseForce d in Testbed.world.mouseForces)
            {
                if (e.Delta != 0) d.Wheel(e.Delta);
            }
        }

		private void pauseStepButton_MouseDown(object sender, MouseEventArgs e)
		{
			pauseStepFrame = 0;
			Testbed.Update();
		}

		private void renderBox_Resize(object sender, EventArgs e)
		{
            Testbed.world.bodyWallRepulse.SetDimensions(renderBox.Width, renderBox.Height);
            Testbed.wallForce.SetDimensions(renderBox.Width, renderBox.Height);
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
			Testbed.SetModel(0, 0);
		}

		private void model12Button_Click(object sender, EventArgs e)
		{
            Testbed.SetModel(0, 1);
		}

		private void model13Button_Click(object sender, EventArgs e)
		{
            Testbed.SetModel(0, 2);
		}

		private void model14Button_Click(object sender, EventArgs e)
		{
            Testbed.SetModel(0, 3);
		}

        private void model15Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(0, 4);
        }

        private void model16Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(0, 5);
        }

        private void model17Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(0, 6);
        }

        private void model21Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(1, 0);
        }

        private void model22Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(1, 1);
        }

        private void model23Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(1, 2);
        }

        private void model24Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(1, 3);
        }

        private void model25Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(1, 4);
        }

        private void model26Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(1, 5);
        }
        
        private void model27Button_Click(object sender, EventArgs e)
        {
            Testbed.SetModel(1, 6);
        }

        private void originButton_Click(object sender, EventArgs e)
        {
            Testbed.screenTranslate = new Vector2(0.0, 0.0);
            Testbed.screenZoom = 1.0;
        }
    }
}