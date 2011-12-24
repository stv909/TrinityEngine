using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    namespace Blueprints
    {
        static class Building
        {
            public static bool[,] blueprint = new bool[50, 50];

            public const int width = 10, height = 20;

            static Building()
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
