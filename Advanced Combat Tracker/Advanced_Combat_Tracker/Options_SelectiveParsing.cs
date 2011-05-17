namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_SelectiveParsing : UserControl
    {
        private Button btnSParseAddPlayer;
        private Button btnSParseClear;
        private Button btnSParseCull;
        private Button btnSParseRemovePlayer;
        private Button btnSParseUncheckAll;
        internal CheckBox cbSParseExportIgnoreOtherAllies;
        internal CheckBox cbSParseIgnoreEnemies;
        internal CheckedListBox clbSParseCombatants;
        private IContainer components;
        private GroupBox groupBox17;
        private GroupBox groupBox18;
        private GroupBox groupBox19;
        internal RadioButton rbSParseExport;
        internal RadioButton rbSParseFull;
        internal RadioButton rbSParseNone;
        private TextBox tbSParsePlayer;

        public Options_SelectiveParsing()
        {
            this.InitializeComponent();
        }

        private void btnSParseAddPlayer_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.SelectiveListAdd(this.tbSParsePlayer.Text);
        }

        private void btnSParseClear_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.SelectiveListClear();
        }

        private void btnSParseCull_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, bool> pair in ActGlobals.selectiveList)
            {
                if (!pair.Value)
                {
                    list.Add(pair.Key);
                }
            }
            foreach (string str in list)
            {
                ActGlobals.selectiveList.Remove(str);
            }
            this.SelectiveListUpdate();
        }

        private void btnSParseRemovePlayer_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.SelectiveListRemove(this.tbSParsePlayer.Text, true);
        }

        private void btnSParseUncheckAll_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.SelectiveListUncheckAll();
        }

        private void clbSParseCombatants_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string str = (string) this.clbSParseCombatants.Items[e.Index];
            ActGlobals.selectiveList[str.ToUpper()] = e.NewValue == CheckState.Checked;
        }

        private void clbSParseCombatants_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.clbSParseCombatants.SelectedIndex != -1)
            {
                this.tbSParsePlayer.Text = (string) this.clbSParseCombatants.Items[this.clbSParseCombatants.SelectedIndex];
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
            this.groupBox19 = new GroupBox();
            this.btnSParseClear = new Button();
            this.btnSParseCull = new Button();
            this.btnSParseUncheckAll = new Button();
            this.groupBox18 = new GroupBox();
            this.btnSParseAddPlayer = new Button();
            this.tbSParsePlayer = new TextBox();
            this.btnSParseRemovePlayer = new Button();
            this.clbSParseCombatants = new CheckedListBox();
            this.groupBox17 = new GroupBox();
            this.cbSParseIgnoreEnemies = new CheckBox();
            this.rbSParseNone = new RadioButton();
            this.rbSParseExport = new RadioButton();
            this.rbSParseFull = new RadioButton();
            this.cbSParseExportIgnoreOtherAllies = new CheckBox();
            this.groupBox19.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox17.SuspendLayout();
            base.SuspendLayout();
            this.groupBox19.Controls.Add(this.btnSParseClear);
            this.groupBox19.Controls.Add(this.btnSParseCull);
            this.groupBox19.Controls.Add(this.btnSParseUncheckAll);
            this.groupBox19.Location = new Point(0xb1, 0xac);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new Size(0x148, 0x30);
            this.groupBox19.TabIndex = 6;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Batch Remove Players";
            this.btnSParseClear.Location = new Point(8, 0x10);
            this.btnSParseClear.Name = "btnSParseClear";
            this.btnSParseClear.Size = new Size(0x68, 0x17);
            this.btnSParseClear.TabIndex = 0;
            this.btnSParseClear.Text = "Clear Entire List";
            this.btnSParseClear.Click += new EventHandler(this.btnSParseClear_Click);
            this.btnSParseClear.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnSParseCull.Location = new Point(0xd8, 0x10);
            this.btnSParseCull.Name = "btnSParseCull";
            this.btnSParseCull.Size = new Size(0x68, 0x17);
            this.btnSParseCull.TabIndex = 2;
            this.btnSParseCull.Text = "Clear Unchecked";
            this.btnSParseCull.Click += new EventHandler(this.btnSParseCull_Click);
            this.btnSParseCull.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnSParseUncheckAll.Location = new Point(0x70, 0x10);
            this.btnSParseUncheckAll.Name = "btnSParseUncheckAll";
            this.btnSParseUncheckAll.Size = new Size(0x68, 0x17);
            this.btnSParseUncheckAll.TabIndex = 1;
            this.btnSParseUncheckAll.Text = "Uncheck All";
            this.btnSParseUncheckAll.Click += new EventHandler(this.btnSParseUncheckAll_Click);
            this.btnSParseUncheckAll.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox18.Controls.Add(this.btnSParseAddPlayer);
            this.groupBox18.Controls.Add(this.tbSParsePlayer);
            this.groupBox18.Controls.Add(this.btnSParseRemovePlayer);
            this.groupBox18.Location = new Point(0xb1, 0x77);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new Size(0x148, 0x2f);
            this.groupBox18.TabIndex = 5;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Add/Remove Players";
            this.btnSParseAddPlayer.Location = new Point(0xe0, 0x10);
            this.btnSParseAddPlayer.Name = "btnSParseAddPlayer";
            this.btnSParseAddPlayer.Size = new Size(40, 0x15);
            this.btnSParseAddPlayer.TabIndex = 1;
            this.btnSParseAddPlayer.Text = "&Add";
            this.btnSParseAddPlayer.Click += new EventHandler(this.btnSParseAddPlayer_Click);
            this.btnSParseAddPlayer.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbSParsePlayer.Location = new Point(8, 0x10);
            this.tbSParsePlayer.Name = "tbSParsePlayer";
            this.tbSParsePlayer.Size = new Size(0xd0, 20);
            this.tbSParsePlayer.TabIndex = 0;
            this.tbSParsePlayer.Text = "Player";
            this.tbSParsePlayer.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnSParseRemovePlayer.Location = new Point(0x108, 0x10);
            this.btnSParseRemovePlayer.Name = "btnSParseRemovePlayer";
            this.btnSParseRemovePlayer.Size = new Size(0x38, 0x15);
            this.btnSParseRemovePlayer.TabIndex = 2;
            this.btnSParseRemovePlayer.Text = "&Remove";
            this.btnSParseRemovePlayer.Click += new EventHandler(this.btnSParseRemovePlayer_Click);
            this.btnSParseRemovePlayer.MouseHover += new EventHandler(this.control_MouseHover);
            this.clbSParseCombatants.IntegralHeight = false;
            this.clbSParseCombatants.Location = new Point(3, 3);
            this.clbSParseCombatants.Name = "clbSParseCombatants";
            this.clbSParseCombatants.Size = new Size(0xa8, 0x1b1);
            this.clbSParseCombatants.Sorted = true;
            this.clbSParseCombatants.TabIndex = 7;
            this.clbSParseCombatants.ItemCheck += new ItemCheckEventHandler(this.clbSParseCombatants_ItemCheck);
            this.clbSParseCombatants.SelectedIndexChanged += new EventHandler(this.clbSParseCombatants_SelectedIndexChanged);
            this.clbSParseCombatants.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox17.Controls.Add(this.cbSParseIgnoreEnemies);
            this.groupBox17.Controls.Add(this.rbSParseNone);
            this.groupBox17.Controls.Add(this.rbSParseExport);
            this.groupBox17.Controls.Add(this.rbSParseFull);
            this.groupBox17.Controls.Add(this.cbSParseExportIgnoreOtherAllies);
            this.groupBox17.Location = new Point(0xb1, 3);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new Size(0x148, 0x6c);
            this.groupBox17.TabIndex = 4;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Combatant Limiting";
            this.cbSParseIgnoreEnemies.AutoSize = true;
            this.cbSParseIgnoreEnemies.Location = new Point(0x18, 0x54);
            this.cbSParseIgnoreEnemies.Name = "cbSParseIgnoreEnemies";
            this.cbSParseIgnoreEnemies.Size = new Size(0xd6, 0x11);
            this.cbSParseIgnoreEnemies.TabIndex = 4;
            this.cbSParseIgnoreEnemies.Text = "Never show non-whitelisted combatants";
            this.cbSParseIgnoreEnemies.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSParseNone.AutoSize = true;
            this.rbSParseNone.Checked = true;
            this.rbSParseNone.Location = new Point(8, 0x10);
            this.rbSParseNone.Name = "rbSParseNone";
            this.rbSParseNone.Size = new Size(280, 0x11);
            this.rbSParseNone.TabIndex = 0;
            this.rbSParseNone.TabStop = true;
            this.rbSParseNone.Text = "No selective parsing (Rely on automatic ally detection)";
            this.rbSParseNone.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSParseExport.AutoSize = true;
            this.rbSParseExport.Location = new Point(8, 0x21);
            this.rbSParseExport.Name = "rbSParseExport";
            this.rbSParseExport.Size = new Size(0x108, 0x11);
            this.rbSParseExport.TabIndex = 1;
            this.rbSParseExport.Text = "Show only listed combatants in graphs/text exports";
            this.rbSParseExport.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSParseFull.AutoSize = true;
            this.rbSParseFull.Location = new Point(8, 0x43);
            this.rbSParseFull.Name = "rbSParseFull";
            this.rbSParseFull.Size = new Size(0x108, 0x11);
            this.rbSParseFull.TabIndex = 3;
            this.rbSParseFull.Text = "Only record combat actions taken by/against listed";
            this.rbSParseFull.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbSParseExportIgnoreOtherAllies.AutoSize = true;
            this.cbSParseExportIgnoreOtherAllies.Location = new Point(0x18, 50);
            this.cbSParseExportIgnoreOtherAllies.Name = "cbSParseExportIgnoreOtherAllies";
            this.cbSParseExportIgnoreOtherAllies.Size = new Size(0xe1, 0x11);
            this.cbSParseExportIgnoreOtherAllies.TabIndex = 2;
            this.cbSParseExportIgnoreOtherAllies.Text = "In \"allied\" totals, only include listed players";
            this.cbSParseExportIgnoreOtherAllies.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.groupBox19);
            base.Controls.Add(this.groupBox18);
            base.Controls.Add(this.clbSParseCombatants);
            base.Controls.Add(this.groupBox17);
            base.Name = "Options_SelectiveParsing";
            base.Size = new Size(0x1fc, 0x1b7);
            this.groupBox19.ResumeLayout(false);
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            base.ResumeLayout(false);
        }

        internal void SelectiveListUpdate()
        {
            List<string> list = new List<string>();
            List<bool> list2 = new List<bool>();
            foreach (KeyValuePair<string, bool> pair in ActGlobals.selectiveList)
            {
                list.Add(pair.Key);
                list2.Add(pair.Value);
            }
            this.clbSParseCombatants.BeginUpdate();
            this.clbSParseCombatants.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                this.clbSParseCombatants.Items.Add(ActGlobals.oFormActMain.StrCapitalize(list[i]), list2[i]);
            }
            this.clbSParseCombatants.EndUpdate();
        }
    }
}

