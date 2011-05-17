namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class FormTimeLine : Form
    {
        private Button btnApplyRecalc;
        private Button btnCheckAll;
        private Button btnInvertChecked;
        private Button btnZoomIn;
        private Button btnZoomOut;
        private CheckBox cbAllowRedraw;
        internal CheckBox cbGlobalMaxLimit;
        internal CheckBox cbTLIncomingDamage;
        internal CheckBox cbTLOutDamage;
        internal CheckBox cbTLOutgoingHealing;
        internal CheckBox cbTLRollingAvg;
        private ColumnHeader columnHeader1;
        private IContainer components;
        private int dataPoints;
        internal ComboBox ddlTLGrouping;
        private EncounterData eD;
        private Graphics fbG;
        private string formTitle;
        private Bitmap frontBuffer;
        private GroupBox groupBox;
        private Label label1;
        private Label label2;
        private ListView lvVisibleCombatants;
        internal NumericUpDown nudTLSampleSize;
        private Panel panel1;
        private PictureBox pb1;
        private bool recalcEventRaised;
        private bool redrawEventRaised;
        private float rowHeight = 32f;
        private List<string> rowLabels = new List<string>();
        private Dictionary<string, int[,]> rowOwners = new Dictionary<string, int[,]>();
        private Timer tmrRedraw;
        private ToolTip tt1;
        private ToolTipGrid ttg = new ToolTipGrid();
        private Color[] typeColors = new Color[] { Color.SlateBlue, Color.Goldenrod, Color.Purple, Color.Crimson, Color.SlateGray };

        public FormTimeLine()
        {
            this.InitializeComponent();
            this.frontBuffer = new Bitmap(this.pb1.Width, this.pb1.Height);
            this.fbG = Graphics.FromImage(this.frontBuffer);
            this.fbG.SmoothingMode = SmoothingMode.AntiAlias;
            this.fbG.Clear(SystemColors.ControlLight);
        }

        private void btnApplyRecalc_Click(object sender, EventArgs e)
        {
            this.recalcEventRaised = true;
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.lvVisibleCombatants.Items.Count; i++)
            {
                this.lvVisibleCombatants.Items[i].Checked = true;
            }
        }

        private void btnInvertChecked_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.lvVisibleCombatants.Items.Count; i++)
            {
                this.lvVisibleCombatants.Items[i].Checked = !this.lvVisibleCombatants.Items[i].Checked;
            }
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            try
            {
                this.rowHeight *= 1.2f;
                this.redrawEventRaised = true;
                this.panel1.Focus();
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            try
            {
                this.rowHeight /= 1.2f;
                this.redrawEventRaised = true;
                this.panel1.Focus();
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }

        private void cbAllowRedraw_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbAllowRedraw.CheckState == CheckState.Checked)
            {
                this.redrawEventRaised = true;
            }
        }

        private void cbGlobalMaxLimit_CheckedChanged(object sender, EventArgs e)
        {
            this.redrawEventRaised = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DrawTimeline()
        {
            try
            {
                if (base.Visible)
                {
                    int length;
                    int num27;
                    this.pb1.Height = Convert.ToInt32((float) ((this.rowHeight * ((this.rowOwners.Count * this.rowLabels.Count) + 1)) + 32f));
                    this.ttg = new ToolTipGrid();
                    Bitmap image = new Bitmap(this.pb1.Width, this.pb1.Height);
                    Graphics graphics = Graphics.FromImage(image);
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.Clear(Color.White);
                    Pen pen = new Pen(Color.White, 0.5f);
                    new Pen(Color.Black, 0.5f);
                    Pen pen2 = new Pen(Color.Black, 1f);
                    new Pen(Color.Black, 1.5f);
                    new Pen(Color.Black, 2f);
                    new Pen(Color.Black, 2.5f);
                    SolidBrush brush = new SolidBrush(Color.Black);
                    SolidBrush brush2 = new SolidBrush(Color.FromArgb(100, Color.Black));
                    SolidBrush brush3 = new SolidBrush(Color.Gray);
                    SolidBrush brush4 = new SolidBrush(Color.WhiteSmoke);
                    SolidBrush brush5 = new SolidBrush(Color.Gainsboro);
                    Font font = new Font("Arial", 8f);
                    Font font2 = new Font("Arial", 6f);
                    Font font3 = new Font("Arial Black", 12f);
                    SolidBrush brush6 = new SolidBrush(Color.FromArgb(0xaf, Color.Orange));
                    SolidBrush brush7 = new SolidBrush(Color.FromArgb(0xaf, Color.Red));
                    SolidBrush brush8 = new SolidBrush(Color.FromArgb(0xaf, Color.CornflowerBlue));
                    HatchBrush brush9 = new HatchBrush(HatchStyle.WideDownwardDiagonal, Color.Transparent, Color.Gray);
                    RectangleF ef = new RectangleF(0f, 0f, (float) this.pb1.Width, (float) this.pb1.Height);
                    RectangleF ef2 = new RectangleF(ef.X + 128f, ef.Y + 16f, ef.Width - 136f, ef.Height - 36f);
                    int num = (int) this.nudTLSampleSize.Value;
                    float top = ef2.Top;
                    float w = ef2.Width / ((float) this.dataPoints);
                    bool flag = this.cbGlobalMaxLimit.Checked;
                    bool flag2 = this.cbTLRollingAvg.Checked;
                    float num4 = 1f;
                    int num5 = 1;
                    int[] numArray = new int[this.rowLabels.Count];
                    if (flag)
                    {
                        foreach (KeyValuePair<string, int[,]> pair in this.rowOwners)
                        {
                            bool flag3 = false;
                            for (int i = 0; i < this.lvVisibleCombatants.Items.Count; i++)
                            {
                                ListViewItem item = this.lvVisibleCombatants.Items[i];
                                if (item.Text == pair.Key)
                                {
                                    flag3 = item.Checked;
                                    break;
                                }
                            }
                            if (flag3)
                            {
                                for (int j = 0; j < this.rowLabels.Count; j++)
                                {
                                    num4 = 0f;
                                    num5 = 0;
                                    for (int k = 0; k <= this.dataPoints; k++)
                                    {
                                        if (((float) pair.Value[j, k]) > num4)
                                        {
                                            num4 = (float) pair.Value[j, k];
                                        }
                                    }
                                    length = (int) num4;
                                    length = length.ToString().Length;
                                    num5 = (int) Math.Pow(10.0, (double) length);
                                    while ((num5 / 2) > num4)
                                    {
                                        num5 /= 2;
                                    }
                                    if ((((float) num5) / 1.25f) > num4)
                                    {
                                        num5 = Convert.ToInt32((float) (((float) num5) / 1.25f));
                                    }
                                    if (num5 > numArray[j])
                                    {
                                        numArray[j] = num5;
                                    }
                                }
                            }
                        }
                    }
                    bool flag4 = false;
                    foreach (KeyValuePair<string, int[,]> pair2 in this.rowOwners)
                    {
                        bool flag5 = false;
                        for (int m = 0; m < this.lvVisibleCombatants.Items.Count; m++)
                        {
                            ListViewItem item2 = this.lvVisibleCombatants.Items[m];
                            if (item2.Text == pair2.Key)
                            {
                                flag5 = item2.Checked;
                                break;
                            }
                        }
                        if (flag5)
                        {
                            flag4 = !flag4;
                            graphics.DrawString(pair2.Key, font, brush, new RectangleF(6f, top + 2f, 118f, 20f), new StringFormat(StringFormatFlags.NoWrap));
                            for (int n = 0; n < this.rowLabels.Count; n++)
                            {
                                graphics.DrawString(this.rowLabels[n], font, brush3, new RectangleF(12f, top, 112f, this.rowHeight));
                                if (flag4)
                                {
                                    graphics.FillRectangle(brush4, ef2.X, top, ef2.Width, this.rowHeight);
                                }
                                else
                                {
                                    graphics.FillRectangle(brush5, ef2.X, top, ef2.Width, this.rowHeight);
                                }
                                graphics.DrawRectangle(pen2, ef2.X, top, ef2.Width, this.rowHeight);
                                this.ttg.Items.Add(new ToolTipRect(-1, pair2.Key, ef2.X, top, ef2.Width, this.rowHeight));
                                string str3 = this.rowLabels[n];
                                if (str3 == null)
                                {
                                    goto Label_05AD;
                                }
                                if (!(str3 == "Outgoing Damage"))
                                {
                                    if (str3 == "Incoming Damage")
                                    {
                                        goto Label_05A1;
                                    }
                                    if (str3 == "Outgoing Healing")
                                    {
                                        goto Label_05A7;
                                    }
                                    goto Label_05AD;
                                }
                                SolidBrush brush10 = brush6;
                                goto Label_05B1;
                            Label_05A1:
                                brush10 = brush7;
                                goto Label_05B1;
                            Label_05A7:
                                brush10 = brush8;
                                goto Label_05B1;
                            Label_05AD:
                                brush10 = brush3;
                            Label_05B1:
                                num4 = 1f;
                                num5 = 1;
                                if (!flag)
                                {
                                    for (int num12 = 0; num12 <= this.dataPoints; num12++)
                                    {
                                        if (((float) pair2.Value[n, num12]) > num4)
                                        {
                                            num4 = (float) pair2.Value[n, num12];
                                        }
                                    }
                                    length = (int) num4;
                                    length = length.ToString().Length;
                                    num5 = (int) Math.Pow(10.0, (double) length);
                                    while ((num5 / 2) > num4)
                                    {
                                        num5 /= 2;
                                    }
                                    if ((((float) num5) / 1.25f) > num4)
                                    {
                                        num5 = Convert.ToInt32((float) (((float) num5) / 1.25f));
                                    }
                                }
                                else
                                {
                                    num5 = numArray[n];
                                }
                                float num13 = (this.rowHeight / ((float) num5)) / 2f;
                                float num14 = (this.rowHeight / 2f) + top;
                                graphics.DrawLine(pen, ef2.Left, num14, ef2.Right, num14);
                                if (n > 0)
                                {
                                    PointF[] points = new PointF[2 + this.dataPoints];
                                    points[0] = new PointF(ef2.X, num14);
                                    for (int num15 = 1; num15 < (this.dataPoints + 1); num15++)
                                    {
                                        float y = num14 - (((float) pair2.Value[n, num15]) * num13);
                                        float x = (w * num15) + ef2.X;
                                        this.ttg.Items.Add(new ToolTipRect(-1, this.rowLabels[n] + ": " + ((int) (pair2.Value[n, num15] / num)), x - (w / 2f), top, w, this.rowHeight));
                                        points[num15] = new PointF(x, y);
                                    }
                                    points[points.Length - 1] = new PointF(ef2.Right, num14);
                                    graphics.FillPolygon(brush10, points);
                                    for (int num18 = 1; num18 < (this.dataPoints + 1); num18++)
                                    {
                                        float num19 = num14 + (((float) pair2.Value[n, num18]) * num13);
                                        float num20 = (w * num18) + ef2.X;
                                        points[num18] = new PointF(num20, num19);
                                    }
                                    graphics.FillPolygon(brush10, points);
                                    graphics.DrawString(string.Format("{0}", num5 / num), font2, brush3, new RectangleF(ef2.X + 2f, top + 1f, 112f, this.rowHeight));
                                }
                                else
                                {
                                    for (int num21 = 1; num21 < (this.dataPoints + 1); num21++)
                                    {
                                        float num22 = (w * num21) + ef2.X;
                                        BitArray array = new BitArray(new int[] { pair2.Value[0, num21] });
                                        if (array[0])
                                        {
                                            graphics.DrawString("K", font3, brush2, (float) (num22 - 8f), (float) (top + ((this.rowHeight / 5f) * 0f)));
                                        }
                                        if (array[1])
                                        {
                                            graphics.DrawString("D", font3, brush2, (float) (num22 - 8f), (float) (top + ((this.rowHeight / 5f) * 1f)));
                                        }
                                        if (array[2])
                                        {
                                            graphics.DrawString("S", font3, brush2, (float) (num22 - 8f), (float) (top + ((this.rowHeight / 5f) * 2f)));
                                        }
                                    }
                                }
                                top += this.rowHeight;
                            }
                        }
                    }
                    if (flag2)
                    {
                        for (int num23 = 1; num23 < (this.dataPoints + 1); num23++)
                        {
                            float num24 = (w * num23) + ef2.X;
                            string toolTipText = string.Concat(new object[] { "+", num23, "  ", this.eD.StartTime.AddSeconds((double) num23).ToLongTimeString() });
                            this.ttg.Items.Insert(0, new ToolTipRect(num23, toolTipText, num24 - (w / 2f), ef2.Top, w, ef2.Height));
                        }
                    }
                    else
                    {
                        for (int num25 = 1; num25 < (this.dataPoints + 1); num25++)
                        {
                            float num26 = (w * num25) + ef2.X;
                            string str2 = string.Concat(new object[] { "+", num25 * num, "  ", this.eD.StartTime.AddSeconds((double) (num25 * num)).ToLongTimeString(), " to ", this.eD.StartTime.AddSeconds((double) ((num25 + 1) * num)).ToLongTimeString() });
                            this.ttg.Items.Insert(0, new ToolTipRect(num25 * num, str2, num26 - (w / 2f), ef2.Top, w, ef2.Height));
                        }
                    }
                    if (!flag2)
                    {
                        w /= (float) num;
                    }
                    if (flag2)
                    {
                        for (num27 = 960; (w * num27) > 100f; num27 /= 2)
                        {
                        }
                    }
                    else
                    {
                        num27 = num;
                        while ((w * num27) < 50f)
                        {
                            num27 *= 2;
                        }
                    }
                    float left = ef2.Left;
                    int num29 = 0;
                    while (left < ef2.Right)
                    {
                        num29 += num27;
                        left += w * num27;
                        graphics.FillRectangle(brush9, (float) ((int) left), ef2.Top + 1f, 0.5f, ef2.Height - 2f);
                        graphics.DrawString(string.Format("+{0:0}", num29), font2, brush3, (float) (((int) left) - 3), ef2.Top - 9f);
                    }
                    graphics.Flush();
                    this.fbG.DrawImageUnscaled(image, 0, 0);
                    this.pb1.Image = this.frontBuffer;
                    this.formTitle = "Timeline";
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
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
            xml.WriteStartElement(string.Empty, "ControlText", string.Empty);
            xml.WriteAttributeString("Form", "FormTimeLine");
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

        private void Form11_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        private void FormTimeLine_Load(object sender, EventArgs e)
        {
            if (this.ddlTLGrouping.SelectedIndex == -1)
            {
                this.ddlTLGrouping.SelectedIndex = 0;
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormTimeLine));
            this.pb1 = new PictureBox();
            this.tt1 = new ToolTip(this.components);
            this.btnZoomIn = new Button();
            this.btnZoomOut = new Button();
            this.cbAllowRedraw = new CheckBox();
            this.panel1 = new Panel();
            this.tmrRedraw = new Timer(this.components);
            this.groupBox = new GroupBox();
            this.lvVisibleCombatants = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.btnInvertChecked = new Button();
            this.btnCheckAll = new Button();
            this.btnApplyRecalc = new Button();
            this.nudTLSampleSize = new NumericUpDown();
            this.ddlTLGrouping = new ComboBox();
            this.label2 = new Label();
            this.cbGlobalMaxLimit = new CheckBox();
            this.cbTLOutgoingHealing = new CheckBox();
            this.cbTLIncomingDamage = new CheckBox();
            this.cbTLOutDamage = new CheckBox();
            this.cbTLRollingAvg = new CheckBox();
            this.label1 = new Label();
            ((ISupportInitialize) this.pb1).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.nudTLSampleSize.BeginInit();
            base.SuspendLayout();
            this.pb1.Dock = DockStyle.Top;
            this.pb1.Location = new Point(0, 0);
            this.pb1.Name = "pb1";
            this.pb1.Size = new Size(0x35b, 590);
            this.pb1.TabIndex = 0;
            this.pb1.TabStop = false;
            this.pb1.MouseClick += new MouseEventHandler(this.pb1_MouseClick);
            this.pb1.MouseMove += new MouseEventHandler(this.pb1_MouseMove);
            this.pb1.Resize += new EventHandler(this.pb1_Resize);
            this.tt1.AutoPopDelay = 0x2710;
            this.tt1.InitialDelay = 100;
            this.tt1.ReshowDelay = 100;
            this.tt1.UseAnimation = false;
            this.tt1.UseFading = false;
            this.btnZoomIn.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnZoomIn.BackColor = SystemColors.ControlDark;
            this.btnZoomIn.FlatStyle = FlatStyle.Flat;
            this.btnZoomIn.ForeColor = SystemColors.ControlLightLight;
            this.btnZoomIn.Location = new Point(8, 500);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new Size(0x18, 0x18);
            this.btnZoomIn.TabIndex = 1;
            this.btnZoomIn.Text = "+";
            this.tt1.SetToolTip(this.btnZoomIn, "Zoom In");
            this.btnZoomIn.UseVisualStyleBackColor = false;
            this.btnZoomIn.Click += new EventHandler(this.btnZoomIn_Click);
            this.btnZoomOut.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnZoomOut.BackColor = SystemColors.ControlDark;
            this.btnZoomOut.FlatStyle = FlatStyle.Flat;
            this.btnZoomOut.ForeColor = SystemColors.ControlLightLight;
            this.btnZoomOut.Location = new Point(0x1f, 500);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new Size(0x18, 0x18);
            this.btnZoomOut.TabIndex = 1;
            this.btnZoomOut.Text = "-";
            this.tt1.SetToolTip(this.btnZoomOut, "Zoom Out");
            this.btnZoomOut.UseVisualStyleBackColor = false;
            this.btnZoomOut.Click += new EventHandler(this.btnZoomOut_Click);
            this.cbAllowRedraw.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.cbAllowRedraw.Appearance = Appearance.Button;
            this.cbAllowRedraw.BackColor = SystemColors.ControlDark;
            this.cbAllowRedraw.Checked = true;
            this.cbAllowRedraw.CheckState = CheckState.Checked;
            this.cbAllowRedraw.FlatStyle = FlatStyle.Flat;
            this.cbAllowRedraw.ForeColor = SystemColors.ControlLightLight;
            this.cbAllowRedraw.Location = new Point(0x36, 500);
            this.cbAllowRedraw.Name = "cbAllowRedraw";
            this.cbAllowRedraw.Size = new Size(0x18, 0x18);
            this.cbAllowRedraw.TabIndex = 3;
            this.cbAllowRedraw.Text = "R";
            this.cbAllowRedraw.TextAlign = ContentAlignment.MiddleCenter;
            this.tt1.SetToolTip(this.cbAllowRedraw, "Allow Timeline Redraw");
            this.cbAllowRedraw.UseVisualStyleBackColor = false;
            this.cbAllowRedraw.CheckedChanged += new EventHandler(this.cbAllowRedraw_CheckedChanged);
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pb1);
            this.panel1.Location = new Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(880, 0x205);
            this.panel1.TabIndex = 3;
            this.panel1.Resize += new EventHandler(this.panel1_Resize);
            this.tmrRedraw.Enabled = true;
            this.tmrRedraw.Interval = 0x3e8;
            this.tmrRedraw.Tick += new EventHandler(this.tmrRedraw_Tick);
            this.groupBox.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            this.groupBox.Controls.Add(this.lvVisibleCombatants);
            this.groupBox.Controls.Add(this.btnInvertChecked);
            this.groupBox.Controls.Add(this.btnCheckAll);
            this.groupBox.Controls.Add(this.btnApplyRecalc);
            this.groupBox.Controls.Add(this.nudTLSampleSize);
            this.groupBox.Controls.Add(this.ddlTLGrouping);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.cbGlobalMaxLimit);
            this.groupBox.Controls.Add(this.cbTLOutgoingHealing);
            this.groupBox.Controls.Add(this.cbTLIncomingDamage);
            this.groupBox.Controls.Add(this.cbTLOutDamage);
            this.groupBox.Controls.Add(this.cbTLRollingAvg);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Location = new Point(0x37e, 8);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new Size(0xe2, 0x205);
            this.groupBox.TabIndex = 4;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Event Toggles";
            this.lvVisibleCombatants.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lvVisibleCombatants.CheckBoxes = true;
            this.lvVisibleCombatants.Columns.AddRange(new ColumnHeader[] { this.columnHeader1 });
            this.lvVisibleCombatants.FullRowSelect = true;
            this.lvVisibleCombatants.HeaderStyle = ColumnHeaderStyle.None;
            this.lvVisibleCombatants.Location = new Point(6, 0xc9);
            this.lvVisibleCombatants.MultiSelect = false;
            this.lvVisibleCombatants.Name = "lvVisibleCombatants";
            this.lvVisibleCombatants.Size = new Size(0x94, 310);
            this.lvVisibleCombatants.TabIndex = 7;
            this.lvVisibleCombatants.UseCompatibleStateImageBehavior = false;
            this.lvVisibleCombatants.View = View.Details;
            this.lvVisibleCombatants.ItemChecked += new ItemCheckedEventHandler(this.lvVisibleCombatants_ItemChecked);
            this.columnHeader1.Width = 0x8f;
            this.btnInvertChecked.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnInvertChecked.Location = new Point(160, 0xf5);
            this.btnInvertChecked.Name = "btnInvertChecked";
            this.btnInvertChecked.Size = new Size(60, 0x26);
            this.btnInvertChecked.TabIndex = 6;
            this.btnInvertChecked.Text = "Invert Checked";
            this.btnInvertChecked.UseVisualStyleBackColor = true;
            this.btnInvertChecked.Click += new EventHandler(this.btnInvertChecked_Click);
            this.btnCheckAll.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnCheckAll.Location = new Point(160, 0xc9);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new Size(60, 0x26);
            this.btnCheckAll.TabIndex = 6;
            this.btnCheckAll.Text = "Check All";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new EventHandler(this.btnCheckAll_Click);
            this.btnApplyRecalc.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnApplyRecalc.Location = new Point(6, 0x9f);
            this.btnApplyRecalc.Name = "btnApplyRecalc";
            this.btnApplyRecalc.Size = new Size(0xd6, 0x12);
            this.btnApplyRecalc.TabIndex = 6;
            this.btnApplyRecalc.Text = "Apply above settings and recalculate";
            this.btnApplyRecalc.UseVisualStyleBackColor = true;
            this.btnApplyRecalc.Click += new EventHandler(this.btnApplyRecalc_Click);
            this.nudTLSampleSize.Location = new Point(0x97, 0x3e);
            int[] bits = new int[4];
            bits[0] = 600;
            this.nudTLSampleSize.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.nudTLSampleSize.Minimum = new decimal(numArray2);
            this.nudTLSampleSize.Name = "nudTLSampleSize";
            this.nudTLSampleSize.Size = new Size(0x45, 20);
            this.nudTLSampleSize.TabIndex = 4;
            int[] numArray3 = new int[4];
            numArray3[0] = 10;
            this.nudTLSampleSize.Value = new decimal(numArray3);
            this.ddlTLGrouping.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlTLGrouping.FormattingEnabled = true;
            this.ddlTLGrouping.Items.AddRange(new object[] { "Individual", "Merged Archtypes VS Them(Split)", "Us VS Them", "Us VS Them(Split)" });
            this.ddlTLGrouping.Location = new Point(6, 0x24);
            this.ddlTLGrouping.Name = "ddlTLGrouping";
            this.ddlTLGrouping.Size = new Size(0xd6, 0x15);
            this.ddlTLGrouping.TabIndex = 0;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(7, 0x41);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x7b, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Sample Detail (Seconds)";
            this.cbGlobalMaxLimit.AutoSize = true;
            this.cbGlobalMaxLimit.Location = new Point(6, 180);
            this.cbGlobalMaxLimit.Margin = new Padding(3, 0, 3, 0);
            this.cbGlobalMaxLimit.Name = "cbGlobalMaxLimit";
            this.cbGlobalMaxLimit.Size = new Size(0xba, 0x11);
            this.cbGlobalMaxLimit.TabIndex = 3;
            this.cbGlobalMaxLimit.Text = "Same type rows share same scale";
            this.cbGlobalMaxLimit.UseVisualStyleBackColor = true;
            this.cbGlobalMaxLimit.CheckedChanged += new EventHandler(this.cbGlobalMaxLimit_CheckedChanged);
            this.cbTLOutgoingHealing.AutoSize = true;
            this.cbTLOutgoingHealing.Location = new Point(6, 0x8b);
            this.cbTLOutgoingHealing.Margin = new Padding(3, 0, 3, 0);
            this.cbTLOutgoingHealing.Name = "cbTLOutgoingHealing";
            this.cbTLOutgoingHealing.Size = new Size(0x6c, 0x11);
            this.cbTLOutgoingHealing.TabIndex = 3;
            this.cbTLOutgoingHealing.Text = "Outgoing Healing";
            this.cbTLOutgoingHealing.UseVisualStyleBackColor = true;
            this.cbTLIncomingDamage.AutoSize = true;
            this.cbTLIncomingDamage.Checked = true;
            this.cbTLIncomingDamage.CheckState = CheckState.Checked;
            this.cbTLIncomingDamage.Location = new Point(6, 0x7a);
            this.cbTLIncomingDamage.Margin = new Padding(3, 0, 3, 0);
            this.cbTLIncomingDamage.Name = "cbTLIncomingDamage";
            this.cbTLIncomingDamage.Size = new Size(0x70, 0x11);
            this.cbTLIncomingDamage.TabIndex = 3;
            this.cbTLIncomingDamage.Text = "Incoming Damage";
            this.cbTLIncomingDamage.UseVisualStyleBackColor = true;
            this.cbTLOutDamage.AutoSize = true;
            this.cbTLOutDamage.Checked = true;
            this.cbTLOutDamage.CheckState = CheckState.Checked;
            this.cbTLOutDamage.Location = new Point(6, 0x69);
            this.cbTLOutDamage.Margin = new Padding(3, 0, 3, 0);
            this.cbTLOutDamage.Name = "cbTLOutDamage";
            this.cbTLOutDamage.Size = new Size(0x70, 0x11);
            this.cbTLOutDamage.TabIndex = 3;
            this.cbTLOutDamage.Text = "Outgoing Damage";
            this.cbTLOutDamage.UseVisualStyleBackColor = true;
            this.cbTLRollingAvg.AutoSize = true;
            this.cbTLRollingAvg.Checked = true;
            this.cbTLRollingAvg.CheckState = CheckState.Checked;
            this.cbTLRollingAvg.Location = new Point(6, 0x51);
            this.cbTLRollingAvg.Name = "cbTLRollingAvg";
            this.cbTLRollingAvg.Size = new Size(0xd6, 0x11);
            this.cbTLRollingAvg.TabIndex = 2;
            this.cbTLRollingAvg.Text = "Use rolling average plots (not segments)";
            this.cbTLRollingAvg.UseVisualStyleBackColor = true;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Combatant Grouping";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x46c, 0x215);
            base.Controls.Add(this.groupBox);
            base.Controls.Add(this.cbAllowRedraw);
            base.Controls.Add(this.btnZoomIn);
            base.Controls.Add(this.btnZoomOut);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "FormTimeLine";
            this.Text = "Timeline (Right-click a time offset to view log lines)";
            base.FormClosing += new FormClosingEventHandler(this.Form11_FormClosing);
            base.Load += new EventHandler(this.FormTimeLine_Load);
            ((ISupportInitialize) this.pb1).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.nudTLSampleSize.EndInit();
            base.ResumeLayout(false);
        }

        private void lvVisibleCombatants_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            this.redrawEventRaised = true;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            try
            {
                if (base.WindowState != FormWindowState.Minimized)
                {
                    this.redrawEventRaised = true;
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }

        private void pb1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                List<int> itemIndicesAt = this.ttg.GetItemIndicesAt(e.X, e.Y);
                DateTime timeStamp = this.eD.StartTime.AddSeconds((double) itemIndicesAt[0]);
                ActGlobals.oFormEncounterLogs.ShowLogs(this.eD.LogLines);
                ActGlobals.oFormEncounterLogs.HighlightDateTime(timeStamp);
            }
        }

        private void pb1_MouseMove(object sender, MouseEventArgs e)
        {
            string toolTipTextAt = this.ttg.GetToolTipTextAt(e.X, e.Y);
            if (string.IsNullOrEmpty(toolTipTextAt))
            {
                this.tt1.Hide(this.pb1);
            }
            else if (toolTipTextAt.Length > 0x3e8)
            {
                this.tt1.SetToolTip(this.pb1, "This tool tip is too large.  Please Middle-Click to view separately.");
            }
            else
            {
                this.tt1.SetToolTip(this.pb1, toolTipTextAt);
            }
        }

        private void pb1_Resize(object sender, EventArgs e)
        {
            try
            {
                if (base.WindowState != FormWindowState.Minimized)
                {
                    this.frontBuffer = new Bitmap(this.pb1.Width, this.pb1.Height);
                    this.fbG = Graphics.FromImage(this.frontBuffer);
                    this.fbG.SmoothingMode = SmoothingMode.AntiAlias;
                    this.fbG.Clear(SystemColors.ControlLight);
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
            this.redrawEventRaised = true;
        }

        public void ShowTimeline(EncounterData Encounter)
        {
            try
            {
                List<CombatantData> list8;
                int num6;
                base.Show();
                this.frontBuffer = new Bitmap(this.pb1.Width, this.pb1.Height);
                this.fbG = Graphics.FromImage(this.frontBuffer);
                this.fbG.SmoothingMode = SmoothingMode.AntiAlias;
                this.fbG.Clear(SystemColors.ControlLight);
                this.fbG.Flush();
                this.pb1.Image = this.frontBuffer;
                Application.DoEvents();
                this.eD = Encounter;
                bool flag = this.cbTLRollingAvg.Checked;
                this.lvVisibleCombatants.Items.Clear();
                this.rowOwners.Clear();
                this.rowLabels.Clear();
                this.rowLabels.Add(string.Empty);
                if (this.cbTLOutDamage.Checked)
                {
                    this.rowLabels.Add("Outgoing Damage");
                }
                if (this.cbTLIncomingDamage.Checked)
                {
                    this.rowLabels.Add("Incoming Damage");
                }
                if (this.cbTLOutgoingHealing.Checked)
                {
                    this.rowLabels.Add("Outgoing Healing");
                }
                int totalSeconds = (int) this.eD.Duration.TotalSeconds;
                int num2 = (int) this.nudTLSampleSize.Value;
                if (flag)
                {
                    this.dataPoints = totalSeconds;
                }
                else
                {
                    this.dataPoints = totalSeconds / num2;
                }
                List<CombatantData> list = new List<CombatantData>(this.eD.Items.Values);
                List<CombatantData> allies = this.eD.GetAllies(false);
                Dictionary<string, List<CombatantData>> dictionary = new Dictionary<string, List<CombatantData>>();
                string text = this.ddlTLGrouping.Text;
                if (text == null)
                {
                    goto Label_0372;
                }
                if (!(text == "Merged Archtypes VS Them(Split)"))
                {
                    if (text == "Us VS Them")
                    {
                        goto Label_02BD;
                    }
                    if (text == "Us VS Them(Split)")
                    {
                        goto Label_0318;
                    }
                    goto Label_0372;
                }
                List<CombatantData> list3 = new List<CombatantData>();
                List<CombatantData> list4 = new List<CombatantData>();
                List<CombatantData> list5 = new List<CombatantData>();
                List<CombatantData> list6 = new List<CombatantData>();
                dictionary.Add("Tanks", list3);
                dictionary.Add("Healers", list4);
                dictionary.Add("Melee DPS", list5);
                dictionary.Add("Other DPS", list6);
                for (int i = 0; i < list.Count; i++)
                {
                    CombatantData data = list[i];
                    if (allies.Contains(data))
                    {
                        switch (data.GetCombatantType())
                        {
                            case 1:
                                list3.Add(data);
                                break;

                            case 2:
                                list4.Add(data);
                                break;

                            case 3:
                                list5.Add(data);
                                break;

                            case 4:
                                list6.Add(data);
                                break;
                        }
                    }
                    else
                    {
                        List<CombatantData> list7 = new List<CombatantData> {
                            data
                        };
                        dictionary.Add(data.Name, list7);
                    }
                }
                goto Label_03B3;
            Label_02BD:
                list8 = new List<CombatantData>();
                for (int j = 0; j < list.Count; j++)
                {
                    CombatantData data2 = list[j];
                    if (!allies.Contains(data2))
                    {
                        list8.Add(data2);
                    }
                }
                dictionary.Add("Us", allies);
                dictionary.Add("Them", list8);
                goto Label_03B3;
            Label_0318:
                dictionary.Add("Us", allies);
                for (int k = 0; k < list.Count; k++)
                {
                    CombatantData data3 = list[k];
                    if (!allies.Contains(data3))
                    {
                        List<CombatantData> list9 = new List<CombatantData> {
                            data3
                        };
                        dictionary.Add(data3.Name, list9);
                    }
                }
                goto Label_03B3;
            Label_0372:
                num6 = 0;
                while (num6 < list.Count)
                {
                    List<CombatantData> list10 = new List<CombatantData> {
                        list[num6]
                    };
                    dictionary.Add(list[num6].Name, list10);
                    num6++;
                }
            Label_03B3:
                foreach (KeyValuePair<string, List<CombatantData>> pair in dictionary)
                {
                    int[,] numArray = new int[this.rowLabels.Count, this.dataPoints * 2];
                    foreach (CombatantData data4 in pair.Value)
                    {
                        foreach (KeyValuePair<string, AttackType> pair2 in data4.AllOut)
                        {
                            if (pair2.Key == ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)
                            {
                                continue;
                            }
                            if (pair2.Key == "Killing")
                            {
                                List<MasterSwing> list11 = new List<MasterSwing>(pair2.Value.Items);
                                for (int m = 0; m < list11.Count; m++)
                                {
                                    MasterSwing swing = list11[m];
                                    if (allies.Contains(data4) || !swing.Victim.Contains(" "))
                                    {
                                        TimeSpan span = (TimeSpan) (swing.Time - this.eD.StartTime);
                                        int num8 = (int) span.TotalSeconds;
                                        if (flag)
                                        {
                                            int num1 = numArray[0, num8];
                                            num1[0] |= 1;
                                        }
                                        else
                                        {
                                            int num22 = numArray[0, (num8 / num2) + 1];
                                            num22[0] |= 1;
                                        }
                                    }
                                }
                                continue;
                            }
                            if (ActGlobals.oFormSpellTimers.TimerLookups.ContainsKey(pair2.Key))
                            {
                                List<MasterSwing> list12 = new List<MasterSwing>(pair2.Value.Items);
                                try
                                {
                                    list12.Sort(new Comparison<MasterSwing>(MasterSwing.CompareTime));
                                }
                                catch (Exception exception)
                                {
                                    ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
                                }
                                List<DateTime> list13 = new List<DateTime>();
                                List<DateTime> list14 = new List<DateTime> {
                                    list12[0].Time
                                };
                                for (int n = 1; n < list12.Count; n++)
                                {
                                    MasterSwing swing2 = list12[n];
                                    MasterSwing swing3 = list12[n - 1];
                                    TimeSpan span6 = (TimeSpan) (swing2.Time - swing3.Time);
                                    int num10 = (int) span6.TotalSeconds;
                                    if (num10 > 12)
                                    {
                                        list13.Add(swing2.Time);
                                    }
                                }
                                for (int num11 = 0; num11 < list13.Count; num11++)
                                {
                                    TimeSpan span2 = list13[num11] - this.eD.StartTime;
                                    int num12 = (int) span2.TotalSeconds;
                                    if (flag)
                                    {
                                        int num23 = numArray[0, num12];
                                        num23[0] |= 4;
                                    }
                                    else
                                    {
                                        int num24 = numArray[0, (num12 / num2) + 1];
                                        num24[0] |= 4;
                                    }
                                }
                                for (int num13 = 0; num13 < list14.Count; num13++)
                                {
                                    TimeSpan span3 = list14[num13] - this.eD.StartTime;
                                    int num14 = (int) span3.TotalSeconds;
                                    if (flag)
                                    {
                                        int num25 = numArray[0, num14];
                                        num25[0] |= 8;
                                    }
                                    else
                                    {
                                        int num26 = numArray[0, (num14 / num2) + 1];
                                        num26[0] |= 8;
                                    }
                                }
                            }
                        }
                        foreach (KeyValuePair<string, AttackType> pair3 in data4.AllInc)
                        {
                            if (pair3.Key == "Killing")
                            {
                                List<MasterSwing> list15 = new List<MasterSwing>(pair3.Value.Items);
                                for (int num15 = 0; num15 < list15.Count; num15++)
                                {
                                    MasterSwing swing4 = list15[num15];
                                    TimeSpan span4 = (TimeSpan) (swing4.Time - this.eD.StartTime);
                                    int num16 = (int) span4.TotalSeconds;
                                    if (flag)
                                    {
                                        int num27 = numArray[0, num16];
                                        num27[0] |= 2;
                                    }
                                    else
                                    {
                                        int num28 = numArray[0, (num16 / num2) + 1];
                                        num28[0] |= 2;
                                    }
                                }
                            }
                        }
                    }
                    foreach (CombatantData data5 in pair.Value)
                    {
                        for (int num17 = 1; num17 < this.rowLabels.Count; num17++)
                        {
                            AttackType attackType = null;
                            text = this.rowLabels[num17];
                            if (text != null)
                            {
                                if (!(text == "Outgoing Damage"))
                                {
                                    if (text == "Incoming Damage")
                                    {
                                        goto Label_0869;
                                    }
                                    if (text == "Outgoing Healing")
                                    {
                                        goto Label_088D;
                                    }
                                }
                                else
                                {
                                    attackType = data5.GetAttackType(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, CombatantData.DamageTypeDataOutgoingDamage);
                                }
                            }
                            goto Label_08AF;
                        Label_0869:
                            attackType = data5.GetAttackType(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, CombatantData.DamageTypeDataIncomingDamage);
                            goto Label_08AF;
                        Label_088D:
                            attackType = data5.GetAttackType(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, CombatantData.DamageTypeDataOutgoingHealing);
                        Label_08AF:
                            if (attackType != null)
                            {
                                List<MasterSwing> list16 = new List<MasterSwing>(attackType.Items);
                                for (int num18 = 0; num18 < list16.Count; num18++)
                                {
                                    MasterSwing swing5 = list16[num18];
                                    if (swing5.Damage > 0)
                                    {
                                        TimeSpan span5 = (TimeSpan) (swing5.Time - this.eD.StartTime);
                                        int num19 = (int) span5.TotalSeconds;
                                        if (flag)
                                        {
                                            for (int num20 = num19; ((num19 >= 0) && (num20 < (num19 + num2))) && (num20 < totalSeconds); num20++)
                                            {
                                                int num29 = numArray[num17, num20];
                                                num29[0] += swing5.Damage;
                                            }
                                        }
                                        else
                                        {
                                            int num30 = numArray[num17, (num19 / num2) + 1];
                                            num30[0] += swing5.Damage;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    this.rowOwners.Add(pair.Key, numArray);
                    ListViewItem item = new ListViewItem(pair.Key) {
                        Checked = true
                    };
                    this.lvVisibleCombatants.Items.Add(item);
                }
                this.panel1.Focus();
                this.redrawEventRaised = true;
            }
            catch (Exception exception2)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception2, string.Empty);
            }
        }

        private void ShowWorking(string workingDesc, float numerator, float denominator)
        {
            float num = (numerator / denominator) * 100f;
            this.formTitle = string.Format("Timeline - {0} {1:0.00}%", workingDesc, num);
            Application.DoEvents();
        }

        private void tmrRedraw_Tick(object sender, EventArgs e)
        {
            if ((this.Text != this.formTitle) && !string.IsNullOrWhiteSpace(this.formTitle))
            {
                this.Text = this.formTitle;
                Application.DoEvents();
            }
            if (this.redrawEventRaised)
            {
                if (this.cbAllowRedraw.CheckState == CheckState.Checked)
                {
                    this.cbAllowRedraw.CheckState = CheckState.Indeterminate;
                    Application.DoEvents();
                    this.DrawTimeline();
                    this.cbAllowRedraw.CheckState = CheckState.Checked;
                }
                this.redrawEventRaised = false;
            }
            if (this.recalcEventRaised)
            {
                this.recalcEventRaised = false;
                this.ShowTimeline(this.eD);
            }
        }

        private enum CombatantEventType
        {
            Death = 2,
            Kill = 1,
            Spell = 4,
            SpellTick = 8
        }
    }
}

