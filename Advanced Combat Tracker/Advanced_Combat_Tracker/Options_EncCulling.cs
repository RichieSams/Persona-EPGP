namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_EncCulling : UserControl
    {
        internal CheckBox cbCullAll;
        internal CheckBox cbCullCount;
        internal CheckBox cbCullCountIgnoreNoAlly;
        internal CheckBox cbCullNoAlly;
        internal CheckBox cbCullOther;
        internal CheckBox cbCullTimer;
        private IContainer components;
        private GroupBox groupBox30;
        private GroupBox groupBox31;
        internal NumericUpDown nudCullAllN;
        internal NumericUpDown nudCullCountN;
        internal NumericUpDown nudCullOtherN;
        internal NumericUpDown nudCullTimerN;

        public Options_EncCulling()
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
            this.groupBox30 = new GroupBox();
            this.cbCullNoAlly = new CheckBox();
            this.cbCullCountIgnoreNoAlly = new CheckBox();
            this.nudCullCountN = new NumericUpDown();
            this.nudCullTimerN = new NumericUpDown();
            this.cbCullCount = new CheckBox();
            this.cbCullTimer = new CheckBox();
            this.groupBox31 = new GroupBox();
            this.nudCullOtherN = new NumericUpDown();
            this.nudCullAllN = new NumericUpDown();
            this.cbCullOther = new CheckBox();
            this.cbCullAll = new CheckBox();
            this.groupBox30.SuspendLayout();
            this.nudCullCountN.BeginInit();
            this.nudCullTimerN.BeginInit();
            this.groupBox31.SuspendLayout();
            this.nudCullOtherN.BeginInit();
            this.nudCullAllN.BeginInit();
            base.SuspendLayout();
            this.groupBox30.Controls.Add(this.cbCullNoAlly);
            this.groupBox30.Controls.Add(this.cbCullCountIgnoreNoAlly);
            this.groupBox30.Controls.Add(this.nudCullCountN);
            this.groupBox30.Controls.Add(this.nudCullTimerN);
            this.groupBox30.Controls.Add(this.cbCullCount);
            this.groupBox30.Controls.Add(this.cbCullTimer);
            this.groupBox30.Location = new Point(3, 3);
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.Size = new Size(0x20a, 0x67);
            this.groupBox30.TabIndex = 3;
            this.groupBox30.TabStop = false;
            this.groupBox30.Text = "General Culling Settings";
            this.groupBox30.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbCullNoAlly.AutoSize = true;
            this.cbCullNoAlly.Checked = true;
            this.cbCullNoAlly.CheckState = CheckState.Checked;
            this.cbCullNoAlly.Location = new Point(14, 0x13);
            this.cbCullNoAlly.Name = "cbCullNoAlly";
            this.cbCullNoAlly.Size = new Size(0x106, 0x11);
            this.cbCullNoAlly.TabIndex = 0;
            this.cbCullNoAlly.Text = "Cull all encounters marked \"Encounter\"  (no allies)";
            this.cbCullNoAlly.UseVisualStyleBackColor = true;
            this.cbCullNoAlly.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbCullCountIgnoreNoAlly.AutoSize = true;
            this.cbCullCountIgnoreNoAlly.Checked = true;
            this.cbCullCountIgnoreNoAlly.CheckState = CheckState.Checked;
            this.cbCullCountIgnoreNoAlly.Location = new Point(0x24, 80);
            this.cbCullCountIgnoreNoAlly.Name = "cbCullCountIgnoreNoAlly";
            this.cbCullCountIgnoreNoAlly.Size = new Size(0x12f, 0x11);
            this.cbCullCountIgnoreNoAlly.TabIndex = 5;
            this.cbCullCountIgnoreNoAlly.Text = "Don't count \"Encounter\" labeled encounters for this count.";
            this.cbCullCountIgnoreNoAlly.UseVisualStyleBackColor = true;
            this.cbCullCountIgnoreNoAlly.MouseHover += new EventHandler(this.control_MouseHover);
            this.nudCullCountN.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.nudCullCountN.Location = new Point(0x1cf, 0x39);
            int[] bits = new int[4];
            bits[0] = 0x3e8;
            this.nudCullCountN.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.nudCullCountN.Minimum = new decimal(numArray2);
            this.nudCullCountN.Name = "nudCullCountN";
            this.nudCullCountN.Size = new Size(0x35, 20);
            this.nudCullCountN.TabIndex = 4;
            int[] numArray3 = new int[4];
            numArray3[0] = 50;
            this.nudCullCountN.Value = new decimal(numArray3);
            this.nudCullTimerN.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.nudCullTimerN.Location = new Point(0x1cf, 0x25);
            int[] numArray4 = new int[4];
            numArray4[0] = 0x3e8;
            this.nudCullTimerN.Maximum = new decimal(numArray4);
            int[] numArray5 = new int[4];
            numArray5[0] = 1;
            this.nudCullTimerN.Minimum = new decimal(numArray5);
            this.nudCullTimerN.Name = "nudCullTimerN";
            this.nudCullTimerN.Size = new Size(0x35, 20);
            this.nudCullTimerN.TabIndex = 2;
            int[] numArray6 = new int[4];
            numArray6[0] = 60;
            this.nudCullTimerN.Value = new decimal(numArray6);
            this.cbCullCount.AutoSize = true;
            this.cbCullCount.Location = new Point(14, 0x3b);
            this.cbCullCount.Name = "cbCullCount";
            this.cbCullCount.Size = new Size(0x161, 0x11);
            this.cbCullCount.TabIndex = 3;
            this.cbCullCount.Text = "Cull old encounters when there are this many normal encounters total:";
            this.cbCullCount.UseVisualStyleBackColor = true;
            this.cbCullCount.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbCullTimer.AutoSize = true;
            this.cbCullTimer.Location = new Point(14, 0x27);
            this.cbCullTimer.Name = "cbCullTimer";
            this.cbCullTimer.Size = new Size(0x152, 0x11);
            this.cbCullTimer.TabIndex = 1;
            this.cbCullTimer.Text = "Cull normal encounters created longer than this many minutes ago:";
            this.cbCullTimer.UseVisualStyleBackColor = true;
            this.cbCullTimer.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox31.Controls.Add(this.nudCullOtherN);
            this.groupBox31.Controls.Add(this.nudCullAllN);
            this.groupBox31.Controls.Add(this.cbCullOther);
            this.groupBox31.Controls.Add(this.cbCullAll);
            this.groupBox31.Location = new Point(3, 0x70);
            this.groupBox31.Name = "groupBox31";
            this.groupBox31.Size = new Size(0x20a, 0x3f);
            this.groupBox31.TabIndex = 4;
            this.groupBox31.TabStop = false;
            this.groupBox31.Text = "Previous Zone Culling";
            this.groupBox31.MouseHover += new EventHandler(this.control_MouseHover);
            this.nudCullOtherN.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.nudCullOtherN.Location = new Point(0x1cf, 0x26);
            int[] numArray7 = new int[4];
            numArray7[0] = 0x3e8;
            this.nudCullOtherN.Maximum = new decimal(numArray7);
            int[] numArray8 = new int[4];
            numArray8[0] = 1;
            this.nudCullOtherN.Minimum = new decimal(numArray8);
            this.nudCullOtherN.Name = "nudCullOtherN";
            this.nudCullOtherN.Size = new Size(0x35, 20);
            this.nudCullOtherN.TabIndex = 3;
            int[] numArray9 = new int[4];
            numArray9[0] = 3;
            this.nudCullOtherN.Value = new decimal(numArray9);
            this.nudCullAllN.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.nudCullAllN.Location = new Point(0x1cf, 0x12);
            int[] numArray10 = new int[4];
            numArray10[0] = 0x3e8;
            this.nudCullAllN.Maximum = new decimal(numArray10);
            int[] numArray11 = new int[4];
            numArray11[0] = 1;
            this.nudCullAllN.Minimum = new decimal(numArray11);
            this.nudCullAllN.Name = "nudCullAllN";
            this.nudCullAllN.Size = new Size(0x35, 20);
            this.nudCullAllN.TabIndex = 1;
            int[] numArray12 = new int[4];
            numArray12[0] = 5;
            this.nudCullAllN.Value = new decimal(numArray12);
            this.cbCullOther.AutoSize = true;
            this.cbCullOther.Location = new Point(14, 40);
            this.cbCullOther.Name = "cbCullOther";
            this.cbCullOther.Size = new Size(0x126, 0x11);
            this.cbCullOther.TabIndex = 2;
            this.cbCullOther.Text = "Cull other encounters from previous zones this many ago:";
            this.cbCullOther.UseVisualStyleBackColor = true;
            this.cbCullOther.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbCullAll.AutoSize = true;
            this.cbCullAll.Location = new Point(14, 20);
            this.cbCullAll.Name = "cbCullAll";
            this.cbCullAll.Size = new Size(0x105, 0x11);
            this.cbCullAll.TabIndex = 0;
            this.cbCullAll.Text = "Cull the \"All\" encounter from this many zones ago:";
            this.cbCullAll.UseVisualStyleBackColor = true;
            this.cbCullAll.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.groupBox30);
            base.Controls.Add(this.groupBox31);
            base.Name = "OpEncCulling";
            base.Size = new Size(540, 0xbd);
            this.groupBox30.ResumeLayout(false);
            this.groupBox30.PerformLayout();
            this.nudCullCountN.EndInit();
            this.nudCullTimerN.EndInit();
            this.groupBox31.ResumeLayout(false);
            this.groupBox31.PerformLayout();
            this.nudCullOtherN.EndInit();
            this.nudCullAllN.EndInit();
            base.ResumeLayout(false);
        }
    }
}

