namespace Advanced_Combat_Tracker
{
    using Advanced_Combat_Tracker.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_TableDamageType : UserControl
    {
        private Button btnMDdn;
        internal Button btnMDSort;
        internal Button btnMDSort2;
        private Button btnMDup;
        private Button btnTableDefaults;
        internal CheckedListBox clbDT;
        private IContainer components;
        private GroupBox gbTableDepth4;
        private Label label48;
        private Label lblSortby2;
        private PictureBox pbMDs;
        private Dictionary<string, LocalizationObject> Trans = ActGlobals.ActLocalization.LocalizationStrings;

        public Options_TableDamageType()
        {
            this.InitializeComponent();
        }

        private void btnMDdn_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.clbDT.SelectedIndex;
            if ((selectedIndex >= 0) && (selectedIndex <= (this.clbDT.Items.Count - 2)))
            {
                string item = (string) this.clbDT.Items[selectedIndex];
                bool itemChecked = this.clbDT.GetItemChecked(selectedIndex);
                this.clbDT.Items.RemoveAt(selectedIndex);
                this.clbDT.Items.Insert(selectedIndex + 1, item);
                this.clbDT.SetItemChecked(selectedIndex + 1, itemChecked);
                this.clbDT.SelectedIndex = selectedIndex + 1;
            }
        }

