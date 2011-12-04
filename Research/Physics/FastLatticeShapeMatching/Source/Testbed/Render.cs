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
        static void RenderLockedParticlesSelection(List<Particle> particles)
        {
            Gl.glColor3d(1, 0, 0);
            Gl.glPointSize(8.0f);
            Gl.glBegin(Gl.GL_POINTS);
            foreach (Particle t in particles)
            {
                if (t.locked)
                    Gl.glVertex2d(t.goal.X, t.goal.Y);
            }
            Gl.glEnd();
        }

        static void RenderCollisionTrace(Particle t)
        {
            Vector2 hPosition = t.goal; // WARNING: why t.x doesn't work correctly???
            Gl.glColor4d(0, 0, 0, 0.5);
            Gl.glLineWidth(1.0f);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2d(hPosition.X, hPosition.Y);
            foreach (CollisionSubframe subframe in t.collisionSubframes)
            {
                hPosition += subframe.v * subframe.timeCoefficient;
                Gl.glVertex2d(hPosition.X, hPosition.Y);
            }
            Gl.glEnd();

            hPosition = t.goal; // WARNING: why t.x doesn't work correctly???
            Gl.glColor4d(1, 0.5, 0, 0.5);
            Gl.glPointSize(4.0f);
            Gl.glBegin(Gl.GL_POINTS);
            foreach (CollisionSubframe subframe in t.collisionSubframes)
            {
                hPosition += subframe.v * subframe.timeCoefficient;
                Gl.glVertex2d(hPosition.X, hPosition.Y);
            }
            Gl.glEnd();
        }

        static void RenderCollisionedParticlesSelection(List<Particle> particles)
        {
            Gl.glColor3d(0, 0, 0);
            Gl.glPointSize(8.0f);
            foreach (Particle t in particles)
            {
                if (t.collisionSubframes.Count > 0)
                {
                    Gl.glBegin(Gl.GL_POINTS);
                    Gl.glVertex2d(t.goal.X, t.goal.Y);
                    Gl.glEnd();
                    RenderCollisionTrace(t);
                }
            }
        }

        static void RenderLattice_Goal(List<Particle> particles, Color3 color)
        {
            Gl.glLineWidth(1.0f);
            Gl.glColor3d(color.R, color.G, color.B);
            Gl.glBegin(Gl.GL_LINES);
            {
                foreach (Particle t in particles)
                {
                    if (t.xPos != null)
                    {
                        Gl.glVertex2d(t.goal.X, t.goal.Y);
                        Gl.glVertex2d(t.xPos.goal.X, t.xPos.goal.Y);
                    }
                    if (t.yPos != null)
                    {
                        Gl.glVertex2d(t.goal.X, t.goal.Y);
                        Gl.glVertex2d(t.yPos.goal.X, t.yPos.goal.Y);
                    }
                }
            }
            Gl.glEnd();
        }

        static void RenderParticleGroup_Goal(List<Particle> particles, Color3 color)
        {
            Gl.glColor3d(color.R, color.G, color.B);
            Gl.glPointSize(4.0f);
            Gl.glBegin(Gl.GL_POINTS);
            foreach (Particle t in particles)
            {
                Gl.glVertex2d(t.goal.X, t.goal.Y);
            }
            Gl.glEnd();
        }

        static void RenderLattice_x(List<Particle> particles, Color3 color)
        {
            Gl.glLineWidth(1.0f);
            Gl.glColor3d(color.R, color.G, color.B);
            Gl.glBegin(Gl.GL_LINES);
            {
                foreach (Particle t in particles)
                {
                    if (t.xPos != null)
                    {
                        Gl.glVertex2d(t.x.X, t.x.Y);
                        Gl.glVertex2d(t.xPos.x.X, t.xPos.x.Y);
                    }
                    if (t.yPos != null)
                    {
                        Gl.glVertex2d(t.x.X, t.x.Y);
                        Gl.glVertex2d(t.yPos.x.X, t.yPos.x.Y);
                    }
                }
            }
            Gl.glEnd();
        }

        static void RenderParticleGroup_x(List<Particle> particles, Color3 color)
        {
            Gl.glColor3d(color.R, color.G, color.B);
            Gl.glPointSize(4.0f);
            Gl.glBegin(Gl.GL_POINTS);
            foreach (Particle t in particles)
            {
                Gl.glVertex2d(t.x.X, t.x.Y);
            }
            Gl.glEnd();
        }

        static void RenderConnection_Goal_X(List<Particle> particles, Color3 color)
        {
            Gl.glColor4d(color.R, color.G, color.B, 0.5);
            Gl.glLineWidth(1.0f);
            Gl.glBegin(Gl.GL_LINES);
            foreach (Particle t in particles)
            {
                if (t.collisionSubframes.Count == 0)
                {
                    Gl.glVertex2d(t.goal.X, t.goal.Y);
                    Gl.glVertex2d(t.x.X, t.x.Y);
                }
            }
            Gl.glEnd();
        }

        static void RenderBodies()
        {
            foreach (LsmBody body in world.bodies)
            {
                RenderLockedParticlesSelection(body.particles);
                if (LsmBody.drawCollisionPointsAndTraces) RenderCollisionedParticlesSelection(body.particles);

                Color3 colorX = new Color3(0, 0.5, 1);
                Color3 colorGoalX = new Color3(1, 0, 1);
                if (LsmBody.drawGoalXConnection) RenderConnection_Goal_X(body.particles, colorGoalX);
                if (LsmBody.drawBodyLattice_x) RenderLattice_x(body.particles, 0.7 * colorX);
                if (LsmBody.drawBodyParticles_x) RenderParticleGroup_x(body.particles, colorX);
                if (LsmBody.drawBodyLattice_goal) RenderLattice_Goal(body.particles, 0.7 * body.Color);
                if (LsmBody.drawBodyParticles_goal) RenderParticleGroup_Goal(body.particles, body.Color);
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

            // TODO: render force-based wall as well
        }

        private static Color3 ccdHelper = new Color3(0.5, 0.5, 1);

        static void RenderCCDHelpers(double ccdTimeOffset)
        {
            Gl.glColor3d(ccdHelper.R, ccdHelper.G, ccdHelper.B);
            foreach (LsmBody body in world.bodies)
            {
                foreach (Particle t in body.particles)
                {
                    Vector2 tHelper = t.goal + ccdTimeOffset * (t.x - t.goal);

                    Gl.glPointSize(4.0f);
                    Gl.glBegin(Gl.GL_POINTS);
                    Gl.glVertex2d(tHelper.X, tHelper.Y);
                    Gl.glEnd();

                    Gl.glLineWidth(1.0f);
                    Gl.glBegin(Gl.GL_LINES);
                    if (t.xPos != null)
                    {
                        Vector2 tHelperNeighbor = t.xPos.goal + ccdTimeOffset * (t.xPos.x - t.xPos.goal);
                        Gl.glVertex2d(tHelper.X, tHelper.Y);
                        Gl.glVertex2d(tHelperNeighbor.X, tHelperNeighbor.Y);
                    }
                    if (t.yPos != null)
                    {
                        Vector2 tHelperNeighbor = t.yPos.goal + ccdTimeOffset * (t.yPos.x - t.yPos.goal);
                        Gl.glVertex2d(tHelper.X, tHelper.Y);
                        Gl.glVertex2d(tHelperNeighbor.X, tHelperNeighbor.Y);
                    }
                    Gl.glEnd();

                    if (t.ccdDebugInfo != null)
                    {
                        Gl.glColor3d(0, 0, 1);

                        Gl.glPointSize(6.0f);
                        Gl.glBegin(Gl.GL_POINTS);
                        Gl.glVertex2d(t.ccdDebugInfo.point.X, t.ccdDebugInfo.point.Y);
                        Gl.glEnd();

                        Gl.glLineWidth(2.0f);
                        Gl.glBegin(Gl.GL_LINES);
                        Gl.glVertex2d(t.ccdDebugInfo.edge.start.X, t.ccdDebugInfo.edge.start.Y);
                        Gl.glVertex2d(t.ccdDebugInfo.edge.end.X, t.ccdDebugInfo.edge.end.Y);
                        Gl.glEnd();

                        Gl.glColor3d(ccdHelper.R, ccdHelper.G, ccdHelper.B);
                    }
                }
            }
        }

        public static void Render()
        {
            RenderWalls();
            RenderBodies();
            if (BodyImpulse.ccdTimeOffset > 0.0 || LsmBody.pauseOnBodyBodyCollision) 
                RenderCCDHelpers(BodyImpulse.ccdTimeOffset);
            RenderForces();
        }
    }
}
