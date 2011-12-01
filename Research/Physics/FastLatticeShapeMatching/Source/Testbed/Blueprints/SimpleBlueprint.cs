using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
	static class SimpleBlueprint
	{
		public static bool[,] blueprint = new bool[50, 50];

		static SimpleBlueprint()
		{
			blueprint[1,1] = true;
			blueprint[2,1] = true;
			blueprint[3,1] = true;
		}
	}
}
