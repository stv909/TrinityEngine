using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
	[Serializable]
	public struct Vector2
	{
		double x, y;

		static public Vector2 ZERO
		{
			get
			{
				return new Vector2(0, 0);
			}
		}

		public double X
		{
			get
			{
				return x;
			}
			set
			{
				x = value;
			}
		}

		public double Y
		{
			get
			{
				return y;
			}
			set
			{
				y = value;
			}
		}

		/*
		public Vector2()
		{
			X = 0;
			Y = 0;
		}
		*/

		public Vector2(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public void Normalize()
		{
			double rlen = 1.0 / Length();
			x *= rlen;
			y *= rlen;
		}

		public Vector2 NormalizedCopy()
		{
			Vector2 ret = this;
			ret.Normalize();
			return ret;
		}

		public Vector2 RotateVector(Vector2 r)
		{
			Vector2 xVec = this;
			Vector2 yVec = new Vector2(-Y, X);
			return (r.X * xVec + r.Y * yVec);
		}

		public double Length()
		{
			return Math.Sqrt(X * X + Y * Y);
		}

		public double LengthSq()
		{
			return (X * X + Y * Y);
		}

		public double Dot(Vector2 r)
		{
			return (x * r.X + y * r.Y);
		}

		static public double Dot(Vector2 a, Vector2 b)
		{
			return a.Dot(b);
		}

		static public Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.X, -a.Y);
		}

		static public Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.X - b.X, a.Y - b.Y);
		}

		static public Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.X + b.X, a.Y + b.Y);
		}

		static public Vector2 operator *(Vector2 a, double b)
		{
			return new Vector2(a.X * b, a.Y * b);
		}

		static public Vector2 operator *(double b, Vector2 a)
		{
			return new Vector2(a.X * b, a.Y * b);
		}

		static public Vector2 operator /(Vector2 a, double b)
		{
			return new Vector2(a.X / b, a.Y / b);
		}

		static public bool operator ==(Vector2 a, Vector2 b)
		{
			return ((object)a != null && (object)b != null && a.X == b.X && a.Y == b.Y);
		}

		static public bool operator !=(Vector2 a, Vector2 b)
		{
			return ((object)a == null || (object)b == null || a.X != b.X || a.Y != b.Y);
		}

		public override string ToString()
		{
			return "(X: " + X + ", Y: " + Y + ")";
		}

		public override bool Equals(object obj)
		{
			if (obj is Vector2)
			{
				Vector2 v = (Vector2)obj;
				return (obj != null && X == v.X && Y == v.Y);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

        public double CrossProduct(Vector2 r)
        {
            return x * r.Y - y * r.X;
        }
	}

    // TODO: move it to separated file Color3.cs
    [Serializable]
    public struct Color3
    { 
        double r, g, b;

		public double R { get{ return r; } set{ r = value; } }
		public double G { get{ return g; } set{ g = value; } }
		public double B { get{ return b; } set{ b = value; } }

        public Color3(double r, double g, double b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }
}
