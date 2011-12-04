using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PhysicsTestbed
{
    public class EdgePointCCDSolver
    {
        public class SolverInput
        {
            public LineSegment currentEdge;
            public LineSegment nextEdge;
            public Vector2 currentPoint;
            public Vector2 nextPoint;

            public SolverInput(LineSegment currentEdge, LineSegment nextEdge, Vector2 currentPoint, Vector2 nextPoint)
            {
                this.currentEdge = currentEdge;
                this.nextEdge = nextEdge;
                this.currentPoint = currentPoint;
                this.nextPoint = nextPoint;
            }
        }

        public // DEBUG
            class SolverResult
        {
            public double? root1;
            public double? root2;

            public SolverResult(double? root1, double? root2)
            {
                Debug.Assert(root1 != null);
                Debug.Assert(root2 == null || root1.Value <= root2.Value);
                this.root1 = root1;
                this.root2 = root2;
            }
        }

        static SolverResult SolveNecessaryCondition(SolverInput input)
        {
            // Let's define line (A,B) and point P that runs time interval [0, 1] from currentMoment to nextMoment:
            //
            //  A0 = currentEdge.start; B0 = currentEdge.end;
            //  A1 = nextEdge.start; B1 = nextEdge.end;
            //  P0 = currentPoint;
            //  P1 = nextPoint;
            //
            //  P = P0 + (P1 - P0)*t
            //  A = A0 + (A1 - A0)*t
            //  B = B0 + (B1 - B0)*t
            //
            //  Necessary condition: (P-A) x (B-A) = 0
            //
            // If we make form vector equation '(P-A) x (B-A) = 0' equation in coordinates in the form 'a*t^2 + b*t + c = 0' to find t, we get such a, b, c:
            double P0x = input.currentPoint.X;
            double P0y = input.currentPoint.Y;

            double P1x = input.nextPoint.X;
            double P1y = input.nextPoint.Y;

            double A0x = input.currentEdge.start.X;
            double A0y = input.currentEdge.start.Y;
            double B0x = input.currentEdge.end.X;
            double B0y = input.currentEdge.end.Y;

            double A1x = input.nextEdge.start.X;
            double A1y = input.nextEdge.start.Y;
            double B1x = input.nextEdge.end.X;
            double B1y = input.nextEdge.end.Y;

            // a*t^2 + b*t + c = 0
            double a = ((-B1x + B0x - A1x + A0x) * P1y + (B1y - B0y + A1y - A0y) * P1x + (B1x - B0x + A1x - A0x) * P0y + (-B1y + B0y - A1y + A0y) * P0x + (A1x - A0x) * B1y + (A0y - A1y) * B1x + (A0x - A1x) * B0y + (A1y - A0y) * B0x);
            double b = ((A0x - B0x) * P1y + (B0y - A0y) * P1x + (-B1x + 2.0 * B0x - A1x) * P0y + (B1y - 2.0 * B0y + A1y) * P0x - A0x * B1y + A0y * B1x + A1x * B0y - A1y * B0x);
            double c = (A0x - B0x) * P0y + (B0y - A0y) * P0x - A0x * B0y + A0y * B0x;

            if (a != 0.0)
            {
                // t^2 + 2*p*t + q = 0
                double p = b/(a+a);
                double q = c/a;

                // t_1/2 = -p +/- sqrt(p^2 - q) = -p +/- sqrt(D)
                double D = p * p - q;
                if (D < 0)
                    return null;
                double t1 = -p - Math.Sqrt(D);
                double t2 = -p + Math.Sqrt(D);
                return new SolverResult(new double?(Math.Min(t1, t2)), new double?(Math.Max(t1, t2)));
            }
            else if (b != 0.0)
            {
                double t = -c / b;
                return new SolverResult(new double?(t), null);
            }
            return null;
        }

        static bool IsPointBelongsToEdge(LineSegment edge, Vector2 point)
        {
            Vector2 edgeDirection = edge.end - edge.start;
            double edgeCoordinate = edgeDirection.Dot(point - edge.start) / edgeDirection.LengthSq();
            return edgeCoordinate >= 0.0 && edgeCoordinate <= 1.0;
        }

        static bool IsSufficientCondition(SolverInput input, double time)
        {
            Debug.Assert(time >= 0.0 && time <= 1.0);
            LineSegment edge = new LineSegment(input.currentEdge.start + (input.nextEdge.start - input.currentEdge.start) * time, input.currentEdge.end + (input.nextEdge.end - input.currentEdge.end) * time);
            Vector2 point = input.currentPoint + (input.nextPoint - input.currentPoint) * time;
            return IsPointBelongsToEdge(edge, point);
        }

        static SolverResult CorrectResultInterval(SolverResult resultIn, double leftRange, double rightRange)
        {
            Debug.Assert(resultIn != null);
            if (resultIn.root1 != null && (resultIn.root1.Value < leftRange || resultIn.root1.Value > rightRange))
                resultIn.root1 = null;
            if (resultIn.root2 != null && (resultIn.root2.Value < leftRange || resultIn.root2.Value > rightRange))
                resultIn.root2 = null;
            if (resultIn.root1 == null && resultIn.root2 == null)
                return null;
            else if (resultIn.root1 == null && resultIn.root2 != null)
                return new SolverResult(resultIn.root2, null);
            else
                return resultIn;
        }

        static SolverResult GetSufficientCondition(SolverInput input, SolverResult resultIn)
        {
            Debug.Assert(resultIn != null);
            if (resultIn.root1 != null && !IsSufficientCondition(input, resultIn.root1.Value))
                resultIn.root1 = null;
            if (resultIn.root2 != null && !IsSufficientCondition(input, resultIn.root2.Value))
                resultIn.root2 = null;
            if (resultIn.root1 == null && resultIn.root2 == null)
                return null;
            else if (resultIn.root1 == null && resultIn.root2 != null)
                return new SolverResult(resultIn.root2, null);
            else
                return resultIn;
        }

        static SolverResult SolveMath(SolverInput input)
        {
            SolverResult necessaryResult = SolveNecessaryCondition(input);
            if (necessaryResult != null)
            {
                SolverResult necessaryFilteredResult = CorrectResultInterval(necessaryResult, 0.0, 1.0);
                if (necessaryFilteredResult != null)
                    return GetSufficientCondition(input, necessaryFilteredResult);
            }
            return null;
        }

        public static double? Solve(SolverInput input)
        {
            SolverResult result = SolveMath(input);
            return result == null ? null : result.root1;
        }

        public static SolverResult SolveDebug(SolverInput input) // DEBUG
        {
            SolverResult necessaryResult = SolveNecessaryCondition(input);
            if (necessaryResult != null)
                return CorrectResultInterval(necessaryResult, 0.0, 1.0);
            return null;
        }
    }
}
