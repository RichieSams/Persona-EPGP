namespace Advanced_Combat_Tracker
{
    using GammaJul.LgLcd;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Windows.Forms;

    internal class Options_LcdMono : UserControl
    {
        internal Button btnLcdFont;
        private IContainer components;
        private GroupBox groupBox7;
        private GroupBox groupBox9;
        private Label label50;
        private Label label56;
        private Label label57;
        private Label label58;
        private Label label59;
        private Label label60;
        private Label label61;
        private Label label62;
        private Label label63;
        private Label label64;
        private Label label65;
        private Label label66;
        private Label label67;
        internal NumericUpDown nudLcd0FontOffset;
        internal NumericUpDown nudLcd0VSpacing;
        internal NumericUpDown nudLcd1FontOffset;
        internal NumericUpDown nudLcd1VSpacing;
        internal NumericUpDown nudLcd2FontOffset;
        internal NumericUpDown nudLcd2VSpacing;
        internal NumericUpDown nudLcd3FontOffset;
        internal NumericUpDown nudLcd3VSpacing;
        private Panel panel1;
        private Panel panel11;
        private Panel panel12;
        private Panel panel2;
        internal PictureBox pbLcd;

        public Options_LcdMono()
        {
            this.InitializeComponent();
        }

        private void btnLcdFont_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog {
                Font = this.btnLcdFont.Font,
                ShowColor = false,
                ShowEffects = false,
                AllowVerticalFonts = false,
                FontMustExist = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Font font = new Font(dialog.Font.Name, 7f);
                this.btnLcdFont.Font = font;
            }
        }

        private void control_MouseHover(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.control_MouseHover(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.groupBox9 = new GroupBox();
            this.panel1 = new Panel();
            this.label57 = new Label();
            this.label58 = new Label();
            this.nudLcd0FontOffset = new NumericUpDown();
            this.label50 = new Label();
            this.nudLcd0VSpacing = new NumericUpDown();
            this.panel2 = new Panel();
            this.label59 = new Label();
            this.label60 = new Label();
            this.nudLcd1FontOffset = new NumericUpDown();
            this.label61 = new Label();
            this.nudLcd1VSpacing = new NumericUpDown();
            this.panel11 = new Panel();
            this.label62 = new Label();
            this.label63 = new Label();
            this.nudLcd2FontOffset = new NumericUpDown();
            this.label64 = new Label();
            this.nudLcd2VSpacing = new NumericUpDown();
            this.panel12 = new Panel();
            this.label65 = new Label();
            this.label66 = new Label();
            this.nudLcd3FontOffset = new NumericUpDown();
            this.label67 = new Label();
            this.nudLcd3VSpacing = new NumericUpDown();
            this.groupBox7 = new GroupBox();
            this.pbLcd = new PictureBox();
            this.btnLcdFont = new Button();
            this.label56 = new Label();
            this.groupBox9.SuspendLayout();
            this.panel1.SuspendLayout();
            this.nudLcd0FontOffset.BeginInit();
            this.nudLcd0VSpacing.BeginInit();
            this.panel2.SuspendLayout();
            this.nudLcd1FontOffset.BeginInit();
            this.nudLcd1VSpacing.BeginInit();
            this.panel11.SuspendLayout();
            this.nudLcd2FontOffset.BeginInit();
            this.nudLcd2VSpacing.BeginInit();
            this.panel12.SuspendLayout();
            this.nudLcd3FontOffset.BeginInit();
            this.nudLcd3VSpacing.BeginInit();
            this.groupBox7.SuspendLayout();
            ((ISupportInitialize) this.pbLcd).BeginInit();
            base.SuspendLayout();
            this.groupBox9.Controls.Add(this.panel1);
            this.groupBox9.Controls.Add(this.panel2);
            this.groupBox9.Controls.Add(this.panel11);
            this.groupBox9.Controls.Add(this.panel12);
            this.groupBox9.Location = new Point(190, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new Size(0xeb, 0x8b);
            this.groupBox9.TabIndex = 0x9a;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Page Text Settings";
            this.panel1.Controls.Add(this.label57);
            this.panel1.Controls.Add(this.label58);
            this.panel1.Controls.Add(this.nudLcd0FontOffset);
            this.panel1.Controls.Add(this.label50);
            this.panel1.Controls.Add(this.nudLcd0VSpacing);
            this.panel1.Location = new Point(6, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x6f, 0x39);
            this.panel1.TabIndex = 0x84;
            this.label57.AutoSize = true;
            this.label57.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label57.Location = new Point(0x1b, 0);
            this.label57.Name = "label57";
            this.label57.Size = new Size(0x38, 12);
            this.label57.TabIndex = 0x84;
            this.label57.Text = "MiniParse";
            this.label58.AutoSize = true;
            this.label58.Location = new Point(3, 0x25);
            this.label58.Name = "label58";
            this.label58.Size = new Size(0x38, 13);
            this.label58.TabIndex = 1;
            this.label58.Text = "V-Spacing";
            this.nudLcd0FontOffset.Location = new Point(70, 0x10);
            int[] bits = new int[4];
            bits[0] = 10;
            this.nudLcd0FontOffset.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 5;
            numArray2[3] = -2147483648;
            this.nudLcd0FontOffset.Minimum = new decimal(numArray2);
            this.nudLcd0FontOffset.Name = "nudLcd0FontOffset";
            this.nudLcd0FontOffset.Size = new Size(0x26, 20);
            this.nudLcd0FontOffset.TabIndex = 0;
            this.nudLcd0FontOffset.ValueChanged += new EventHandler(this.nudLcdOffset_ValueChanged);
            this.label50.AutoSize = true;
            this.label50.Location = new Point(3, 0x12);
            this.label50.Name = "label50";
            this.label50.Size = new Size(0x3a, 13);
            this.label50.TabIndex = 0x85;
            this.label50.Text = "Size Offset";
            this.nudLcd0VSpacing.Location = new Point(70, 0x23);
            int[] numArray3 = new int[4];
            numArray3[0] = 15;
            this.nudLcd0VSpacing.Maximum = new decimal(numArray3);
            int[] numArray4 = new int[4];
            numArray4[0] = 1;
            this.nudLcd0VSpacing.Minimum = new decimal(numArray4);
            this.nudLcd0VSpacing.Name = "nudLcd0VSpacing";
            this.nudLcd0VSpacing.Size = new Size(0x26, 20);
            this.nudLcd0VSpacing.TabIndex = 0x88;
            int[] numArray5 = new int[4];
            numArray5[0] = 7;
            this.nudLcd0VSpacing.Value = new decimal(numArray5);
            this.nudLcd0VSpacing.ValueChanged += new EventHandler(this.nudLcdOffset_ValueChanged);
            this.panel2.Controls.Add(this.label59);
            this.panel2.Controls.Add(this.label60);
            this.panel2.Controls.Add(this.nudLcd1FontOffset);
            this.panel2.Controls.Add(this.label61);
            this.panel2.Controls.Add(this.nudLcd1VSpacing);
            this.panel2.Location = new Point(0x76, 15);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x6f, 0x39);
            this.panel2.TabIndex = 0x89;
            this.label59.AutoSize = true;
            this.label59.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label59.Location = new Point(0x10, 0);
            this.label59.Name = "label59";
            this.label59.Size = new Size(0x4f, 12);
            this.label59.TabIndex = 0x8a;
            this.label59.Text = "Personal Stats";
            this.label60.AutoSize = true;
            this.label60.Enabled = false;
            this.label60.Location = new Point(3, 0x25);
            this.label60.Name = "label60";
            this.label60.Size = new Size(0x38, 13);
            this.label60.TabIndex = 1;
            this.label60.Text = "V-Spacing";
            this.nudLcd1FontOffset.Location = new Point(70, 0x10);
            int[] numArray6 = new int[4];
            numArray6[0] = 10;
            this.nudLcd1FontOffset.Maximum = new decimal(numArray6);
            int[] numArray7 = new int[4];
            numArray7[0] = 5;
            numArray7[3] = -2147483648;
            this.nudLcd1FontOffset.Minimum = new decimal(numArray7);
            this.nudLcd1FontOffset.Name = "nudLcd1FontOffset";
            this.nudLcd1FontOffset.Size = new Size(0x26, 20);
            this.nudLcd1FontOffset.TabIndex = 0;
            int[] numArray8 = new int[4];
            numArray8[0] = 3;
            this.nudLcd1FontOffset.Value = new decimal(numArray8);
            this.nudLcd1FontOffset.ValueChanged += new EventHandler(this.nudLcdOffset_ValueChanged);
            this.label61.AutoSize = true;
            this.label61.Location = new Point(3, 0x12);
            this.label61.Name = "label61";
            this.label61.Size = new Size(0x3a, 13);
            this.label61.TabIndex = 0x8b;
            this.label61.Text = "Size Offset";
            this.nudLcd1VSpacing.Location = new Point(70, 0x23);
            int[] numArray9 = new int[4];
            numArray9[0] = 15;
            this.nudLcd1VSpacing.Maximum = new decimal(numArray9);
            this.nudLcd1VSpacing.Name = "nudLcd1VSpacing";
            this.nudLcd1VSpacing.Size = new Size(0x26, 20);
            this.nudLcd1VSpacing.TabIndex = 0x8f;
            int[] numArray10 = new int[4];
            numArray10[0] = 12;
            this.nudLcd1VSpacing.Value = new decimal(numArray10);
            this.nudLcd1VSpacing.ValueChanged += new EventHandler(this.nudLcdOffset_ValueChanged);
            this.panel11.Controls.Add(this.label62);
            this.panel11.Controls.Add(this.label63);
            this.panel11.Controls.Add(this.nudLcd2FontOffset);
            this.panel11.Controls.Add(this.label64);
            this.panel11.Controls.Add(this.nudLcd2VSpacing);
            this.panel11.Location = new Point(6, 0x4a);
            this.panel11.Name = "panel11";
            this.panel11.Size = new Size(0x6f, 0x39);
            this.panel11.TabIndex = 0x90;
            this.label62.AutoSize = true;
            this.label62.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label62.Location = new Point(0x1b, 0);
            this.label62.Name = "label62";
            this.label62.Size = new Size(0x39, 12);
            this.label62.TabIndex = 0x91;
            this.label62.Text = "AE Timers";
            this.label63.AutoSize = true;
            this.label63.Location = new Point(3, 0x25);
            this.label63.Name = "label63";
            this.label63.Size = new Size(0x38, 13);
            this.label63.TabIndex = 1;
            this.label63.Text = "V-Spacing";
            this.nudLcd2FontOffset.Location = new Point(70, 0x10);
            int[] numArray11 = new int[4];
            numArray11[0] = 10;
            this.nudLcd2FontOffset.Maximum = new decimal(numArray11);
            int[] numArray12 = new int[4];
            numArray12[0] = 5;
            numArray12[3] = -2147483648;
            this.nudLcd2FontOffset.Minimum = new decimal(numArray12);
            this.nudLcd2FontOffset.Name = "nudLcd2FontOffset";
            this.nudLcd2FontOffset.Size = new Size(0x26, 20);
            this.nudLcd2FontOffset.TabIndex = 0;
            int[] numArray13 = new int[4];
            numArray13[0] = 2;
            this.nudLcd2FontOffset.Value = new decimal(numArray13);
            this.nudLcd2FontOffset.ValueChanged += new EventHandler(this.nudLcdOffset_ValueChanged);
            this.label64.AutoSize = true;
            this.label64.Location = new Point(3, 0x12);
            this.label64.Name = "label64";
            this.label64.Size = new Size(0x3a, 13);
            this.label64.TabIndex = 0x92;
            this.label64.Text = "Size Offset";
            this.nudLcd2VSpacing.Location = new Point(70, 0x23);
            int[] numArray14 = new int[4];
            numArray14[0] = 15;
            this.nudLcd2VSpacing.Maximum = new decimal(numArray14);
            int[] numArray15 = new int[4];
            numArray15[0] = 1;
            this.nudLcd2VSpacing.Minimum = new decimal(numArray15);
            this.nudLcd2VSpacing.Name = "nudLcd2VSpacing";
            this.nudLcd2VSpacing.Size = new Size(0x26, 20);
            this.nudLcd2VSpacing.TabIndex = 2;
            int[] numArray16 = new int[4];
            numArray16[0] = 11;
            this.nudLcd2VSpacing.Value = new decimal(numArray16);
            this.nudLcd2VSpacing.ValueChanged += new EventHandler(this.nudLcdOffset_ValueChanged);
            this.panel12.Controls.Add(this.label65);
            this.panel12.Controls.Add(this.label66);
            this.panel12.Controls.Add(this.nudLcd3FontOffset);
            this.panel12.Controls.Add(this.label67);
            this.panel12.Controls.Add(this.nudLcd3VSpacing);
            this.panel12.Location = new Point(0x76, 0x4a);
            this.panel12.Name = "panel12";
            this.panel12.Size = new Size(0x6f, 0x39);
            this.panel12.TabIndex = 150;
            this.label65.AutoSize = true;
            this.label65.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label65.Location = new Point(0x1d, 0);
            this.label65.Name = "label65";
            this.label65.Size = new Size(0x34, 12);
            this.label65.TabIndex = 0x97;
            this.label65.Text = "Sort Bars";
            this.label66.AutoSize = true;
            this.label66.Location = new Point(3, 0x25);
            this.label66.Name = "label66";
            this.label66.Size = new Size(0x38, 13);
            this.label66.TabIndex = 1;
            this.label66.Text = "V-Spacing";
            this.nudLcd3FontOffset.Location = new Point(70, 0x10);
            int[] numArray17 = new int[4];
            numArray17[0] = 10;
            this.nudLcd3FontOffset.Maximum = new decimal(numArray17);
            int[] numArray18 = new int[4];
            numArray18[0] = 5;
            numArray18[3] = -2147483648;
            this.nudLcd3FontOffset.Minimum = new decimal(numArray18);
            this.nudLcd3FontOffset.Name = "nudLcd3FontOffset";
            this.nudLcd3FontOffset.Size = new Size(0x26, 20);
            this.nudLcd3FontOffset.TabIndex = 0;
            this.nudLcd3FontOffset.ValueChanged += new EventHandler(this.nudLcdOffset_ValueChanged);
            this.label67.AutoSize = true;
            this.label67.Location = new Point(3, 0x12);
            this.label67.Name = "label67";
            this.label67.Size = new Size(0x3a, 13);
            this.label67.TabIndex = 0x98;
            this.label67.Text = "Size Offset";
            this.nudLcd3VSpacing.Location = new Point(70, 0x23);
            int[] numArray19 = new int[4];
            numArray19[0] = 15;
            this.nudLcd3VSpacing.Maximum = new decimal(numArray19);
            int[] numArray20 = new int[4];
            numArray20[0] = 1;
            this.nudLcd3VSpacing.Minimum = new decimal(numArray20);
            this.nudLcd3VSpacing.Name = "nudLcd3VSpacing";
            this.nudLcd3VSpacing.Size = new Size(0x26, 20);
            this.nudLcd3VSpacing.TabIndex = 0x9c;
            int[] numArray21 = new int[4];
            numArray21[0] = 9;
            this.nudLcd3VSpacing.Value = new decimal(numArray21);
            this.nudLcd3VSpacing.ValueChanged += new EventHandler(this.nudLcdOffset_ValueChanged);
            this.groupBox7.Controls.Add(this.pbLcd);
            this.groupBox7.Location = new Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new Size(0xb0, 0x44);
            this.groupBox7.TabIndex = 0x99;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "LCD Preview";
            this.pbLcd.BackColor = Color.Beige;
            this.pbLcd.BorderStyle = BorderStyle.FixedSingle;
            this.pbLcd.InitialImage = null;
            this.pbLcd.Location = new Point(6, 0x10);
            this.pbLcd.Name = "pbLcd";
            this.pbLcd.Size = new Size(0xa2, 0x2d);
            this.pbLcd.TabIndex = 13;
            this.pbLcd.TabStop = false;
            this.btnLcdFont.BackColor = Color.Beige;
            this.btnLcdFont.Font = new Font("Lucida Console", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnLcdFont.Location = new Point(100, 0x4d);
            this.btnLcdFont.Name = "btnLcdFont";
            this.btnLcdFont.Size = new Size(0x4f, 0x19);
            this.btnLcdFont.TabIndex = 0x9b;
            this.btnLcdFont.Text = "Font";
            this.btnLcdFont.UseVisualStyleBackColor = false;
            this.btnLcdFont.Click += new EventHandler(this.btnLcdFont_Click);
            this.btnLcdFont.MouseHover += new EventHandler(this.control_MouseHover);
            this.label56.AutoSize = true;
            this.label56.Location = new Point(6, 0x53);
            this.label56.Name = "label56";
            this.label56.Size = new Size(0x3f, 13);
            this.label56.TabIndex = 0x9c;
            this.label56.Text = "Master Font";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.btnLcdFont);
            base.Controls.Add(this.label56);
            base.Controls.Add(this.groupBox9);
            base.Controls.Add(this.groupBox7);
            base.Name = "Options_LcdMono";
            base.Size = new Size(0x1ac, 0x91);
            base.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox9.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.nudLcd0FontOffset.EndInit();
            this.nudLcd0VSpacing.EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.nudLcd1FontOffset.EndInit();
            this.nudLcd1VSpacing.EndInit();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.nudLcd2FontOffset.EndInit();
            this.nudLcd2VSpacing.EndInit();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.nudLcd3FontOffset.EndInit();
            this.nudLcd3VSpacing.EndInit();
            this.groupBox7.ResumeLayout(false);
            ((ISupportInitialize) this.pbLcd).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void nudLcdOffset_ValueChanged(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(160, 0x2b);
            Graphics graphics = Graphics.FromImage(image);
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            graphics.Clear(Color.Beige);
            SolidBrush brush = new SolidBrush(Color.Black);
            for (int i = 0; i < 4; i++)
            {
                int num2 = 0;
                switch (i)
                {
                    case 0:
                        num2 = Convert.ToInt32(ActGlobals.oFormActMain.opLcdMono.btnLcdFont.Font.Size) + ((int) this.nudLcd0FontOffset.Value);
                        break;

                    case 1:
                        num2 = Convert.ToInt32(ActGlobals.oFormActMain.opLcdMono.btnLcdFont.Font.Size) + ((int) this.nudLcd1FontOffset.Value);
                        break;

                    case 2:
                        num2 = Convert.ToInt32(ActGlobals.oFormActMain.opLcdMono.btnLcdFont.Font.Size) + ((int) this.nudLcd2FontOffset.Value);
                        break;

                    case 3:
                        num2 = Convert.ToInt32(ActGlobals.oFormActMain.opLcdMono.btnLcdFont.Font.Size) + ((int) this.nudLcd3FontOffset.Value);
                        break;
                }
                Font font = new Font(ActGlobals.oFormActMain.opLcdMono.btnLcdFont.Font.Name, (float) num2);
                int num3 = i * 0x24;
                switch (i)
                {
                    case 0:
                    {
                        int num1 = (int) this.nudLcd0VSpacing.Value;
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) 0);
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) ((int) this.nudLcd0VSpacing.Value));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd0VSpacing.Value) * 2));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd0VSpacing.Value) * 3));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd0VSpacing.Value) * 4));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd0VSpacing.Value) * 5));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd0VSpacing.Value) * 6));
                        break;
                    }
                    case 1:
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) 1);
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (num2 + 1));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) ((num2 * 2) + 1));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) ((num2 * 3) + 1));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) ((num2 * 4) + 1));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) ((num2 * 5) + 1));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) ((num2 * 6) + 1));
                        break;

                    case 2:
                    {
                        int num6 = (int) this.nudLcd2VSpacing.Value;
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) 0);
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) ((int) this.nudLcd2VSpacing.Value));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd2VSpacing.Value) * 2));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd2VSpacing.Value) * 3));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd2VSpacing.Value) * 4));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd2VSpacing.Value) * 5));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd2VSpacing.Value) * 6));
                        break;
                    }
                    case 3:
                    {
                        int num7 = (int) this.nudLcd3VSpacing.Value;
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) 0);
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) ((int) this.nudLcd3VSpacing.Value));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd3VSpacing.Value) * 2));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd3VSpacing.Value) * 3));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd3VSpacing.Value) * 4));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd3VSpacing.Value) * 5));
                        graphics.DrawString("Sz" + num2, font, brush, (float) num3, (float) (((int) this.nudLcd3VSpacing.Value) * 6));
                        break;
                    }
                }
            }
            this.pbLcd.Image = image;
            if (ActGlobals.oFormActMain.opLcdGeneral.cbLcdEnabled.Checked)
            {
                if (ActGlobals.oFormActMain.opLcdGeneral.cbLcdRoute.Checked && ActGlobals.oFormActMain.opLcdGeneral.cbLcdRoute.Enabled)
                {
                    try
                    {
                        if (ActGlobals.oFormActMain.lcdDeviceType == LcdDeviceType.Monochrome)
                        {
                            byte[] lcdData = ActGlobals.oFormActMain.LcdGetMonoView(image, 0);
                            ActGlobals.oFormActMain.LcdSendToSharer(lcdData);
                        }
                    }
                    catch (Exception exception)
                    {
                        ActGlobals.oFormActMain.UpdateLcdStatus(exception.Message, true);
                    }
                }
                else if (ActGlobals.oFormActMain.lcdDeviceType == LcdDeviceType.Monochrome)
                {
                    ActGlobals.oFormActMain.lcdDevice.UpdateBitmap(ActGlobals.oFormActMain.LcdGetMonoView(image, 0), LcdPriority.Normal, LcdUpdateMode.Async);
                    ActGlobals.oFormActMain.lcdDevice.DoUpdateAndDraw();
                }
            }
        }
    }
}

