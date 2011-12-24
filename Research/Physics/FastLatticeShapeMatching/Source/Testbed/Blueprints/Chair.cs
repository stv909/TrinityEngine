using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    namespace Blueprints
    {
        static class Chair
        {
            public static bool[,] blueprint = new bool[50, 50];

            static Chair()
            {
                int x, y;

                for (y = 4; y <= 20; y++)
                {
                    for (x = 3; x <= 4; x++)
                    {
                        blueprint[x, y] = true;
                    }
                }
                for (y = 4; y <= 10; y++)
                {
                    for (x = 10; x <= 11; x++)
                    {
                        blueprint[x, y] = true;
                    }
                }
                for (y = 9; y <= 10; y++)
                {
                    for (x = 3; x <= 11; x++)
                    {
                        blueprint[x, y] = true;
                    }
                }
            }
        }
    }
}
