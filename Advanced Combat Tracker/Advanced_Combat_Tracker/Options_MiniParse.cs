namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_MiniParse : UserControl
    {
        internal Button btnAddPresetMini;
        internal Button btnRemovePresetMini;
        internal CheckBox cbMinEncBox;
        internal CheckBox cbMiniClickThrough;
        internal CheckBox cbMiniColumnAlign;
        internal CheckBox cbRestoreEncBox;
        internal CheckBox cbSmallEncTop;
        private IContainer components;
        internal ComboBox ddlMiniFormat;
        internal FontColorControl fccMiniParse;
        private Label label22;
        private Label label23;
        private Label label44;
        private Label label89;
        internal NumericUpDown nudMiniUpdateInterval;
        internal TextBox tbMiniOpacity;
        internal TrackBar trbMiniOpacity;

        public Options_MiniParse()
        {
            this.InitializeComponent();
        }

        private void btnAddPresetMini_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.btnShowTextFormatWindow_Click(sender, e);
        }

        private void btnMiniFormatRemove_Click(object sender, EventArgs e)
        {
            if (this.ddlMiniFormat.SelectedIndex > -1)
            {
                ActGlobals.oFormActMain.RemoveTextFormat(this.ddlMiniFormat.SelectedIndex);
            }
        }

        private void btnRemovePresetMini_Click(object sender, EventArgs e)
        {
            if (this.ddlMiniFormat.SelectedIndex > -1)
            {
                ActGlobals.oFormActMain.RemoveTextFormat(this.ddlMiniFormat.SelectedIndex);
            }
        }

        private void cbMiniClickThrough_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormMiniParse.SetClickThrough(this.cbMiniClickThrough.Checked);
        }

        private void cbSmallEncTop_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormMiniParse.TopMost = this.cbSmallEncTop.Checked;
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

        private void fccMiniParse_BackColorSettingChanged(Color NewColor)
        {
            ActGlobals.oFormMiniParse.BackColor = NewColor;
            ActGlobals.oFormMiniParse.rtb2.BackColor = NewColor;
        }

        private void fccMiniParse_FontSettingChanged(Font NewFont)
        {
            ActGlobals.oFormMiniParse.rtb2.Font = NewFont;
        }

        private void fccMiniParse_ForeColorSettingChanged(Color NewColor)
        {
            ActGlobals.oFormMiniParse.ForeColor = NewColor;
            ActGlobals.oFormMiniParse.rtb2.ForeColor = NewColor;
        }

        private void InitializeComponent()
        {
            this.btnAddPresetMini = new Button();
            this.tbMiniOpacity = new TextBox();
            this.label44 = new Label();
            this.label89 = new Label();
            this.trbMiniOpacity = new TrackBar();
            this.label23 = new Label();
            this.label22 = new Label();
            this.nudMiniUpdateInterval = new NumericUpDown();
            this.cbRestoreEncBox = new CheckBox();
            this.cbSmallEncTop = new CheckBox();
            this.cbMiniColumnAlign = new CheckBox();
            this.ddlMiniFormat = new ComboBox();
            this.cbMiniClickThrough = new CheckBox();
            this.cbMinEncBox = new CheckBox();
            this.btnRemovePresetMini = new Button();
            this.fccMiniParse = new FontColorControl();
            this.trbMiniOpacity.BeginInit();
            this.nudMiniUpdateInterval.BeginInit();
            base.SuspendLayout();
            this.btnAddPresetMini.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnAddPresetMini.Location = new Point(0x1cd, 0xb6);
            this.btnAddPresetMini.Name = "btnAddPresetMini";
            this.btnAddPresetMini.Size = new Size(0x4a, 0x15);
            this.btnAddPresetMini.TabIndex = 0x76;
            this.btnAddPresetMini.Text = "Add Preset";
            this.btnAddPresetMini.UseVisualStyleBackColor = true;
            this.btnAddPresetMini.Click += new EventHandler(this.btnAddPresetMini_Click);
            this.btnAddPresetMini.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbMiniOpacity.Enabled = false;
            this.tbMiniOpacity.Location = new Point(0x2a, 0x67);
            this.tbMiniOpacity.Name = "tbMiniOpacity";
            this.tbMiniOpacity.Size = new Size(0x20, 20);
            this.tbMiniOpacity.TabIndex = 0x7b;
            this.tbMiniOpacity.Text = "100";
            this.tbMiniOpacity.TextAlign = HorizontalAlignment.Center;
            this.tbMiniOpacity.TextChanged += new EventHandler(this.tbMiniOpacity_TextChanged);
            this.tbMiniOpacity.MouseHover += new EventHandler(this.control_MouseHover);
            this.label44.Location = new Point(4, 0x57);
            this.label44.Name = "label44";
            this.label44.Size = new Size(110, 0x10);
            this.label44.TabIndex = 0x7a;
            this.label44.Text = "Window Opacity";
            this.label44.TextAlign = ContentAlignment.TopCenter;
            this.label44.MouseHover += new EventHandler(this.control_MouseHover);
            this.label89.AutoSize = true;
            this.label89.Location = new Point(0x15, 0xba);
            this.label89.Name = "label89";
            this.label89.Size = new Size(0x87, 13);
            this.label89.TabIndex = 0x80;
            this.label89.Text = "Mini-Parse Text Formatting:";
            this.label89.MouseHover += new EventHandler(this.control_MouseHover);
            this.trbMiniOpacity.LargeChange = 10;
            this.trbMiniOpacity.Location = new Point(120, 0x57);
            this.trbMiniOpacity.Maximum = 100;
            this.trbMiniOpacity.Minimum = 10;
            this.trbMiniOpacity.Name = "trbMiniOpacity";
            this.trbMiniOpacity.Size = new Size(0x88, 0x2d);
            this.trbMiniOpacity.SmallChange = 5;
            this.trbMiniOpacity.TabIndex = 0x74;
            this.trbMiniOpacity.TickFrequency = 5;
            this.trbMiniOpacity.Value = 100;
            this.trbMiniOpacity.Scroll += new EventHandler(this.trbMiniOpacity_Scroll);
            this.trbMiniOpacity.MouseHover += new EventHandler(this.control_MouseHover);
            this.label23.AutoSize = true;
            this.label23.Location = new Point(0xd0, 0x3f);
            this.label23.Name = "label23";
            this.label23.Size = new Size(50, 13);
            this.label23.TabIndex = 0x79;
            this.label23.Text = "seconds.";
            this.label23.MouseHover += new EventHandler(this.control_MouseHover);
            this.label22.AutoSize = true;
            this.label22.Location = new Point(20, 0x40);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x52, 13);
            this.label22.TabIndex = 0x72;
            this.label22.Text = "Update interval:";
            this.label22.MouseHover += new EventHandler(this.control_MouseHover);
            this.nudMiniUpdateInterval.Location = new Point(0x9a, 0x3d);
            int[] bits = new int[4];
            bits[0] = 60;
            this.nudMiniUpdateInterval.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.nudMiniUpdateInterval.Minimum = new decimal(numArray2);
            this.nudMiniUpdateInterval.Name = "nudMiniUpdateInterval";
            this.nudMiniUpdateInterval.Size = new Size(0x30, 20);
            this.nudMiniUpdateInterval.TabIndex = 0x73;
            int[] numArray3 = new int[4];
            numArray3[0] = 5;
            this.nudMiniUpdateInterval.Value = new decimal(numArray3);
            this.cbRestoreEncBox.AutoSize = true;
            this.cbRestoreEncBox.Location = new Point(0x17, 0x16);
            this.cbRestoreEncBox.Name = "cbRestoreEncBox";
            this.cbRestoreEncBox.Size = new Size(0xcc, 0x11);
            this.cbRestoreEncBox.TabIndex = 110;
            this.cbRestoreEncBox.Text = "Hide Mini Parse on application restore";
            this.cbRestoreEncBox.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbSmallEncTop.AutoSize = true;
            this.cbSmallEncTop.Checked = true;
            this.cbSmallEncTop.CheckState = CheckState.Checked;
            this.cbSmallEncTop.Location = new Point(0x17, 0x29);
            this.cbSmallEncTop.Name = "cbSmallEncTop";
            this.cbSmallEncTop.Size = new Size(0x9c, 0x11);
            this.cbSmallEncTop.TabIndex = 0x71;
            this.cbSmallEncTop.Text = "Mini Parse - Always On Top";
            this.cbSmallEncTop.CheckedChanged += new EventHandler(this.cbSmallEncTop_CheckedChanged);
            this.cbSmallEncTop.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbMiniColumnAlign.AutoSize = true;
            this.cbMiniColumnAlign.Checked = true;
            this.cbMiniColumnAlign.CheckState = CheckState.Checked;
            this.cbMiniColumnAlign.Location = new Point(0x18, 0x99);
            this.cbMiniColumnAlign.Name = "cbMiniColumnAlign";
            this.cbMiniColumnAlign.Size = new Size(280, 0x11);
            this.cbMiniColumnAlign.TabIndex = 0x77;
            this.cbMiniColumnAlign.Text = "Align text to appear in columns based on cell contents";
            this.cbMiniColumnAlign.MouseHover += new EventHandler(this.control_MouseHover);
            this.ddlMiniFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlMiniFormat.ItemHeight = 13;
            this.ddlMiniFormat.Location = new Point(0xb1, 0xb6);
            this.ddlMiniFormat.Name = "ddlMiniFormat";
            this.ddlMiniFormat.Size = new Size(0x116, 0x15);
            this.ddlMiniFormat.TabIndex = 0x70;
            this.ddlMiniFormat.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbMiniClickThrough.AutoSize = true;
            this.cbMiniClickThrough.Location = new Point(0x18, 0x86);
            this.cbMiniClickThrough.Name = "cbMiniClickThrough";
            this.cbMiniClickThrough.Size = new Size(0x146, 0x11);
            this.cbMiniClickThrough.TabIndex = 120;
            this.cbMiniClickThrough.Text = "Allow mouse clicks to pass through.   (Win2k or above required)";
            this.cbMiniClickThrough.CheckedChanged += new EventHandler(this.cbMiniClickThrough_CheckedChanged);
            this.cbMiniClickThrough.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbMinEncBox.AutoSize = true;
            this.cbMinEncBox.Location = new Point(0x17, 3);
            this.cbMinEncBox.Name = "cbMinEncBox";
            this.cbMinEncBox.Size = new Size(0xdf, 0x11);
            this.cbMinEncBox.TabIndex = 0x6d;
            this.cbMinEncBox.Text = "Minimize application to Mini Parse window";
            this.cbMinEncBox.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnRemovePresetMini.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnRemovePresetMini.Location = new Point(0x21d, 0xb6);
            this.btnRemovePresetMini.Name = "btnRemovePresetMini";
            this.btnRemovePresetMini.Size = new Size(0x44, 0x15);
            this.btnRemovePresetMini.TabIndex = 0x6f;
            this.btnRemovePresetMini.Text = "Remove";
            this.btnRemovePresetMini.UseVisualStyleBackColor = true;
            this.btnRemovePresetMini.Click += new EventHandler(this.btnRemovePresetMini_Click);
            this.btnRemovePresetMini.MouseHover += new EventHandler(this.control_MouseHover);
            this.fccMiniParse.AutoSize = true;
            this.fccMiniParse.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.fccMiniParse.BackColorSetting = Color.Black;
            this.fccMiniParse.FontChangable = true;
            this.fccMiniParse.FontSetting = new Font("Lucida Console", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.fccMiniParse.ForeColorSetting = Color.Yellow;
            this.fccMiniParse.Location = new Point(0x106, 0x52);
            this.fccMiniParse.Name = "fccMiniParse";
            this.fccMiniParse.Size = new Size(0x100, 50);
            this.fccMiniParse.TabIndex = 0x81;
            this.fccMiniParse.Text = " ";
            this.fccMiniParse.FontSettingChanged += new FontColorControl.FontSettingEventDelegate(this.fccMiniParse_FontSettingChanged);
            this.fccMiniParse.ForeColorSettingChanged += new FontColorControl.ColorSettingEventDelegate(this.fccMiniParse_ForeColorSettingChanged);
            this.fccMiniParse.BackColorSettingChanged += new FontColorControl.ColorSettingEventDelegate(this.fccMiniParse_BackColorSettingChanged);
            this.fccMiniParse.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.fccMiniParse);
            base.Controls.Add(this.btnAddPresetMini);
            base.Controls.Add(this.tbMiniOpacity);
            base.Controls.Add(this.label44);
            base.Controls.Add(this.label89);
            base.Controls.Add(this.trbMiniOpacity);
            base.Controls.Add(this.label23);
            base.Controls.Add(this.label22);
            base.Controls.Add(this.nudMiniUpdateInterval);
            base.Controls.Add(this.cbRestoreEncBox);
            base.Controls.Add(this.cbSmallEncTop);
            base.Controls.Add(this.cbMiniColumnAlign);
            base.Controls.Add(this.ddlMiniFormat);
            base.Controls.Add(this.cbMiniClickThrough);
            base.Controls.Add(this.cbMinEncBox);
            base.Controls.Add(this.btnRemovePresetMini);
            base.Name = "Options_MiniParse";
            base.Size = new Size(0x264, 0xe2);
            base.MouseHover += new EventHandler(this.control_MouseHover);
            this.trbMiniOpacity.EndInit();
            this.nudMiniUpdateInterval.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void tbMiniOpacity_TextChanged(object sender, EventArgs e)
        {
            this.trbMiniOpacity.Value = int.Parse(this.tbMiniOpacity.Text);
            ActGlobals.oFormMiniParse.Opacity = double.Parse(this.tbMiniOpacity.Text) / 100.0;
        }

        private void trbMiniOpacity_Scroll(object sender, EventArgs e)
        {
            this.tbMiniOpacity.Text = this.trbMiniOpacity.Value.ToString();
        }
    }
}

