using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace PhysicsTestbed
{
	public class ControllerPanel : Panel
	{
		public List<Controller> controllers = new List<Controller>();
		Point nextLayoutPoint = new Point(10, 10);
		bool initialized = false;

		public ControllerPanel()
		{
			this.Layout += new LayoutEventHandler(ControllerPanel_Layout);
		}

		void ControllerPanel_Layout(object sender, LayoutEventArgs e)
		{
			SetupControllers();
		}

		public void SetupControllers()
		{
			if (initialized) return;
			initialized = true;

			foreach(Type t in Assembly.GetAssembly(this.GetType()).GetTypes())
			{
				foreach (PropertyInfo field in t.GetProperties())
				{
					foreach (Attribute att in field.GetCustomAttributes(false))
					{
						if (att is ControllableAttribute)
						{
							ControllableAttribute ca = (ControllableAttribute)att;
							Controller c = null;
							if (ca.Type == ControllableAttribute.ControllerType.Slider)
							{
								c = new SliderController();
							}
							else if (ca.Type == ControllableAttribute.ControllerType.Textbox)
							{
								c = new TextboxController();
							}
							else if (ca.Type == ControllableAttribute.ControllerType.Checkbox)
							{
								c = new CheckboxController();
							}
							if (c != null)
							{
								c.Initialize(ca, field, this, nextLayoutPoint);
								controllers.Add(c);
								nextLayoutPoint = new Point(nextLayoutPoint.X, nextLayoutPoint.Y + (int)c.Height);
							}
						}
					}
				}
				foreach(FieldInfo field in t.GetFields())
				{
					foreach(Attribute att in field.GetCustomAttributes(false))
					{
						if(att is ControllableAttribute)
						{
							ControllableAttribute ca = (ControllableAttribute)att;
							Controller c = null;
							if(ca.Type == ControllableAttribute.ControllerType.Slider)								
							{
								c = new SliderController();
							}
							else if (ca.Type == ControllableAttribute.ControllerType.Textbox)
							{
								c = new TextboxController();
							}
							else if (ca.Type == ControllableAttribute.ControllerType.Checkbox)
							{
								c = new CheckboxController();
							}
							if (c != null)
							{
								c.Initialize(ca, field, this, nextLayoutPoint);
								controllers.Add(c);
								nextLayoutPoint = new Point(nextLayoutPoint.X, nextLayoutPoint.Y + (int)c.Height);
							}
						}
					}
				}
			}
		}
	}
}
