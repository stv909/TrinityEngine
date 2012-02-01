using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace PhysicsTestbed
{
	public class MessageTextBox : RichTextBox
	{
		public MessageTextBox()
			: base()
		{
		}

		public void PostMessageNoBreak(Color color, string message)
		{
			this.SelectionLength = 0;
			this.SelectionStart = this.TextLength;
			this.SelectionColor = color;
			this.AppendText(message);
			this.ScrollToCaret();
			return;
		}

		public void PostMessage(Color color, string message)
		{
			this.SelectionLength = 0;
			this.SelectionStart = this.TextLength;
			this.SelectionColor = color;
			this.AppendText(message + "\n");
			this.ScrollToCaret();
			return;
		}

		public void PostMessageNoBreak(string message)
		{
			PostMessageNoBreak(Color.Black, message);
		}

		public void PostMessage(string message)
		{
			PostMessage(Color.Black, message);
		}
	}
}
