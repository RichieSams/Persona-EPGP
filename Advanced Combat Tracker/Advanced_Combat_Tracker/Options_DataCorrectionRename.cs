namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_DataCorrectionRename : UserControl
    {
        private Button btnRenameAdd;
        private Button btnRenameRemove;
        internal CheckedListBox clbRename;
        private IContainer components;
        private Label label80;
        private Label label81;
        private Label label82;
        private TextBox tbRenameAfter;
        private TextBox tbRenameBefore;

        public Options_DataCorrectionRename()
        {
            this.InitializeComponent();
        }

        private void btnRenameAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbRenameBefore.Text) || string.IsNullOrEmpty(this.tbRenameAfter.Text))
            {
                MessageBox.Show("Please fill in both the Before and After fields.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                FormActMain.RenameFix newItem = new FormActMain.RenameFix(this.tbRenameBefore.Text, this.tbRenameAfter.Text);
                ActGlobals.oFormActMain.RenameAddFix(newItem);
            }
        }

        private void btnRenameRemove_Click(object sender, EventArgs e)
        {
            string key = this.tbRenameBefore.Text + " -> " + this.tbRenameAfter.Text;
            if (ActGlobals.oFormActMain.renameList.ContainsKey(key))
            {
                ActGlobals.oFormActMain.renameList.Remove(key);
                this.clbRename.Items.Remove(key);
            }
        }

        private void clbRename_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string str = (string) this.clbRename.Items[e.Index];
            FormActMain.RenameFix fix = ActGlobals.oFormActMain.renameList[str];
            fix.Active = e.NewValue == CheckState.Checked;
        }

        private void clbRename_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.clbRename.SelectedIndex != -1)
            {
                string str = (string) this.clbRename.Items[this.clbRename.SelectedIndex];
                FormActMain.RenameFix fix = ActGlobals.oFormActMain.renameList[str];
                this.tbRenameBefore.Text = fix.Before;
                this.tbRenameAfter.Text = fix.After;
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
            this.clbRename = new CheckedListBox();
            this.tbRenameBefore = new TextBox();
            this.tbRenameAfter = new TextBox();
            this.btnRenameAdd = new Button();
            this.btnRenameRemove = new Button();
            this.label81 = new Label();
            this.label82 = new Label();
            this.label80 = new Label();
            base.SuspendLayout();
            this.clbRename.FormattingEnabled = true;
            this.clbRename.IntegralHeight = false;
            this.clbRename.Location = new Point(3, 3);
            this.clbRename.Name = "clbRename";
            this.clbRename.Size = new Size(0x116, 0xc3);
            this.clbRename.Sorted = true;
            this.clbRename.TabIndex = 12;
            this.clbRename.ItemCheck += new ItemCheckEventHandler(this.clbRename_ItemCheck);
            this.clbRename.SelectedIndexChanged += new EventHandler(this.clbRename_SelectedIndexChanged);
            this.tbRenameBefore.Location = new Point(290, 0x13);
            this.tbRenameBefore.Name = "tbRenameBefore";
            this.tbRenameBefore.Size = new Size(0x15f, 20);
            this.tbRenameBefore.TabIndex = 15;
            this.tbRenameAfter.Location = new Point(290, 0x2d);
            this.tbRenameAfter.Name = "tbRenameAfter";
            this.tbRenameAfter.Size = new Size(0x15f, 20);
            this.tbRenameAfter.TabIndex = 0x10;
            this.btnRenameAdd.Location = new Point(0x22f, 0x47);
            this.btnRenameAdd.Name = "btnRenameAdd";
            this.btnRenameAdd.Size = new Size(0x7e, 0x17);
            this.btnRenameAdd.TabIndex = 0x11;
            this.btnRenameAdd.Text = "Add Correction";
            this.btnRenameAdd.UseVisualStyleBackColor = true;
            this.btnRenameAdd.Click += new EventHandler(this.btnRenameAdd_Click);
            this.btnRenameRemove.Location = new Point(0x22f, 0x60);
            this.btnRenameRemove.Name = "btnRenameRemove";
            this.btnRenameRemove.Size = new Size(0x7e, 0x17);
            this.btnRenameRemove.TabIndex = 0x12;
            this.btnRenameRemove.Text = "Remove Correction";
            this.btnRenameRemove.UseVisualStyleBackColor = true;
            this.btnRenameRemove.Click += new EventHandler(this.btnRenameRemove_Click);
            this.label81.AutoSize = true;
            this.label81.Location = new Point(0x287, 0x16);
            this.label81.Name = "label81";
            this.label81.Size = new Size(0x26, 13);
            this.label81.TabIndex = 14;
            this.label81.Text = "Before";
            this.label82.AutoSize = true;
            this.label82.Location = new Point(0x287, 0x30);
            this.label82.Name = "label82";
            this.label82.Size = new Size(0x1d, 13);
            this.label82.TabIndex = 13;
            this.label82.Text = "After";
            this.label80.AutoSize = true;
            this.label80.Location = new Point(0x11f, 3);
            this.label80.Name = "label80";
            this.label80.Size = new Size(0x126, 13);
            this.label80.TabIndex = 11;
            this.label80.Text = "Rename a combatant to a new one, or merge into an old one";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.label82);
            base.Controls.Add(this.label81);
            base.Controls.Add(this.clbRename);
            base.Controls.Add(this.tbRenameBefore);
            base.Controls.Add(this.tbRenameAfter);
            base.Controls.Add(this.btnRenameAdd);
            base.Controls.Add(this.btnRenameRemove);
            base.Controls.Add(this.label80);
            base.Name = "Options_DataCorrectionRename";
            base.Size = new Size(0x2b0, 0xc9);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

