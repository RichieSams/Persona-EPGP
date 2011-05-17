namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class FormEncounterVcr : Form
    {
        private Button btnBegining;
        private Button btnEnding;
        private Button btnFrameBack;
        private Button btnFrameForward;
        private CheckBox cbLinkToEnc;
        internal CheckBox cbOutlineTraces;
        internal CheckBox cbPersistantMobs;
        internal CheckBox cbRadialDisplay;
        private IContainer components;
        private DateTimePicker dateTimePicker1;
        private Graphics fbG;
        private Bitmap frontBuffer;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblStatus;
        internal NumericUpDown nudHistoryShadowSecs;
        internal NumericUpDown nudMaxOpacityThreshold;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private PictureBox pb1;
        private RadioButton rbFastForward;
        private RadioButton rbPause;
        private RadioButton rbPlay;
        private RadioButton rbRewind;
        private bool redraw;
        private uint tickCount;
        internal Timer timer1;
        private ToolTip toolTip1;
        private TrackBar trackBar1;
        private ToolTipGrid ttg = new ToolTipGrid();
        private TreeView tvDataTypes;
        private VcrEncounter veD;

        public FormEncounterVcr()
        {
            this.InitializeComponent();
            this.frontBuffer = new Bitmap(this.pb1.Width, this.pb1.Height);
            this.fbG = Graphics.FromImage(this.frontBuffer);
            this.fbG.SmoothingMode = SmoothingMode.AntiAlias;
            this.fbG.Clear(Color.White);
            this.pb1.Image = this.frontBuffer;
        }

        private void btnBegining_Click(object sender, EventArgs e)
        {
            this.rbPause.Checked = true;
            this.trackBar1.Value = 0;
            this.dateTimePicker1.Value = this.dateTimePicker1.MinDate;
            this.cbLinkToEnc.Checked = false;
        }

        private void btnEnding_Click(object sender, EventArgs e)
        {
            this.rbPause.Checked = true;
            this.trackBar1.Value = this.trackBar1.Maximum;
            this.dateTimePicker1.Value = this.dateTimePicker1.MaxDate;
        }

        private void btnFrameBack_Click(object sender, EventArgs e)
        {
            this.rbPause.Checked = true;
            if (this.trackBar1.Value > this.trackBar1.Minimum)
            {
                this.trackBar1.Value--;
                this.dateTimePicker1.Value -= TimeSpan.FromSeconds(1.0);
            }
            this.cbLinkToEnc.Checked = false;
        }

        private void btnFrameForward_Click(object sender, EventArgs e)
        {
            this.rbPause.Checked = true;
            if (this.trackBar1.Value < this.trackBar1.Maximum)
            {
                this.trackBar1.Value++;
                this.dateTimePicker1.Value += TimeSpan.FromSeconds(1.0);
            }
            this.cbLinkToEnc.Checked = false;
        }

        private void cbLinkToEnc_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbLinkToEnc.Checked)
            {
                this.rbPlay.Checked = true;
                this.trackBar1.Value = this.trackBar1.Maximum;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TimeSpan span = (TimeSpan) (this.dateTimePicker1.Value - this.veD.Encounter.StartTime);
                this.trackBar1.Value = (int) span.TotalSeconds;
                this.redraw = true;
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

        private void DrawVcr()
        {
            try
            {
                int height;
                Bitmap image = new Bitmap(this.pb1.Width, this.pb1.Height);
                Graphics graphics = Graphics.FromImage(image);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.Clear(Color.White);
                this.ttg.Items.Clear();
                DateTime time = this.veD.Encounter.StartTime.AddSeconds((double) this.trackBar1.Value);
                Rectangle rect = new Rectangle(0, 0, this.pb1.Width, this.pb1.Height);
                if (rect.Height < rect.Width)
                {
                    height = rect.Height;
                }
                else
                {
                    height = rect.Width;
                }
                int num2 = (int) (height * 0.04f);
                int num3 = (int) (height * 0.08f);
                Font font = new Font("Arial", 8f, FontStyle.Bold);
                SolidBrush brush = new SolidBrush(Color.Black);
                List<VcrCombatant> list = new List<VcrCombatant>();
                List<VcrCombatant> list2 = new List<VcrCombatant>();
                List<VcrCombatant> list3 = new List<VcrCombatant>();
                List<VcrCombatant> list4 = new List<VcrCombatant>();
                List<VcrCombatant> list5 = new List<VcrCombatant>();
                List<VcrCombatant> list6 = new List<VcrCombatant>(this.veD.Items);
                if (!this.cbPersistantMobs.Checked)
                {
                    for (int j = list6.Count - 1; j >= 0; j--)
                    {
                        bool active = list6[j].GetActive(time, 15);
                        foreach (VcrCombatant combatant in list6)
                        {
                            if (active)
                            {
                                break;
                            }
                            if (combatant.GetActiveTargets(time, 15).Contains(list6[j].Name))
                            {
                                active = true;
                            }
                        }
                        if (!active && !this.veD.Encounter.GetAllies().Contains(new CombatantData(list6[j].Name, null)))
                        {
                            list6.RemoveAt(j);
                        }
                    }
                }
                int count = this.veD.Encounter.GetAllies().Count;
                int num6 = list6.Count - count;
                float num7 = 360f / ((float) num6);
                if (this.cbRadialDisplay.Checked)
                {
                    for (int k = 0; k < list6.Count; k++)
                    {
                        if (list6[k].Type == 0)
                        {
                            list.Add(list6[k]);
                        }
                        else
                        {
                            list3.Add(list6[k]);
                        }
                    }
                    StringFormat format = new StringFormat();
                    for (int m = 0; m < list.Count; m++)
                    {
                        list[m].Location = DrawingHelpers.GetRadialLocation(num7 * m, height * 0.2f, (float) DrawingHelpers.RectCx(rect), (float) DrawingHelpers.RectCy(rect));
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Near;
                        graphics.DrawString(list[m].Name, font, brush, list[m].Location.X, list[m].Location.Y + (((float) num3) / 1.75f), format);
                    }
                    float num10 = 1f;
                    float num11 = 360f / ((float) list3.Count);
                    for (int n = 0; n < list3.Count; n++)
                    {
                        float angle = (num11 * n) + num10;
                        list3[n].Location = DrawingHelpers.GetRadialLocation(angle, height * 0.4f, (float) DrawingHelpers.RectCx(rect), (float) DrawingHelpers.RectCy(rect));
                        if ((angle > 0f) && (angle < 180f))
                        {
                            format.LineAlignment = StringAlignment.Center;
                            format.Alignment = StringAlignment.Near;
                            PointF tf = DrawingHelpers.GetRadialLocation(angle, ((float) num2) / 1.5f, list3[n].Location);
                            graphics.TranslateTransform(tf.X, tf.Y);
                            angle += (90f - angle) * 0.8f;
                            graphics.RotateTransform(angle - 90f);
                            graphics.DrawString(list3[n].Name, font, brush, 0f, 0f, format);
                        }
                        else
                        {
                            format = new StringFormat {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Far
                            };
                            PointF tf2 = DrawingHelpers.GetRadialLocation(angle, ((float) num2) / 1.5f, list3[n].Location);
                            graphics.TranslateTransform(tf2.X, tf2.Y);
                            angle += (270f - angle) * 0.8f;
                            graphics.RotateTransform(angle + 90f);
                            graphics.DrawString(list3[n].Name, font, brush, 0f, 0f, format);
                        }
                        graphics.ResetTransform();
                    }
                }
                else
                {
                    for (int num14 = 0; num14 < list6.Count; num14++)
                    {
                        switch (list6[num14].Type)
                        {
                            case 0:
                                list.Add(list6[num14]);
                                break;

                            case 1:
                                list2.Add(list6[num14]);
                                break;

                            case 2:
                                list3.Add(list6[num14]);
                                break;

                            case 3:
                                list5.Add(list6[num14]);
                                break;

                            case 4:
                                list4.Add(list6[num14]);
                                break;
                        }
                    }
                    StringFormat format2 = new StringFormat {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Near
                    };
                    for (int num15 = 0; num15 < list.Count; num15++)
                    {
                        list[num15].Location = DrawingHelpers.GetRadialLocation(num7 * num15, height * 0.2f, (float) DrawingHelpers.RectCx(rect), (float) (DrawingHelpers.RectCy(rect) + (height / 5)));
                        graphics.DrawString(list[num15].Name, font, brush, list[num15].Location.X, list[num15].Location.Y + (((float) num3) / 1.5f), format2);
                    }
                    format2.LineAlignment = StringAlignment.Center;
                    format2.Alignment = StringAlignment.Near;
                    float num16 = 175f;
                    float num17 = 10f / ((float) list2.Count);
                    for (int num18 = 0; num18 < list2.Count; num18++)
                    {
                        float num19 = ((num17 * num18) + num16) + (num17 / 2f);
                        list2[num18].Location = DrawingHelpers.GetRadialLocation(num19, height * 1.3f, (float) DrawingHelpers.RectCx(rect), 0f - (height * 1.1f));
                        PointF tf3 = DrawingHelpers.GetRadialLocation(135f, ((float) num2) / 1.5f, list2[num18].Location);
                        graphics.TranslateTransform(tf3.X, tf3.Y);
                        graphics.RotateTransform(45f);
                        graphics.DrawString(list2[num18].Name, font, brush, 0f, 0f, format2);
                        graphics.ResetTransform();
                    }
                    num16 = 160f;
                    num17 = 40f / ((float) list3.Count);
                    for (int num20 = 0; num20 < list3.Count; num20++)
                    {
                        float num21 = ((num17 * num20) + num16) + (num17 / 2f);
                        list3[num20].Location = DrawingHelpers.GetRadialLocation(num21, height * 1.2f, (float) DrawingHelpers.RectCx(rect), 0f - (height * 1.1f));
                        PointF tf4 = DrawingHelpers.GetRadialLocation(135f, ((float) num2) / 1.5f, list3[num20].Location);
                        graphics.TranslateTransform(tf4.X, tf4.Y);
                        graphics.RotateTransform(45f);
                        graphics.DrawString(list3[num20].Name, font, brush, 0f, 0f, format2);
                        graphics.ResetTransform();
                    }
                    num16 = 55f;
                    num17 = 70f / ((float) list4.Count);
                    for (int num22 = 0; num22 < list4.Count; num22++)
                    {
                        float num23 = ((num17 * num22) + num16) + (num17 / 2f);
                        if (list4.Count == 1)
                        {
                            num23 = (num17 / 2f) + num16;
                        }
                        list4[num22].Location = DrawingHelpers.GetRadialLocation(num23, height * 0.75f, (float) (DrawingHelpers.RectCx(rect) - (rect.Width / 7)), (float) DrawingHelpers.RectCy(rect));
                        graphics.DrawString(list4[num22].Name, font, brush, list4[num22].Location.X + (((float) num2) / 1.75f), list4[num22].Location.Y, format2);
                    }
                    list5.Reverse();
                    num16 = 235f;
                    num17 = 70f / ((float) list5.Count);
                    for (int num24 = 0; num24 < list5.Count; num24++)
                    {
                        float num25 = ((num17 * num24) + num16) + (num17 / 2f);
                        if (list5.Count == 1)
                        {
                            num25 = (num17 / 2f) + num16;
                        }
                        list5[num24].Location = DrawingHelpers.GetRadialLocation(num25, height * 0.8f, (float) (DrawingHelpers.RectCx(rect) + (rect.Width / 7)), (float) DrawingHelpers.RectCy(rect));
                        graphics.DrawString(list5[num24].Name, font, brush, list5[num24].Location.X + (((float) num2) / 1.75f), list5[num24].Location.Y, format2);
                    }
                }
                Dictionary<string, Color> dictionary = new Dictionary<string, Color>();
                foreach (TreeNode node in this.tvDataTypes.Nodes)
                {
                    if (node.Checked)
                    {
                        dictionary.Add(node.Text, node.ForeColor);
                    }
                }
                for (int i = 0; i < list6.Count; i++)
                {
                    int num27;
                    if (this.veD.Encounter.GetAllies().Contains(new CombatantData(list6[i].Name, null)))
                    {
                        num27 = num2;
                    }
                    else
                    {
                        num27 = num3;
                    }
                    List<string> activeTargets = list6[i].GetActiveTargets(time, (int) this.nudHistoryShadowSecs.Value);
                    if (activeTargets.Count > 0)
                    {
                        foreach (string str in activeTargets)
                        {
                            foreach (KeyValuePair<string, Color> pair in dictionary)
                            {
                                int num28 = list6[i].GetCompiledAmount(time, (int) this.nudHistoryShadowSecs.Value, str, pair.Key);
                                if (num28 > 0)
                                {
                                    this.ttg.Items.Add(new ToolTipRect(-1, string.Format("{3} {0}  {1} -> {2}", new object[] { num28, list6[i].Name, str, pair.Key }), list6[i].Location.X - (num27 / 2), list6[i].Location.Y - (num27 / 2), (float) num27, (float) num27));
                                    PointF[] points = DrawingHelpers.GetArrowPolygon(this.veD.GetCombatant(str).Location, (float) (num27 / 2), list6[i].Location);
                                    int alpha = Convert.ToInt32((decimal) ((num28 / this.nudMaxOpacityThreshold.Value) * 200M));
                                    if (alpha > 200)
                                    {
                                        alpha = 200;
                                    }
                                    if (alpha < 10)
                                    {
                                        alpha = 10;
                                    }
                                    graphics.FillPolygon(new SolidBrush(Color.FromArgb(alpha, pair.Value)), points);
                                    if (this.cbOutlineTraces.Checked)
                                    {
                                        graphics.DrawPolygon(new Pen(Color.FromArgb(alpha + 0x37, pair.Value), 0.1f), points);
                                    }
                                }
                            }
                        }
                    }
                    float health = list6[i].GetHealth(time);
                    this.ttg.Items.Add(new ToolTipRect(-1, string.Format("Current Health: {0:0.00%} of {1}", health, list6[i].MaxHealth), list6[i].Location.X - (num27 / 2), list6[i].Location.Y - (num27 / 2), (float) num27, (float) num27));
                    graphics.FillPie(new SolidBrush(Color.FromArgb(50, Color.Black)), list6[i].Location.X - (num27 / 2), list6[i].Location.Y - (num27 / 2), (float) num27, (float) num27, -90f, 360f * health);
                    if (list6[i].Instances == 1)
                    {
                        Pen pen;
                        if (list6[i].GetInstancesAlive(time) == 1)
                        {
                            if (list6[i].GetActive(time, 15))
                            {
                                pen = new Pen(Color.Blue, 2f);
                            }
                            else
                            {
                                pen = new Pen(Color.Gray, 2f);
                            }
                        }
                        else
                        {
                            pen = new Pen(Color.Red, 2f);
                        }
                        graphics.DrawEllipse(pen, list6[i].Location.X - (num27 / 2), list6[i].Location.Y - (num27 / 2), (float) num27, (float) num27);
                    }
                    else
                    {
                        int instancesAlive = list6[i].GetInstancesAlive(time);
                        if (list6[i].Instances < 5)
                        {
                            float num33 = 360f / ((float) list6[i].Instances);
                            for (int num34 = 0; num34 < list6[i].Instances; num34++)
                            {
                                Pen pen2;
                                PointF tf6 = DrawingHelpers.GetRadialLocation(num33 * num34, (float) (num27 / 4), list6[i].Location);
                                if (num34 < instancesAlive)
                                {
                                    if (list6[i].GetActive(time, 15))
                                    {
                                        pen2 = new Pen(Color.Blue, 1.5f);
                                    }
                                    else
                                    {
                                        pen2 = new Pen(Color.Gray, 1.5f);
                                    }
                                }
                                else
                                {
                                    pen2 = new Pen(Color.Red, 1.5f);
                                }
                                graphics.DrawEllipse(pen2, tf6.X - (num27 / 4), tf6.Y - (num27 / 4), (float) (num27 / 2), (float) (num27 / 2));
                            }
                        }
                        if (list6[i].Instances > 4)
                        {
                            float num35 = 360f / ((float) list6[i].Instances);
                            for (int num36 = 0; num36 < list6[i].Instances; num36++)
                            {
                                Pen pen3;
                                PointF tf7 = DrawingHelpers.GetRadialLocation(num35 * num36, ((float) num27) / 2.25f, list6[i].Location);
                                if (num36 < instancesAlive)
                                {
                                    if (list6[i].GetActive(time, 15))
                                    {
                                        pen3 = new Pen(Color.Blue, 1.5f);
                                    }
                                    else
                                    {
                                        pen3 = new Pen(Color.Gray, 1.5f);
                                    }
                                }
                                else
                                {
                                    pen3 = new Pen(Color.Red, 1.5f);
                                }
                                graphics.DrawEllipse(pen3, tf7.X - (num27 / 8), tf7.Y - (num27 / 8), (float) (num27 / 4), (float) (num27 / 4));
                            }
                        }
                    }
                    float damageTaken = list6[i].GetDamageTaken(time);
                    this.ttg.Items.Add(new ToolTipRect(-1, string.Format("Damage taken: {0:0.00%} of {1}", damageTaken, list6[i].DamageTaken), list6[i].Location.X - (num27 / 2), list6[i].Location.Y + (num27 / 2), (float) num27, (float) (num27 / 4)));
                    graphics.FillRectangle(new SolidBrush(Color.Red), list6[i].Location.X - (num27 / 2), list6[i].Location.Y + (num27 / 2), num27 * damageTaken, (float) (num27 / 8));
                    float healsTaken = list6[i].GetHealsTaken(time);
                    this.ttg.Items.Add(new ToolTipRect(-1, string.Format("Heals taken: {0:0.00%} of {1}", healsTaken, list6[i].HealsTaken), list6[i].Location.X - (num27 / 2), list6[i].Location.Y + (num27 / 2), (float) num27, (float) (num27 / 4)));
                    graphics.FillRectangle(new SolidBrush(Color.CornflowerBlue), list6[i].Location.X - (num27 / 2), (list6[i].Location.Y + (num27 / 2)) + (num27 / 8), num27 * healsTaken, (float) (num27 / 8));
                }
                graphics.Flush();
                this.fbG.DrawImageUnscaled(image, 0, 0);
                this.pb1.Image = this.frontBuffer;
                image.Dispose();
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
            xml.WriteStartElement("", "ControlText", "");
            xml.WriteAttributeString("Form", "FormEncounterVcr");
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

        private void Form12_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.rbPause.Checked = true;
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
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormEncounterVcr));
            this.pb1 = new PictureBox();
            this.btnFrameBack = new Button();
            this.btnFrameForward = new Button();
            this.btnBegining = new Button();
            this.btnEnding = new Button();
            this.dateTimePicker1 = new DateTimePicker();
            this.rbPlay = new RadioButton();
            this.rbFastForward = new RadioButton();
            this.rbRewind = new RadioButton();
            this.rbPause = new RadioButton();
            this.trackBar1 = new TrackBar();
            this.nudHistoryShadowSecs = new NumericUpDown();
            this.label1 = new Label();
            this.label3 = new Label();
            this.nudMaxOpacityThreshold = new NumericUpDown();
            this.timer1 = new Timer(this.components);
            this.cbOutlineTraces = new CheckBox();
            this.lblStatus = new Label();
            this.panel1 = new Panel();
            this.cbLinkToEnc = new CheckBox();
            this.cbRadialDisplay = new CheckBox();
            this.label2 = new Label();
            this.cbPersistantMobs = new CheckBox();
            this.toolTip1 = new ToolTip(this.components);
            this.panel2 = new Panel();
            this.panel3 = new Panel();
            this.tvDataTypes = new TreeView();
            ((ISupportInitialize) this.pb1).BeginInit();
            this.trackBar1.BeginInit();
            this.nudHistoryShadowSecs.BeginInit();
            this.nudMaxOpacityThreshold.BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            base.SuspendLayout();
            this.pb1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.pb1.BorderStyle = BorderStyle.FixedSingle;
            this.pb1.Location = new Point(12, 12);
            this.pb1.Name = "pb1";
            this.pb1.Size = new Size(0x43b, 0x209);
            this.pb1.TabIndex = 0;
            this.pb1.TabStop = false;
            this.pb1.MouseMove += new MouseEventHandler(this.pb1_MouseMove);
            this.pb1.Resize += new EventHandler(this.pb1_Resize);
            this.btnFrameBack.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnFrameBack.BackColor = SystemColors.ControlDark;
            this.btnFrameBack.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnFrameBack.ForeColor = SystemColors.ControlLight;
            this.btnFrameBack.Location = new Point(0x4f, 0x2d);
            this.btnFrameBack.Name = "btnFrameBack";
            this.btnFrameBack.Size = new Size(0x20, 0x18);
            this.btnFrameBack.TabIndex = 3;
            this.btnFrameBack.Text = "< ||";
            this.btnFrameBack.UseVisualStyleBackColor = false;
            this.btnFrameBack.Click += new EventHandler(this.btnFrameBack_Click);
            this.btnFrameForward.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnFrameForward.BackColor = SystemColors.ControlDark;
            this.btnFrameForward.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnFrameForward.ForeColor = SystemColors.ControlLight;
            this.btnFrameForward.Location = new Point(0xc1, 0x2d);
            this.btnFrameForward.Name = "btnFrameForward";
            this.btnFrameForward.Size = new Size(0x20, 0x18);
            this.btnFrameForward.TabIndex = 6;
            this.btnFrameForward.Text = "|| >";
            this.btnFrameForward.UseVisualStyleBackColor = false;
            this.btnFrameForward.Click += new EventHandler(this.btnFrameForward_Click);
            this.btnBegining.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnBegining.BackColor = SystemColors.ControlDark;
            this.btnBegining.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnBegining.ForeColor = SystemColors.ControlLight;
            this.btnBegining.Location = new Point(3, 0x2d);
            this.btnBegining.Name = "btnBegining";
            this.btnBegining.Size = new Size(0x20, 0x18);
            this.btnBegining.TabIndex = 1;
            this.btnBegining.Text = "|<";
            this.btnBegining.UseVisualStyleBackColor = false;
            this.btnBegining.Click += new EventHandler(this.btnBegining_Click);
            this.btnEnding.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnEnding.BackColor = SystemColors.ControlDark;
            this.btnEnding.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnEnding.ForeColor = SystemColors.ControlLight;
            this.btnEnding.Location = new Point(0x10d, 0x2d);
            this.btnEnding.Name = "btnEnding";
            this.btnEnding.Size = new Size(0x20, 0x18);
            this.btnEnding.TabIndex = 8;
            this.btnEnding.Text = ">|";
            this.btnEnding.UseVisualStyleBackColor = false;
            this.btnEnding.Click += new EventHandler(this.btnEnding_Click);
            this.dateTimePicker1.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.dateTimePicker1.Format = DateTimePickerFormat.Time;
            this.dateTimePicker1.Location = new Point(0x21d, 0x30);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new Size(0x61, 20);
            this.dateTimePicker1.TabIndex = 9;
            this.dateTimePicker1.ValueChanged += new EventHandler(this.dateTimePicker1_ValueChanged);
            this.rbPlay.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.rbPlay.Appearance = Appearance.Button;
            this.rbPlay.BackColor = SystemColors.ControlDarkDark;
            this.rbPlay.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.rbPlay.ForeColor = SystemColors.ControlLight;
            this.rbPlay.Location = new Point(0x9b, 0x2d);
            this.rbPlay.Name = "rbPlay";
            this.rbPlay.Size = new Size(0x20, 0x18);
            this.rbPlay.TabIndex = 5;
            this.rbPlay.Text = ">";
            this.rbPlay.TextAlign = ContentAlignment.MiddleCenter;
            this.rbPlay.UseVisualStyleBackColor = false;
            this.rbFastForward.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.rbFastForward.Appearance = Appearance.Button;
            this.rbFastForward.BackColor = SystemColors.ControlDark;
            this.rbFastForward.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.rbFastForward.ForeColor = SystemColors.ControlLight;
            this.rbFastForward.Location = new Point(0xe7, 0x2d);
            this.rbFastForward.Name = "rbFastForward";
            this.rbFastForward.Size = new Size(0x20, 0x18);
            this.rbFastForward.TabIndex = 7;
            this.rbFastForward.Text = ">>";
            this.rbFastForward.TextAlign = ContentAlignment.MiddleCenter;
            this.rbFastForward.UseVisualStyleBackColor = false;
            this.rbRewind.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.rbRewind.Appearance = Appearance.Button;
            this.rbRewind.BackColor = SystemColors.ControlDark;
            this.rbRewind.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.rbRewind.ForeColor = SystemColors.ControlLight;
            this.rbRewind.Location = new Point(0x29, 0x2d);
            this.rbRewind.Name = "rbRewind";
            this.rbRewind.Size = new Size(0x20, 0x18);
            this.rbRewind.TabIndex = 2;
            this.rbRewind.Text = "<<";
            this.rbRewind.TextAlign = ContentAlignment.MiddleCenter;
            this.rbRewind.UseVisualStyleBackColor = false;
            this.rbPause.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.rbPause.Appearance = Appearance.Button;
            this.rbPause.BackColor = SystemColors.ControlDarkDark;
            this.rbPause.Checked = true;
            this.rbPause.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.rbPause.ForeColor = SystemColors.ControlLight;
            this.rbPause.Location = new Point(0x75, 0x2d);
            this.rbPause.Name = "rbPause";
            this.rbPause.Size = new Size(0x20, 0x18);
            this.rbPause.TabIndex = 4;
            this.rbPause.TabStop = true;
            this.rbPause.Text = "||";
            this.rbPause.TextAlign = ContentAlignment.MiddleCenter;
            this.rbPause.UseVisualStyleBackColor = false;
            this.trackBar1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.trackBar1.LargeChange = 6;
            this.trackBar1.Location = new Point(-1, -1);
            this.trackBar1.Maximum = 360;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new Size(0x283, 0x2d);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.TickFrequency = 6;
            this.trackBar1.Scroll += new EventHandler(this.trackBar1_Scroll);
            this.nudHistoryShadowSecs.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.nudHistoryShadowSecs.Location = new Point(0xb1, 0x35);
            int[] bits = new int[4];
            bits[0] = 30;
            this.nudHistoryShadowSecs.Maximum = new decimal(bits);
            this.nudHistoryShadowSecs.Name = "nudHistoryShadowSecs";
            this.nudHistoryShadowSecs.Size = new Size(0x2b, 0x12);
            this.nudHistoryShadowSecs.TabIndex = 11;
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.nudHistoryShadowSecs.Value = new decimal(numArray2);
            this.nudHistoryShadowSecs.ValueChanged += new EventHandler(this.redrawEvents);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(0x84, 0x39);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x27, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "Shadow";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label3.Location = new Point(3, 0x39);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x43, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "Dmg Threshold";
            this.nudMaxOpacityThreshold.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            int[] numArray3 = new int[4];
            numArray3[0] = 500;
            this.nudMaxOpacityThreshold.Increment = new decimal(numArray3);
            this.nudMaxOpacityThreshold.Location = new Point(0x4c, 0x35);
            int[] numArray4 = new int[4];
            numArray4[0] = 0x7d00;
            this.nudMaxOpacityThreshold.Maximum = new decimal(numArray4);
            int[] numArray5 = new int[4];
            numArray5[0] = 100;
            this.nudMaxOpacityThreshold.Minimum = new decimal(numArray5);
            this.nudMaxOpacityThreshold.Name = "nudMaxOpacityThreshold";
            this.nudMaxOpacityThreshold.Size = new Size(50, 0x12);
            this.nudMaxOpacityThreshold.TabIndex = 12;
            int[] numArray6 = new int[4];
            numArray6[0] = 0x1388;
            this.nudMaxOpacityThreshold.Value = new decimal(numArray6);
            this.nudMaxOpacityThreshold.ValueChanged += new EventHandler(this.redrawEvents);
            this.timer1.Interval = 250;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.cbOutlineTraces.AutoSize = true;
            this.cbOutlineTraces.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cbOutlineTraces.Location = new Point(6, 0x13);
            this.cbOutlineTraces.Name = "cbOutlineTraces";
            this.cbOutlineTraces.Size = new Size(0x6f, 0x10);
            this.cbOutlineTraces.TabIndex = 10;
            this.cbOutlineTraces.Text = "Outline trace pointers";
            this.cbOutlineTraces.UseVisualStyleBackColor = true;
            this.cbOutlineTraces.CheckedChanged += new EventHandler(this.redrawEvents);
            this.lblStatus.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.lblStatus.BackColor = SystemColors.Window;
            this.lblStatus.BorderStyle = BorderStyle.Fixed3D;
            this.lblStatus.Location = new Point(0x1b3, 0x2e);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(100, 0x18);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Paused";
            this.lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbLinkToEnc);
            this.panel1.Controls.Add(this.cbRadialDisplay);
            this.panel1.Controls.Add(this.cbOutlineTraces);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.nudHistoryShadowSecs);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.nudMaxOpacityThreshold);
            this.panel1.Controls.Add(this.cbPersistantMobs);
            this.panel1.Location = new Point(860, 0x214);
            this.panel1.Margin = new Padding(0, 3, 3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xeb, 0x4a);
            this.panel1.TabIndex = 14;
            this.cbLinkToEnc.Appearance = Appearance.Button;
            this.cbLinkToEnc.FlatStyle = FlatStyle.Popup;
            this.cbLinkToEnc.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cbLinkToEnc.Location = new Point(0x9e, 3);
            this.cbLinkToEnc.Name = "cbLinkToEnc";
            this.cbLinkToEnc.Size = new Size(70, 0x20);
            this.cbLinkToEnc.TabIndex = 13;
            this.cbLinkToEnc.Text = "Realtime Mode";
            this.cbLinkToEnc.TextAlign = ContentAlignment.MiddleCenter;
            this.cbLinkToEnc.UseVisualStyleBackColor = true;
            this.cbLinkToEnc.CheckedChanged += new EventHandler(this.cbLinkToEnc_CheckedChanged);
            this.cbRadialDisplay.AutoSize = true;
            this.cbRadialDisplay.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cbRadialDisplay.Location = new Point(6, 3);
            this.cbRadialDisplay.Name = "cbRadialDisplay";
            this.cbRadialDisplay.Size = new Size(0x86, 0x10);
            this.cbRadialDisplay.TabIndex = 10;
            this.cbRadialDisplay.Text = "Show Combatants in a ring";
            this.cbRadialDisplay.UseVisualStyleBackColor = true;
            this.cbRadialDisplay.CheckedChanged += new EventHandler(this.redrawEvents);
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label2.Location = new Point(220, 0x38);
            this.label2.Name = "label2";
            this.label2.Size = new Size(10, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "s";
            this.cbPersistantMobs.AutoSize = true;
            this.cbPersistantMobs.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cbPersistantMobs.Location = new Point(6, 0x23);
            this.cbPersistantMobs.Name = "cbPersistantMobs";
            this.cbPersistantMobs.Size = new Size(0x71, 0x10);
            this.cbPersistantMobs.TabIndex = 10;
            this.cbPersistantMobs.Text = "Inactive mobs persist";
            this.cbPersistantMobs.UseVisualStyleBackColor = true;
            this.cbPersistantMobs.CheckedChanged += new EventHandler(this.redrawEvents);
            this.toolTip1.AutomaticDelay = 800;
            this.toolTip1.AutoPopDelay = 0x186a0;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 160;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
            this.panel2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.panel2.BorderStyle = BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.trackBar1);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.rbPause);
            this.panel2.Controls.Add(this.lblStatus);
            this.panel2.Controls.Add(this.rbRewind);
            this.panel2.Controls.Add(this.btnBegining);
            this.panel2.Controls.Add(this.rbFastForward);
            this.panel2.Controls.Add(this.btnFrameBack);
            this.panel2.Controls.Add(this.rbPlay);
            this.panel2.Controls.Add(this.btnFrameForward);
            this.panel2.Controls.Add(this.btnEnding);
            this.panel2.Location = new Point(12, 0x214);
            this.panel2.Margin = new Padding(3, 3, 0, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x283, 0x4a);
            this.panel2.TabIndex = 15;
            this.panel3.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.panel3.BorderStyle = BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.tvDataTypes);
            this.panel3.Location = new Point(0x28e, 0x214);
            this.panel3.Margin = new Padding(0, 3, 0, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0xcf, 0x4a);
            this.panel3.TabIndex = 0x10;
            this.tvDataTypes.CheckBoxes = true;
            this.tvDataTypes.Dock = DockStyle.Fill;
            this.tvDataTypes.FullRowSelect = true;
            this.tvDataTypes.Location = new Point(0, 0);
            this.tvDataTypes.Name = "tvDataTypes";
            this.tvDataTypes.ShowLines = false;
            this.tvDataTypes.ShowPlusMinus = false;
            this.tvDataTypes.ShowRootLines = false;
            this.tvDataTypes.Size = new Size(0xcd, 0x48);
            this.tvDataTypes.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x452, 0x265);
            base.Controls.Add(this.panel3);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.pb1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            this.MinimumSize = new Size(0x30a, 0x282);
            base.Name = "FormEncounterVcr";
            this.Text = "Encounter VCR";
            base.FormClosing += new FormClosingEventHandler(this.Form12_FormClosing);
            ((ISupportInitialize) this.pb1).EndInit();
            this.trackBar1.EndInit();
            this.nudHistoryShadowSecs.EndInit();
            this.nudMaxOpacityThreshold.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
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

        private void pb1_Resize(object sender, EventArgs e)
        {
            try
            {
                if (base.WindowState != FormWindowState.Minimized)
                {
                    this.frontBuffer = new Bitmap(this.pb1.Width, this.pb1.Height);
                    this.fbG = Graphics.FromImage(this.frontBuffer);
                    this.fbG.SmoothingMode = SmoothingMode.AntiAlias;
                    this.fbG.Clear(Color.White);
                    this.redraw = true;
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }

        private void redrawEvents(object sender, EventArgs e)
        {
            this.redraw = true;
        }

        public void ShowVcr(EncounterData Encounter, bool Linked)
        {
            try
            {
                if (Encounter.StartTime != DateTime.MaxValue)
                {
                    if (this.tvDataTypes.Nodes.Count == 0)
                    {
                        int index = 0;
                        Color[] colorArray = new Color[CombatantData.OutgoingDamageTypeDataObjects.Count];
                        foreach (KeyValuePair<string, CombatantData.DamageTypeDef> pair in CombatantData.OutgoingDamageTypeDataObjects)
                        {
                            TreeNode node = this.tvDataTypes.Nodes.Add(pair.Key);
                            colorArray[index] = pair.Value.TypeColor;
                            node.ForeColor = colorArray[index];
                            node.Checked = true;
                            index++;
                        }
                    }
                    this.veD = new VcrEncounter(Encounter);
                    List<CombatantData> list = new List<CombatantData>(this.veD.Encounter.Items.Values);
                    list.Sort(new Comparison<CombatantData>(CombatantData.CompareDamageTakenTime));
                    list.Reverse();
                    for (int i = 0; i < Encounter.Items.Count; i++)
                    {
                        VcrCombatant item = new VcrCombatant(list[i]) {
                            PositionMod = i,
                            PositionMod = i + (list[i].GetCombatantType() * Encounter.Items.Count)
                        };
                        this.veD.Items.Add(item);
                    }
                    this.veD.Items.Sort();
                    this.trackBar1.Value = 0;
                    this.trackBar1.Maximum = (int) Encounter.Duration.TotalSeconds;
                    this.dateTimePicker1.MinDate = new DateTime(0x76c, 1, 1);
                    this.dateTimePicker1.MaxDate = new DateTime(0x834, 1, 1);
                    this.dateTimePicker1.Value = Encounter.StartTime;
                    this.dateTimePicker1.MinDate = Encounter.StartTime;
                    this.dateTimePicker1.MaxDate = Encounter.EndTime;
                    if (Linked)
                    {
                        this.trackBar1.Value = this.trackBar1.Maximum;
                        this.dateTimePicker1.Value = this.dateTimePicker1.MaxDate;
                        this.rbPlay.Checked = true;
                    }
                    else
                    {
                        this.rbPause.Checked = true;
                        base.Show();
                    }
                    this.redraw = true;
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.tickCount += 1;
                if (this.rbPause.Checked)
                {
                    this.lblStatus.Text = string.Format("Paused @ T+{0}", this.trackBar1.Value);
                    this.cbLinkToEnc.Checked = false;
                }
                if (this.rbRewind.Checked && (this.trackBar1.Value > this.trackBar1.Minimum))
                {
                    this.trackBar1.Value--;
                    this.dateTimePicker1.Value -= TimeSpan.FromSeconds(1.0);
                    this.redraw = true;
                    this.lblStatus.Text = string.Format("Rewind @ T+{0}", this.trackBar1.Value);
                    this.cbLinkToEnc.Checked = false;
                }
                if (this.rbPlay.Checked && ((this.tickCount % 4) == 0))
                {
                    if (this.cbLinkToEnc.Checked)
                    {
                        this.ShowVcr(ActGlobals.oFormActMain.ActiveZone.ActiveEncounter, true);
                        if (this.nudHistoryShadowSecs.Value < 1M)
                        {
                            this.nudHistoryShadowSecs.Value = 1M;
                        }
                        this.lblStatus.Text = string.Format("Linked @ T+{0}", this.trackBar1.Value);
                    }
                    if (this.trackBar1.Value < this.trackBar1.Maximum)
                    {
                        this.trackBar1.Value++;
                        this.dateTimePicker1.Value += TimeSpan.FromSeconds(1.0);
                        this.lblStatus.Text = string.Format("Play @ T+{0}", this.trackBar1.Value);
                    }
                    this.redraw = true;
                }
                if (this.rbFastForward.Checked & (this.trackBar1.Value < this.trackBar1.Maximum))
                {
                    this.trackBar1.Value++;
                    this.dateTimePicker1.Value += TimeSpan.FromSeconds(1.0);
                    this.redraw = true;
                    this.lblStatus.Text = string.Format("4x Play @ T+{0}", this.trackBar1.Value);
                    this.cbLinkToEnc.Checked = false;
                }
                if (this.redraw && base.Visible)
                {
                    this.redraw = false;
                    this.DrawVcr();
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                this.dateTimePicker1.Value = this.veD.Encounter.StartTime + TimeSpan.FromSeconds((double) this.trackBar1.Value);
                this.redraw = true;
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }
    }
}

