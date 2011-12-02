using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
    public class BodyImpulse : EnvironmentImpulse
    {
        public LsmBody body = null;

        public BodyImpulse(LsmBody body)
        {
            this.body = body;
        }

        public bool CollideSweptSegments(LineSegment first, LineSegment second, ref Vector2 intersection)
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

        public override void ApplyImpulse(System.Collections.IEnumerable particles)
        {
            // avoid self-collision
            if (particles.Equals(body.particles))
                return;

            foreach (Particle t in particles)
            {
                foreach (Particle bodyt in body.particles)
                {
                    Vector2 intersection = new Vector2();
                    if (bodyt.xPos != null)
                    {
                        if (CollideSweptSegments(new LineSegment(bodyt.goal, bodyt.xPos.goal), new LineSegment(t.x, t.x + t.v), ref intersection))
                            Testbed.PostMessage(System.Drawing.Color.Green, "LineSegments Collision Detected in (" + intersection.X + ", " + intersection.Y + ")."); // DEBUG
                    }

                    if (bodyt.yPos != null)
                    {
                        if (CollideSweptSegments(new LineSegment(bodyt.goal, bodyt.yPos.goal), new LineSegment(t.x, t.x + t.v), ref intersection))
                            Testbed.PostMessage(System.Drawing.Color.Green, "LineSegments Collision Detected in (" + intersection.X + ", " + intersection.Y + ")."); // DEBUG
                    }
                }
            }
        }
    }
}
