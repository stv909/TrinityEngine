using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    [Serializable]
    public struct Color3
    {
        double r, g, b;

        public double R { get { return r; } set { r = value; } }
        public double G { get { return g; } set { g = value; } }
        public double B { get { return b; } set { b = value; } }

        public Color3(double r, double g, double b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        static public Color3 operator *(Color3 c, double k)
        {
            return new Color3(c.R * k, c.G * k, c.B * k);
        }

        static public Color3 operator *(double k, Color3 c)
        {
            return new Color3(c.R * k, c.G * k, c.B * k);
        }
    }
}
