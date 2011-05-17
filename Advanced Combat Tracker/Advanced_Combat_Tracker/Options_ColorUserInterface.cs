namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_ColorUserInterface : UserControl
    {
        private Button btnResetColors;
        internal ColorControl ccDGAllyText;
        internal ColorControl ccDGPersonalBackcolor;
        internal ColorControl ccEncLabel1;
        internal ColorControl ccEncLabel2;
        internal ColorControl ccEncLabel3;
        private IContainer components;
        internal FontColorControl fccDataGrid;
        internal FontColorControl fccMainWindow;
        internal FontColorControl fccTreeView;
        internal FontColorControl fccWindowColors;
        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox1;

        public Options_ColorUserInterface()
        {
            this.InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You must restart ACT to revert some of these color changes.", "Restart required", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            ActGlobals.oFormActMain.xmlSettings.RemoveControlSetting(this.fccMainWindow.Name);
            ActGlobals.oFormActMain.xmlSettings.RemoveControlSetting(this.fccTreeView.Name);
            ActGlobals.oFormActMain.xmlSettings.RemoveControlSetting(this.fccWindowColors.Name);
            ActGlobals.oFormActMain.xmlSettings.RemoveControlSetting(this.fccDataGrid.Name);
            ActGlobals.oFormActMain.xmlSettings.RemoveControlSetting(this.ccEncLabel1.Name);
            ActGlobals.oFormActMain.xmlSettings.RemoveControlSetting(this.ccEncLabel2.Name);
            ActGlobals.oFormActMain.xmlSettings.RemoveControlSetting(this.ccEncLabel3.Name);
            ActGlobals.oFormActMain.xmlSettings.RemoveControlSetting(this.ccDGAllyText.Name);
            ActGlobals.oFormActMain.xmlSettings.RemoveControlSetting(this.ccDGPersonalBackcolor.Name);
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

        private void fccDataGrid_BackColorSettingChanged(Color NewColor)
        {
            ActGlobals.oFormActMain.lvDG.BackColor = NewColor;
        }

        private void fccDataGrid_FontSettingChanged(Font NewFont)
        {
            ActGlobals.oFormActMain.lvDG.Font = NewFont;
        }

        private void fccDataGrid_ForeColorSettingChanged(Color NewColor)
        {
            ActGlobals.oFormActMain.lvDG.ForeColor = NewColor;
        }

        private void fccMainWindow_BackColorSettingChanged(Color NewColor)
        {
            ActGlobals.oFormActMain.ChangeFormBackColor(ActGlobals.oFormActMain, NewColor);
        }

        private void fccMainWindow_FontSettingChanged(Font NewFont)
        {
            ActGlobals.oFormActMain.Font = NewFont;
        }

        private void fccMainWindow_ForeColorSettingChanged(Color NewColor)
        {
            ActGlobals.oFormActMain.ForeColor = NewColor;
        }

        private void fccTreeView_BackColorSettingChanged(Color NewColor)
        {
            ActGlobals.oFormActMain.tvDG.BackColor = NewColor;
        }

        private void fccTreeView_FontSettingChanged(Font NewFont)
        {
            ActGlobals.oFormActMain.tvDG.Font = NewFont;
        }

        private void fccTreeView_ForeColorSettingChanged(Color NewColor)
        {
            ActGlobals.oFormActMain.tvDG.ForeColor = NewColor;
        }

        private void fccWindowColors_BackColorSettingChanged(Color NewColor)
        {
            ActGlobals.oFormActMain.ChangeWindowBackColor(ActGlobals.oFormActMain, NewColor);
        }

        private void fccWindowColors_ForeColorSettingChanged(Color NewColor)
        {
            ActGlobals.oFormActMain.ChangeWindowForeColor(ActGlobals.oFormActMain, NewColor);
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new FlowLayoutPanel();
            this.fccMainWindow = new FontColorControl();
            this.fccWindowColors = new FontColorControl();
            this.fccTreeView = new FontColorControl();
            this.ccEncLabel1 = new ColorControl();
            this.ccEncLabel2 = new ColorControl();
            this.ccEncLabel3 = new ColorControl();
            this.fccDataGrid = new FontColorControl();
            this.ccDGAllyText = new ColorControl();
            this.ccDGPersonalBackcolor = new ColorControl();
            this.groupBox1 = new GroupBox();
            this.btnResetColors = new Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.fccMainWindow);
            this.flowLayoutPanel1.Controls.Add(this.fccWindowColors);
            this.flowLayoutPanel1.Controls.Add(this.fccTreeView);
            this.flowLayoutPanel1.Controls.Add(this.ccEncLabel1);
            this.flowLayoutPanel1.Controls.Add(this.ccEncLabel2);
            this.flowLayoutPanel1.Controls.Add(this.ccEncLabel3);
            this.flowLayoutPanel1.Controls.Add(this.fccDataGrid);
            this.flowLayoutPanel1.Controls.Add(this.ccDGAllyText);
            this.flowLayoutPanel1.Controls.Add(this.ccDGPersonalBackcolor);
            this.flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new Point(6, 0x13);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new Size(0x170, 0x194);
            this.flowLayoutPanel1.TabIndex = 1;
            this.fccMainWindow.AutoSize = true;
            this.fccMainWindow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.fccMainWindow.BackColorSetting = SystemColors.Control;
            this.fccMainWindow.FontChangable = true;
            this.fccMainWindow.FontSetting = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.fccMainWindow.ForeColorSetting = SystemColors.ControlText;
            this.fccMainWindow.Location = new Point(3, 3);
            this.fccMainWindow.Name = "fccMainWindow";
            this.fccMainWindow.Size = new Size(350, 50);
            this.fccMainWindow.TabIndex = 0;
            this.fccMainWindow.Text = "Main Window Colors";
            this.fccMainWindow.FontSettingChanged += new FontColorControl.FontSettingEventDelegate(this.fccMainWindow_FontSettingChanged);
            this.fccMainWindow.ForeColorSettingChanged += new FontColorControl.ColorSettingEventDelegate(this.fccMainWindow_ForeColorSettingChanged);
            this.fccMainWindow.BackColorSettingChanged += new FontColorControl.ColorSettingEventDelegate(this.fccMainWindow_BackColorSettingChanged);
            this.fccMainWindow.MouseHover += new EventHandler(this.control_MouseHover);
            this.fccWindowColors.AutoSize = true;
            this.fccWindowColors.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.fccWindowColors.BackColorSetting = SystemColors.Window;
            this.fccWindowColors.FontChangable = false;
            this.fccWindowColors.FontSetting = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.fccWindowColors.ForeColorSetting = SystemColors.WindowText;
            this.fccWindowColors.Location = new Point(3, 0x3b);
            this.fccWindowColors.Name = "fccWindowColors";
            this.fccWindowColors.Size = new Size(0x16a, 50);
            this.fccWindowColors.TabIndex = 0;
            this.fccWindowColors.Text = "Internal Window Colors";
            this.fccWindowColors.ForeColorSettingChanged += new FontColorControl.ColorSettingEventDelegate(this.fccWindowColors_ForeColorSettingChanged);
            this.fccWindowColors.BackColorSettingChanged += new FontColorControl.ColorSettingEventDelegate(this.fccWindowColors_BackColorSettingChanged);
            this.fccWindowColors.MouseHover += new EventHandler(this.control_MouseHover);
            this.fccTreeView.AutoSize = true;
            this.fccTreeView.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.fccTreeView.BackColorSetting = SystemColors.Window;
            this.fccTreeView.FontChangable = true;
            this.fccTreeView.FontSetting = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.fccTreeView.ForeColorSetting = SystemColors.WindowText;
            this.fccTreeView.Location = new Point(3, 0x73);
            this.fccTreeView.Name = "fccTreeView";
            this.fccTreeView.Size = new Size(330, 50);
            this.fccTreeView.TabIndex = 0;
            this.fccTreeView.Text = "TreeView Colors";
            this.fccTreeView.FontSettingChanged += new FontColorControl.FontSettingEventDelegate(this.fccTreeView_FontSettingChanged);
            this.fccTreeView.ForeColorSettingChanged += new FontColorControl.ColorSettingEventDelegate(this.fccTreeView_ForeColorSettingChanged);
            this.fccTreeView.BackColorSettingChanged += new FontColorControl.ColorSettingEventDelegate(this.fccTreeView_BackColorSettingChanged);
            this.fccTreeView.MouseHover += new EventHandler(this.control_MouseHover);
            this.ccEncLabel1.AutoSize = true;
            this.ccEncLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ccEncLabel1.ForeColorSetting = Color.ForestGreen;
            this.ccEncLabel1.Location = new Point(110, 0xab);
            this.ccEncLabel1.Margin = new Padding(110, 3, 3, 3);
            this.ccEncLabel1.Name = "ccEncLabel1";
            this.ccEncLabel1.Size = new Size(0x80, 30);
            this.ccEncLabel1.TabIndex = 1;
            this.ccEncLabel1.Text = "Encounter Color 1";
            this.ccEncLabel1.MouseHover += new EventHandler(this.control_MouseHover);
            this.ccEncLabel2.AutoSize = true;
            this.ccEncLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ccEncLabel2.ForeColorSetting = Color.DarkOrange;
            this.ccEncLabel2.Location = new Point(110, 0xcf);
            this.ccEncLabel2.Margin = new Padding(110, 3, 3, 3);
            this.ccEncLabel2.Name = "ccEncLabel2";
            this.ccEncLabel2.Size = new Size(0x80, 30);
            this.ccEncLabel2.TabIndex = 1;
            this.ccEncLabel2.Text = "Encounter Color 2";
            this.ccEncLabel2.MouseHover += new EventHandler(this.control_MouseHover);
            this.ccEncLabel3.AutoSize = true;
            this.ccEncLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ccEncLabel3.ForeColorSetting = Color.Crimson;
            this.ccEncLabel3.Location = new Point(110, 0xf3);
            this.ccEncLabel3.Margin = new Padding(110, 3, 3, 3);
            this.ccEncLabel3.Name = "ccEncLabel3";
            this.ccEncLabel3.Size = new Size(0x80, 30);
            this.ccEncLabel3.TabIndex = 1;
            this.ccEncLabel3.Text = "Encounter Color 3";
            this.ccEncLabel3.MouseHover += new EventHandler(this.control_MouseHover);
            this.fccDataGrid.AutoSize = true;
            this.fccDataGrid.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.fccDataGrid.BackColorSetting = SystemColors.Window;
            this.fccDataGrid.FontChangable = true;
            this.fccDataGrid.FontSetting = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.fccDataGrid.ForeColorSetting = SystemColors.WindowText;
            this.fccDataGrid.Location = new Point(3, 0x117);
            this.fccDataGrid.Name = "fccDataGrid";
            this.fccDataGrid.Size = new Size(330, 50);
            this.fccDataGrid.TabIndex = 0;
            this.fccDataGrid.Text = "Data Grid Colors";
            this.fccDataGrid.FontSettingChanged += new FontColorControl.FontSettingEventDelegate(this.fccDataGrid_FontSettingChanged);
            this.fccDataGrid.ForeColorSettingChanged += new FontColorControl.ColorSettingEventDelegate(this.fccDataGrid_ForeColorSettingChanged);
            this.fccDataGrid.BackColorSettingChanged += new FontColorControl.ColorSettingEventDelegate(this.fccDataGrid_BackColorSettingChanged);
            this.fccDataGrid.MouseHover += new EventHandler(this.control_MouseHover);
            this.ccDGAllyText.AutoSize = true;
            this.ccDGAllyText.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ccDGAllyText.ForeColorSetting = Color.ForestGreen;
            this.ccDGAllyText.Location = new Point(110, 0x14f);
            this.ccDGAllyText.Margin = new Padding(110, 3, 3, 3);
            this.ccDGAllyText.Name = "ccDGAllyText";
            this.ccDGAllyText.Size = new Size(110, 30);
            this.ccDGAllyText.TabIndex = 1;
            this.ccDGAllyText.Text = "Ally Text Color";
            this.ccDGAllyText.MouseHover += new EventHandler(this.control_MouseHover);
            this.ccDGPersonalBackcolor.AutoSize = true;
            this.ccDGPersonalBackcolor.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ccDGPersonalBackcolor.ForeColorSetting = Color.FromArgb(200, 0xff, 200);
            this.ccDGPersonalBackcolor.Location = new Point(110, 0x173);
            this.ccDGPersonalBackcolor.Margin = new Padding(110, 3, 3, 3);
            this.ccDGPersonalBackcolor.Name = "ccDGPersonalBackcolor";
            this.ccDGPersonalBackcolor.Size = new Size(0x9b, 30);
            this.ccDGPersonalBackcolor.TabIndex = 1;
            this.ccDGPersonalBackcolor.Text = "Personal Cell Backcolor";
            this.ccDGPersonalBackcolor.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.btnResetColors);
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(380, 0x1ba);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Main User Interface";
            this.btnResetColors.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnResetColors.Location = new Point(0x131, 0);
            this.btnResetColors.Name = "btnResetColors";
            this.btnResetColors.Size = new Size(0x45, 20);
            this.btnResetColors.TabIndex = 2;
            this.btnResetColors.Text = "Reset";
            this.btnResetColors.UseVisualStyleBackColor = true;
            this.btnResetColors.Click += new EventHandler(this.btnReset_Click);
            this.btnResetColors.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.groupBox1);
            base.Name = "Options_ColorUserInterface";
            base.Size = new Size(0x182, 0x1c0);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

