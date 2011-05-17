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

    public class FormSpellRecastCalc : Form
    {
        private Button btnClose;
        private Button btnSendToTimers;
        private CheckBox cbRestrictShortDelays;
        private CheckBox cbUseRecastCalcs;
        private ContextMenu cmCopySave;
        private MenuItem cmiCopy;
        private MenuItem cmiSave;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private IContainer components;
        private Graphics fbG;
        private Bitmap frontBuffer;
        private AttackType lastAttackType;
        private ListView lv1;
        private PictureBox pb1;
        private bool recalculate;
        private int spellDelayMin;
        private TextBox tbDelays;
        private Timer timer1;

        public FormSpellRecastCalc()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
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

        private void btnSendToTimers_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormSpellTimers.tbSpellName.Text = this.lastAttackType.Type;
            ActGlobals.oFormSpellTimers.tbCategory.Text = this.lastAttackType.Items[0].Attacker;
            ActGlobals.oFormSpellTimers.cbAllowMod.Checked = this.cbUseRecastCalcs.Checked;
            if (this.spellDelayMin == 0x3039)
            {
                this.spellDelayMin = 0;
            }
            ActGlobals.oFormSpellTimers.nudRecastDelay.Value = this.spellDelayMin;
            if (MessageBox.Show("The lowest calculated recast has been sent to the spell timer options.  Do you wish to add/replace this timer now?", "Add Timer Now?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ActGlobals.oFormSpellTimers.btnAddEdit_Click(this, new EventArgs());
            }
        }

        private void cbRestrictShortDelays_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowMedian(this.lastAttackType);
        }

        private void cmiCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(this.frontBuffer, true);
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }

        private void cmiSave_Click(object sender, EventArgs e)
        {
            try
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
                dialog.ShowDialog();
                if (dialog.FileName != "")
                {
                    FileStream stream = new FileStream(dialog.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.SetLength(0);
                    this.frontBuffer.Save(stream, ImageFormat.Png);
                    stream.Close();
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }

        private void ColorRows()
        {
            foreach (ListViewItem item in this.lv1.Items)
            {
                if (item.Checked)
                {
                    item.BackColor = Color.LemonChiffon;
                }
                else
                {
                    item.BackColor = Color.White;
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
            xml.WriteAttributeString("Form", "FormSpellRecastCalc");
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

        private void Form6_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormSpellRecastCalc));
            this.pb1 = new PictureBox();
            this.cmCopySave = new ContextMenu();
            this.cmiCopy = new MenuItem();
            this.cmiSave = new MenuItem();
            this.btnClose = new Button();
            this.lv1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.timer1 = new Timer(this.components);
            this.tbDelays = new TextBox();
            this.btnSendToTimers = new Button();
            this.cbRestrictShortDelays = new CheckBox();
            this.cbUseRecastCalcs = new CheckBox();
            ((ISupportInitialize) this.pb1).BeginInit();
            base.SuspendLayout();
            this.pb1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.pb1.BorderStyle = BorderStyle.Fixed3D;
            this.pb1.ContextMenu = this.cmCopySave;
            this.pb1.Location = new Point(8, 8);
            this.pb1.Name = "pb1";
            this.pb1.Size = new Size(0x1c5, 0xd0);
            this.pb1.TabIndex = 0;
            this.pb1.TabStop = false;
            this.cmCopySave.MenuItems.AddRange(new MenuItem[] { this.cmiCopy, this.cmiSave });
            this.cmiCopy.Index = 0;
            this.cmiCopy.Text = "Copy";
            this.cmiCopy.Click += new EventHandler(this.cmiCopy_Click);
            this.cmiSave.Index = 1;
            this.cmiSave.Text = "Save As...";
            this.cmiSave.Click += new EventHandler(this.cmiSave_Click);
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnClose.DialogResult = DialogResult.Cancel;
            this.btnClose.Location = new Point(0x2dd, 0xed);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x12);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.lv1.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lv1.BackColor = Color.White;
            this.lv1.CheckBoxes = true;
            this.lv1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3 });
            this.lv1.ForeColor = Color.Black;
            this.lv1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.lv1.Location = new Point(0x1d3, 8);
            this.lv1.Name = "lv1";
            this.lv1.Size = new Size(0x155, 0xd0);
            this.lv1.TabIndex = 3;
            this.lv1.UseCompatibleStateImageBehavior = false;
            this.lv1.View = View.Details;
            this.lv1.ItemCheck += new ItemCheckEventHandler(this.lv1_ItemCheck);
            this.columnHeader1.Text = "Time";
            this.columnHeader1.Width = 0x70;
            this.columnHeader2.Text = "Combatant";
            this.columnHeader2.Width = 0x65;
            this.columnHeader3.Text = "Damage";
            this.columnHeader3.Width = 0x56;
            this.timer1.Enabled = true;
            this.timer1.Interval = 0x3e8;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.tbDelays.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.tbDelays.BackColor = SystemColors.Control;
            this.tbDelays.BorderStyle = BorderStyle.None;
            this.tbDelays.Location = new Point(8, 220);
            this.tbDelays.Multiline = true;
            this.tbDelays.Name = "tbDelays";
            this.tbDelays.ReadOnly = true;
            this.tbDelays.Size = new Size(0x235, 0x1f);
            this.tbDelays.TabIndex = 4;
            this.tbDelays.Text = "Median Delay is N seconds.";
            this.btnSendToTimers.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnSendToTimers.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnSendToTimers.Location = new Point(0x2dd, 0xda);
            this.btnSendToTimers.Name = "btnSendToTimers";
            this.btnSendToTimers.Size = new Size(0x4b, 0x12);
            this.btnSendToTimers.TabIndex = 5;
            this.btnSendToTimers.Text = "Send to Timers";
            this.btnSendToTimers.UseVisualStyleBackColor = true;
            this.btnSendToTimers.Click += new EventHandler(this.btnSendToTimers_Click);
            this.cbRestrictShortDelays.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.cbRestrictShortDelays.Checked = true;
            this.cbRestrictShortDelays.CheckState = CheckState.Checked;
            this.cbRestrictShortDelays.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cbRestrictShortDelays.Location = new Point(0x243, 0xda);
            this.cbRestrictShortDelays.Name = "cbRestrictShortDelays";
            this.cbRestrictShortDelays.Size = new Size(0x97, 0x10);
            this.cbRestrictShortDelays.TabIndex = 6;
            this.cbRestrictShortDelays.Text = "Use delays between 12-120s";
            this.cbRestrictShortDelays.UseVisualStyleBackColor = true;
            this.cbRestrictShortDelays.CheckedChanged += new EventHandler(this.cbRestrictShortDelays_CheckedChanged);
            this.cbUseRecastCalcs.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.cbUseRecastCalcs.Checked = true;
            this.cbUseRecastCalcs.CheckState = CheckState.Checked;
            this.cbUseRecastCalcs.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cbUseRecastCalcs.Location = new Point(0x243, 0xed);
            this.cbUseRecastCalcs.Name = "cbUseRecastCalcs";
            this.cbUseRecastCalcs.Size = new Size(0x97, 0x10);
            this.cbUseRecastCalcs.TabIndex = 6;
            this.cbUseRecastCalcs.Text = "Recalculate with Recast AAs";
            this.cbUseRecastCalcs.UseVisualStyleBackColor = true;
            this.cbUseRecastCalcs.CheckedChanged += new EventHandler(this.cbRestrictShortDelays_CheckedChanged);
            this.AutoScaleBaseSize = new Size(5, 13);
            base.CancelButton = this.btnClose;
            base.ClientSize = new Size(0x332, 0xff);
            base.Controls.Add(this.cbUseRecastCalcs);
            base.Controls.Add(this.cbRestrictShortDelays);
            base.Controls.Add(this.btnSendToTimers);
            base.Controls.Add(this.tbDelays);
            base.Controls.Add(this.lv1);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.pb1);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "FormSpellRecastCalc";
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Spell Recast Calculation";
            base.TopMost = true;
            base.Closing += new CancelEventHandler(this.Form6_Closing);
            ((ISupportInitialize) this.pb1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void lv1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.recalculate = true;
        }

        private void RecalculateDelays()
        {
            AttackType type = null;
            SolidBrush brush;
            Pen pen;
            Pen pen2;
            float num15;
            if (this.cbUseRecastCalcs.Checked)
            {
                for (int n = 0; n < this.lastAttackType.Parent.Parent.AllInc.Count; n++)
                {
                    if (((this.lastAttackType.Parent.Parent.AllInc.Values[n].Type == "Traumatic Swipe") || (this.lastAttackType.Parent.Parent.AllInc.Values[n].Type == "Traumatischer Greifer")) || ((this.lastAttackType.Parent.Parent.AllInc.Values[n].Type == "Coup traumatisant") || (this.lastAttackType.Parent.Parent.AllInc.Values[n].Type == "Травмирующий удар силача")))
                    {
                        type = this.lastAttackType.Parent.Parent.AllInc.Values[n];
                    }
                }
            }
            List<int> list = new List<int>();
            StringBuilder builder = new StringBuilder("Checked Delays: ");
            int num2 = 0x3039;
            for (int i = 1; i < this.lv1.CheckedItems.Count; i++)
            {
                DateTime tag = (DateTime) this.lv1.CheckedItems[i].Tag;
                DateTime time2 = (DateTime) this.lv1.CheckedItems[i - 1].Tag;
                TimeSpan span = (TimeSpan) (tag - time2);
                int totalSeconds = (int) span.TotalSeconds;
                if (type != null)
                {
                    bool flag = false;
                    if (type != null)
                    {
                        for (int num5 = 0; num5 < type.Items.Count; num5++)
                        {
                            if (((type.Items[num5].Damage > -1) && (time2 > type.Items[num5].Time)) && (time2 < type.Items[num5].Time.AddSeconds(31.0)))
                            {
                                flag = true;
                                CombatantData combatant = type.Parent.Parent.Parent.GetCombatant(type.Items[num5].Attacker);
                                if (combatant != null)
                                {
                                    AttackType type2 = null;
                                    if (!combatant.AllInc.TryGetValue("Killing", out type2))
                                    {
                                        combatant.AllInc.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type2);
                                    }
                                    if (type2 != null)
                                    {
                                        for (int num6 = 0; num6 < type2.Items.Count; num6++)
                                        {
                                            if (((type2.Items[num6].Damage == Dnum.Death) && (type2.Items[num6].Time >= type.Items[num5].Time)) && (type2.Items[num6].Time < time2))
                                            {
                                                flag = false;
                                            }
                                        }
                                    }
                                }
                                if (flag)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    int num7 = 0;
                    if (flag)
                    {
                        num7 += 50;
                    }
                    float num8 = ((float) totalSeconds) / (1f + (((float) num7) / 100f));
                    totalSeconds = (int) num8;
                }
                if ((totalSeconds < num2) && (totalSeconds > 1))
                {
                    num2 = totalSeconds;
                }
                if ((!this.cbRestrictShortDelays.Checked || (totalSeconds < 0x79)) && (totalSeconds > 1))
                {
                    list.Add(totalSeconds);
                    builder.AppendFormat("{0}, ", totalSeconds);
                }
            }
            this.spellDelayMin = num2;
            List<int> list2 = new List<int>();
            foreach (int num9 in list)
            {
                if (!list2.Contains(num9))
                {
                    list2.Add(num9);
                }
            }
            list2.Sort();
            int[] numArray = new int[list2.Count];
            for (int j = 0; j < list2.Count; j++)
            {
                int num11 = list2[j];
                foreach (int num12 in list)
                {
                    if (num12 == num11)
                    {
                        numArray[j]++;
                    }
                }
            }
            this.tbDelays.Text = builder.ToString().TrimEnd(new char[] { ' ', ',' });
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
            new SolidBrush(Color.DarkRed);
            SolidBrush brush2 = new SolidBrush(Color.DarkBlue);
            SolidBrush brush3 = new SolidBrush(Color.White);
            Bitmap image = new Bitmap(this.pb1.Width - 4, this.pb1.Height - 4);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(SystemColors.ControlLight);
            int num13 = 0x10;
            Rectangle rect = new Rectangle(4, 4, (image.Width - 1) - (num13 * 2), (image.Height - 1) - num13);
            graphics.DrawRectangle(pen, rect);
            float bottom = rect.Bottom;
            try
            {
                num15 = rect.Width / list2.Count;
            }
            catch
            {
                return;
            }
            float left = rect.Left;
            float num17 = 0f;
            for (int k = 0; k < numArray.Length; k++)
            {
                if (numArray[k] > num17)
                {
                    num17 = numArray[k];
                }
            }
            float num19 = 4f - (num17 % 4f);
            if (num19 != 4f)
            {
                num17 += num19;
            }
            float num20 = ((float) rect.Height) / num17;
            Font font = new Font("Arial", 8f);
            Font font2 = new Font("Courier New", 8f);
            graphics.DrawString("0", font, brush, (float) (rect.Right + 5), (float) rect.Bottom);
            int num28 = (int) num17;
            graphics.DrawString(num28.ToString(), font, brush, (float) (rect.Right + 5), (float) rect.Top);
            try
            {
                int num21 = Convert.ToInt32((float) (bottom - Convert.ToInt32((float) ((num17 / 4f) * num20))));
                graphics.DrawLine(pen2, rect.Left, num21, rect.Right, num21);
                graphics.DrawString(((int) (num17 / 4f)).ToString(), font, brush, (float) (rect.Right + 5), (float) num21);
                num21 = Convert.ToInt32((float) (bottom - Convert.ToInt32((float) (((num17 / 4f) * 2f) * num20))));
                graphics.DrawLine(pen2, rect.Left, num21, rect.Right, num21);
                graphics.DrawString(((int) ((num17 / 4f) * 2f)).ToString(), font, brush, (float) (rect.Right + 5), (float) num21);
                num21 = Convert.ToInt32((float) (bottom - Convert.ToInt32((float) (((num17 / 4f) * 3f) * num20))));
                graphics.DrawLine(pen2, rect.Left, num21, rect.Right, num21);
                graphics.DrawString(((int) ((num17 / 4f) * 3f)).ToString(), font, brush, (float) (rect.Right + 5), (float) num21);
            }
            catch
            {
            }
            for (int m = 0; m < list2.Count; m++)
            {
                float x = left + (num15 / 4f);
                float y = bottom - (numArray[m] * num20);
                float width = num15 / 2f;
                float height = numArray[m] * num20;
                RectangleF ef = new RectangleF(x, y, width, height);
                graphics.FillRectangle(brush2, ef);
                graphics.DrawRectangle(pen, ef.X, ef.Y, ef.Width, ef.Height);
                StringFormat format = new StringFormat {
                    FormatFlags = StringFormatFlags.DirectionVertical
                };
                int num27 = ((int) (ef.Height / 6f)) - 2;
                SizeF ef2 = graphics.MeasureString(numArray[m].ToString(), font2);
                if (numArray[m].ToString().Length < num27)
                {
                    graphics.DrawString(numArray[m].ToString(), font2, brush3, (left - 8f) + (num15 / 2f), (bottom - (numArray[m] * num20)) + 12f, format);
                }
                else
                {
                    graphics.DrawString(numArray[m].ToString(), font2, brush2, (left - 8f) + (num15 / 2f), ((bottom - (numArray[m] * num20)) - ef2.Width) - 4f, format);
                }
                graphics.DrawLine(pen, left + (num15 / 2f), (float) rect.Bottom, left + (num15 / 2f), (float) (rect.Bottom + 2));
                graphics.DrawString(list2[m].ToString(), font, brush, (float) ((left - 8f) + (num15 / 2f)), (float) (bottom + 2f));
                left += num15;
            }
            this.fbG.DrawImageUnscaled(image, 0, 0);
            this.pb1.Image = this.frontBuffer;
        }

        public void ShowMedian(AttackType aT)
        {
            ListViewItem item;
            this.lastAttackType = aT;
            List<MasterSwing> list = new List<MasterSwing>(aT.Items);
            try
            {
                list.Sort(new Comparison<MasterSwing>(MasterSwing.CompareTime));
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
            base.Show();
            this.lv1.Items.Clear();
            this.lv1.BeginUpdate();
            if (aT.Parent.Outgoing)
            {
                item = new ListViewItem(new string[] { list[0].Time.ToLongTimeString(), list[0].Victim, list[0].Damage.ToString() });
            }
            else
            {
                item = new ListViewItem(new string[] { list[0].Time.ToLongTimeString(), list[0].Attacker, list[0].Damage.ToString() });
            }
            item.Tag = list[0].Time;
            this.lv1.Items.Add(item);
            this.lv1.Items[0].Checked = true;
            for (int i = 1; i < list.Count; i++)
            {
                MasterSwing swing = list[i];
                MasterSwing swing2 = list[i - 1];
                TimeSpan span = (TimeSpan) (swing.Time - swing2.Time);
                int totalSeconds = (int) span.TotalSeconds;
                if (aT.Parent.Outgoing)
                {
                    item = new ListViewItem(new string[] { swing.Time.ToLongTimeString(), swing.Victim, swing.Damage.ToString() });
                }
                else
                {
                    item = new ListViewItem(new string[] { swing.Time.ToLongTimeString(), swing.Attacker, swing.Damage.ToString() });
                }
                item.Tag = swing.Time;
                if (this.cbRestrictShortDelays.Checked)
                {
                    if (totalSeconds > 11)
                    {
                        item.Checked = true;
                    }
                    else
                    {
                        item.Checked = false;
                    }
                }
                else if (totalSeconds > 1)
                {
                    item.Checked = true;
                }
                else
                {
                    item.Checked = false;
                }
                this.lv1.Items.Add(item);
            }
            this.ColorRows();
            this.lv1.EndUpdate();
            this.RecalculateDelays();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.recalculate)
            {
                this.recalculate = false;
                this.RecalculateDelays();
                this.ColorRows();
            }
        }
    }
}

