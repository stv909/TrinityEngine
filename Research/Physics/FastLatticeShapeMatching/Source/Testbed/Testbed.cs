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

		public static LsmBody testBody;
        public static Array currentBlueprints = null;

		public static DragForce dragForce;
        //public static WallForce wallForce;
        public static PushForce pushForce;
		public static LockForce lockForce;
		public static GravityForce gravityForce;
        public static WallImpulse wallImpulse;
        public static BodyImpulse bodyImpulse;

		public static bool paused = false;
		public static int updateFrame = 0;

		//[Controllable(Type = ControllableAttribute.ControllerType.Checkbox, Caption="Walls closing in")]
		public static bool wallsClosingIn = false;

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
            // SimpleX1Blueprint; SimpleX2Blueprint; SimpleX3Blueprint; RectangleBlueprint; HumanBlueprint; BuildingBlueprint; ChairBlueprint;
            currentBlueprints = new Array[2] { SimpleX2Blueprint.blueprint, SimpleX1Blueprint.blueprint };
            //currentBlueprints = new Array[5] { SimpleX2Blueprint.blueprint, SimpleX1Blueprint.blueprint, RectangleBlueprint.blueprint, RectangleBlueprint.blueprint, RectangleBlueprint.blueprint };
            GenerateBodies(currentBlueprints);
            
            dragForce = new DragForce();
			pushForce = new PushForce();
			lockForce = new LockForce();
			gravityForce = new GravityForce();
			wallImpulse = new WallImpulse(9999, 9999);
            bodyImpulse = new BodyImpulse(testBody);

			world.environmentForces.Add(dragForce);
			world.environmentForces.Add(pushForce);
			world.environmentForces.Add(lockForce);
			world.environmentForces.Add(gravityForce);
            world.environmentImpulses.Add(wallImpulse);
            world.environmentImpulses.Add(bodyImpulse);
        }

        public static void GenerateBodies(Array blueprints)
        {
            testBody = null;
            int verticalOffset = 0;
            foreach (bool[,] blueprint in blueprints)
            {
                LsmBody body = new LsmBody(
                    new Vector2(50, 50 + 150 * verticalOffset), 
                    new Color3(verticalOffset == 1 ? 1 : 0, verticalOffset == 0 ? 1 : 0, verticalOffset == 2 ? 1 : 0)
                );
                world.bodies.Add(body);
                ++verticalOffset;
                body.GenerateFromBlueprint(blueprint);
                if (testBody == null)
                    testBody = body;
            }
        }

		public static void Reset()
		{
            world.bodies.Clear();
            GenerateBodies(currentBlueprints);
            bodyImpulse.body = testBody;
        }

		public static void SetModel(int blueprintNo, int modelNo)
		{
			switch(modelNo)
			{
				case 1:
                    currentBlueprints.SetValue(RectangleBlueprint.blueprint, blueprintNo);
					break;
				case 2:
                    currentBlueprints.SetValue(HumanBlueprint.blueprint, blueprintNo);
					break;
				case 3:
                    currentBlueprints.SetValue(ChairBlueprint.blueprint, blueprintNo);
					break;
				case 4:
                    currentBlueprints.SetValue(SimpleX2Blueprint.blueprint, blueprintNo);
					break;
                case 5:
                    currentBlueprints.SetValue(SimpleX1Blueprint.blueprint, blueprintNo);
                    break;
            }
			Reset();
		}

		public static void Update()
		{
			if(wallsClosingIn)
			{
				wallImpulse.bottom++;
				wallImpulse.top--;
				//wallForce.left++;
				//wallForce.right--;
			}

			world.Update();
		}

		public static void PauseStep()
		{
			paused = false;
			Update();
			paused = true;
		}

		public static void PostMessage(string message)
		{
			Program.window.statusBox.PostMessage(Color.Black, message);
		}

		public static void PostMessage(Color color, string message)
		{
			Program.window.statusBox.PostMessage(color, message);
		}
	}
}
