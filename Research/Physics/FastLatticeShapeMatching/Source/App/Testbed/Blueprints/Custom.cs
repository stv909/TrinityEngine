using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    namespace Blueprints
    {
        static class Custom
        {
            public static bool[,] blueprint = new bool[50, 50]; // TODO: use dynamic array here for all blueprints

            static Custom()
            {
                // TODO: implement loading data from file
            }
        }
    }
}
