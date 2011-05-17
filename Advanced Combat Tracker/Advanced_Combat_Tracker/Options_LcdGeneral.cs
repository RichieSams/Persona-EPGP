namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_LcdGeneral : UserControl
    {
        internal Button btnLcdAddMiniPreset;
        internal Button btnLcdAddPersonalPreset;
        private Button btnLcdRemoveMiniPreset;
        private Button btnLcdRemovePersonalPreset;
        internal CheckBox cbLcdEnabled;
        internal CheckBox cbLcdRoute;
        private IContainer components;
        internal ComboBox ddlLcdMiniFormat;
        internal ComboBox ddlLcdPersonalFormat;
        private Label lblLcdMiniFormat;
        internal Label lblLcdQuery;
        private Label lblPersonalParseFormat;
        internal RichTextBox rtbLcdStatus;

        public Options_LcdGeneral()
        {
            this.InitializeComponent();
        }

        private void btnLcdRemovePersonalPreset_Click(object sender, EventArgs e)
        {
            if (this.ddlLcdPersonalFormat.SelectedIndex > -1)
            {
                ActGlobals.oFormActMain.RemoveTextFormat(this.ddlLcdPersonalFormat.SelectedIndex);
            }
        }

        private void btnRemovePreset_Click(object sender, EventArgs e)
        {
            if (this.ddlLcdMiniFormat.SelectedIndex > -1)
            {
                ActGlobals.oFormActMain.RemoveTextFormat(this.ddlLcdMiniFormat.SelectedIndex);
            }
        }

        private void btnShowTextFormatWindow_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.btnShowTextFormatWindow_Click(sender, e);
        }

        private void cbLCDEnabled_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.cbLCDEnabled_CheckedChanged();
        }

        private void cbLcdRoute_CheckedChanged(object sender, EventArgs e)
        {
            this.cbLcdEnabled.Checked = false;
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
            this.btnLcdAddPersonalPreset = new Button();
            this.btnLcdAddMiniPreset = new Button();
            this.lblPersonalParseFormat = new Label();
            this.ddlLcdPersonalFormat = new ComboBox();
            this.lblLcdMiniFormat = new Label();
            this.btnLcdRemovePersonalPreset = new Button();
            this.ddlLcdMiniFormat = new ComboBox();
            this.btnLcdRemoveMiniPreset = new Button();
            this.cbLcdEnabled = new CheckBox();
            this.lblLcdQuery = new Label();
            this.cbLcdRoute = new CheckBox();
            this.rtbLcdStatus = new RichTextBox();
            base.SuspendLayout();
            this.btnLcdAddPersonalPreset.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnLcdAddPersonalPreset.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnLcdAddPersonalPreset.Location = new Point(510, 0x61);
            this.btnLcdAddPersonalPreset.Name = "btnLcdAddPersonalPreset";
            this.btnLcdAddPersonalPreset.Size = new Size(0x4a, 0x15);
            this.btnLcdAddPersonalPreset.TabIndex = 0x92;
            this.btnLcdAddPersonalPreset.Text = "Add Preset";
            this.btnLcdAddPersonalPreset.UseVisualStyleBackColor = true;
            this.btnLcdAddPersonalPreset.Click += new EventHandler(this.btnShowTextFormatWindow_Click);
            this.btnLcdAddPersonalPreset.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnLcdAddMiniPreset.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnLcdAddMiniPreset.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnLcdAddMiniPreset.Location = new Point(510, 0x49);
            this.btnLcdAddMiniPreset.Name = "btnLcdAddMiniPreset";
            this.btnLcdAddMiniPreset.Size = new Size(0x4a, 0x15);
            this.btnLcdAddMiniPreset.TabIndex = 0x91;
            this.btnLcdAddMiniPreset.Text = "Add Preset";
            this.btnLcdAddMiniPreset.UseVisualStyleBackColor = true;
            this.btnLcdAddMiniPreset.Click += new EventHandler(this.btnShowTextFormatWindow_Click);
            this.btnLcdAddMiniPreset.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblPersonalParseFormat.AutoSize = true;
            this.lblPersonalParseFormat.Location = new Point(3, 0x65);
            this.lblPersonalParseFormat.Name = "lblPersonalParseFormat";
            this.lblPersonalParseFormat.Size = new Size(130, 13);
            this.lblPersonalParseFormat.TabIndex = 0x94;
            this.lblPersonalParseFormat.Text = "Personal Parse Formatting";
            this.lblPersonalParseFormat.MouseHover += new EventHandler(this.control_MouseHover);
            this.ddlLcdPersonalFormat.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.ddlLcdPersonalFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlLcdPersonalFormat.ItemHeight = 13;
            this.ddlLcdPersonalFormat.Location = new Point(0xb2, 0x61);
            this.ddlLcdPersonalFormat.Name = "ddlLcdPersonalFormat";
            this.ddlLcdPersonalFormat.Size = new Size(0x146, 0x15);
            this.ddlLcdPersonalFormat.TabIndex = 0x8f;
            this.ddlLcdPersonalFormat.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblLcdMiniFormat.AutoSize = true;
            this.lblLcdMiniFormat.Location = new Point(3, 0x4d);
            this.lblLcdMiniFormat.Name = "lblLcdMiniFormat";
            this.lblLcdMiniFormat.Size = new Size(0x86, 13);
            this.lblLcdMiniFormat.TabIndex = 0x93;
            this.lblLcdMiniFormat.Text = "LCD-Parse Text Formatting";
            this.lblLcdMiniFormat.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnLcdRemovePersonalPreset.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnLcdRemovePersonalPreset.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnLcdRemovePersonalPreset.Location = new Point(590, 0x61);
            this.btnLcdRemovePersonalPreset.Name = "btnLcdRemovePersonalPreset";
            this.btnLcdRemovePersonalPreset.Size = new Size(0x42, 0x15);
            this.btnLcdRemovePersonalPreset.TabIndex = 0x8e;
            this.btnLcdRemovePersonalPreset.Text = "Remove";
            this.btnLcdRemovePersonalPreset.UseVisualStyleBackColor = true;
            this.btnLcdRemovePersonalPreset.Click += new EventHandler(this.btnLcdRemovePersonalPreset_Click);
            this.btnLcdRemovePersonalPreset.MouseHover += new EventHandler(this.control_MouseHover);
            this.ddlLcdMiniFormat.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.ddlLcdMiniFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlLcdMiniFormat.ItemHeight = 13;
            this.ddlLcdMiniFormat.Location = new Point(0xb2, 0x49);
            this.ddlLcdMiniFormat.Name = "ddlLcdMiniFormat";
            this.ddlLcdMiniFormat.Size = new Size(0x146, 0x15);
            this.ddlLcdMiniFormat.TabIndex = 0x90;
            this.ddlLcdMiniFormat.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnLcdRemoveMiniPreset.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnLcdRemoveMiniPreset.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnLcdRemoveMiniPreset.Location = new Point(590, 0x49);
            this.btnLcdRemoveMiniPreset.Name = "btnLcdRemoveMiniPreset";
            this.btnLcdRemoveMiniPreset.Size = new Size(0x42, 0x15);
            this.btnLcdRemoveMiniPreset.TabIndex = 0x8d;
            this.btnLcdRemoveMiniPreset.Text = "Remove";
            this.btnLcdRemoveMiniPreset.UseVisualStyleBackColor = true;
            this.btnLcdRemoveMiniPreset.Click += new EventHandler(this.btnRemovePreset_Click);
            this.btnLcdRemoveMiniPreset.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbLcdEnabled.AutoSize = true;
            this.cbLcdEnabled.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.cbLcdEnabled.Location = new Point(3, 3);
            this.cbLcdEnabled.Name = "cbLcdEnabled";
            this.cbLcdEnabled.Size = new Size(160, 0x11);
            this.cbLcdEnabled.TabIndex = 0x87;
            this.cbLcdEnabled.Text = "Enable the LCD Display";
            this.cbLcdEnabled.UseVisualStyleBackColor = false;
            this.cbLcdEnabled.CheckedChanged += new EventHandler(this.cbLCDEnabled_CheckedChanged);
            this.cbLcdEnabled.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblLcdQuery.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblLcdQuery.BorderStyle = BorderStyle.FixedSingle;
            this.lblLcdQuery.Location = new Point(0x103, 3);
            this.lblLcdQuery.Name = "lblLcdQuery";
            this.lblLcdQuery.Size = new Size(0x18d, 0x15);
            this.lblLcdQuery.TabIndex = 0x8b;
            this.lblLcdQuery.Text = "Disconnected.";
            this.lblLcdQuery.TextAlign = ContentAlignment.MiddleLeft;
            this.lblLcdQuery.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbLcdRoute.AutoSize = true;
            this.cbLcdRoute.Enabled = false;
            this.cbLcdRoute.Location = new Point(3, 0x1a);
            this.cbLcdRoute.Name = "cbLcdRoute";
            this.cbLcdRoute.Size = new Size(0xd0, 0x11);
            this.cbLcdRoute.TabIndex = 0x88;
            this.cbLcdRoute.Text = "Route to Clipboard Sharer (If available)";
            this.cbLcdRoute.UseVisualStyleBackColor = true;
            this.cbLcdRoute.CheckedChanged += new EventHandler(this.cbLcdRoute_CheckedChanged);
            this.cbLcdRoute.MouseHover += new EventHandler(this.control_MouseHover);
            this.rtbLcdStatus.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.rtbLcdStatus.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.rtbLcdStatus.Location = new Point(6, 160);
            this.rtbLcdStatus.Name = "rtbLcdStatus";
            this.rtbLcdStatus.ReadOnly = true;
            this.rtbLcdStatus.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            this.rtbLcdStatus.Size = new Size(650, 0x73);
            this.rtbLcdStatus.TabIndex = 0x8a;
            this.rtbLcdStatus.Text = "";
            this.rtbLcdStatus.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnLcdAddPersonalPreset);
            base.Controls.Add(this.btnLcdAddMiniPreset);
            base.Controls.Add(this.lblPersonalParseFormat);
            base.Controls.Add(this.ddlLcdPersonalFormat);
            base.Controls.Add(this.lblLcdMiniFormat);
            base.Controls.Add(this.btnLcdRemovePersonalPreset);
            base.Controls.Add(this.ddlLcdMiniFormat);
            base.Controls.Add(this.btnLcdRemoveMiniPreset);
            base.Controls.Add(this.cbLcdEnabled);
            base.Controls.Add(this.lblLcdQuery);
            base.Controls.Add(this.cbLcdRoute);
            base.Controls.Add(this.rtbLcdStatus);
            base.Name = "Options_LcdGeneral";
            base.Size = new Size(0x293, 0x123);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

