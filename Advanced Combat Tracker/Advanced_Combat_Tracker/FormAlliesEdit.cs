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

    public class FormAlliesEdit : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private CheckedListBox cbbCombatants;
        private IContainer components;
        private EncounterData eD;
        private TreeNode selectedNode;
        private TableLayoutPanel tableLayoutPanel1;

        public FormAlliesEdit()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Hide();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<CombatantData> allies = new List<CombatantData>();
            for (int i = 0; i < this.cbbCombatants.Items.Count; i++)
            {
                if (this.cbbCombatants.GetItemChecked(i))
                {
                    allies.Add(this.eD.GetCombatant((string) this.cbbCombatants.Items[i]));
                }
            }
            this.eD.SetAllies(allies);
            this.eD.Title = this.eD.GetStrongestEnemy(ActGlobals.charName);
            ActGlobals.oFormActMain.EncDatabaseAdd(new HistoryRecord(1, this.eD.StartTime, this.eD.EndTime, this.eD.Title, this.eD.CharName));
            this.selectedNode.Text = this.eD.ToString();
            base.Hide();
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
            xml.WriteAttributeString("Form", "FormAlliesEdit");
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

        private void FormAlliesEdit_FormClosing(object sender, FormClosingEventArgs e)
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
            this.cbbCombatants = new CheckedListBox();
            this.btnOK = new Button();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.btnCancel = new Button();
            this.tableLayoutPanel1.SuspendLayout();
            base.SuspendLayout();
            this.cbbCombatants.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbbCombatants.CheckOnClick = true;
            this.cbbCombatants.FormattingEnabled = true;
            this.cbbCombatants.IntegralHeight = false;
            this.cbbCombatants.Location = new Point(12, 12);
            this.cbbCombatants.Name = "cbbCombatants";
            this.cbbCombatants.Size = new Size(0xe3, 0x14d);
            this.cbbCombatants.Sorted = true;
            this.cbbCombatants.TabIndex = 0;
            this.btnOK.Dock = DockStyle.Fill;
            this.btnOK.Location = new Point(3, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(110, 20);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.tableLayoutPanel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel1.Controls.Add(this.btnOK, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 0);
            this.tableLayoutPanel1.Location = new Point(10, 0x159);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel1.Size = new Size(0xe9, 0x1a);
            this.tableLayoutPanel1.TabIndex = 3;
            this.btnCancel.Dock = DockStyle.Fill;
            this.btnCancel.Location = new Point(0x77, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x6f, 20);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0xfb, 0x177);
            base.Controls.Add(this.cbbCombatants);
            base.Controls.Add(this.tableLayoutPanel1);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            this.MinimumSize = new Size(0x103, 0x192);
            base.Name = "FormAlliesEdit";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "Edit Allies for this Encounter";
            base.TopMost = true;
            base.FormClosing += new FormClosingEventHandler(this.FormAlliesEdit_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void ShowAlliesWindow(EncounterData encounter, TreeNode SelectedNode)
        {
            this.eD = encounter;
            List<CombatantData> allies = this.eD.GetAllies();
            this.cbbCombatants.BeginUpdate();
            this.cbbCombatants.Items.Clear();
            List<CombatantData> list2 = new List<CombatantData>(this.eD.Items.Values);
            for (int i = 0; i < list2.Count; i++)
            {
                CombatantData item = list2[i];
                this.cbbCombatants.Items.Add(item.Name, allies.Contains(item));
            }
            this.cbbCombatants.EndUpdate();
            this.selectedNode = SelectedNode;
            base.Show();
        }
    }
}

