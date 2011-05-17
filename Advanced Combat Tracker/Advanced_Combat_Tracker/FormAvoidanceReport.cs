namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Xml;

    public class FormAvoidanceReport : Form
    {
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem copyAsCSVToolStripMenuItem;
        private ToolStripMenuItem copyAsFormattedPlainTextToolStripMenuItem;
        private ToolStripMenuItem copyAsHTMLExtendedToolStripMenuItem;
        private ToolStripMenuItem copyAsHTMLToolStripMenuItem;
        private ToolStripMenuItem copyAsSimpleTextForEQ2PastingToolStripMenuItem;
        private ToolStripMenuItem copyAsSimpleTextNameZzHPSToolStripMenuItem;
        private ToolStripMenuItem copyAsTSVToolStripMenuItem;
        private bool encounterAvoidance = true;
        private ListView listView1;
        private ListView listView2;

        public FormAvoidanceReport()
        {
            this.InitializeComponent();
        }

        private void copyAsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.listView1.Columns.Count; i++)
            {
                builder.AppendFormat("{0},", this.listView1.Columns[i].Text.Replace(',', '_'));
            }
            builder.Length--;
            builder.AppendLine();
            for (int j = 0; j < this.listView1.Items.Count; j++)
            {
                ListViewItem item = this.listView1.Items[j];
                for (int k = 0; k < item.SubItems.Count; k++)
                {
                    builder.AppendFormat("{0},", item.SubItems[k].Text.Replace(',', '_'));
                }
                builder.Length--;
                builder.AppendLine();
            }
            ActGlobals.oFormActMain.SendToClipboard(builder.ToString(), true);
        }

        private void copyAsFormattedPlainTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] numArray = new int[this.listView1.Columns.Count];
            for (int i = 0; i < this.listView1.Columns.Count; i++)
            {
                int length = this.listView1.Columns[i].Text.Length;
                for (int m = 0; m < this.listView1.Items.Count; m++)
                {
                    ListViewItem item = this.listView1.Items[m];
                    int num4 = item.SubItems[i].Text.Length;
                    if (num4 > length)
                    {
                        length = num4;
                    }
                }
                numArray[i] = length + 2;
            }
            StringBuilder builder = new StringBuilder();
            for (int j = 0; j < this.listView1.Columns.Count; j++)
            {
                builder.Append(this.listView1.Columns[j].Text.ToUpper().PadRight(numArray[j]));
            }
            builder.AppendLine();
            for (int k = 0; k < this.listView1.Items.Count; k++)
            {
                ListViewItem item2 = this.listView1.Items[k];
                for (int n = 0; n < item2.SubItems.Count; n++)
                {
                    builder.Append(item2.SubItems[n].Text.PadRight(numArray[n]));
                }
                builder.AppendLine();
            }
            ActGlobals.oFormActMain.SendToClipboard(builder.ToString(), true);
        }

        private void copyAsHTMLExtendedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<table border='1'> <tr>");
            for (int i = 0; i < this.listView1.Columns.Count; i++)
            {
                builder.AppendFormat("<th>{0}</th> ", this.listView1.Columns[i].Text);
            }
            builder.Append("</tr> ");
            for (int j = 0; j < this.listView1.Groups.Count; j++)
            {
                builder.AppendFormat("<tr><td colspan='4'><br><b>{0}</b></th></tr> ", this.listView1.Groups[j].Header);
                for (int k = 0; k < this.listView1.Items.Count; k++)
                {
                    if (this.listView1.Items[k].Group.Name == this.listView1.Groups[j].Name)
                    {
                        builder.Append("<tr>");
                        for (int m = 0; m < this.listView1.Columns.Count; m++)
                        {
                            builder.AppendFormat("<td>{0}</td> ", this.listView1.Items[k].SubItems[m].Text);
                        }
                        builder.Append("</tr> ");
                        if (!this.listView1.Items[k].Text.EndsWith("TOTAL"))
                        {
                            builder.Append("<tr><td colspan='4'><i>");
                            SortedList<AvoidanceItem, int> tag = (SortedList<AvoidanceItem, int>) this.listView1.Items[k].Tag;
                            for (int n = 0; n < tag.Count; n++)
                            {
                                builder.AppendFormat("{0} (x{1})<br>", tag.Keys[n], tag.Values[n]);
                            }
                            builder.Append("</i></td></tr> ");
                        }
                    }
                }
            }
            builder.Append("</table>");
            ActGlobals.oFormActMain.SendHtmlToClipboard(builder.ToString());
        }

        private void copyAsHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<table border='1'> <tr>");
            for (int i = 0; i < this.listView1.Columns.Count; i++)
            {
                builder.AppendFormat("<th>{0}</th> ", this.listView1.Columns[i].Text);
            }
            builder.Append("</tr> ");
            for (int j = 0; j < this.listView1.Groups.Count; j++)
            {
                builder.AppendFormat("<tr><td colspan='4'><br><b>{0}</b></th></tr> ", this.listView1.Groups[j].Header);
                for (int k = 0; k < this.listView1.Items.Count; k++)
                {
                    if (this.listView1.Items[k].Group.Name == this.listView1.Groups[j].Name)
                    {
                        builder.Append("<tr>");
                        for (int m = 0; m < this.listView1.Columns.Count; m++)
                        {
                            builder.AppendFormat("<td>{0}</td> ", this.listView1.Items[k].SubItems[m].Text);
                        }
                        builder.Append("</tr> ");
                    }
                }
            }
            builder.Append("</table>");
            ActGlobals.oFormActMain.SendHtmlToClipboard(builder.ToString());
        }

        private void copyAsSimpleTextForEQ2PastingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            if (this.encounterAvoidance)
            {
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    if (this.listView1.Items[i].SubItems[0].Text.Contains("TOTAL"))
                    {
                        builder.AppendFormat("{0} {1} {2}\n", this.listView1.Items[i].SubItems[0].Text, this.listView1.Items[i].SubItems[1].Text, this.listView1.Items[i].SubItems[2].Text);
                    }
                }
                builder.Replace(" TOTAL", string.Empty);
                ActGlobals.oFormActMain.SendToClipboard(builder.ToString(), true);
            }
            else
            {
                for (int j = 0; j < this.listView1.Items.Count; j++)
                {
                    if (this.listView1.Items[j].Group.Header == "Both")
                    {
                        if (this.listView1.Items[j].SubItems[0].Text.Contains("TOTAL"))
                        {
                            builder.Insert(0, string.Format("{0} {1} {2}\n", this.listView1.Items[j].SubItems[0].Text, this.listView1.Items[j].SubItems[3].Text, this.listView1.Items[j].SubItems[4].Text));
                        }
                        else
                        {
                            builder.AppendFormat("{0} {1} {2}\n", this.listView1.Items[j].SubItems[0].Text, this.listView1.Items[j].SubItems[3].Text, this.listView1.Items[j].SubItems[4].Text);
                        }
                    }
                }
                builder.Replace("     TOTAL", "TOTAL");
                builder.Replace("No Damage (Stoneskin)", "No Damage");
                builder.Replace(" / ", "/");
                builder.Replace("(", string.Empty);
                builder.Replace(")", string.Empty);
                ActGlobals.oFormActMain.SendToClipboard(Regex.Replace(builder.ToString(), @"(\.\d)\d%", "$1%"), true);
            }
        }

        private void copyAsSimpleTextNameZzHPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            if (this.encounterAvoidance)
            {
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    if (this.listView1.Items[i].SubItems[0].Text.Contains("TOTAL"))
                    {
                        builder.AppendFormat("{0} {1} {2}\n", this.listView1.Items[i].SubItems[0].Text, this.listView1.Items[i].SubItems[1].Text, this.listView1.Items[i].SubItems[3].Text);
                    }
                }
                builder.Replace(" TOTAL", string.Empty);
                ActGlobals.oFormActMain.SendToClipboard(builder.ToString(), true);
            }
            else
            {
                for (int j = 0; j < this.listView1.Items.Count; j++)
                {
                    if (this.listView1.Items[j].Group.Header == "Both")
                    {
                        if (this.listView1.Items[j].SubItems[0].Text.Contains("TOTAL"))
                        {
                            builder.Insert(0, string.Format("{0} {1} {2}\n", this.listView1.Items[j].SubItems[0].Text, this.listView1.Items[j].SubItems[4].Text, this.listView1.Items[j].SubItems[6].Text));
                        }
                        else
                        {
                            builder.AppendFormat("{0} {1} {2}\n", this.listView1.Items[j].SubItems[0].Text, this.listView1.Items[j].SubItems[4].Text, this.listView1.Items[j].SubItems[6].Text);
                        }
                    }
                }
                builder.Replace("     TOTAL", "TOTAL");
                builder.Replace("No Damage (Stoneskin)", "No Damage");
                builder.Replace(" / ", "/");
                builder.Replace("(", string.Empty);
                builder.Replace(")", string.Empty);
                ActGlobals.oFormActMain.SendToClipboard(Regex.Replace(builder.ToString(), @"(\.\d)\d%", "$1%"), true);
            }
        }

        private void copyAsTSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.listView1.Columns.Count; i++)
            {
                builder.AppendFormat("{0}\t", this.listView1.Columns[i].Text);
            }
            builder.Length--;
            builder.AppendLine();
            for (int j = 0; j < this.listView1.Items.Count; j++)
            {
                ListViewItem item = this.listView1.Items[j];
                for (int k = 0; k < item.SubItems.Count; k++)
                {
                    builder.AppendFormat("{0}\t", item.SubItems[k].Text);
                }
                builder.Length--;
                builder.AppendLine();
            }
            ActGlobals.oFormActMain.SendToClipboard(builder.ToString(), true);
        }

        private void copyAsXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryStream w = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(w, Encoding.UTF8) {
                Formatting = Formatting.Indented,
                Indentation = 4,
                Namespaces = false
            };
            writer.WriteStartDocument();
            writer.WriteStartElement("", "AvoidanceReport", "");
            writer.WriteAttributeString("", "Title", "", this.Text);
            for (int i = 0; i < this.listView1.Groups.Count; i++)
            {
                writer.WriteStartElement("", this.listView1.Groups[i].Name.Replace(' ', '_'), "");
                for (int j = 0; j < this.listView1.Items.Count; j++)
                {
                    if (this.listView1.Items[j].Group.Name == this.listView1.Groups[i].Name)
                    {
                        writer.WriteStartElement("", "AvoidanceItem", "");
                        for (int k = 0; k < this.listView1.Columns.Count; k++)
                        {
                            writer.WriteStartElement("", this.listView1.Columns[k].Text.Replace(' ', '_'), "");
                            writer.WriteString(this.listView1.Items[j].SubItems[k].Text);
                            writer.WriteEndElement();
                        }
                        SortedList<AvoidanceItem, int> tag = (SortedList<AvoidanceItem, int>) this.listView1.Items[j].Tag;
                        StringBuilder builder = new StringBuilder();
                        for (int m = 0; m < tag.Count; m++)
                        {
                            builder.AppendFormat("{0} (x{1})\n", tag.Keys[m], tag.Values[m]);
                        }
                        writer.WriteStartElement("", "DetailInfo", "");
                        writer.WriteString(builder.ToString(0, builder.Length - 1));
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            w.Seek(0, SeekOrigin.Begin);
            ActGlobals.oFormActMain.SendToClipboard(new StreamReader(w, Encoding.UTF8).ReadToEnd(), true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ExportControlTextXML(Stream Output)
        {
            XmlTextWriter xml = new XmlTextWriter(Output, Encoding.UTF8) {
                Formatting = Formatting.Indented,
                Indentation = 4,
                Namespaces = false
            };
            xml.WriteStartDocument();
            xml.WriteStartElement("", "ControlText", "");
            xml.WriteAttributeString("Form", "FormAvoidanceReport");
            ActGlobals.oFormActMain.ExportControlChilderenText(xml, this, "root");
            xml.WriteEndElement();
            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();
        }

        public void ExportControlTextXML(string FilePath)
        {
            this.ExportControlTextXML(new FileInfo(FilePath).OpenWrite());
        }

        private void FormAvoidanceReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        private void FormAvoidanceReport_ResizeEnd(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.ResizeLVCols(this.listView1);
            ActGlobals.oFormActMain.ResizeLVCols(this.listView2);
        }

        private List<MasterSwing> GetDnumOccurances(Dnum dnum, DamageTypeData dtGroup)
        {
            AttackType type;
            dtGroup.Items.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type);
            List<MasterSwing> list = new List<MasterSwing>();
            for (int i = 0; i < type.Items.Count; i++)
            {
                if (type.Items[i].Damage == dnum)
                {
                    list.Add(type.Items[i]);
                }
            }
            return list;
        }

        private SortedList<string, Dnum> GetUniqueAvoidances(DamageTypeData dt)
        {
            AttackType type;
            SortedList<string, Dnum> list = new SortedList<string, Dnum>();
            if (dt.Items.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
            {
                for (int i = 0; i < type.Items.Count; i++)
                {
                    MasterSwing swing = type.Items[i];
                    if ((swing.Damage < 1) && !list.ContainsKey(swing.Damage.DamageString))
                    {
                        list.Add(swing.Damage.DamageString, swing.Damage);
                    }
                }
            }
            return list;
        }

        public void ImportControlTextXML(Stream Input)
        {
            XmlTextReader reader = new XmlTextReader(Input);
            try
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        try
                        {
                            if (reader.LocalName == "Control")
                            {
                                bool found = false;
                                Control c = this;
                                string attribute = reader.GetAttribute("UniqueName");
                                string controlText = reader.GetAttribute("Text");
                                if (!ActGlobals.oFormActMain.ImportControlChilderenText(attribute, controlText, found, c))
                                {
                                    throw new ArgumentException(string.Format("Control {0} could not be located in the windows form.", attribute));
                                }
                            }
                            continue;
                        }
                        catch (Exception exception)
                        {
                            ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Format(ActGlobals.ActLocalization.LocalizationStrings["messageBox-xmlReadError"].DisplayedText, reader.LineNumber, reader.LocalName, exception.Message));
                            continue;
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                MessageBox.Show(string.Format(ActGlobals.ActLocalization.LocalizationStrings["messageBox-xmlSyntaxError"].DisplayedText, exception2.Message), ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-xmlPrefError"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            reader.Close();
        }

        public void ImportControlTextXML(string FilePath)
        {
            this.ImportControlTextXML(new FileInfo(FilePath).OpenRead());
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ListViewGroup group = new ListViewGroup("Separate", HorizontalAlignment.Left);
            ListViewGroup group2 = new ListViewGroup("Per Attack Type", HorizontalAlignment.Left);
            ListViewGroup group3 = new ListViewGroup("Per Special Type", HorizontalAlignment.Left);
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormAvoidanceReport));
            this.listView1 = new ListView();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.copyAsFormattedPlainTextToolStripMenuItem = new ToolStripMenuItem();
            this.copyAsCSVToolStripMenuItem = new ToolStripMenuItem();
            this.copyAsTSVToolStripMenuItem = new ToolStripMenuItem();
            this.copyAsHTMLToolStripMenuItem = new ToolStripMenuItem();
            this.copyAsHTMLExtendedToolStripMenuItem = new ToolStripMenuItem();
            this.copyAsSimpleTextForEQ2PastingToolStripMenuItem = new ToolStripMenuItem();
            this.copyAsSimpleTextNameZzHPSToolStripMenuItem = new ToolStripMenuItem();
            this.listView2 = new ListView();
            this.columnHeader5 = new ColumnHeader();
            this.columnHeader6 = new ColumnHeader();
            this.columnHeader7 = new ColumnHeader();
            this.columnHeader8 = new ColumnHeader();
            this.columnHeader9 = new ColumnHeader();
            this.columnHeader10 = new ColumnHeader();
            this.columnHeader11 = new ColumnHeader();
            this.columnHeader12 = new ColumnHeader();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.listView1.Activation = ItemActivation.OneClick;
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new Point(3, 3);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x2e3, 0x1b2);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.Visible = false;
            this.listView1.ItemActivate += new EventHandler(this.listView1_ItemActivate);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.copyAsFormattedPlainTextToolStripMenuItem, this.copyAsCSVToolStripMenuItem, this.copyAsTSVToolStripMenuItem, this.copyAsHTMLToolStripMenuItem, this.copyAsHTMLExtendedToolStripMenuItem, this.copyAsSimpleTextForEQ2PastingToolStripMenuItem, this.copyAsSimpleTextNameZzHPSToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0xfb, 180);
            this.copyAsFormattedPlainTextToolStripMenuItem.Name = "copyAsFormattedPlainTextToolStripMenuItem";
            this.copyAsFormattedPlainTextToolStripMenuItem.Size = new Size(250, 0x16);
            this.copyAsFormattedPlainTextToolStripMenuItem.Text = "Copy as Formatted Plain Text";
            this.copyAsFormattedPlainTextToolStripMenuItem.Click += new EventHandler(this.copyAsFormattedPlainTextToolStripMenuItem_Click);
            this.copyAsCSVToolStripMenuItem.Name = "copyAsCSVToolStripMenuItem";
            this.copyAsCSVToolStripMenuItem.Size = new Size(250, 0x16);
            this.copyAsCSVToolStripMenuItem.Text = "Copy as CSV";
            this.copyAsCSVToolStripMenuItem.Click += new EventHandler(this.copyAsCSVToolStripMenuItem_Click);
            this.copyAsTSVToolStripMenuItem.Name = "copyAsTSVToolStripMenuItem";
            this.copyAsTSVToolStripMenuItem.Size = new Size(250, 0x16);
            this.copyAsTSVToolStripMenuItem.Text = "Copy as TSV";
            this.copyAsTSVToolStripMenuItem.Click += new EventHandler(this.copyAsTSVToolStripMenuItem_Click);
            this.copyAsHTMLToolStripMenuItem.Name = "copyAsHTMLToolStripMenuItem";
            this.copyAsHTMLToolStripMenuItem.Size = new Size(250, 0x16);
            this.copyAsHTMLToolStripMenuItem.Text = "Copy as HTML";
            this.copyAsHTMLToolStripMenuItem.Click += new EventHandler(this.copyAsHTMLToolStripMenuItem_Click);
            this.copyAsHTMLExtendedToolStripMenuItem.Name = "copyAsHTMLExtendedToolStripMenuItem";
            this.copyAsHTMLExtendedToolStripMenuItem.Size = new Size(250, 0x16);
            this.copyAsHTMLExtendedToolStripMenuItem.Text = "Copy as HTML Extended";
            this.copyAsHTMLExtendedToolStripMenuItem.Click += new EventHandler(this.copyAsHTMLExtendedToolStripMenuItem_Click);
            this.copyAsSimpleTextForEQ2PastingToolStripMenuItem.Name = "copyAsSimpleTextForEQ2PastingToolStripMenuItem";
            this.copyAsSimpleTextForEQ2PastingToolStripMenuItem.Size = new Size(250, 0x16);
            this.copyAsSimpleTextForEQ2PastingToolStripMenuItem.Text = "Copy as Simple Text";
            this.copyAsSimpleTextForEQ2PastingToolStripMenuItem.Click += new EventHandler(this.copyAsSimpleTextForEQ2PastingToolStripMenuItem_Click);
            this.copyAsSimpleTextNameZzHPSToolStripMenuItem.Name = "copyAsSimpleTextNameZzHPSToolStripMenuItem";
            this.copyAsSimpleTextNameZzHPSToolStripMenuItem.Size = new Size(250, 0x16);
            this.copyAsSimpleTextNameZzHPSToolStripMenuItem.Text = "Copy as Simple Text (/w ENCHPS)";
            this.copyAsSimpleTextNameZzHPSToolStripMenuItem.Click += new EventHandler(this.copyAsSimpleTextNameZzHPSToolStripMenuItem_Click);
            this.listView2.Columns.AddRange(new ColumnHeader[] { this.columnHeader5, this.columnHeader6, this.columnHeader7, this.columnHeader8, this.columnHeader9, this.columnHeader10, this.columnHeader11, this.columnHeader12 });
            this.listView2.Dock = DockStyle.Fill;
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            group.Header = "Separate";
            group.Name = "listViewGroup1";
            group2.Header = "Per Attack Type";
            group2.Name = "listViewGroup2";
            group3.Header = "Per Special Type";
            group3.Name = "listViewGroup3";
            this.listView2.Groups.AddRange(new ListViewGroup[] { group, group2, group3 });
            this.listView2.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView2.Location = new Point(3, 3);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new Size(0x2e3, 0x1b2);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = View.Details;
            this.listView2.Visible = false;
            this.columnHeader5.Text = "Type";
            this.columnHeader6.Text = "Proc Count";
            this.columnHeader7.Text = "Extra Attacks";
            this.columnHeader8.Text = "All Attacks";
            this.columnHeader9.Text = "Proc Chance";
            this.columnHeader10.Text = "Extra to Normal";
            this.columnHeader11.Text = "Total EncDPS";
            this.columnHeader12.Text = "Added EncDPS";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2e9, 440);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.listView2);
            this.DoubleBuffered = true;
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "FormAvoidanceReport";
            base.Padding = new Padding(3);
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "FormAvoidanceReport";
            base.FormClosing += new FormClosingEventHandler(this.FormAvoidanceReport_FormClosing);
            base.ResizeEnd += new EventHandler(this.FormAvoidanceReport_ResizeEnd);
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem item;
            SortedList<AvoidanceItem, int> tag;
            try
            {
                item = this.listView1.SelectedItems[0];
                tag = (SortedList<AvoidanceItem, int>) item.Tag;
            }
            catch
            {
                return;
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < tag.Count; i++)
            {
                builder.AppendFormat("{0} (x{1})\n", tag.Keys[i], tag.Values[i]);
            }
            if (MessageBox.Show(builder.ToString() + ActGlobals.ActLocalization.LocalizationStrings["messageBox-avoidanceCopyDetail"].DisplayedText, string.Format(ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-avoidanceCopyDetail"].DisplayedText, item.Text), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ActGlobals.oFormActMain.SendToClipboard(builder.ToString(), true);
            }
        }

        public void ShowAvoidanceReport(CombatantData cD)
        {
            this.encounterAvoidance = false;
            List<string> list = new List<string>();
            foreach (int num in CombatantData.DamageSwingTypes)
            {
                foreach (string str in CombatantData.SwingTypeToDamageTypeDataLinksOutgoing[num])
                {
                    if (!list.Contains(str))
                    {
                        list.Add(str);
                    }
                }
            }
            Dictionary<string, DamageTypeData> dictionary = new Dictionary<string, DamageTypeData>();
            SortedList<string, AttackType> allInc = cD.AllInc;
            foreach (string str2 in list)
            {
                dictionary.Add(str2, new DamageTypeData(false, str2, null));
            }
            for (int i = 0; i < allInc.Count; i++)
            {
                AttackType type = allInc.Values[i];
                if (type.Type != ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)
                {
                    for (int j = 0; j < type.Items.Count; j++)
                    {
                        MasterSwing action = type.Items[j];
                        if (CombatantData.DamageSwingTypes.Contains(action.SwingType) && (action.Damage != Dnum.Death))
                        {
                            foreach (string str3 in CombatantData.SwingTypeToDamageTypeDataLinksOutgoing[action.SwingType])
                            {
                                dictionary[str3].AddCombatAction(action, ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText);
                                dictionary[str3].AddCombatAction(action, action.AttackType);
                            }
                        }
                    }
                }
            }
            List<DamageTypeData> list3 = new List<DamageTypeData>();
            foreach (KeyValuePair<string, DamageTypeData> pair in dictionary)
            {
                list3.Add(pair.Value);
            }
            list3.Sort();
            this.listView2.Visible = false;
            this.listView1.Visible = true;
            this.listView1.Groups.Clear();
            foreach (DamageTypeData data in list3)
            {
                this.listView1.Groups.Add(data.Type, data.Type);
            }
            this.listView1.Columns.Clear();
            this.listView1.Columns.Add("Type", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Avoids", 100, HorizontalAlignment.Center);
            this.listView1.Columns.Add("% of Avoids", 100, HorizontalAlignment.Center);
            this.listView1.Columns.Add("Swings", 100, HorizontalAlignment.Center);
            this.listView1.Columns.Add("% of Swings", 100, HorizontalAlignment.Center);
            this.listView1.Columns.Add("Damage Avoided By Average", 100, HorizontalAlignment.Center);
            this.listView1.Columns.Add("EncHPS", 100, HorizontalAlignment.Center);
            this.listView1.BeginUpdate();
            this.listView1.Items.Clear();
            bool blockIsHit = ActGlobals.blockIsHit;
            ActGlobals.blockIsHit = false;
            foreach (DamageTypeData data2 in list3)
            {
                SortedList<string, Dnum> uniqueAvoidances = this.GetUniqueAvoidances(data2);
                float num4 = 0f;
                float num5 = 0f;
                float swings = data2.Swings;
                float num7 = 0f;
                SortedList<AvoidanceItem, int> list5 = new SortedList<AvoidanceItem, int>();
                for (int k = 0; k < uniqueAvoidances.Count; k++)
                {
                    SortedList<AvoidanceItem, int> list6 = new SortedList<AvoidanceItem, int>();
                    List<MasterSwing> dnumOccurances = this.GetDnumOccurances(uniqueAvoidances.Values[k], data2);
                    float count = dnumOccurances.Count;
                    num4 += count;
                    float num10 = data2.Swings - data2.Hits;
                    num5 += num10;
                    float num11 = (count / num10) * 100f;
                    float num12 = (count / swings) * 100f;
                    float num13 = 0f;
                    for (int m = 0; m < dnumOccurances.Count; m++)
                    {
                        int num17;
                        MasterSwing swing2 = dnumOccurances[m];
                        CombatantData combatant = cD.Parent.GetCombatant(swing2.Attacker);
                        AttackType type2 = null;
                        if (CombatantData.SwingTypeToDamageTypeDataLinksOutgoing[swing2.SwingType].Contains(CombatantData.DamageTypeDataNonSkillDamage))
                        {
                            type2 = combatant.Items[CombatantData.DamageTypeDataNonSkillDamage].Items[ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText];
                        }
                        else
                        {
                            type2 = combatant.AllOut[swing2.AttackType];
                        }
                        AttackType type3 = new AttackType(type2.Type, type2.Parent);
                        for (int n = 0; n < type2.Items.Count; n++)
                        {
                            MasterSwing swing3 = type2.Items[n];
                            if ((swing3.Victim == swing2.Victim) && CombatantData.DamageSwingTypes.Contains(swing2.SwingType))
                            {
                                type3.AddCombatAction(swing3);
                            }
                        }
                        float average = 0f;
                        if (!float.IsNaN(type3.Average))
                        {
                            average = type3.Average;
                        }
                        else if (!float.IsNaN(type2.Average))
                        {
                            average = type2.Average;
                        }
                        num13 += average;
                        AvoidanceItem key = new AvoidanceItem(uniqueAvoidances.Values[k].DamageString, combatant.Name, (type2.Type == ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText) ? "Melee" : type2.Type, (average == type3.Average) ? cD.Name : ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, average);
                        if (!list6.TryGetValue(key, out num17))
                        {
                            list6.Add(key, 1);
                        }
                        else
                        {
                            num17++;
                            list6.Remove(key);
                            list6.Add(key, num17);
                        }
                        if (!list5.TryGetValue(key, out num17))
                        {
                            list5.Add(key, 1);
                        }
                        else
                        {
                            num17++;
                            list5.Remove(key);
                            list5.Add(key, num17);
                        }
                    }
                    num7 += num13;
                    ListViewItem item2 = new ListViewItem {
                        Group = this.listView1.Groups[data2.Type],
                        Text = uniqueAvoidances.Values[k].DamageString
                    };
                    if (item2.Text == "No Damage")
                    {
                        item2.Text = "No Damage (Stoneskin)";
                    }
                    if (item2.Text == "0")
                    {
                        item2.Text = "No Damage (Stoneskin)";
                    }
                    item2.SubItems.Add(string.Format("({0} / {1})", count, num10));
                    item2.SubItems.Add(string.Format("{0:0.00}%", num11));
                    item2.SubItems.Add(string.Format("({0} / {1})", count, swings));
                    item2.SubItems.Add(string.Format("{0:0.00}%", num12));
                    item2.SubItems.Add(string.Format("{0:0,0}", num13));
                    item2.SubItems.Add(string.Format("{0:0.00}", ((double) num13) / cD.Parent.Duration.TotalSeconds));
                    item2.Tag = list6;
                    this.listView1.Items.Add(item2);
                }
                float num18 = (num4 / swings) * 100f;
                ListViewItem item3 = new ListViewItem {
                    Group = this.listView1.Groups[data2.Type],
                    Text = "     TOTAL"
                };
                item3.SubItems.Add("-");
                item3.SubItems.Add("-");
                item3.SubItems.Add(string.Format("({0} / {1})", num4, swings));
                item3.SubItems.Add(string.Format("{0:0.00}%", num18));
                item3.SubItems.Add(string.Format("{0:0,0}", num7));
                item3.SubItems.Add(string.Format("{0:0.00}", ((double) num7) / cD.Parent.Duration.TotalSeconds));
                item3.Tag = list5;
                this.listView1.Items.Add(item3);
            }
            this.listView1.EndUpdate();
            ActGlobals.oFormActMain.ResizeLVCols(this.listView1);
            base.Show();
            ActGlobals.blockIsHit = blockIsHit;
        }

        public void ShowEncounterAvoidance(EncounterData eD)
        {
            this.encounterAvoidance = true;
            this.listView2.Visible = false;
            this.listView1.Visible = true;
            this.listView1.BeginUpdate();
            this.listView1.Groups.Clear();
            this.listView1.Items.Clear();
            this.listView1.Columns.Clear();
            this.listView1.Columns.Add("Type", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Count", 100, HorizontalAlignment.Center);
            this.listView1.Columns.Add("Damage Avoided By Average", 100, HorizontalAlignment.Center);
            this.listView1.Columns.Add("EncHPS", 100, HorizontalAlignment.Center);
            bool blockIsHit = ActGlobals.blockIsHit;
            ActGlobals.blockIsHit = false;
            DamageTypeData dt = new DamageTypeData(false, string.Empty, null);
            for (int i = 0; i < eD.Items.Count; i++)
            {
                CombatantData data2 = eD.Items.Values[i];
                SortedList<string, AttackType> allInc = data2.AllInc;
                for (int m = 0; m < allInc.Values.Count; m++)
                {
                    AttackType type = allInc.Values[m];
                    if (type.Type != ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)
                    {
                        for (int n = 0; n < type.Items.Count; n++)
                        {
                            if (CombatantData.DamageSwingTypes.Contains(type.Items[n].SwingType) && (type.Items[n].Damage != Dnum.Death))
                            {
                                dt.AddCombatAction(type.Items[n], ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText);
                            }
                        }
                    }
                }
            }
            SortedList<string, Dnum> uniqueAvoidances = this.GetUniqueAvoidances(dt);
            List<string> list3 = new List<string>();
            SortedList<string, Dnum> list4 = new SortedList<string, Dnum>();
            List<CombatantData> allies = eD.GetAllies(false);
            allies.Add(new CombatantData("YOU", eD));
            for (int j = 0; j < uniqueAvoidances.Count; j++)
            {
                string str = uniqueAvoidances.Keys[j];
                for (int num5 = 0; num5 < allies.Count; num5++)
                {
                    if (str.Contains(allies[num5].Name))
                    {
                        if (!list3.Contains(allies[num5].Name))
                        {
                            list3.Add(allies[num5].Name);
                        }
                        list4.Add(uniqueAvoidances.Keys[j], uniqueAvoidances.Values[j]);
                        break;
                    }
                }
            }
            for (int k = 0; k < list3.Count; k++)
            {
                string key = list3[k];
                this.listView1.Groups.Add(key, key);
                int num7 = 0;
                float num8 = 0f;
                SortedList<AvoidanceItem, int> list6 = new SortedList<AvoidanceItem, int>();
                for (int num9 = 0; num9 < uniqueAvoidances.Count; num9++)
                {
                    string str3 = uniqueAvoidances.Keys[num9];
                    if (str3.Contains(key))
                    {
                        SortedList<AvoidanceItem, int> list7 = new SortedList<AvoidanceItem, int>();
                        List<MasterSwing> dnumOccurances = this.GetDnumOccurances(uniqueAvoidances.Values[num9], dt);
                        num7 += dnumOccurances.Count;
                        float num10 = 0f;
                        for (int num11 = 0; num11 < dnumOccurances.Count; num11++)
                        {
                            int num14;
                            MasterSwing swing = dnumOccurances[num11];
                            CombatantData combatant = eD.GetCombatant(swing.Attacker);
                            AttackType type2 = null;
                            if (CombatantData.SwingTypeToDamageTypeDataLinksOutgoing[swing.SwingType].Contains(CombatantData.DamageTypeDataNonSkillDamage))
                            {
                                type2 = combatant.Items[CombatantData.DamageTypeDataNonSkillDamage].Items[ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText];
                            }
                            else
                            {
                                type2 = combatant.AllOut[swing.AttackType];
                            }
                            AttackType type3 = new AttackType(type2.Type, type2.Parent);
                            for (int num12 = 0; num12 < type2.Items.Count; num12++)
                            {
                                MasterSwing action = type2.Items[num12];
                                if ((action.Victim == swing.Victim) && CombatantData.DamageSwingTypes.Contains(swing.SwingType))
                                {
                                    type3.AddCombatAction(action);
                                }
                            }
                            float average = 0f;
                            if (!float.IsNaN(type3.Average))
                            {
                                average = type3.Average;
                            }
                            else if (!float.IsNaN(type2.Average))
                            {
                                average = type2.Average;
                            }
                            num10 += average;
                            AvoidanceItem item = new AvoidanceItem(uniqueAvoidances.Values[num9].DamageString, combatant.Name, (type2.Type == ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText) ? "Melee" : type2.Type, (average == type3.Average) ? swing.Victim : ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, average);
                            if (!list7.TryGetValue(item, out num14))
                            {
                                list7.Add(item, 1);
                            }
                            else
                            {
                                num14++;
                                list7.Remove(item);
                                list7.Add(item, num14);
                            }
                            if (!list6.TryGetValue(item, out num14))
                            {
                                list6.Add(item, 1);
                            }
                            else
                            {
                                num14++;
                                list6.Remove(item);
                                list6.Add(item, num14);
                            }
                        }
                        num8 += num10;
                        ListViewItem item2 = new ListViewItem {
                            Group = this.listView1.Groups[key],
                            Text = str3
                        };
                        item2.SubItems.Add(dnumOccurances.Count.ToString());
                        item2.SubItems.Add(string.Format("{0:0,0}", num10));
                        item2.SubItems.Add(string.Format("{0:0.00}", ((double) num10) / eD.Duration.TotalSeconds));
                        item2.Tag = list7;
                        this.listView1.Items.Add(item2);
                    }
                }
                ListViewItem item3 = new ListViewItem {
                    Group = this.listView1.Groups[key],
                    Text = key + " TOTAL"
                };
                item3.SubItems.Add(num7.ToString());
                item3.SubItems.Add(string.Format("{0:0,0}", num8));
                item3.SubItems.Add(string.Format("{0:0.00}", ((double) num8) / eD.Duration.TotalSeconds));
                item3.Tag = list6;
                this.listView1.Items.Add(item3);
            }
            this.listView1.EndUpdate();
            ActGlobals.oFormActMain.ResizeLVCols(this.listView1);
            base.Show();
            ActGlobals.blockIsHit = blockIsHit;
        }

        public void ShowSpecialsReport(CombatantData cD)
        {
            this.listView2.Visible = true;
            this.listView1.Visible = false;
            this.listView2.BeginUpdate();
            this.listView2.Items.Clear();
            DamageTypeData data = new DamageTypeData(true, string.Empty, cD);
            DamageTypeData data2 = new DamageTypeData(true, string.Empty, cD);
            DamageTypeData data3 = new DamageTypeData(true, string.Empty, cD);
            DamageTypeData data4 = new DamageTypeData(true, string.Empty, cD);
            DamageTypeData data5 = new DamageTypeData(true, string.Empty, cD);
            DamageTypeData data6 = new DamageTypeData(true, string.Empty, cD);
            AttackType type = cD.AllOut[ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText];
            for (int i = 0; (type != null) && (i < type.Items.Count); i++)
            {
                MasterSwing action = type.Items[i];
                if (CombatantData.DamageSwingTypes.Contains(action.SwingType))
                {
                    if (action.Special != ActGlobals.ActLocalization.LocalizationStrings["specialAttackTerm-none"].DisplayedText)
                    {
                        data.AddCombatAction(action, string.Format("{0}({1})", action.AttackType, action.Special));
                        data2.AddCombatAction(action, action.AttackType);
                        data3.AddCombatAction(action, action.Special);
                    }
                    data4.AddCombatAction(action, action.AttackType);
                    data5.AddCombatAction(action, action.AttackType);
                    data6.AddCombatAction(action, action.Special);
                }
            }
            foreach (KeyValuePair<string, AttackType> pair in data.Items)
            {
                int length = pair.Key.LastIndexOf('(');
                string str = pair.Key.Substring(0, length);
                string str2 = pair.Key.Substring(length + 1, (pair.Key.Length - length) - 2);
                AttackType type2 = data4.Items[str];
                Dictionary<string, int> attackSpecials = type2.GetAttackSpecials();
                if (!attackSpecials.ContainsKey(ActGlobals.ActLocalization.LocalizationStrings["specialAttackTerm-none"].DisplayedText))
                {
                    attackSpecials.Add(ActGlobals.ActLocalization.LocalizationStrings["specialAttackTerm-none"].DisplayedText, 0);
                }
                ListViewItem item = new ListViewItem {
                    Group = this.listView2.Groups[0],
                    Text = pair.Key
                };
                int num3 = attackSpecials[str2];
                item.SubItems.Add(num3.ToString());
                item.SubItems.Add(pair.Value.Swings.ToString());
                item.SubItems.Add(type2.Swings.ToString());
                float num6 = ((float) attackSpecials[str2]) / ((float) attackSpecials[ActGlobals.ActLocalization.LocalizationStrings["specialAttackTerm-none"].DisplayedText]);
                item.SubItems.Add(num6.ToString("0.00%"));
                float num7 = ((float) pair.Value.Swings) / ((float) attackSpecials[ActGlobals.ActLocalization.LocalizationStrings["specialAttackTerm-none"].DisplayedText]);
                item.SubItems.Add(num7.ToString("0.00%"));
                item.SubItems.Add(type2.EncDPS.ToString("0.00"));
                item.SubItems.Add(pair.Value.EncDPS.ToString("0.00"));
                this.listView2.Items.Add(item);
            }
            foreach (KeyValuePair<string, AttackType> pair2 in data2.Items)
            {
                Dictionary<string, int> dictionary2 = data4.Items[pair2.Key].GetAttackSpecials();
                if (!dictionary2.ContainsKey(ActGlobals.ActLocalization.LocalizationStrings["specialAttackTerm-none"].DisplayedText))
                {
                    dictionary2.Add(ActGlobals.ActLocalization.LocalizationStrings["specialAttackTerm-none"].DisplayedText, 0);
                }
                AttackType type4 = data5.Items[pair2.Key];
                ListViewItem item2 = new ListViewItem {
                    Group = this.listView2.Groups[1],
                    Text = pair2.Key
                };
                int num10 = dictionary2["ANY"];
                item2.SubItems.Add(num10.ToString());
                item2.SubItems.Add(pair2.Value.Swings.ToString());
                item2.SubItems.Add(type4.Swings.ToString());
                float num13 = ((float) dictionary2["ONCE"]) / ((float) dictionary2[ActGlobals.ActLocalization.LocalizationStrings["specialAttackTerm-none"].DisplayedText]);
                item2.SubItems.Add(num13.ToString("0.00%"));
                float num14 = ((float) pair2.Value.Swings) / ((float) dictionary2[ActGlobals.ActLocalization.LocalizationStrings["specialAttackTerm-none"].DisplayedText]);
                item2.SubItems.Add(num14.ToString("0.00%"));
                item2.SubItems.Add(type4.EncDPS.ToString("0.00"));
                item2.SubItems.Add(pair2.Value.EncDPS.ToString("0.00"));
                this.listView2.Items.Add(item2);
            }
            foreach (KeyValuePair<string, AttackType> pair3 in data3.Items)
            {
                pair3.Value.GetAttackSpecials();
                ListViewItem item3 = new ListViewItem {
                    Group = this.listView2.Groups[2],
                    Text = pair3.Key
                };
                item3.SubItems.Add("--");
                item3.SubItems.Add(pair3.Value.Swings.ToString());
                item3.SubItems.Add("--");
                item3.SubItems.Add("--");
                item3.SubItems.Add("--");
                item3.SubItems.Add("--");
                item3.SubItems.Add(pair3.Value.EncDPS.ToString("0.00"));
                this.listView2.Items.Add(item3);
            }
            this.listView2.EndUpdate();
            ActGlobals.oFormActMain.ResizeLVCols(this.listView2);
            this.Text = string.Format("{0}'s Special Attack Report", cD.Name);
            base.Show();
        }

        public class AvoidanceItem : IComparable<FormAvoidanceReport.AvoidanceItem>, IEquatable<FormAvoidanceReport.AvoidanceItem>
        {
            public string attacker;
            public string attackType;
            public float average;
            public string avoidString;
            public string sampleVictim;

            public AvoidanceItem(string AvoidString, string Attacker, string AttackType, string SampleVictim, float Average)
            {
                this.avoidString = AvoidString;
                this.attacker = Attacker;
                this.attackType = AttackType;
                this.sampleVictim = SampleVictim;
                this.average = Average;
            }

            public int CompareTo(FormAvoidanceReport.AvoidanceItem other)
            {
                return this.ToString().CompareTo(other.ToString());
            }

            public bool Equals(FormAvoidanceReport.AvoidanceItem other)
            {
                return this.ToString().Equals(other.ToString());
            }

            public override string ToString()
            {
                return string.Format("[{4}] {0}'s {1} vs {2} = {3:0} Avg", new object[] { this.attacker, this.attackType, this.sampleVictim, this.average, this.avoidString });
            }
        }
    }
}

