using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace PhysicsTestbed
{
	public class ControllableAttribute : Attribute
	{
		public enum ControllerType { Slider, Textbox, Checkbox };
		public ControllerType Type;
		public double Max, Min;
		public string Caption = "";
		public bool Integral = false;
	}

	public abstract class Controller
	{
		public ControllableAttribute att;
		public MemberInfo field;
		public ControllerPanel panel;
		public Point layoutPoint;
		public double Height = 25;

		public void Initialize(ControllableAttribute att, MemberInfo field, ControllerPanel panel, Point layoutPoint)
		{
			this.att = att;
			this.field = field;
			this.panel = panel;
			this.layoutPoint = layoutPoint;

			SetupControls();
		}

		protected double FieldValue
		{
			get
			{
				if (field is PropertyInfo)
				{
					PropertyInfo p = (PropertyInfo)field;
					return Convert.ToDouble(p.GetValue(null, null));
				}
				else if (field is FieldInfo)
				{
					FieldInfo f = (FieldInfo)field;
					return Convert.ToDouble(f.GetValue(null));
				}
				else
					return -1;
			}
			set
			{
				if (field is PropertyInfo)
				{
					PropertyInfo p = (PropertyInfo)field;
					if (p.PropertyType == typeof(double))
						p.SetValue(null, value, null);
					else if (p.PropertyType == typeof(float))
						p.SetValue(null, (float)value, null);
					else if (p.PropertyType == typeof(int))
						p.SetValue(null, (int)value, null);
					else if (p.PropertyType == typeof(bool))
						if (value != 0)
							p.SetValue(null, true, null);
						else
							p.SetValue(null, false, null);
					else
						p.SetValue(null, null, null);
				}
				else if (field is FieldInfo)
				{
					FieldInfo f = (FieldInfo)field;
					if (f.FieldType == typeof(double))
						f.SetValue(null, value);
					else if (f.FieldType == typeof(float))
						f.SetValue(null, (float)value);
					else if (f.FieldType == typeof(int))
						f.SetValue(null, (int)value);
					else if (f.FieldType == typeof(bool))
						if (value != 0)
							f.SetValue(null, true);
						else
							f.SetValue(null, false);
					else
						f.SetValue(null, null);
				}
			}
		}

		protected abstract void SetupControls();
	}

	public class SliderController : Controller
	{
		TrackBar trackBar = new TrackBar();
		Label caption = new Label();
		Label value = new Label();
		int numTicks = 100;

		protected override void SetupControls()
		{
			caption.Location = layoutPoint;
			caption.Width = 160;
			caption.Text = att.Caption + ":";
			trackBar.Location = new Point(layoutPoint.X + 160, layoutPoint.Y - 5);
			trackBar.AutoSize = false;
			if (att.Integral == true)
			{
				numTicks = (int)att.Max - (int)att.Min + 1;
				trackBar.Size = new Size(100, 25);
			}
			else
			{
				trackBar.Size = new Size(numTicks, 25);
				trackBar.TickStyle = TickStyle.None;
			}
			trackBar.Minimum = 0;
			trackBar.Maximum = numTicks;
			value.Location = new Point(layoutPoint.X + 160 + 100, layoutPoint.Y);
			value.Width = 40;
			panel.SuspendLayout();
			panel.Controls.Add(caption);
			panel.Controls.Add(trackBar);
			panel.Controls.Add(value);
			panel.ResumeLayout(false);

			Refresh();

			trackBar.ValueChanged += new EventHandler(trackBar_ValueChanged);
		}

		void Refresh()
		{
			double d = FieldValue;
			value.Text = "" + d;
			int val = (int)(((d - att.Min) / (att.Max - att.Min)) * numTicks);
			if (trackBar.Value != val)
				trackBar.Value = val;
		}

		void trackBar_ValueChanged(object sender, EventArgs e)
		{
			FieldValue = att.Min + (trackBar.Value / (double)numTicks) * (att.Max - att.Min);
			Refresh();
		}
	}

	public class TextboxController : Controller
	{
		TextBox textBox = new TextBox();
		Label caption = new Label();

		protected override void SetupControls()
		{
			caption.Location = layoutPoint;
			caption.Width = 160;
			caption.Text = att.Caption + ":";
			textBox.Location = new Point(layoutPoint.X + 160, layoutPoint.Y);
			textBox.TextAlign = HorizontalAlignment.Right;
			panel.Controls.Add(caption);
			panel.Controls.Add(textBox);

			Refresh();

			textBox.TextChanged += new EventHandler(textBox_TextChanged);
		}

		void Refresh()
		{
			textBox.Text = "" + FieldValue;
		}

		void textBox_TextChanged(object sender, EventArgs e)
		{
			double d;
			try
			{
				d = Convert.ToDouble(textBox.Text);

				if (d < att.Min) d = att.Min;
				if (d > att.Max) d = att.Max;
				if (att.Integral)
					d = (int)d;
			}
			catch
			{
				Console.Beep(1600, 5);
				d = FieldValue;
			}
			FieldValue = d;

			Refresh();
		}
	}

	public class CheckboxController : Controller
	{
		CheckBox checkBox = new CheckBox();
		Label caption = new Label();

		protected override void SetupControls()
		{
			caption.Location = layoutPoint;
			caption.Width = 160;
			caption.Text = att.Caption + ":";
			checkBox.Location = new Point(layoutPoint.X + 160, layoutPoint.Y - 5);
			panel.Controls.Add(caption);
			panel.Controls.Add(checkBox);

			Refresh();

			checkBox.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
		}

		void Refresh()
		{
			checkBox.Checked = (FieldValue != 0);
		}

		void checkBox_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox.Checked)
				FieldValue = 1;
			else
				FieldValue = 0;

			Refresh();
		}
	}
}
