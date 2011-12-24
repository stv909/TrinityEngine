using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    namespace Blueprints
    {
        static class SimpleX1
        {
            public static bool[,] blueprint = new bool[50, 50];

            static SimpleX1()
            {
                blueprint[1, 1] = true;
            }
        }
        static class SimpleX2
        {
            public static bool[,] blueprint = new bool[50, 50];

            static SimpleX2()
            {
                blueprint[1, 1] = true;
                blueprint[2, 1] = true;
            }
        }
        static class SimpleX3
        {
            public static bool[,] blueprint = new bool[50, 50];

            static SimpleX3()
            {
                blueprint[1, 1] = true;
                blueprint[2, 1] = true;
                blueprint[3, 1] = true;
            }
        }
    }
}
