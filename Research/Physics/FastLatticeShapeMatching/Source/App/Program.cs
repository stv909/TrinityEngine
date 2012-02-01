using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PhysicsTestbed
{
	public class Program
	{
        public static Form window = null;

        public static TestbedWindow testbedWindow = null;
        public static CoeWindow coeWindow = null;
        public static EditorWindow editorWindow = null;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Initialize(args.Length > 0 ? args[0] : string.Empty); // TODO: use MainWindow form with 3xTabPanels instead of command line

            if (window == null)
                Application.Exit();
            else
			    Application.Run(window);
		}

		public static void Initialize(string windowType)
		{
			Testbed.Initialize();

            if (windowType == "?" || windowType == "h" || windowType == "help")
            {
                MessageBox.Show(
                    "editor - run as physical editor\n" +
                    "coe - run as conservation of energy demo\n" +
                    "testbed - run as physical testbed\n" +
                    "help - show this text",
                    "LSM Prototype commands"
                );
            }
            else if (windowType == "editor")
            {
                editorWindow = new EditorWindow();
                window = editorWindow;
            }
            else if (windowType == "coe")
            {
                coeWindow = new CoeWindow();
                window = coeWindow;
            }
            else if (windowType == "main")
            {
                window = new MainWindow();
            }
            else
            {
                testbedWindow = new TestbedWindow();
                window = testbedWindow;
            }
		}
	}
}
