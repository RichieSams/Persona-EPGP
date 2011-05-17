namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Media;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    internal class Options_XmlShare : UserControl
    {
        private Button btnShareAddBanned;
        private Button btnShareAddTrusted;
        private Button btnShareDelete;
        private Button btnShareImport;
        private Button btnShareRemoveBanned;
        private Button btnShareRemoveTrusted;
        internal CheckedListBox clbShareBanned;
        internal CheckedListBox clbShareTrusted;
        private IContainer components;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        internal ListBox lbShareIncoming;
        private Regex shareRegex = new Regex("(?<xml><(?<type>Spell|Trigger) .+/>)", RegexOptions.Compiled);
        private TableLayoutPanel tableLayoutPanel2;
        internal TextBox tbShareIncomingPreview;
        internal TextBox tbSharePlayerName;
        private ToolTip toolTip1;

        public Options_XmlShare()
        {
            this.InitializeComponent();
        }

        private void btnShareAddBanned_Click(object sender, EventArgs e)
        {
            if (this.tbSharePlayerName.Text.Length == 0)
            {
                this.tbSharePlayerName.BackColor = Color.LightCoral;
                Application.DoEvents();
                Thread.Sleep(100);
                this.tbSharePlayerName.BackColor = SystemColors.Window;
                this.toolTip1.Show("A name must be entered", this.tbSharePlayerName, 0xbb8);
            }
            else
            {
                char[] chArray = this.tbSharePlayerName.Text.ToCharArray();
                chArray[0] = char.ToUpper(chArray[0]);
                string str = new string(chArray);
                if (!this.clbShareBanned.Items.Contains(str))
                {
                    this.clbShareBanned.Items.Add(str);
                }
                int index = this.clbShareBanned.Items.IndexOf(str);
                this.clbShareBanned.SetItemChecked(index, true);
            }
        }

        private void btnShareAddTrusted_Click(object sender, EventArgs e)
        {
            if (this.tbSharePlayerName.Text.Length == 0)
            {
                this.tbSharePlayerName.BackColor = Color.LightCoral;
                Application.DoEvents();
                Thread.Sleep(100);
                this.tbSharePlayerName.BackColor = SystemColors.Window;
                this.toolTip1.Show("A name must be entered", this.tbSharePlayerName, 0xbb8);
            }
            else
            {
                char[] chArray = this.tbSharePlayerName.Text.ToCharArray();
                chArray[0] = char.ToUpper(chArray[0]);
                string str = new string(chArray);
                if (!this.clbShareTrusted.Items.Contains(str))
                {
                    this.clbShareTrusted.Items.Add(str);
                }
                int index = this.clbShareTrusted.Items.IndexOf(str);
                this.clbShareTrusted.SetItemChecked(index, true);
            }
        }

        private void btnShareDelete_Click(object sender, EventArgs e)
        {
            if (this.lbShareIncoming.SelectedIndex != -1)
            {
                this.lbShareIncoming.Items.RemoveAt(this.lbShareIncoming.SelectedIndex);
            }
        }

        private void btnShareImport_Click(object sender, EventArgs e)
        {
            if (this.lbShareIncoming.SelectedIndex == -1)
            {
                return;
            }
            ChatShareEntry entry = (ChatShareEntry) this.lbShareIncoming.Items[this.lbShareIncoming.SelectedIndex];
            string type = entry.type;
            if (type == null)
            {
                goto Label_012D;
            }
            if (!(type == "Spell"))
            {
                if (type == "Trigger")
                {
                    goto Label_00CE;
                }
                goto Label_012D;
            }
            try
            {
                TimerData newTd = ActGlobals.oFormActMain.ShareXmlToSpell(entry.data);
                ActGlobals.oFormSpellTimers.AddEditTimerDef(newTd);
                ActGlobals.oFormSpellTimers.RebuildSpellTreeView();
                goto Label_012D;
            }
            catch (NullReferenceException)
            {
                this.toolTip1.Show("The shared XML seems to be missing required attributes to be imported", this.btnShareImport, 0x1388);
                SystemSounds.Exclamation.Play();
                return;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error adding Spell", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
        Label_00CE:
            try
            {
                CustomTrigger cT = ActGlobals.oFormActMain.ShareXmlToCustomTrigger(entry.data);
                ActGlobals.oFormActMain.AddEditCustomTrigger(cT);
            }
            catch (NullReferenceException)
            {
                this.toolTip1.Show("The shared XML seems to be missing required attributes to be imported", this.btnShareImport, 0x1388);
                SystemSounds.Exclamation.Play();
                return;
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, "Error adding Trigger", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
        Label_012D:
            this.lbShareIncoming.Items.RemoveAt(this.lbShareIncoming.SelectedIndex);
        }

        private void btnShareRemoveBanned_Click(object sender, EventArgs e)
        {
            if (this.tbSharePlayerName.Text.Length == 0)
            {
                this.tbSharePlayerName.BackColor = Color.LightCoral;
                Application.DoEvents();
                Thread.Sleep(100);
                this.tbSharePlayerName.BackColor = SystemColors.Window;
                this.toolTip1.Show("A name must be entered", this.tbSharePlayerName, 0xbb8);
            }
            else
            {
                char[] chArray = this.tbSharePlayerName.Text.ToCharArray();
                chArray[0] = char.ToUpper(chArray[0]);
                string str = new string(chArray);
                if (this.clbShareBanned.Items.Contains(str))
                {
                    this.clbShareBanned.Items.Remove(str);
                }
            }
        }

        private void btnShareRemoveTrusted_Click(object sender, EventArgs e)
        {
            if (this.tbSharePlayerName.Text.Length == 0)
            {
                this.tbSharePlayerName.BackColor = Color.LightCoral;
                Application.DoEvents();
                Thread.Sleep(100);
                this.tbSharePlayerName.BackColor = SystemColors.Window;
                this.toolTip1.Show("A name must be entered", this.tbSharePlayerName, 0xbb8);
            }
            else
            {
                char[] chArray = this.tbSharePlayerName.Text.ToCharArray();
                chArray[0] = char.ToUpper(chArray[0]);
                string str = new string(chArray);
                if (this.clbShareTrusted.Items.Contains(str))
                {
                    this.clbShareTrusted.Items.Remove(str);
                }
            }
        }

        private void clbShareBanned_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.clbShareBanned.SelectedIndex != -1)
            {
                string str = (string) this.clbShareBanned.Items[this.clbShareBanned.SelectedIndex];
                this.tbSharePlayerName.Text = str;
            }
        }

        private void clbShareTrusted_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.clbShareTrusted.SelectedIndex != -1)
            {
                string str = (string) this.clbShareTrusted.Items[this.clbShareTrusted.SelectedIndex];
                this.tbSharePlayerName.Text = str;
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
            this.components = new Container();
            this.clbShareTrusted = new CheckedListBox();
            this.lbShareIncoming = new ListBox();
            this.groupBox4 = new GroupBox();
            this.tableLayoutPanel2 = new TableLayoutPanel();
            this.btnShareDelete = new Button();
            this.btnShareRemoveBanned = new Button();
            this.btnShareAddBanned = new Button();
            this.btnShareRemoveTrusted = new Button();
            this.tbShareIncomingPreview = new TextBox();
            this.groupBox5 = new GroupBox();
            this.tbSharePlayerName = new TextBox();
            this.groupBox6 = new GroupBox();
            this.clbShareBanned = new CheckedListBox();
            this.btnShareAddTrusted = new Button();
            this.btnShareImport = new Button();
            this.toolTip1 = new ToolTip(this.components);
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            base.SuspendLayout();
            this.clbShareTrusted.BackColor = Color.Honeydew;
            this.clbShareTrusted.Dock = DockStyle.Fill;
            this.clbShareTrusted.ForeColor = Color.Black;
            this.clbShareTrusted.FormattingEnabled = true;
            this.clbShareTrusted.IntegralHeight = false;
            this.clbShareTrusted.Location = new Point(3, 0x10);
            this.clbShareTrusted.Name = "clbShareTrusted";
            this.clbShareTrusted.Size = new Size(0x11a, 0x61);
            this.clbShareTrusted.Sorted = true;
            this.clbShareTrusted.TabIndex = 0;
            this.clbShareTrusted.SelectedIndexChanged += new EventHandler(this.clbShareTrusted_SelectedIndexChanged);
            this.clbShareTrusted.MouseHover += new EventHandler(this.xmlShare_MouseHover);
            this.lbShareIncoming.BackColor = Color.LemonChiffon;
            this.tableLayoutPanel2.SetColumnSpan(this.lbShareIncoming, 2);
            this.lbShareIncoming.Dock = DockStyle.Fill;
            this.lbShareIncoming.FormattingEnabled = true;
            this.lbShareIncoming.IntegralHeight = false;
            this.lbShareIncoming.Location = new Point(3, 0xb1);
            this.lbShareIncoming.Name = "lbShareIncoming";
            this.tableLayoutPanel2.SetRowSpan(this.lbShareIncoming, 2);
            this.lbShareIncoming.Size = new Size(0x120, 0x7b);
            this.lbShareIncoming.TabIndex = 7;
            this.lbShareIncoming.SelectedIndexChanged += new EventHandler(this.lbShareIncoming_SelectedIndexChanged);
            this.lbShareIncoming.MouseHover += new EventHandler(this.xmlShare_MouseHover);
            this.groupBox4.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox4.Controls.Add(this.tableLayoutPanel2);
            this.groupBox4.Location = new Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x254, 0x142);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Incoming XML Shares";
            this.groupBox4.MouseHover += new EventHandler(this.xmlShare_MouseHover);
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            this.tableLayoutPanel2.Controls.Add(this.btnShareDelete, 3, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnShareRemoveBanned, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnShareAddBanned, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnShareRemoveTrusted, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbShareIncomingPreview, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.groupBox5, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbSharePlayerName, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbShareIncoming, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.groupBox6, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnShareAddTrusted, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnShareImport, 2, 4);
            this.tableLayoutPanel2.Dock = DockStyle.Fill;
            this.tableLayoutPanel2.Location = new Point(3, 0x10);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 26f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 26f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 102f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 27f));
            this.tableLayoutPanel2.Size = new Size(590, 0x12f);
            this.tableLayoutPanel2.TabIndex = 7;
            this.btnShareDelete.Dock = DockStyle.Fill;
            this.btnShareDelete.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnShareDelete.Location = new Point(0x1bc, 0x117);
            this.btnShareDelete.Name = "btnShareDelete";
            this.btnShareDelete.Size = new Size(0x8f, 0x15);
            this.btnShareDelete.TabIndex = 10;
            this.btnShareDelete.Text = "Delete Above Data";
            this.btnShareDelete.UseVisualStyleBackColor = true;
            this.btnShareDelete.Click += new EventHandler(this.btnShareDelete_Click);
            this.btnShareDelete.MouseHover += new EventHandler(this.xmlShare_MouseHover);
            this.btnShareRemoveBanned.Dock = DockStyle.Fill;
            this.btnShareRemoveBanned.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnShareRemoveBanned.Location = new Point(0x1bc, 0x7d);
            this.btnShareRemoveBanned.Name = "btnShareRemoveBanned";
            this.btnShareRemoveBanned.Size = new Size(0x8f, 20);
            this.btnShareRemoveBanned.TabIndex = 5;
            this.btnShareRemoveBanned.Text = "Remove";
            this.btnShareRemoveBanned.UseVisualStyleBackColor = true;
            this.btnShareRemoveBanned.Click += new EventHandler(this.btnShareRemoveBanned_Click);
            this.btnShareRemoveBanned.MouseHover += new EventHandler(this.xmlShare_MouseHover);
            this.btnShareAddBanned.Dock = DockStyle.Fill;
            this.btnShareAddBanned.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnShareAddBanned.Location = new Point(0x129, 0x7d);
            this.btnShareAddBanned.Name = "btnShareAddBanned";
            this.btnShareAddBanned.Size = new Size(0x8d, 20);
            this.btnShareAddBanned.TabIndex = 4;
            this.btnShareAddBanned.Text = "Add";
            this.btnShareAddBanned.UseVisualStyleBackColor = true;
            this.btnShareAddBanned.Click += new EventHandler(this.btnShareAddBanned_Click);
            this.btnShareAddBanned.MouseHover += new EventHandler(this.xmlShare_MouseHover);
            this.btnShareRemoveTrusted.Dock = DockStyle.Fill;
            this.btnShareRemoveTrusted.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnShareRemoveTrusted.Location = new Point(150, 0x7d);
            this.btnShareRemoveTrusted.Name = "btnShareRemoveTrusted";
            this.btnShareRemoveTrusted.Size = new Size(0x8d, 20);
            this.btnShareRemoveTrusted.TabIndex = 3;
            this.btnShareRemoveTrusted.Text = "Remove";
            this.btnShareRemoveTrusted.UseVisualStyleBackColor = true;
            this.btnShareRemoveTrusted.Click += new EventHandler(this.btnShareRemoveTrusted_Click);
            this.btnShareRemoveTrusted.MouseHover += new EventHandler(this.xmlShare_MouseHover);
            this.tableLayoutPanel2.SetColumnSpan(this.tbShareIncomingPreview, 2);
            this.tbShareIncomingPreview.Dock = DockStyle.Fill;
            this.tbShareIncomingPreview.Font = new Font("Lucida Console", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.tbShareIncomingPreview.Location = new Point(0x129, 0x97);
            this.tbShareIncomingPreview.Multiline = true;
            this.tbShareIncomingPreview.Name = "tbShareIncomingPreview";
            this.tableLayoutPanel2.SetRowSpan(this.tbShareIncomingPreview, 2);
            this.tbShareIncomingPreview.Size = new Size(290, 0x7a);
            this.tbShareIncomingPreview.TabIndex = 8;
            this.tbShareIncomingPreview.TextChanged += new EventHandler(this.tbShareIncomingPreview_TextChanged);
            this.tbShareIncomingPreview.Enter += new EventHandler(this.tbShareIncomingPreview_Enter);
            this.tbShareIncomingPreview.MouseHover += new EventHandler(this.xmlShare_MouseHover);
            this.tableLayoutPanel2.SetColumnSpan(this.groupBox5, 2);
            this.groupBox5.Controls.Add(this.clbShareTrusted);
            this.groupBox5.Dock = DockStyle.Fill;
            this.groupBox5.Location = new Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x120, 0x74);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Automatically accept data from:";
            this.groupBox5.MouseHover += new EventHandler(this.xmlShare_MouseHover);
            this.tableLayoutPanel2.SetColumnSpan(this.tbSharePlayerName, 2);
            this.tbSharePlayerName.Dock = DockStyle.Fill;
            this.tbSharePlayerName.Location = new Point(3, 0x97);
            this.tbSharePlayerName.Name = "tbSharePlayerName";
            this.tbSharePlayerName.Size = new Size(0x120, 20);
            this.tbSharePlayerName.TabIndex = 6;
            this.tbSharePlayerName.MouseHover += new EventHandler(this.xmlShare_MouseHover);
            this.tableLayoutPanel2.SetColumnSpan(this.groupBox6, 2);
            this.groupBox6.Controls.Add(this.clbShareBanned);
            this.groupBox6.Dock = DockStyle.Fill;
            this.groupBox6.Location = new Point(0x129, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(290, 0x74);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Automatically reject data from:";
            this.groupBox6.MouseHover += new EventHandler(this.xmlShare_MouseHover);
            this.clbShareBanned.BackColor = Color.MistyRose;
            this.clbShareBanned.Dock = DockStyle.Fill;
            this.clbShareBanned.ForeColor = Color.Black;
            this.clbShareBanned.FormattingEnabled = true;
            this.clbShareBanned.IntegralHeight = false;
            this.clbShareBanned.Location = new Point(3, 0x10);
            this.clbShareBanned.Name = "clbShareBanned";
            this.clbShareBanned.Size = new Size(0x11c, 0x61);
            this.clbShareBanned.Sorted = true;
            this.clbShareBanned.TabIndex = 0;
            this.clbShareBanned.SelectedIndexChanged += new EventHandler(this.clbShareBanned_SelectedIndexChanged);
            this.clbShareBanned.MouseHover += new EventHandler(this.xmlShare_MouseHover);
            this.btnShareAddTrusted.Dock = DockStyle.Fill;
            this.btnShareAddTrusted.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnShareAddTrusted.Location = new Point(3, 0x7d);
            this.btnShareAddTrusted.Name = "btnShareAddTrusted";
            this.btnShareAddTrusted.Size = new Size(0x8d, 20);
            this.btnShareAddTrusted.TabIndex = 2;
            this.btnShareAddTrusted.Text = "Add";
            this.btnShareAddTrusted.UseVisualStyleBackColor = true;
            this.btnShareAddTrusted.Click += new EventHandler(this.btnShareAddTrusted_Click);
            this.btnShareAddTrusted.MouseHover += new EventHandler(this.xmlShare_MouseHover);
            this.btnShareImport.Dock = DockStyle.Fill;
            this.btnShareImport.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnShareImport.Location = new Point(0x129, 0x117);
            this.btnShareImport.Name = "btnShareImport";
            this.btnShareImport.Size = new Size(0x8d, 0x15);
            this.btnShareImport.TabIndex = 9;
            this.btnShareImport.Text = "Import Above Data";
            this.btnShareImport.UseVisualStyleBackColor = true;
            this.btnShareImport.Click += new EventHandler(this.btnShareImport_Click);
            this.btnShareImport.MouseHover += new EventHandler(this.xmlShare_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.groupBox4);
            base.Name = "Options_XmlShare";
            base.Size = new Size(600, 0x148);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void lbShareIncoming_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lbShareIncoming.SelectedIndex == -1)
            {
                this.tbSharePlayerName.Text = string.Empty;
                this.tbShareIncomingPreview.Text = string.Empty;
            }
            else
            {
                this.tbShareIncomingPreview.TextChanged -= new EventHandler(this.tbShareIncomingPreview_TextChanged);
                ChatShareEntry entry = (ChatShareEntry) this.lbShareIncoming.Items[this.lbShareIncoming.SelectedIndex];
                this.tbSharePlayerName.Text = entry.character;
                string type = entry.type;
                if (type != null)
                {
                    if (!(type == "Spell"))
                    {
                        if (type == "Trigger")
                        {
                            try
                            {
                                CustomTrigger trigger = ActGlobals.oFormActMain.ShareXmlToCustomTrigger(entry.data);
                                this.tbShareIncomingPreview.Text = string.Format("Custom Trigger:\r\n{0}\r\nCategory: {1}\r\nTimer/Tab Name: {2}", trigger.ShortRegexString, trigger.Category, trigger.TimerName);
                            }
                            catch (NullReferenceException)
                            {
                                this.toolTip1.Show("The shared XML seems to be missing required attributes to be imported", this.btnShareImport, 0x1388);
                                SystemSounds.Exclamation.Play();
                            }
                            catch (Exception exception2)
                            {
                                MessageBox.Show(exception2.Message, "Error parsing Trigger", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            TimerData data = ActGlobals.oFormActMain.ShareXmlToSpell(entry.data);
                            this.tbShareIncomingPreview.Text = string.Format("Spell: {0}\r\nDelay: {1}s\r\nCategory: {2}\r\nTooltip: {3}", new object[] { data.Name, data.TimerValue, data.Category, data.Tooltip });
                        }
                        catch (NullReferenceException)
                        {
                            this.toolTip1.Show("The shared XML seems to be missing required attributes to be imported", this.btnShareImport, 0x1388);
                            SystemSounds.Exclamation.Play();
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, "Error parsing Spell", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                    }
                }
                this.tbShareIncomingPreview.BackColor = SystemColors.Window;
                this.tbShareIncomingPreview.TextChanged += new EventHandler(this.tbShareIncomingPreview_TextChanged);
            }
        }

        internal void PromptStoredSnippets()
        {
            if (this.lbShareIncoming.Items.Count > 0)
            {
                List<string> values = new List<string>();
                for (int i = 0; i < this.lbShareIncoming.Items.Count; i++)
                {
                    ChatShareEntry entry = (ChatShareEntry) this.lbShareIncoming.Items[i];
                    if (!values.Contains(entry.character))
                    {
                        values.Add(entry.character);
                    }
                }
                if (MessageBox.Show(string.Format("There are currently {0} XML snippets stored from the following characters:\n{1}\n\nWould you like to import these now?", this.lbShareIncoming.Items.Count, string.Join(", ", values)), "Import XML Snippets?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int j = this.lbShareIncoming.Items.Count - 1; j >= 0; j--)
                    {
                        ChatShareEntry entry2 = (ChatShareEntry) this.lbShareIncoming.Items[j];
                        string type = entry2.type;
                        if (type == null)
                        {
                            goto Label_01C3;
                        }
                        if (!(type == "Spell"))
                        {
                            if (type == "Trigger")
                            {
                                goto Label_0169;
                            }
                            goto Label_01C3;
                        }
                        try
                        {
                            TimerData newTd = ActGlobals.oFormActMain.ShareXmlToSpell(entry2.data);
                            ActGlobals.oFormSpellTimers.AddEditTimerDef(newTd);
                            ActGlobals.oFormSpellTimers.RebuildSpellTreeView();
                            goto Label_01C3;
                        }
                        catch (NullReferenceException)
                        {
                            MessageBox.Show("The shared XML seems to be missing required attributes to be imported", "Error adding Spell", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            break;
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, "Error adding Spell", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            break;
                        }
                    Label_0169:
                        try
                        {
                            CustomTrigger cT = ActGlobals.oFormActMain.ShareXmlToCustomTrigger(entry2.data);
                            ActGlobals.oFormActMain.AddEditCustomTrigger(cT);
                        }
                        catch (NullReferenceException)
                        {
                            MessageBox.Show("The shared XML seems to be missing required attributes to be imported", "Error adding Trigger", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            SystemSounds.Exclamation.Play();
                            break;
                        }
                        catch (Exception exception2)
                        {
                            MessageBox.Show(exception2.Message, "Error adding Trigger", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            break;
                        }
                    Label_01C3:
                        this.lbShareIncoming.Items.RemoveAt(j);
                    }
                }
            }
        }

        private void tbShareIncomingPreview_Enter(object sender, EventArgs e)
        {
            if (this.lbShareIncoming.SelectedIndex != -1)
            {
                ChatShareEntry entry = (ChatShareEntry) this.lbShareIncoming.Items[this.lbShareIncoming.SelectedIndex];
                this.tbShareIncomingPreview.Text = entry.data;
            }
        }

        private void tbShareIncomingPreview_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbShareIncomingPreview.Text))
            {
                this.tbShareIncomingPreview.BackColor = SystemColors.Window;
            }
            else
            {
                try
                {
                    if (this.shareRegex.IsMatch(this.tbShareIncomingPreview.Text))
                    {
                        new XmlDocument().LoadXml(this.tbShareIncomingPreview.Text);
                        this.tbShareIncomingPreview.BackColor = Color.Honeydew;
                        string type = this.shareRegex.Replace(this.tbShareIncomingPreview.Text, "$2").Trim();
                        string data = this.shareRegex.Replace(this.tbShareIncomingPreview.Text, "$1").Trim();
                        ChatShareEntry entry = new ChatShareEntry("* Custom Entry *", type, data);
                        this.lbShareIncoming.SelectedIndexChanged -= new EventHandler(this.lbShareIncoming_SelectedIndexChanged);
                        int index = this.lbShareIncoming.Items.IndexOf(entry);
                        if (index != -1)
                        {
                            this.lbShareIncoming.Items.RemoveAt(index);
                        }
                        this.lbShareIncoming.Items.Insert(0, entry);
                        this.lbShareIncoming.SelectedIndex = 0;
                        this.lbShareIncoming.SelectedIndexChanged += new EventHandler(this.lbShareIncoming_SelectedIndexChanged);
                        this.toolTip1.Hide(this.btnShareImport);
                    }
                    else
                    {
                        this.toolTip1.Show("Invalid XML: Use this field to paste XML share text", this.btnShareImport, 0xbb8);
                        this.tbShareIncomingPreview.BackColor = Color.MistyRose;
                    }
                }
                catch
                {
                    this.toolTip1.Show("Invalid XML: Use this field to paste XML share text", this.btnShareImport, 0xbb8);
                    this.tbShareIncomingPreview.BackColor = Color.MistyRose;
                }
            }
        }

        private void xmlShare_MouseHover(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.lblHelpText.Text = ActGlobals.ActLocalization.LocalizationStrings["helpPanel-xmlShare"].DisplayedText;
        }
    }
}

