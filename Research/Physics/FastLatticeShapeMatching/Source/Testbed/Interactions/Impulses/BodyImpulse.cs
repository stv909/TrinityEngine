using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    public class BodyImpulse : EnvironmentImpulse
    {
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
            Particle origin, Particle neighbor,
            Vector2 pos, Vector2 posNext, Vector2 velocity, ref List<CollisionSubframe> collisionBuffer
        )
        {
            // simple approximation of really swept approach // TODO: implement full featured swept for point-lineSegment
            CheckSegment(origin.goal, neighbor.goal, pos, posNext, velocity, ref collisionBuffer); // current edge position
            CheckSegment(origin.x, neighbor.x, pos, posNext, velocity, ref collisionBuffer); // next edge position
            CheckSegment(origin.goal, origin.x, pos, posNext, velocity, ref collisionBuffer); // origin current to next virtual edge 
            CheckSegment(neighbor.goal, neighbor.x, pos, posNext, velocity, ref collisionBuffer); // neighbor current to next virtual edge
        }

        public override void ApplyImpulse(LsmBody applyBody, Vector2 pos, Vector2 posNext, Vector2 velocity, ref List<CollisionSubframe> collisionBuffer)
        {
            foreach (LsmBody body in Testbed.world.bodies)
            {
                if (body.Equals(applyBody))
                    continue; // avoid self-collision here
                // iterate all possible edges of body and test them with current subframed point
                if (body.frozen)
                {
                    // simple collision - swept particle only
                    foreach (Particle bodyt in body.particles)
                    {
                        if (bodyt.xPos != null)
                        {
                            CheckSegment(bodyt.goal, bodyt.xPos.goal, pos, posNext, velocity, ref collisionBuffer); // current edge position
                        }
                        if (bodyt.yPos != null)
                        {
                            CheckSegment(bodyt.goal, bodyt.yPos.goal, pos, posNext, velocity, ref collisionBuffer); // current edge position
                        }
                    }
                }
                else
                {
                    // swept collision for body
                    foreach (Particle bodyt in body.particles)
                    {
                        if (bodyt.xPos != null)
                        {
                            CheckParticleEdge(bodyt, bodyt.xPos, pos, posNext, velocity, ref collisionBuffer);
                        }
                        if (bodyt.yPos != null)
                        {
                            CheckParticleEdge(bodyt, bodyt.yPos, pos, posNext, velocity, ref collisionBuffer);
                        }
                    }
                }
            }
        }
    }
}
