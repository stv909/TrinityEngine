namespace PhysicsTestbed
{
	partial class TestWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.pauseStepButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.renderBox = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.centerOfMassX = new System.Windows.Forms.TextBox();
            this.linearMomentumX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.angularMomentum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.centerOfMassY = new System.Windows.Forms.TextBox();
            this.linearMomentumY = new System.Windows.Forms.TextBox();
            this.runButton = new System.Windows.Forms.Button();
            this.dAngularMomentum = new System.Windows.Forms.TextBox();
            this.runCheckBox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rmbPushRadioButton = new System.Windows.Forms.RadioButton();
            this.rmbLockRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.controllerPanel1 = new PhysicsTestbed.ControllerPanel();
            this.resetButton = new System.Windows.Forms.Button();
            this.model1Button = new System.Windows.Forms.Button();
            this.model2Button = new System.Windows.Forms.Button();
            this.model4Button = new System.Windows.Forms.Button();
            this.model3Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.model02DisableCollisions = new System.Windows.Forms.CheckBox();
            this.model01DisableCollisions = new System.Windows.Forms.CheckBox();
            this.model02Freeze = new System.Windows.Forms.CheckBox();
            this.model01Freeze = new System.Windows.Forms.CheckBox();
            this.statusBox = new PhysicsTestbed.MessageTextBox();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pauseStepButton
            // 
            this.pauseStepButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pauseStepButton.Location = new System.Drawing.Point(8, 718);
            this.pauseStepButton.Name = "pauseStepButton";
            this.pauseStepButton.Size = new System.Drawing.Size(80, 24);
            this.pauseStepButton.TabIndex = 1;
            this.pauseStepButton.Text = "Step";
            this.pauseStepButton.UseVisualStyleBackColor = true;
            this.pauseStepButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pauseStepButton_MouseDown);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.TimerTick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.renderBox);
            this.panel1.Location = new System.Drawing.Point(400, 32);
            this.panel1.MinimumSize = new System.Drawing.Size(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 710);
            this.panel1.TabIndex = 2;
            // 
            // renderBox
            // 
            this.renderBox.AccumBits = ((byte)(0));
            this.renderBox.AutoCheckErrors = false;
            this.renderBox.AutoFinish = false;
            this.renderBox.AutoMakeCurrent = true;
            this.renderBox.AutoSwapBuffers = true;
            this.renderBox.BackColor = System.Drawing.Color.Black;
            this.renderBox.ColorBits = ((byte)(32));
            this.renderBox.DepthBits = ((byte)(16));
            this.renderBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderBox.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.renderBox.Location = new System.Drawing.Point(0, 0);
            this.renderBox.Name = "renderBox";
            this.renderBox.Size = new System.Drawing.Size(623, 706);
            this.renderBox.StencilBits = ((byte)(0));
            this.renderBox.TabIndex = 7;
            this.renderBox.TabStop = false;
            this.renderBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.renderBox_MouseDown);
            this.renderBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.renderBox_MouseMove);
            this.renderBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.renderBox_MouseUp);
            this.renderBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(renderBox_MouseWheel);
            this.renderBox.Resize += new System.EventHandler(this.renderBox_Resize);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1035, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Center of mass:";
            // 
            // centerOfMassX
            // 
            this.centerOfMassX.BackColor = System.Drawing.Color.White;
            this.centerOfMassX.Location = new System.Drawing.Point(112, 32);
            this.centerOfMassX.Name = "centerOfMassX";
            this.centerOfMassX.ReadOnly = true;
            this.centerOfMassX.Size = new System.Drawing.Size(136, 20);
            this.centerOfMassX.TabIndex = 5;
            // 
            // linearMomentumX
            // 
            this.linearMomentumX.BackColor = System.Drawing.Color.White;
            this.linearMomentumX.Location = new System.Drawing.Point(112, 56);
            this.linearMomentumX.Name = "linearMomentumX";
            this.linearMomentumX.ReadOnly = true;
            this.linearMomentumX.Size = new System.Drawing.Size(136, 20);
            this.linearMomentumX.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Linear momentum:";
            // 
            // angularMomentum
            // 
            this.angularMomentum.BackColor = System.Drawing.Color.White;
            this.angularMomentum.Location = new System.Drawing.Point(112, 80);
            this.angularMomentum.Name = "angularMomentum";
            this.angularMomentum.ReadOnly = true;
            this.angularMomentum.Size = new System.Drawing.Size(280, 20);
            this.angularMomentum.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Angular momentum:";
            // 
            // centerOfMassY
            // 
            this.centerOfMassY.BackColor = System.Drawing.Color.White;
            this.centerOfMassY.Location = new System.Drawing.Point(256, 32);
            this.centerOfMassY.Name = "centerOfMassY";
            this.centerOfMassY.ReadOnly = true;
            this.centerOfMassY.Size = new System.Drawing.Size(136, 20);
            this.centerOfMassY.TabIndex = 12;
            // 
            // linearMomentumY
            // 
            this.linearMomentumY.BackColor = System.Drawing.Color.White;
            this.linearMomentumY.Location = new System.Drawing.Point(256, 56);
            this.linearMomentumY.Name = "linearMomentumY";
            this.linearMomentumY.ReadOnly = true;
            this.linearMomentumY.Size = new System.Drawing.Size(136, 20);
            this.linearMomentumY.TabIndex = 13;
            // 
            // runButton
            // 
            this.runButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.runButton.Location = new System.Drawing.Point(40, 694);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(48, 24);
            this.runButton.TabIndex = 14;
            this.runButton.Text = "Run";
            this.runButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.runButton.UseVisualStyleBackColor = true;
            // 
            // dAngularMomentum
            // 
            this.dAngularMomentum.BackColor = System.Drawing.Color.White;
            this.dAngularMomentum.Location = new System.Drawing.Point(224, 80);
            this.dAngularMomentum.Name = "dAngularMomentum";
            this.dAngularMomentum.ReadOnly = true;
            this.dAngularMomentum.Size = new System.Drawing.Size(168, 20);
            this.dAngularMomentum.TabIndex = 25;
            this.dAngularMomentum.Visible = false;
            // 
            // runCheckBox
            // 
            this.runCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.runCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.runCheckBox.Checked = true;
            this.runCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.runCheckBox.Location = new System.Drawing.Point(8, 694);
            this.runCheckBox.Name = "runCheckBox";
            this.runCheckBox.Size = new System.Drawing.Size(32, 24);
            this.runCheckBox.TabIndex = 32;
            this.runCheckBox.Text = " X";
            this.runCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.runCheckBox.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.Location = new System.Drawing.Point(313, 694);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 48);
            this.label8.TabIndex = 37;
            this.label8.Text = "RMB:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rmbPushRadioButton
            // 
            this.rmbPushRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rmbPushRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.rmbPushRadioButton.Location = new System.Drawing.Point(344, 694);
            this.rmbPushRadioButton.Name = "rmbPushRadioButton";
            this.rmbPushRadioButton.Size = new System.Drawing.Size(48, 24);
            this.rmbPushRadioButton.TabIndex = 38;
            this.rmbPushRadioButton.Text = "Push";
            this.rmbPushRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rmbPushRadioButton.UseVisualStyleBackColor = true;
            // 
            // rmbLockRadioButton
            // 
            this.rmbLockRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rmbLockRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.rmbLockRadioButton.Checked = true;
            this.rmbLockRadioButton.Location = new System.Drawing.Point(344, 718);
            this.rmbLockRadioButton.Name = "rmbLockRadioButton";
            this.rmbLockRadioButton.Size = new System.Drawing.Size(48, 24);
            this.rmbLockRadioButton.TabIndex = 39;
            this.rmbLockRadioButton.TabStop = true;
            this.rmbLockRadioButton.Text = "Lock";
            this.rmbLockRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rmbLockRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.controllerPanel1);
            this.groupBox1.Location = new System.Drawing.Point(8, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(384, 450);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tunable parameters";
            // 
            // controllerPanel1
            // 
            this.controllerPanel1.AutoScroll = true;
            this.controllerPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controllerPanel1.Location = new System.Drawing.Point(3, 16);
            this.controllerPanel1.Name = "controllerPanel1";
            this.controllerPanel1.Size = new System.Drawing.Size(378, 431);
            this.controllerPanel1.TabIndex = 0;
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetButton.Location = new System.Drawing.Point(96, 694);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(80, 48);
            this.resetButton.TabIndex = 60;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // model1Button
            // 
            this.model1Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model1Button.BackColor = System.Drawing.Color.HotPink;
            this.model1Button.Location = new System.Drawing.Point(56, 641);
            this.model1Button.Name = "model1Button";
            this.model1Button.Size = new System.Drawing.Size(24, 23);
            this.model1Button.TabIndex = 61;
            this.model1Button.Text = "1";
            this.model1Button.UseVisualStyleBackColor = false;
            this.model1Button.Click += new System.EventHandler(this.model21Button_Click);
            // 
            // model2Button
            // 
            this.model2Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model2Button.BackColor = System.Drawing.Color.HotPink;
            this.model2Button.Location = new System.Drawing.Point(80, 641);
            this.model2Button.Name = "model2Button";
            this.model2Button.Size = new System.Drawing.Size(24, 23);
            this.model2Button.TabIndex = 62;
            this.model2Button.Text = "2";
            this.model2Button.UseVisualStyleBackColor = false;
            this.model2Button.Click += new System.EventHandler(this.model22Button_Click);
            // 
            // model4Button
            // 
            this.model4Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model4Button.BackColor = System.Drawing.Color.HotPink;
            this.model4Button.Location = new System.Drawing.Point(128, 641);
            this.model4Button.Name = "model4Button";
            this.model4Button.Size = new System.Drawing.Size(24, 23);
            this.model4Button.TabIndex = 64;
            this.model4Button.Text = "4";
            this.model4Button.UseVisualStyleBackColor = false;
            this.model4Button.Click += new System.EventHandler(this.model24Button_Click);
            // 
            // model3Button
            // 
            this.model3Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model3Button.BackColor = System.Drawing.Color.HotPink;
            this.model3Button.Location = new System.Drawing.Point(104, 641);
            this.model3Button.Name = "model3Button";
            this.model3Button.Size = new System.Drawing.Size(24, 23);
            this.model3Button.TabIndex = 63;
            this.model3Button.Text = "3";
            this.model3Button.UseVisualStyleBackColor = false;
            this.model3Button.Click += new System.EventHandler(this.model23Button_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(-6, 651);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 24);
            this.label2.TabIndex = 65;
            this.label2.Text = "Models:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.LawnGreen;
            this.button1.Location = new System.Drawing.Point(128, 664);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 23);
            this.button1.TabIndex = 69;
            this.button1.Text = "4";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.model14Button_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.BackColor = System.Drawing.Color.LawnGreen;
            this.button2.Location = new System.Drawing.Point(104, 664);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 23);
            this.button2.TabIndex = 68;
            this.button2.Text = "3";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.model13Button_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.BackColor = System.Drawing.Color.LawnGreen;
            this.button3.Location = new System.Drawing.Point(80, 664);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(24, 23);
            this.button3.TabIndex = 67;
            this.button3.Text = "2";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.model12Button_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.BackColor = System.Drawing.Color.LawnGreen;
            this.button4.Location = new System.Drawing.Point(56, 664);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(24, 23);
            this.button4.TabIndex = 66;
            this.button4.Text = "1";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.model11Button_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button5.BackColor = System.Drawing.Color.HotPink;
            this.button5.Location = new System.Drawing.Point(152, 641);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(24, 23);
            this.button5.TabIndex = 70;
            this.button5.Text = "5";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.model25Button_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button6.BackColor = System.Drawing.Color.LawnGreen;
            this.button6.Location = new System.Drawing.Point(152, 664);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(24, 23);
            this.button6.TabIndex = 71;
            this.button6.Text = "5";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.model15Button_Click);
            // 
            // model02DisableCollisions
            // 
            this.model02DisableCollisions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model02DisableCollisions.AutoSize = true;
            this.model02DisableCollisions.Location = new System.Drawing.Point(182, 645);
            this.model02DisableCollisions.Name = "model02DisableCollisions";
            this.model02DisableCollisions.Size = new System.Drawing.Size(104, 17);
            this.model02DisableCollisions.TabIndex = 72;
            this.model02DisableCollisions.Text = "disable collisions";
            this.model02DisableCollisions.UseVisualStyleBackColor = true;
            this.model02DisableCollisions.CheckedChanged += new System.EventHandler(this.model02DisableCollisions_CheckedChanged);
            // 
            // model01DisableCollisions
            // 
            this.model01DisableCollisions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model01DisableCollisions.AutoSize = true;
            this.model01DisableCollisions.Location = new System.Drawing.Point(182, 669);
            this.model01DisableCollisions.Name = "model01DisableCollisions";
            this.model01DisableCollisions.Size = new System.Drawing.Size(104, 17);
            this.model01DisableCollisions.TabIndex = 73;
            this.model01DisableCollisions.Text = "disable collisions";
            this.model01DisableCollisions.UseVisualStyleBackColor = true;
            this.model01DisableCollisions.CheckedChanged += new System.EventHandler(this.model01DisableCollisions_CheckedChanged);
            // 
            // model02Freeze
            // 
            this.model02Freeze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model02Freeze.AutoSize = true;
            this.model02Freeze.Location = new System.Drawing.Point(285, 645);
            this.model02Freeze.Name = "model02Freeze";
            this.model02Freeze.Size = new System.Drawing.Size(55, 17);
            this.model02Freeze.TabIndex = 74;
            this.model02Freeze.Text = "freeze";
            this.model02Freeze.UseVisualStyleBackColor = true;
            this.model02Freeze.CheckedChanged += new System.EventHandler(this.model02Freeze_CheckedChanged);
            // 
            // model01Freeze
            // 
            this.model01Freeze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model01Freeze.AutoSize = true;
            this.model01Freeze.Location = new System.Drawing.Point(285, 668);
            this.model01Freeze.Name = "model01Freeze";
            this.model01Freeze.Size = new System.Drawing.Size(55, 17);
            this.model01Freeze.TabIndex = 75;
            this.model01Freeze.Text = "freeze";
            this.model01Freeze.UseVisualStyleBackColor = true;
            this.model01Freeze.CheckedChanged += new System.EventHandler(this.model01Freeze_CheckedChanged);
            // 
            // statusBox
            // 
            this.statusBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusBox.Location = new System.Drawing.Point(8, 560);
            this.statusBox.Name = "statusBox";
            this.statusBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.statusBox.Size = new System.Drawing.Size(384, 80);
            this.statusBox.TabIndex = 36;
            this.statusBox.Text = "";
            // 
            // TestWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 750);
            this.Controls.Add(this.model01Freeze);
            this.Controls.Add(this.model02Freeze);
            this.Controls.Add(this.model01DisableCollisions);
            this.Controls.Add(this.model02DisableCollisions);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.centerOfMassX);
            this.Controls.Add(this.model4Button);
            this.Controls.Add(this.model3Button);
            this.Controls.Add(this.model2Button);
            this.Controls.Add(this.model1Button);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.linearMomentumY);
            this.Controls.Add(this.centerOfMassY);
            this.Controls.Add(this.angularMomentum);
            this.Controls.Add(this.linearMomentumX);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rmbLockRadioButton);
            this.Controls.Add(this.rmbPushRadioButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.runCheckBox);
            this.Controls.Add(this.dAngularMomentum);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pauseStepButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TestWindow";
            this.Text = "Physics Testbed";
            this.Load += new System.EventHandler(this.TestWindow_Load);
            this.SizeChanged += new System.EventHandler(this.TestWindow_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button pauseStepButton;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox centerOfMassX;
		private System.Windows.Forms.TextBox linearMomentumX;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox angularMomentum;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox centerOfMassY;
		private System.Windows.Forms.TextBox linearMomentumY;
		private System.Windows.Forms.Button runButton;
		public Tao.Platform.Windows.SimpleOpenGlControl renderBox;
		private System.Windows.Forms.TextBox dAngularMomentum;
		public System.Windows.Forms.CheckBox runCheckBox;
		public MessageTextBox statusBox;
		private System.Windows.Forms.Label label8;
		public System.Windows.Forms.RadioButton rmbPushRadioButton;
		public System.Windows.Forms.RadioButton rmbLockRadioButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private ControllerPanel controllerPanel1;
		private System.Windows.Forms.Button resetButton;
		private System.Windows.Forms.Button model1Button;
		private System.Windows.Forms.Button model2Button;
		private System.Windows.Forms.Button model4Button;
		private System.Windows.Forms.Button model3Button;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.CheckBox model02DisableCollisions;
        private System.Windows.Forms.CheckBox model01DisableCollisions;
        private System.Windows.Forms.CheckBox model02Freeze;
        private System.Windows.Forms.CheckBox model01Freeze;
	}
}

