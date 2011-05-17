namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_LcdColor : UserControl
    {
        private IContainer components;
        internal FontColorControl fccLcdMini;
        internal FontColorControl fccLcdPersonal;
        private GroupBox groupBox10;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        internal NumericUpDown nudLcdMiniVSpacing;
        internal NumericUpDown nudLcdPersonalVSpacing;
        public PictureBox pbColorLcd;

        public Options_LcdColor()
        {
            this.InitializeComponent();
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
            this.groupBox10 = new GroupBox();
            this.pbColorLcd = new PictureBox();
            this.label1 = new Label();
            this.nudLcdMiniVSpacing = new NumericUpDown();
            this.label2 = new Label();
            this.nudLcdPersonalVSpacing = new NumericUpDown();
            this.fccLcdPersonal = new FontColorControl();
            this.fccLcdMini = new FontColorControl();
            this.label3 = new Label();
            this.label4 = new Label();
            this.groupBox10.SuspendLayout();
            ((ISupportInitialize) this.pbColorLcd).BeginInit();
            this.nudLcdMiniVSpacing.BeginInit();
            this.nudLcdPersonalVSpacing.BeginInit();
            base.SuspendLayout();
            this.groupBox10.Controls.Add(this.pbColorLcd);
            this.groupBox10.Location = new Point(3, 3);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new Size(0x14e, 0x109);
            this.groupBox10.TabIndex = 0x99;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "LCD Preview";
            this.pbColorLcd.BackColor = Color.Beige;
            this.pbColorLcd.BorderStyle = BorderStyle.FixedSingle;
            this.pbColorLcd.InitialImage = null;
            this.pbColorLcd.Location = new Point(6, 0x10);
            this.pbColorLcd.Name = "pbColorLcd";
            this.pbColorLcd.Size = new Size(0x142, 0xf2);
            this.pbColorLcd.TabIndex = 13;
            this.pbColorLcd.TabStop = false;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x157, 0x125);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x38, 13);
            this.label1.TabIndex = 0x9b;
            this.label1.Text = "V-Spacing";
            this.nudLcdMiniVSpacing.Location = new Point(0x195, 0x121);
            int[] bits = new int[4];
            bits[0] = 0x40;
            this.nudLcdMiniVSpacing.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.nudLcdMiniVSpacing.Minimum = new decimal(numArray2);
            this.nudLcdMiniVSpacing.Name = "nudLcdMiniVSpacing";
            this.nudLcdMiniVSpacing.Size = new Size(0x26, 20);
            this.nudLcdMiniVSpacing.TabIndex = 0x9c;
            int[] numArray3 = new int[4];
            numArray3[0] = 0x10;
            this.nudLcdMiniVSpacing.Value = new decimal(numArray3);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x157, 0x15d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x38, 13);
            this.label2.TabIndex = 0x9b;
            this.label2.Text = "V-Spacing";
            this.nudLcdPersonalVSpacing.Location = new Point(0x195, 0x159);
            int[] numArray4 = new int[4];
            numArray4[0] = 0x40;
            this.nudLcdPersonalVSpacing.Maximum = new decimal(numArray4);
            int[] numArray5 = new int[4];
            numArray5[0] = 1;
            this.nudLcdPersonalVSpacing.Minimum = new decimal(numArray5);
            this.nudLcdPersonalVSpacing.Name = "nudLcdPersonalVSpacing";
            this.nudLcdPersonalVSpacing.Size = new Size(0x26, 20);
            this.nudLcdPersonalVSpacing.TabIndex = 0x9c;
            int[] numArray6 = new int[4];
            numArray6[0] = 0x10;
            this.nudLcdPersonalVSpacing.Value = new decimal(numArray6);
            this.fccLcdPersonal.AutoSize = true;
            this.fccLcdPersonal.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.fccLcdPersonal.BackColorSetting = Color.Black;
            this.fccLcdPersonal.FontChangable = true;
            this.fccLcdPersonal.FontSetting = new Font("Courier New", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.fccLcdPersonal.ForeColorSetting = Color.White;
            this.fccLcdPersonal.Location = new Point(0x5b, 330);
            this.fccLcdPersonal.Name = "fccLcdPersonal";
            this.fccLcdPersonal.Size = new Size(0x100, 50);
            this.fccLcdPersonal.TabIndex = 0x9a;
            this.fccLcdPersonal.Text = " ";
            this.fccLcdMini.AutoSize = true;
            this.fccLcdMini.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.fccLcdMini.BackColorSetting = Color.Black;
            this.fccLcdMini.FontChangable = true;
            this.fccLcdMini.FontSetting = new Font("Courier New", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.fccLcdMini.ForeColorSetting = Color.Yellow;
            this.fccLcdMini.Location = new Point(0x5b, 0x112);
            this.fccLcdMini.Name = "fccLcdMini";
            this.fccLcdMini.Size = new Size(0x100, 50);
            this.fccLcdMini.TabIndex = 0x9a;
            this.fccLcdMini.Text = " ";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(9, 0x125);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x38, 13);
            this.label3.TabIndex = 0x9d;
            this.label3.Text = "Mini Parse";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(9, 0x15d);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x4e, 13);
            this.label4.TabIndex = 0x9d;
            this.label4.Text = "Personal Parse";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.nudLcdPersonalVSpacing);
            base.Controls.Add(this.nudLcdMiniVSpacing);
            base.Controls.Add(this.fccLcdPersonal);
            base.Controls.Add(this.fccLcdMini);
            base.Controls.Add(this.groupBox10);
            base.Name = "Options_LcdColor";
            base.Size = new Size(0x1be, 0x17f);
            base.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox10.ResumeLayout(false);
            ((ISupportInitialize) this.pbColorLcd).EndInit();
            this.nudLcdMiniVSpacing.EndInit();
            this.nudLcdPersonalVSpacing.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

