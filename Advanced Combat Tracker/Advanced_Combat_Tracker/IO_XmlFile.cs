namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Media;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    internal class IO_XmlFile : UserControl
    {
        private Button btnCancel;
        private Button btnExportXmlFile;
        private IContainer components;
        private Thread exportThread;
        private volatile bool exportThreadActive;
        private GroupBox groupBox1;
        private Label lblXmlFileStatus;
        private RadioButton rbXml1;
        private RadioButton rbXml2;
        private RadioButton rbXml3;
        private System.Windows.Forms.Timer timer500;
        private Dictionary<string, LocalizationObject> Trans = ActGlobals.ActLocalization.LocalizationStrings;

        public IO_XmlFile()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.exportThread.Abort();
            }
            catch
            {
            }
        }

        private void btnExportXmlFile_Click(object sender, EventArgs e)
        {
            this.exportThread = null;
            if (this.rbXml1.Checked)
            {
                this.exportThread = new Thread(new ThreadStart(this.ExportXml1));
            }
            else if (this.rbXml2.Checked)
            {
                this.exportThread = new Thread(new ThreadStart(this.ExportXml2));
            }
            else if (this.rbXml3.Checked)
            {
                this.exportThread = new Thread(new ThreadStart(this.ExportXml3));
            }
            this.exportThread.IsBackground = true;
            this.exportThread.Priority = ThreadPriority.Normal;
            this.exportThread.Name = "XML Export Thread";
            this.exportThread.SetApartmentState(ApartmentState.STA);
            this.exportThread.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void ExportXml(int Depth)
        {
            try
            {
                EncounterData data;
                try
                {
                    if ((ActGlobals.oFormActMain.MainTreeView.SelectedNode.Parent.Parent == null) && (ActGlobals.oFormActMain.MainTreeView.SelectedNode.Parent != null))
                    {
                        ZoneData data2 = ActGlobals.oFormActMain.ZoneList[ActGlobals.oFormActMain.MainTreeView.SelectedNode.Parent.Index];
                        data = data2.Items[ActGlobals.oFormActMain.MainTreeView.SelectedNode.Index];
                        goto Label_00BC;
                    }
                }
                catch
                {
                    MessageBox.Show(this.Trans["messageBox-exportNoEncounters"].DisplayedText, this.Trans["messageBoxTitle-exportNoEncounters"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return;
            Label_00BC:
                this.exportThreadActive = true;
                SaveFileDialog dialog = new SaveFileDialog {
                    AddExtension = true,
                    CheckPathExists = true,
                    CreatePrompt = false,
                    Filter = "XML File (*.xml)|*.xml",
                    OverwritePrompt = true,
                    Title = "Export Encounter to XML",
                    ValidateNames = true
                };
                if ((ActGlobals.oFormActMain.folderExports != null) && ActGlobals.oFormActMain.folderExports.Exists)
                {
                    dialog.InitialDirectory = ActGlobals.oFormActMain.folderExports.FullName;
                }
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    this.exportThreadActive = false;
                    return;
                }
                ActGlobals.oFormActMain.folderExports = new DirectoryInfo(Path.GetDirectoryName(dialog.FileName));
                ThreadInvokes.ControlSetText(ActGlobals.oFormActMain, this.lblXmlFileStatus, "Exporting XML...\nPlease wait...");
                Application.DoEvents();
                DateTime now = DateTime.Now;
                MemoryStream w = new MemoryStream();
                XmlTextWriter writer2 = new XmlTextWriter(w, Encoding.UTF8) {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    Namespaces = false
                };
                writer2.WriteStartDocument();
                writer2.WriteStartElement("", "EncounterTable", "");
                writer2.WriteAttributeString("", "Name", "", data.ToString());
                foreach (CombatantData data3 in data.Items.Values)
                {
                    writer2.WriteStartElement("", "Row", "");
                    for (int i = 0; i < CombatantData.ColHeaderCollection.Length; i++)
                    {
                        writer2.WriteStartElement("", CombatantData.ColHeaderCollection[i], "");
                        writer2.WriteString(data3.ColCollection[i]);
                        writer2.WriteEndElement();
                    }
                    if (Depth > 1)
                    {
                        writer2.WriteStartElement("", "CombatantTable", "");
                        writer2.WriteAttributeString("", "Name", "", data.ToString() + " | " + data3.Name);
                        foreach (DamageTypeData data4 in data3.Items.Values)
                        {
                            writer2.WriteStartElement("", "Row", "");
                            for (int j = 0; j < DamageTypeData.ColHeaderCollection.Length; j++)
                            {
                                writer2.WriteStartElement("", DamageTypeData.ColHeaderCollection[j], "");
                                writer2.WriteString(data4.ColCollection[j]);
                                writer2.WriteEndElement();
                            }
                            writer2.WriteStartElement("", "DamageTypeTable", "");
                            writer2.WriteAttributeString("", "Name", "", data.ToString() + " | " + data3.Name + " | " + data4.Type);
                            foreach (AttackType type in data4.Items.Values)
                            {
                                writer2.WriteStartElement("", "Row", "");
                                for (int k = 0; k < AttackType.ColHeaderCollection.Length; k++)
                                {
                                    writer2.WriteStartElement("", AttackType.ColHeaderCollection[k], "");
                                    writer2.WriteString(type.ColCollection[k]);
                                    writer2.WriteEndElement();
                                }
                                if (Depth > 2)
                                {
                                    writer2.WriteStartElement("", "AttackTypeTable", "");
                                    writer2.WriteAttributeString("", "Name", "", data.ToString() + " | " + data3.Name + " | " + data4.Type + " | " + type.Type);
                                    List<MasterSwing> list = new List<MasterSwing>(type.Items);
                                    foreach (MasterSwing swing in list)
                                    {
                                        writer2.WriteStartElement("", "Row", "");
                                        for (int m = 0; m < MasterSwing.ColHeaderCollection.Length; m++)
                                        {
                                            writer2.WriteStartElement("", MasterSwing.ColHeaderCollection[m], "");
                                            if (m == 0)
                                            {
                                                writer2.WriteString(type.Parent.Parent.Parent.EncId);
                                            }
                                            else
                                            {
                                                writer2.WriteString(swing.ColCollection[m]);
                                            }
                                            writer2.WriteEndElement();
                                        }
                                        writer2.WriteEndElement();
                                    }
                                    writer2.WriteEndElement();
                                }
                                writer2.WriteEndElement();
                            }
                            writer2.WriteEndElement();
                            writer2.WriteEndElement();
                        }
                        writer2.WriteEndElement();
                    }
                    writer2.WriteEndElement();
                }
                writer2.WriteEndElement();
                writer2.WriteEndDocument();
                writer2.Flush();
                w.Seek(0, SeekOrigin.Begin);
                StreamReader reader = new StreamReader(w, Encoding.UTF8);
                StreamWriter writer = new StreamWriter(dialog.FileName, false);
                writer.Write(reader.ReadToEnd());
                writer.Flush();
                writer.Close();
                reader.Close();
                string[] strArray3 = new string[5];
                strArray3[0] = "Export to ";
                strArray3[1] = dialog.FileName;
                strArray3[2] = " has completed.\n";
                TimeSpan span = (TimeSpan) (DateTime.Now - now);
                strArray3[3] = span.TotalSeconds.ToString("F");
                strArray3[4] = " seconds elapsed.";
                ThreadInvokes.ControlSetText(ActGlobals.oFormActMain, this.lblXmlFileStatus, string.Concat(strArray3));
                SystemSounds.Beep.Play();
            }
            catch (ThreadAbortException)
            {
                this.exportThreadActive = false;
                ThreadInvokes.ControlSetText(ActGlobals.oFormActMain, this.lblXmlFileStatus, "Export canceled...");
            }
            catch (Exception exception)
            {
                this.exportThreadActive = false;
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
            this.exportThreadActive = false;
        }

        private void ExportXml1()
        {
            this.ExportXml(1);
        }

        private void ExportXml2()
        {
            this.ExportXml(2);
        }

        private void ExportXml3()
        {
            this.ExportXml(3);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.groupBox1 = new GroupBox();
            this.btnCancel = new Button();
            this.lblXmlFileStatus = new Label();
            this.btnExportXmlFile = new Button();
            this.rbXml1 = new RadioButton();
            this.rbXml2 = new RadioButton();
            this.rbXml3 = new RadioButton();
            this.timer500 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.lblXmlFileStatus);
            this.groupBox1.Location = new Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1b0, 0x4d);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            this.btnCancel.Location = new Point(0x162, 0x33);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.lblXmlFileStatus.Dock = DockStyle.Fill;
            this.lblXmlFileStatus.Location = new Point(3, 0x10);
            this.lblXmlFileStatus.Name = "lblXmlFileStatus";
            this.lblXmlFileStatus.Size = new Size(0x1aa, 0x3a);
            this.lblXmlFileStatus.TabIndex = 0;
            this.btnExportXmlFile.Location = new Point(310, 0x93);
            this.btnExportXmlFile.Name = "btnExportXmlFile";
            this.btnExportXmlFile.Size = new Size(0x7d, 0x17);
            this.btnExportXmlFile.TabIndex = 0;
            this.btnExportXmlFile.Text = "Export...";
            this.btnExportXmlFile.UseVisualStyleBackColor = true;
            this.btnExportXmlFile.Click += new EventHandler(this.btnExportXmlFile_Click);
            this.rbXml1.AutoSize = true;
            this.rbXml1.Checked = true;
            this.rbXml1.Location = new Point(3, 0x54);
            this.rbXml1.Name = "rbXml1";
            this.rbXml1.Size = new Size(170, 0x11);
            this.rbXml1.TabIndex = 2;
            this.rbXml1.TabStop = true;
            this.rbXml1.Text = "Export only the main table data";
            this.rbXml1.UseVisualStyleBackColor = true;
            this.rbXml2.AutoSize = true;
            this.rbXml2.Location = new Point(3, 0x69);
            this.rbXml2.Name = "rbXml2";
            this.rbXml2.Size = new Size(0x12b, 0x11);
            this.rbXml2.TabIndex = 3;
            this.rbXml2.Text = "Export from the main table to the DamageType data tables";
            this.rbXml2.UseVisualStyleBackColor = true;
            this.rbXml3.AutoSize = true;
            this.rbXml3.Location = new Point(3, 0x7e);
            this.rbXml3.Name = "rbXml3";
            this.rbXml3.Size = new Size(0xdf, 0x11);
            this.rbXml3.TabIndex = 4;
            this.rbXml3.Text = "Export all tables of the selected encounter";
            this.rbXml3.UseVisualStyleBackColor = true;
            this.timer500.Enabled = true;
            this.timer500.Interval = 500;
            this.timer500.Tick += new EventHandler(this.timer500_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.rbXml3);
            base.Controls.Add(this.rbXml2);
            base.Controls.Add(this.rbXml1);
            base.Controls.Add(this.btnExportXmlFile);
            base.Controls.Add(this.groupBox1);
            base.Name = "IO_XmlFile";
            base.Size = new Size(0x1b6, 0xad);
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void timer500_Tick(object sender, EventArgs e)
        {
            this.btnCancel.Visible = this.exportThreadActive;
        }
    }
}

