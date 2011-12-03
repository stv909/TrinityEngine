using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    public class BodyImpulse : EnvironmentImpulse
    {
        public LsmBody body = null;
        private double epsilon = 0.00001;

        public BodyImpulse(LsmBody body)
        {
            this.body = body;
        }

        private bool CollideSweptSegments(LineSegment first, LineSegment second, ref Vector2 intersection)
        {
            // TODO: test this algorithm and optimize if needed
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

        private void CheckParticlePair(
            Particle origin, Particle neighbor,
            Vector2 pos, Vector2 posNext, Vector2 velocity, ref List<CollisionSubframe> collisionBuffer
        )
        {
            Vector2 intersection = new Vector2();
            if (
                CollideSweptSegments(new LineSegment(origin.goal, neighbor.goal), new LineSegment(pos, posNext), ref intersection) && 
                (intersection - pos).Length() > epsilon // to prevent slipping of start point to just reflected edge
            )
            {
                // reflect velocity relative edge
                Vector2 reflectSurface = neighbor.goal - origin.goal;
                Vector2 reflectNormal = new Vector2(-reflectSurface.Y, reflectSurface.X);
                if (reflectNormal.Dot(velocity) < 0) reflectNormal = -reflectNormal;
                Vector2 newVelocity = velocity - 2.0 * reflectNormal * (reflectNormal.Dot(velocity) / reflectNormal.LengthSq());

                collisionBuffer.Add(new CollisionSubframe(newVelocity, (intersection - pos).Length() / velocity.Length()));
            }
        }

        public override void ApplyImpulse(Vector2 pos, Vector2 posNext, Vector2 velocity, ref List<CollisionSubframe> collisionBuffer)
        {
            foreach (Particle bodyt in body.particles)
            {
                if (bodyt.xPos != null)
                {
                    CheckParticlePair(bodyt, bodyt.xPos, pos, posNext, velocity, ref collisionBuffer);
                }
                if (bodyt.yPos != null)
                {
                    CheckParticlePair(bodyt, bodyt.yPos, pos, posNext, velocity, ref collisionBuffer);
                }
            }
        }
    }
}
