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

    public class FormExportFormat : Form
    {
        private Button btnAddTextPreset;
        private Button btnAppend;
        private Button btnAppendAllies;
        private Button btnEditAllyString;
        private Button btnEditCombatantString;
        private Button btnRemove;
        private Button btnRemoveAllies;
        private Button btnShiftLeft;
        private Button btnShiftLeftAllies;
        private Button btnShiftRight;
        private Button btnShiftRightAllies;
        private CheckBox cbAlliesOnly;
        private CheckBox cbPrefixAllied;
        private IContainer components;
        private FormatElement customText;
        internal ComboBox ddlSort;
        private FlowLayoutPanel flAlliesFormat;
        private FlowLayoutPanel flPlayerFormat;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private Label label1;
        private ListBox lbFormatElements;
        private Label lblElementDescription;
        private RichTextBox rtbFormatExample;
        private TextBox tbAllyDirect;
        private TextBox tbCombatantDirect;
        private TextBox tbTextElement;
        private TextBox tbTextElementAllies;

        public FormExportFormat()
        {
            this.InitializeComponent();
        }

        private void AppendElement(FlowLayoutPanel FlowPanel, FormatElement Format, string CustomText)
        {
            FormatElement element = new FormatElement(Format.FormatString, Format.FriendlyString, Format.DescriptionString);
            if (Format.FriendlyString == "Custom Text")
            {
                element.FormatString = CustomText;
            }
            Label label = new Label {
                Margin = new Padding(0, 1, 0, 1),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.CornflowerBlue,
                ForeColor = Color.White,
                Tag = element
            };
            if (Format.FriendlyString == "Custom Text")
            {
                label.Text = element.FormatString;
            }
            else
            {
                if (!string.IsNullOrEmpty(CustomText))
                {
                    CustomText = ":" + CustomText;
                }
                label.Text = ActGlobals.oFormActMain.TextExportFormatterCap1 + element.FormatString + CustomText + ActGlobals.oFormActMain.TextExportFormatterCap2;
            }
            label.Click += new EventHandler(this.element_Click);
            label.AutoSize = true;
            FlowPanel.Controls.Add(label);
        }

        private void btnAddClipPreset_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.AddTextFormat(new TextExportFormatOptions(this.GetPlayerFormatter(), this.ddlSort.Text, this.cbAlliesOnly.Checked, this.cbPrefixAllied.Checked, this.GetAllyFormatter()));
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            if (this.lbFormatElements.SelectedIndex != -1)
            {
                FormatElement selectedItem = (FormatElement) this.lbFormatElements.SelectedItem;
                this.AppendElement(this.flPlayerFormat, selectedItem, (selectedItem.FriendlyString == "Custom Text") ? this.tbTextElement.Text : string.Empty);
            }
            this.UpdateFormatPreview();
        }

        private void btnAppendAllies_Click(object sender, EventArgs e)
        {
            if (this.lbFormatElements.SelectedIndex != -1)
            {
                FormatElement selectedItem = (FormatElement) this.lbFormatElements.SelectedItem;
                this.AppendElement(this.flAlliesFormat, selectedItem, (selectedItem.FriendlyString == "Custom Text") ? this.tbTextElementAllies.Text : string.Empty);
            }
            this.UpdateFormatPreview();
        }

        private void btnEditAllyString_Click(object sender, EventArgs e)
        {
            this.tbAllyDirect.Visible = !this.tbAllyDirect.Visible;
            if (this.tbAllyDirect.Visible)
            {
                this.tbAllyDirect.Text = this.GetAllyFormatter();
                this.btnEditAllyString.Text = "Save";
            }
            else
            {
                this.SetElements(this.tbAllyDirect.Text, this.flAlliesFormat);
                this.UpdateFormatPreview();
                this.btnEditAllyString.Text = "Edit Directly";
            }
        }

        private void btnEditCombatantString_Click(object sender, EventArgs e)
        {
            this.tbCombatantDirect.Visible = !this.tbCombatantDirect.Visible;
            if (this.tbCombatantDirect.Visible)
            {
                this.tbCombatantDirect.Text = this.GetPlayerFormatter();
                this.btnEditCombatantString.Text = "Save";
            }
            else
            {
                this.SetElements(this.tbCombatantDirect.Text, this.flPlayerFormat);
                this.UpdateFormatPreview();
                this.btnEditCombatantString.Text = "Edit Directly";
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (Label label in this.flPlayerFormat.Controls)
            {
                if (label.BackColor == Color.Goldenrod)
                {
                    this.flPlayerFormat.Controls.Remove(label);
                    break;
                }
            }
            this.btnRemove.Enabled = false;
            this.btnShiftLeft.Enabled = false;
            this.btnShiftRight.Enabled = false;
            this.UpdateFormatPreview();
        }

        private void btnRemoveAllies_Click(object sender, EventArgs e)
        {
            foreach (Label label in this.flAlliesFormat.Controls)
            {
                if (label.BackColor == Color.Goldenrod)
                {
                    this.flAlliesFormat.Controls.Remove(label);
                    break;
                }
            }
            this.btnRemoveAllies.Enabled = false;
            this.btnShiftLeftAllies.Enabled = false;
            this.btnShiftRightAllies.Enabled = false;
            this.UpdateFormatPreview();
        }

        private void btnShiftLeft_Click(object sender, EventArgs e)
        {
            int num = this.ShiftElement(this.flPlayerFormat, -1);
            this.btnShiftLeft.Enabled = num > 0;
            this.btnShiftRight.Enabled = num < (this.flPlayerFormat.Controls.Count - 1);
            this.UpdateFormatPreview();
        }

        private void btnShiftLeftAllies_Click(object sender, EventArgs e)
        {
            int num = this.ShiftElement(this.flAlliesFormat, -1);
            this.btnShiftLeftAllies.Enabled = num > 0;
            this.btnShiftRightAllies.Enabled = num < (this.flAlliesFormat.Controls.Count - 1);
            this.UpdateFormatPreview();
        }

        private void btnShiftRight_Click(object sender, EventArgs e)
        {
            int num = this.ShiftElement(this.flPlayerFormat, 1);
            this.btnShiftLeft.Enabled = num > 0;
            this.btnShiftRight.Enabled = num < (this.flPlayerFormat.Controls.Count - 1);
            this.UpdateFormatPreview();
        }

        private void btnShiftRightAllies_Click(object sender, EventArgs e)
        {
            int num = this.ShiftElement(this.flAlliesFormat, 1);
            this.btnShiftLeftAllies.Enabled = num > 0;
            this.btnShiftRightAllies.Enabled = num < (this.flAlliesFormat.Controls.Count - 1);
            this.UpdateFormatPreview();
        }

        private void cbAlliesOnly_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateFormatPreview();
        }

        private void cbPrefixAllied_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateFormatPreview();
        }

        private void cbTabulate_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateFormatPreview();
        }

        private void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateFormatPreview();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void element_Click(object sender, EventArgs e)
        {
            Label control = (Label) sender;
            FlowLayoutPanel parent = (FlowLayoutPanel) control.Parent;
            foreach (Label label2 in parent.Controls)
            {
                label2.BackColor = Color.CornflowerBlue;
            }
            control.BackColor = Color.Goldenrod;
            int index = parent.Controls.IndexOf(control);
            FormatElement tag = (FormatElement) control.Tag;
            int num2 = this.lbFormatElements.Items.IndexOf(tag);
            this.lbFormatElements.SelectedIndex = num2;
            if (parent == this.flPlayerFormat)
            {
                if (this.tbTextElement.Enabled)
                {
                    this.tbTextElement.Text = tag.FormatString;
                }
                else
                {
                    this.tbTextElement.Text = string.Empty;
                }
                this.btnRemove.Enabled = true;
                this.btnShiftLeft.Enabled = index > 0;
                this.btnShiftRight.Enabled = index < (this.flPlayerFormat.Controls.Count - 1);
            }
            else
            {
                if (this.tbTextElementAllies.Enabled)
                {
                    this.tbTextElementAllies.Text = tag.FormatString;
                }
                else
                {
                    this.tbTextElementAllies.Text = string.Empty;
                }
                this.btnRemoveAllies.Enabled = true;
                this.btnShiftLeftAllies.Enabled = index > 0;
                this.btnShiftRightAllies.Enabled = index < (this.flAlliesFormat.Controls.Count - 1);
            }
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
            xml.WriteAttributeString("Form", "FormExportFormat");
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

        private void FormExportFormat_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        private string GetAllyFormatter()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.flAlliesFormat.Controls.Count; i++)
            {
                builder.Append(this.flAlliesFormat.Controls[i].Text);
            }
            return builder.ToString();
        }

        private string GetPlayerFormatter()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.flPlayerFormat.Controls.Count; i++)
            {
                builder.Append(this.flPlayerFormat.Controls[i].Text);
            }
            return builder.ToString();
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
            this.groupBox1 = new GroupBox();
            this.ddlSort = new ComboBox();
            this.label1 = new Label();
            this.btnAddTextPreset = new Button();
            this.cbPrefixAllied = new CheckBox();
            this.cbAlliesOnly = new CheckBox();
            this.groupBox6 = new GroupBox();
            this.btnEditAllyString = new Button();
            this.tbAllyDirect = new TextBox();
            this.flAlliesFormat = new FlowLayoutPanel();
            this.btnShiftRightAllies = new Button();
            this.tbTextElementAllies = new TextBox();
            this.btnShiftLeftAllies = new Button();
            this.btnAppendAllies = new Button();
            this.btnRemoveAllies = new Button();
            this.groupBox5 = new GroupBox();
            this.btnEditCombatantString = new Button();
            this.tbCombatantDirect = new TextBox();
            this.flPlayerFormat = new FlowLayoutPanel();
            this.btnShiftRight = new Button();
            this.tbTextElement = new TextBox();
            this.btnShiftLeft = new Button();
            this.btnAppend = new Button();
            this.btnRemove = new Button();
            this.groupBox2 = new GroupBox();
            this.lbFormatElements = new ListBox();
            this.groupBox3 = new GroupBox();
            this.lblElementDescription = new Label();
            this.groupBox4 = new GroupBox();
            this.rtbFormatExample = new RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.ddlSort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnAddTextPreset);
            this.groupBox1.Controls.Add(this.cbPrefixAllied);
            this.groupBox1.Controls.Add(this.cbAlliesOnly);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x2e1, 0xe5);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Formatting String";
            this.ddlSort.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.ddlSort.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlSort.Location = new Point(310, 0xc3);
            this.ddlSort.Name = "ddlSort";
            this.ddlSort.Size = new Size(0xda, 0x15);
            this.ddlSort.TabIndex = 4;
            this.ddlSort.SelectedIndexChanged += new EventHandler(this.ddlSort_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0xcb, 0xc7);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x65, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sort combatants by:";
            this.btnAddTextPreset.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnAddTextPreset.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnAddTextPreset.ForeColor = Color.DarkRed;
            this.btnAddTextPreset.Location = new Point(0x216, 0xbc);
            this.btnAddTextPreset.Name = "btnAddTextPreset";
            this.btnAddTextPreset.Size = new Size(0xc5, 0x24);
            this.btnAddTextPreset.TabIndex = 0;
            this.btnAddTextPreset.Text = "Add Text Format Preset";
            this.btnAddTextPreset.UseVisualStyleBackColor = true;
            this.btnAddTextPreset.Click += new EventHandler(this.btnAddClipPreset_Click);
            this.cbPrefixAllied.AutoSize = true;
            this.cbPrefixAllied.Checked = true;
            this.cbPrefixAllied.CheckState = CheckState.Checked;
            this.cbPrefixAllied.Location = new Point(0x25, 0xcf);
            this.cbPrefixAllied.Name = "cbPrefixAllied";
            this.cbPrefixAllied.Size = new Size(0x7b, 0x11);
            this.cbPrefixAllied.TabIndex = 2;
            this.cbPrefixAllied.Text = "Prefix Allied statistics";
            this.cbPrefixAllied.UseVisualStyleBackColor = true;
            this.cbPrefixAllied.CheckedChanged += new EventHandler(this.cbPrefixAllied_CheckedChanged);
            this.cbAlliesOnly.AutoSize = true;
            this.cbAlliesOnly.Checked = true;
            this.cbAlliesOnly.CheckState = CheckState.Checked;
            this.cbAlliesOnly.Location = new Point(0x10, 0xbb);
            this.cbAlliesOnly.Name = "cbAlliesOnly";
            this.cbAlliesOnly.Size = new Size(0xa4, 0x11);
            this.cbAlliesOnly.TabIndex = 1;
            this.cbAlliesOnly.Text = "Only export allied combatants";
            this.cbAlliesOnly.UseVisualStyleBackColor = true;
            this.cbAlliesOnly.CheckedChanged += new EventHandler(this.cbAlliesOnly_CheckedChanged);
            this.groupBox6.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox6.Controls.Add(this.btnEditAllyString);
            this.groupBox6.Controls.Add(this.tbAllyDirect);
            this.groupBox6.Controls.Add(this.flAlliesFormat);
            this.groupBox6.Controls.Add(this.btnShiftRightAllies);
            this.groupBox6.Controls.Add(this.tbTextElementAllies);
            this.groupBox6.Controls.Add(this.btnShiftLeftAllies);
            this.groupBox6.Controls.Add(this.btnAppendAllies);
            this.groupBox6.Controls.Add(this.btnRemoveAllies);
            this.groupBox6.Location = new Point(10, 0x13);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x2d1, 80);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Allies Formatting";
            this.btnEditAllyString.Location = new Point(0xf6, 0x2f);
            this.btnEditAllyString.Name = "btnEditAllyString";
            this.btnEditAllyString.Size = new Size(0x58, 0x17);
            this.btnEditAllyString.TabIndex = 6;
            this.btnEditAllyString.Text = "Edit Directly";
            this.btnEditAllyString.UseVisualStyleBackColor = true;
            this.btnEditAllyString.Click += new EventHandler(this.btnEditAllyString_Click);
            this.tbAllyDirect.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbAllyDirect.Font = new Font("Lucida Console", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.tbAllyDirect.Location = new Point(7, 0x13);
            this.tbAllyDirect.Name = "tbAllyDirect";
            this.tbAllyDirect.Size = new Size(0x2c4, 0x16);
            this.tbAllyDirect.TabIndex = 1;
            this.tbAllyDirect.Visible = false;
            this.flAlliesFormat.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.flAlliesFormat.BackColor = SystemColors.Window;
            this.flAlliesFormat.BorderStyle = BorderStyle.Fixed3D;
            this.flAlliesFormat.ForeColor = SystemColors.WindowText;
            this.flAlliesFormat.Location = new Point(6, 0x13);
            this.flAlliesFormat.Name = "flAlliesFormat";
            this.flAlliesFormat.Padding = new Padding(1);
            this.flAlliesFormat.Size = new Size(0x2c5, 0x18);
            this.flAlliesFormat.TabIndex = 0;
            this.btnShiftRightAllies.Enabled = false;
            this.btnShiftRightAllies.Location = new Point(0xcf, 0x2f);
            this.btnShiftRightAllies.Name = "btnShiftRightAllies";
            this.btnShiftRightAllies.Size = new Size(0x21, 0x17);
            this.btnShiftRightAllies.TabIndex = 5;
            this.btnShiftRightAllies.Text = "->";
            this.btnShiftRightAllies.UseVisualStyleBackColor = true;
            this.btnShiftRightAllies.Click += new EventHandler(this.btnShiftRightAllies_Click);
            this.tbTextElementAllies.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbTextElementAllies.BackColor = Color.CornflowerBlue;
            this.tbTextElementAllies.Font = new Font("Courier New", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.tbTextElementAllies.ForeColor = Color.White;
            this.tbTextElementAllies.Location = new Point(0x17a, 50);
            this.tbTextElementAllies.Name = "tbTextElementAllies";
            this.tbTextElementAllies.Size = new Size(0x151, 0x16);
            this.tbTextElementAllies.TabIndex = 7;
            this.tbTextElementAllies.Visible = false;
            this.btnShiftLeftAllies.Enabled = false;
            this.btnShiftLeftAllies.Location = new Point(0xa8, 0x2f);
            this.btnShiftLeftAllies.Name = "btnShiftLeftAllies";
            this.btnShiftLeftAllies.Size = new Size(0x21, 0x17);
            this.btnShiftLeftAllies.TabIndex = 4;
            this.btnShiftLeftAllies.Text = "<-";
            this.btnShiftLeftAllies.UseVisualStyleBackColor = true;
            this.btnShiftLeftAllies.Click += new EventHandler(this.btnShiftLeftAllies_Click);
            this.btnAppendAllies.Enabled = false;
            this.btnAppendAllies.Location = new Point(6, 0x2f);
            this.btnAppendAllies.Name = "btnAppendAllies";
            this.btnAppendAllies.Size = new Size(0x4b, 0x17);
            this.btnAppendAllies.TabIndex = 2;
            this.btnAppendAllies.Text = "Append";
            this.btnAppendAllies.UseVisualStyleBackColor = true;
            this.btnAppendAllies.Click += new EventHandler(this.btnAppendAllies_Click);
            this.btnRemoveAllies.Enabled = false;
            this.btnRemoveAllies.Location = new Point(0x57, 0x2f);
            this.btnRemoveAllies.Name = "btnRemoveAllies";
            this.btnRemoveAllies.Size = new Size(0x4b, 0x17);
            this.btnRemoveAllies.TabIndex = 3;
            this.btnRemoveAllies.Text = "Remove";
            this.btnRemoveAllies.UseVisualStyleBackColor = true;
            this.btnRemoveAllies.Click += new EventHandler(this.btnRemoveAllies_Click);
            this.groupBox5.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox5.Controls.Add(this.btnEditCombatantString);
            this.groupBox5.Controls.Add(this.tbCombatantDirect);
            this.groupBox5.Controls.Add(this.flPlayerFormat);
            this.groupBox5.Controls.Add(this.btnShiftRight);
            this.groupBox5.Controls.Add(this.tbTextElement);
            this.groupBox5.Controls.Add(this.btnShiftLeft);
            this.groupBox5.Controls.Add(this.btnAppend);
            this.groupBox5.Controls.Add(this.btnRemove);
            this.groupBox5.Location = new Point(10, 0x69);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x2d1, 80);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Per-Combatant Formatting";
            this.btnEditCombatantString.Location = new Point(0xf6, 0x2f);
            this.btnEditCombatantString.Name = "btnEditCombatantString";
            this.btnEditCombatantString.Size = new Size(0x58, 0x17);
            this.btnEditCombatantString.TabIndex = 6;
            this.btnEditCombatantString.Text = "Edit Directly";
            this.btnEditCombatantString.UseVisualStyleBackColor = true;
            this.btnEditCombatantString.Click += new EventHandler(this.btnEditCombatantString_Click);
            this.tbCombatantDirect.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbCombatantDirect.Font = new Font("Lucida Console", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.tbCombatantDirect.Location = new Point(7, 0x13);
            this.tbCombatantDirect.Name = "tbCombatantDirect";
            this.tbCombatantDirect.Size = new Size(0x2c4, 0x16);
            this.tbCombatantDirect.TabIndex = 1;
            this.tbCombatantDirect.Visible = false;
            this.flPlayerFormat.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.flPlayerFormat.BackColor = SystemColors.Window;
            this.flPlayerFormat.BorderStyle = BorderStyle.Fixed3D;
            this.flPlayerFormat.ForeColor = SystemColors.WindowText;
            this.flPlayerFormat.Location = new Point(6, 0x13);
            this.flPlayerFormat.Name = "flPlayerFormat";
            this.flPlayerFormat.Padding = new Padding(1);
            this.flPlayerFormat.Size = new Size(0x2c5, 0x18);
            this.flPlayerFormat.TabIndex = 0;
            this.btnShiftRight.Enabled = false;
            this.btnShiftRight.Location = new Point(0xcf, 0x2f);
            this.btnShiftRight.Name = "btnShiftRight";
            this.btnShiftRight.Size = new Size(0x21, 0x17);
            this.btnShiftRight.TabIndex = 5;
            this.btnShiftRight.Text = "->";
            this.btnShiftRight.UseVisualStyleBackColor = true;
            this.btnShiftRight.Click += new EventHandler(this.btnShiftRight_Click);
            this.tbTextElement.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbTextElement.BackColor = Color.CornflowerBlue;
            this.tbTextElement.Font = new Font("Courier New", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.tbTextElement.ForeColor = Color.White;
            this.tbTextElement.Location = new Point(0x17a, 50);
            this.tbTextElement.Name = "tbTextElement";
            this.tbTextElement.Size = new Size(0x151, 0x16);
            this.tbTextElement.TabIndex = 7;
            this.tbTextElement.Visible = false;
            this.btnShiftLeft.Enabled = false;
            this.btnShiftLeft.Location = new Point(0xa8, 0x2f);
            this.btnShiftLeft.Name = "btnShiftLeft";
            this.btnShiftLeft.Size = new Size(0x21, 0x17);
            this.btnShiftLeft.TabIndex = 4;
            this.btnShiftLeft.Text = "<-";
            this.btnShiftLeft.UseVisualStyleBackColor = true;
            this.btnShiftLeft.Click += new EventHandler(this.btnShiftLeft_Click);
            this.btnAppend.Enabled = false;
            this.btnAppend.Location = new Point(6, 0x2f);
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Size = new Size(0x4b, 0x17);
            this.btnAppend.TabIndex = 2;
            this.btnAppend.Text = "Append";
            this.btnAppend.UseVisualStyleBackColor = true;
            this.btnAppend.Click += new EventHandler(this.btnAppend_Click);
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new Point(0x57, 0x2f);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new Size(0x4b, 0x17);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new EventHandler(this.btnRemove_Click);
            this.groupBox2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.lbFormatElements);
            this.groupBox2.Location = new Point(12, 0xf7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(200, 0x10b);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available Formatting Elements";
            this.lbFormatElements.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lbFormatElements.FormattingEnabled = true;
            this.lbFormatElements.IntegralHeight = false;
            this.lbFormatElements.Location = new Point(10, 0x12);
            this.lbFormatElements.Name = "lbFormatElements";
            this.lbFormatElements.Size = new Size(180, 240);
            this.lbFormatElements.TabIndex = 0;
            this.lbFormatElements.SelectedIndexChanged += new EventHandler(this.lbFormatElements_SelectedIndexChanged);
            this.groupBox3.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox3.Controls.Add(this.lblElementDescription);
            this.groupBox3.Location = new Point(0xda, 0xf7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x213, 100);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Formating Element Description";
            this.lblElementDescription.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lblElementDescription.BorderStyle = BorderStyle.Fixed3D;
            this.lblElementDescription.Location = new Point(6, 0x12);
            this.lblElementDescription.Name = "lblElementDescription";
            this.lblElementDescription.Size = new Size(0x207, 0x4a);
            this.lblElementDescription.TabIndex = 0;
            this.lblElementDescription.Text = "No Element Selected";
            this.groupBox4.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.groupBox4.Controls.Add(this.rtbFormatExample);
            this.groupBox4.Location = new Point(0xda, 0x161);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x213, 0xa1);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Formatting Example";
            this.rtbFormatExample.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.rtbFormatExample.Cursor = Cursors.Arrow;
            this.rtbFormatExample.DetectUrls = false;
            this.rtbFormatExample.Location = new Point(6, 0x10);
            this.rtbFormatExample.Name = "rtbFormatExample";
            this.rtbFormatExample.ReadOnly = true;
            this.rtbFormatExample.ScrollBars = RichTextBoxScrollBars.None;
            this.rtbFormatExample.Size = new Size(0x207, 0x88);
            this.rtbFormatExample.TabIndex = 0;
            this.rtbFormatExample.Text = "";
            this.rtbFormatExample.WordWrap = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2f9, 0x20e);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            this.MinimumSize = new Size(0x27f, 0x1dc);
            base.Name = "FormExportFormat";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "Text Export Formatting";
            base.TopMost = true;
            base.FormClosing += new FormClosingEventHandler(this.FormExportFormat_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void lbFormatElements_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lbFormatElements.SelectedIndex != -1)
            {
                FormatElement selectedItem = (FormatElement) this.lbFormatElements.SelectedItem;
                this.lblElementDescription.Text = selectedItem.DescriptionString;
                this.tbTextElement.Visible = selectedItem.FriendlyString == "Custom Text";
                this.btnAppend.Enabled = true;
                this.tbTextElementAllies.Visible = selectedItem.FriendlyString == "Custom Text";
                this.btnAppendAllies.Enabled = true;
            }
            else
            {
                this.lblElementDescription.Text = "No Element Selected";
                this.tbTextElement.Visible = false;
                this.btnAppend.Enabled = false;
                this.tbTextElementAllies.Visible = false;
                this.btnAppendAllies.Enabled = false;
            }
        }

        private void PopulateLb()
        {
            List<string> list = new List<string> {
                string.Empty
            };
            this.customText = new FormatElement(string.Empty, ActGlobals.ActLocalization.LocalizationStrings["exportFormattingLabel-custom"].DisplayedText, ActGlobals.ActLocalization.LocalizationStrings["exportFormattingDesc-custom"].DisplayedText);
            this.lbFormatElements.Items.Add(this.customText);
            foreach (KeyValuePair<string, CombatantData.TextExportFormatter> pair in CombatantData.ExportVariables)
            {
                if (!list.Contains(pair.Key))
                {
                    list.Add(pair.Key);
                    this.lbFormatElements.Items.Add(new FormatElement(pair.Value.Name, pair.Value.Label, pair.Value.Description));
                }
            }
            foreach (KeyValuePair<string, EncounterData.TextExportFormatter> pair2 in EncounterData.ExportVariables)
            {
                if (!list.Contains(pair2.Key))
                {
                    list.Add(pair2.Key);
                    this.lbFormatElements.Items.Add(new FormatElement(pair2.Value.Name, pair2.Value.Label, pair2.Value.Description));
                }
            }
        }

        private void SetElements(string Format, FlowLayoutPanel FlowPanel)
        {
            FlowPanel.Controls.Clear();
            foreach (Match match in ActGlobals.oFormActMain.TextExportFormatterRegex.Matches(Format))
            {
                if (match.Groups["formatter"].Success)
                {
                    string str = match.Groups["formatter"].Value;
                    string customText = match.Groups["extra"].Value;
                    foreach (FormatElement element in this.lbFormatElements.Items)
                    {
                        if (str == element.FormatString)
                        {
                            this.AppendElement(FlowPanel, element, customText);
                            break;
                        }
                    }
                    continue;
                }
                this.AppendElement(FlowPanel, this.customText, match.Value);
            }
        }

        private int ShiftElement(FlowLayoutPanel FlowPanel, int PositionDelta)
        {
            List<Label> list = new List<Label>(FlowPanel.Controls.Count);
            int index = -1;
            Label item = null;
            for (int i = 0; i < FlowPanel.Controls.Count; i++)
            {
                Label label2 = (Label) FlowPanel.Controls[i];
                if (label2.BackColor == Color.Goldenrod)
                {
                    index = i;
                    item = label2;
                }
                list.Add(label2);
            }
            list.RemoveAt(index);
            list.Insert(index + PositionDelta, item);
            FlowPanel.Controls.Clear();
            FlowPanel.Controls.AddRange(list.ToArray());
            return (index + PositionDelta);
        }

        public void ShowWindow(TextExportFormatOptions InitData)
        {
            if (this.lbFormatElements.Items.Count == 0)
            {
                ActGlobals.oFormActMain.GenerateFormatPreviewEnc();
                this.PopulateLb();
            }
            this.cbAlliesOnly.Checked = InitData.ShowOnlyAllies;
            this.cbPrefixAllied.Checked = InitData.ShowAlliedInfo;
            this.ddlSort.DropDownStyle = ComboBoxStyle.DropDown;
            this.ddlSort.Text = InitData.Sorting;
            this.ddlSort.DropDownStyle = ComboBoxStyle.DropDownList;
            this.SetElements(InitData.AlliesFormat, this.flAlliesFormat);
            this.SetElements(InitData.PlayerFormat, this.flPlayerFormat);
            this.UpdateFormatPreview();
            base.Show();
        }

        private void UpdateFormatPreview()
        {
            this.rtbFormatExample.Text = ActGlobals.oFormActMain.GetTextExport(ActGlobals.oFormActMain.formatPreviewEnc, new TextExportFormatOptions(this.GetPlayerFormatter(), this.ddlSort.Text, this.cbAlliesOnly.Checked, this.cbPrefixAllied.Checked, this.GetAllyFormatter()));
        }

        internal class FormatElement
        {
            private string descriptionString;
            private string formatString;
            private string friendlyString;

            public FormatElement(string FormatString, string FriendlyString, string DescriptionString)
            {
                this.formatString = FormatString;
                this.friendlyString = FriendlyString;
                this.descriptionString = DescriptionString;
            }

            public override bool Equals(object obj)
            {
                if (obj == DBNull.Value)
                {
                    return false;
                }
                FormExportFormat.FormatElement element = (FormExportFormat.FormatElement) obj;
                return (this.friendlyString == element.friendlyString);
            }

            public override int GetHashCode()
            {
                return this.ToString().GetHashCode();
            }

            public override string ToString()
            {
                return this.friendlyString;
            }

            public string DescriptionString
            {
                get
                {
                    return this.descriptionString;
                }
                set
                {
                    this.descriptionString = value;
                }
            }

            public string FormatString
            {
                get
                {
                    return this.formatString;
                }
                set
                {
                    this.formatString = value;
                }
            }

            public string FriendlyString
            {
                get
                {
                    return this.friendlyString;
                }
                set
                {
                    this.friendlyString = value;
                }
            }
        }
    }
}

