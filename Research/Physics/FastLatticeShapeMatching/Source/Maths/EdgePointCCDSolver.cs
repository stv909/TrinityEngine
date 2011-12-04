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

        SolverResult SolveNecessaryCondition(LineSegment currentEdge, LineSegment nextEdge, Vector2 currentPoint, Vector2 nextPoint)
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
            double P0x = currentPoint.X;
            double P0y = currentPoint.Y;

            double P1x = nextPoint.X;
            double P1y = nextPoint.Y;

            double A0x = currentEdge.start.X;
            double A0y = currentEdge.start.Y;

            double B0x = currentEdge.end.X;
            double B0y = currentEdge.end.Y;
            
            double A1x = nextEdge.start.X;
            double A1y = nextEdge.start.Y;
            
            double B1y = nextEdge.end.X;
            double B1x = nextEdge.end.Y;

            double a = ((-B1x + B0x - A1x + A0x) * P1y + (B1y - B0y + A1y - A0y) * P1x + (B1x - B0x + A1x - A0x) * P0y + (-B1y + B0y - A1y + A0y) * P0x + (A1x - A0x) * B1y + (A0y - A1y) * B1x + (A0x - A1x) * B0y + (A1y - A0y) * B0x);
            double b = ((A0x - B0x) * P1y + (B0y - A0y) * P1x + (-B1x + 2 * B0x - A1x) * P0y + (B1y - 2 * B0y + A1y) * P0x - A0x * B1y + A0y * B1x + A1x * B0y - A1y * B0x);
            double c = (A0x - B0x) * P0y + (B0y - A0y) * P0x - A0x * B0y + A0y * B0x;

            if (a != 0.0)
            {
                double D = Math.Sqrt(b * b - 4.0 * a * c);
                double t1 = (D + b) / (2.0 * a);
                double t2 = (D - b) / (2.0 * a);
                return new SolverResult(new double?(Math.Min(t1, t2)), new double?(Math.Max(t1, t2)));
            }
            else if (b != 0.0)
            {
                double t = -c / b;
                return new SolverResult(new double?(t), null);
            }
            return null;
        }

        bool IsPointBelongsToEdge(LineSegment edge, Vector2 point)
        {
            Vector2 edgeDirection = edge.end - edge.start;
            double edgeCoordinate = edgeDirection.Dot(point - edge.start) / edgeDirection.LengthSq();
            return edgeCoordinate >= 0.0 && edgeCoordinate <= 1.0;
        }

        bool IsSufficientCondition(LineSegment currentEdge, LineSegment nextEdge, Vector2 currentPoint, Vector2 nextPoint, double time)
        {
            Debug.Assert(time >= 0.0 && time <= 1.0);
            LineSegment edge = new LineSegment(currentEdge.start + (nextEdge.start - currentEdge.start) * time, currentEdge.end + (nextEdge.end - currentEdge.end) * time);
            Vector2 point = currentPoint + (nextPoint - currentPoint) * time;
            return IsPointBelongsToEdge(edge, point);
        }

        SolverResult CorrectResultInterval(SolverResult resultIn, double leftRange, double rightRange)
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

        SolverResult GetSufficientCondition(LineSegment currentEdge, LineSegment nextEdge, Vector2 currentPoint, Vector2 nextPoint, SolverResult resultIn)
        {
            Debug.Assert(resultIn != null);
            if (resultIn.root1 != null && !IsSufficientCondition(currentEdge, nextEdge, currentPoint, nextPoint, resultIn.root1.Value))
                resultIn.root1 = null;
            if (resultIn.root2 != null && !IsSufficientCondition(currentEdge, nextEdge, currentPoint, nextPoint, resultIn.root2.Value))
                resultIn.root2 = null;
            if (resultIn.root1 == null && resultIn.root2 == null)
                return null;
            else if (resultIn.root1 == null && resultIn.root2 != null)
                return new SolverResult(resultIn.root2, null);
            else
                return resultIn;
        }

        SolverResult SolveMath(LineSegment currentEdge, LineSegment nextEdge, Vector2 currentPoint, Vector2 nextPoint)
        {
            SolverResult necessaryResult = SolveNecessaryCondition(currentEdge, nextEdge, currentPoint, nextPoint);
            if (necessaryResult != null)
            {
                SolverResult necessaryFilteredResult = CorrectResultInterval(necessaryResult, 0.0, 1.0);
                if (necessaryFilteredResult != null)
                    return GetSufficientCondition(currentEdge, nextEdge, currentPoint, nextPoint, necessaryFilteredResult);
            }
            return null;
        }

        public double? Solve(LineSegment currentEdge, LineSegment nextEdge, Vector2 currentPoint, Vector2 nextPoint)
        {
            SolverResult result = SolveMath(currentEdge, nextEdge, currentPoint, nextPoint);
            return result == null ? null : result.root1;
        }
    }
}
