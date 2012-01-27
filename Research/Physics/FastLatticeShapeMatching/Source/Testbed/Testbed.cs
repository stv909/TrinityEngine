using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Tao.OpenGl;

namespace PhysicsTestbed
{
	public partial class Testbed
	{
		public static World world = World.Singleton;
        public static Array blueprintsCollection = null;
        public static Array bodiesCollection = null;

        // forces
        public static GravityForce gravityForce;
        public static WallForce wallForce;

        // services
        public static PanAndZoom panAndZoom;

        // mouse forces
        public static DragParticle dragParticle;
		public static LockParticle lockParticle;
        public static PushParticleGroup pushParticle;


		public static bool paused = false;
		public static int updateFrame = 0;

		public static bool Paused
		{
			get { return paused; }
			set 
            { 
				if (paused != value)
				{
					paused = value;
					if (paused) Program.window.runCheckBox.Checked = false;
				}
			}
		}

		public static void Initialize()
		{
            blueprintsCollection = new Array[7] { 
                Blueprints.SimpleX1.blueprint, Blueprints.SimpleX2.blueprint, Blueprints.SimpleX3.blueprint, 
                Blueprints.Rectangle.blueprint, Blueprints.Building.blueprint, Blueprints.Chair.blueprint, Blueprints.Human.blueprint 
            };

            // SimpleX1; SimpleX2; SimpleX3; Rectangle; Human; Building; Chair; //
            bodiesCollection = new Array[2] { Blueprints.SimpleX1.blueprint, Blueprints.SimpleX2.blueprint };
            //currentBlueprints = new Array[5] { Blueprints.SimpleX2.blueprint, Blueprints.SimpleX1.blueprint, Blueprints.Rectangle.blueprint, Blueprints.Rectangle.blueprint, Blueprints.Rectangle.blueprint };
            GenerateBodies(bodiesCollection);
            
            dragParticle = new DragParticle();
            wallForce = new WallForce(9999, 9999);
			pushParticle = new PushParticleGroup();
			lockParticle = new LockParticle();
			gravityForce = new GravityForce();
            panAndZoom = new PanAndZoom();

            world.environmentForces.Add(wallForce);
			world.environmentForces.Add(gravityForce);

            world.environmentForces.Add(dragParticle);
            world.forceServices.Add(dragParticle);
			world.environmentForces.Add(lockParticle);
            world.forceServices.Add(lockParticle);
            world.environmentForces.Add(pushParticle);
            world.forceServices.Add(pushParticle);

            world.interactionServices.Add(panAndZoom);

            world.mouseServices.Add(dragParticle);
            world.mouseServices.Add(pushParticle);
            world.mouseServices.Add(lockParticle);
            world.mouseServices.Add(panAndZoom);
        }

        public static void PostMessage(string message)
        {
            Program.window.statusBox.PostMessage(Color.Black, message);
        }

        public static void PostMessage(Color color, string message)
        {
            Program.window.statusBox.PostMessage(color, message);
        }

        static void MakeCheckBoxDataBinding(TestWindow window, string checkBoxName, string checkBoxPropertyName, LsmBody body, string bodyPropertyName)
        {
            System.Windows.Forms.Control[] controls = window.Controls.Find(checkBoxName, true);
            if (controls.Length > 0 && controls.GetValue(0) != null && controls.GetValue(0) is System.Windows.Forms.CheckBox)
            {
                System.Windows.Forms.CheckBox checkBox = controls.GetValue(0) as System.Windows.Forms.CheckBox;
                checkBox.DataBindings.Clear();
                checkBox.DataBindings.Add(checkBoxPropertyName, body, bodyPropertyName).DataSourceUpdateMode = System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged;
            }
        }

        public static void MakeDataBindingForBody(TestWindow window, LsmBody body, int bodyIndex)
        {
            // TODO: make this method suitable for N bodies - not only for 2 of them
            if (window != null && (bodyIndex == 0 || bodyIndex == 1))
            {
                MakeCheckBoxDataBinding(window, "model0" + (bodyIndex + 1) + "DisableCollisions", "Checked", body, "UseWallForce");
                MakeCheckBoxDataBinding(window, "model0" + (bodyIndex + 1) + "Freeze", "Checked", body, "Frozen");
            }
        }

        private static LsmBody GenerateBody(int verticalIndex, bool[,] blueprint)
        {
            LsmBody body = new LsmBody(
                new Vector2(50, 50 + 150 * verticalIndex),
                new Point(LsmBody.blueprintSpacingX, LsmBody.blueprintSpacingY),
                new Color3(verticalIndex == 1 ? 1 : 0, verticalIndex == 0 ? 1 : 0, verticalIndex == 2 ? 1 : 0)
            );
            body.GenerateFromBlueprint(blueprint);
            if (verticalIndex < 2)
            {
                MakeDataBindingForBody(Program.window, body, verticalIndex);
            }
            return body;
        }

        public static void GenerateBodies(Array blueprints)
        {
            int verticalIndex = 0;
            foreach (bool[,] blueprint in blueprints)
            {
                LsmBody body = GenerateBody(verticalIndex, blueprint);
                world.bodies.Add(body);
                ++verticalIndex;
            }
        }

        private static void RegenerateBody(int bodyIndex, bool[,] blueprint)
        {
            if (bodyIndex < 0 || bodyIndex >= world.bodies.Count)
            {
                PostMessage(Color.Red, "Invalud world.body index: " + bodyIndex + ". Failed to regenerate body.");
                return;
            }
            LsmBody body = GenerateBody(bodyIndex, blueprint);
            world.bodies[bodyIndex] = body;
        }

		public static void Reset()
		{
            world.bodies.Clear();
            GenerateBodies(bodiesCollection);
        }

		public static void SetModel(int bodyIndex, int blueprintIndex)
		{
            if (blueprintIndex < 0 || blueprintIndex >= blueprintsCollection.Length)
            {
                PostMessage(Color.Red, "Invalud blueprint index: " + blueprintIndex + ". Failed to generate body." );
                return;
            }
            bodiesCollection.SetValue(blueprintsCollection.GetValue(blueprintIndex), bodyIndex);
            RegenerateBody(bodyIndex, bodiesCollection.GetValue(bodyIndex) as bool[,]);
		}

		public static void Update()
		{
			world.Update();
		}
	}
}
