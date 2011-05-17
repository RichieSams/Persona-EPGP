namespace Advanced_Combat_Tracker
{
    using Advanced_Combat_Tracker.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_TableCombatant : UserControl
    {
        private Button btnCDdn;
        private Button btnCDup;
        private Button btnTableDefaults;
        internal CheckedListBox clbCD;
        private IContainer components;
        private GroupBox gbTableDepth3;
        private PictureBox pbCDs;

        public Options_TableCombatant()
        {
            this.InitializeComponent();
        }

        private void btnCDdn_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.clbCD.SelectedIndex;
            if ((selectedIndex >= 0) && (selectedIndex <= (this.clbCD.Items.Count - 2)))
            {
                string item = (string) this.clbCD.Items[selectedIndex];
                bool itemChecked = this.clbCD.GetItemChecked(selectedIndex);
                this.clbCD.Items.RemoveAt(selectedIndex);
                this.clbCD.Items.Insert(selectedIndex + 1, item);
                this.clbCD.SetItemChecked(selectedIndex + 1, itemChecked);
                this.clbCD.SelectedIndex = selectedIndex + 1;
            }
        }

        private void btnCDup_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.clbCD.SelectedIndex;
            if (selectedIndex >= 1)
            {
                string item = (string) this.clbCD.Items[selectedIndex];
                bool itemChecked = this.clbCD.GetItemChecked(selectedIndex);
                this.clbCD.Items.RemoveAt(selectedIndex);
                this.clbCD.Items.Insert(selectedIndex - 1, item);
                this.clbCD.SetItemChecked(selectedIndex - 1, itemChecked);
                this.clbCD.SelectedIndex = selectedIndex - 1;
            }
        }

        private void btnTableDefaults_Click(object sender, EventArgs e)
        {
            this.clbCD.Items.Clear();
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
            this.gbTableDepth3 = new GroupBox();
            this.clbCD = new CheckedListBox();
            this.btnCDdn = new Button();
            this.btnCDup = new Button();
            this.pbCDs = new PictureBox();
            this.btnTableDefaults = new Button();
            this.gbTableDepth3.SuspendLayout();
            ((ISupportInitialize) this.pbCDs).BeginInit();
            base.SuspendLayout();
            this.gbTableDepth3.Controls.Add(this.clbCD);
            this.gbTableDepth3.Controls.Add(this.btnCDdn);
            this.gbTableDepth3.Controls.Add(this.btnCDup);
            this.gbTableDepth3.Controls.Add(this.pbCDs);
            this.gbTableDepth3.Location = new Point(3, 3);
            this.gbTableDepth3.Name = "gbTableDepth3";
            this.gbTableDepth3.Size = new Size(0xd0, 0x194);
            this.gbTableDepth3.TabIndex = 2;
            this.gbTableDepth3.TabStop = false;
            this.gbTableDepth3.Text = "Combatant View";
            this.clbCD.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.clbCD.IntegralHeight = false;
            this.clbCD.Location = new Point(8, 0x10);
            this.clbCD.Name = "clbCD";
            this.clbCD.Size = new Size(160, 380);
            this.clbCD.TabIndex = 0;
            this.clbCD.ThreeDCheckBoxes = true;
            this.btnCDdn.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnCDdn.Image = Resources.dn;
            this.btnCDdn.Location = new Point(0xa8, 40);
            this.btnCDdn.Name = "btnCDdn";
            this.btnCDdn.Size = new Size(0x20, 0x18);
            this.btnCDdn.TabIndex = 2;
            this.btnCDdn.Click += new EventHandler(this.btnCDdn_Click);
            this.btnCDup.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnCDup.Image = Resources.up;
            this.btnCDup.Location = new Point(0xa8, 0x10);
            this.btnCDup.Name = "btnCDup";
            this.btnCDup.Size = new Size(0x20, 0x18);
            this.btnCDup.TabIndex = 1;
            this.btnCDup.Click += new EventHandler(this.btnCDup_Click);
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
            base.Controls.Add(this.gbTableDepth3);
            base.Name = "Options_TableCombatant";
            base.Size = new Size(0xd6, 0x1b3);
            this.gbTableDepth3.ResumeLayout(false);
            ((ISupportInitialize) this.pbCDs).EndInit();
            base.ResumeLayout(false);
        }
    }
}

