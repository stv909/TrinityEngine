using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    [Serializable]
    public class Matrix2x2
    {
        double[,] m = new double[2,2];

        static public Matrix2x2 ZERO
        {
            get
            {
                return new Matrix2x2(0, 0, 0, 0);
            }
        }

        static public Matrix2x2 IDENTITY
        {
            get
            {
                return new Matrix2x2(1, 0, 0, 1);
            }
        }

        public double this[int x, int y]
        {
            get
            {
                return m[x,y];
            }
            set
            {
                m[x,y] = value;
            }
        }

        public Matrix2x2()
        {
        }

        public Matrix2x2(double m00, double m01, double m10, double m11)
        {
            m[0,0] = m00;
            m[0,1] = m01;
            m[1,0] = m10;
            m[1,1] = m11;
        }

        public double Determinant()
        {
            return m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];
        }

        static public Matrix2x2 operator -(Matrix2x2 a)
        {
            return new Matrix2x2(-a[0,0], -a[0,1], -a[1,0], -a[1,1]);
        }

        static public Matrix2x2 operator -(Matrix2x2 a, Matrix2x2 b)
        {
            return new Matrix2x2(a[0,0]-b[0,0], a[0,1]-b[0,1], a[1,0]-b[1,0], a[1,1]-b[1,1]);
        }

        static public Matrix2x2 operator +(Matrix2x2 a, Matrix2x2 b)
        {
            return new Matrix2x2(a[0,0]+b[0,0], a[0,1]+b[0,1], a[1,0]+b[1,0], a[1,1]+b[1,1]);
        }

        static public Matrix2x2 operator *(Matrix2x2 a, double b)
        {
            return new Matrix2x2(a[0,0]*b, a[0,1]*b, a[1,0]*b, a[1,1]*b);
        }

        static public Matrix2x2 operator *(double b, Matrix2x2 a)
        {
            return new Matrix2x2(a[0,0]*b, a[0,1]*b, a[1,0]*b, a[1,1]*b);
        }

        static public Vector2 operator *(Matrix2x2 a, Vector2 b)
        {
            return new Vector2(a[0, 0] * b.X + a[0, 1] * b.Y, a[1, 0] * b.X + a[1, 1] * b.Y);
        }

        static public Matrix2x2 operator *(Matrix2x2 a, Matrix2x2 b)
        {
            Matrix2x2 res = new Matrix2x2();
		    res[0,0] = a[0,0]*b[0,0] + a[0,1]*b[1,0];
		    res[0,1] = a[0,0]*b[0,1] + a[0,1]*b[1,1];
		    res[1,0] = a[1,0]*b[0,0] + a[1,1]*b[1,0];
		    res[1,1] = a[1,0]*b[0,1] + a[1,1]*b[1,1];
		    return res;
	    }

        static public Matrix2x2 operator /(Matrix2x2 a, double b)
        {
            return new Matrix2x2(a[0,0]/b, a[0,1]/b, a[1,0]/b, a[1,1]/b);
        }

        static public bool operator ==(Matrix2x2 a, Matrix2x2 b)
        {
            return ((object)a != null && (object)b != null && a.m == b.m);
        }

        static public bool operator !=(Matrix2x2 a, Matrix2x2 b)
        {
            return ((object)a == null || (object)b == null || a.m != b.m);
        }

        public override string ToString()
        {
            return "(" + m[0,0] + "," + m[0,1] + ";" + m[1,0] + "," + m[1,1] + ")";
        }

        public override bool Equals(object obj)
        {
            if (obj is Matrix2x2)
            {
                Matrix2x2 v = (Matrix2x2)obj;
                return (obj != null && this == v);
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

        public static Matrix2x2 MultiplyWithTranspose(Vector2 p, Vector2 q)
        {
            return new Matrix2x2(p.X * q.X, p.X * q.Y, p.Y * q.X, p.Y * q.Y);
        }
                
        protected static void JacobiRotate(ref Matrix2x2 A, ref Matrix2x2 R)
        {
	        // rotates A through phi in 01-plane to set A(0,1) = 0
	        // rotation stored in R whose columns are eigenvectors of A
	        double d = (A[0,0] - A[1,1])/(2.0f*A[0,1]);
	        double t = 1.0f / (Math.Abs(d) + Math.Sqrt(d*d + 1.0f));
	        if (d < 0.0f) t = -t;
	        double c = 1.0f/Math.Sqrt(t*t + 1);
	        double s = t*c;
	        A[0,0] += t*A[0,1];
	        A[1,1] -= t*A[0,1];
	        A[0,1] = A[1,0] = 0.0f;
	        // store rotation in R
	        for (int k = 0; k < 2; k++) {
		        double Rkp = c*R[k,0] + s*R[k,1];
		        double Rkq =-s*R[k,0] + c*R[k,1];
		        R[k,0] = Rkp;
		        R[k,1] = Rkq;
	        }
        }

        protected static void EigenDecomposition(ref Matrix2x2 A, ref Matrix2x2 R)
        {
	        // only for symmetric matrices!
	        // A = R A' R^T, where A' is diagonal and R orthonormal

	        R = Matrix2x2.IDENTITY;	// unit matrix
	        JacobiRotate(ref A, ref R);
        }

        public Matrix2x2 ExtractRotation()
        {
	        // A = RS, where S is symmetric and R is orthonormal
	        // -> S = (A^T A)^(1/2)
            Matrix2x2 A = this;
			Matrix2x2 S = new Matrix2x2(); 
			Matrix2x2 R = Matrix2x2.IDENTITY;	// default answer

			// Here we use a closed-form evaluation of the decomposition for the 2D case
			// In 3D, you'd have to use jacobi iteration to diagonalize a matrix
			// See http://www.mpi-hd.mpg.de/personalhomes/jkopp/3x3/ for implementations of the 3D case

            Matrix2x2 ATA;
	        ATA = Matrix2x2.MultiplyTransposedLeft(A, A);

            Matrix2x2 U = new Matrix2x2();
	        R = Matrix2x2.IDENTITY;
	        EigenDecomposition(ref ATA, ref U);

	        double l0 = ATA[0,0]; if (l0 <= 0.0f) l0 = 0.0f; else l0 = 1.0f / Math.Sqrt(l0);
	        double l1 = ATA[1,1]; if (l1 <= 0.0f) l1 = 0.0f; else l1 = 1.0f / Math.Sqrt(l1);

            Matrix2x2 S1 = new Matrix2x2();
	        S1[0,0] = l0*U[0,0]*U[0,0] + l1*U[0,1]*U[0,1];
            S1[0,1] = l0*U[0,0]*U[1,0] + l1*U[0,1]*U[1,1];
	        S1[1,0] = S1[0,1];
            S1[1,1] = l0*U[1,0]*U[1,0] + l1*U[1,1]*U[1,1];
	        R = A * S1;
	        S = Matrix2x2.MultiplyTransposedLeft(R, A);

			return R;
        }

        public static Matrix2x2 MultiplyTransposedLeft(Matrix2x2 left, Matrix2x2 right)
        {
	        Matrix2x2 res = new Matrix2x2();
	        res[0,0] = left[0,0]*right[0,0] + left[1,0]*right[1,0];
	        res[0,1] = left[0,0]*right[0,1] + left[1,0]*right[1,1];
	        res[1,0] = left[0,1]*right[0,0] + left[1,1]*right[1,0];
	        res[1,1] = left[0,1]*right[0,1] + left[1,1]*right[1,1];
            return res;
        }

        public Matrix2x2 Transpose()
        {
            return new Matrix2x2(this[0, 0], this[1, 0], this[0, 1], this[1, 1]);
        }
    }
}
