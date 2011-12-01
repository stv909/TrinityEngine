using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
	static class RectangleBlueprint
	{
		public static bool[,] blueprint = new bool[50, 50];

		public const int width = 15, height = 4;

		static RectangleBlueprint()
		{
			int x, y;
			for (x = 0; x < width; x++)
			{
				for (y = 0; y < height; y++)
				{
					blueprint[x, y] = true;
				}
			}
		}
	}
}
