namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_ColorMisc : UserControl
    {
        private Button btnResetColors;
        internal ColorControl ccSpellTimerBackColor;
        internal ColorControl ccSpellTimerExpireColor;
        internal ColorControl ccSpellTimerForeColor;
        internal ColorControl ccSpellTimerWarnColor;
        private IContainer components;
        private GroupBox groupBox1;

        public Options_ColorMisc()
        {
            this.InitializeComponent();
        }

        private void btnResetColors_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You must restart ACT to revert some of these color changes.", "Restart required", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            ActGlobals.oFormActMain.xmlSettings.RemoveControlSetting(this.ccSpellTimerBackColor.Name);
            ActGlobals.oFormActMain.xmlSettings.RemoveControlSetting(this.ccSpellTimerExpireColor.Name);
            ActGlobals.oFormActMain.xmlSettings.RemoveControlSetting(this.ccSpellTimerForeColor.Name);
            ActGlobals.oFormActMain.xmlSettings.RemoveControlSetting(this.ccSpellTimerWarnColor.Name);
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
            this.groupBox1 = new GroupBox();
            this.ccSpellTimerForeColor = new ColorControl();
            this.ccSpellTimerExpireColor = new ColorControl();
            this.ccSpellTimerWarnColor = new ColorControl();
            this.ccSpellTimerBackColor = new ColorControl();
            this.btnResetColors = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnResetColors);
            this.groupBox1.Controls.Add(this.ccSpellTimerForeColor);
            this.groupBox1.Controls.Add(this.ccSpellTimerExpireColor);
            this.groupBox1.Controls.Add(this.ccSpellTimerWarnColor);
            this.groupBox1.Controls.Add(this.ccSpellTimerBackColor);
            this.groupBox1.Location = new Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x197, 0x5f);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spell Timer Window";
            this.ccSpellTimerForeColor.AutoSize = true;
            this.ccSpellTimerForeColor.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ccSpellTimerForeColor.ForeColorSetting = SystemColors.ControlText;
            this.ccSpellTimerForeColor.Location = new Point(6, 0x37);
            this.ccSpellTimerForeColor.Name = "ccSpellTimerForeColor";
            this.ccSpellTimerForeColor.Size = new Size(0xbc, 30);
            this.ccSpellTimerForeColor.TabIndex = 0;
            this.ccSpellTimerForeColor.Text = "Spell Timer Window Fore Color";
            this.ccSpellTimerExpireColor.AutoSize = true;
            this.ccSpellTimerExpireColor.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ccSpellTimerExpireColor.ForeColorSetting = Color.DarkOrange;
            this.ccSpellTimerExpireColor.Location = new Point(0xcc, 0x37);
            this.ccSpellTimerExpireColor.Name = "ccSpellTimerExpireColor";
            this.ccSpellTimerExpireColor.Size = new Size(0xa5, 30);
            this.ccSpellTimerExpireColor.TabIndex = 0;
            this.ccSpellTimerExpireColor.Text = "Spell Timer Warning Color";
            this.ccSpellTimerWarnColor.AutoSize = true;
            this.ccSpellTimerWarnColor.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ccSpellTimerWarnColor.ForeColorSetting = Color.Firebrick;
            this.ccSpellTimerWarnColor.Location = new Point(0xcc, 0x13);
            this.ccSpellTimerWarnColor.Name = "ccSpellTimerWarnColor";
            this.ccSpellTimerWarnColor.Size = new Size(0xa5, 30);
            this.ccSpellTimerWarnColor.TabIndex = 0;
            this.ccSpellTimerWarnColor.Text = "Spell Timer Warning Color";
            this.ccSpellTimerBackColor.AutoSize = true;
            this.ccSpellTimerBackColor.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ccSpellTimerBackColor.ForeColorSetting = SystemColors.Control;
            this.ccSpellTimerBackColor.Location = new Point(6, 0x13);
            this.ccSpellTimerBackColor.Name = "ccSpellTimerBackColor";
            this.ccSpellTimerBackColor.Size = new Size(0xc0, 30);
            this.ccSpellTimerBackColor.TabIndex = 0;
            this.ccSpellTimerBackColor.Text = "Spell Timer Window Back Color";
            this.btnResetColors.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnResetColors.Location = new Point(0x14c, 0);
            this.btnResetColors.Name = "btnResetColors";
            this.btnResetColors.Size = new Size(0x45, 20);
            this.btnResetColors.TabIndex = 3;
            this.btnResetColors.Text = "Reset";
            this.btnResetColors.UseVisualStyleBackColor = true;
            this.btnResetColors.Click += new EventHandler(this.btnResetColors_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.groupBox1);
            base.Name = "Options_ColorMisc";
            base.Size = new Size(0x19d, 0x65);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }
    }
}

