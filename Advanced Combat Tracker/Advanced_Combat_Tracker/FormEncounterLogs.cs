namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Media;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Xml;

    public class FormEncounterLogs : Form
    {
        private Button btnCopyAll;
        private Button btnCopyCombat;
        private Button btnCopyResults;
        private Button btnCopySelection;
        private Button btnFindNext;
        private Button btnFindPrev;
        private Button btnSearchRegex;
        private Button btnSearchText;
        private CheckBox cbOnlyResults;
        private ColumnHeader col1;
        private Container components;
        private int curIndex = -1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private List<LogLineEntry> logLines = new List<LogLineEntry>();
        private ListView lv1;
        private List<LogLineEntry> searchLogLines = new List<LogLineEntry>();
        private TextBox tbSearch;

        public FormEncounterLogs()
        {
            this.InitializeComponent();
        }

        private void btnCopyAll_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            foreach (LogLineEntry entry in this.logLines)
            {
                builder.AppendFormat("{0}\n", entry.LogLine);
            }
            ActGlobals.oFormActMain.SendToClipboard(builder.ToString(), true);
            SystemSounds.Beep.Play();
        }

        private void btnCopyCombat_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            foreach (LogLineEntry entry in this.logLines)
            {
                if ((entry.Type != 0) && (entry.Type != -1))
                {
                    builder.AppendFormat("{0}\n", entry.LogLine);
                }
            }
            ActGlobals.oFormActMain.SendToClipboard(builder.ToString(), true);
            SystemSounds.Beep.Play();
        }

        private void btnCopyResults_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.logLines.Count; i++)
            {
                if (this.logLines[i].SearchSelected)
                {
                    builder.AppendFormat("{0}\n", this.logLines[i].LogLine);
                }
            }
            ActGlobals.oFormActMain.SendToClipboard(builder.ToString(), true);
            SystemSounds.Beep.Play();
        }

        private void btnCopySelection_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            int[] dest = new int[this.lv1.SelectedIndices.Count];
            this.lv1.SelectedIndices.CopyTo(dest, 0);
            if (this.cbOnlyResults.Checked)
            {
                for (int i = 0; i < dest.Length; i++)
                {
                    builder.AppendFormat("{0}\n", this.searchLogLines[dest[i]].LogLine);
                }
            }
            else
            {
                for (int j = 0; j < dest.Length; j++)
                {
                    builder.AppendFormat("{0}\n", this.logLines[dest[j]].LogLine);
                }
            }
            ActGlobals.oFormActMain.SendToClipboard(builder.ToString(), true);
            SystemSounds.Beep.Play();
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            for (int i = this.curIndex + 1; i < this.lv1.Items.Count; i++)
            {
                ListViewItem item = this.lv1.Items[i];
                if (this.logLines[i].SearchSelected)
                {
                    this.lv1.Focus();
                    if (this.curIndex > -1)
                    {
                        this.lv1.Items[this.curIndex].Selected = false;
                    }
                    item.Selected = true;
                    item.EnsureVisible();
                    this.curIndex = item.Index;
                    return;
                }
            }
            SystemSounds.Beep.Play();
        }

        private void btnFindPrev_Click(object sender, EventArgs e)
        {
            for (int i = this.curIndex - 1; i > -1; i--)
            {
                ListViewItem item = this.lv1.Items[i];
                if (this.logLines[i].SearchSelected)
                {
                    this.lv1.Focus();
                    if (this.curIndex > -1)
                    {
                        this.lv1.Items[this.curIndex].Selected = false;
                    }
                    item.Selected = true;
                    item.EnsureVisible();
                    this.curIndex = item.Index;
                    return;
                }
            }
            SystemSounds.Beep.Play();
        }

        private void btnSearchRegex_Click(object sender, EventArgs e)
        {
            try
            {
                this.lv1.BeginUpdate();
                this.cbOnlyResults.Checked = false;
                this.searchLogLines.Clear();
                for (int i = 0; i < this.logLines.Count; i++)
                {
                    if (Regex.IsMatch(this.logLines[i].LogLine, this.tbSearch.Text, RegexOptions.IgnoreCase))
                    {
                        this.logLines[i].SearchSelected = true;
                        this.searchLogLines.Add(this.logLines[i]);
                    }
                    else
                    {
                        this.logLines[i].SearchSelected = false;
                    }
                }
                this.lv1.EndUpdate();
                MessageBox.Show(string.Format(ActGlobals.ActLocalization.LocalizationStrings["messageBox-searchResults"].DisplayedText, this.searchLogLines.Count), ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-searchResults"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.curIndex = -1;
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show(exception.Message);
                this.lv1.EndUpdate();
                this.curIndex = -1;
            }
        }

        private void btnSearchText_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            this.lv1.BeginUpdate();
            this.cbOnlyResults.Checked = false;
            this.searchLogLines.Clear();
            for (int i = 0; i < this.logLines.Count; i++)
            {
                if (this.logLines[i].LogLine.ToUpper().IndexOf(this.tbSearch.Text.ToUpper()) > -1)
                {
                    this.logLines[i].SearchSelected = true;
                    this.searchLogLines.Add(this.logLines[i]);
                }
                else
                {
                    this.logLines[i].SearchSelected = false;
                }
            }
            this.lv1.EndUpdate();
            MessageBox.Show(string.Format(ActGlobals.ActLocalization.LocalizationStrings["messageBox-searchResults"].DisplayedText, this.searchLogLines.Count), ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-searchResults"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.curIndex = -1;
        }

        private void cbOnlyResults_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbOnlyResults.Checked)
            {
                this.lv1.VirtualListSize = this.searchLogLines.Count;
            }
            else
            {
                this.lv1.VirtualListSize = this.logLines.Count;
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

        public void ExportControlTextXML(Stream Output)
        {
            XmlTextWriter xml = new XmlTextWriter(Output, Encoding.UTF8) {
                Formatting = Formatting.Indented,
                Indentation = 4,
                Namespaces = false
            };
            xml.WriteStartDocument();
            xml.WriteStartElement("", "ControlText", "");
            xml.WriteAttributeString("Form", "FormEncounterLogs");
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

        private void Form8_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        private void Form8_Resize(object sender, EventArgs e)
        {
            this.col1.Width = this.lv1.Width - 0x18;
        }

        private Color GetLviForeColor(int Type)
        {
            switch (Type)
            {
                case -1:
                case 0:
                    return Color.Gray;

                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    return Color.Black;
            }
            return Color.FromArgb(Type);
        }

        public void HighlightDateTime(DateTime TimeStamp)
        {
            if ((this.curIndex > -1) && (this.curIndex < this.lv1.VirtualListSize))
            {
                this.lv1.Items[this.curIndex].Selected = false;
            }
            for (int i = 0; i < this.lv1.Items.Count; i++)
            {
                ListViewItem item = this.lv1.Items[i];
                if (this.logLines[i].Time == TimeStamp)
                {
                    item.Selected = true;
                    item.EnsureVisible();
                    this.curIndex = item.Index;
                    break;
                }
            }
            this.lv1.Focus();
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormEncounterLogs));
            this.tbSearch = new TextBox();
            this.groupBox1 = new GroupBox();
            this.btnSearchText = new Button();
            this.btnSearchRegex = new Button();
            this.btnFindNext = new Button();
            this.btnFindPrev = new Button();
            this.lv1 = new ListView();
            this.col1 = new ColumnHeader();
            this.btnCopyResults = new Button();
            this.btnCopyAll = new Button();
            this.btnCopySelection = new Button();
            this.btnCopyCombat = new Button();
            this.groupBox2 = new GroupBox();
            this.cbOnlyResults = new CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.tbSearch.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbSearch.Location = new Point(8, 0x10);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new Size(0x196, 20);
            this.tbSearch.TabIndex = 0;
            this.tbSearch.KeyDown += new KeyEventHandler(this.tbSearch_KeyDown);
            this.groupBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.btnSearchText);
            this.groupBox1.Controls.Add(this.tbSearch);
            this.groupBox1.Controls.Add(this.btnSearchRegex);
            this.groupBox1.Controls.Add(this.btnFindNext);
            this.groupBox1.Controls.Add(this.btnFindPrev);
            this.groupBox1.Location = new Point(8, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(580, 0x30);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Find in Encounter logs...";
            this.btnSearchText.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnSearchText.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnSearchText.Location = new Point(420, 10);
            this.btnSearchText.Name = "btnSearchText";
            this.btnSearchText.Size = new Size(0x58, 0x10);
            this.btnSearchText.TabIndex = 1;
            this.btnSearchText.Text = "Search as Text";
            this.btnSearchText.TextAlign = ContentAlignment.BottomCenter;
            this.btnSearchText.Click += new EventHandler(this.btnSearchText_Click);
            this.btnSearchRegex.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnSearchRegex.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnSearchRegex.Location = new Point(420, 0x1a);
            this.btnSearchRegex.Name = "btnSearchRegex";
            this.btnSearchRegex.Size = new Size(0x58, 0x10);
            this.btnSearchRegex.TabIndex = 1;
            this.btnSearchRegex.Text = "Search as Regex";
            this.btnSearchRegex.TextAlign = ContentAlignment.BottomCenter;
            this.btnSearchRegex.Click += new EventHandler(this.btnSearchRegex_Click);
            this.btnFindNext.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnFindNext.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnFindNext.Location = new Point(0x1fc, 10);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new Size(0x40, 0x10);
            this.btnFindNext.TabIndex = 1;
            this.btnFindNext.Text = "Find Next";
            this.btnFindNext.TextAlign = ContentAlignment.BottomCenter;
            this.btnFindNext.Click += new EventHandler(this.btnFindNext_Click);
            this.btnFindPrev.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnFindPrev.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnFindPrev.Location = new Point(0x1fc, 0x1a);
            this.btnFindPrev.Name = "btnFindPrev";
            this.btnFindPrev.Size = new Size(0x40, 0x10);
            this.btnFindPrev.TabIndex = 1;
            this.btnFindPrev.Text = "Find Prev";
            this.btnFindPrev.TextAlign = ContentAlignment.BottomCenter;
            this.btnFindPrev.Click += new EventHandler(this.btnFindPrev_Click);
            this.lv1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lv1.BackColor = Color.White;
            this.lv1.Columns.AddRange(new ColumnHeader[] { this.col1 });
            this.lv1.ForeColor = Color.Black;
            this.lv1.HeaderStyle = ColumnHeaderStyle.None;
            this.lv1.HideSelection = false;
            this.lv1.Location = new Point(8, 0x36);
            this.lv1.Name = "lv1";
            this.lv1.Size = new Size(0x363, 0x14c);
            this.lv1.TabIndex = 2;
            this.lv1.UseCompatibleStateImageBehavior = false;
            this.lv1.View = View.Details;
            this.lv1.VirtualMode = true;
            this.lv1.MouseUp += new MouseEventHandler(this.lv1_MouseUp);
            this.lv1.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(this.lv1_RetrieveVirtualItem);
            this.col1.Width = 720;
            this.btnCopyResults.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnCopyResults.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnCopyResults.Location = new Point(8, 10);
            this.btnCopyResults.Name = "btnCopyResults";
            this.btnCopyResults.Size = new Size(80, 0x10);
            this.btnCopyResults.TabIndex = 3;
            this.btnCopyResults.Text = "Copy Results";
            this.btnCopyResults.TextAlign = ContentAlignment.BottomCenter;
            this.btnCopyResults.Click += new EventHandler(this.btnCopyResults_Click);
            this.btnCopyAll.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnCopyAll.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnCopyAll.Location = new Point(0x58, 0x1a);
            this.btnCopyAll.Name = "btnCopyAll";
            this.btnCopyAll.Size = new Size(80, 0x10);
            this.btnCopyAll.TabIndex = 3;
            this.btnCopyAll.Text = "Copy All";
            this.btnCopyAll.TextAlign = ContentAlignment.BottomCenter;
            this.btnCopyAll.Click += new EventHandler(this.btnCopyAll_Click);
            this.btnCopySelection.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnCopySelection.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnCopySelection.Location = new Point(0x58, 10);
            this.btnCopySelection.Name = "btnCopySelection";
            this.btnCopySelection.Size = new Size(80, 0x10);
            this.btnCopySelection.TabIndex = 3;
            this.btnCopySelection.Text = "Copy Selection";
            this.btnCopySelection.TextAlign = ContentAlignment.BottomCenter;
            this.btnCopySelection.Click += new EventHandler(this.btnCopySelection_Click);
            this.btnCopyCombat.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnCopyCombat.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnCopyCombat.Location = new Point(8, 0x1a);
            this.btnCopyCombat.Name = "btnCopyCombat";
            this.btnCopyCombat.Size = new Size(80, 0x10);
            this.btnCopyCombat.TabIndex = 3;
            this.btnCopyCombat.Text = "Copy Combat";
            this.btnCopyCombat.TextAlign = ContentAlignment.BottomCenter;
            this.btnCopyCombat.Click += new EventHandler(this.btnCopyCombat_Click);
            this.groupBox2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.btnCopyCombat);
            this.groupBox2.Controls.Add(this.btnCopyResults);
            this.groupBox2.Controls.Add(this.btnCopySelection);
            this.groupBox2.Controls.Add(this.btnCopyAll);
            this.groupBox2.Location = new Point(0x252, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xb0, 0x30);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.cbOnlyResults.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.cbOnlyResults.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cbOnlyResults.Location = new Point(0x308, 10);
            this.cbOnlyResults.Name = "cbOnlyResults";
            this.cbOnlyResults.Size = new Size(0x63, 0x20);
            this.cbOnlyResults.TabIndex = 2;
            this.cbOnlyResults.Text = "Only Show\r\nSearch Results";
            this.cbOnlyResults.UseVisualStyleBackColor = true;
            this.cbOnlyResults.CheckedChanged += new EventHandler(this.cbOnlyResults_CheckedChanged);
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x373, 0x189);
            base.Controls.Add(this.cbOnlyResults);
            base.Controls.Add(this.lv1);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.groupBox2);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "FormEncounterLogs";
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Encounter Logs";
            base.Closing += new CancelEventHandler(this.Form8_Closing);
            base.Resize += new EventHandler(this.Form8_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void lv1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                this.curIndex = this.lv1.GetItemAt(e.X, e.Y).Index;
            }
            catch
            {
                this.curIndex = -1;
            }
        }

        private void lv1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            LogLineEntry entry;
            ListViewItem item = new ListViewItem();
            this.lv1.Items.Clear();
            if (this.cbOnlyResults.Checked)
            {
                entry = this.searchLogLines[e.ItemIndex];
            }
            else
            {
                entry = this.logLines[e.ItemIndex];
            }
            item = new ListViewItem(entry.LogLine) {
                ForeColor = this.GetLviForeColor(entry.Type)
            };
            if (entry.SearchSelected)
            {
                item.BackColor = Color.LemonChiffon;
            }
            else if ((entry.Time.Second % 2) == 0)
            {
                item.BackColor = Color.White;
            }
            else
            {
                item.BackColor = Color.WhiteSmoke;
            }
            e.Item = item;
        }

        public void ScrollToGlobalTimeSorter(int GlobalTimeSorter)
        {
            for (int i = 0; i < this.lv1.Items.Count; i++)
            {
                ListViewItem item = this.lv1.Items[i];
                if (this.logLines[i].GlobalTimeSorter == GlobalTimeSorter)
                {
                    this.lv1.Focus();
                    if ((this.curIndex > -1) && (this.curIndex < this.lv1.VirtualListSize))
                    {
                        this.lv1.Items[this.curIndex].Selected = false;
                    }
                    item.Selected = true;
                    item.EnsureVisible();
                    this.curIndex = item.Index;
                    return;
                }
            }
        }

        public void ShowLogs(List<LogLineEntry> LogLines)
        {
            this.searchLogLines.Clear();
            this.logLines.Clear();
            this.logLines.AddRange(LogLines);
            this.lv1.VirtualListSize = this.logLines.Count;
            this.cbOnlyResults.Checked = false;
            base.Show();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                this.btnSearchText_Click(this.tbSearch, EventArgs.Empty);
            }
        }
    }
}

