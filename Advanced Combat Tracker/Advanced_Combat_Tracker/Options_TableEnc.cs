namespace Advanced_Combat_Tracker
{
    using Advanced_Combat_Tracker.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_TableEnc : UserControl
    {
        private Button btnEDdn;
        internal Button btnEDSort;
        internal Button btnEDSort2;
        private Button btnEDup;
        private Button btnTableDefaults;
        internal CheckedListBox clbED;
        private IContainer components;
        private GroupBox gbTableDepth2;
        private Label label47;
        private Label lblSortby1;
        private PictureBox pbEDs;
        private Dictionary<string, LocalizationObject> Trans = ActGlobals.ActLocalization.LocalizationStrings;

        public Options_TableEnc()
        {
            this.InitializeComponent();
        }

        private void btnEDdn_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.clbED.SelectedIndex;
            if ((selectedIndex >= 0) && (selectedIndex <= (this.clbED.Items.Count - 2)))
            {
                string item = (string) this.clbED.Items[selectedIndex];
                bool itemChecked = this.clbED.GetItemChecked(selectedIndex);
                this.clbED.Items.RemoveAt(selectedIndex);
                this.clbED.Items.Insert(selectedIndex + 1, item);
                this.clbED.SetItemChecked(selectedIndex + 1, itemChecked);
                this.clbED.SelectedIndex = selectedIndex + 1;
            }
        }

        private void btnEDSort_Click(object sender, EventArgs e)
        {
            try
            {
                string str = (string) this.clbED.Items[this.clbED.SelectedIndex];
                this.btnEDSort.Text = str;
            }
            catch
            {
                MessageBox.Show(this.Trans["messageBox-sortingColumnError"].DisplayedText, this.Trans["messageBoxTitle-sortingColumnError"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEDSort_TextChanged(object sender, EventArgs e)
        {
            ActGlobals.eDSort = this.btnEDSort.Text;
        }

        private void btnEDSort2_Click(object sender, EventArgs e)
        {
            try
            {
                string str = (string) this.clbED.Items[this.clbED.SelectedIndex];
                this.btnEDSort2.Text = str;
            }
            catch
            {
                MessageBox.Show(this.Trans["messageBox-sortingColumnError"].DisplayedText, this.Trans["messageBoxTitle-sortingColumnError"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEDSort2_TextChanged(object sender, EventArgs e)
        {
            ActGlobals.eDSort2 = this.btnEDSort2.Text;
        }

        private void btnEDup_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.clbED.SelectedIndex;
            if (selectedIndex >= 1)
            {
                string item = (string) this.clbED.Items[selectedIndex];
                bool itemChecked = this.clbED.GetItemChecked(selectedIndex);
                this.clbED.Items.RemoveAt(selectedIndex);
                this.clbED.Items.Insert(selectedIndex - 1, item);
                this.clbED.SetItemChecked(selectedIndex - 1, itemChecked);
                this.clbED.SelectedIndex = selectedIndex - 1;
            }
        }

        private void btnTableDefaults_Click(object sender, EventArgs e)
        {
            this.clbED.Items.Clear();
            ActGlobals.oFormActMain.ValidateTableSetup();
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
            this.gbTableDepth2 = new GroupBox();
            this.pbEDs = new PictureBox();
            this.btnEDup = new Button();
            this.clbED = new CheckedListBox();
            this.btnEDdn = new Button();
            this.lblSortby1 = new Label();
            this.btnEDSort = new Button();
            this.label47 = new Label();
            this.btnEDSort2 = new Button();
            this.btnTableDefaults = new Button();
            this.gbTableDepth2.SuspendLayout();
            ((ISupportInitialize) this.pbEDs).BeginInit();
            base.SuspendLayout();
            this.gbTableDepth2.Controls.Add(this.pbEDs);
            this.gbTableDepth2.Controls.Add(this.btnEDup);
            this.gbTableDepth2.Controls.Add(this.clbED);
            this.gbTableDepth2.Controls.Add(this.btnEDdn);
            this.gbTableDepth2.Controls.Add(this.lblSortby1);
            this.gbTableDepth2.Controls.Add(this.btnEDSort);
            this.gbTableDepth2.Controls.Add(this.label47);
            this.gbTableDepth2.Controls.Add(this.btnEDSort2);
            this.gbTableDepth2.Location = new Point(3, 3);
            this.gbTableDepth2.Name = "gbTableDepth2";
            this.gbTableDepth2.Size = new Size(0xd0, 0x259);
            this.gbTableDepth2.TabIndex = 1;
            this.gbTableDepth2.TabStop = false;
            this.gbTableDepth2.Text = "Encounter View";
            this.pbEDs.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.pbEDs.BorderStyle = BorderStyle.Fixed3D;
            this.pbEDs.Image = Resources.reorder;
            this.pbEDs.Location = new Point(0xb0, 0x48);
            this.pbEDs.Name = "pbEDs";
            this.pbEDs.Size = new Size(20, 20);
            this.pbEDs.TabIndex = 5;
            this.pbEDs.TabStop = false;
            this.btnEDup.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnEDup.Image = Resources.up;
            this.btnEDup.ImageAlign = ContentAlignment.BottomCenter;
            this.btnEDup.Location = new Point(0xa8, 0x10);
            this.btnEDup.Name = "btnEDup";
            this.btnEDup.Size = new Size(0x20, 0x18);
            this.btnEDup.TabIndex = 1;
            this.btnEDup.Click += new EventHandler(this.btnEDup_Click);
            this.clbED.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.clbED.IntegralHeight = false;
            this.clbED.Location = new Point(8, 0x10);
            this.clbED.Name = "clbED";
            this.clbED.Size = new Size(160, 0x209);
            this.clbED.TabIndex = 0;
            this.clbED.ThreeDCheckBoxes = true;
            this.btnEDdn.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnEDdn.Image = Resources.dn;
            this.btnEDdn.ImageAlign = ContentAlignment.TopCenter;
            this.btnEDdn.Location = new Point(0xa8, 40);
            this.btnEDdn.Name = "btnEDdn";
            this.btnEDdn.Size = new Size(0x20, 0x18);
            this.btnEDdn.TabIndex = 2;
            this.btnEDdn.Click += new EventHandler(this.btnEDdn_Click);
            this.lblSortby1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblSortby1.Location = new Point(8, 0x221);
            this.lblSortby1.Name = "lblSortby1";
            this.lblSortby1.Size = new Size(0x30, 0x18);
            this.lblSortby1.TabIndex = 4;
            this.lblSortby1.Text = "Sort #1:";
            this.lblSortby1.TextAlign = ContentAlignment.MiddleRight;
            this.btnEDSort.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnEDSort.FlatStyle = FlatStyle.Flat;
            this.btnEDSort.Location = new Point(0x40, 0x221);
            this.btnEDSort.Name = "btnEDSort";
            this.btnEDSort.Size = new Size(0x80, 0x17);
            this.btnEDSort.TabIndex = 3;
            this.btnEDSort.Text = "Ext DPS";
            this.btnEDSort.TextAlign = ContentAlignment.MiddleLeft;
            this.btnEDSort.TextChanged += new EventHandler(this.btnEDSort_TextChanged);
            this.btnEDSort.Click += new EventHandler(this.btnEDSort_Click);
            this.label47.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label47.Location = new Point(8, 0x239);
            this.label47.Name = "label47";
            this.label47.Size = new Size(0x30, 0x18);
            this.label47.TabIndex = 6;
            this.label47.Text = "Sort #2:";
            this.label47.TextAlign = ContentAlignment.MiddleRight;
            this.btnEDSort2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnEDSort2.FlatStyle = FlatStyle.Flat;
            this.btnEDSort2.Location = new Point(0x40, 0x239);
            this.btnEDSort2.Name = "btnEDSort2";
            this.btnEDSort2.Size = new Size(0x80, 0x17);
            this.btnEDSort2.TabIndex = 4;
            this.btnEDSort2.Text = "Duration";
            this.btnEDSort2.TextAlign = ContentAlignment.MiddleLeft;
            this.btnEDSort2.TextChanged += new EventHandler(this.btnEDSort2_TextChanged);
            this.btnEDSort2.Click += new EventHandler(this.btnEDSort2_Click);
            this.btnTableDefaults.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnTableDefaults.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnTableDefaults.Location = new Point(3, 610);
            this.btnTableDefaults.Name = "btnTableDefaults";
            this.btnTableDefaults.Size = new Size(0xd0, 0x13);
            this.btnTableDefaults.TabIndex = 6;
            this.btnTableDefaults.Text = "Reset Columns to Default";
            this.btnTableDefaults.UseVisualStyleBackColor = true;
            this.btnTableDefaults.Click += new EventHandler(this.btnTableDefaults_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.btnTableDefaults);
            base.Controls.Add(this.gbTableDepth2);
            base.Name = "Options_TableEnc";
            base.Size = new Size(0xd6, 0x278);
            this.gbTableDepth2.ResumeLayout(false);
            ((ISupportInitialize) this.pbEDs).EndInit();
            base.ResumeLayout(false);
        }
    }
}

