namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_MainTableGen : UserControl
    {
        internal CheckBox cbIdleEnd;
        internal CheckBox cbIdleTimerEnd;
        internal CheckBox cbReverseSort;
        internal CheckBox cbTableCommas;
        private IContainer components;
        private GroupBox groupBox6;
        private Label label2;
        internal NumericUpDown nudIdleLimit;
        internal NumericUpDown nudUpdateValue;

        public Options_MainTableGen()
        {
            this.InitializeComponent();
        }

        private void cbTableCommas_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.mainTableShowCommas = this.cbTableCommas.Checked;
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
            this.nudIdleLimit = new NumericUpDown();
            this.cbIdleEnd = new CheckBox();
            this.cbIdleTimerEnd = new CheckBox();
            this.groupBox6 = new GroupBox();
            this.cbTableCommas = new CheckBox();
            this.cbReverseSort = new CheckBox();
            this.nudUpdateValue = new NumericUpDown();
            this.label2 = new Label();
            this.nudIdleLimit.BeginInit();
            this.groupBox6.SuspendLayout();
            this.nudUpdateValue.BeginInit();
            base.SuspendLayout();
            this.nudIdleLimit.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.nudIdleLimit.Location = new Point(0x204, 0x56);
            int[] bits = new int[4];
            bits[0] = 600;
            this.nudIdleLimit.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 2;
            this.nudIdleLimit.Minimum = new decimal(numArray2);
            this.nudIdleLimit.Name = "nudIdleLimit";
            this.nudIdleLimit.Size = new Size(0x30, 20);
            this.nudIdleLimit.TabIndex = 7;
            int[] numArray3 = new int[4];
            numArray3[0] = 6;
            this.nudIdleLimit.Value = new decimal(numArray3);
            this.cbIdleEnd.AutoSize = true;
            this.cbIdleEnd.Checked = true;
            this.cbIdleEnd.CheckState = CheckState.Checked;
            this.cbIdleEnd.Location = new Point(3, 0x57);
            this.cbIdleEnd.Name = "cbIdleEnd";
            this.cbIdleEnd.Size = new Size(410, 0x11);
            this.cbIdleEnd.TabIndex = 6;
            this.cbIdleEnd.Text = "Number of seconds to wait after the last combat action to begin a new encounter.";
            this.cbIdleEnd.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbIdleTimerEnd.AutoSize = true;
            this.cbIdleTimerEnd.Checked = true;
            this.cbIdleTimerEnd.CheckState = CheckState.Checked;
            this.cbIdleTimerEnd.Location = new Point(0x15, 0x69);
            this.cbIdleTimerEnd.Name = "cbIdleTimerEnd";
            this.cbIdleTimerEnd.Size = new Size(400, 0x11);
            this.cbIdleTimerEnd.TabIndex = 8;
            this.cbIdleTimerEnd.Text = "Use an internal timer to count down instead of relying only on logfile timestamps.";
            this.cbIdleTimerEnd.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox6.Controls.Add(this.cbTableCommas);
            this.groupBox6.Controls.Add(this.cbReverseSort);
            this.groupBox6.Controls.Add(this.nudUpdateValue);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Location = new Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x233, 0x4e);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.cbTableCommas.AutoSize = true;
            this.cbTableCommas.Checked = true;
            this.cbTableCommas.CheckState = CheckState.Checked;
            this.cbTableCommas.Location = new Point(8, 0x33);
            this.cbTableCommas.Name = "cbTableCommas";
            this.cbTableCommas.Size = new Size(0x100, 0x11);
            this.cbTableCommas.TabIndex = 30;
            this.cbTableCommas.Text = "Show thousand separators in the Main tab tables";
            this.cbTableCommas.UseVisualStyleBackColor = true;
            this.cbTableCommas.CheckedChanged += new EventHandler(this.cbTableCommas_CheckedChanged);
            this.cbTableCommas.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbReverseSort.AutoSize = true;
            this.cbReverseSort.Checked = true;
            this.cbReverseSort.CheckState = CheckState.Checked;
            this.cbReverseSort.Location = new Point(8, 11);
            this.cbReverseSort.Name = "cbReverseSort";
            this.cbReverseSort.Size = new Size(320, 0x11);
            this.cbReverseSort.TabIndex = 0;
            this.cbReverseSort.Text = "Reverse the sorting order of the main table.  (Decending order)";
            this.cbReverseSort.MouseHover += new EventHandler(this.control_MouseHover);
            this.nudUpdateValue.Location = new Point(0xfb, 30);
            int[] numArray4 = new int[4];
            numArray4[0] = 60;
            this.nudUpdateValue.Maximum = new decimal(numArray4);
            this.nudUpdateValue.Name = "nudUpdateValue";
            this.nudUpdateValue.Size = new Size(0x30, 20);
            this.nudUpdateValue.TabIndex = 1;
            int[] numArray5 = new int[4];
            numArray5[0] = 5;
            this.nudUpdateValue.Value = new decimal(numArray5);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 0x20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xc1, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Main table update frequncy in seconds:";
            this.label2.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.groupBox6);
            base.Controls.Add(this.nudIdleLimit);
            base.Controls.Add(this.cbIdleEnd);
            base.Controls.Add(this.cbIdleTimerEnd);
            base.Name = "Options_MainTableGen";
            base.Size = new Size(0x23b, 0x7d);
            this.nudIdleLimit.EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.nudUpdateValue.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

