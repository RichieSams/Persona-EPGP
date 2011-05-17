namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_DataCorrectionRedirect : UserControl
    {
        private Button btnAbilityRedirectAdd;
        private Button btnAbilityRedirectRemove;
        internal CheckedListBox clbAbilityRedirect;
        private IContainer components;
        private ComboBox ddlAbilityRedirectType;
        private Label label95;
        private Label label96;
        private Label label97;
        private TextBox tbAbilityRedirectInto;
        private TextBox tbAbilityRedirectName;

        public Options_DataCorrectionRedirect()
        {
            this.InitializeComponent();
        }

        private void btnAbilityRedirectAdd_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(this.tbAbilityRedirectInto.Text) || string.IsNullOrEmpty(this.tbAbilityRedirectName.Text)) || (this.ddlAbilityRedirectType.SelectedIndex == -1))
            {
                MessageBox.Show("Please fill in both the Ability and Into fields and select an ability type.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                FormActMain.RedirectFix newItem = new FormActMain.RedirectFix(this.tbAbilityRedirectName.Text, this.tbAbilityRedirectInto.Text, this.ddlAbilityRedirectType.SelectedIndex);
                ActGlobals.oFormActMain.RedirectAddFix(newItem);
            }
        }

        private void btnAbilityRedirectRemove_Click(object sender, EventArgs e)
        {
            string key = string.Format("* ({1}) -> {0} ({1})", this.tbAbilityRedirectInto.Text, this.tbAbilityRedirectName.Text);
            if (ActGlobals.oFormActMain.redirectList.ContainsKey(key))
            {
                ActGlobals.oFormActMain.redirectList.Remove(key);
                this.clbAbilityRedirect.Items.Remove(key);
            }
        }

        private void clbAbilityRedirect_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string str = (string) this.clbAbilityRedirect.Items[e.Index];
            FormActMain.RedirectFix fix = ActGlobals.oFormActMain.redirectList[str];
            fix.Active = e.NewValue == CheckState.Checked;
        }

        private void clbAbilityRedirect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.clbAbilityRedirect.SelectedIndex != -1)
            {
                string str = (string) this.clbAbilityRedirect.Items[this.clbAbilityRedirect.SelectedIndex];
                FormActMain.RedirectFix fix = ActGlobals.oFormActMain.redirectList[str];
                this.tbAbilityRedirectInto.Text = fix.DestinationCombatant;
                this.tbAbilityRedirectName.Text = fix.Ability;
                this.ddlAbilityRedirectType.SelectedIndex = fix.Type;
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
            this.ddlAbilityRedirectType = new ComboBox();
            this.label97 = new Label();
            this.label96 = new Label();
            this.btnAbilityRedirectRemove = new Button();
            this.btnAbilityRedirectAdd = new Button();
            this.tbAbilityRedirectInto = new TextBox();
            this.tbAbilityRedirectName = new TextBox();
            this.clbAbilityRedirect = new CheckedListBox();
            this.label95 = new Label();
            base.SuspendLayout();
            this.ddlAbilityRedirectType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlAbilityRedirectType.FormattingEnabled = true;
            this.ddlAbilityRedirectType.Items.AddRange(new object[] { "Any", "Damage", "Healing", "Other (Non-Damage/Healing)" });
            this.ddlAbilityRedirectType.Location = new Point(290, 0x47);
            this.ddlAbilityRedirectType.Name = "ddlAbilityRedirectType";
            this.ddlAbilityRedirectType.Size = new Size(0xba, 0x15);
            this.ddlAbilityRedirectType.TabIndex = 0x19;
            this.label97.AutoSize = true;
            this.label97.Location = new Point(0x287, 0x30);
            this.label97.Name = "label97";
            this.label97.Size = new Size(0x19, 13);
            this.label97.TabIndex = 0x17;
            this.label97.Text = "Into";
            this.label96.AutoSize = true;
            this.label96.Location = new Point(0x287, 0x16);
            this.label96.Name = "label96";
            this.label96.Size = new Size(0x20, 13);
            this.label96.TabIndex = 0x18;
            this.label96.Text = "Abilty";
            this.btnAbilityRedirectRemove.Location = new Point(0x22f, 0x60);
            this.btnAbilityRedirectRemove.Name = "btnAbilityRedirectRemove";
            this.btnAbilityRedirectRemove.Size = new Size(0x7e, 0x17);
            this.btnAbilityRedirectRemove.TabIndex = 0x16;
            this.btnAbilityRedirectRemove.Text = "Remove Correction";
            this.btnAbilityRedirectRemove.UseVisualStyleBackColor = true;
            this.btnAbilityRedirectRemove.Click += new EventHandler(this.btnAbilityRedirectRemove_Click);
            this.btnAbilityRedirectAdd.Location = new Point(0x22f, 0x47);
            this.btnAbilityRedirectAdd.Name = "btnAbilityRedirectAdd";
            this.btnAbilityRedirectAdd.Size = new Size(0x7e, 0x17);
            this.btnAbilityRedirectAdd.TabIndex = 0x15;
            this.btnAbilityRedirectAdd.Text = "Add Correction";
            this.btnAbilityRedirectAdd.UseVisualStyleBackColor = true;
            this.btnAbilityRedirectAdd.Click += new EventHandler(this.btnAbilityRedirectAdd_Click);
            this.tbAbilityRedirectInto.Location = new Point(290, 0x2d);
            this.tbAbilityRedirectInto.Name = "tbAbilityRedirectInto";
            this.tbAbilityRedirectInto.Size = new Size(0x15f, 20);
            this.tbAbilityRedirectInto.TabIndex = 0x13;
            this.tbAbilityRedirectName.Location = new Point(290, 0x13);
            this.tbAbilityRedirectName.Name = "tbAbilityRedirectName";
            this.tbAbilityRedirectName.Size = new Size(0x15f, 20);
            this.tbAbilityRedirectName.TabIndex = 20;
            this.clbAbilityRedirect.FormattingEnabled = true;
            this.clbAbilityRedirect.IntegralHeight = false;
            this.clbAbilityRedirect.Location = new Point(3, 3);
            this.clbAbilityRedirect.Name = "clbAbilityRedirect";
            this.clbAbilityRedirect.Size = new Size(0x116, 0xc3);
            this.clbAbilityRedirect.TabIndex = 0x11;
            this.clbAbilityRedirect.ItemCheck += new ItemCheckEventHandler(this.clbAbilityRedirect_ItemCheck);
            this.clbAbilityRedirect.SelectedIndexChanged += new EventHandler(this.clbAbilityRedirect_SelectedIndexChanged);
            this.label95.AutoSize = true;
            this.label95.Location = new Point(0x11f, 3);
            this.label95.Name = "label95";
            this.label95.Size = new Size(0x13d, 13);
            this.label95.TabIndex = 0x12;
            this.label95.Text = "Redirect damage done by a specific ability name into a combatant";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.ddlAbilityRedirectType);
            base.Controls.Add(this.label97);
            base.Controls.Add(this.label96);
            base.Controls.Add(this.btnAbilityRedirectRemove);
            base.Controls.Add(this.btnAbilityRedirectAdd);
            base.Controls.Add(this.tbAbilityRedirectInto);
            base.Controls.Add(this.tbAbilityRedirectName);
            base.Controls.Add(this.clbAbilityRedirect);
            base.Controls.Add(this.label95);
            base.Name = "Options_DataCorrectionRedirect";
            base.Size = new Size(0x2b0, 0xc9);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

