namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class FormXmlSettingsIO : Form
    {
        private Button btnCancel;
        private Button btnSave;
        private CheckBox cbColorsGraphing;
        private CheckBox cbColorsMain;
        private CheckBox cbColorsMisc;
        private CheckBox cbCustomTriggers;
        private CheckBox cbDataCorrectAbility;
        private CheckBox cbDataCorrectMisc;
        private CheckBox cbDataCorrectRename;
        private CheckBox cbEncCulling;
        private CheckBox cbFormSizes;
        private CheckBox cbLcdColor;
        private CheckBox cbLcdMisc;
        private CheckBox cbLcdMono;
        private CheckBox cbMisc;
        private CheckBox cbOutputGraphing;
        private CheckBox cbOutputHtml;
        private CheckBox cbOutputMini;
        private CheckBox cbOutputOdbc;
        private CheckBox cbOutputText;
        private CheckBox cbOutputWebServer;
        private CheckBox cbPerfWizard;
        private CheckBox cbSelectiveParsing;
        private CheckBox cbSoundSettings;
        private CheckBox cbSpellTimers;
        private CheckBox cbSpellTimersEnabled;
        private CheckBox cbSpellTimersSound;
        private CheckBox cbTableAt;
        private CheckBox cbTableCd;
        private CheckBox cbTableDt;
        private CheckBox cbTableEnc;
        private CheckBox cbTableGeneral;
        private CheckBox cbTableZone;
        private CheckBox cbXmlSnippets;
        private CheckBox cbXmlSubs;
        private CheckBox checkBox17;
        private CheckBox checkBox24;
        private CheckBox checkBox32;
        private CheckBox checkBox5;
        private CheckBox checkBox9;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private LinkLabel linkLabelAppData;

        public FormXmlSettingsIO()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                CheckPathExists = true,
                CreatePrompt = false,
                Filter = "XML Settings File (*.xml)|*.xml",
                Title = "Export Settings to XML",
                AddExtension = true,
                ValidateNames = true,
                OverwritePrompt = false,
                InitialDirectory = ActGlobals.oFormActMain.AppDataFolder.FullName
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileStream w = new FileStream(dialog.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                w.SetLength(0);
                XmlTextWriter xWriter = new XmlTextWriter(w, Encoding.UTF8);
                this.SaveXmlSettingsExport(xWriter);
                xWriter.Flush();
                xWriter.Close();
                base.Hide();
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
            xml.WriteAttributeString("Form", "FormXmlSettingsIO");
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

        private void Form10_FormClosing(object sender, FormClosingEventArgs e)
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
            this.btnCancel = new Button();
            this.btnSave = new Button();
            this.cbMisc = new CheckBox();
            this.groupBox1 = new GroupBox();
            this.checkBox24 = new CheckBox();
            this.checkBox17 = new CheckBox();
            this.cbColorsGraphing = new CheckBox();
            this.cbColorsMain = new CheckBox();
            this.cbOutputHtml = new CheckBox();
            this.cbOutputWebServer = new CheckBox();
            this.cbOutputOdbc = new CheckBox();
            this.cbOutputText = new CheckBox();
            this.cbOutputMini = new CheckBox();
            this.cbOutputGraphing = new CheckBox();
            this.cbTableAt = new CheckBox();
            this.cbTableDt = new CheckBox();
            this.cbTableCd = new CheckBox();
            this.cbTableEnc = new CheckBox();
            this.cbTableZone = new CheckBox();
            this.cbEncCulling = new CheckBox();
            this.cbTableGeneral = new CheckBox();
            this.cbLcdMono = new CheckBox();
            this.cbLcdMisc = new CheckBox();
            this.cbXmlSubs = new CheckBox();
            this.checkBox9 = new CheckBox();
            this.cbLcdColor = new CheckBox();
            this.checkBox5 = new CheckBox();
            this.cbXmlSnippets = new CheckBox();
            this.cbSoundSettings = new CheckBox();
            this.groupBox2 = new GroupBox();
            this.cbFormSizes = new CheckBox();
            this.cbPerfWizard = new CheckBox();
            this.cbSelectiveParsing = new CheckBox();
            this.cbDataCorrectAbility = new CheckBox();
            this.cbDataCorrectMisc = new CheckBox();
            this.cbDataCorrectRename = new CheckBox();
            this.checkBox32 = new CheckBox();
            this.cbSpellTimersSound = new CheckBox();
            this.cbSpellTimersEnabled = new CheckBox();
            this.cbSpellTimers = new CheckBox();
            this.cbCustomTriggers = new CheckBox();
            this.linkLabelAppData = new LinkLabel();
            this.cbColorsMisc = new CheckBox();
            this.label1 = new Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x2cf, 0x183);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnSave.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnSave.Location = new Point(0x27e, 0x183);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(0x4b, 0x17);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            this.cbMisc.AutoSize = true;
            this.cbMisc.Location = new Point(5, 0x12);
            this.cbMisc.Margin = new Padding(2);
            this.cbMisc.Name = "cbMisc";
            this.cbMisc.Size = new Size(0x5d, 0x11);
            this.cbMisc.TabIndex = 0;
            this.cbMisc.Text = "Miscellaneous";
            this.cbMisc.UseVisualStyleBackColor = true;
            this.groupBox1.Controls.Add(this.checkBox24);
            this.groupBox1.Controls.Add(this.checkBox17);
            this.groupBox1.Controls.Add(this.cbDataCorrectAbility);
            this.groupBox1.Controls.Add(this.cbSelectiveParsing);
            this.groupBox1.Controls.Add(this.cbDataCorrectMisc);
            this.groupBox1.Controls.Add(this.cbColorsMisc);
            this.groupBox1.Controls.Add(this.cbDataCorrectRename);
            this.groupBox1.Controls.Add(this.cbColorsGraphing);
            this.groupBox1.Controls.Add(this.checkBox32);
            this.groupBox1.Controls.Add(this.cbColorsMain);
            this.groupBox1.Controls.Add(this.cbOutputHtml);
            this.groupBox1.Controls.Add(this.cbOutputWebServer);
            this.groupBox1.Controls.Add(this.cbOutputOdbc);
            this.groupBox1.Controls.Add(this.cbOutputText);
            this.groupBox1.Controls.Add(this.cbOutputMini);
            this.groupBox1.Controls.Add(this.cbOutputGraphing);
            this.groupBox1.Controls.Add(this.cbTableAt);
            this.groupBox1.Controls.Add(this.cbTableDt);
            this.groupBox1.Controls.Add(this.cbTableCd);
            this.groupBox1.Controls.Add(this.cbTableEnc);
            this.groupBox1.Controls.Add(this.cbTableZone);
            this.groupBox1.Controls.Add(this.cbEncCulling);
            this.groupBox1.Controls.Add(this.cbTableGeneral);
            this.groupBox1.Controls.Add(this.cbLcdMono);
            this.groupBox1.Controls.Add(this.cbLcdMisc);
            this.groupBox1.Controls.Add(this.cbXmlSubs);
            this.groupBox1.Controls.Add(this.checkBox9);
            this.groupBox1.Controls.Add(this.cbLcdColor);
            this.groupBox1.Controls.Add(this.checkBox5);
            this.groupBox1.Controls.Add(this.cbXmlSnippets);
            this.groupBox1.Controls.Add(this.cbSoundSettings);
            this.groupBox1.Controls.Add(this.cbMisc);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1ef, 0x171);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options tab settings";
            this.checkBox24.AutoSize = true;
            this.checkBox24.Enabled = false;
            this.checkBox24.Location = new Point(0xfb, 0xa5);
            this.checkBox24.Margin = new Padding(2);
            this.checkBox24.Name = "checkBox24";
            this.checkBox24.Size = new Size(0x88, 0x11);
            this.checkBox24.TabIndex = 0x17;
            this.checkBox24.Text = "Color and Font Settings";
            this.checkBox24.UseVisualStyleBackColor = true;
            this.checkBox17.AutoSize = true;
            this.checkBox17.Enabled = false;
            this.checkBox17.Location = new Point(0xfb, 0x12);
            this.checkBox17.Margin = new Padding(2);
            this.checkBox17.Name = "checkBox17";
            this.checkBox17.Size = new Size(0x5f, 0x11);
            this.checkBox17.TabIndex = 0x10;
            this.checkBox17.Text = "Output Display";
            this.checkBox17.UseVisualStyleBackColor = true;
            this.cbColorsGraphing.AutoSize = true;
            this.cbColorsGraphing.Location = new Point(0x10d, 0xcf);
            this.cbColorsGraphing.Margin = new Padding(2);
            this.cbColorsGraphing.Name = "cbColorsGraphing";
            this.cbColorsGraphing.Size = new Size(0x45, 0x11);
            this.cbColorsGraphing.TabIndex = 0x19;
            this.cbColorsGraphing.Text = "Graphing";
            this.cbColorsGraphing.UseVisualStyleBackColor = true;
            this.cbColorsMain.AutoSize = true;
            this.cbColorsMain.Location = new Point(0x10d, 0xba);
            this.cbColorsMain.Margin = new Padding(2);
            this.cbColorsMain.Name = "cbColorsMain";
            this.cbColorsMain.Size = new Size(0x77, 0x11);
            this.cbColorsMain.TabIndex = 0x18;
            this.cbColorsMain.Text = "Main User Interface";
            this.cbColorsMain.UseVisualStyleBackColor = true;
            this.cbOutputHtml.AutoSize = true;
            this.cbOutputHtml.Location = new Point(0x10d, 0x90);
            this.cbOutputHtml.Margin = new Padding(2);
            this.cbOutputHtml.Name = "cbOutputHtml";
            this.cbOutputHtml.Size = new Size(130, 0x11);
            this.cbOutputHtml.TabIndex = 0x16;
            this.cbOutputHtml.Text = "HTML File Generation";
            this.cbOutputHtml.UseVisualStyleBackColor = true;
            this.cbOutputWebServer.AutoSize = true;
            this.cbOutputWebServer.Location = new Point(0x10d, 0x7b);
            this.cbOutputWebServer.Margin = new Padding(2);
            this.cbOutputWebServer.Name = "cbOutputWebServer";
            this.cbOutputWebServer.Size = new Size(0x6b, 0x11);
            this.cbOutputWebServer.TabIndex = 0x15;
            this.cbOutputWebServer.Text = "ACT Web Server";
            this.cbOutputWebServer.UseVisualStyleBackColor = true;
            this.cbOutputOdbc.AutoSize = true;
            this.cbOutputOdbc.Location = new Point(0x10d, 0x66);
            this.cbOutputOdbc.Margin = new Padding(2);
            this.cbOutputOdbc.Name = "cbOutputOdbc";
            this.cbOutputOdbc.Size = new Size(0x56, 0x11);
            this.cbOutputOdbc.TabIndex = 20;
            this.cbOutputOdbc.Text = "ODBC (SQL)";
            this.cbOutputOdbc.UseVisualStyleBackColor = true;
            this.cbOutputText.AutoSize = true;
            this.cbOutputText.Location = new Point(0x10d, 0x51);
            this.cbOutputText.Margin = new Padding(2);
            this.cbOutputText.Name = "cbOutputText";
            this.cbOutputText.Size = new Size(0x79, 0x11);
            this.cbOutputText.TabIndex = 0x13;
            this.cbOutputText.Text = "Text Export Settings";
            this.cbOutputText.UseVisualStyleBackColor = true;
            this.cbOutputMini.AutoSize = true;
            this.cbOutputMini.Location = new Point(0x10d, 60);
            this.cbOutputMini.Margin = new Padding(2);
            this.cbOutputMini.Name = "cbOutputMini";
            this.cbOutputMini.Size = new Size(0x75, 0x11);
            this.cbOutputMini.TabIndex = 0x12;
            this.cbOutputMini.Text = "Mini Parse Window";
            this.cbOutputMini.UseVisualStyleBackColor = true;
            this.cbOutputGraphing.AutoSize = true;
            this.cbOutputGraphing.Location = new Point(0x10d, 0x27);
            this.cbOutputGraphing.Margin = new Padding(2);
            this.cbOutputGraphing.Name = "cbOutputGraphing";
            this.cbOutputGraphing.Size = new Size(0x45, 0x11);
            this.cbOutputGraphing.TabIndex = 0x11;
            this.cbOutputGraphing.Text = "Graphing";
            this.cbOutputGraphing.UseVisualStyleBackColor = true;
            this.cbTableAt.AutoSize = true;
            this.cbTableAt.Location = new Point(0x17, 0x14d);
            this.cbTableAt.Margin = new Padding(2);
            this.cbTableAt.Name = "cbTableAt";
            this.cbTableAt.Size = new Size(0x92, 0x11);
            this.cbTableAt.TabIndex = 15;
            this.cbTableAt.Text = "AttackType View Options";
            this.cbTableAt.UseVisualStyleBackColor = true;
            this.cbTableDt.AutoSize = true;
            this.cbTableDt.Location = new Point(0x17, 0x138);
            this.cbTableDt.Margin = new Padding(2);
            this.cbTableDt.Name = "cbTableDt";
            this.cbTableDt.Size = new Size(0x9b, 0x11);
            this.cbTableDt.TabIndex = 14;
            this.cbTableDt.Text = "DamageType View Options";
            this.cbTableDt.UseVisualStyleBackColor = true;
            this.cbTableCd.AutoSize = true;
            this.cbTableCd.Location = new Point(0x17, 0x123);
            this.cbTableCd.Margin = new Padding(2);
            this.cbTableCd.Name = "cbTableCd";
            this.cbTableCd.Size = new Size(0x8e, 0x11);
            this.cbTableCd.TabIndex = 13;
            this.cbTableCd.Text = "Combatant View Options";
            this.cbTableCd.UseVisualStyleBackColor = true;
            this.cbTableEnc.AutoSize = true;
            this.cbTableEnc.Location = new Point(0x17, 270);
            this.cbTableEnc.Margin = new Padding(2);
            this.cbTableEnc.Name = "cbTableEnc";
            this.cbTableEnc.Size = new Size(140, 0x11);
            this.cbTableEnc.TabIndex = 12;
            this.cbTableEnc.Text = "Encounter View Options";
            this.cbTableEnc.UseVisualStyleBackColor = true;
            this.cbTableZone.AutoSize = true;
            this.cbTableZone.Location = new Point(0x17, 0xf9);
            this.cbTableZone.Margin = new Padding(2);
            this.cbTableZone.Name = "cbTableZone";
            this.cbTableZone.Size = new Size(0x74, 0x11);
            this.cbTableZone.TabIndex = 11;
            this.cbTableZone.Text = "Zone View Options";
            this.cbTableZone.UseVisualStyleBackColor = true;
            this.cbEncCulling.AutoSize = true;
            this.cbEncCulling.Location = new Point(0x17, 0xe4);
            this.cbEncCulling.Margin = new Padding(2);
            this.cbEncCulling.Name = "cbEncCulling";
            this.cbEncCulling.Size = new Size(0x6d, 0x11);
            this.cbEncCulling.TabIndex = 10;
            this.cbEncCulling.Text = "Encounter Culling";
            this.cbEncCulling.UseVisualStyleBackColor = true;
            this.cbTableGeneral.AutoSize = true;
            this.cbTableGeneral.Location = new Point(0x17, 0xcf);
            this.cbTableGeneral.Margin = new Padding(2);
            this.cbTableGeneral.Name = "cbTableGeneral";
            this.cbTableGeneral.Size = new Size(0x3f, 0x11);
            this.cbTableGeneral.TabIndex = 9;
            this.cbTableGeneral.Text = "General";
            this.cbTableGeneral.UseVisualStyleBackColor = true;
            this.cbLcdMono.AutoSize = true;
            this.cbLcdMono.Location = new Point(0x17, 0x90);
            this.cbLcdMono.Margin = new Padding(2);
            this.cbLcdMono.Name = "cbLcdMono";
            this.cbLcdMono.Size = new Size(0x97, 0x11);
            this.cbLcdMono.TabIndex = 6;
            this.cbLcdMono.Text = "Monochrome LCD Options";
            this.cbLcdMono.UseVisualStyleBackColor = true;
            this.cbLcdMisc.AutoSize = true;
            this.cbLcdMisc.Location = new Point(5, 0x7b);
            this.cbLcdMisc.Margin = new Padding(2);
            this.cbLcdMisc.Name = "cbLcdMisc";
            this.cbLcdMisc.Size = new Size(0x7b, 0x11);
            this.cbLcdMisc.TabIndex = 5;
            this.cbLcdMisc.Text = "LCD Display Options";
            this.cbLcdMisc.UseVisualStyleBackColor = true;
            this.cbXmlSubs.AutoSize = true;
            this.cbXmlSubs.Location = new Point(0x17, 0x66);
            this.cbXmlSubs.Margin = new Padding(2);
            this.cbXmlSubs.Name = "cbXmlSubs";
            this.cbXmlSubs.Size = new Size(0x93, 0x11);
            this.cbXmlSubs.TabIndex = 4;
            this.cbXmlSubs.Text = "XML Config Subscriptions";
            this.cbXmlSubs.UseVisualStyleBackColor = true;
            this.checkBox9.AutoSize = true;
            this.checkBox9.Enabled = false;
            this.checkBox9.Location = new Point(5, 0xba);
            this.checkBox9.Margin = new Padding(2);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new Size(0x8a, 0x11);
            this.checkBox9.TabIndex = 8;
            this.checkBox9.Text = "Main Table/Encounters";
            this.checkBox9.UseVisualStyleBackColor = true;
            this.cbLcdColor.AutoSize = true;
            this.cbLcdColor.Location = new Point(0x17, 0xa5);
            this.cbLcdColor.Margin = new Padding(2);
            this.cbLcdColor.Name = "cbLcdColor";
            this.cbLcdColor.Size = new Size(0x71, 0x11);
            this.cbLcdColor.TabIndex = 7;
            this.cbLcdColor.Text = "Color LCD Options";
            this.cbLcdColor.UseVisualStyleBackColor = true;
            this.checkBox5.AutoSize = true;
            this.checkBox5.Enabled = false;
            this.checkBox5.Location = new Point(5, 60);
            this.checkBox5.Margin = new Padding(2);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new Size(0x9b, 0x11);
            this.checkBox5.TabIndex = 2;
            this.checkBox5.Text = "Configuration Import/Export";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.cbXmlSnippets.AutoSize = true;
            this.cbXmlSnippets.Location = new Point(0x17, 0x51);
            this.cbXmlSnippets.Margin = new Padding(2);
            this.cbXmlSnippets.Name = "cbXmlSnippets";
            this.cbXmlSnippets.Size = new Size(0x7b, 0x11);
            this.cbXmlSnippets.TabIndex = 3;
            this.cbXmlSnippets.Text = "XML Share Snippets";
            this.cbXmlSnippets.UseVisualStyleBackColor = true;
            this.cbSoundSettings.AutoSize = true;
            this.cbSoundSettings.Location = new Point(5, 0x27);
            this.cbSoundSettings.Margin = new Padding(2);
            this.cbSoundSettings.Name = "cbSoundSettings";
            this.cbSoundSettings.Size = new Size(0x62, 0x11);
            this.cbSoundSettings.TabIndex = 1;
            this.cbSoundSettings.Text = "Sound Settings";
            this.cbSoundSettings.UseVisualStyleBackColor = true;
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbFormSizes);
            this.groupBox2.Controls.Add(this.cbPerfWizard);
            this.groupBox2.Controls.Add(this.cbSpellTimersSound);
            this.groupBox2.Controls.Add(this.cbSpellTimersEnabled);
            this.groupBox2.Controls.Add(this.cbSpellTimers);
            this.groupBox2.Controls.Add(this.cbCustomTriggers);
            this.groupBox2.Location = new Point(0x207, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x113, 0x171);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.cbFormSizes.AutoSize = true;
            this.cbFormSizes.Location = new Point(5, 0x90);
            this.cbFormSizes.Margin = new Padding(2);
            this.cbFormSizes.Name = "cbFormSizes";
            this.cbFormSizes.Size = new Size(0xa8, 0x11);
            this.cbFormSizes.TabIndex = 5;
            this.cbFormSizes.Text = "ACT Window Locations/Sizes";
            this.cbFormSizes.UseVisualStyleBackColor = true;
            this.cbPerfWizard.AutoSize = true;
            this.cbPerfWizard.Location = new Point(5, 0x7b);
            this.cbPerfWizard.Margin = new Padding(2);
            this.cbPerfWizard.Name = "cbPerfWizard";
            this.cbPerfWizard.Size = new Size(0xa3, 0x11);
            this.cbPerfWizard.TabIndex = 4;
            this.cbPerfWizard.Text = "Performance Wizard Settings";
            this.cbPerfWizard.UseVisualStyleBackColor = true;
            this.cbSelectiveParsing.AutoSize = true;
            this.cbSelectiveParsing.Location = new Point(0xfb, 0xf9);
            this.cbSelectiveParsing.Margin = new Padding(2);
            this.cbSelectiveParsing.Name = "cbSelectiveParsing";
            this.cbSelectiveParsing.Size = new Size(0x95, 0x11);
            this.cbSelectiveParsing.TabIndex = 0x1b;
            this.cbSelectiveParsing.Text = "Selective Parsing Settings";
            this.cbSelectiveParsing.UseVisualStyleBackColor = true;
            this.cbDataCorrectAbility.AutoSize = true;
            this.cbDataCorrectAbility.Location = new Point(270, 0x14d);
            this.cbDataCorrectAbility.Margin = new Padding(2);
            this.cbDataCorrectAbility.Name = "cbDataCorrectAbility";
            this.cbDataCorrectAbility.Size = new Size(110, 0x11);
            this.cbDataCorrectAbility.TabIndex = 0x1f;
            this.cbDataCorrectAbility.Text = "Ability Redirection";
            this.cbDataCorrectAbility.UseVisualStyleBackColor = true;
            this.cbDataCorrectMisc.AutoSize = true;
            this.cbDataCorrectMisc.Location = new Point(270, 0x123);
            this.cbDataCorrectMisc.Margin = new Padding(2);
            this.cbDataCorrectMisc.Name = "cbDataCorrectMisc";
            this.cbDataCorrectMisc.Size = new Size(0x5d, 0x11);
            this.cbDataCorrectMisc.TabIndex = 0x1d;
            this.cbDataCorrectMisc.Text = "Miscellaneous";
            this.cbDataCorrectMisc.UseVisualStyleBackColor = true;
            this.cbDataCorrectRename.AutoSize = true;
            this.cbDataCorrectRename.Location = new Point(270, 0x138);
            this.cbDataCorrectRename.Margin = new Padding(2);
            this.cbDataCorrectRename.Name = "cbDataCorrectRename";
            this.cbDataCorrectRename.Size = new Size(0x80, 0x11);
            this.cbDataCorrectRename.TabIndex = 30;
            this.cbDataCorrectRename.Text = "Combatant Renaming";
            this.cbDataCorrectRename.UseVisualStyleBackColor = true;
            this.checkBox32.AutoSize = true;
            this.checkBox32.Enabled = false;
            this.checkBox32.Location = new Point(0xfb, 270);
            this.checkBox32.Margin = new Padding(2);
            this.checkBox32.Name = "checkBox32";
            this.checkBox32.Size = new Size(100, 0x11);
            this.checkBox32.TabIndex = 0x1c;
            this.checkBox32.Text = "Data Correction";
            this.checkBox32.UseVisualStyleBackColor = true;
            this.cbSpellTimersSound.AutoSize = true;
            this.cbSpellTimersSound.Location = new Point(0x18, 0x51);
            this.cbSpellTimersSound.Margin = new Padding(2);
            this.cbSpellTimersSound.Name = "cbSpellTimersSound";
            this.cbSpellTimersSound.Size = new Size(0x99, 0x11);
            this.cbSpellTimersSound.TabIndex = 3;
            this.cbSpellTimersSound.Text = "Spell Timer Sound Settings";
            this.cbSpellTimersSound.UseVisualStyleBackColor = true;
            this.cbSpellTimersEnabled.AutoSize = true;
            this.cbSpellTimersEnabled.Location = new Point(0x18, 60);
            this.cbSpellTimersEnabled.Margin = new Padding(2);
            this.cbSpellTimersEnabled.Name = "cbSpellTimersEnabled";
            this.cbSpellTimersEnabled.Size = new Size(0xab, 0x11);
            this.cbSpellTimersEnabled.TabIndex = 2;
            this.cbSpellTimersEnabled.Text = "Spell Timer Enabled Checkbox";
            this.cbSpellTimersEnabled.UseVisualStyleBackColor = true;
            this.cbSpellTimers.AutoSize = true;
            this.cbSpellTimers.Location = new Point(5, 0x27);
            this.cbSpellTimers.Margin = new Padding(2);
            this.cbSpellTimers.Name = "cbSpellTimers";
            this.cbSpellTimers.Size = new Size(0x61, 0x11);
            this.cbSpellTimers.TabIndex = 1;
            this.cbSpellTimers.Text = "Spell Timer List";
            this.cbSpellTimers.UseVisualStyleBackColor = true;
            this.cbCustomTriggers.AutoSize = true;
            this.cbCustomTriggers.Location = new Point(5, 0x12);
            this.cbCustomTriggers.Margin = new Padding(2);
            this.cbCustomTriggers.Name = "cbCustomTriggers";
            this.cbCustomTriggers.Size = new Size(0x66, 0x11);
            this.cbCustomTriggers.TabIndex = 0;
            this.cbCustomTriggers.Text = "Custom Triggers";
            this.cbCustomTriggers.UseVisualStyleBackColor = true;
            this.linkLabelAppData.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.linkLabelAppData.AutoSize = true;
            this.linkLabelAppData.Location = new Point(9, 0x189);
            this.linkLabelAppData.Name = "linkLabelAppData";
            this.linkLabelAppData.Size = new Size(0x75, 13);
            this.linkLabelAppData.TabIndex = 0;
            this.linkLabelAppData.TabStop = true;
            this.linkLabelAppData.Text = "Application Data Folder";
            this.linkLabelAppData.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabelAppData_LinkClicked);
            this.cbColorsMisc.AutoSize = true;
            this.cbColorsMisc.Location = new Point(0x10d, 0xe4);
            this.cbColorsMisc.Margin = new Padding(2);
            this.cbColorsMisc.Name = "cbColorsMisc";
            this.cbColorsMisc.Size = new Size(0x5d, 0x11);
            this.cbColorsMisc.TabIndex = 0x1a;
            this.cbColorsMisc.Text = "Miscellaneous";
            this.cbColorsMisc.UseVisualStyleBackColor = true;
            this.label1.Location = new Point(6, 0x123);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x107, 0x3b);
            this.label1.TabIndex = 6;
            this.label1.Text = "This feature should not be used to back up your full ACT settings.  Click on the Application Data Folder link and copy the Config folder from it someplace safe.";
            this.label1.TextAlign = ContentAlignment.BottomLeft;
            base.AcceptButton = this.btnSave;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x326, 0x19f);
            base.Controls.Add(this.linkLabelAppData);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnSave);
            base.Controls.Add(this.btnCancel);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new Size(0x32c, 0x1b7);
            base.Name = "FormXmlSettingsIO";
            this.Text = "Save Settings to XML";
            base.TopMost = true;
            base.FormClosing += new FormClosingEventHandler(this.Form10_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void linkLabelAppData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(ActGlobals.oFormActMain.AppDataFolder.FullName);
        }

        internal void SaveXmlSettingsExport(XmlTextWriter xWriter)
        {
            SettingsSerializer serializer = new SettingsSerializer(this);
            xWriter.Formatting = Formatting.Indented;
            xWriter.Indentation = 4;
            xWriter.Namespaces = false;
            xWriter.WriteStartDocument();
            xWriter.WriteStartElement("Config");
            using (FormActMain main = ActGlobals.oFormActMain)
            {
                if (this.cbPerfWizard.Checked)
                {
                    serializer.AddControlSetting(main.opMisc.cbZoneAllListing.Name, main.opMisc.cbZoneAllListing);
                    serializer.AddControlSetting(main.opMisc.cbRestrictToAll.Name, main.opMisc.cbRestrictToAll);
                    serializer.AddControlSetting(main.opGraphing.ddlGraphPriority.Name, main.opGraphing.ddlGraphPriority);
                    serializer.AddControlSetting(main.opMisc.ddlLogPriority.Name, main.opMisc.ddlLogPriority);
                    serializer.AddControlSetting(main.opMiniParse.nudMiniUpdateInterval.Name, main.opMiniParse.nudMiniUpdateInterval);
                    serializer.AddControlSetting(main.opMainTableGen.nudUpdateValue.Name, main.opMainTableGen.nudUpdateValue);
                    serializer.AddControlSetting(main.opGraphing.rbGraphAdv.Name, main.opGraphing.rbGraphAdv);
                    serializer.AddControlSetting(main.opGraphing.rbGraphSimple.Name, main.opGraphing.rbGraphSimple);
                    serializer.AddControlSetting(main.opMisc.cbRecordLogs.Name, main.opMisc.cbRecordLogs);
                }
                if (this.cbMisc.Checked)
                {
                    serializer.AddControlSetting(main.opMisc.cbAutoLoadLogs.Name, main.opMisc.cbAutoLoadLogs);
                    serializer.AddControlSetting(main.opMisc.cbClipAutoConnect.Name, main.opMisc.cbClipAutoConnect);
                    serializer.AddControlSetting(main.opMisc.cbGCollectOnClear.Name, main.opMisc.cbGCollectOnClear);
                    serializer.AddControlSetting(main.opMisc.cbMinimizeToIcon.Name, main.opMisc.cbMinimizeToIcon);
                    serializer.AddControlSetting(main.opMisc.cbRecordLogs.Name, main.opMisc.cbRecordLogs);
                    serializer.AddControlSetting(main.opMisc.cbRestrictToAll.Name, main.opMisc.cbRestrictToAll);
                    serializer.AddControlSetting(main.opMisc.cbZoneAllListing.Name, main.opMisc.cbZoneAllListing);
                    serializer.AddControlSetting(main.opMisc.ddlCpuAffinity.Name, main.opMisc.ddlCpuAffinity);
                    serializer.AddControlSetting(main.opMisc.ddlLogPriority.Name, main.opMisc.ddlLogPriority);
                    serializer.AddControlSetting(main.opMisc.tbClipIP.Name, main.opMisc.tbClipIP);
                }
                if (this.cbSoundSettings.Checked)
                {
                    serializer.AddControlSetting(main.opSound.rbSndExportBeep.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndExportNone.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndExportTTS.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndExportWAV.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndPlugin.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndStartBeep.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndStartNone.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndStartTTS.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndStartWAV.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndTimerBeep.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndTimerNone.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndTimerTTS.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndTimerWAV.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndWarnBeep.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndWarnNone.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndWarnTTS.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndWarnWAV.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndWinApi.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.rbSndWmpApi.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.tbarTtsVol.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.tbarWavVol.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.tbSndExport.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.tbSndStart.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.tbSndTimer.Name, main.opSound.rbSndExportBeep);
                    serializer.AddControlSetting(main.opSound.tbSndWarn.Name, main.opSound.rbSndExportBeep);
                }
                if (this.cbXmlSnippets.Checked)
                {
                    serializer.AddControlSetting(main.opXmlShare.clbShareBanned.Name, main.opXmlShare.clbShareBanned);
                    serializer.AddControlSetting(main.opXmlShare.clbShareTrusted.Name, main.opXmlShare.clbShareTrusted);
                }
                if (this.cbXmlSubs.Checked)
                {
                    main.SaveXmlXmlSubs(xWriter);
                }
                if (this.cbLcdMisc.Checked)
                {
                    serializer.AddControlSetting(main.opLcdGeneral.cbLcdEnabled.Name, main.opLcdGeneral.cbLcdEnabled);
                    serializer.AddControlSetting(main.opLcdGeneral.cbLcdRoute.Name, main.opLcdGeneral.cbLcdRoute);
                    serializer.AddControlSetting(main.opLcdGeneral.ddlLcdMiniFormat.Name, main.opLcdGeneral.ddlLcdMiniFormat);
                    serializer.AddControlSetting(main.opLcdGeneral.ddlLcdPersonalFormat.Name, main.opLcdGeneral.ddlLcdPersonalFormat);
                }
                if (this.cbLcdMono.Checked)
                {
                    serializer.AddControlSetting(main.opLcdMono.btnLcdFont.Name, main.opLcdMono.btnLcdFont);
                    serializer.AddControlSetting(main.opLcdMono.nudLcd0FontOffset.Name, main.opLcdMono.nudLcd0FontOffset);
                    serializer.AddControlSetting(main.opLcdMono.nudLcd0VSpacing.Name, main.opLcdMono.nudLcd0VSpacing);
                    serializer.AddControlSetting(main.opLcdMono.nudLcd1FontOffset.Name, main.opLcdMono.nudLcd1FontOffset);
                    serializer.AddControlSetting(main.opLcdMono.nudLcd1VSpacing.Name, main.opLcdMono.nudLcd1VSpacing);
                    serializer.AddControlSetting(main.opLcdMono.nudLcd2FontOffset.Name, main.opLcdMono.nudLcd2FontOffset);
                    serializer.AddControlSetting(main.opLcdMono.nudLcd2VSpacing.Name, main.opLcdMono.nudLcd2VSpacing);
                    serializer.AddControlSetting(main.opLcdMono.nudLcd3FontOffset.Name, main.opLcdMono.nudLcd3FontOffset);
                    serializer.AddControlSetting(main.opLcdMono.nudLcd3VSpacing.Name, main.opLcdMono.nudLcd3VSpacing);
                }
                if (this.cbLcdColor.Checked)
                {
                    serializer.AddControlSetting(main.opLcdColor.fccLcdMini.Name, main.opLcdColor.fccLcdMini);
                    serializer.AddControlSetting(main.opLcdColor.fccLcdPersonal.Name, main.opLcdColor.fccLcdPersonal);
                    serializer.AddControlSetting(main.opLcdColor.nudLcdMiniVSpacing.Name, main.opLcdColor.nudLcdMiniVSpacing);
                    serializer.AddControlSetting(main.opLcdColor.nudLcdPersonalVSpacing.Name, main.opLcdColor.nudLcdPersonalVSpacing);
                }
                if (this.cbTableGeneral.Checked)
                {
                    serializer.AddControlSetting(main.opMainTableGen.cbIdleEnd.Name, main.opMainTableGen.cbIdleEnd);
                    serializer.AddControlSetting(main.opMainTableGen.cbIdleTimerEnd.Name, main.opMainTableGen.cbIdleTimerEnd);
                    serializer.AddControlSetting(main.opMainTableGen.cbReverseSort.Name, main.opMainTableGen.cbReverseSort);
                    serializer.AddControlSetting(main.opMainTableGen.cbTableCommas.Name, main.opMainTableGen.cbTableCommas);
                    serializer.AddControlSetting(main.opMainTableGen.nudIdleLimit.Name, main.opMainTableGen.nudIdleLimit);
                    serializer.AddControlSetting(main.opMainTableGen.nudUpdateValue.Name, main.opMainTableGen.nudUpdateValue);
                }
                if (this.cbEncCulling.Checked)
                {
                    serializer.AddControlSetting(main.opEncCulling.cbCullAll.Name, main.opEncCulling.cbCullAll);
                    serializer.AddControlSetting(main.opEncCulling.cbCullCount.Name, main.opEncCulling.cbCullCount);
                    serializer.AddControlSetting(main.opEncCulling.cbCullCountIgnoreNoAlly.Name, main.opEncCulling.cbCullCountIgnoreNoAlly);
                    serializer.AddControlSetting(main.opEncCulling.cbCullNoAlly.Name, main.opEncCulling.cbCullNoAlly);
                    serializer.AddControlSetting(main.opEncCulling.cbCullOther.Name, main.opEncCulling.cbCullOther);
                    serializer.AddControlSetting(main.opEncCulling.cbCullTimer.Name, main.opEncCulling.cbCullTimer);
                    serializer.AddControlSetting(main.opEncCulling.nudCullAllN.Name, main.opEncCulling.nudCullAllN);
                    serializer.AddControlSetting(main.opEncCulling.nudCullCountN.Name, main.opEncCulling.nudCullCountN);
                    serializer.AddControlSetting(main.opEncCulling.nudCullOtherN.Name, main.opEncCulling.nudCullOtherN);
                    serializer.AddControlSetting(main.opEncCulling.nudCullTimerN.Name, main.opEncCulling.nudCullTimerN);
                }
                if (this.cbTableZone.Checked)
                {
                    serializer.AddControlSetting(main.opTableZone.clbZD.Name, main.opTableZone.clbZD);
                }
                if (this.cbTableEnc.Checked)
                {
                    serializer.AddControlSetting(main.opTableEncounter.btnEDSort.Name, main.opTableEncounter.btnEDSort);
                    serializer.AddControlSetting(main.opTableEncounter.btnEDSort2.Name, main.opTableEncounter.btnEDSort2);
                    serializer.AddControlSetting(main.opTableEncounter.clbED.Name, main.opTableEncounter.clbED);
                }
                if (this.cbTableCd.Checked)
                {
                    serializer.AddControlSetting(main.opTableCombatant.clbCD.Name, main.opTableCombatant.clbCD);
                }
                if (this.cbTableDt.Checked)
                {
                    serializer.AddControlSetting(main.opTableDamageType.btnMDSort.Name, main.opTableDamageType.btnMDSort);
                    serializer.AddControlSetting(main.opTableDamageType.btnMDSort2.Name, main.opTableDamageType.btnMDSort2);
                    serializer.AddControlSetting(main.opTableDamageType.clbDT.Name, main.opTableDamageType.clbDT);
                }
                if (this.cbTableAt.Checked)
                {
                    serializer.AddControlSetting(main.opTableAttackType.btnATSort.Name, main.opTableAttackType.btnATSort);
                    serializer.AddControlSetting(main.opTableAttackType.btnATSort2.Name, main.opTableAttackType.btnATSort2);
                    serializer.AddControlSetting(main.opTableAttackType.clbAT.Name, main.opTableAttackType.clbAT);
                }
                if (this.cbOutputGraphing.Checked)
                {
                    serializer.AddControlSetting(main.opGraphing.rbGraphAdv.Name, main.opGraphing.rbGraphAdv);
                    serializer.AddControlSetting(main.opGraphing.rbGraphSimple.Name, main.opGraphing.rbGraphSimple);
                    serializer.AddControlSetting(main.opGraphing.cbGraphRollingAvg.Name, main.opGraphing.cbGraphRollingAvg);
                    serializer.AddControlSetting(main.opGraphing.cbOnlyGraphAllies.Name, main.opGraphing.cbOnlyGraphAllies);
                    serializer.AddControlSetting(main.opGraphing.cbSimpleGraphTotals.Name, main.opGraphing.cbSimpleGraphTotals);
                    serializer.AddControlSetting(main.opGraphing.clbSoloGraphTypes.Name, main.opGraphing.clbSoloGraphTypes);
                    serializer.AddControlSetting(main.opGraphing.ddlGraphPriority.Name, main.opGraphing.ddlGraphPriority);
                    serializer.AddControlSetting(main.opGraphing.nudGraphAvg.Name, main.opGraphing.nudGraphAvg);
                }
                if (this.cbOutputMini.Checked)
                {
                    serializer.AddControlSetting(main.opMiniParse.cbMinEncBox.Name, main.opMiniParse.cbMinEncBox);
                    serializer.AddControlSetting(main.opMiniParse.cbMiniClickThrough.Name, main.opMiniParse.cbMiniClickThrough);
                    serializer.AddControlSetting(main.opMiniParse.cbMiniColumnAlign.Name, main.opMiniParse.cbMiniColumnAlign);
                    serializer.AddControlSetting(main.opMiniParse.cbRestoreEncBox.Name, main.opMiniParse.cbRestoreEncBox);
                    serializer.AddControlSetting(main.opMiniParse.cbSmallEncTop.Name, main.opMiniParse.cbSmallEncTop);
                    serializer.AddControlSetting(main.opMiniParse.nudMiniUpdateInterval.Name, main.opMiniParse.nudMiniUpdateInterval);
                    serializer.AddControlSetting(main.opMiniParse.fccMiniParse.Name, main.opMiniParse.fccMiniParse);
                    serializer.AddControlSetting(main.opMiniParse.trbMiniOpacity.Name, main.opMiniParse.trbMiniOpacity);
                    serializer.AddControlSetting(main.opMiniParse.ddlMiniFormat.Name, main.opMiniParse.ddlMiniFormat);
                }
                if (this.cbOutputText.Checked)
                {
                    serializer.AddControlSetting(main.opTextExports.cbExportFilterSpace.Name, main.opTextExports.cbExFileColumnAlign);
                    serializer.AddControlSetting(main.opTextExports.cbExText.Name, main.opTextExports.cbExFileColumnAlign);
                    serializer.AddControlSetting(main.opTextExports.ddlClipFormat.Name, main.opTextExports.cbExFileColumnAlign);
                    main.SaveXmlMacroExports(xWriter);
                }
                if (this.cbOutputOdbc.Checked)
                {
                    serializer.AddControlSetting(main.opOdbc.cbCurrentOdbc.Name, main.opOdbc.cbCurrentOdbc);
                    serializer.AddControlSetting(main.opOdbc.cbExOdbc.Name, main.opOdbc.cbExOdbc);
                    serializer.AddControlSetting(main.opOdbc.cbSqlSafeMode.Name, main.opOdbc.cbSqlSafeMode);
                    serializer.AddControlSetting(main.opOdbc.rbOdbcEx1.Name, main.opOdbc.rbOdbcEx1);
                    serializer.AddControlSetting(main.opOdbc.rbOdbcEx2.Name, main.opOdbc.rbOdbcEx2);
                    serializer.AddControlSetting(main.opOdbc.rbOdbcEx3.Name, main.opOdbc.rbOdbcEx3);
                    serializer.AddControlSetting(main.opOdbc.rbOdbcEx4.Name, main.opOdbc.rbOdbcEx4);
                    serializer.AddControlSetting(main.opOdbc.rbOdbcEx5.Name, main.opOdbc.rbOdbcEx5);
                    serializer.AddControlSetting(main.opOdbc.nudCOdbcDelay.Name, main.opOdbc.nudCOdbcDelay);
                    serializer.AddControlSetting(main.opOdbc.tbOdbcConnectionString.Name, main.opOdbc.tbOdbcConnectionString);
                }
                if (this.cbOutputWebServer.Checked)
                {
                    serializer.AddControlSetting(main.opWebServer.cbWebServerEnabled.Name, main.opWebServer.cbWebServerEnabled);
                    serializer.AddControlSetting(main.opWebServer.cbWebServerShowReq.Name, main.opWebServer.cbWebServerShowReq);
                    serializer.AddControlSetting(main.opWebServer.nudWebServerPort.Name, main.opWebServer.nudWebServerPort);
                }
                if (this.cbOutputHtml.Checked)
                {
                    serializer.AddControlSetting(main.opFileHTML.cbCurrentGraph.Name, main.opFileHTML.cbCurrentGraph);
                    serializer.AddControlSetting(main.opFileHTML.cbCurrentTable.Name, main.opFileHTML.cbCurrentTable);
                    serializer.AddControlSetting(main.opFileHTML.cbExGraph.Name, main.opFileHTML.cbExGraph);
                    serializer.AddControlSetting(main.opFileHTML.cbExHTML.Name, main.opFileHTML.cbExHTML);
                    serializer.AddControlSetting(main.opFileHTML.cbExHTMLFTP.Name, main.opFileHTML.cbExHTMLFTP);
                    serializer.AddControlSetting(main.opFileHTML.cbHTMLCullEncounters.Name, main.opFileHTML.cbHTMLCullEncounters);
                    serializer.AddControlSetting(main.opFileHTML.cbHtmlTimers.Name, main.opFileHTML.cbHtmlTimers);
                    serializer.AddControlSetting(main.opFileHTML.nudCGraphDelay.Name, main.opFileHTML.nudCGraphDelay);
                    serializer.AddControlSetting(main.opFileHTML.nudExFTPPort.Name, main.opFileHTML.nudExFTPPort);
                    serializer.AddControlSetting(main.opFileHTML.nudGraphX.Name, main.opFileHTML.nudGraphX);
                    serializer.AddControlSetting(main.opFileHTML.nudGraphY.Name, main.opFileHTML.nudGraphY);
                    serializer.AddControlSetting(main.opFileHTML.nudHTMLCullingCount.Name, main.opFileHTML.nudHTMLCullingCount);
                    serializer.AddControlSetting(main.opFileHTML.rbExFTPActive.Name, main.opFileHTML.rbExFTPActive);
                    serializer.AddControlSetting(main.opFileHTML.rbExFTPPassive.Name, main.opFileHTML.rbExFTPPassive);
                    serializer.AddControlSetting(main.opFileHTML.tbExFTPPass.Name, main.opFileHTML.tbExFTPPass);
                    serializer.AddControlSetting(main.opFileHTML.tbExFTPPath.Name, main.opFileHTML.tbExFTPPath);
                    serializer.AddControlSetting(main.opFileHTML.tbExFTPServer.Name, main.opFileHTML.tbExFTPServer);
                    serializer.AddControlSetting(main.opFileHTML.tbExFTPUser.Name, main.opFileHTML.tbExFTPUser);
                }
                if (this.cbColorsGraphing.Checked)
                {
                    serializer.AddControlSetting(main.opColorGraphing.ccEncBarAvgText.Name, main.opColorGraphing.ccEncBarAvgText);
                    serializer.AddControlSetting(main.opColorGraphing.ccEncBarBackFill.Name, main.opColorGraphing.ccEncBarBackFill);
                    serializer.AddControlSetting(main.opColorGraphing.ccEncBarColor1.Name, main.opColorGraphing.ccEncBarColor1);
                    serializer.AddControlSetting(main.opColorGraphing.ccEncBarColor2.Name, main.opColorGraphing.ccEncBarColor2);
                    serializer.AddControlSetting(main.opColorGraphing.ccEncBarColor3.Name, main.opColorGraphing.ccEncBarColor3);
                    serializer.AddControlSetting(main.opColorGraphing.ccEncBarLegendText.Name, main.opColorGraphing.ccEncBarLegendText);
                    serializer.AddControlSetting(main.opColorGraphing.ccEncBarOutlining.Name, main.opColorGraphing.ccEncBarOutlining);
                    serializer.AddControlSetting(main.opColorGraphing.ccEncBarText.Name, main.opColorGraphing.ccEncBarText);
                    serializer.AddControlSetting(main.opColorGraphing.ccEncBarYLines.Name, main.opColorGraphing.ccEncBarYLines);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill1.Name, main.opColorGraphing.ccGraphFill1);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill2.Name, main.opColorGraphing.ccGraphFill2);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill3.Name, main.opColorGraphing.ccGraphFill3);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill4.Name, main.opColorGraphing.ccGraphFill4);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill5.Name, main.opColorGraphing.ccGraphFill5);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill6.Name, main.opColorGraphing.ccGraphFill6);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill7.Name, main.opColorGraphing.ccGraphFill7);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill8.Name, main.opColorGraphing.ccGraphFill8);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill9.Name, main.opColorGraphing.ccGraphFill9);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill10.Name, main.opColorGraphing.ccGraphFill10);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill11.Name, main.opColorGraphing.ccGraphFill11);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill12.Name, main.opColorGraphing.ccGraphFill12);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill13.Name, main.opColorGraphing.ccGraphFill13);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill14.Name, main.opColorGraphing.ccGraphFill14);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill15.Name, main.opColorGraphing.ccGraphFill15);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill16.Name, main.opColorGraphing.ccGraphFill16);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill17.Name, main.opColorGraphing.ccGraphFill17);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill18.Name, main.opColorGraphing.ccGraphFill18);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill19.Name, main.opColorGraphing.ccGraphFill19);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill20.Name, main.opColorGraphing.ccGraphFill20);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill21.Name, main.opColorGraphing.ccGraphFill21);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill22.Name, main.opColorGraphing.ccGraphFill22);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill23.Name, main.opColorGraphing.ccGraphFill23);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill24.Name, main.opColorGraphing.ccGraphFill24);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill25.Name, main.opColorGraphing.ccGraphFill25);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill26.Name, main.opColorGraphing.ccGraphFill26);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill27.Name, main.opColorGraphing.ccGraphFill27);
                    serializer.AddControlSetting(main.opColorGraphing.ccGraphFill28.Name, main.opColorGraphing.ccGraphFill28);
                }
                if (this.cbColorsMisc.Checked)
                {
                    serializer.AddControlSetting(main.opColorMisc.ccSpellTimerBackColor.Name, main.opColorMisc.ccSpellTimerBackColor);
                    serializer.AddControlSetting(main.opColorMisc.ccSpellTimerExpireColor.Name, main.opColorMisc.ccSpellTimerExpireColor);
                    serializer.AddControlSetting(main.opColorMisc.ccSpellTimerForeColor.Name, main.opColorMisc.ccSpellTimerForeColor);
                    serializer.AddControlSetting(main.opColorMisc.ccSpellTimerWarnColor.Name, main.opColorMisc.ccSpellTimerWarnColor);
                }
                if (this.cbSelectiveParsing.Checked)
                {
                    main.SaveXmlSParseList(xWriter);
                }
                if (this.cbDataCorrectMisc.Checked)
                {
                    serializer.AddControlSetting(main.opDataCorrectionMisc.cbBlockisHit.Name, main.opDataCorrectionMisc.cbBlockisHit);
                    serializer.AddControlSetting(main.opDataCorrectionMisc.cbCalcRealAvgDly.Name, main.opDataCorrectionMisc.cbCalcRealAvgDly);
                    serializer.AddControlSetting(main.opDataCorrectionMisc.cbLongEncDuration.Name, main.opDataCorrectionMisc.cbLongEncDuration);
                    serializer.AddControlSetting(main.opDataCorrectionMisc.tbCharName.Name, main.opDataCorrectionMisc.tbCharName);
                }
                if (this.cbDataCorrectRename.Checked)
                {
                    main.SaveXmlRenameFix(xWriter);
                }
                if (this.cbDataCorrectAbility.Checked)
                {
                    main.SaveXmlAbilityRedirectFix(xWriter);
                }
                if (this.cbCustomTriggers.Checked)
                {
                    main.SaveXmlCustomTriggers(xWriter);
                }
                if (this.cbSpellTimers.Checked)
                {
                    main.SaveXmlSpellTimers(xWriter, this.cbSpellTimersEnabled.Checked, this.cbSpellTimersSound.Checked);
                }
                if (this.cbFormSizes.Checked)
                {
                    serializer.AddControlSetting(ActGlobals.oFormActMain.Name, ActGlobals.oFormActMain);
                    serializer.AddControlSetting(ActGlobals.oFormMiniParse.Name, ActGlobals.oFormMiniParse);
                    serializer.AddControlSetting(ActGlobals.oFormUpdater.Name, ActGlobals.oFormUpdater);
                    serializer.AddControlSetting(ActGlobals.oFormCombatantSearch.Name, ActGlobals.oFormCombatantSearch);
                    serializer.AddControlSetting(ActGlobals.oFormResistsDeathReport.Name, ActGlobals.oFormResistsDeathReport);
                    serializer.AddControlSetting(ActGlobals.oFormSpellRecastCalc.Name, ActGlobals.oFormSpellRecastCalc);
                    serializer.AddControlSetting(ActGlobals.oFormSpellTimers.Name, ActGlobals.oFormSpellTimers);
                    serializer.AddControlSetting(ActGlobals.oFormSpellTimersPanel.Name, ActGlobals.oFormSpellTimersPanel);
                    serializer.AddControlSetting(ActGlobals.oFormSpellTimersPanel2.Name, ActGlobals.oFormSpellTimersPanel2);
                    serializer.AddControlSetting(ActGlobals.oFormEncounterLogs.Name, ActGlobals.oFormEncounterLogs);
                    serializer.AddControlSetting(ActGlobals.oFormPerformanceWizard.Name, ActGlobals.oFormPerformanceWizard);
                    serializer.AddControlSetting(ActGlobals.oFormXmlSettingsIO.Name, ActGlobals.oFormXmlSettingsIO);
                    serializer.AddControlSetting(ActGlobals.oFormTimeLine.Name, ActGlobals.oFormTimeLine);
                    serializer.AddControlSetting(ActGlobals.oFormEncounterVcr.Name, ActGlobals.oFormEncounterVcr);
                    serializer.AddControlSetting(ActGlobals.oFormSqlQuery.Name, ActGlobals.oFormSqlQuery);
                    serializer.AddControlSetting(ActGlobals.oFormByCombatantLookup.Name, ActGlobals.oFormByCombatantLookup);
                    serializer.AddControlSetting(ActGlobals.oFormStartupWizard.Name, ActGlobals.oFormStartupWizard);
                    serializer.AddControlSetting(ActGlobals.oFormAlliesEdit.Name, ActGlobals.oFormAlliesEdit);
                    serializer.AddControlSetting(ActGlobals.oFormAvoidanceReport.Name, ActGlobals.oFormAvoidanceReport);
                    serializer.AddControlSetting(ActGlobals.oFormExportFormat.Name, ActGlobals.oFormExportFormat);
                    serializer.AddControlSetting(ActGlobals.oFormImportProgress.Name, ActGlobals.oFormImportProgress);
                }
                if (this.cbLcdColor.Checked)
                {
                    serializer.AddControlSetting(main.opLcdColor.fccLcdMini.Name, main.opLcdColor.fccLcdMini);
                    serializer.AddControlSetting(main.opLcdColor.fccLcdPersonal.Name, main.opLcdColor.fccLcdPersonal);
                }
                if (this.cbColorsMain.Checked)
                {
                    serializer.AddControlSetting(main.opColorUserInterface.ccDGAllyText.Name, main.opColorUserInterface.ccDGAllyText);
                    serializer.AddControlSetting(main.opColorUserInterface.ccDGPersonalBackcolor.Name, main.opColorUserInterface.ccDGPersonalBackcolor);
                    serializer.AddControlSetting(main.opColorUserInterface.ccEncLabel1.Name, main.opColorUserInterface.ccEncLabel1);
                    serializer.AddControlSetting(main.opColorUserInterface.ccEncLabel2.Name, main.opColorUserInterface.ccEncLabel2);
                    serializer.AddControlSetting(main.opColorUserInterface.ccEncLabel3.Name, main.opColorUserInterface.ccEncLabel3);
                    serializer.AddControlSetting(main.opColorUserInterface.fccDataGrid.Name, main.opColorUserInterface.fccDataGrid);
                    serializer.AddControlSetting(main.opColorUserInterface.fccMainWindow.Name, main.opColorUserInterface.fccMainWindow);
                    serializer.AddControlSetting(main.opColorUserInterface.fccTreeView.Name, main.opColorUserInterface.fccTreeView);
                    serializer.AddControlSetting(main.opColorUserInterface.fccWindowColors.Name, main.opColorUserInterface.fccWindowColors);
                }
            }
            xWriter.WriteStartElement("SettingsSerializer");
            serializer.ExportToXml(xWriter);
            xWriter.WriteEndElement();
        }
    }
}

