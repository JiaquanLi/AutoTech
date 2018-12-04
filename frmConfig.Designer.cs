namespace AutoTech
{
    partial class frmConfig
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_Plc = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_Parity = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_StopBit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_Databit = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tb_Baud = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.tb_ComNum = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.gb_Y = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btn_MoveHomeY = new System.Windows.Forms.Button();
            this.btn_MovePosY = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_MoveLocY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.gb_X = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_MoveHomeX = new System.Windows.Forms.Button();
            this.btn_MovePosX = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_MoveLocX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_GlobalCamera = new System.Windows.Forms.TextBox();
            this.tb_LocalCamera = new System.Windows.Forms.TextBox();
            this.btn_Exit = new Telerik.WinControls.UI.RadButton();
            this.btn_Save = new Telerik.WinControls.UI.RadButton();
            this.tabControl1.SuspendLayout();
            this.tab_Plc.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gb_Y.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.gb_X.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Exit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Save)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_Plc);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(805, 459);
            this.tabControl1.TabIndex = 0;
            // 
            // tab_Plc
            // 
            this.tab_Plc.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tab_Plc.Controls.Add(this.groupBox9);
            this.tab_Plc.Controls.Add(this.gb_Y);
            this.tab_Plc.Controls.Add(this.gb_X);
            this.tab_Plc.Location = new System.Drawing.Point(4, 22);
            this.tab_Plc.Name = "tab_Plc";
            this.tab_Plc.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Plc.Size = new System.Drawing.Size(797, 433);
            this.tab_Plc.TabIndex = 0;
            this.tab_Plc.Text = "电机参数";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.panel1);
            this.groupBox9.Location = new System.Drawing.Point(16, 6);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(762, 78);
            this.groupBox9.TabIndex = 2;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "通信端口设置：";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cb_Parity);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.tb_StopBit);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.tb_Databit);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.tb_Baud);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.tb_ComNum);
            this.panel1.Controls.Add(this.label27);
            this.panel1.Location = new System.Drawing.Point(28, 18);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(699, 46);
            this.panel1.TabIndex = 1;
            // 
            // cb_Parity
            // 
            this.cb_Parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Parity.FormattingEnabled = true;
            this.cb_Parity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.cb_Parity.Location = new System.Drawing.Point(621, 14);
            this.cb_Parity.Name = "cb_Parity";
            this.cb_Parity.Size = new System.Drawing.Size(49, 20);
            this.cb_Parity.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(569, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "奇偶：";
            // 
            // tb_StopBit
            // 
            this.tb_StopBit.Location = new System.Drawing.Point(498, 14);
            this.tb_StopBit.Name = "tb_StopBit";
            this.tb_StopBit.Size = new System.Drawing.Size(23, 21);
            this.tb_StopBit.TabIndex = 23;
            this.tb_StopBit.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(446, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "停止位：";
            // 
            // tb_Databit
            // 
            this.tb_Databit.Location = new System.Drawing.Point(385, 14);
            this.tb_Databit.Name = "tb_Databit";
            this.tb_Databit.Size = new System.Drawing.Size(24, 21);
            this.tb_Databit.TabIndex = 21;
            this.tb_Databit.Text = "7";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(331, 17);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(59, 13);
            this.label25.TabIndex = 20;
            this.label25.Text = "数据位：";
            // 
            // tb_Baud
            // 
            this.tb_Baud.Location = new System.Drawing.Point(235, 14);
            this.tb_Baud.Name = "tb_Baud";
            this.tb_Baud.Size = new System.Drawing.Size(47, 21);
            this.tb_Baud.TabIndex = 19;
            this.tb_Baud.Text = "9600";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(181, 17);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(59, 13);
            this.label26.TabIndex = 18;
            this.label26.Text = "波特率：";
            // 
            // tb_ComNum
            // 
            this.tb_ComNum.Location = new System.Drawing.Point(48, 14);
            this.tb_ComNum.Name = "tb_ComNum";
            this.tb_ComNum.Size = new System.Drawing.Size(67, 21);
            this.tb_ComNum.TabIndex = 17;
            this.tb_ComNum.Text = "COM3";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(8, 17);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(46, 13);
            this.label27.TabIndex = 16;
            this.label27.Text = "串口：";
            // 
            // gb_Y
            // 
            this.gb_Y.Controls.Add(this.groupBox6);
            this.gb_Y.Controls.Add(this.groupBox7);
            this.gb_Y.Controls.Add(this.groupBox8);
            this.gb_Y.Location = new System.Drawing.Point(16, 260);
            this.gb_Y.Name = "gb_Y";
            this.gb_Y.Size = new System.Drawing.Size(762, 161);
            this.gb_Y.TabIndex = 1;
            this.gb_Y.TabStop = false;
            this.gb_Y.Text = "Y轴电机控制：";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btn_MoveHomeY);
            this.groupBox6.Controls.Add(this.btn_MovePosY);
            this.groupBox6.Location = new System.Drawing.Point(437, 21);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(200, 125);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "运动控制：";
            // 
            // btn_MoveHomeY
            // 
            this.btn_MoveHomeY.Location = new System.Drawing.Point(52, 73);
            this.btn_MoveHomeY.Name = "btn_MoveHomeY";
            this.btn_MoveHomeY.Size = new System.Drawing.Size(97, 23);
            this.btn_MoveHomeY.TabIndex = 3;
            this.btn_MoveHomeY.Text = "移动至Home";
            this.btn_MoveHomeY.UseVisualStyleBackColor = true;
            this.btn_MoveHomeY.Click += new System.EventHandler(this.btn_MoveHomeY_Click);
            // 
            // btn_MovePosY
            // 
            this.btn_MovePosY.Location = new System.Drawing.Point(52, 35);
            this.btn_MovePosY.Name = "btn_MovePosY";
            this.btn_MovePosY.Size = new System.Drawing.Size(97, 23);
            this.btn_MovePosY.TabIndex = 3;
            this.btn_MovePosY.Text = "移动至目标位置";
            this.btn_MovePosY.UseVisualStyleBackColor = true;
            this.btn_MovePosY.Click += new System.EventHandler(this.btn_MovePosY_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.textBox4);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.tb_MoveLocY);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Location = new System.Drawing.Point(228, 21);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(200, 125);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "位置设置：";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(94, 57);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(51, 21);
            this.textBox4.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "设置速度：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_MoveLocY
            // 
            this.tb_MoveLocY.Location = new System.Drawing.Point(94, 30);
            this.tb_MoveLocY.Name = "tb_MoveLocY";
            this.tb_MoveLocY.Size = new System.Drawing.Size(51, 21);
            this.tb_MoveLocY.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "设置位置：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.textBox6);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.button9);
            this.groupBox8.Controls.Add(this.button10);
            this.groupBox8.Location = new System.Drawing.Point(19, 21);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(200, 125);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "点动模式：";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(97, 30);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(51, 21);
            this.textBox6.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "设置速度：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(97, 66);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(51, 23);
            this.button9.TabIndex = 0;
            this.button9.Text = "JOG-";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(22, 66);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(51, 23);
            this.button10.TabIndex = 0;
            this.button10.Text = "JOG+";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // gb_X
            // 
            this.gb_X.Controls.Add(this.groupBox4);
            this.gb_X.Controls.Add(this.groupBox3);
            this.gb_X.Controls.Add(this.groupBox1);
            this.gb_X.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gb_X.Location = new System.Drawing.Point(16, 90);
            this.gb_X.Name = "gb_X";
            this.gb_X.Size = new System.Drawing.Size(762, 161);
            this.gb_X.TabIndex = 1;
            this.gb_X.TabStop = false;
            this.gb_X.Text = "X轴电机控制：";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_MoveHomeX);
            this.groupBox4.Controls.Add(this.btn_MovePosX);
            this.groupBox4.Location = new System.Drawing.Point(437, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 125);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "运动控制：";
            // 
            // btn_MoveHomeX
            // 
            this.btn_MoveHomeX.Location = new System.Drawing.Point(52, 73);
            this.btn_MoveHomeX.Name = "btn_MoveHomeX";
            this.btn_MoveHomeX.Size = new System.Drawing.Size(97, 23);
            this.btn_MoveHomeX.TabIndex = 3;
            this.btn_MoveHomeX.Text = "移动至Home";
            this.btn_MoveHomeX.UseVisualStyleBackColor = true;
            this.btn_MoveHomeX.Click += new System.EventHandler(this.btn_MoveHomeX_Click);
            // 
            // btn_MovePosX
            // 
            this.btn_MovePosX.Location = new System.Drawing.Point(52, 35);
            this.btn_MovePosX.Name = "btn_MovePosX";
            this.btn_MovePosX.Size = new System.Drawing.Size(97, 23);
            this.btn_MovePosX.TabIndex = 3;
            this.btn_MovePosX.Text = "移动至目标位置";
            this.btn_MovePosX.UseVisualStyleBackColor = true;
            this.btn_MovePosX.Click += new System.EventHandler(this.btn_MovePosX_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.tb_MoveLocX);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(228, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 125);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "位置设置：";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(88, 71);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(51, 21);
            this.textBox3.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "设置速度：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_MoveLocX
            // 
            this.tb_MoveLocX.Location = new System.Drawing.Point(88, 29);
            this.tb_MoveLocX.Name = "tb_MoveLocX";
            this.tb_MoveLocX.Size = new System.Drawing.Size(51, 21);
            this.tb_MoveLocX.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "设置位置：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(19, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 125);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "点动模式：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(97, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(51, 21);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "设置速度：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(97, 66);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(51, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "JOG-";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "JOG+";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(797, 433);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "相机参数";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_LocalCamera);
            this.groupBox2.Controls.Add(this.tb_GlobalCamera);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(15, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 206);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "相机选择";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "全局视野相机：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 107);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "局部视野相机：";
            // 
            // tb_GlobalCamera
            // 
            this.tb_GlobalCamera.Location = new System.Drawing.Point(123, 47);
            this.tb_GlobalCamera.Name = "tb_GlobalCamera";
            this.tb_GlobalCamera.Size = new System.Drawing.Size(124, 21);
            this.tb_GlobalCamera.TabIndex = 1;
            // 
            // tb_LocalCamera
            // 
            this.tb_LocalCamera.Location = new System.Drawing.Point(123, 104);
            this.tb_LocalCamera.Name = "tb_LocalCamera";
            this.tb_LocalCamera.Size = new System.Drawing.Size(124, 21);
            this.tb_LocalCamera.TabIndex = 1;
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(698, 465);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(82, 30);
            this.btn_Exit.TabIndex = 2;
            this.btn_Exit.Text = "退出";
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(608, 465);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(82, 30);
            this.btn_Save.TabIndex = 2;
            this.btn_Save.Text = "保存";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 497);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmConfig";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "参数设置";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab_Plc.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gb_Y.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.gb_X.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Exit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Save)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_Plc;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cb_Parity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_StopBit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_Databit;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tb_Baud;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox tb_ComNum;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.GroupBox gb_Y;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btn_MoveHomeY;
        private System.Windows.Forms.Button btn_MovePosY;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_MoveLocY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.GroupBox gb_X;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_MoveHomeX;
        private System.Windows.Forms.Button btn_MovePosX;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_MoveLocX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_LocalCamera;
        private System.Windows.Forms.TextBox tb_GlobalCamera;
        private Telerik.WinControls.UI.RadButton btn_Save;
        private Telerik.WinControls.UI.RadButton btn_Exit;
    }
}
