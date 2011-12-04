using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    public class BodyImpulse : EnvironmentImpulse
    {
        [Controllable(Type = ControllableAttribute.ControllerType.Slider, Caption = "CCD time offset (pure visual)", Min = 0.0, Max = 1.0)]
        public static double ccdTimeOffset = 0.0;

        private double epsilon = 0.00001;

        private bool CollideSweptSegments(LineSegment first, LineSegment second, ref Vector2 intersection)
        {
            double Ua, Ub;
            // Equations to determine whether lines intersect
            Ua = ((second.end.X - second.start.X) * (first.start.Y - second.start.Y) - (second.end.Y - second.start.Y) * (first.start.X - second.start.X)) /
                ((second.end.Y - second.start.Y) * (first.end.X - first.start.X) - (second.end.X - second.start.X) * (first.end.Y - first.start.Y));
            Ub = ((first.end.X - first.start.X) * (first.start.Y - second.start.Y) - (first.end.Y - first.start.Y) * (first.start.X - second.start.X)) /
                ((second.end.Y - second.start.Y) * (first.end.X - first.start.X) - (second.end.X - second.start.X) * (first.end.Y - first.start.Y));
            if (Ua >= 0.0f && Ua <= 1.0f && Ub >= 0.0f && Ub <= 1.0f)
            {
                intersection.X = first.start.X + Ua*(first.end.X - first.start.X);
                intersection.Y = first.start.Y + Ua*(first.end.Y - first.start.Y);
                return true;
            }
            return false;
        }

        private void CheckSegment(
            Vector2 origin, Vector2 neighbor,
            Vector2 pos, Vector2 posNext, Vector2 velocity, ref List<CollisionSubframe> collisionBuffer
        )
        {
            Vector2 intersection = new Vector2();
            if (
                CollideSweptSegments(new LineSegment(origin, neighbor), new LineSegment(pos, posNext), ref intersection) &&
                (intersection - pos).Length() > epsilon // to prevent slipping of start point to just reflected edge
            )
            {
                // reflect velocity relative edge
                Vector2 reflectSurface = neighbor - origin;
                Vector2 reflectNormal = new Vector2(-reflectSurface.Y, reflectSurface.X);
                if (reflectNormal.Dot(velocity) < 0) reflectNormal = -reflectNormal;
                Vector2 newVelocity = velocity - 2.0 * reflectNormal * (reflectNormal.Dot(velocity) / reflectNormal.LengthSq());

                collisionBuffer.Add(new CollisionSubframe(newVelocity, (intersection - pos).Length() / velocity.Length()));
            }
        }

        private void CheckParticleEdge(
            bool isFrozenEdge, ref Particle.CCDDebugInfo ccd,
            Particle origin, Particle neighbor,
            Vector2 pos, Vector2 posNext, Vector2 velocity, ref List<CollisionSubframe> collisionBuffer
        )
        {
            if (isFrozenEdge)
            {
                // simple collision for frozen body
                CheckSegment(origin.goal, neighbor.goal, pos, posNext, velocity, ref collisionBuffer); // current edge position
                return;
            }

            // swept collision for body
            EdgePointCCDSolver.SolverInput solverInput = new EdgePointCCDSolver.SolverInput(new LineSegment(origin.goal, neighbor.goal), new LineSegment(origin.x, neighbor.x), pos, posNext);
            double? ccdCollisionTime = EdgePointCCDSolver.Solve(solverInput);

            ccd = null;
            if (ccdCollisionTime != null)
            {
                ccd = GenerateDebugInfo(solverInput, ccdCollisionTime.Value);
                Vector2 velocityEdgeCollisionPoint = origin.v + (neighbor.v - origin.v) * ccd.coordinateOfPointOnEdge;
                Vector2 velocityPointRelativeEdge = velocity - velocityEdgeCollisionPoint;

                Vector2 edgeCollisionPoint_Pos = origin.goal + (neighbor.goal - origin.goal) * ccd.coordinateOfPointOnEdge;
                Vector2 edgeCollisionPoint_Intersection = ccd.edge.start + (ccd.edge.end - ccd.edge.start) * ccd.coordinateOfPointOnEdge;

                // compute velocity reflection relativly moving edge
                Vector2 reflectSurface = ccd.edge.end - ccd.edge.start;
                Vector2 reflectNormal = new Vector2(-reflectSurface.Y, reflectSurface.X);
                if (reflectNormal.Dot(velocityPointRelativeEdge) < 0) reflectNormal = -reflectNormal;
                Vector2 newVelocity = velocityPointRelativeEdge - 2.0 * reflectNormal * (reflectNormal.Dot(velocityPointRelativeEdge) / reflectNormal.LengthSq());
                //newVelocity += velocityEdgeCollisionPoint; // newVelocity should be in global coordinates

                Vector2 particleGlobalDelta = ccd.point - pos;
                Vector2 edgeCollisionPointGlobalDelta = edgeCollisionPoint_Pos - edgeCollisionPoint_Intersection;
                Vector2 particleDeltaRelativeEdgeCollisionPoint = particleGlobalDelta - edgeCollisionPointGlobalDelta;

                collisionBuffer.Add(new CollisionSubframe(newVelocity, particleDeltaRelativeEdgeCollisionPoint.Length() / velocityPointRelativeEdge.Length())); // ccdCollisionTime.Value

                if (LsmBody.pauseOnBodyBodyCollision)
                    Testbed.Paused = true;
            }
        }

        Particle.CCDDebugInfo GenerateDebugInfo(EdgePointCCDSolver.SolverInput solverInput, double time) // DEBUG
        {
            Particle.CCDDebugInfo ccd = new Particle.CCDDebugInfo(
                solverInput.currentPoint + (solverInput.nextPoint - solverInput.currentPoint) * time,
                new LineSegment(
                    solverInput.currentEdge.start + (solverInput.nextEdge.start - solverInput.currentEdge.start) * time,
                    solverInput.currentEdge.end + (solverInput.nextEdge.end - solverInput.currentEdge.end) * time
                )
            );

            // DEBUG test (P-A)x(B-A) = 0
            //Testbed.PostMessage(System.Drawing.Color.Blue, "Test CCD math: 0 =?= " + (ccd.point - ccd.edge.start).CrossProduct(ccd.edge.end - ccd.edge.start)); // DEBUG

            return ccd;
        }

        public override void ApplyImpulse(LsmBody applyBody, Particle applyParticle, Vector2 pos, Vector2 posNext, Vector2 velocity, ref List<CollisionSubframe> collisionBuffer)
        {
            foreach (LsmBody body in Testbed.world.bodies)
            {
                if (body.Equals(applyBody))
                    continue; // avoid self-collision here
                // iterate all possible edges of body and test them with current subframed point
                {
                    foreach (Particle bodyt in body.particles)
                    {
                        if (bodyt.xPos != null)
                        {
                            CheckParticleEdge(body.frozen, ref applyParticle.ccdDebugInfo, bodyt, bodyt.xPos, pos, posNext, velocity, ref collisionBuffer);
                        }
                        if (bodyt.yPos != null)
                        {
                            CheckParticleEdge(body.frozen, ref applyParticle.ccdDebugInfo, bodyt, bodyt.yPos, pos, posNext, velocity, ref collisionBuffer);
                        }
                    }
                }
            }
        }
    }
}
