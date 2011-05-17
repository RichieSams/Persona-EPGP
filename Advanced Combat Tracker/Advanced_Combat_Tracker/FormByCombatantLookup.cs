namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class FormByCombatantLookup : Form
    {
        private List<ListViewItem> cListViewItems = new List<ListViewItem>();
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem copyAsCSVToolStripMenuItem;
        private ToolStripMenuItem copyAsFormattedPlainTextToolStripMenuItem;
        private ToolStripMenuItem copyAsHTMLToolStripMenuItem;
        private ToolStripMenuItem copyAsTSVToolStripMenuItem;
        private object currentTable = new object();
        private List<int> iListViewItems = new List<int>();
        private List<DamageTypeData> lookupRoot = new List<DamageTypeData>();
        private ListView lv1;
        private TableLayoutPanel tableLayoutPanel1;
        private string tableType = string.Empty;
        private TreeView tv1;

        public FormByCombatantLookup()
        {
            this.InitializeComponent();
        }

        private void copyAsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.lv1.Columns.Count; i++)
            {
                builder.AppendFormat("{0},", this.lv1.Columns[i].Text.Replace(',', '_'));
            }
            builder.Length--;
            builder.AppendLine();
            for (int j = 0; j < this.lv1.Items.Count; j++)
            {
                ListViewItem item = this.lv1.Items[j];
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
            int[] numArray = new int[this.lv1.Columns.Count];
            for (int i = 0; i < this.lv1.Columns.Count; i++)
            {
                int length = this.lv1.Columns[i].Text.Length;
                for (int m = 0; m < this.lv1.Items.Count; m++)
                {
                    ListViewItem item = this.lv1.Items[m];
                    int num4 = item.SubItems[i].Text.Length;
                    if (num4 > length)
                    {
                        length = num4;
                    }
                }
                numArray[i] = length + 2;
            }
            StringBuilder builder = new StringBuilder();
            for (int j = 0; j < this.lv1.Columns.Count; j++)
            {
                builder.Append(this.lv1.Columns[j].Text.ToUpper().PadRight(numArray[j]));
            }
            builder.AppendLine();
            for (int k = 0; k < this.lv1.Items.Count; k++)
            {
                ListViewItem item2 = this.lv1.Items[k];
                for (int n = 0; n < item2.SubItems.Count; n++)
                {
                    builder.Append(item2.SubItems[n].Text.PadRight(numArray[n]));
                }
                builder.AppendLine();
            }
            ActGlobals.oFormActMain.SendToClipboard(builder.ToString(), true);
        }

        private void copyAsHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<table border='1'> <tr>");
            for (int i = 0; i < this.lv1.Columns.Count; i++)
            {
                builder.AppendFormat("<th>{0}</th> ", this.lv1.Columns[i].Text);
            }
            builder.Append("</tr> ");
            for (int j = 0; j < this.lv1.Items.Count; j++)
            {
                builder.Append("<tr>");
                for (int k = 0; k < this.lv1.Columns.Count; k++)
                {
                    builder.AppendFormat("<td>{0}</td> ", this.lv1.Items[j].SubItems[k].Text);
                }
                builder.Append("</tr> ");
            }
            builder.Append("</table>");
            ActGlobals.oFormActMain.SendHtmlToClipboard(builder.ToString());
        }

        private void copyAsTSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.lv1.Columns.Count; i++)
            {
                builder.AppendFormat("{0}\t", this.lv1.Columns[i].Text);
            }
            builder.Length--;
            builder.AppendLine();
            for (int j = 0; j < this.lv1.Items.Count; j++)
            {
                ListViewItem item = this.lv1.Items[j];
                for (int k = 0; k < item.SubItems.Count; k++)
                {
                    builder.AppendFormat("{0}\t", item.SubItems[k].Text);
                }
                builder.Length--;
                builder.AppendLine();
            }
            ActGlobals.oFormActMain.SendToClipboard(builder.ToString(), true);
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
            xml.WriteAttributeString("Form", "FormByCombatantLookup");
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

        private void Form14_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormByCombatantLookup));
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.tv1 = new TreeView();
            this.lv1 = new ListView();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.copyAsCSVToolStripMenuItem = new ToolStripMenuItem();
            this.copyAsTSVToolStripMenuItem = new ToolStripMenuItem();
            this.copyAsHTMLToolStripMenuItem = new ToolStripMenuItem();
            this.copyAsFormattedPlainTextToolStripMenuItem = new ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.tableLayoutPanel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.27273f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72.72727f));
            this.tableLayoutPanel1.Controls.Add(this.tv1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lv1, 1, 0);
            this.tableLayoutPanel1.Location = new Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 66.66666f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
            this.tableLayoutPanel1.Size = new Size(0x3b7, 0x231);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tv1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tv1.HideSelection = false;
            this.tv1.Location = new Point(3, 3);
            this.tv1.Name = "tv1";
            this.tableLayoutPanel1.SetRowSpan(this.tv1, 2);
            this.tv1.Size = new Size(0xfd, 0x22b);
            this.tv1.TabIndex = 0;
            this.tv1.AfterSelect += new TreeViewEventHandler(this.tv1_AfterSelect);
            this.lv1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lv1.ContextMenuStrip = this.contextMenuStrip1;
            this.lv1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.lv1.Location = new Point(0x106, 3);
            this.lv1.Name = "lv1";
            this.tableLayoutPanel1.SetRowSpan(this.lv1, 2);
            this.lv1.Size = new Size(0x2ae, 0x22b);
            this.lv1.TabIndex = 1;
            this.lv1.UseCompatibleStateImageBehavior = false;
            this.lv1.View = View.Details;
            this.lv1.VirtualMode = true;
            this.lv1.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(this.lv1_RetrieveVirtualItem);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.copyAsCSVToolStripMenuItem, this.copyAsTSVToolStripMenuItem, this.copyAsHTMLToolStripMenuItem, this.copyAsFormattedPlainTextToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0xd5, 0x72);
            this.copyAsCSVToolStripMenuItem.Name = "copyAsCSVToolStripMenuItem";
            this.copyAsCSVToolStripMenuItem.Size = new Size(0xd4, 0x16);
            this.copyAsCSVToolStripMenuItem.Text = "Copy as CSV";
            this.copyAsCSVToolStripMenuItem.Click += new EventHandler(this.copyAsCSVToolStripMenuItem_Click);
            this.copyAsTSVToolStripMenuItem.Name = "copyAsTSVToolStripMenuItem";
            this.copyAsTSVToolStripMenuItem.Size = new Size(0xd4, 0x16);
            this.copyAsTSVToolStripMenuItem.Text = "Copy as TSV";
            this.copyAsTSVToolStripMenuItem.Click += new EventHandler(this.copyAsTSVToolStripMenuItem_Click);
            this.copyAsHTMLToolStripMenuItem.Name = "copyAsHTMLToolStripMenuItem";
            this.copyAsHTMLToolStripMenuItem.Size = new Size(0xd4, 0x16);
            this.copyAsHTMLToolStripMenuItem.Text = "Copy as HTML";
            this.copyAsHTMLToolStripMenuItem.Click += new EventHandler(this.copyAsHTMLToolStripMenuItem_Click);
            this.copyAsFormattedPlainTextToolStripMenuItem.Name = "copyAsFormattedPlainTextToolStripMenuItem";
            this.copyAsFormattedPlainTextToolStripMenuItem.Size = new Size(0xd4, 0x16);
            this.copyAsFormattedPlainTextToolStripMenuItem.Text = "Copy as Formatted Plain Text";
            this.copyAsFormattedPlainTextToolStripMenuItem.Click += new EventHandler(this.copyAsFormattedPlainTextToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3cf, 0x249);
            base.Controls.Add(this.tableLayoutPanel1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "FormByCombatantLookup";
            this.Text = "Lookup by Player";
            base.FormClosing += new FormClosingEventHandler(this.Form14_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void lv1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            int index = this.iListViewItems.IndexOf(e.ItemIndex);
            if (index > -1)
            {
                e.Item = this.cListViewItems[index];
            }
            else
            {
                ListViewItem item = new ListViewItem {
                    UseItemStyleForSubItems = false
                };
                int num2 = -1;
                string tableType = this.tableType;
                if (tableType != null)
                {
                    if (!(tableType == "CD"))
                    {
                        if (tableType == "DT")
                        {
                            AttackType type = ((List<AttackType>) this.currentTable)[e.ItemIndex];
                            for (int i = 0; i < ActGlobals.oFormActMain.opTableDamageType.clbDT.Items.Count; i++)
                            {
                                string name = (string) ActGlobals.oFormActMain.opTableDamageType.clbDT.Items[i];
                                if (ActGlobals.oFormActMain.opTableDamageType.clbDT.GetItemChecked(i))
                                {
                                    num2++;
                                    if (num2 == 0)
                                    {
                                        item.Text = type.GetColumnByName(name);
                                    }
                                    else
                                    {
                                        item.SubItems.Add(type.GetColumnByName(name));
                                    }
                                }
                            }
                            e.Item = item;
                        }
                        else if (tableType == "AT")
                        {
                            MasterSwing swing = ((List<MasterSwing>) this.currentTable)[e.ItemIndex];
                            for (int j = 0; j < ActGlobals.oFormActMain.opTableAttackType.clbAT.Items.Count; j++)
                            {
                                string str3 = (string) ActGlobals.oFormActMain.opTableAttackType.clbAT.Items[j];
                                if (ActGlobals.oFormActMain.opTableAttackType.clbAT.GetItemChecked(j))
                                {
                                    num2++;
                                    if (num2 == 0)
                                    {
                                        item.Text = swing.GetColumnByName(str3);
                                    }
                                    else
                                    {
                                        item.SubItems.Add(swing.GetColumnByName(str3));
                                    }
                                }
                            }
                            e.Item = item;
                        }
                    }
                    else
                    {
                        DamageTypeData data = ((List<DamageTypeData>) this.currentTable)[e.ItemIndex];
                        for (int k = 0; k < ActGlobals.oFormActMain.opTableCombatant.clbCD.Items.Count; k++)
                        {
                            string str = (string) ActGlobals.oFormActMain.opTableCombatant.clbCD.Items[k];
                            if (ActGlobals.oFormActMain.opTableCombatant.clbCD.GetItemChecked(k))
                            {
                                num2++;
                                if (num2 == 0)
                                {
                                    item.Text = data.GetColumnByName(str);
                                }
                                else
                                {
                                    item.SubItems.Add(data.GetColumnByName(str));
                                }
                            }
                        }
                        e.Item = item;
                    }
                }
                this.iListViewItems.Add(e.ItemIndex);
                this.cListViewItems.Add(e.Item);
            }
        }

        private void PopulateLV()
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string tableType = this.tableType;
            if (tableType != null)
            {
                if (!(tableType == "CD"))
                {
                    if (!(tableType == "DT"))
                    {
                        if (tableType == "AT")
                        {
                            try
                            {
                                this.lv1.VirtualListSize = ((List<MasterSwing>) this.currentTable).Count;
                            }
                            catch (Exception exception3)
                            {
                                ActGlobals.oFormActMain.WriteExceptionLog(exception3, "AT Set Size");
                            }
                            this.lv1.Columns.Clear();
                            List<string> attackTypeColumns = ActGlobals.oFormActMain.GetAttackTypeColumns(true);
                            for (int j = 0; j < attackTypeColumns.Count; j++)
                            {
                                if (attackTypeColumns[j] == ActGlobals.aTSort)
                                {
                                    str2 = str;
                                }
                                else
                                {
                                    str2 = string.Empty;
                                }
                                this.lv1.Columns.Add(str2 + attackTypeColumns[j], 100, HorizontalAlignment.Center);
                            }
                        }
                        return;
                    }
                }
                else
                {
                    try
                    {
                        if (!this.lv1.VirtualMode)
                        {
                            this.lv1.Items.Clear();
                            this.lv1.VirtualMode = true;
                        }
                        this.lv1.VirtualListSize = ((List<DamageTypeData>) this.currentTable).Count;
                    }
                    catch (Exception exception)
                    {
                        ActGlobals.oFormActMain.WriteExceptionLog(exception, "CD Set Size");
                    }
                    this.lv1.Columns.Clear();
                    List<string> combatantDataColumns = ActGlobals.oFormActMain.GetCombatantDataColumns(true);
                    for (int k = 0; k < combatantDataColumns.Count; k++)
                    {
                        string str4 = combatantDataColumns[k];
                        if (str4 == null)
                        {
                            goto Label_01AD;
                        }
                        if (!(str4 == "Type"))
                        {
                            if (str4 == "Average Hit")
                            {
                                goto Label_0141;
                            }
                            if (str4 == "Median Hit")
                            {
                                goto Label_015C;
                            }
                            if (str4 == "Hits")
                            {
                                goto Label_0177;
                            }
                            if (str4 == "Average Delay")
                            {
                                goto Label_0192;
                            }
                            goto Label_01AD;
                        }
                        this.lv1.Columns.Add("Type", 100, HorizontalAlignment.Left);
                        continue;
                    Label_0141:
                        this.lv1.Columns.Add("Average", 100, HorizontalAlignment.Center);
                        continue;
                    Label_015C:
                        this.lv1.Columns.Add("Median", 100, HorizontalAlignment.Center);
                        continue;
                    Label_0177:
                        this.lv1.Columns.Add("Hits  ", 100, HorizontalAlignment.Center);
                        continue;
                    Label_0192:
                        this.lv1.Columns.Add("Avg Dly", 100, HorizontalAlignment.Center);
                        continue;
                    Label_01AD:
                        this.lv1.Columns.Add(combatantDataColumns[k], 100, HorizontalAlignment.Center);
                    }
                    return;
                }
                try
                {
                    this.lv1.VirtualListSize = ((List<AttackType>) this.currentTable).Count;
                }
                catch (Exception exception2)
                {
                    ActGlobals.oFormActMain.WriteExceptionLog(exception2, "DT Set Size");
                }
                this.lv1.Columns.Clear();
                List<string> damageTypeDataColumns = ActGlobals.oFormActMain.GetDamageTypeDataColumns(true);
                for (int i = 0; i < damageTypeDataColumns.Count; i++)
                {
                    if (damageTypeDataColumns[i] == ActGlobals.mDSort)
                    {
                        str2 = str;
                    }
                    else
                    {
                        str2 = string.Empty;
                    }
                    string str5 = damageTypeDataColumns[i];
                    if (str5 == null)
                    {
                        goto Label_035D;
                    }
                    if (!(str5 == "Type"))
                    {
                        if (str5 == "Average Hit")
                        {
                            goto Label_02D6;
                        }
                        if (str5 == "Median Hit")
                        {
                            goto Label_02FA;
                        }
                        if (str5 == "Hits")
                        {
                            goto Label_031B;
                        }
                        if (str5 == "Average Delay")
                        {
                            goto Label_033C;
                        }
                        goto Label_035D;
                    }
                    this.lv1.Columns.Add(str2 + "Type", 100, HorizontalAlignment.Left);
                    continue;
                Label_02D6:
                    this.lv1.Columns.Add(str2 + "Average", 100, HorizontalAlignment.Center);
                    continue;
                Label_02FA:
                    this.lv1.Columns.Add(str2 + "Median", 100, HorizontalAlignment.Center);
                    continue;
                Label_031B:
                    this.lv1.Columns.Add(str2 + "Hits  ", 100, HorizontalAlignment.Center);
                    continue;
                Label_033C:
                    this.lv1.Columns.Add(str2 + "Avg Dly", 100, HorizontalAlignment.Center);
                    continue;
                Label_035D:
                    this.lv1.Columns.Add(str2 + damageTypeDataColumns[i], 100, HorizontalAlignment.Center);
                }
            }
        }

        private void PopulateTView()
        {
            this.tv1.Nodes.Clear();
            TreeNode node = new TreeNode(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText) {
                Tag = "CD"
            };
            this.tv1.Nodes.Add(node);
            for (int i = 0; i < this.lookupRoot.Count; i++)
            {
                node = new TreeNode(this.lookupRoot[i].Type) {
                    Tag = "DT"
                };
                this.tv1.Nodes[0].Nodes.Add(node);
                for (int j = 0; j < this.lookupRoot[i].Items.Count; j++)
                {
                    node = new TreeNode(this.lookupRoot[i].Items.Values[j].Type) {
                        Tag = "AT"
                    };
                    this.tv1.Nodes[0].Nodes[i].Nodes.Add(node);
                }
            }
        }

        public void ShowLookup(string title, DamageTypeData dtInput)
        {
            AttackType type;
            List<MasterSwing> list = new List<MasterSwing>();
            if (dtInput.Items.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
            {
                list = new List<MasterSwing>(type.Items);
            }
            this.lookupRoot = new List<DamageTypeData>();
            for (int i = 0; i < list.Count; i++)
            {
                DamageTypeData data;
                if (dtInput.Outgoing)
                {
                    data = new DamageTypeData(true, list[i].Victim, dtInput.Parent.Parent.GetCombatant(list[i].Victim));
                }
                else
                {
                    data = new DamageTypeData(false, list[i].Attacker, dtInput.Parent.Parent.GetCombatant(list[i].Attacker));
                }
                int index = this.lookupRoot.IndexOf(data);
                if (index > -1)
                {
                    data = this.lookupRoot[index];
                }
                else
                {
                    this.lookupRoot.Add(data);
                }
                MasterSwing action = new MasterSwing(list[i].SwingType, list[i].Critical, list[i].Damage, list[i].Time, list[i].TimeSorter, list[i].AttackType, list[i].Attacker, list[i].DamageType, list[i].Victim);
                data.AddCombatAction(action, ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText);
                data.AddCombatAction(action, action.AttackType);
            }
            this.PopulateTView();
            this.Text = string.Format("{0} Breakdown by Combatant", title);
            base.Show();
        }

        private void tv1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                this.iListViewItems.Clear();
                this.cListViewItems.Clear();
                if (e.Node == null)
                {
                    return;
                }
                this.lv1.BeginUpdate();
                string tag = (string) e.Node.Tag;
                string str2 = tag;
                if (str2 == null)
                {
                    goto Label_0186;
                }
                if (!(str2 == "CD"))
                {
                    if (str2 == "DT")
                    {
                        goto Label_00A1;
                    }
                    if (str2 == "AT")
                    {
                        goto Label_00F8;
                    }
                    goto Label_0186;
                }
                this.tableType = "CD";
                this.lookupRoot.Sort();
                this.currentTable = this.lookupRoot;
                goto Label_0193;
            Label_00A1:
                this.tableType = "DT";
                int index = this.lookupRoot.IndexOf(new DamageTypeData(true, e.Node.Text, null));
                List<AttackType> list = new List<AttackType>(this.lookupRoot[index].Items.Values);
                list.Sort();
                this.currentTable = list;
                goto Label_0193;
            Label_00F8:
                this.tableType = "AT";
                int num2 = this.lookupRoot.IndexOf(new DamageTypeData(true, e.Node.Parent.Text, null));
                List<AttackType> list2 = new List<AttackType>(this.lookupRoot[num2].Items.Values);
                int num3 = list2.IndexOf(new AttackType(e.Node.Text, null));
                list2[num3].Items.Sort();
                this.currentTable = list2[num3].Items;
                goto Label_0193;
            Label_0186:
                this.lv1.EndUpdate();
                return;
            Label_0193:
                this.PopulateLV();
                ActGlobals.oFormActMain.ResizeLVCols(this.lv1);
                this.lv1.EndUpdate();
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }
    }
}

