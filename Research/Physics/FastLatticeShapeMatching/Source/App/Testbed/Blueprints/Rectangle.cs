using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    namespace Blueprints
    {
        static class Rectangle
        {
            public static bool[,] blueprint = new bool[50, 50];

            public const int width = 15, height = 4;

            static Rectangle()
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
}
