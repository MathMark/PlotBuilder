namespace PlotBuilder
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.sheet = new System.Windows.Forms.PictureBox();
            this.scale = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.parametricToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametricToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Trigonometry = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Hyperbolical = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Hystory = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.scaletable = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scale)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(113, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(208, 23);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.button1.Location = new System.Drawing.Point(817, 480);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 27);
            this.button1.TabIndex = 4;
            this.button1.Text = "Build";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // sheet
            // 
            this.sheet.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.sheet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sheet.Cursor = System.Windows.Forms.Cursors.Cross;
            this.sheet.Location = new System.Drawing.Point(28, 64);
            this.sheet.MaximumSize = new System.Drawing.Size(700, 700);
            this.sheet.MinimumSize = new System.Drawing.Size(700, 700);
            this.sheet.Name = "sheet";
            this.sheet.Size = new System.Drawing.Size(700, 700);
            this.sheet.TabIndex = 5;
            this.sheet.TabStop = false;
            this.sheet.Paint += new System.Windows.Forms.PaintEventHandler(this.sheet_Paint);
            this.sheet.MouseLeave += new System.EventHandler(this.sheet_MouseLeave);
            this.sheet.MouseMove += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseMove);
            // 
            // scale
            // 
            this.scale.BackColor = System.Drawing.SystemColors.Menu;
            this.scale.DecimalPlaces = 1;
            this.scale.Location = new System.Drawing.Point(0, 28);
            this.scale.Name = "scale";
            this.scale.Size = new System.Drawing.Size(51, 20);
            this.scale.TabIndex = 7;
            this.scale.ValueChanged += new System.EventHandler(this.scale_ValueChanged);
            this.scale.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scale_Scroll);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.button2.Location = new System.Drawing.Point(919, 480);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 27);
            this.button2.TabIndex = 9;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(1107, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(35, 22);
            this.toolStripButton1.Text = "Save";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(44, 22);
            this.toolStripButton2.Text = "About";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(29, 22);
            this.toolStripButton3.Text = "Exit";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.ForeColor = System.Drawing.Color.Maroon;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(83, 22);
            this.toolStripButton4.Text = "Choose Color";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parametricToolStripMenuItem,
            this.parametricToolStripMenuItem1});
            this.toolStripDropDownButton1.ForeColor = System.Drawing.Color.SeaGreen;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(99, 22);
            this.toolStripDropDownButton1.Text = "Representation";
            // 
            // parametricToolStripMenuItem
            // 
            this.parametricToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.parametricToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.parametricToolStripMenuItem.Name = "parametricToolStripMenuItem";
            this.parametricToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.parametricToolStripMenuItem.Text = "Explicit";
            this.parametricToolStripMenuItem.Click += new System.EventHandler(this.parametricToolStripMenuItem_Click);
            // 
            // parametricToolStripMenuItem1
            // 
            this.parametricToolStripMenuItem1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.parametricToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.parametricToolStripMenuItem1.Name = "parametricToolStripMenuItem1";
            this.parametricToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.parametricToolStripMenuItem1.Text = "Parametric";
            this.parametricToolStripMenuItem1.Click += new System.EventHandler(this.parametricToolStripMenuItem1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Location = new System.Drawing.Point(750, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 90);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Function";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(25, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 23);
            this.label4.TabIndex = 28;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox2.Location = new System.Drawing.Point(113, 48);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(208, 23);
            this.textBox2.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(25, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 23);
            this.label3.TabIndex = 26;
            // 
            // Trigonometry
            // 
            this.Trigonometry.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Trigonometry.ForeColor = System.Drawing.Color.Silver;
            this.Trigonometry.FormattingEnabled = true;
            this.Trigonometry.Items.AddRange(new object[] {
            "sin(x)",
            "cos(x)",
            "tan(x)",
            "cot(x)",
            "arcsin(x)",
            "arccos(x)",
            "arctan(x)",
            "arccot(x)"});
            this.Trigonometry.Location = new System.Drawing.Point(19, 19);
            this.Trigonometry.Name = "Trigonometry";
            this.Trigonometry.Size = new System.Drawing.Size(107, 23);
            this.Trigonometry.TabIndex = 12;
            this.Trigonometry.SelectedValueChanged += new System.EventHandler(this.Trigonometry_SelectedValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox2.Controls.Add(this.Trigonometry);
            this.groupBox2.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox2.Location = new System.Drawing.Point(750, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(155, 53);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Trigonomety functions";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.ForeColor = System.Drawing.Color.SteelBlue;
            this.radioButton1.Location = new System.Drawing.Point(13, 18);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(32, 17);
            this.radioButton1.TabIndex = 14;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Y";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.ForeColor = System.Drawing.Color.SteelBlue;
            this.radioButton2.Location = new System.Drawing.Point(13, 41);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(32, 17);
            this.radioButton2.TabIndex = 15;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "X";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox3.Controls.Add(this.Hyperbolical);
            this.groupBox3.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox3.Location = new System.Drawing.Point(929, 150);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(155, 53);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Hyperbolical functions";
            // 
            // Hyperbolical
            // 
            this.Hyperbolical.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Hyperbolical.ForeColor = System.Drawing.Color.SeaGreen;
            this.Hyperbolical.FormattingEnabled = true;
            this.Hyperbolical.Items.AddRange(new object[] {
            "sinh(x)",
            "cosh(x)",
            "tanh(x)",
            "cth(x)",
            "arsinh(x)",
            "arcosh(x)",
            "artanh(x)",
            "arcth(x)"});
            this.Hyperbolical.Location = new System.Drawing.Point(19, 19);
            this.Hyperbolical.Name = "Hyperbolical";
            this.Hyperbolical.Size = new System.Drawing.Size(107, 23);
            this.Hyperbolical.TabIndex = 12;
            this.Hyperbolical.SelectedValueChanged += new System.EventHandler(this.Hyperbolical_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(761, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 17;
            // 
            // Hystory
            // 
            this.Hystory.BackColor = System.Drawing.SystemColors.Menu;
            this.Hystory.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Hystory.FormattingEnabled = true;
            this.Hystory.ItemHeight = 15;
            this.Hystory.Location = new System.Drawing.Point(750, 576);
            this.Hystory.Name = "Hystory";
            this.Hystory.Size = new System.Drawing.Size(343, 199);
            this.Hystory.TabIndex = 20;
            this.Hystory.SelectedValueChanged += new System.EventHandler(this.Hystory_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(865, 535);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Hystory of functions";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Location = new System.Drawing.Point(1020, 223);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(53, 82);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.MouseHover += new System.EventHandler(this.groupBox4_MouseHover);
            // 
            // scaletable
            // 
            this.scaletable.AutoSize = true;
            this.scaletable.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.scaletable.Location = new System.Drawing.Point(653, 767);
            this.scaletable.Name = "scaletable";
            this.scaletable.Size = new System.Drawing.Size(0, 13);
            this.scaletable.TabIndex = 25;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Location = new System.Drawing.Point(10, 44);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(734, 750);
            this.panel.TabIndex = 26;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1107, 787);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.scaletable);
            this.Controls.Add(this.sheet);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Hystory);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.scale);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel);
            this.ForeColor = System.Drawing.Color.DarkRed;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(986, 789);
            this.Name = "Form1";
            this.Text = "PlotBuilder";
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scale)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown scale;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox Trigonometry;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox Hyperbolical;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox Hystory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        public System.Windows.Forms.PictureBox sheet;
        private System.Windows.Forms.Label scaletable;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem parametricToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametricToolStripMenuItem1;
        private System.Windows.Forms.Label panel;
    }
}

