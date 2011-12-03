using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    static class SimpleX1Blueprint
    {
        public static bool[,] blueprint = new bool[50, 50];

        static SimpleX1Blueprint()
        {
            blueprint[1, 1] = true;
        }
    }
    static class SimpleX2Blueprint
    {
        public static bool[,] blueprint = new bool[50, 50];

        static SimpleX2Blueprint()
        {
            blueprint[1, 1] = true;
            blueprint[2, 1] = true;
        }
    }
    static class SimpleX3Blueprint
    {
        public static bool[,] blueprint = new bool[50, 50];

        static SimpleX3Blueprint()
        {
            blueprint[1, 1] = true;
            blueprint[2, 1] = true;
            blueprint[3, 1] = true;
        }
    }
}
