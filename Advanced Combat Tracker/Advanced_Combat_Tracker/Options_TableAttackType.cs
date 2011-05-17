namespace Advanced_Combat_Tracker
{
    using Advanced_Combat_Tracker.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_TableAttackType : UserControl
    {
        private Button btnATdn;
        internal Button btnATSort;
        internal Button btnATSort2;
        private Button btnATup;
        private Button btnTableDefaults;
        internal CheckedListBox clbAT;
        private IContainer components;
        private GroupBox gbTableDepth5;
        private Label label49;
        private Label lblSortby3;
        private PictureBox pbATs;
        private Dictionary<string, LocalizationObject> Trans = ActGlobals.ActLocalization.LocalizationStrings;

        public Options_TableAttackType()
        {
            this.InitializeComponent();
        }

        private void btnATdn_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.clbAT.SelectedIndex;
            if ((selectedIndex >= 0) && (selectedIndex <= (this.clbAT.Items.Count - 2)))
            {
                string item = (string) this.clbAT.Items[selectedIndex];
                bool itemChecked = this.clbAT.GetItemChecked(selectedIndex);
                this.clbAT.Items.RemoveAt(selectedIndex);
                this.clbAT.Items.Insert(selectedIndex + 1, item);
                this.clbAT.SetItemChecked(selectedIndex + 1, itemChecked);
                this.clbAT.SelectedIndex = selectedIndex + 1;
            }
        }

        private void btnATSort_Click(object sender, EventArgs e)
        {
            try
            {
                string str = (string) this.clbAT.Items[this.clbAT.SelectedIndex];
                this.btnATSort.Text = str;
            }
            catch
            {
                MessageBox.Show(this.Trans["messageBox-sortingColumnError"].DisplayedText, this.Trans["messageBoxTitle-sortingColumnError"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnATSort_TextChanged(object sender, EventArgs e)
        {
            ActGlobals.aTSort = this.btnATSort.Text;
        }

        private void btnATSort2_Click(object sender, EventArgs e)
        {
            try
            {
                string str = (string) this.clbAT.Items[this.clbAT.SelectedIndex];
                this.btnATSort2.Text = str;
            }
            catch
            {
                MessageBox.Show(this.Trans["messageBox-sortingColumnError"].DisplayedText, this.Trans["messageBoxTitle-sortingColumnError"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnATSort2_TextChanged(object sender, EventArgs e)
        {
            ActGlobals.aTSort2 = this.btnATSort2.Text;
        }

        private void btnATup_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.clbAT.SelectedIndex;
            if (selectedIndex >= 1)
            {
                string item = (string) this.clbAT.Items[selectedIndex];
                bool itemChecked = this.clbAT.GetItemChecked(selectedIndex);
                this.clbAT.Items.RemoveAt(selectedIndex);
                this.clbAT.Items.Insert(selectedIndex - 1, item);
                this.clbAT.SetItemChecked(selectedIndex - 1, itemChecked);
                this.clbAT.SelectedIndex = selectedIndex - 1;
            }
        }

        private void btnTableDefaults_Click(object sender, EventArgs e)
        {
            this.clbAT.Items.Clear();
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
            this.gbTableDepth5 = new GroupBox();
            this.clbAT = new CheckedListBox();
            this.btnATdn = new Button();
            this.btnATup = new Button();
            this.lblSortby3 = new Label();
            this.btnATSort = new Button();
            this.pbATs = new PictureBox();
            this.label49 = new Label();
            this.btnATSort2 = new Button();
            this.btnTableDefaults = new Button();
            this.gbTableDepth5.SuspendLayout();
            ((ISupportInitialize) this.pbATs).BeginInit();
            base.SuspendLayout();
            this.gbTableDepth5.Controls.Add(this.clbAT);
            this.gbTableDepth5.Controls.Add(this.btnATdn);
            this.gbTableDepth5.Controls.Add(this.btnATup);
            this.gbTableDepth5.Controls.Add(this.lblSortby3);
            this.gbTableDepth5.Controls.Add(this.btnATSort);
            this.gbTableDepth5.Controls.Add(this.pbATs);
            this.gbTableDepth5.Controls.Add(this.label49);
            this.gbTableDepth5.Controls.Add(this.btnATSort2);
            this.gbTableDepth5.Location = new Point(3, 3);
            this.gbTableDepth5.Name = "gbTableDepth5";
            this.gbTableDepth5.Size = new Size(0xd0, 280);
            this.gbTableDepth5.TabIndex = 4;
            this.gbTableDepth5.TabStop = false;
            this.gbTableDepth5.Text = "Attack Type View";
            this.clbAT.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.clbAT.IntegralHeight = false;
            this.clbAT.Location = new Point(8, 0x10);
            this.clbAT.Name = "clbAT";
            this.clbAT.Size = new Size(160, 200);
            this.clbAT.TabIndex = 0;
            this.clbAT.ThreeDCheckBoxes = true;
            this.btnATdn.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnATdn.Image = Resources.dn;
            this.btnATdn.Location = new Point(0xa8, 40);
            this.btnATdn.Name = "btnATdn";
            this.btnATdn.Size = new Size(0x20, 0x18);
            this.btnATdn.TabIndex = 2;
            this.btnATdn.Click += new EventHandler(this.btnATdn_Click);
            this.btnATup.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnATup.Image = Resources.up;
            this.btnATup.Location = new Point(0xa8, 0x10);
            this.btnATup.Name = "btnATup";
            this.btnATup.Size = new Size(0x20, 0x18);
            this.btnATup.TabIndex = 1;
            this.btnATup.Click += new EventHandler(this.btnATup_Click);
            this.lblSortby3.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblSortby3.Location = new Point(8, 0xe0);
            this.lblSortby3.Name = "lblSortby3";
            this.lblSortby3.Size = new Size(0x30, 0x18);
            this.lblSortby3.TabIndex = 0x18;
            this.lblSortby3.Text = "Sort #1:";
            this.lblSortby3.TextAlign = ContentAlignment.MiddleRight;
            this.btnATSort.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnATSort.FlatStyle = FlatStyle.Flat;
            this.btnATSort.Location = new Point(0x40, 0xe0);
            this.btnATSort.Name = "btnATSort";
            this.btnATSort.Size = new Size(0x80, 0x17);
            this.btnATSort.TabIndex = 3;
            this.btnATSort.Text = "Time";
            this.btnATSort.TextAlign = ContentAlignment.MiddleLeft;
            this.btnATSort.TextChanged += new EventHandler(this.btnATSort_TextChanged);
            this.btnATSort.Click += new EventHandler(this.btnATSort_Click);
            this.pbATs.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.pbATs.BorderStyle = BorderStyle.Fixed3D;
            this.pbATs.Image = Resources.reorder;
            this.pbATs.Location = new Point(0xb0, 0x48);
            this.pbATs.Name = "pbATs";
            this.pbATs.Size = new Size(20, 20);
            this.pbATs.TabIndex = 5;
            this.pbATs.TabStop = false;
            this.label49.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label49.Location = new Point(8, 0xf8);
            this.label49.Name = "label49";
            this.label49.Size = new Size(0x30, 0x18);
            this.label49.TabIndex = 0x1a;
            this.label49.Text = "Sort #2:";
            this.label49.TextAlign = ContentAlignment.MiddleRight;
            this.btnATSort2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnATSort2.FlatStyle = FlatStyle.Flat;
            this.btnATSort2.Location = new Point(0x40, 0xf8);
            this.btnATSort2.Name = "btnATSort2";
            this.btnATSort2.Size = new Size(0x80, 0x17);
            this.btnATSort2.TabIndex = 4;
            this.btnATSort2.Text = "Time";
            this.btnATSort2.TextAlign = ContentAlignment.MiddleLeft;
            this.btnATSort2.TextChanged += new EventHandler(this.btnATSort2_TextChanged);
            this.btnATSort2.Click += new EventHandler(this.btnATSort2_Click);
            this.btnTableDefaults.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnTableDefaults.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnTableDefaults.Location = new Point(3, 0x121);
            this.btnTableDefaults.Name = "btnTableDefaults";
            this.btnTableDefaults.Size = new Size(0xd0, 0x13);
            this.btnTableDefaults.TabIndex = 5;
            this.btnTableDefaults.Text = "Reset Columns to Default";
            this.btnTableDefaults.UseVisualStyleBackColor = true;
            this.btnTableDefaults.Click += new EventHandler(this.btnTableDefaults_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.gbTableDepth5);
            base.Controls.Add(this.btnTableDefaults);
            base.Name = "Options_TableAttackType";
            base.Size = new Size(0xd6, 0x137);
            this.gbTableDepth5.ResumeLayout(false);
            ((ISupportInitialize) this.pbATs).EndInit();
            base.ResumeLayout(false);
        }
    }
}

