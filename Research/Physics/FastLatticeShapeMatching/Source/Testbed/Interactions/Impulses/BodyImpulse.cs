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

                if (LsmBody.pauseOnBodyBodyCollision)
                    collisionWasDetected = true; // HACK
                else
                    collisionBuffer.Add(new CollisionSubframe(newVelocity, (intersection - pos).Length() / velocity.Length()));
            }
        }

        bool collisionWasDetected = false; // HACK for CCD DEBUG

        private void CheckParticleEdge(
            bool isFrozenEdge, ref Particle.ccdDebugInfo ccd01, ref Particle.ccdDebugInfo ccd02,
            Particle origin, Particle neighbor,
            Vector2 pos, Vector2 posNext, Vector2 velocity, ref List<CollisionSubframe> collisionBuffer
        )
        {
            if (isFrozenEdge)
            {
                CheckSegment(origin.goal, neighbor.goal, pos, posNext, velocity, ref collisionBuffer); // current edge position
                return;
            }

            // simple approximation of really swept approach // TODO: implement full featured swept for point-lineSegment
            CheckSegment(origin.goal, neighbor.goal, pos, posNext, velocity, ref collisionBuffer); // current edge position
            CheckSegment(origin.x, neighbor.x, pos, posNext, velocity, ref collisionBuffer); // next edge position
            CheckSegment(origin.goal, origin.x, pos, posNext, velocity, ref collisionBuffer); // origin current to next virtual edge 
            CheckSegment(neighbor.goal, neighbor.x, pos, posNext, velocity, ref collisionBuffer); // neighbor current to next virtual edge

            // HACK for CCD DEBUG
            ccd01 = null;
            ccd02 = null;
            if (collisionWasDetected)
            {
                EdgePointCCDSolver.SolverInput solverInput = new EdgePointCCDSolver.SolverInput(new LineSegment(origin.goal, neighbor.goal), new LineSegment(origin.x, neighbor.x), pos, posNext);
                //double? ccdCollisionTime = EdgePointCCDSolver.Solve(solverInput);
                EdgePointCCDSolver.SolverResult result = EdgePointCCDSolver.SolveDebug(solverInput);
                if (result != null)
                {
                    ccd01 = GenerateDebugInfo(solverInput, result.root1.Value);
                    if (result.root2 != null)
                    {
                        ccd02 = GenerateDebugInfo(solverInput, result.root2.Value);
                    }
                }
                Testbed.Paused = true;
                collisionWasDetected = false;
            }
        }

        Particle.ccdDebugInfo GenerateDebugInfo(EdgePointCCDSolver.SolverInput solverInput, double time) // DEBUG
        {
            Particle.ccdDebugInfo ccd = new Particle.ccdDebugInfo(
                solverInput.currentPoint + (solverInput.nextPoint - solverInput.currentPoint) * time,
                new LineSegment(
                    solverInput.currentEdge.start + (solverInput.nextEdge.start - solverInput.currentEdge.start) * time,
                    solverInput.currentEdge.end + (solverInput.nextEdge.end - solverInput.currentEdge.end) * time
                )
            );
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
                    // swept collision for body
                    foreach (Particle bodyt in body.particles)
                    {
                        if (bodyt.xPos != null)
                        {
                            CheckParticleEdge(body.frozen, ref applyParticle.ccdDebugInfo01, ref applyParticle.ccdDebugInfo02, bodyt, bodyt.xPos, pos, posNext, velocity, ref collisionBuffer);
                        }
                        if (bodyt.yPos != null)
                        {
                            CheckParticleEdge(body.frozen, ref applyParticle.ccdDebugInfo01, ref applyParticle.ccdDebugInfo02, bodyt, bodyt.yPos, pos, posNext, velocity, ref collisionBuffer);
                        }
                    }
                }
            }
        }
    }
}
