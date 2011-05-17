namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class FormResistsDeathReport : Form
    {
        private Button btnCopyAll;
        private Button btnCopySelected;
        private Button btnHide;
        private ContextMenu cmCopySave;
        private MenuItem cmiCopy;
        private MenuItem cmiSave;
        private IContainer components;
        private ComboBox ddlDeathPicker;
        private List<DeathData> deaths;
        private List<DateTime> deathTimes;
        private Graphics fbG;
        private Bitmap frontBuffer;
        private Label lblDeathSelect;
        private string mobName = string.Empty;
        private PictureBox pb1;
        private RichTextBox rtb1;
        private ToolTip toolTip1;
        private ToolTipGrid ttg = new ToolTipGrid();

        public FormResistsDeathReport()
        {
            this.InitializeComponent();
        }

        private void btnCopyAll_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.SendToClipboard(this.rtb1.Text, true);
            base.Hide();
        }

        private void btnCopySelected_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.SendToClipboard(this.rtb1.SelectedText, true);
            base.Hide();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            base.Hide();
            try
            {
                this.fbG.Clear(SystemColors.ControlLight);
                this.pb1.Image = this.frontBuffer;
            }
            catch
            {
            }
        }

        private void cmiCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.frontBuffer, true);
        }

        private void cmiSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                AddExtension = true,
                CheckPathExists = true,
                CreatePrompt = false,
                Filter = "Portable Network Graphics (*.png)|*.png",
                OverwritePrompt = true,
                Title = "Save graph as...",
                ValidateNames = true
            };
            if ((ActGlobals.oFormActMain.folderExports != null) && ActGlobals.oFormActMain.folderExports.Exists)
            {
                dialog.InitialDirectory = ActGlobals.oFormActMain.folderExports.FullName;
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ActGlobals.oFormActMain.folderExports = new DirectoryInfo(Path.GetDirectoryName(dialog.FileName));
                FileStream stream = new FileStream(dialog.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                stream.Seek(0, SeekOrigin.Begin);
                stream.SetLength(0);
                this.frontBuffer.Save(stream, ImageFormat.Png);
                stream.Close();
            }
        }

        private void ddlDeathPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SolidBrush brush;
                Pen pen;
                Pen pen2;
                float num4;
                this.ttg.Items.Clear();
                DeathData data = this.deaths[this.ddlDeathPicker.SelectedIndex];
                int num = 0;
                this.frontBuffer = new Bitmap(this.pb1.Width, this.pb1.Height);
                this.fbG = Graphics.FromImage(this.frontBuffer);
                if ((SystemColors.Control.GetBrightness() < 0.2f) || (SystemColors.Control.GetBrightness() > 0.9f))
                {
                    new SolidBrush(Color.White);
                    brush = new SolidBrush(Color.Black);
                    new SolidBrush(Color.Gray);
                    new Pen(Color.White);
                    pen = new Pen(Color.Black);
                    pen2 = new Pen(Color.Gray);
                }
                else
                {
                    new SolidBrush(SystemColors.ControlLight);
                    brush = new SolidBrush(SystemColors.ControlText);
                    new SolidBrush(SystemColors.ControlDark);
                    new Pen(SystemColors.ControlLight);
                    pen = new Pen(SystemColors.ControlText);
                    pen2 = new Pen(SystemColors.ControlDark);
                }
                SolidBrush brush2 = new SolidBrush(Color.DarkRed);
                SolidBrush brush3 = new SolidBrush(Color.DarkBlue);
                Pen pen3 = new Pen(Color.Orange, 2f);
                SolidBrush brush4 = new SolidBrush(Color.White);
                Bitmap image = new Bitmap(this.pb1.Width - 4, this.pb1.Height - 4);
                Graphics graphics = Graphics.FromImage(image);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.Clear(SystemColors.ControlLight);
                int num2 = 0x10;
                Rectangle rect = new Rectangle(4, 4, (image.Width - 1) - (num2 * 3), (image.Height - 1) - num2);
                graphics.DrawRectangle(pen, rect);
                float bottom = rect.Bottom;
                try
                {
                    num4 = rect.Width / 10;
                }
                catch
                {
                    return;
                }
                float left = rect.Left;
                float num6 = 0f;
                for (int i = 9; i > -1; i--)
                {
                    num6 += data.Items[i].Result;
                    if (num6 < 0f)
                    {
                        num6 = 0f;
                    }
                }
                float num8 = ((float) rect.Height) / num6;
                Font font = new Font("Arial", 8f);
                Font font2 = new Font("Courier New", 8f);
                graphics.DrawString("0", font, brush, (float) (rect.Right + 5), (float) rect.Bottom);
                graphics.DrawString(((int) num6).ToString(), font, brush, (float) (rect.Right + 5), (float) rect.Top);
                try
                {
                    int num9 = Convert.ToInt32((float) (bottom - Convert.ToInt32((float) ((num6 / 4f) * num8))));
                    graphics.DrawLine(pen2, rect.Left, num9, rect.Right, num9);
                    graphics.DrawString(((int) (num6 / 4f)).ToString(), font, brush, (float) (rect.Right + 5), (float) num9);
                    num9 = Convert.ToInt32((float) (bottom - Convert.ToInt32((float) (((num6 / 4f) * 2f) * num8))));
                    graphics.DrawLine(pen2, rect.Left, num9, rect.Right, num9);
                    graphics.DrawString(((int) ((num6 / 4f) * 2f)).ToString(), font, brush, (float) (rect.Right + 5), (float) num9);
                    num9 = Convert.ToInt32((float) (bottom - Convert.ToInt32((float) (((num6 / 4f) * 3f) * num8))));
                    graphics.DrawLine(pen2, rect.Left, num9, rect.Right, num9);
                    graphics.DrawString(((int) ((num6 / 4f) * 3f)).ToString(), font, brush, (float) (rect.Right + 5), (float) num9);
                }
                catch
                {
                }
                for (int j = 9; j > -1; j--)
                {
                    float x = left;
                    float y = bottom - (data.Items[j].Damage * num8);
                    float width = num4 / 2f;
                    float height = data.Items[j].Damage * num8;
                    RectangleF ef = new RectangleF(x, y, width, height);
                    float num15 = left + (num4 / 2f);
                    float num16 = bottom - (data.Items[j].Healing * num8);
                    float num17 = num4 / 2f;
                    float num18 = data.Items[j].Healing * num8;
                    RectangleF ef2 = new RectangleF(num15, num16, num17, num18);
                    this.ttg.Items.Add(new ToolTipRect(-1, data.Items[j].ToString(), x, (float) rect.Top, num4, (float) rect.Height));
                    try
                    {
                        graphics.FillRectangle(brush2, ef);
                        graphics.DrawRectangle(pen, ef.X, ef.Y, ef.Width, ef.Height);
                        graphics.FillRectangle(brush3, ef2);
                        graphics.DrawRectangle(pen, ef2.X, ef2.Y, ef2.Width, ef2.Height);
                    }
                    catch
                    {
                    }
                    int num19 = 3;
                    num += data.Items[j].Result;
                    if (num < 0)
                    {
                        num = 0;
                    }
                    graphics.FillEllipse(brush, (left - num19) + (num4 / 2f), (bottom - Convert.ToInt32((float) (num * num8))) - num19, (float) (num19 * 2), (float) (num19 * 2));
                    if (j != 9)
                    {
                        graphics.DrawLine(pen3, (float) (left - (num4 / 2f)), (float) (bottom - Convert.ToInt32((float) ((num - data.Items[j].Result) * num8))), (float) (left + (num4 / 2f)), (float) (bottom - Convert.ToInt32((float) (num * num8))));
                    }
                    StringFormat format = new StringFormat {
                        FormatFlags = StringFormatFlags.DirectionVertical
                    };
                    int num20 = ((int) (ef.Height / 6f)) - 2;
                    SizeF ef3 = graphics.MeasureString(data.Items[j].Damage.ToString(), font2);
                    if (data.Items[j].Damage.ToString().Length < num20)
                    {
                        graphics.DrawString(data.Items[j].Damage.ToString(), font2, brush4, (left - 8f) + (num4 / 4f), (bottom - (data.Items[j].Damage * num8)) + 12f, format);
                    }
                    else
                    {
                        graphics.DrawString(data.Items[j].Damage.ToString(), font2, brush2, (left - 8f) + (num4 / 4f), ((bottom - (data.Items[j].Damage * num8)) - ef3.Width) - 4f, format);
                    }
                    num20 = ((int) (ef2.Height / 6f)) - 2;
                    ef3 = graphics.MeasureString(data.Items[j].Healing.ToString(), font2);
                    if (data.Items[j].Healing.ToString().Length < num20)
                    {
                        graphics.DrawString(data.Items[j].Healing.ToString(), font2, brush4, ((left - 8f) + num4) - (num4 / 4f), (bottom - (data.Items[j].Healing * num8)) + 12f, format);
                    }
                    else
                    {
                        graphics.DrawString(data.Items[j].Healing.ToString(), font2, brush3, ((left - 8f) + num4) - (num4 / 4f), ((bottom - (data.Items[j].Healing * num8)) - ef3.Width) - 4f, format);
                    }
                    graphics.DrawLine(pen2, left + num4, (float) rect.Top, left + num4, (float) rect.Bottom);
                    graphics.DrawLine(pen, left + (num4 / 2f), (float) rect.Bottom, left + (num4 / 2f), (float) (rect.Bottom + 2));
                    graphics.DrawString("-" + j.ToString(), font, brush, (float) ((left - 8f) + (num4 / 2f)), (float) (bottom + 2f));
                    left += num4;
                }
                this.fbG.DrawImageUnscaled(image, 0, 0);
                this.pb1.Image = this.frontBuffer;
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
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
            xml.WriteAttributeString("Form", "FormResistsDeathReport");
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

        private void Form5_Closing(object sender, CancelEventArgs e)
        {
            base.Hide();
            try
            {
                this.fbG.Clear(SystemColors.ControlLight);
                this.pb1.Image = this.frontBuffer;
            }
            catch
            {
            }
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

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormResistsDeathReport));
            this.rtb1 = new RichTextBox();
            this.btnCopySelected = new Button();
            this.btnCopyAll = new Button();
            this.btnHide = new Button();
            this.pb1 = new PictureBox();
            this.cmCopySave = new ContextMenu();
            this.cmiCopy = new MenuItem();
            this.cmiSave = new MenuItem();
            this.ddlDeathPicker = new ComboBox();
            this.lblDeathSelect = new Label();
            this.toolTip1 = new ToolTip(this.components);
            ((ISupportInitialize) this.pb1).BeginInit();
            base.SuspendLayout();
            this.rtb1.DetectUrls = false;
            this.rtb1.Location = new Point(8, 8);
            this.rtb1.Name = "rtb1";
            this.rtb1.ReadOnly = true;
            this.rtb1.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            this.rtb1.Size = new Size(0x108, 0xf8);
            this.rtb1.TabIndex = 0;
            this.rtb1.Text = "";
            this.rtb1.WordWrap = false;
            this.btnCopySelected.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnCopySelected.Location = new Point(8, 0x103);
            this.btnCopySelected.Name = "btnCopySelected";
            this.btnCopySelected.Size = new Size(0x80, 0x17);
            this.btnCopySelected.TabIndex = 1;
            this.btnCopySelected.Text = "Copy Selected";
            this.btnCopySelected.Click += new EventHandler(this.btnCopySelected_Click);
            this.btnCopyAll.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnCopyAll.Location = new Point(0x90, 0x103);
            this.btnCopyAll.Name = "btnCopyAll";
            this.btnCopyAll.Size = new Size(0x80, 0x17);
            this.btnCopyAll.TabIndex = 1;
            this.btnCopyAll.Text = "Copy All";
            this.btnCopyAll.Click += new EventHandler(this.btnCopyAll_Click);
            this.btnHide.DialogResult = DialogResult.Cancel;
            this.btnHide.Location = new Point(640, 260);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new Size(80, 0x17);
            this.btnHide.TabIndex = 2;
            this.btnHide.Text = "Close";
            this.btnHide.Click += new EventHandler(this.btnHide_Click);
            this.pb1.BorderStyle = BorderStyle.Fixed3D;
            this.pb1.ContextMenu = this.cmCopySave;
            this.pb1.Location = new Point(280, 8);
            this.pb1.Name = "pb1";
            this.pb1.Size = new Size(440, 0xf8);
            this.pb1.TabIndex = 3;
            this.pb1.TabStop = false;
            this.pb1.MouseMove += new MouseEventHandler(this.pb1_MouseMove);
            this.cmCopySave.MenuItems.AddRange(new MenuItem[] { this.cmiCopy, this.cmiSave });
            this.cmiCopy.Index = 0;
            this.cmiCopy.Text = "Copy";
            this.cmiCopy.Click += new EventHandler(this.cmiCopy_Click);
            this.cmiSave.Index = 1;
            this.cmiSave.Text = "Save As...";
            this.cmiSave.Click += new EventHandler(this.cmiSave_Click);
            this.ddlDeathPicker.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlDeathPicker.Location = new Point(0x1b0, 260);
            this.ddlDeathPicker.Name = "ddlDeathPicker";
            this.ddlDeathPicker.Size = new Size(200, 0x15);
            this.ddlDeathPicker.TabIndex = 4;
            this.ddlDeathPicker.SelectedIndexChanged += new EventHandler(this.ddlDeathPicker_SelectedIndexChanged);
            this.lblDeathSelect.Location = new Point(320, 260);
            this.lblDeathSelect.Name = "lblDeathSelect";
            this.lblDeathSelect.Size = new Size(100, 20);
            this.lblDeathSelect.TabIndex = 5;
            this.lblDeathSelect.Text = "Death Selection:";
            this.lblDeathSelect.TextAlign = ContentAlignment.MiddleLeft;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.CancelButton = this.btnHide;
            base.ClientSize = new Size(730, 0x120);
            base.Controls.Add(this.lblDeathSelect);
            base.Controls.Add(this.ddlDeathPicker);
            base.Controls.Add(this.pb1);
            base.Controls.Add(this.btnHide);
            base.Controls.Add(this.btnCopySelected);
            base.Controls.Add(this.rtb1);
            base.Controls.Add(this.btnCopyAll);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "FormResistsDeathReport";
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Death Report";
            base.TopMost = true;
            base.Closing += new CancelEventHandler(this.Form5_Closing);
            ((ISupportInitialize) this.pb1).EndInit();
            base.ResumeLayout(false);
        }

        private void pb1_MouseMove(object sender, MouseEventArgs e)
        {
            string toolTipTextAt = this.ttg.GetToolTipTextAt(e.X, e.Y);
            if (this.toolTip1.GetToolTip(this.pb1) != toolTipTextAt)
            {
                if (string.IsNullOrWhiteSpace(toolTipTextAt))
                {
                    this.toolTip1.Hide(this.pb1);
                }
                else
                {
                    this.toolTip1.SetToolTip(this.pb1, toolTipTextAt);
                }
            }
        }

        public void ShowDeathReport(CombatantData cD)
        {
            AttackType type;
            this.deathTimes = new List<DateTime>();
            this.deaths = new List<DeathData>();
            if (cD.AllInc.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
            {
                List<MasterSwing> allSwings = new List<MasterSwing>(type.Items);
                allSwings.Sort(new Comparison<MasterSwing>(MasterSwing.CompareTime));
                for (int i = 0; i < allSwings.Count; i++)
                {
                    if (allSwings[i].Damage == Dnum.Death)
                    {
                        this.deathTimes.Add(allSwings[i].Time);
                    }
                }
                string str = string.Empty;
                foreach (DateTime time in this.deathTimes)
                {
                    DeathData item = new DeathData(time, allSwings);
                    this.deaths.Add(item);
                    str = str + string.Format("\n{0} - {1}\n{2}", cD.Name, time.ToLongTimeString(), item.ToString());
                }
                this.Text = "Death Report";
                this.lblDeathSelect.Visible = true;
                this.ddlDeathPicker.Visible = true;
                this.pb1.Visible = true;
                this.rtb1.Text = str;
                this.ddlDeathPicker.Items.Clear();
                for (int j = 0; j < this.deathTimes.Count; j++)
                {
                    this.ddlDeathPicker.Items.Add(this.deathTimes[j]);
                }
                base.Show();
                if (this.ddlDeathPicker.Items.Count > 0)
                {
                    this.ddlDeathPicker.SelectedIndex = this.ddlDeathPicker.Items.Count - 1;
                }
            }
        }

        internal class DeathData
        {
            private SortedList<int, FormResistsDeathReport.DeathSecond> deathSeconds = new SortedList<int, FormResistsDeathReport.DeathSecond>();
            private DateTime timeOfDeath;

            public DeathData(DateTime TimeOfDeath, List<MasterSwing> AllSwings)
            {
                this.timeOfDeath = TimeOfDeath;
                for (int i = 0; i < 10; i++)
                {
                    DateTime time = TimeOfDeath.AddSeconds((double) -i);
                    this.deathSeconds.Add(i, new FormResistsDeathReport.DeathSecond(time));
                    for (int j = 0; j < AllSwings.Count; j++)
                    {
                        if (AllSwings[j].Time == time)
                        {
                            this.deathSeconds[i].AddAction(AllSwings[j]);
                        }
                    }
                }
            }

            public override string ToString()
            {
                StringBuilder builder = new StringBuilder("Time: Dmg (Dmg - Heal) [Dmg Total]\n");
                int num = 0;
                for (int i = 0; i < 10; i++)
                {
                    num += this.deathSeconds[i].Damage;
                    builder.AppendFormat("T-{0}: {1} ({2}) [{3}]\n", new object[] { i, this.deathSeconds[i].Damage, this.deathSeconds[i].Result, num });
                }
                return builder.ToString();
            }

            public SortedList<int, FormResistsDeathReport.DeathSecond> Items
            {
                get
                {
                    return this.deathSeconds;
                }
            }

            public DateTime TimeOfDeath
            {
                get
                {
                    return this.timeOfDeath;
                }
                set
                {
                    this.timeOfDeath = value;
                }
            }
        }

        internal class DeathSecond
        {
            private int damage;
            private int healing;
            private DateTime time;
            private string tooltip = string.Empty;

            public DeathSecond(DateTime Time)
            {
                this.time = Time;
            }

            public void AddAction(MasterSwing action)
            {
                if (CombatantData.DamageSwingTypes.Contains(action.SwingType))
                {
                    this.damage += action.Damage;
                    this.tooltip = this.tooltip + string.Format("{0} - {1} - {2}\n", action.Attacker, action.AttackType, action.Damage);
                }
                if (CombatantData.HealingSwingTypes.Contains(action.SwingType))
                {
                    this.healing += action.Damage;
                    this.tooltip = this.tooltip + string.Format("{0} - {1} - {2}\n", action.Attacker, action.AttackType, action.Damage);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}\n{1}", this.time.ToLongTimeString(), this.tooltip);
            }

            public int Damage
            {
                get
                {
                    return this.damage;
                }
            }

            public int Healing
            {
                get
                {
                    return this.healing;
                }
            }

            public int Result
            {
                get
                {
                    return (this.Damage - this.Healing);
                }
            }
        }
    }
}

