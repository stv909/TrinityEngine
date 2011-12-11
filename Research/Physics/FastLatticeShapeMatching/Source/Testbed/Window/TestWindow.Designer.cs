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
            this.model1Button2 = new System.Windows.Forms.Button();
            this.model2Button2 = new System.Windows.Forms.Button();
            this.model4Button2 = new System.Windows.Forms.Button();
            this.model3Button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.model4Button1 = new System.Windows.Forms.Button();
            this.model3Button1 = new System.Windows.Forms.Button();
            this.model2Button1 = new System.Windows.Forms.Button();
            this.model1Button1 = new System.Windows.Forms.Button();
            this.model5Button2 = new System.Windows.Forms.Button();
            this.model5Button1 = new System.Windows.Forms.Button();
            this.model02DisableCollisions = new System.Windows.Forms.CheckBox();
            this.model01DisableCollisions = new System.Windows.Forms.CheckBox();
            this.model02Freeze = new System.Windows.Forms.CheckBox();
            this.model01Freeze = new System.Windows.Forms.CheckBox();
            this.originButton = new System.Windows.Forms.Button();
            this.model7Button1 = new System.Windows.Forms.Button();
            this.model7Button2 = new System.Windows.Forms.Button();
            this.model6Button1 = new System.Windows.Forms.Button();
            this.model6Button2 = new System.Windows.Forms.Button();
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
            this.renderBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.renderBox_MouseWheel);
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
            // model1Button2
            // 
            this.model1Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model1Button2.BackColor = System.Drawing.Color.HotPink;
            this.model1Button2.Location = new System.Drawing.Point(56, 641);
            this.model1Button2.Name = "model1Button2";
            this.model1Button2.Size = new System.Drawing.Size(24, 23);
            this.model1Button2.TabIndex = 61;
            this.model1Button2.Text = "1";
            this.model1Button2.UseVisualStyleBackColor = false;
            this.model1Button2.Click += new System.EventHandler(this.model21Button_Click);
            // 
            // model2Button2
            // 
            this.model2Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model2Button2.BackColor = System.Drawing.Color.HotPink;
            this.model2Button2.Location = new System.Drawing.Point(80, 641);
            this.model2Button2.Name = "model2Button2";
            this.model2Button2.Size = new System.Drawing.Size(24, 23);
            this.model2Button2.TabIndex = 62;
            this.model2Button2.Text = "2";
            this.model2Button2.UseVisualStyleBackColor = false;
            this.model2Button2.Click += new System.EventHandler(this.model22Button_Click);
            // 
            // model4Button2
            // 
            this.model4Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model4Button2.BackColor = System.Drawing.Color.HotPink;
            this.model4Button2.Location = new System.Drawing.Point(128, 641);
            this.model4Button2.Name = "model4Button2";
            this.model4Button2.Size = new System.Drawing.Size(24, 23);
            this.model4Button2.TabIndex = 64;
            this.model4Button2.Text = "4";
            this.model4Button2.UseVisualStyleBackColor = false;
            this.model4Button2.Click += new System.EventHandler(this.model24Button_Click);
            // 
            // model3Button2
            // 
            this.model3Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model3Button2.BackColor = System.Drawing.Color.HotPink;
            this.model3Button2.Location = new System.Drawing.Point(104, 641);
            this.model3Button2.Name = "model3Button2";
            this.model3Button2.Size = new System.Drawing.Size(24, 23);
            this.model3Button2.TabIndex = 63;
            this.model3Button2.Text = "3";
            this.model3Button2.UseVisualStyleBackColor = false;
            this.model3Button2.Click += new System.EventHandler(this.model23Button_Click);
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
            // model4Button1
            // 
            this.model4Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model4Button1.BackColor = System.Drawing.Color.LawnGreen;
            this.model4Button1.Location = new System.Drawing.Point(128, 664);
            this.model4Button1.Name = "model4Button1";
            this.model4Button1.Size = new System.Drawing.Size(24, 23);
            this.model4Button1.TabIndex = 69;
            this.model4Button1.Text = "4";
            this.model4Button1.UseVisualStyleBackColor = false;
            this.model4Button1.Click += new System.EventHandler(this.model14Button_Click);
            // 
            // model3Button1
            // 
            this.model3Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model3Button1.BackColor = System.Drawing.Color.LawnGreen;
            this.model3Button1.Location = new System.Drawing.Point(104, 664);
            this.model3Button1.Name = "model3Button1";
            this.model3Button1.Size = new System.Drawing.Size(24, 23);
            this.model3Button1.TabIndex = 68;
            this.model3Button1.Text = "3";
            this.model3Button1.UseVisualStyleBackColor = false;
            this.model3Button1.Click += new System.EventHandler(this.model13Button_Click);
            // 
            // model2Button1
            // 
            this.model2Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model2Button1.BackColor = System.Drawing.Color.LawnGreen;
            this.model2Button1.Location = new System.Drawing.Point(80, 664);
            this.model2Button1.Name = "model2Button1";
            this.model2Button1.Size = new System.Drawing.Size(24, 23);
            this.model2Button1.TabIndex = 67;
            this.model2Button1.Text = "2";
            this.model2Button1.UseVisualStyleBackColor = false;
            this.model2Button1.Click += new System.EventHandler(this.model12Button_Click);
            // 
            // model1Button1
            // 
            this.model1Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model1Button1.BackColor = System.Drawing.Color.LawnGreen;
            this.model1Button1.Location = new System.Drawing.Point(56, 664);
            this.model1Button1.Name = "model1Button1";
            this.model1Button1.Size = new System.Drawing.Size(24, 23);
            this.model1Button1.TabIndex = 66;
            this.model1Button1.Text = "1";
            this.model1Button1.UseVisualStyleBackColor = false;
            this.model1Button1.Click += new System.EventHandler(this.model11Button_Click);
            // 
            // model5Button2
            // 
            this.model5Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model5Button2.BackColor = System.Drawing.Color.HotPink;
            this.model5Button2.Location = new System.Drawing.Point(152, 641);
            this.model5Button2.Name = "model5Button2";
            this.model5Button2.Size = new System.Drawing.Size(24, 23);
            this.model5Button2.TabIndex = 70;
            this.model5Button2.Text = "5";
            this.model5Button2.UseVisualStyleBackColor = false;
            this.model5Button2.Click += new System.EventHandler(this.model25Button_Click);
            // 
            // model5Button1
            // 
            this.model5Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model5Button1.BackColor = System.Drawing.Color.LawnGreen;
            this.model5Button1.Location = new System.Drawing.Point(152, 664);
            this.model5Button1.Name = "model5Button1";
            this.model5Button1.Size = new System.Drawing.Size(24, 23);
            this.model5Button1.TabIndex = 71;
            this.model5Button1.Text = "5";
            this.model5Button1.UseVisualStyleBackColor = false;
            this.model5Button1.Click += new System.EventHandler(this.model15Button_Click);
            // 
            // model02DisableCollisions
            // 
            this.model02DisableCollisions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model02DisableCollisions.AutoSize = true;
            this.model02DisableCollisions.Location = new System.Drawing.Point(236, 645);
            this.model02DisableCollisions.Name = "model02DisableCollisions";
            this.model02DisableCollisions.Size = new System.Drawing.Size(104, 17);
            this.model02DisableCollisions.TabIndex = 72;
            this.model02DisableCollisions.Text = "disable collisions";
            this.model02DisableCollisions.UseVisualStyleBackColor = true;
            // 
            // model01DisableCollisions
            // 
            this.model01DisableCollisions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model01DisableCollisions.AutoSize = true;
            this.model01DisableCollisions.Location = new System.Drawing.Point(236, 669);
            this.model01DisableCollisions.Name = "model01DisableCollisions";
            this.model01DisableCollisions.Size = new System.Drawing.Size(104, 17);
            this.model01DisableCollisions.TabIndex = 73;
            this.model01DisableCollisions.Text = "disable collisions";
            this.model01DisableCollisions.UseVisualStyleBackColor = true;
            // 
            // model02Freeze
            // 
            this.model02Freeze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model02Freeze.AutoSize = true;
            this.model02Freeze.Location = new System.Drawing.Point(339, 645);
            this.model02Freeze.Name = "model02Freeze";
            this.model02Freeze.Size = new System.Drawing.Size(55, 17);
            this.model02Freeze.TabIndex = 74;
            this.model02Freeze.Text = "freeze";
            this.model02Freeze.UseVisualStyleBackColor = true;
            // 
            // model01Freeze
            // 
            this.model01Freeze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model01Freeze.AutoSize = true;
            this.model01Freeze.Location = new System.Drawing.Point(339, 668);
            this.model01Freeze.Name = "model01Freeze";
            this.model01Freeze.Size = new System.Drawing.Size(55, 17);
            this.model01Freeze.TabIndex = 75;
            this.model01Freeze.Text = "freeze";
            this.model01Freeze.UseVisualStyleBackColor = true;
            // 
            // originButton
            // 
            this.originButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.originButton.Location = new System.Drawing.Point(245, 694);
            this.originButton.Name = "originButton";
            this.originButton.Size = new System.Drawing.Size(59, 48);
            this.originButton.TabIndex = 76;
            this.originButton.Text = "Origin";
            this.originButton.UseVisualStyleBackColor = true;
            this.originButton.Click += new System.EventHandler(this.originButton_Click);
            // 
            // model7Button1
            // 
            this.model7Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model7Button1.BackColor = System.Drawing.Color.LawnGreen;
            this.model7Button1.Location = new System.Drawing.Point(200, 664);
            this.model7Button1.Name = "model7Button1";
            this.model7Button1.Size = new System.Drawing.Size(24, 23);
            this.model7Button1.TabIndex = 80;
            this.model7Button1.Text = "7";
            this.model7Button1.UseVisualStyleBackColor = false;
            this.model7Button1.Click += new System.EventHandler(this.model17Button_Click);
            // 
            // model7Button2
            // 
            this.model7Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model7Button2.BackColor = System.Drawing.Color.HotPink;
            this.model7Button2.Location = new System.Drawing.Point(200, 641);
            this.model7Button2.Name = "model7Button2";
            this.model7Button2.Size = new System.Drawing.Size(24, 23);
            this.model7Button2.TabIndex = 79;
            this.model7Button2.Text = "7";
            this.model7Button2.UseVisualStyleBackColor = false;
            this.model7Button2.Click += new System.EventHandler(this.model27Button_Click);
            // 
            // model6Button1
            // 
            this.model6Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model6Button1.BackColor = System.Drawing.Color.LawnGreen;
            this.model6Button1.Location = new System.Drawing.Point(176, 664);
            this.model6Button1.Name = "model6Button1";
            this.model6Button1.Size = new System.Drawing.Size(24, 23);
            this.model6Button1.TabIndex = 78;
            this.model6Button1.Text = "6";
            this.model6Button1.UseVisualStyleBackColor = false;
            this.model6Button1.Click += new System.EventHandler(this.model16Button_Click);
            // 
            // model6Button2
            // 
            this.model6Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.model6Button2.BackColor = System.Drawing.Color.HotPink;
            this.model6Button2.Location = new System.Drawing.Point(176, 641);
            this.model6Button2.Name = "model6Button2";
            this.model6Button2.Size = new System.Drawing.Size(24, 23);
            this.model6Button2.TabIndex = 77;
            this.model6Button2.Text = "6";
            this.model6Button2.UseVisualStyleBackColor = false;
            this.model6Button2.Click += new System.EventHandler(this.model26Button_Click);
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
            this.Controls.Add(this.model7Button1);
            this.Controls.Add(this.model7Button2);
            this.Controls.Add(this.model6Button1);
            this.Controls.Add(this.model6Button2);
            this.Controls.Add(this.originButton);
            this.Controls.Add(this.model01Freeze);
            this.Controls.Add(this.model02Freeze);
            this.Controls.Add(this.model01DisableCollisions);
            this.Controls.Add(this.model02DisableCollisions);
            this.Controls.Add(this.model5Button1);
            this.Controls.Add(this.model5Button2);
            this.Controls.Add(this.model4Button1);
            this.Controls.Add(this.model3Button1);
            this.Controls.Add(this.model2Button1);
            this.Controls.Add(this.model1Button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.centerOfMassX);
            this.Controls.Add(this.model4Button2);
            this.Controls.Add(this.model3Button2);
            this.Controls.Add(this.model2Button2);
            this.Controls.Add(this.model1Button2);
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
		private System.Windows.Forms.Button model1Button2;
		private System.Windows.Forms.Button model2Button2;
		private System.Windows.Forms.Button model4Button2;
		private System.Windows.Forms.Button model3Button2;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button model4Button1;
        private System.Windows.Forms.Button model3Button1;
        private System.Windows.Forms.Button model2Button1;
        private System.Windows.Forms.Button model1Button1;
        private System.Windows.Forms.Button model5Button2;
        private System.Windows.Forms.Button model5Button1;
        private System.Windows.Forms.CheckBox model02DisableCollisions;
        private System.Windows.Forms.CheckBox model01DisableCollisions;
        private System.Windows.Forms.CheckBox model02Freeze;
        private System.Windows.Forms.CheckBox model01Freeze;
        private System.Windows.Forms.Button originButton;
        private System.Windows.Forms.Button model7Button1;
        private System.Windows.Forms.Button model7Button2;
        private System.Windows.Forms.Button model6Button1;
        private System.Windows.Forms.Button model6Button2;
	}
}

