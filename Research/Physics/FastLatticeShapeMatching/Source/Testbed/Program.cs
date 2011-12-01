using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PhysicsTestbed
{
	public class Program
	{
		public static TestWindow window;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Initialize();

			Application.Run(window);
		}

		public static void Initialize()
		{
			Testbed.Initialize();
			window = new TestWindow();
		}
	}
}
