namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class FormPerformanceWizard : Form
    {
        private Button btnCancel;
        private Button btnCancel1;
        private Button btnCancel2;
        private Button btnCancel3;
        private Button btnCancel4;
        private Button btnCancel5;
        private Button btnCancel6;
        private Button btnCancel7;
        private Button btnCancel8;
        private Button btnOk;
        private Button btnTab1Next;
        private Button btnTab2Back;
        private Button btnTab2Next;
        private Button btnTab3Back;
        private Button btnTab3Next;
        private Button btnTab4Back;
        private Button btnTab4Next;
        private Button btnTab5Back;
        private Button btnTab5Next;
        private Button btnTab6Back;
        private Button btnTab6Next;
        private Button btnTab7Back;
        private Button btnTab7Next;
        private Button btnTab8Back;
        private Button btnTab8Next;
        private Button btnTab9Back;
        private Button button2;
        private Button button5;
        private CheckBox cbRecordLogs;
        private CheckBox cbRestrictToAll;
        private CheckBox cbZoneAllListing;
        private IContainer components;
        private ComboBox ddlGraphPriority;
        private ComboBox ddlLogPriority;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label2;
        private Label label20;
        private Label label22;
        private Label label23;
        private Label label25;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblLogPriority;
        private Label lblS2A;
        private Label lblS2B;
        private Label lblS3A;
        private Label lblS3B;
        private Label lblS4A;
        private Label lblS4B;
        private Label lblS5A;
        private Label lblS5B;
        private Label lblS6A;
        private Label lblS6B;
        private Label lblS7A;
        private Label lblS7B;
        private Label lblS8A;
        private Label lblS8B;
        private NumericUpDown nudMiniUpdateInterval;
        private NumericUpDown nudUpdateValue;
        private RadioButton rbGraphAdv;
        private RadioButton rbGraphSimple;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private TabPage tabPage8;
        private TabPage tabPage9;
        private TabControl tc1;

        public FormPerformanceWizard()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Hide();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.opMisc.cbZoneAllListing.Checked = this.cbZoneAllListing.Checked;
            ActGlobals.oFormActMain.opMisc.cbRestrictToAll.Checked = this.cbRestrictToAll.Checked;
            ActGlobals.oFormActMain.opGraphing.ddlGraphPriority.Text = this.ddlGraphPriority.Text;
            ActGlobals.oFormActMain.opMisc.ddlLogPriority.Text = this.ddlLogPriority.Text;
            ActGlobals.oFormActMain.opMiniParse.nudMiniUpdateInterval.Value = this.nudMiniUpdateInterval.Value;
            ActGlobals.oFormActMain.opMainTableGen.nudUpdateValue.Value = this.nudUpdateValue.Value;
            ActGlobals.oFormActMain.opGraphing.rbGraphAdv.Checked = this.rbGraphAdv.Checked;
            ActGlobals.oFormActMain.opGraphing.rbGraphSimple.Checked = this.rbGraphSimple.Checked;
            ActGlobals.oFormActMain.opMisc.cbRecordLogs.Checked = this.cbRecordLogs.Checked;
            base.Hide();
        }

        private void btnTabBack_Click(object sender, EventArgs e)
        {
            this.tc1.SelectedIndex--;
        }

        private void btnTabNext_Click(object sender, EventArgs e)
        {
            this.tc1.SelectedIndex++;
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
            xml.WriteAttributeString("Form", "FormPerformanceWizard");
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

        private void Form9_FormClosing(object sender, FormClosingEventArgs e)
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormPerformanceWizard));
            this.tc1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.btnCancel1 = new Button();
            this.button2 = new Button();
            this.btnTab1Next = new Button();
            this.label1 = new Label();
            this.tabPage2 = new TabPage();
            this.btnCancel2 = new Button();
            this.btnTab2Back = new Button();
            this.btnTab2Next = new Button();
            this.label2 = new Label();
            this.ddlLogPriority = new ComboBox();
            this.lblLogPriority = new Label();
            this.tabPage3 = new TabPage();
            this.btnCancel3 = new Button();
            this.btnTab3Back = new Button();
            this.btnTab3Next = new Button();
            this.label3 = new Label();
            this.cbZoneAllListing = new CheckBox();
            this.tabPage4 = new TabPage();
            this.btnCancel4 = new Button();
            this.btnTab4Back = new Button();
            this.btnTab4Next = new Button();
            this.label4 = new Label();
            this.cbRestrictToAll = new CheckBox();
            this.tabPage5 = new TabPage();
            this.btnCancel5 = new Button();
            this.btnTab5Back = new Button();
            this.btnTab5Next = new Button();
            this.label6 = new Label();
            this.label23 = new Label();
            this.label22 = new Label();
            this.nudMiniUpdateInterval = new NumericUpDown();
            this.nudUpdateValue = new NumericUpDown();
            this.label5 = new Label();
            this.tabPage6 = new TabPage();
            this.btnCancel6 = new Button();
            this.btnTab6Back = new Button();
            this.btnTab6Next = new Button();
            this.label7 = new Label();
            this.rbGraphSimple = new RadioButton();
            this.rbGraphAdv = new RadioButton();
            this.tabPage7 = new TabPage();
            this.btnCancel7 = new Button();
            this.btnTab7Back = new Button();
            this.btnTab7Next = new Button();
            this.label8 = new Label();
            this.label11 = new Label();
            this.ddlGraphPriority = new ComboBox();
            this.tabPage9 = new TabPage();
            this.btnCancel8 = new Button();
            this.btnTab8Back = new Button();
            this.btnTab8Next = new Button();
            this.cbRecordLogs = new CheckBox();
            this.label20 = new Label();
            this.tabPage8 = new TabPage();
            this.lblS8A = new Label();
            this.lblS8B = new Label();
            this.label25 = new Label();
            this.lblS7A = new Label();
            this.lblS6A = new Label();
            this.lblS5A = new Label();
            this.lblS4A = new Label();
            this.lblS3A = new Label();
            this.lblS2A = new Label();
            this.label12 = new Label();
            this.lblS7B = new Label();
            this.lblS6B = new Label();
            this.lblS5B = new Label();
            this.lblS4B = new Label();
            this.lblS3B = new Label();
            this.lblS2B = new Label();
            this.label10 = new Label();
            this.label19 = new Label();
            this.label18 = new Label();
            this.label17 = new Label();
            this.label15 = new Label();
            this.label14 = new Label();
            this.label13 = new Label();
            this.label9 = new Label();
            this.btnOk = new Button();
            this.btnCancel = new Button();
            this.button5 = new Button();
            this.btnTab9Back = new Button();
            this.tc1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.nudMiniUpdateInterval.BeginInit();
            this.nudUpdateValue.BeginInit();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabPage8.SuspendLayout();
            base.SuspendLayout();
            this.tc1.Appearance = TabAppearance.FlatButtons;
            this.tc1.Controls.Add(this.tabPage1);
            this.tc1.Controls.Add(this.tabPage2);
            this.tc1.Controls.Add(this.tabPage3);
            this.tc1.Controls.Add(this.tabPage4);
            this.tc1.Controls.Add(this.tabPage5);
            this.tc1.Controls.Add(this.tabPage6);
            this.tc1.Controls.Add(this.tabPage7);
            this.tc1.Controls.Add(this.tabPage9);
            this.tc1.Controls.Add(this.tabPage8);
            this.tc1.Dock = DockStyle.Fill;
            this.tc1.Location = new Point(0, 0);
            this.tc1.Name = "tc1";
            this.tc1.SelectedIndex = 0;
            this.tc1.Size = new Size(0x200, 0x16b);
            this.tc1.TabIndex = 0;
            this.tc1.SelectedIndexChanged += new EventHandler(this.tc1_SelectedIndexChanged);
            this.tabPage1.Controls.Add(this.btnCancel1);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.btnTab1Next);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new Point(4, 0x19);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x1f8, 0x14e);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Step 1 ";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.btnCancel1.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel1.DialogResult = DialogResult.Cancel;
            this.btnCancel1.Location = new Point(0x103, 0x134);
            this.btnCancel1.Name = "btnCancel1";
            this.btnCancel1.Size = new Size(0x4b, 0x17);
            this.btnCancel1.TabIndex = 0x2b;
            this.btnCancel1.Text = "Cancel";
            this.btnCancel1.UseVisualStyleBackColor = true;
            this.btnCancel1.Click += new EventHandler(this.btnCancel_Click);
            this.button2.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.button2.Enabled = false;
            this.button2.Location = new Point(340, 0x134);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 2;
            this.button2.Text = "< Back";
            this.button2.UseVisualStyleBackColor = true;
            this.btnTab1Next.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab1Next.Location = new Point(0x1a5, 0x134);
            this.btnTab1Next.Name = "btnTab1Next";
            this.btnTab1Next.Size = new Size(0x4b, 0x17);
            this.btnTab1Next.TabIndex = 1;
            this.btnTab1Next.Text = "Next >";
            this.btnTab1Next.UseVisualStyleBackColor = true;
            this.btnTab1Next.Click += new EventHandler(this.btnTabNext_Click);
            this.label1.Location = new Point(8, 0x1f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1e8, 0x6d);
            this.label1.TabIndex = 0;
            this.label1.Text = manager.GetString("label1.Text");
            this.tabPage2.Controls.Add(this.btnCancel2);
            this.tabPage2.Controls.Add(this.btnTab2Back);
            this.tabPage2.Controls.Add(this.btnTab2Next);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.ddlLogPriority);
            this.tabPage2.Controls.Add(this.lblLogPriority);
            this.tabPage2.Location = new Point(4, 0x19);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x1f8, 0x14e);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Step 2 ";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.btnCancel2.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel2.DialogResult = DialogResult.Cancel;
            this.btnCancel2.Location = new Point(0x103, 0x134);
            this.btnCancel2.Name = "btnCancel2";
            this.btnCancel2.Size = new Size(0x4b, 0x17);
            this.btnCancel2.TabIndex = 0x2b;
            this.btnCancel2.Text = "Cancel";
            this.btnCancel2.UseVisualStyleBackColor = true;
            this.btnCancel2.Click += new EventHandler(this.btnCancel_Click);
            this.btnTab2Back.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab2Back.Location = new Point(340, 0x134);
            this.btnTab2Back.Name = "btnTab2Back";
            this.btnTab2Back.Size = new Size(0x4b, 0x17);
            this.btnTab2Back.TabIndex = 30;
            this.btnTab2Back.Text = "< Back";
            this.btnTab2Back.UseVisualStyleBackColor = true;
            this.btnTab2Back.Click += new EventHandler(this.btnTabBack_Click);
            this.btnTab2Next.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab2Next.Location = new Point(0x1a5, 0x134);
            this.btnTab2Next.Name = "btnTab2Next";
            this.btnTab2Next.Size = new Size(0x4b, 0x17);
            this.btnTab2Next.TabIndex = 0x1d;
            this.btnTab2Next.Text = "Next >";
            this.btnTab2Next.UseVisualStyleBackColor = true;
            this.btnTab2Next.Click += new EventHandler(this.btnTabNext_Click);
            this.label2.Location = new Point(11, 0x1f);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1e5, 0xae);
            this.label2.TabIndex = 0x1c;
            this.label2.Text = manager.GetString("label2.Text");
            this.ddlLogPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlLogPriority.Items.AddRange(new object[] { "Above Normal", "Normal", "Below Normal", "Lowest" });
            this.ddlLogPriority.Location = new Point(0xd0, 6);
            this.ddlLogPriority.Name = "ddlLogPriority";
            this.ddlLogPriority.Size = new Size(0x88, 0x15);
            this.ddlLogPriority.TabIndex = 0x1a;
            this.lblLogPriority.Location = new Point(11, 3);
            this.lblLogPriority.Name = "lblLogPriority";
            this.lblLogPriority.Size = new Size(0xb2, 0x18);
            this.lblLogPriority.TabIndex = 0x1b;
            this.lblLogPriority.Text = "CPU Priority of normal log parsing:";
            this.lblLogPriority.TextAlign = ContentAlignment.MiddleLeft;
            this.tabPage3.Controls.Add(this.btnCancel3);
            this.tabPage3.Controls.Add(this.btnTab3Back);
            this.tabPage3.Controls.Add(this.btnTab3Next);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.cbZoneAllListing);
            this.tabPage3.Location = new Point(4, 0x19);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(3);
            this.tabPage3.Size = new Size(0x1f8, 0x14e);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Step 3 ";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.btnCancel3.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel3.DialogResult = DialogResult.Cancel;
            this.btnCancel3.Location = new Point(0x103, 0x134);
            this.btnCancel3.Name = "btnCancel3";
            this.btnCancel3.Size = new Size(0x4b, 0x17);
            this.btnCancel3.TabIndex = 0x2b;
            this.btnCancel3.Text = "Cancel";
            this.btnCancel3.UseVisualStyleBackColor = true;
            this.btnCancel3.Click += new EventHandler(this.btnCancel_Click);
            this.btnTab3Back.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab3Back.Location = new Point(340, 0x134);
            this.btnTab3Back.Name = "btnTab3Back";
            this.btnTab3Back.Size = new Size(0x4b, 0x17);
            this.btnTab3Back.TabIndex = 0x20;
            this.btnTab3Back.Text = "< Back";
            this.btnTab3Back.UseVisualStyleBackColor = true;
            this.btnTab3Back.Click += new EventHandler(this.btnTabBack_Click);
            this.btnTab3Next.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab3Next.Location = new Point(0x1a5, 0x134);
            this.btnTab3Next.Name = "btnTab3Next";
            this.btnTab3Next.Size = new Size(0x4b, 0x17);
            this.btnTab3Next.TabIndex = 0x1f;
            this.btnTab3Next.Text = "Next >";
            this.btnTab3Next.UseVisualStyleBackColor = true;
            this.btnTab3Next.Click += new EventHandler(this.btnTabNext_Click);
            this.label3.Location = new Point(11, 0x2c);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1e5, 0x92);
            this.label3.TabIndex = 0x17;
            this.label3.Text = manager.GetString("label3.Text");
            this.cbZoneAllListing.Checked = true;
            this.cbZoneAllListing.CheckState = CheckState.Checked;
            this.cbZoneAllListing.Location = new Point(8, 6);
            this.cbZoneAllListing.Name = "cbZoneAllListing";
            this.cbZoneAllListing.Size = new Size(0x1b3, 0x23);
            this.cbZoneAllListing.TabIndex = 0x16;
            this.cbZoneAllListing.Text = "Populate an 'All' encounter including all data from separate encounters within a zone.  (Not Retroactive)";
            this.tabPage4.Controls.Add(this.btnCancel4);
            this.tabPage4.Controls.Add(this.btnTab4Back);
            this.tabPage4.Controls.Add(this.btnTab4Next);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.cbRestrictToAll);
            this.tabPage4.Location = new Point(4, 0x19);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new Padding(3);
            this.tabPage4.Size = new Size(0x1f8, 0x14e);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Step 4 ";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.btnCancel4.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel4.DialogResult = DialogResult.Cancel;
            this.btnCancel4.Location = new Point(0x103, 0x134);
            this.btnCancel4.Name = "btnCancel4";
            this.btnCancel4.Size = new Size(0x4b, 0x17);
            this.btnCancel4.TabIndex = 0x2b;
            this.btnCancel4.Text = "Cancel";
            this.btnCancel4.UseVisualStyleBackColor = true;
            this.btnCancel4.Click += new EventHandler(this.btnCancel_Click);
            this.btnTab4Back.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab4Back.Location = new Point(340, 0x134);
            this.btnTab4Back.Name = "btnTab4Back";
            this.btnTab4Back.Size = new Size(0x4b, 0x17);
            this.btnTab4Back.TabIndex = 0x22;
            this.btnTab4Back.Text = "< Back";
            this.btnTab4Back.UseVisualStyleBackColor = true;
            this.btnTab4Back.Click += new EventHandler(this.btnTabBack_Click);
            this.btnTab4Next.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab4Next.Location = new Point(0x1a5, 0x134);
            this.btnTab4Next.Name = "btnTab4Next";
            this.btnTab4Next.Size = new Size(0x4b, 0x17);
            this.btnTab4Next.TabIndex = 0x21;
            this.btnTab4Next.Text = "Next >";
            this.btnTab4Next.UseVisualStyleBackColor = true;
            this.btnTab4Next.Click += new EventHandler(this.btnTabNext_Click);
            this.label4.Location = new Point(11, 0x1f);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1e5, 170);
            this.label4.TabIndex = 0x16;
            this.label4.Text = manager.GetString("label4.Text");
            this.cbRestrictToAll.Location = new Point(8, 6);
            this.cbRestrictToAll.Name = "cbRestrictToAll";
            this.cbRestrictToAll.Size = new Size(430, 0x10);
            this.cbRestrictToAll.TabIndex = 0x15;
            this.cbRestrictToAll.Text = "Only populate the 'All' entry for DamageType categories except those marked (Ref).";
            this.tabPage5.Controls.Add(this.btnCancel5);
            this.tabPage5.Controls.Add(this.btnTab5Back);
            this.tabPage5.Controls.Add(this.btnTab5Next);
            this.tabPage5.Controls.Add(this.label6);
            this.tabPage5.Controls.Add(this.label23);
            this.tabPage5.Controls.Add(this.label22);
            this.tabPage5.Controls.Add(this.nudMiniUpdateInterval);
            this.tabPage5.Controls.Add(this.nudUpdateValue);
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Location = new Point(4, 0x19);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new Padding(3);
            this.tabPage5.Size = new Size(0x1f8, 0x14e);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Step 5 ";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.btnCancel5.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel5.DialogResult = DialogResult.Cancel;
            this.btnCancel5.Location = new Point(0x103, 0x134);
            this.btnCancel5.Name = "btnCancel5";
            this.btnCancel5.Size = new Size(0x4b, 0x17);
            this.btnCancel5.TabIndex = 0x2b;
            this.btnCancel5.Text = "Cancel";
            this.btnCancel5.UseVisualStyleBackColor = true;
            this.btnCancel5.Click += new EventHandler(this.btnCancel_Click);
            this.btnTab5Back.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab5Back.Location = new Point(340, 0x134);
            this.btnTab5Back.Name = "btnTab5Back";
            this.btnTab5Back.Size = new Size(0x4b, 0x17);
            this.btnTab5Back.TabIndex = 0x24;
            this.btnTab5Back.Text = "< Back";
            this.btnTab5Back.UseVisualStyleBackColor = true;
            this.btnTab5Back.Click += new EventHandler(this.btnTabBack_Click);
            this.btnTab5Next.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab5Next.Location = new Point(0x1a5, 0x134);
            this.btnTab5Next.Name = "btnTab5Next";
            this.btnTab5Next.Size = new Size(0x4b, 0x17);
            this.btnTab5Next.TabIndex = 0x23;
            this.btnTab5Next.Text = "Next >";
            this.btnTab5Next.UseVisualStyleBackColor = true;
            this.btnTab5Next.Click += new EventHandler(this.btnTabNext_Click);
            this.label6.Location = new Point(11, 0x4a);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1e5, 0x71);
            this.label6.TabIndex = 0x19;
            this.label6.Text = manager.GetString("label6.Text");
            this.label23.Location = new Point(0xfb, 0x31);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x35, 0x10);
            this.label23.TabIndex = 0x18;
            this.label23.Text = "seconds.";
            this.label22.Location = new Point(6, 0x31);
            this.label22.Name = "label22";
            this.label22.Size = new Size(190, 0x10);
            this.label22.TabIndex = 0x17;
            this.label22.Text = "Mini Parse window update interval:";
            this.nudMiniUpdateInterval.Location = new Point(0xc5, 0x2d);
            int[] bits = new int[4];
            bits[0] = 60;
            this.nudMiniUpdateInterval.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.nudMiniUpdateInterval.Minimum = new decimal(numArray2);
            this.nudMiniUpdateInterval.Name = "nudMiniUpdateInterval";
            this.nudMiniUpdateInterval.Size = new Size(0x30, 20);
            this.nudMiniUpdateInterval.TabIndex = 0x16;
            int[] numArray3 = new int[4];
            numArray3[0] = 5;
            this.nudMiniUpdateInterval.Value = new decimal(numArray3);
            this.nudUpdateValue.Location = new Point(8, 6);
            int[] numArray4 = new int[4];
            numArray4[0] = 60;
            this.nudUpdateValue.Maximum = new decimal(numArray4);
            this.nudUpdateValue.Name = "nudUpdateValue";
            this.nudUpdateValue.Size = new Size(0x30, 20);
            this.nudUpdateValue.TabIndex = 20;
            int[] numArray5 = new int[4];
            numArray5[0] = 5;
            this.nudUpdateValue.Value = new decimal(numArray5);
            this.label5.Location = new Point(0x3e, 10);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0xb7, 0x10);
            this.label5.TabIndex = 0x12;
            this.label5.Text = "Update Table every N seconds.";
            this.tabPage6.Controls.Add(this.btnCancel6);
            this.tabPage6.Controls.Add(this.btnTab6Back);
            this.tabPage6.Controls.Add(this.btnTab6Next);
            this.tabPage6.Controls.Add(this.label7);
            this.tabPage6.Controls.Add(this.rbGraphSimple);
            this.tabPage6.Controls.Add(this.rbGraphAdv);
            this.tabPage6.Location = new Point(4, 0x19);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new Padding(3);
            this.tabPage6.Size = new Size(0x1f8, 0x14e);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Step 6 ";
            this.tabPage6.UseVisualStyleBackColor = true;
            this.btnCancel6.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel6.DialogResult = DialogResult.Cancel;
            this.btnCancel6.Location = new Point(0x103, 0x130);
            this.btnCancel6.Name = "btnCancel6";
            this.btnCancel6.Size = new Size(0x4b, 0x17);
            this.btnCancel6.TabIndex = 0x2b;
            this.btnCancel6.Text = "Cancel";
            this.btnCancel6.UseVisualStyleBackColor = true;
            this.btnCancel6.Click += new EventHandler(this.btnCancel_Click);
            this.btnTab6Back.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab6Back.Location = new Point(340, 0x130);
            this.btnTab6Back.Name = "btnTab6Back";
            this.btnTab6Back.Size = new Size(0x4b, 0x17);
            this.btnTab6Back.TabIndex = 0x26;
            this.btnTab6Back.Text = "< Back";
            this.btnTab6Back.UseVisualStyleBackColor = true;
            this.btnTab6Back.Click += new EventHandler(this.btnTabBack_Click);
            this.btnTab6Next.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab6Next.Location = new Point(0x1a5, 0x130);
            this.btnTab6Next.Name = "btnTab6Next";
            this.btnTab6Next.Size = new Size(0x4b, 0x17);
            this.btnTab6Next.TabIndex = 0x25;
            this.btnTab6Next.Text = "Next >";
            this.btnTab6Next.UseVisualStyleBackColor = true;
            this.btnTab6Next.Click += new EventHandler(this.btnTabNext_Click);
            this.label7.Location = new Point(11, 0x56);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x1e5, 0x92);
            this.label7.TabIndex = 0x20;
            this.label7.Text = manager.GetString("label7.Text");
            this.rbGraphSimple.Checked = true;
            this.rbGraphSimple.Location = new Point(8, 6);
            this.rbGraphSimple.Name = "rbGraphSimple";
            this.rbGraphSimple.Size = new Size(0xe8, 0x10);
            this.rbGraphSimple.TabIndex = 0x1f;
            this.rbGraphSimple.TabStop = true;
            this.rbGraphSimple.Text = "Simple bar graphs depicting the current sort.";
            this.rbGraphAdv.Location = new Point(8, 0x16);
            this.rbGraphAdv.Name = "rbGraphAdv";
            this.rbGraphAdv.Size = new Size(0x110, 0x10);
            this.rbGraphAdv.TabIndex = 30;
            this.rbGraphAdv.Text = "Advanced DPS over time graphs.  (CPU Intensive)";
            this.tabPage7.Controls.Add(this.btnCancel7);
            this.tabPage7.Controls.Add(this.btnTab7Back);
            this.tabPage7.Controls.Add(this.btnTab7Next);
            this.tabPage7.Controls.Add(this.label8);
            this.tabPage7.Controls.Add(this.label11);
            this.tabPage7.Controls.Add(this.ddlGraphPriority);
            this.tabPage7.Location = new Point(4, 0x19);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new Padding(3);
            this.tabPage7.Size = new Size(0x1f8, 0x14e);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Step 7 ";
            this.tabPage7.UseVisualStyleBackColor = true;
            this.btnCancel7.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel7.DialogResult = DialogResult.Cancel;
            this.btnCancel7.Location = new Point(0x103, 0x130);
            this.btnCancel7.Name = "btnCancel7";
            this.btnCancel7.Size = new Size(0x4b, 0x17);
            this.btnCancel7.TabIndex = 0x2b;
            this.btnCancel7.Text = "Cancel";
            this.btnCancel7.UseVisualStyleBackColor = true;
            this.btnCancel7.Click += new EventHandler(this.btnCancel_Click);
            this.btnTab7Back.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab7Back.Location = new Point(340, 0x130);
            this.btnTab7Back.Name = "btnTab7Back";
            this.btnTab7Back.Size = new Size(0x4b, 0x17);
            this.btnTab7Back.TabIndex = 40;
            this.btnTab7Back.Text = "< Back";
            this.btnTab7Back.UseVisualStyleBackColor = true;
            this.btnTab7Back.Click += new EventHandler(this.btnTabBack_Click);
            this.btnTab7Next.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab7Next.Location = new Point(0x1a5, 0x130);
            this.btnTab7Next.Name = "btnTab7Next";
            this.btnTab7Next.Size = new Size(0x4b, 0x17);
            this.btnTab7Next.TabIndex = 0x27;
            this.btnTab7Next.Text = "Next >";
            this.btnTab7Next.UseVisualStyleBackColor = true;
            this.btnTab7Next.Click += new EventHandler(this.btnTabNext_Click);
            this.label8.Location = new Point(11, 0x1f);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x1e5, 0xd1);
            this.label8.TabIndex = 0x1c;
            this.label8.Text = manager.GetString("label8.Text");
            this.label11.Location = new Point(8, 3);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0xb0, 0x18);
            this.label11.TabIndex = 0x1b;
            this.label11.Text = "CPU Priority of graph generation:";
            this.label11.TextAlign = ContentAlignment.MiddleLeft;
            this.ddlGraphPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlGraphPriority.Items.AddRange(new object[] { "Above Normal", "Normal", "Below Normal", "Lowest" });
            this.ddlGraphPriority.Location = new Point(0xb8, 3);
            this.ddlGraphPriority.Name = "ddlGraphPriority";
            this.ddlGraphPriority.Size = new Size(0x79, 0x15);
            this.ddlGraphPriority.TabIndex = 0x1a;
            this.tabPage9.Controls.Add(this.btnCancel8);
            this.tabPage9.Controls.Add(this.btnTab8Back);
            this.tabPage9.Controls.Add(this.btnTab8Next);
            this.tabPage9.Controls.Add(this.cbRecordLogs);
            this.tabPage9.Controls.Add(this.label20);
            this.tabPage9.Location = new Point(4, 0x19);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new Padding(3);
            this.tabPage9.Size = new Size(0x1f8, 0x14e);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "Step 8";
            this.tabPage9.UseVisualStyleBackColor = true;
            this.btnCancel8.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel8.DialogResult = DialogResult.Cancel;
            this.btnCancel8.Location = new Point(0x103, 0x130);
            this.btnCancel8.Name = "btnCancel8";
            this.btnCancel8.Size = new Size(0x4b, 0x17);
            this.btnCancel8.TabIndex = 0x2e;
            this.btnCancel8.Text = "Cancel";
            this.btnCancel8.UseVisualStyleBackColor = true;
            this.btnCancel8.Click += new EventHandler(this.btnCancel_Click);
            this.btnTab8Back.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab8Back.Location = new Point(340, 0x130);
            this.btnTab8Back.Name = "btnTab8Back";
            this.btnTab8Back.Size = new Size(0x4b, 0x17);
            this.btnTab8Back.TabIndex = 0x2d;
            this.btnTab8Back.Text = "< Back";
            this.btnTab8Back.UseVisualStyleBackColor = true;
            this.btnTab8Back.Click += new EventHandler(this.btnTabBack_Click);
            this.btnTab8Next.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab8Next.Location = new Point(0x1a5, 0x130);
            this.btnTab8Next.Name = "btnTab8Next";
            this.btnTab8Next.Size = new Size(0x4b, 0x17);
            this.btnTab8Next.TabIndex = 0x2c;
            this.btnTab8Next.Text = "Next >";
            this.btnTab8Next.UseVisualStyleBackColor = true;
            this.btnTab8Next.Click += new EventHandler(this.btnTabNext_Click);
            this.cbRecordLogs.Checked = true;
            this.cbRecordLogs.CheckState = CheckState.Checked;
            this.cbRecordLogs.Location = new Point(12, 6);
            this.cbRecordLogs.Name = "cbRecordLogs";
            this.cbRecordLogs.Size = new Size(0x180, 0x10);
            this.cbRecordLogs.TabIndex = 0x23;
            this.cbRecordLogs.Text = "Record all log lines while parsing.  (View Logs context menu option)";
            this.label20.Location = new Point(9, 0x1f);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x1e7, 170);
            this.label20.TabIndex = 0x18;
            this.label20.Text = manager.GetString("label20.Text");
            this.tabPage8.Controls.Add(this.lblS8A);
            this.tabPage8.Controls.Add(this.lblS8B);
            this.tabPage8.Controls.Add(this.label25);
            this.tabPage8.Controls.Add(this.lblS7A);
            this.tabPage8.Controls.Add(this.lblS6A);
            this.tabPage8.Controls.Add(this.lblS5A);
            this.tabPage8.Controls.Add(this.lblS4A);
            this.tabPage8.Controls.Add(this.lblS3A);
            this.tabPage8.Controls.Add(this.lblS2A);
            this.tabPage8.Controls.Add(this.label12);
            this.tabPage8.Controls.Add(this.lblS7B);
            this.tabPage8.Controls.Add(this.lblS6B);
            this.tabPage8.Controls.Add(this.lblS5B);
            this.tabPage8.Controls.Add(this.lblS4B);
            this.tabPage8.Controls.Add(this.lblS3B);
            this.tabPage8.Controls.Add(this.lblS2B);
            this.tabPage8.Controls.Add(this.label10);
            this.tabPage8.Controls.Add(this.label19);
            this.tabPage8.Controls.Add(this.label18);
            this.tabPage8.Controls.Add(this.label17);
            this.tabPage8.Controls.Add(this.label15);
            this.tabPage8.Controls.Add(this.label14);
            this.tabPage8.Controls.Add(this.label13);
            this.tabPage8.Controls.Add(this.label9);
            this.tabPage8.Controls.Add(this.btnOk);
            this.tabPage8.Controls.Add(this.btnCancel);
            this.tabPage8.Controls.Add(this.button5);
            this.tabPage8.Controls.Add(this.btnTab9Back);
            this.tabPage8.Location = new Point(4, 0x19);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new Padding(3);
            this.tabPage8.Size = new Size(0x1f8, 0x14e);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Finish ";
            this.tabPage8.UseVisualStyleBackColor = true;
            this.lblS8A.Location = new Point(0x152, 0x89);
            this.lblS8A.Name = "lblS8A";
            this.lblS8A.Size = new Size(100, 0x10);
            this.lblS8A.TabIndex = 0x2e;
            this.lblS8A.Text = "After";
            this.lblS8B.Location = new Point(0xe8, 0x89);
            this.lblS8B.Name = "lblS8B";
            this.lblS8B.Size = new Size(100, 0x10);
            this.lblS8B.TabIndex = 0x2d;
            this.lblS8B.Text = "Before";
            this.label25.Location = new Point(6, 0x89);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0xcd, 0x10);
            this.label25.TabIndex = 0x2c;
            this.label25.Text = "Step 8: Recording Encounter Logs";
            this.lblS7A.Location = new Point(0x152, 0x79);
            this.lblS7A.Name = "lblS7A";
            this.lblS7A.Size = new Size(100, 0x10);
            this.lblS7A.TabIndex = 0x2b;
            this.lblS7A.Text = "After";
            this.lblS6A.Location = new Point(0x152, 0x69);
            this.lblS6A.Name = "lblS6A";
            this.lblS6A.Size = new Size(100, 0x10);
            this.lblS6A.TabIndex = 0x2b;
            this.lblS6A.Text = "After";
            this.lblS5A.Location = new Point(0x152, 0x59);
            this.lblS5A.Name = "lblS5A";
            this.lblS5A.Size = new Size(100, 0x10);
            this.lblS5A.TabIndex = 0x2b;
            this.lblS5A.Text = "After";
            this.lblS4A.Location = new Point(0x152, 0x49);
            this.lblS4A.Name = "lblS4A";
            this.lblS4A.Size = new Size(100, 0x10);
            this.lblS4A.TabIndex = 0x2b;
            this.lblS4A.Text = "After";
            this.lblS3A.Location = new Point(0x152, 0x39);
            this.lblS3A.Name = "lblS3A";
            this.lblS3A.Size = new Size(100, 0x10);
            this.lblS3A.TabIndex = 0x2b;
            this.lblS3A.Text = "After";
            this.lblS2A.Location = new Point(0x152, 0x29);
            this.lblS2A.Name = "lblS2A";
            this.lblS2A.Size = new Size(100, 0x10);
            this.lblS2A.TabIndex = 0x2b;
            this.lblS2A.Text = "After";
            this.label12.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label12.Location = new Point(0x152, 0x19);
            this.label12.Name = "label12";
            this.label12.Size = new Size(100, 0x10);
            this.label12.TabIndex = 0x2b;
            this.label12.Text = "After";
            this.lblS7B.Location = new Point(0xe8, 0x79);
            this.lblS7B.Name = "lblS7B";
            this.lblS7B.Size = new Size(100, 0x10);
            this.lblS7B.TabIndex = 0x2b;
            this.lblS7B.Text = "Before";
            this.lblS6B.Location = new Point(0xe8, 0x69);
            this.lblS6B.Name = "lblS6B";
            this.lblS6B.Size = new Size(100, 0x10);
            this.lblS6B.TabIndex = 0x2b;
            this.lblS6B.Text = "Before";
            this.lblS5B.Location = new Point(0xe8, 0x59);
            this.lblS5B.Name = "lblS5B";
            this.lblS5B.Size = new Size(100, 0x10);
            this.lblS5B.TabIndex = 0x2b;
            this.lblS5B.Text = "Before";
            this.lblS4B.Location = new Point(0xe8, 0x49);
            this.lblS4B.Name = "lblS4B";
            this.lblS4B.Size = new Size(100, 0x10);
            this.lblS4B.TabIndex = 0x2b;
            this.lblS4B.Text = "Before";
            this.lblS3B.Location = new Point(0xe8, 0x39);
            this.lblS3B.Name = "lblS3B";
            this.lblS3B.Size = new Size(100, 0x10);
            this.lblS3B.TabIndex = 0x2b;
            this.lblS3B.Text = "Before";
            this.lblS2B.Location = new Point(0xe8, 0x29);
            this.lblS2B.Name = "lblS2B";
            this.lblS2B.Size = new Size(100, 0x10);
            this.lblS2B.TabIndex = 0x2b;
            this.lblS2B.Text = "Before";
            this.label10.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label10.Location = new Point(0xe8, 0x19);
            this.label10.Name = "label10";
            this.label10.Size = new Size(100, 0x10);
            this.label10.TabIndex = 0x2b;
            this.label10.Text = "Before";
            this.label19.Location = new Point(6, 0x79);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0xcd, 0x10);
            this.label19.TabIndex = 0x2b;
            this.label19.Text = "Step 7: Graphing/HTML priority";
            this.label18.Location = new Point(6, 0x69);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0xcd, 0x10);
            this.label18.TabIndex = 0x2b;
            this.label18.Text = "Step 6: Main encounter graphing";
            this.label17.Location = new Point(6, 0x59);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0xcd, 0x10);
            this.label17.TabIndex = 0x2b;
            this.label17.Text = "Step 5: Table data updates";
            this.label15.Location = new Point(6, 0x49);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0xcd, 0x10);
            this.label15.TabIndex = 0x2b;
            this.label15.Text = "Step 4: Only populate All (Ref)";
            this.label14.Location = new Point(6, 0x39);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0xcd, 0x10);
            this.label14.TabIndex = 0x2b;
            this.label14.Text = "Step 3: Populate an 'All' encounter";
            this.label13.Location = new Point(6, 0x29);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0xcd, 0x10);
            this.label13.TabIndex = 0x2b;
            this.label13.Text = "Step 2: Log parsing priority";
            this.label9.Location = new Point(6, 0x19);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x90, 0x10);
            this.label9.TabIndex = 0x2b;
            this.label9.Text = "Step 1: ";
            this.btnOk.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnOk.Location = new Point(0xb2, 0x130);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(0x4b, 0x17);
            this.btnOk.TabIndex = 0x2a;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x103, 0x130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 0x2a;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.button5.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.button5.Enabled = false;
            this.button5.Location = new Point(0x1a5, 0x130);
            this.button5.Name = "button5";
            this.button5.Size = new Size(0x4b, 0x17);
            this.button5.TabIndex = 0x2a;
            this.button5.Text = "Next >";
            this.button5.UseVisualStyleBackColor = true;
            this.btnTab9Back.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTab9Back.Location = new Point(340, 0x130);
            this.btnTab9Back.Name = "btnTab9Back";
            this.btnTab9Back.Size = new Size(0x4b, 0x17);
            this.btnTab9Back.TabIndex = 0x2a;
            this.btnTab9Back.Text = "< Back";
            this.btnTab9Back.UseVisualStyleBackColor = true;
            this.btnTab9Back.Click += new EventHandler(this.btnTabBack_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x200, 0x16b);
            base.Controls.Add(this.tc1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new Size(0x206, 0x181);
            base.Name = "FormPerformanceWizard";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "ACT Performance Wizard";
            base.TopMost = true;
            base.FormClosing += new FormClosingEventHandler(this.Form9_FormClosing);
            this.tc1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.nudMiniUpdateInterval.EndInit();
            this.nudUpdateValue.EndInit();
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void ShowWizard()
        {
            this.tc1.SelectedIndex = 0;
            this.cbRestrictToAll.Checked = ActGlobals.oFormActMain.opMisc.cbRestrictToAll.Checked;
            this.ddlGraphPriority.Text = ActGlobals.oFormActMain.opGraphing.ddlGraphPriority.Text;
            this.ddlLogPriority.Text = ActGlobals.oFormActMain.opMisc.ddlLogPriority.Text;
            this.cbZoneAllListing.Checked = ActGlobals.oFormActMain.opMisc.cbZoneAllListing.Checked;
            this.nudMiniUpdateInterval.Value = ActGlobals.oFormActMain.opMiniParse.nudMiniUpdateInterval.Value;
            this.nudUpdateValue.Value = ActGlobals.oFormActMain.opMainTableGen.nudUpdateValue.Value;
            this.rbGraphAdv.Checked = ActGlobals.oFormActMain.opGraphing.rbGraphAdv.Checked;
            this.rbGraphSimple.Checked = ActGlobals.oFormActMain.opGraphing.rbGraphSimple.Checked;
            this.cbRecordLogs.Checked = ActGlobals.oFormActMain.opMisc.cbRecordLogs.Checked;
            base.Show();
        }

        private void tc1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tc1.SelectedIndex == 8)
            {
                this.lblS2B.Text = ActGlobals.oFormActMain.opMisc.ddlLogPriority.Text;
                this.lblS2A.Text = this.ddlLogPriority.Text;
                this.lblS3B.Text = ActGlobals.oFormActMain.opMisc.cbZoneAllListing.Checked.ToString();
                this.lblS3A.Text = this.cbZoneAllListing.Checked.ToString();
                this.lblS4B.Text = ActGlobals.oFormActMain.opMisc.cbRestrictToAll.Checked.ToString();
                this.lblS4A.Text = this.cbRestrictToAll.Checked.ToString();
                this.lblS5B.Text = string.Format("{0:0} | {1:0}", ActGlobals.oFormActMain.opMainTableGen.nudUpdateValue.Value, ActGlobals.oFormActMain.opMiniParse.nudMiniUpdateInterval.Value);
                this.lblS5A.Text = string.Format("{0:0} | {1:0}", this.nudUpdateValue.Value, this.nudMiniUpdateInterval.Value);
                if (ActGlobals.oFormActMain.opGraphing.rbGraphSimple.Checked)
                {
                    this.lblS6B.Text = "Simple";
                }
                if (ActGlobals.oFormActMain.opGraphing.rbGraphAdv.Checked)
                {
                    this.lblS6B.Text = "Advanced";
                }
                if (this.rbGraphSimple.Checked)
                {
                    this.lblS6A.Text = "Simple";
                }
                if (this.rbGraphAdv.Checked)
                {
                    this.lblS6A.Text = "Advanced";
                }
                this.lblS7B.Text = ActGlobals.oFormActMain.opGraphing.ddlGraphPriority.Text;
                this.lblS7A.Text = this.ddlGraphPriority.Text;
                this.lblS8B.Text = ActGlobals.oFormActMain.opMisc.cbRecordLogs.Checked.ToString();
                this.lblS8A.Text = this.cbRecordLogs.Checked.ToString();
            }
        }
    }
}

