using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Tao.OpenGl;

namespace PhysicsTestbed
{
    class CommonShapes
    {
        public static void RenderCircle(Vector2 center, double radius, Color3 color)
        {
            double angle;
            Gl.glLineWidth(1.0f);
            Gl.glColor4d(color.R, color.G, color.B, 1.0);
            Gl.glBegin(Gl.GL_LINES);
            for (angle = 0; angle < Math.PI * 2; angle += 0.1)
            {
                double angle2 = Math.Min(angle + 0.1, Math.PI * 2);
                double x1, y1, x2, y2;
                x1 = center.X + Math.Cos(angle) * radius;
                y1 = center.Y + Math.Sin(angle) * radius;
                x2 = center.X + Math.Cos(angle2) * radius;
                y2 = center.Y + Math.Sin(angle2) * radius;
                Gl.glVertex2d(x1, y1);
                Gl.glVertex2d(x2, y2);
            }
            Gl.glEnd();
        }

        public static void RenderKnob(Vector2 knob, Color3 color)
        {
            //   Outline
            Gl.glPointSize(12.0f);
            Gl.glColor4f(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glBegin(Gl.GL_POINTS);
            {
                Gl.glVertex2d(knob.X, knob.Y);
            }
            Gl.glEnd();

            //   Knob
            Gl.glPointSize(9.0f);
            Gl.glColor4d(color.R, color.G, color.B, 1.0);
            Gl.glBegin(Gl.GL_POINTS);
            {
                Gl.glVertex2d(knob.X, knob.Y);
            }
            Gl.glEnd();
        }
    }

    public partial class Testbed
    {
        static void RenderCollisionTrace(Particle t)
        {
            Vector2 hPosition = t.goal;
            Gl.glLineWidth(1.0f);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2d(hPosition.X, hPosition.Y);
            foreach (CollisionSubframe subframe in t.collisionSubframes)
            {
                hPosition += subframe.v * subframe.timeCoefficient;
                Gl.glVertex2d(hPosition.X, hPosition.Y);
            }
            Gl.glEnd();
        }

        static void RenderBodies()
        {
            foreach (LsmBody body in world.bodies)
            {
                // Draw locked particles selection
                Gl.glColor3d(1, 0, 0);
                Gl.glPointSize(8.0f);
                Gl.glBegin(Gl.GL_POINTS);
                foreach (Particle t in body.particles)
                {
                    if (t.locked)
                        Gl.glVertex2d(t.goal.X, t.goal.Y);
                }
                Gl.glEnd();

                // Draw collisioned particles selection and trace
                Gl.glColor3d(0, 0, 0);
                Gl.glPointSize(8.0f);
                foreach (Particle t in body.particles)
                {
                    if (t.collisionSubframes.Count > 0)
                    {
                        Gl.glBegin(Gl.GL_POINTS);
                        Gl.glVertex2d(t.goal.X, t.goal.Y);
                        Gl.glEnd();
                        RenderCollisionTrace(t);
                    }
                }

                // Draw all particles
                Gl.glColor3d(body.Color.R, body.Color.G, body.Color.B);
                Gl.glPointSize(4.0f);
                Gl.glBegin(Gl.GL_POINTS);
                foreach (Particle t in body.particles)
                {
                    Gl.glVertex2d(t.goal.X, t.goal.Y);
                }
                Gl.glEnd();
            }
        }

        static void RenderDragForce()
        {
            if (dragForce.Dragging)
            {
                // Draw line between knobs
                Gl.glLineWidth(1.0f);
                Gl.glColor4f(0.0f, 0.0f, 0.0f, 1.0f);
                Gl.glBegin(Gl.GL_LINES);
                {
                    Gl.glVertex2i(dragForce.mouse.X, dragForce.mouse.Y);
                    Gl.glVertex2d(dragForce.selected.goal.X, dragForce.selected.goal.Y);
                }
                Gl.glEnd();
                // Draw source knob
                CommonShapes.RenderKnob(dragForce.selected.goal, new Color3(1, 0, 0));
                // Draw destination knob
                CommonShapes.RenderKnob(new Vector2(dragForce.mouse.X, dragForce.mouse.Y), new Color3(0, 1, 0));
            }
        }

        static void RenderPushForce()
        {
            if (pushForce.Pushing)
            {
                CommonShapes.RenderCircle(new Vector2(pushForce.PushPosition.X, pushForce.PushPosition.Y), pushForce.PushDistance, new Color3(0, 0, 1));
            }
        }

        static void RenderForces()
        {
            RenderDragForce();
            RenderPushForce();
        }

        static void RenderWalls()
        {
            Gl.glLineWidth(2.0f);
            Gl.glColor4f(0.5f, 0.5f, 0.5f, 1.0f);
            Gl.glBegin(Gl.GL_LINES);
            {
                Gl.glVertex2d(wallImpulse.left + wallImpulse.border, wallImpulse.top);
                Gl.glVertex2d(wallImpulse.left + wallImpulse.border, wallImpulse.bottom);
                Gl.glVertex2d(wallImpulse.right - wallImpulse.border, wallImpulse.top);
                Gl.glVertex2d(wallImpulse.right - wallImpulse.border, wallImpulse.bottom);
                Gl.glVertex2d(wallImpulse.left, wallImpulse.top - wallImpulse.border);
                Gl.glVertex2d(wallImpulse.right, wallImpulse.top - wallImpulse.border);
                Gl.glVertex2d(wallImpulse.left, wallImpulse.bottom + wallImpulse.border);
                Gl.glVertex2d(wallImpulse.right, wallImpulse.bottom + wallImpulse.border);
            }
            Gl.glEnd();
        }

        public static void Render()
        {
            RenderWalls();
            RenderBodies();
            RenderForces();
        }
    }
}
