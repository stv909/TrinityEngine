using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    public class BodyImpulse : EnvironmentImpulse
    {
        [Controllable(Type = ControllableAttribute.ControllerType.Slider, Caption = "CCD time offset (pure visual)", Min = 0.01, Max = 0.99)]
        public static double ccdTimeOffset = 0.01;
        [Controllable(Type = ControllableAttribute.ControllerType.Slider, Caption = "0.01x CCD time offset", Min = -0.01, Max = 0.01)]
        public static double ccdTimeOffset001x = -0.01;

        public static double CCDTimeOffset { get{ return ccdTimeOffset + ccdTimeOffset001x; } }

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
            Vector2 pos, Vector2 posNext, Vector2 velocity, ref List<CollisionSubframeBuffer> collisionBuffer
        )
        {
            Vector2 intersection = new Vector2();
            if (CollideSweptSegments(new LineSegment(origin, neighbor), new LineSegment(pos, posNext), ref intersection))
            {
                double particleOffset = (intersection - pos).Length();
                if (particleOffset > epsilon) // to prevent slipping of start point to just reflected edge // TODO: check if this condition is really usefull. may be (timeOffset > 0.0) is a sufficient condition
                {
                    // reflect velocity relative edge
                    Vector2 reflectSurface = neighbor - origin;
                    Vector2 reflectNormal = new Vector2(-reflectSurface.Y, reflectSurface.X);
                    if (reflectNormal.Dot(velocity) < 0) reflectNormal = -reflectNormal;
                    Vector2 newVelocity = velocity - 2.0 * reflectNormal * (reflectNormal.Dot(velocity) / reflectNormal.LengthSq());

                    collisionBuffer.Add(new CollisionSubframeBuffer(newVelocity, particleOffset / velocity.Length()));
                }
            }
        }

        private void CheckParticleEdge(
            bool isFrozenEdge, ref List<Particle.CCDDebugInfo> ccds,
            Particle origin, Particle neighbor,
            Vector2 pos, Vector2 posNext, Vector2 velocity,
            ref List<CollisionSubframeBuffer> collisionBuffer, double accumulatedSubframeTime
        )
        {
            if (isFrozenEdge)
            {
                // simple collision for frozen body
                CheckSegment(origin.goal, neighbor.goal, pos, posNext, velocity, ref collisionBuffer); // current edge position
                return;
            }

            // swept collision for body
            double timePrediction = (1.0 - accumulatedSubframeTime);
            LineSegment edge = new LineSegment(origin.x + origin.v * accumulatedSubframeTime, neighbor.x + neighbor.v * accumulatedSubframeTime);
            LineSegment edgeNext = new LineSegment(edge.start + origin.v * timePrediction, edge.end + neighbor.v * timePrediction);

            //LineSegment edge = new LineSegment(origin.goal + origin.v * accumulatedSubframeTime, neighbor.goal + neighbor.v * accumulatedSubframeTime);
            //LineSegment edgeNext = new LineSegment(edge.start + origin.v * timePrediction, edge.end + neighbor.v * timePrediction);

            //LineSegment edge = new LineSegment(origin.goal, neighbor.goal);
            //LineSegment edgeNext = new LineSegment(origin.x, neighbor.x);

            EdgePointCCDSolver.SolverInput solverInput = new EdgePointCCDSolver.SolverInput(edge, edgeNext, pos, posNext);
            double? ccdCollisionTime = EdgePointCCDSolver.Solve(solverInput);

            if (ccdCollisionTime != null)
            {
                Particle.CCDDebugInfo ccd = GenerateDebugInfo(solverInput, ccdCollisionTime.Value);

                // TODO: use the Rule of conservative impulse to handle this case. Simple reflection rule is not effective here.

                Vector2 velocityEdgeCollisionPoint = origin.v + (neighbor.v - origin.v) * ccd.coordinateOfPointOnEdge;
                Vector2 velocityPointRelativeEdge = velocity - velocityEdgeCollisionPoint;

                // compute velocity reflection relativly moving edge
                Vector2 reflectSurface = ccd.edge.end - ccd.edge.start;
                Vector2 reflectNormal = new Vector2(-reflectSurface.Y, reflectSurface.X);
                if (reflectNormal.Dot(velocityPointRelativeEdge) < 0) reflectNormal = -reflectNormal;
                Vector2 newVelocity = velocityPointRelativeEdge - 2.0 * reflectNormal * (reflectNormal.Dot(velocityPointRelativeEdge) / reflectNormal.LengthSq());
                newVelocity += velocityEdgeCollisionPoint; // newVelocity should be in global coordinates
                /*
                Vector2 edgeCollisionPoint_Pos = origin.x + (neighbor.x - origin.x) * ccd.coordinateOfPointOnEdge;
                Vector2 edgeCollisionPoint_Intersection = ccd.edge.start + (ccd.edge.end - ccd.edge.start) * ccd.coordinateOfPointOnEdge;
                Vector2 particleGlobalDelta = ccd.point - pos;
                Vector2 edgeCollisionPointGlobalDelta = edgeCollisionPoint_Pos - edgeCollisionPoint_Intersection;
                Vector2 particleDeltaRelativeEdgeCollisionPoint = particleGlobalDelta - edgeCollisionPointGlobalDelta;
                collisionBuffer.Add(new CollisionSubframe(newVelocity, particleDeltaRelativeEdgeCollisionPoint.Length() / velocityPointRelativeEdge.Length()));
                */
                collisionBuffer.Add(new CollisionSubframeBuffer(newVelocity, (1.0 - accumulatedSubframeTime) * ccdCollisionTime.Value));
                ccds.Add(ccd);

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

        public override void ApplyImpulse(
            LsmBody applyBody, Particle applyParticle, Vector2 pos, Vector2 posNext, Vector2 velocity,
            ref List<CollisionSubframeBuffer> collisionBuffer, double accumulatedSubframeTime
        )
        {
            //foreach (LsmBody body in Testbed.world.bodies)
            LsmBody body = Testbed.world.bodyPassiveDebug; // DEBUG
            {
                if (!body.Equals(applyBody)) // avoid self-collision here
                {
                    // iterate all possible edges of body and test them with current subframed point
                    {
                        foreach (Particle bodyt in body.particles)
                        {
                            if (bodyt.xPos != null)
                            {
                                CheckParticleEdge(body.frozen, ref applyParticle.ccdDebugInfos, bodyt, bodyt.xPos, pos, posNext, velocity, ref collisionBuffer, accumulatedSubframeTime);
                            }
                            if (bodyt.yPos != null)
                            {
                                CheckParticleEdge(body.frozen, ref applyParticle.ccdDebugInfos, bodyt, bodyt.yPos, pos, posNext, velocity, ref collisionBuffer, accumulatedSubframeTime);
                            }
                        }
                    }
                }
            }
        }
    }
}
