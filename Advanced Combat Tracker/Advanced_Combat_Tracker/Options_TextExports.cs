namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_TextExports : UserControl
    {
        internal Button btnAddPresetClip;
        private Button btnExFileAddPreset;
        private Button btnExFileRemovePreset;
        private Button btnRemovePresetClip;
        internal CheckBox cbExFileColumnAlign;
        internal CheckBox cbExportFilterSpace;
        internal CheckBox cbExText;
        internal CheckedListBox clbExFilePresets;
        private IContainer components;
        internal ComboBox ddlClipFormat;
        internal ComboBox ddlExMacroClipPreset;
        private GroupBox groupBox35;
        private Label label3;
        private Label lblExFile1;
        private Label lblExFile2;
        private Label lblExFile3;
        private Label lblExFileLines;
        internal NumericUpDown nudExFileLines;
        private TextBox tbExFileName;
        internal TextBox tbExFilePrefix;

        public Options_TextExports()
        {
            this.InitializeComponent();
        }

        private void btnAddPresetClip_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.btnShowTextFormatWindow_Click(sender, e);
        }

        private void btnExFileAddPreset_Click(object sender, EventArgs e)
        {
            MacroExportSetting setting = new MacroExportSetting(this.tbExFilePrefix.Text, this.ddlExMacroClipPreset.SelectedIndex, (int) this.nudExFileLines.Value, this.cbExFileColumnAlign.Checked, this.tbExFileName.Text);
            if (!this.clbExFilePresets.Items.Contains(setting))
            {
                this.clbExFilePresets.Items.Add(setting);
                int index = this.clbExFilePresets.Items.IndexOf(setting);
                this.clbExFilePresets.SetItemChecked(index, true);
            }
        }

        private void btnExFileRemovePreset_Click(object sender, EventArgs e)
        {
            if (this.clbExFilePresets.SelectedIndex > -1)
            {
                this.clbExFilePresets.Items.RemoveAt(this.clbExFilePresets.SelectedIndex);
            }
        }

        private void btnRemovePresetClip_Click(object sender, EventArgs e)
        {
            if (this.ddlClipFormat.SelectedIndex > -1)
            {
                ActGlobals.oFormActMain.RemoveTextFormat(this.ddlClipFormat.SelectedIndex);
            }
        }

        private void clbExFilePresets_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            MacroExportSetting setting = (MacroExportSetting) this.clbExFilePresets.Items[e.Index];
            setting.Active = e.NewValue == CheckState.Checked;
        }

        private void clbExFilePresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.clbExFilePresets.SelectedIndex > -1)
            {
                MacroExportSetting setting = (MacroExportSetting) this.clbExFilePresets.Items[this.clbExFilePresets.SelectedIndex];
                this.tbExFilePrefix.Text = setting.ExportChannel;
                this.ddlExMacroClipPreset.SelectedIndex = setting.ExportPresetIndex;
                this.nudExFileLines.Value = setting.ExportMaxLines;
                this.cbExFileColumnAlign.Checked = setting.AlignToContent;
                this.tbExFileName.Text = setting.ExportFile;
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
            this.label3 = new Label();
            this.ddlClipFormat = new ComboBox();
            this.groupBox35 = new GroupBox();
            this.ddlExMacroClipPreset = new ComboBox();
            this.tbExFileName = new TextBox();
            this.lblExFile3 = new Label();
            this.btnExFileRemovePreset = new Button();
            this.btnExFileAddPreset = new Button();
            this.clbExFilePresets = new CheckedListBox();
            this.lblExFile2 = new Label();
            this.lblExFile1 = new Label();
            this.lblExFileLines = new Label();
            this.nudExFileLines = new NumericUpDown();
            this.cbExFileColumnAlign = new CheckBox();
            this.tbExFilePrefix = new TextBox();
            this.btnAddPresetClip = new Button();
            this.btnRemovePresetClip = new Button();
            this.cbExText = new CheckBox();
            this.cbExportFilterSpace = new CheckBox();
            this.groupBox35.SuspendLayout();
            this.nudExFileLines.BeginInit();
            base.SuspendLayout();
            this.label3.AutoSize = true;
            this.label3.Location = new Point(3, 0x1a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x6a, 13);
            this.label3.TabIndex = 0x72;
            this.label3.Text = "Clipboard Formatting:";
            this.label3.MouseHover += new EventHandler(this.control_MouseHover);
            this.ddlClipFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlClipFormat.ItemHeight = 13;
            this.ddlClipFormat.Location = new Point(0x93, 0x17);
            this.ddlClipFormat.Name = "ddlClipFormat";
            this.ddlClipFormat.Size = new Size(0x17a, 0x15);
            this.ddlClipFormat.TabIndex = 0x6f;
            this.ddlClipFormat.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox35.Controls.Add(this.ddlExMacroClipPreset);
            this.groupBox35.Controls.Add(this.tbExFileName);
            this.groupBox35.Controls.Add(this.lblExFile3);
            this.groupBox35.Controls.Add(this.btnExFileRemovePreset);
            this.groupBox35.Controls.Add(this.btnExFileAddPreset);
            this.groupBox35.Controls.Add(this.clbExFilePresets);
            this.groupBox35.Controls.Add(this.lblExFile2);
            this.groupBox35.Controls.Add(this.lblExFile1);
            this.groupBox35.Controls.Add(this.lblExFileLines);
            this.groupBox35.Controls.Add(this.nudExFileLines);
            this.groupBox35.Controls.Add(this.cbExFileColumnAlign);
            this.groupBox35.Controls.Add(this.tbExFilePrefix);
            this.groupBox35.Location = new Point(3, 0x3a);
            this.groupBox35.Name = "groupBox35";
            this.groupBox35.Size = new Size(0x29d, 0xa2);
            this.groupBox35.TabIndex = 0x71;
            this.groupBox35.TabStop = false;
            this.groupBox35.Text = "Export to EQ2 Macro File after combat.";
            this.ddlExMacroClipPreset.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlExMacroClipPreset.FormattingEnabled = true;
            this.ddlExMacroClipPreset.Location = new Point(0x138, 0x11);
            this.ddlExMacroClipPreset.Name = "ddlExMacroClipPreset";
            this.ddlExMacroClipPreset.Size = new Size(0x15f, 0x15);
            this.ddlExMacroClipPreset.TabIndex = 0x3e;
            this.ddlExMacroClipPreset.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbExFileName.Location = new Point(0x1ac, 0x88);
            this.tbExFileName.Name = "tbExFileName";
            this.tbExFileName.Size = new Size(0xeb, 20);
            this.tbExFileName.TabIndex = 60;
            this.tbExFileName.Text = "act-export.txt";
            this.tbExFileName.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblExFile3.AutoSize = true;
            this.lblExFile3.Location = new Point(0xe2, 0x8b);
            this.lblExFile3.Name = "lblExFile3";
            this.lblExFile3.Size = new Size(0xb1, 13);
            this.lblExFile3.TabIndex = 0x3d;
            this.lblExFile3.Text = "EQ2 Command: /do_file_commands";
            this.lblExFile3.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnExFileRemovePreset.Location = new Point(0x72, 0x8a);
            this.btnExFileRemovePreset.Name = "btnExFileRemovePreset";
            this.btnExFileRemovePreset.Size = new Size(0x6a, 0x12);
            this.btnExFileRemovePreset.TabIndex = 0x3b;
            this.btnExFileRemovePreset.Text = "Remove";
            this.btnExFileRemovePreset.UseVisualStyleBackColor = true;
            this.btnExFileRemovePreset.Click += new EventHandler(this.btnExFileRemovePreset_Click);
            this.btnExFileRemovePreset.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnExFileAddPreset.Location = new Point(6, 0x8a);
            this.btnExFileAddPreset.Name = "btnExFileAddPreset";
            this.btnExFileAddPreset.Size = new Size(0x6a, 0x12);
            this.btnExFileAddPreset.TabIndex = 0x3b;
            this.btnExFileAddPreset.Text = "Add";
            this.btnExFileAddPreset.UseVisualStyleBackColor = true;
            this.btnExFileAddPreset.Click += new EventHandler(this.btnExFileAddPreset_Click);
            this.btnExFileAddPreset.MouseHover += new EventHandler(this.control_MouseHover);
            this.clbExFilePresets.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.clbExFilePresets.FormattingEnabled = true;
            this.clbExFilePresets.IntegralHeight = false;
            this.clbExFilePresets.Location = new Point(6, 0x13);
            this.clbExFilePresets.Name = "clbExFilePresets";
            this.clbExFilePresets.Size = new Size(0xd6, 0x75);
            this.clbExFilePresets.TabIndex = 0x3a;
            this.clbExFilePresets.ItemCheck += new ItemCheckEventHandler(this.clbExFilePresets_ItemCheck);
            this.clbExFilePresets.SelectedIndexChanged += new EventHandler(this.clbExFilePresets_SelectedIndexChanged);
            this.clbExFilePresets.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblExFile2.AutoSize = true;
            this.lblExFile2.Location = new Point(0xe2, 0x29);
            this.lblExFile2.Name = "lblExFile2";
            this.lblExFile2.Size = new Size(0x83, 0x1a);
            this.lblExFile2.TabIndex = 0x39;
            this.lblExFile2.Text = "Channel command\r\n(gsay, raidsay, tellchannel)";
            this.lblExFile2.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblExFile1.AutoSize = true;
            this.lblExFile1.Location = new Point(0xe2, 0x16);
            this.lblExFile1.Name = "lblExFile1";
            this.lblExFile1.Size = new Size(0x3b, 13);
            this.lblExFile1.TabIndex = 0x39;
            this.lblExFile1.Text = "Formatting:";
            this.lblExFile1.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblExFileLines.AutoSize = true;
            this.lblExFileLines.Location = new Point(0x128, 0x75);
            this.lblExFileLines.Name = "lblExFileLines";
            this.lblExFileLines.Size = new Size(0xdb, 13);
            this.lblExFileLines.TabIndex = 0x36;
            this.lblExFileLines.Text = "Maximum number of lines to attempt to export";
            this.lblExFileLines.MouseHover += new EventHandler(this.control_MouseHover);
            this.nudExFileLines.Location = new Point(0xe5, 0x71);
            int[] bits = new int[4];
            bits[0] = 1;
            this.nudExFileLines.Minimum = new decimal(bits);
            this.nudExFileLines.Name = "nudExFileLines";
            this.nudExFileLines.Size = new Size(0x3d, 20);
            this.nudExFileLines.TabIndex = 0x35;
            int[] numArray2 = new int[4];
            numArray2[0] = 0x10;
            this.nudExFileLines.Value = new decimal(numArray2);
            this.cbExFileColumnAlign.AutoSize = true;
            this.cbExFileColumnAlign.Checked = true;
            this.cbExFileColumnAlign.CheckState = CheckState.Checked;
            this.cbExFileColumnAlign.Location = new Point(0xe5, 90);
            this.cbExFileColumnAlign.Name = "cbExFileColumnAlign";
            this.cbExFileColumnAlign.Size = new Size(280, 0x11);
            this.cbExFileColumnAlign.TabIndex = 0;
            this.cbExFileColumnAlign.Text = "Align text to appear in columns based on cell contents";
            this.cbExFileColumnAlign.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbExFilePrefix.Location = new Point(0x187, 0x2c);
            this.tbExFilePrefix.Name = "tbExFilePrefix";
            this.tbExFilePrefix.Size = new Size(0xa3, 20);
            this.tbExFilePrefix.TabIndex = 50;
            this.tbExFilePrefix.Text = "gsay";
            this.tbExFilePrefix.Leave += new EventHandler(this.tbExFilePrefix_Leave);
            this.tbExFilePrefix.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnAddPresetClip.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnAddPresetClip.Location = new Point(0x213, 0x17);
            this.btnAddPresetClip.Name = "btnAddPresetClip";
            this.btnAddPresetClip.Size = new Size(0x43, 0x15);
            this.btnAddPresetClip.TabIndex = 0x70;
            this.btnAddPresetClip.Text = "Add Preset";
            this.btnAddPresetClip.UseVisualStyleBackColor = true;
            this.btnAddPresetClip.Click += new EventHandler(this.btnAddPresetClip_Click);
            this.btnAddPresetClip.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnRemovePresetClip.BackColor = SystemColors.Control;
            this.btnRemovePresetClip.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnRemovePresetClip.Location = new Point(0x25c, 0x17);
            this.btnRemovePresetClip.Name = "btnRemovePresetClip";
            this.btnRemovePresetClip.Size = new Size(0x44, 0x15);
            this.btnRemovePresetClip.TabIndex = 110;
            this.btnRemovePresetClip.Text = "Remove";
            this.btnRemovePresetClip.UseVisualStyleBackColor = true;
            this.btnRemovePresetClip.Click += new EventHandler(this.btnRemovePresetClip_Click);
            this.btnRemovePresetClip.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbExText.AutoSize = true;
            this.cbExText.Checked = true;
            this.cbExText.CheckState = CheckState.Checked;
            this.cbExText.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.cbExText.Location = new Point(3, 3);
            this.cbExText.Name = "cbExText";
            this.cbExText.Size = new Size(0xd5, 0x11);
            this.cbExText.TabIndex = 0x6d;
            this.cbExText.Text = "Export to Clipboard after combat.";
            this.cbExText.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbExportFilterSpace.AutoSize = true;
            this.cbExportFilterSpace.Location = new Point(3, 0xed);
            this.cbExportFilterSpace.Margin = new Padding(3, 1, 3, 1);
            this.cbExportFilterSpace.Name = "cbExportFilterSpace";
            this.cbExportFilterSpace.Size = new Size(0x1ac, 0x11);
            this.cbExportFilterSpace.TabIndex = 0x73;
            this.cbExportFilterSpace.Text = "For text exports, filter out combatants with spaces in their name. (Possible swarm pets)";
            this.cbExportFilterSpace.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.cbExportFilterSpace);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.ddlClipFormat);
            base.Controls.Add(this.groupBox35);
            base.Controls.Add(this.btnAddPresetClip);
            base.Controls.Add(this.btnRemovePresetClip);
            base.Controls.Add(this.cbExText);
            base.Name = "Options_TextExports";
            base.Size = new Size(0x2a3, 0xff);
            base.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox35.ResumeLayout(false);
            this.groupBox35.PerformLayout();
            this.nudExFileLines.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void tbExFilePrefix_Leave(object sender, EventArgs e)
        {
            this.tbExFilePrefix.Text = this.tbExFilePrefix.Text.Replace("/", "").Trim();
            if (string.IsNullOrEmpty(this.tbExFilePrefix.Text))
            {
                MessageBox.Show("Leaving this blank field is dangerous and will result in macro files that will not function unless the used Clipboard Format properly prefixes channel commands to every line.", "DoFileCommands Channel Prefix", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}

