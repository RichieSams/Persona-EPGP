namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_DataCorrectionMisc : UserControl
    {
        private Button btnCharNameApply;
        internal CheckBox cbBlockisHit;
        internal CheckBox cbCalcRealAvgDly;
        internal CheckBox cbLongEncDuration;
        private IContainer components;
        private Label lblCharName;
        internal TextBox tbCharName;

        public Options_DataCorrectionMisc()
        {
            this.InitializeComponent();
        }

        private void btnCharNameApply_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tbCharName.Text))
            {
                ActGlobals.oFormActMain.SetCharName(false);
            }
            else
            {
                ActGlobals.oFormActMain.SetCharName(true);
            }
        }

        private void cbBlockisHit_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.blockIsHit = this.cbBlockisHit.Checked;
        }

        private void cbCalcRealAvgDly_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.calcRealAvgDelay = this.cbCalcRealAvgDly.Checked;
        }

        private void cbLongEncDuration_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.longDuration = this.cbLongEncDuration.Checked;
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
            this.cbCalcRealAvgDly = new CheckBox();
            this.btnCharNameApply = new Button();
            this.lblCharName = new Label();
            this.tbCharName = new TextBox();
            this.cbBlockisHit = new CheckBox();
            this.cbLongEncDuration = new CheckBox();
            base.SuspendLayout();
            this.cbCalcRealAvgDly.AutoSize = true;
            this.cbCalcRealAvgDly.Checked = true;
            this.cbCalcRealAvgDly.CheckState = CheckState.Checked;
            this.cbCalcRealAvgDly.Location = new Point(12, 0x41);
            this.cbCalcRealAvgDly.Margin = new Padding(3, 1, 3, 1);
            this.cbCalcRealAvgDly.Name = "cbCalcRealAvgDly";
            this.cbCalcRealAvgDly.Size = new Size(0x1e3, 0x11);
            this.cbCalcRealAvgDly.TabIndex = 40;
            this.cbCalcRealAvgDly.Text = "When calculating average delay, group together multiple hits in a single second.  (More accurate)";
            this.cbCalcRealAvgDly.CheckedChanged += new EventHandler(this.cbCalcRealAvgDly_CheckedChanged);
            this.cbCalcRealAvgDly.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnCharNameApply.Location = new Point(0x1bc, 3);
            this.btnCharNameApply.Name = "btnCharNameApply";
            this.btnCharNameApply.Size = new Size(70, 20);
            this.btnCharNameApply.TabIndex = 0x26;
            this.btnCharNameApply.Text = "Apply";
            this.btnCharNameApply.UseVisualStyleBackColor = true;
            this.btnCharNameApply.Click += new EventHandler(this.btnCharNameApply_Click);
            this.btnCharNameApply.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblCharName.AutoSize = true;
            this.lblCharName.Location = new Point(9, 7);
            this.lblCharName.Margin = new Padding(3, 1, 3, 1);
            this.lblCharName.Name = "lblCharName";
            this.lblCharName.Size = new Size(0x117, 13);
            this.lblCharName.TabIndex = 0x27;
            this.lblCharName.Text = "Default character name if not defined by the log file name.";
            this.lblCharName.TextAlign = ContentAlignment.MiddleLeft;
            this.lblCharName.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbCharName.Location = new Point(0x13b, 3);
            this.tbCharName.Name = "tbCharName";
            this.tbCharName.Size = new Size(0x7b, 20);
            this.tbCharName.TabIndex = 0x25;
            this.tbCharName.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbBlockisHit.AutoSize = true;
            this.cbBlockisHit.Checked = true;
            this.cbBlockisHit.CheckState = CheckState.Checked;
            this.cbBlockisHit.Location = new Point(12, 0x2e);
            this.cbBlockisHit.Margin = new Padding(3, 1, 3, 1);
            this.cbBlockisHit.Name = "cbBlockisHit";
            this.cbBlockisHit.Size = new Size(0x12a, 0x11);
            this.cbBlockisHit.TabIndex = 0x24;
            this.cbBlockisHit.Text = "Count a hit that inflicts no damage as a Hit in calculations.";
            this.cbBlockisHit.CheckedChanged += new EventHandler(this.cbBlockisHit_CheckedChanged);
            this.cbBlockisHit.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbLongEncDuration.AutoSize = true;
            this.cbLongEncDuration.Location = new Point(12, 0x1b);
            this.cbLongEncDuration.Margin = new Padding(3, 1, 3, 1);
            this.cbLongEncDuration.Name = "cbLongEncDuration";
            this.cbLongEncDuration.Size = new Size(0x178, 0x11);
            this.cbLongEncDuration.TabIndex = 0x23;
            this.cbLongEncDuration.Text = "Allow heals to extend an encounter's duration past the last damage action.";
            this.cbLongEncDuration.CheckedChanged += new EventHandler(this.cbLongEncDuration_CheckedChanged);
            this.cbLongEncDuration.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.cbCalcRealAvgDly);
            base.Controls.Add(this.btnCharNameApply);
            base.Controls.Add(this.lblCharName);
            base.Controls.Add(this.tbCharName);
            base.Controls.Add(this.cbBlockisHit);
            base.Controls.Add(this.cbLongEncDuration);
            base.Name = "Options_DataCorrectionMisc";
            base.Size = new Size(0x205, 0x53);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

