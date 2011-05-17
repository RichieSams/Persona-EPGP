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

    public class FormCombatantSearch : Form
    {
        private Button btnClose;
        private Button btnSelectEnc;
        private List<string> combatantList = new List<string>();
        private Container components;
        private ListBox lbCD;
        private ListBox lbEnc;
        private TextBox tbCD;
        private List<ZoneData> zoneList;

        public FormCombatantSearch()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Hide();
        }

        private void btnSelectEnc_Click(object sender, EventArgs e)
        {
            if (this.lbEnc.SelectedIndex > -1)
            {
                try
                {
                    int num = int.Parse(Regex.Replace(this.lbEnc.SelectedItem.ToString(), @"\[(\d+),\d+\] - .+", "$1"));
                    int num2 = int.Parse(Regex.Replace(this.lbEnc.SelectedItem.ToString(), @"\[\d+,(\d+)\] - .+", "$1"));
                    ActGlobals.oFormActMain.MainTreeView.Nodes[num].Expand();
                    ActGlobals.oFormActMain.MainTreeView.SelectedNode = ActGlobals.oFormActMain.MainTreeView.Nodes[num].Nodes[num2];
                }
                catch
                {
                }
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
            xml.WriteAttributeString("Form", "FormCombatantSearch");
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

        private void Form4_Closing(object sender, CancelEventArgs e)
        {
            base.Hide();
            e.Cancel = true;
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

        public void Init(List<ZoneData> zList)
        {
            this.zoneList = zList;
            this.lbCD.Items.Clear();
            this.lbEnc.Items.Clear();
            this.tbCD.Text = string.Empty;
            this.combatantList.Clear();
            for (int i = 0; i < this.zoneList.Count; i++)
            {
                for (int j = 0; j < this.zoneList[i].Items.Count; j++)
                {
                    EncounterData data = this.zoneList[i].Items[j];
                    for (int k = 0; k < data.Items.Values.Count; k++)
                    {
                        bool flag = false;
                        for (int m = 0; m < this.combatantList.Count; m++)
                        {
                            if (this.combatantList[m].ToLower() == data.Items.Values[k].Name.ToLower())
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            this.combatantList.Add(data.Items.Values[k].Name);
                        }
                    }
                }
            }
            foreach (string str in this.combatantList)
            {
                this.lbCD.Items.Add(str);
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormCombatantSearch));
            this.lbCD = new ListBox();
            this.lbEnc = new ListBox();
            this.tbCD = new TextBox();
            this.btnSelectEnc = new Button();
            this.btnClose = new Button();
            base.SuspendLayout();
            this.lbCD.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lbCD.IntegralHeight = false;
            this.lbCD.Location = new Point(8, 8);
            this.lbCD.Name = "lbCD";
            this.lbCD.Size = new Size(0x112, 0xe1);
            this.lbCD.Sorted = true;
            this.lbCD.TabIndex = 0;
            this.lbCD.SelectedIndexChanged += new EventHandler(this.lbCD_SelectedIndexChanged);
            this.lbEnc.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lbEnc.IntegralHeight = false;
            this.lbEnc.Location = new Point(0x120, 8);
            this.lbEnc.Name = "lbEnc";
            this.lbEnc.Size = new Size(0x111, 0xe1);
            this.lbEnc.TabIndex = 0;
            this.lbEnc.DoubleClick += new EventHandler(this.lbEnc_DoubleClick);
            this.tbCD.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.tbCD.Location = new Point(8, 240);
            this.tbCD.Name = "tbCD";
            this.tbCD.Size = new Size(0x112, 20);
            this.tbCD.TabIndex = 1;
            this.tbCD.TextChanged += new EventHandler(this.tbCD_TextChanged);
            this.btnSelectEnc.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnSelectEnc.Location = new Point(0x120, 240);
            this.btnSelectEnc.Name = "btnSelectEnc";
            this.btnSelectEnc.Size = new Size(0x86, 20);
            this.btnSelectEnc.TabIndex = 2;
            this.btnSelectEnc.Text = "Select";
            this.btnSelectEnc.Click += new EventHandler(this.btnSelectEnc_Click);
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnClose.DialogResult = DialogResult.Cancel;
            this.btnClose.Location = new Point(0x1ab, 0xef);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x86, 20);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x239, 0x108);
            base.Controls.Add(this.btnSelectEnc);
            base.Controls.Add(this.tbCD);
            base.Controls.Add(this.lbCD);
            base.Controls.Add(this.lbEnc);
            base.Controls.Add(this.btnClose);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormCombatantSearch";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Combatant Search";
            base.TopMost = true;
            base.Closing += new CancelEventHandler(this.Form4_Closing);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void lbCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbEnc.Items.Clear();
            if (this.lbCD.SelectedIndex != -1)
            {
                string str = (string) this.lbCD.Items[this.lbCD.SelectedIndex];
                for (int i = 0; i < this.zoneList.Count; i++)
                {
                    for (int j = 0; j < this.zoneList[i].Items.Count; j++)
                    {
                        bool flag = false;
                        EncounterData data = this.zoneList[i].Items[j];
                        for (int k = 0; k < data.Items.Count; k++)
                        {
                            if (str == data.Items.Values[k].Name)
                            {
                                flag = true;
                            }
                            if (flag)
                            {
                                this.lbEnc.Items.Add(string.Format("[{0},{1}] - {2}", i, j, data.ToString()));
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void lbEnc_DoubleClick(object sender, EventArgs e)
        {
            if (this.lbEnc.SelectedIndex > -1)
            {
                try
                {
                    int num = int.Parse(Regex.Replace(this.lbEnc.SelectedItem.ToString(), @"\[(\d+),\d+\] - .+", "$1"));
                    int num2 = int.Parse(Regex.Replace(this.lbEnc.SelectedItem.ToString(), @"\[\d+,(\d+)\] - .+", "$1"));
                    ActGlobals.oFormActMain.MainTreeView.Nodes[num].Expand();
                    ActGlobals.oFormActMain.MainTreeView.SelectedNode = ActGlobals.oFormActMain.MainTreeView.Nodes[num].Nodes[num2];
                }
                catch
                {
                }
            }
        }

        private void tbCD_TextChanged(object sender, EventArgs e)
        {
            this.lbCD.Items.Clear();
            Regex regex = new Regex(string.Format(".*{0}.*", this.tbCD.Text), RegexOptions.IgnoreCase);
            foreach (string str in this.combatantList)
            {
                if (regex.IsMatch(str))
                {
                    this.lbCD.Items.Add(str);
                }
            }
        }
    }
}