        private void btnMDSort_Click(object sender, EventArgs e)
        {
            try
            {
                string str = (string) this.clbDT.Items[this.clbDT.SelectedIndex];
                this.btnMDSort.Text = str;
            }
            catch
            {
                MessageBox.Show(this.Trans["messageBox-sortingColumnError"].DisplayedText, this.Trans["messageBoxTitle-sortingColumnError"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnMDSort_TextChanged(object sender, EventArgs e)
        {
            ActGlobals.mDSort = this.btnMDSort.Text;
        }

        private void btnMDSort2_Click(object sender, EventArgs e)
        {
            try
            {
                string str = (string) this.clbDT.Items[this.clbDT.SelectedIndex];
                this.btnMDSort2.Text = str;
            }
            catch
            {
                MessageBox.Show(this.Trans["messageBox-sortingColumnError"].DisplayedText, this.Trans["messageBoxTitle-sortingColumnError"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnMDSort2_TextChanged(object sender, EventArgs e)
        {
            ActGlobals.mDSort2 = this.btnMDSort2.Text;
        }

        private void btnMDup_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.clbDT.SelectedIndex;
            if (selectedIndex >= 1)
            {
                string item = (string) this.clbDT.Items[selectedIndex];
                bool itemChecked = this.clbDT.GetItemChecked(selectedIndex);
                this.clbDT.Items.RemoveAt(selectedIndex);
                this.clbDT.Items.Insert(selectedIndex - 1, item);
                this.clbDT.SetItemChecked(selectedIndex - 1, itemChecked);
                this.clbDT.SelectedIndex = selectedIndex - 1;
            }
        }

        private void btnTableDefaults_Click(object sender, EventArgs e)
        {
            this.clbDT.Items.Clear();
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
            this.gbTableDepth4 = new GroupBox();
            this.clbDT = new CheckedListBox();
            this.btnMDdn = new Button();
            this.btnMDup = new Button();
            this.lblSortby2 = new Label();
            this.btnMDSort = new Button();
            this.pbMDs = new PictureBox();
            this.label48 = new Label();
            this.btnMDSort2 = new Button();
            this.btnTableDefaults = new Button();
            this.gbTableDepth4.SuspendLayout();
            ((ISupportInitialize) this.pbMDs).BeginInit();
            base.SuspendLayout();
            this.gbTableDepth4.Controls.Add(this.clbDT);
            this.gbTableDepth4.Controls.Add(this.btnMDdn);
            this.gbTableDepth4.Controls.Add(this.btnMDup);
            this.gbTableDepth4.Controls.Add(this.lblSortby2);
            this.gbTableDepth4.Controls.Add(this.btnMDSort);
            this.gbTableDepth4.Controls.Add(this.pbMDs);
            this.gbTableDepth4.Controls.Add(this.label48);
            this.gbTableDepth4.Controls.Add(this.btnMDSort2);
            this.gbTableDepth4.Location = new Point(3, 3);
            this.gbTableDepth4.Name = "gbTableDepth4";
            this.gbTableDepth4.Size = new Size(0xd0, 0x1e9);
            this.gbTableDepth4.TabIndex = 3;
            this.gbTableDepth4.TabStop = false;
            this.gbTableDepth4.Text = "Damage Types View";
            this.clbDT.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.clbDT.IntegralHeight = false;
            this.clbDT.Location = new Point(8, 0x10);
            this.clbDT.Name = "clbDT";
            this.clbDT.Size = new Size(160, 0x199);
            this.clbDT.TabIndex = 0;
            this.clbDT.ThreeDCheckBoxes = true;
            this.btnMDdn.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnMDdn.Image = Resources.dn;
            this.btnMDdn.Location = new Point(0xa8, 40);
            this.btnMDdn.Name = "btnMDdn";
            this.btnMDdn.Size = new Size(0x20, 0x18);
            this.btnMDdn.TabIndex = 2;
            this.btnMDdn.Click += new EventHandler(this.btnMDdn_Click);
            this.btnMDup.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnMDup.Image = Resources.up;
            this.btnMDup.Location = new Point(0xa8, 0x10);
            this.btnMDup.Name = "btnMDup";
            this.btnMDup.Size = new Size(0x20, 0x18);
            this.btnMDup.TabIndex = 1;
            this.btnMDup.Click += new EventHandler(this.btnMDup_Click);
            this.lblSortby2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblSortby2.Location = new Point(8, 0x1b1);
            this.lblSortby2.Name = "lblSortby2";
            this.lblSortby2.Size = new Size(0x30, 0x18);
            this.lblSortby2.TabIndex = 0x10;
            this.lblSortby2.Text = "Sort #1:";
            this.lblSortby2.TextAlign = ContentAlignment.MiddleRight;
            this.btnMDSort.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnMDSort.FlatStyle = FlatStyle.Flat;
            this.btnMDSort.Location = new Point(0x40, 0x1b1);
            this.btnMDSort.Name = "btnMDSort";
            this.btnMDSort.Size = new Size(0x88, 0x17);
            this.btnMDSort.TabIndex = 3;
            this.btnMDSort.Text = "Damage";
            this.btnMDSort.TextAlign = ContentAlignment.MiddleLeft;
            this.btnMDSort.TextChanged += new EventHandler(this.btnMDSort_TextChanged);
            this.btnMDSort.Click += new EventHandler(this.btnMDSort_Click);
            this.pbMDs.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.pbMDs.BorderStyle = BorderStyle.Fixed3D;
            this.pbMDs.Image = Resources.reorder;
            this.pbMDs.Location = new Point(0xb0, 0x48);
            this.pbMDs.Name = "pbMDs";
            this.pbMDs.Size = new Size(20, 20);
            this.pbMDs.TabIndex = 5;
            this.pbMDs.TabStop = false;
            this.label48.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label48.Location = new Point(8, 0x1c9);
            this.label48.Name = "label48";
            this.label48.Size = new Size(0x30, 0x18);
            this.label48.TabIndex = 0x12;
            this.label48.Text = "Sort #2:";
            this.label48.TextAlign = ContentAlignment.MiddleRight;
            this.btnMDSort2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnMDSort2.FlatStyle = FlatStyle.Flat;
            this.btnMDSort2.Location = new Point(0x40, 0x1c9);
            this.btnMDSort2.Name = "btnMDSort2";
            this.btnMDSort2.Size = new Size(0x88, 0x17);
            this.btnMDSort2.TabIndex = 4;
            this.btnMDSort2.Text = "Duration";
            this.btnMDSort2.TextAlign = ContentAlignment.MiddleLeft;
            this.btnMDSort2.Click += new EventHandler(this.btnMDSort2_Click);
            this.btnTableDefaults.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnTableDefaults.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnTableDefaults.Location = new Point(3, 0x1f2);
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
            base.Controls.Add(this.gbTableDepth4);
            base.Name = "Options_TableDamageType";
            base.Size = new Size(0xd6, 520);
            this.gbTableDepth4.ResumeLayout(false);
            ((ISupportInitialize) this.pbMDs).EndInit();
            base.ResumeLayout(false);
        }
    }
}

