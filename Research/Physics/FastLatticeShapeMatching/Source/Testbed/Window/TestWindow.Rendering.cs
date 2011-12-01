using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Tao.OpenGl;

namespace PhysicsTestbed
{
	public partial class TestWindow
	{
		bool glInitialized = false;

		private void TestWindow_Load(object sender, EventArgs e)
		{
			InitGL();
		}

		private void TestWindow_SizeChanged(object sender, EventArgs e)
		{
			if (glInitialized)
			{
				ResizeGL(renderBox.Width, renderBox.Height);
				Invalidate(true);
			}
		}

		private void InitGL()
		{
			renderBox.InitializeContexts();

			Gl.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
			Gl.glColor3f(0.0f, 0.0f, 0.0f);
			Gl.glPointSize(4.0f);
			Gl.glLineWidth(1.0f);
			Gl.glEnable(Gl.GL_BLEND);
			Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
			Gl.glEnable(Gl.GL_POINT_SMOOTH);
			Gl.glEnable(Gl.GL_LINE_SMOOTH);
			Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_NICEST);
			Gl.glHint(Gl.GL_POINT_SMOOTH_HINT, Gl.GL_NICEST);

			glInitialized = true;

			ResizeGL(renderBox.Width, renderBox.Height);
		}

		private void ResizeGL(int width, int height)
		{
			Gl.glViewport(0, 0, width, height);
			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();

			Gl.glOrtho(0.0f, (double)width, 0, (double)height, -10, 10);

			this.Render();
		}

		public void Render()
		{
			renderBox.Invalidate(true);

			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

			Testbed.Render();

			Gl.glFlush();
		}
	}
}
