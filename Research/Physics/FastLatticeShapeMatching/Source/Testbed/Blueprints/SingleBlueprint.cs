using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
	static class SingleBlueprint
	{
		public static bool[,] blueprint = new bool[50, 50];

        static SingleBlueprint()
		{
			blueprint[1,1] = true;
		}
	}
}
