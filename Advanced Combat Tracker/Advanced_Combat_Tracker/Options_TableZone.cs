namespace Advanced_Combat_Tracker
{
    using Advanced_Combat_Tracker.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_TableZone : UserControl
    {
        private Button btnTableDefaults;
        private Button btnZDdn;
        private Button btnZDup;
        internal CheckedListBox clbZD;
        private IContainer components;
        private GroupBox gbTableDepth1;
        private PictureBox pbCDs;

        public Options_TableZone()
        {
            this.InitializeComponent();
        }

        private void btnTableDefaults_Click(object sender, EventArgs e)
        {
            this.clbZD.Items.Clear();
            ActGlobals.oFormActMain.ValidateTableSetup();
        }

        private void btnZDdn_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.clbZD.SelectedIndex;
            if ((selectedIndex >= 0) && (selectedIndex <= (this.clbZD.Items.Count - 2)))
            {
                string item = (string) this.clbZD.Items[selectedIndex];
                bool itemChecked = this.clbZD.GetItemChecked(selectedIndex);
                this.clbZD.Items.RemoveAt(selectedIndex);
                this.clbZD.Items.Insert(selectedIndex + 1, item);
                this.clbZD.SetItemChecked(selectedIndex + 1, itemChecked);
                this.clbZD.SelectedIndex = selectedIndex + 1;
            }
        }

        private void btnZDup_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.clbZD.SelectedIndex;
            if (selectedIndex >= 1)
            {
                string item = (string) this.clbZD.Items[selectedIndex];
                bool itemChecked = this.clbZD.GetItemChecked(selectedIndex);
                this.clbZD.Items.RemoveAt(selectedIndex);
                this.clbZD.Items.Insert(selectedIndex - 1, item);
                this.clbZD.SetItemChecked(selectedIndex - 1, itemChecked);
                this.clbZD.SelectedIndex = selectedIndex - 1;
            }
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
            this.gbTableDepth1 = new GroupBox();
            this.clbZD = new CheckedListBox();
            this.btnZDdn = new Button();
            this.btnZDup = new Button();
            this.pbCDs = new PictureBox();
            this.btnTableDefaults = new Button();
            this.gbTableDepth1.SuspendLayout();
            ((ISupportInitialize) this.pbCDs).BeginInit();
            base.SuspendLayout();
            this.gbTableDepth1.Controls.Add(this.clbZD);
            this.gbTableDepth1.Controls.Add(this.btnZDdn);
            this.gbTableDepth1.Controls.Add(this.btnZDup);
            this.gbTableDepth1.Controls.Add(this.pbCDs);
            this.gbTableDepth1.Location = new Point(3, 3);
            this.gbTableDepth1.Name = "gbTableDepth1";
            this.gbTableDepth1.Size = new Size(0xd0, 0x194);
            this.gbTableDepth1.TabIndex = 3;
            this.gbTableDepth1.TabStop = false;
            this.gbTableDepth1.Text = "Zone View";
            this.clbZD.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.clbZD.IntegralHeight = false;
            this.clbZD.Location = new Point(8, 0x10);
            this.clbZD.Name = "clbZD";
            this.clbZD.Size = new Size(160, 380);
            this.clbZD.TabIndex = 0;
            this.clbZD.ThreeDCheckBoxes = true;
            this.btnZDdn.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnZDdn.Image = Resources.dn;
            this.btnZDdn.Location = new Point(0xa8, 40);
            this.btnZDdn.Name = "btnZDdn";
            this.btnZDdn.Size = new Size(0x20, 0x18);
            this.btnZDdn.TabIndex = 2;
            this.btnZDdn.Click += new EventHandler(this.btnZDdn_Click);
            this.btnZDup.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnZDup.Image = Resources.up;
            this.btnZDup.Location = new Point(0xa8, 0x10);
            this.btnZDup.Name = "btnZDup";
            this.btnZDup.Size = new Size(0x20, 0x18);
            this.btnZDup.TabIndex = 1;
            this.btnZDup.Click += new EventHandler(this.btnZDup_Click);
            this.pbCDs.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.pbCDs.BorderStyle = BorderStyle.Fixed3D;
            this.pbCDs.Image = Resources.reorder;
            this.pbCDs.Location = new Point(0xb0, 0x48);
            this.pbCDs.Name = "pbCDs";
            this.pbCDs.Size = new Size(20, 20);
            this.pbCDs.TabIndex = 5;
            this.pbCDs.TabStop = false;
            this.btnTableDefaults.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnTableDefaults.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnTableDefaults.Location = new Point(3, 0x19d);
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
            base.Controls.Add(this.gbTableDepth1);
            base.Name = "Options_TableZone";
            base.Size = new Size(0xd6, 0x1b3);
            this.gbTableDepth1.ResumeLayout(false);
            ((ISupportInitialize) this.pbCDs).EndInit();
            base.ResumeLayout(false);
        }
    }
}

