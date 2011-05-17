namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Media;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Web;
    using System.Windows.Forms;
    using System.Xml;

    public class FormSpellTimers : Form
    {
        private ToolStripMenuItem browseForWAVToolStripMenuItem;
        private Button btnAddEdit;
        private Button btnClearSearch;
        private Button btnPlayStartWav;
        private Button btnPlayWarningWav;
        private Button btnRemove;
        private Button btnStartWavBrowse;
        private Button btnWarningWavBrowse;
        private Button btnWhiteListAdd;
        private Button btnWhiteListRemove;
        private CheckBox cbAbsoluteTiming;
        internal CheckBox cbAllowMod;
        internal CheckBox cbbAllowPanel2;
        private CheckBox cbOnlyMasterTicks;
        private CheckBox cbPanel1;
        private CheckBox cbPanel2;
        private CheckBox cbRestrictToCategory;
        private CheckBox cbRestrictToMe;
        internal CheckBox cbShowRadial;
        internal CheckBox cbTimersClickThrough;
        internal CheckBox cbTimersClickThrough2;
        internal CheckBox cbTimersOnTop;
        internal CheckBox cbTimersOnTop2;
        private CheckBox checkBox1;
        private ContextMenuStrip cmWAV1;
        private ContextMenuStrip cmWAV2;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem copyAsSharableXMLToolStripMenuItem;
        private ToolStripMenuItem copyAsSharableXMLToolStripMenuItem2;
        private ToolStripMenuItem copyAsSharableXMLToolStripMenuItem3;
        private ToolStripMenuItem copyAsSharableXMLToolStripMenuItem4;
        private ToolStripMenuItem defaultSoundToolStripMenuItem;
        private ToolStripMenuItem defaultSoundToolStripMenuItem1;
        private volatile bool duringNotify;
        private Graphics fbG;
        private Graphics fbG2;
        private Bitmap frontBuffer;
        private Bitmap frontBuffer2;
        public SpellTimerViewGenerator GenerateSpellTimerView;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label2;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private ListBox lbWhiteList;
        private ToolStripMenuItem noSoundToolStripMenuItem;
        internal NumericUpDown nudRecastDelay;
        internal NumericUpDown nudTimerRemove;
        internal NumericUpDown nudTimerScale;
        internal NumericUpDown nudTimerScale2;
        internal NumericUpDown nudTimersOpacity;
        internal NumericUpDown nudTimersOpacity2;
        internal NumericUpDown nudTimerWarning;
        private SpellTimerEventDelegate OnSpellTimerExpire;
        private EventHandler OnSpellTimerImageRefreshed;
        private SpellTimerEventDelegate OnSpellTimerNotify;
        private SpellTimerEventDelegate OnSpellTimerRemoved;
        private SpellTimerEventDelegate OnSpellTimerWarning;
        private PictureBox pbTimerColor;
        internal bool rebuildSpellTreeView;
        private Regex regexCaptureGroupLabel = new Regex(@"^(?<left>.*)\${(?<var>[^}]+)}(?<right>.*)$", RegexOptions.Compiled);
        private SoundPlayer soundPlayer = new SoundPlayer();
        private SplitContainer splitContainer2;
        private bool startupToggle = true;
        private ToolStripMenuItem systemBeepToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        internal TextBox tbCategory;
        private TextBox tbSearch;
        internal TextBox tbSpellName;
        private TextBox tbStartWav;
        private TextBox tbTooltip;
        private TextBox tbWarningWav;
        internal TextBox tbWhiteList;
        private ToolStripMenuItem textToSpeechcustomToolStripMenuItem;
        private ToolStripMenuItem textToSpeechcustomToolStripMenuItem1;
        private SortedDictionary<string, TimerData> timerDefs = new SortedDictionary<string, TimerData>();
        private Dictionary<string, TimerFrame> timerFrames = new Dictionary<string, TimerFrame>();
        private SortedDictionary<string, List<string>> timerLookups = new SortedDictionary<string, List<string>>();
        private List<TimerMod> timerMods = new List<TimerMod>();
        internal System.Windows.Forms.Timer tmrSec;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolTip toolTip1;
        private ToolStripMenuItem tTSToolStripMenuItem;
        private ToolStripMenuItem tTSToolStripMenuItem1;
        private TreeView tvSpells;
        private bool updateDisplay;
        private Dictionary<string, string> whiteList = new Dictionary<string, string>();

        public event SpellTimerEventDelegate OnSpellTimerExpire
        {
            add
            {
                SpellTimerEventDelegate delegate3;
                SpellTimerEventDelegate onSpellTimerExpire = this.OnSpellTimerExpire;
                do
                {
                    delegate3 = onSpellTimerExpire;
                    SpellTimerEventDelegate delegate4 = (SpellTimerEventDelegate) Delegate.Combine(delegate3, value);
                    onSpellTimerExpire = Interlocked.CompareExchange<SpellTimerEventDelegate>(ref this.OnSpellTimerExpire, delegate4, delegate3);
                }
                while (onSpellTimerExpire != delegate3);
            }
            remove
            {
                SpellTimerEventDelegate delegate3;
                SpellTimerEventDelegate onSpellTimerExpire = this.OnSpellTimerExpire;
                do
                {
                    delegate3 = onSpellTimerExpire;
                    SpellTimerEventDelegate delegate4 = (SpellTimerEventDelegate) Delegate.Remove(delegate3, value);
                    onSpellTimerExpire = Interlocked.CompareExchange<SpellTimerEventDelegate>(ref this.OnSpellTimerExpire, delegate4, delegate3);
                }
                while (onSpellTimerExpire != delegate3);
            }
        }

        public event EventHandler OnSpellTimerImageRefreshed
        {
            add
            {
                EventHandler handler2;
                EventHandler onSpellTimerImageRefreshed = this.OnSpellTimerImageRefreshed;
                do
                {
                    handler2 = onSpellTimerImageRefreshed;
                    EventHandler handler3 = (EventHandler) Delegate.Combine(handler2, value);
                    onSpellTimerImageRefreshed = Interlocked.CompareExchange<EventHandler>(ref this.OnSpellTimerImageRefreshed, handler3, handler2);
                }
                while (onSpellTimerImageRefreshed != handler2);
            }
            remove
            {
                EventHandler handler2;
                EventHandler onSpellTimerImageRefreshed = this.OnSpellTimerImageRefreshed;
                do
                {
                    handler2 = onSpellTimerImageRefreshed;
                    EventHandler handler3 = (EventHandler) Delegate.Remove(handler2, value);
                    onSpellTimerImageRefreshed = Interlocked.CompareExchange<EventHandler>(ref this.OnSpellTimerImageRefreshed, handler3, handler2);
                }
                while (onSpellTimerImageRefreshed != handler2);
            }
        }

        public event SpellTimerEventDelegate OnSpellTimerNotify
        {
            add
            {
                SpellTimerEventDelegate delegate3;
                SpellTimerEventDelegate onSpellTimerNotify = this.OnSpellTimerNotify;
                do
                {
                    delegate3 = onSpellTimerNotify;
                    SpellTimerEventDelegate delegate4 = (SpellTimerEventDelegate) Delegate.Combine(delegate3, value);
                    onSpellTimerNotify = Interlocked.CompareExchange<SpellTimerEventDelegate>(ref this.OnSpellTimerNotify, delegate4, delegate3);
                }
                while (onSpellTimerNotify != delegate3);
            }
            remove
            {
                SpellTimerEventDelegate delegate3;
                SpellTimerEventDelegate onSpellTimerNotify = this.OnSpellTimerNotify;
                do
                {
                    delegate3 = onSpellTimerNotify;
                    SpellTimerEventDelegate delegate4 = (SpellTimerEventDelegate) Delegate.Remove(delegate3, value);
                    onSpellTimerNotify = Interlocked.CompareExchange<SpellTimerEventDelegate>(ref this.OnSpellTimerNotify, delegate4, delegate3);
                }
                while (onSpellTimerNotify != delegate3);
            }
        }

        public event SpellTimerEventDelegate OnSpellTimerRemoved
        {
            add
            {
                SpellTimerEventDelegate delegate3;
                SpellTimerEventDelegate onSpellTimerRemoved = this.OnSpellTimerRemoved;
                do
                {
                    delegate3 = onSpellTimerRemoved;
                    SpellTimerEventDelegate delegate4 = (SpellTimerEventDelegate) Delegate.Combine(delegate3, value);
                    onSpellTimerRemoved = Interlocked.CompareExchange<SpellTimerEventDelegate>(ref this.OnSpellTimerRemoved, delegate4, delegate3);
                }
                while (onSpellTimerRemoved != delegate3);
            }
            remove
            {
                SpellTimerEventDelegate delegate3;
                SpellTimerEventDelegate onSpellTimerRemoved = this.OnSpellTimerRemoved;
                do
                {
                    delegate3 = onSpellTimerRemoved;
                    SpellTimerEventDelegate delegate4 = (SpellTimerEventDelegate) Delegate.Remove(delegate3, value);
                    onSpellTimerRemoved = Interlocked.CompareExchange<SpellTimerEventDelegate>(ref this.OnSpellTimerRemoved, delegate4, delegate3);
                }
                while (onSpellTimerRemoved != delegate3);
            }
        }

        public event SpellTimerEventDelegate OnSpellTimerWarning
        {
            add
            {
                SpellTimerEventDelegate delegate3;
                SpellTimerEventDelegate onSpellTimerWarning = this.OnSpellTimerWarning;
                do
                {
                    delegate3 = onSpellTimerWarning;
                    SpellTimerEventDelegate delegate4 = (SpellTimerEventDelegate) Delegate.Combine(delegate3, value);
                    onSpellTimerWarning = Interlocked.CompareExchange<SpellTimerEventDelegate>(ref this.OnSpellTimerWarning, delegate4, delegate3);
                }
                while (onSpellTimerWarning != delegate3);
            }
            remove
            {
                SpellTimerEventDelegate delegate3;
                SpellTimerEventDelegate onSpellTimerWarning = this.OnSpellTimerWarning;
                do
                {
                    delegate3 = onSpellTimerWarning;
                    SpellTimerEventDelegate delegate4 = (SpellTimerEventDelegate) Delegate.Remove(delegate3, value);
                    onSpellTimerWarning = Interlocked.CompareExchange<SpellTimerEventDelegate>(ref this.OnSpellTimerWarning, delegate4, delegate3);
                }
                while (onSpellTimerWarning != delegate3);
            }
        }

        public FormSpellTimers()
        {
            this.InitializeComponent();
            this.GenerateSpellTimerView = new SpellTimerViewGenerator(this.GenSpellTimerView);
        }

        public void AddEditTimerDef(TimerData newTd)
        {
            if (this.timerDefs.ContainsKey(newTd.Key))
            {
                this.timerDefs[newTd.Key] = newTd;
            }
            else
            {
                this.timerDefs.Add(newTd.Key, newTd);
            }
        }

        public void ApplyTimerMod(string Attacker, string Victim, string ModName, float RecastMod, int EffectDuration)
        {
            TimerMod item = new TimerMod(Attacker, Victim, ModName, RecastMod, ActGlobals.oFormActMain.LastEstimatedTime, TimeSpan.FromSeconds((double) EffectDuration));
            int index = this.timerMods.IndexOf(item);
            if (index > -1)
            {
                this.timerMods[index] = item;
            }
            else
            {
                this.timerMods.Add(item);
            }
        }

        internal void btnAddEdit_Click(object sender, EventArgs e)
        {
            try
            {
                TimerData newTd = new TimerData(this.tbSpellName.Text, this.cbOnlyMasterTicks.Checked, (int) this.nudRecastDelay.Value, this.cbRestrictToMe.Checked, this.cbAbsoluteTiming.Checked, this.tbStartWav.Text, this.tbWarningWav.Text, (int) this.nudTimerWarning.Value, this.cbShowRadial.Checked, this.cbAllowMod.Checked, this.tbTooltip.Text, this.pbTimerColor.BackColor, this.cbPanel1.Checked, this.cbPanel2.Checked) {
                    RemoveValue = (int) this.nudTimerRemove.Value,
                    Category = this.tbCategory.Text,
                    RestrictToCategory = this.cbRestrictToCategory.Checked
                };
                if (this.timerDefs.ContainsKey(newTd.Key))
                {
                    newTd.ActiveInList = this.timerDefs[newTd.Key].ActiveInList;
                }
                this.AddEditTimerDef(newTd);
                this.RebuildSpellTreeView();
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            this.tbSearch.ForeColor = SystemColors.GrayText;
            this.tbSearch.Text = "Search name or tooltip";
            this.RebuildSpellTreeView();
        }

        private void btnPlayStartWav_Click(object sender, EventArgs e)
        {
            if (this.tbStartWav.Text == "tts")
            {
                this.PlaySound("tts " + this.tbStartWav.Text + " started");
            }
            else
            {
                this.PlaySound(this.tbStartWav.Text);
            }
        }

        private void btnPlayWarningWav_Click(object sender, EventArgs e)
        {
            if (this.tbWarningWav.Text == "tts")
            {
                this.PlaySound("tts " + this.tbSpellName.Text + " warning");
            }
            else
            {
                this.PlaySound(this.tbWarningWav.Text);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tvSpells.SelectedNode != null)
                {
                    string tag = (string) this.tvSpells.SelectedNode.Tag;
                    if (this.timerDefs.ContainsKey(tag))
                    {
                        this.RemoveTimerDef(this.timerDefs[tag]);
                        this.RebuildSpellTreeView();
                    }
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }

        private void btnStartWavBrowse_Click(object sender, EventArgs e)
        {
            this.cmWAV1.Show(this.btnStartWavBrowse, new Point(8, 8));
        }

        private void btnWarningWavBrowse_Click(object sender, EventArgs e)
        {
            this.cmWAV2.Show(this.btnWarningWavBrowse, new Point(8, 8));
        }

        private void btnWhiteListAdd_Click(object sender, EventArgs e)
        {
            string key = this.tbWhiteList.Text.ToLower();
            if (!this.whiteList.ContainsKey(key))
            {
                this.lbWhiteList.Items.Add(this.tbWhiteList.Text);
                this.whiteList.Add(key, this.tbWhiteList.Text);
            }
        }

        private void btnWhiteListRemove_Click(object sender, EventArgs e)
        {
            if (this.lbWhiteList.SelectedIndex == -1)
            {
                MessageBox.Show(ActGlobals.ActLocalization.LocalizationStrings["messageBox-playerRemoveError"].DisplayedText, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-playerRemoveError"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                string key = ((string) this.lbWhiteList.Items[this.lbWhiteList.SelectedIndex]).ToLower();
                if (this.whiteList.ContainsKey(key))
                {
                    this.lbWhiteList.Items.Remove(this.tbWhiteList.Text);
                    this.whiteList.Remove(key);
                }
            }
        }

        private void cbbAllowPanel2_CheckedChanged(object sender, EventArgs e)
        {
            if (ActGlobals.oFormSpellTimersPanel.Visible)
            {
                ActGlobals.oFormSpellTimersPanel2.Visible = this.cbbAllowPanel2.Checked;
            }
        }

        private void cbTimersClickThrough_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormSpellTimersPanel.SetClickThrough(this.cbTimersClickThrough.Checked);
        }

        private void cbTimersClickThrough2_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormSpellTimersPanel2.SetClickThrough(this.cbTimersClickThrough2.Checked);
        }

        private void cbTimersOnTop_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormSpellTimersPanel.TopMost = this.cbTimersOnTop.Checked;
            ActGlobals.oFormSpellTimersPanel2.TopMost = this.cbTimersOnTop2.Checked;
        }

        public void ClearTimerDefs()
        {
            this.timerDefs.Clear();
            this.tvSpells.Nodes.Clear();
        }

        private void cmWAV1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string text = e.ClickedItem.Text;
            if (text != null)
            {
                if (!(text == "Default sound"))
                {
                    if (!(text == "No sound"))
                    {
                        if (!(text == "System Beep"))
                        {
                            if (!(text == "Text to Speech"))
                            {
                                if (!(text == "Text to Speech <custom>"))
                                {
                                    if (text == "Browse for WAV")
                                    {
                                        this.cmWAV1.Hide();
                                        OpenFileDialog dialog = new OpenFileDialog();
                                        DialogResult cancel = DialogResult.Cancel;
                                        dialog.Filter = "Waveform Files (*.wav)|*.wav";
                                        dialog.InitialDirectory = ActGlobals.oFormActMain.folderMedia.FullName;
                                        try
                                        {
                                            cancel = dialog.ShowDialog();
                                        }
                                        catch (SecurityException exception)
                                        {
                                            MessageBox.Show(ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText + exception.Message, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                            ActGlobals.oFormActMain.WriteExceptionLog(exception, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText);
                                            return;
                                        }
                                        if (cancel == DialogResult.OK)
                                        {
                                            ActGlobals.oFormActMain.folderMedia = new DirectoryInfo(Path.GetDirectoryName(dialog.FileName));
                                            this.tbStartWav.Text = dialog.FileName;
                                        }
                                    }
                                    return;
                                }
                                this.tbStartWav.Text = "tts [replace]";
                                return;
                            }
                            this.tbStartWav.Text = "tts";
                            return;
                        }
                        this.tbStartWav.Text = "beep";
                        return;
                    }
                }
                else
                {
                    this.tbStartWav.Text = string.Empty;
                    return;
                }
                this.tbStartWav.Text = "none";
            }
        }

        private void cmWAV2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string text = e.ClickedItem.Text;
            if (text != null)
            {
                if (!(text == "Default sound"))
                {
                    if (!(text == "No sound"))
                    {
                        if (!(text == "System Beep"))
                        {
                            if (!(text == "Text to Speech"))
                            {
                                if (!(text == "Text to Speech <custom>"))
                                {
                                    if (text == "Browse for WAV")
                                    {
                                        this.cmWAV2.Hide();
                                        OpenFileDialog dialog = new OpenFileDialog();
                                        DialogResult cancel = DialogResult.Cancel;
                                        dialog.Filter = "Waveform Files (*.wav)|*.wav";
                                        dialog.InitialDirectory = ActGlobals.oFormActMain.folderMedia.FullName;
                                        try
                                        {
                                            cancel = dialog.ShowDialog();
                                        }
                                        catch (SecurityException exception)
                                        {
                                            MessageBox.Show(ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText + exception.Message, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                            ActGlobals.oFormActMain.WriteExceptionLog(exception, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText);
                                            return;
                                        }
                                        if (cancel == DialogResult.OK)
                                        {
                                            ActGlobals.oFormActMain.folderMedia = new DirectoryInfo(Path.GetDirectoryName(dialog.FileName));
                                            this.tbWarningWav.Text = dialog.FileName;
                                        }
                                    }
                                    return;
                                }
                                this.tbWarningWav.Text = "tts [replace]";
                                return;
                            }
                            this.tbWarningWav.Text = "tts";
                            return;
                        }
                        this.tbWarningWav.Text = "beep";
                        return;
                    }
                }
                else
                {
                    this.tbWarningWav.Text = string.Empty;
                    return;
                }
                this.tbWarningWav.Text = "none";
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (this.tvSpells.SelectedNode != null)
            {
                this.copyAsSharableXMLToolStripMenuItem.Enabled = this.timerDefs.ContainsKey((string) this.tvSpells.SelectedNode.Tag);
                this.copyAsSharableXMLToolStripMenuItem2.Enabled = this.timerDefs.ContainsKey((string) this.tvSpells.SelectedNode.Tag);
                this.copyAsSharableXMLToolStripMenuItem3.Enabled = this.timerDefs.ContainsKey((string) this.tvSpells.SelectedNode.Tag);
                this.copyAsSharableXMLToolStripMenuItem4.Enabled = this.timerDefs.ContainsKey((string) this.tvSpells.SelectedNode.Tag);
            }
        }

        private void copyAsSharableXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tag = (string) this.tvSpells.SelectedNode.Tag;
            if (this.timerDefs.ContainsKey(tag))
            {
                ActGlobals.oFormActMain.SendToClipboard(ActGlobals.oFormActMain.ShareSpellToXml(this.timerDefs[tag], false), true);
            }
        }

        private void copyAsSharableXMLToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string tag = (string) this.tvSpells.SelectedNode.Tag;
            if (this.timerDefs.ContainsKey(tag))
            {
                string s = ActGlobals.oFormActMain.ShareSpellToXml(this.timerDefs[tag], false);
                ActGlobals.oFormActMain.SendToClipboard(HttpUtility.HtmlEncode(s).Replace("&#39;", "'"), true);
            }
        }

        private void copyAsSharableXMLToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            string tag = (string) this.tvSpells.SelectedNode.Tag;
            if (this.timerDefs.ContainsKey(tag))
            {
                ActGlobals.oFormActMain.SendToClipboard(ActGlobals.oFormActMain.ShareSpellToXml(this.timerDefs[tag], true), true);
            }
        }

        private void copyAsSharableXMLToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string tag = (string) this.tvSpells.SelectedNode.Tag;
            if (this.timerDefs.ContainsKey(tag))
            {
                string s = ActGlobals.oFormActMain.ShareSpellToXml(this.timerDefs[tag], true);
                ActGlobals.oFormActMain.SendToClipboard(HttpUtility.HtmlEncode(s).Replace("&#39;", "'"), true);
            }
        }

        public void DispellTimerMods(string DebuffTarget)
        {
            DebuffTarget = DebuffTarget.ToLower();
            for (int i = this.timerMods.Count - 1; i >= 0; i--)
            {
                TimerMod mod = this.timerMods[i];
                if (mod.Victim == DebuffTarget)
                {
                    this.timerMods.RemoveAt(i);
                }
            }
            foreach (KeyValuePair<string, TimerFrame> pair in this.timerFrames)
            {
                pair.Value.RemoveModTarget(DebuffTarget, ActGlobals.oFormActMain.LastKnownTime);
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
            xml.WriteAttributeString("Form", "FormSpellTimers");
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

        private void Form7_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        internal Bitmap GenSpellTimerView(int Width, int Height, float ScaleValue, List<TimerFrame> FrameList, int PanelNum, Dictionary<string, Color> DrawColors)
        {
            Bitmap image = new Bitmap(Width, Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Pen pen = new Pen(DrawColors["ForeColor"], 1f);
            SolidBrush brush = new SolidBrush(DrawColors["ForeColor"]);
            SolidBrush brush2 = new SolidBrush(DrawColors["WarningColor"]);
            SolidBrush brush3 = new SolidBrush(DrawColors["ExpiredColor"]);
            SolidBrush brush4 = new SolidBrush(DrawColors["BackColor"]);
            graphics.Clear(brush4.Color);
            Font font = new Font("Arial", ScaleValue * 14f);
            Font font2 = new Font("Arial", ScaleValue * 9f);
            float width = ScaleValue * 94f;
            float num2 = ScaleValue * 88f;
            float num3 = ScaleValue * 72f;
            float num4 = ScaleValue * 192f;
            float num5 = (num4 - width) / 2f;
            float num6 = (num4 - num2) / 2f;
            float num7 = (num4 - num3) / 2f;
            float startAngle = -90f;
            float single1 = ((float) Width) / (num4 + 6f);
            FlowLayoutPanel panel = new FlowLayoutPanel {
                FlowDirection = FlowDirection.LeftToRight,
                Width = Width,
                Height = Height
            };
            FrameList.Sort();
            FrameList.Reverse();
            for (int i = 0; i < FrameList.Count; i++)
            {
                float num14;
                string name;
                TimerFrame frame = FrameList[i];
                int num12 = 0;
                for (int j = 0; j < FrameList.Count; j++)
                {
                    if (frame.Name == FrameList[j].Name)
                    {
                        num12++;
                    }
                }
                if (frame.RadialDisplay)
                {
                    num14 = ScaleValue * 128f;
                }
                else
                {
                    num14 = ScaleValue * 40f;
                }
                Panel panel2 = new Panel {
                    Width = (int) num4,
                    Height = (int) num14
                };
                panel.Controls.Add(panel2);
                float num15 = (num14 - width) / 2f;
                float num16 = (num14 - num2) / 2f;
                float num17 = (num14 - num3) / 2f;
                TimeSpan span = TimeSpan.FromSeconds((double) frame.GetLargestVal(false));
                Rectangle rect = new Rectangle(panel.Controls[i].Location.X, panel.Controls[i].Location.Y, panel.Controls[i].Size.Width, panel.Controls[i].Size.Height);
                RectangleF ef = new RectangleF(rect.Left + num5, rect.Top + num15, width, width);
                graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                if (PanelNum == 1)
                {
                    ActGlobals.oFormSpellTimersPanel.ttg.Items.Add(new ToolTipRect(-1, string.Format("{0}s - {1}", frame.TimerData.TimerValue, frame.TimerData.Tooltip), (float) rect.X, (float) rect.Y, (float) rect.Width, (float) rect.Height));
                }
                if (PanelNum == 2)
                {
                    ActGlobals.oFormSpellTimersPanel2.ttg.Items.Add(new ToolTipRect(-1, string.Format("{0}s - {1}", frame.TimerData.TimerValue, frame.TimerData.Tooltip), (float) rect.X, (float) rect.Y, (float) rect.Width, (float) rect.Height));
                }
                if (frame.RadialDisplay)
                {
                    float num10;
                    if (frame.TimerData.WarningValue < 60)
                    {
                        num10 = frame.TimerData.WarningValue * 6;
                        graphics.FillPie(brush2, ef.Left, ef.Top, ef.Width, ef.Height, startAngle + num10, 2f);
                        SizeF ef2 = graphics.MeasureString(frame.TimerData.WarningValue.ToString(), font2);
                        graphics.DrawString(frame.TimerData.WarningValue.ToString(), font2, brush2, DrawingHelpers.GetRadialLocation(num10, ef.Width / 1.66f, DrawingHelpers.RectCx(ef) - (ef2.Width / 2f), DrawingHelpers.RectCy(ef) - (ef2.Height / 2f)));
                    }
                    int[] timerVals = frame.TimerVals;
                    for (int k = 0; k < timerVals.Length; k++)
                    {
                        if (timerVals[k] < 60)
                        {
                            num10 = timerVals[k] * 6;
                            if (timerVals[k] < 0)
                            {
                                graphics.FillPie(brush3, ef.Left, ef.Top, ef.Width, ef.Height, startAngle + num10, 1f);
                            }
                            else
                            {
                                graphics.FillPie(brush, ef.Left, ef.Top, ef.Width, ef.Height, startAngle + num10, 1f);
                            }
                        }
                    }
                    num10 = frame.TimerData.TimerValue * 6;
                    if (frame.TimerData.TimerValue < 60)
                    {
                        graphics.FillPie(brush, ef.Left, ef.Top, ef.Width, ef.Height, startAngle + num10, 2f);
                    }
                    graphics.FillPie(brush4, rect.Left + num6, rect.Top + num16, num2, num2, startAngle, 360f);
                    if (frame.TimerData.TimerValue < 60)
                    {
                        SizeF ef3 = graphics.MeasureString(frame.TimerData.TimerValue.ToString(), font2);
                        graphics.DrawString(frame.TimerData.TimerValue.ToString(), font2, brush, DrawingHelpers.GetRadialLocation(num10, ef.Width / 3f, DrawingHelpers.RectCx(ef) - (ef3.Width / 2f), DrawingHelpers.RectCy(ef) - (ef3.Height / 2f)));
                    }
                    num10 = span.Seconds * 6;
                    float sweepAngle = span.Minutes * 6;
                    graphics.FillPie(new SolidBrush(frame.TimerData.FillColor), rect.Left + num6, rect.Top + num16, num2, num2, startAngle, num10);
                    graphics.DrawPie(pen, rect.Left + num6, rect.Top + num16, num2, num2, startAngle, num10);
                    graphics.FillPie(new SolidBrush(frame.TimerData.FillColor), rect.Left + num7, rect.Top + num17, num3, num3, startAngle, sweepAngle);
                    graphics.DrawPie(pen, rect.Left + num7, rect.Top + num17, num3, num3, startAngle, sweepAngle);
                }
                else
                {
                    if (span.TotalSeconds > 60.0)
                    {
                        graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, frame.TimerData.FillColor)), rect);
                    }
                    else
                    {
                        double num19 = span.TotalSeconds / 60.0;
                        graphics.FillRectangle(new SolidBrush(Color.FromArgb(80, frame.TimerData.FillColor)), rect.X, rect.Y, (int) (rect.Width * num19), rect.Height);
                        if (span.TotalSeconds > 0.0)
                        {
                            graphics.DrawLine(new Pen(frame.TimerData.FillColor, 2f), rect.X + ((int) (rect.Width * num19)), rect.Top, rect.X + ((int) (rect.Width * num19)), rect.Bottom);
                        }
                    }
                    if (frame.TimerData.WarningValue < 60)
                    {
                        double num20 = ((double) frame.TimerData.WarningValue) / 60.0;
                        graphics.DrawLine(new Pen(brush2.Color, 1f), (float) (rect.X + ((int) (rect.Width * num20))), (float) rect.Top, (float) (rect.X + ((int) (rect.Width * num20))), rect.Top + (rect.Height * 0.1f));
                        graphics.DrawLine(new Pen(brush2.Color, 1f), (float) (rect.X + ((int) (rect.Width * num20))), (float) rect.Bottom, (float) (rect.X + ((int) (rect.Width * num20))), rect.Bottom - (rect.Height * 0.1f));
                        graphics.MeasureString(frame.TimerData.WarningValue.ToString(), font2);
                    }
                    if (frame.TimerData.TimerValue < 60)
                    {
                        double num21 = ((double) frame.TimerData.TimerValue) / 60.0;
                        graphics.DrawLine(new Pen(brush.Color, 1f), (float) (rect.X + ((int) (rect.Width * num21))), (float) rect.Top, (float) (rect.X + ((int) (rect.Width * num21))), rect.Top + (rect.Height * 0.1f));
                        graphics.DrawLine(new Pen(brush.Color, 1f), (float) (rect.X + ((int) (rect.Width * num21))), (float) rect.Bottom, (float) (rect.X + ((int) (rect.Width * num21))), rect.Bottom - (rect.Height * 0.1f));
                        graphics.MeasureString(frame.TimerData.TimerValue.ToString(), font2);
                    }
                }
                string str = string.Empty;
                if (frame.TopModAmount > 0f)
                {
                    int num22 = ((int) (frame.TopModAmount * 100f)) + 100;
                    str = string.Format(" ({0}%)", num22);
                }
                string text = string.Format("{0:00}m {1:00}s{2}", span.Minutes, span.Seconds, str);
                SizeF ef4 = graphics.MeasureString(text, font);
                if (num12 > 1)
                {
                    name = frame.ToString();
                }
                else
                {
                    name = frame.Name;
                }
                if (span.TotalSeconds <= frame.WarningValue)
                {
                    if (span.TotalSeconds < 1.0)
                    {
                        graphics.DrawString(name, font2, brush3, (float) (rect.X + 4), (float) (rect.Y + 4));
                        graphics.DrawString(text, font, brush3, (float) ((rect.X + (rect.Width / 2)) - (ef4.Width / 2f)), (float) ((rect.Bottom - ef4.Height) + 2f));
                    }
                    else
                    {
                        graphics.DrawString(name, font2, brush2, (float) (rect.X + 4), (float) (rect.Y + 4));
                        graphics.DrawString(text, font, brush2, (float) ((rect.X + (rect.Width / 2)) - (ef4.Width / 2f)), (float) ((rect.Bottom - ef4.Height) + 2f));
                    }
                }
                else
                {
                    graphics.DrawString(name, font2, brush, (float) (rect.X + 4), (float) (rect.Y + 4));
                    graphics.DrawString(text, font, brush, (float) ((rect.X + (rect.Width / 2)) - (ef4.Width / 2f)), (float) ((rect.Bottom - ef4.Height) + 2f));
                }
            }
            panel.Dispose();
            return image;
        }

        public List<TimerMod> GetRecastMods(string Caster)
        {
            List<TimerMod> list = new List<TimerMod>();
            for (int i = 0; i < this.timerMods.Count; i++)
            {
                TimerMod item = this.timerMods[i];
                if (item.Victim == Caster)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public List<TimerFrame> GetTimerFrames()
        {
            return this.GetTimerFrames(0);
        }

        public List<TimerFrame> GetTimerFrames(int PanelNum)
        {
            List<TimerFrame> list = new List<TimerFrame>(this.timerFrames.Values);
            if (PanelNum != 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if ((!list[i].TimerData.Panel1Display || (PanelNum != 1)) && (!list[i].TimerData.Panel2Display || (PanelNum != 2)))
                    {
                        list.RemoveAt(i);
                    }
                }
            }
            list.TrimExcess();
            return list;
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormSpellTimers));
            this.tmrSec = new System.Windows.Forms.Timer(this.components);
            this.cmWAV1 = new ContextMenuStrip(this.components);
            this.defaultSoundToolStripMenuItem1 = new ToolStripMenuItem();
            this.noSoundToolStripMenuItem = new ToolStripMenuItem();
            this.systemBeepToolStripMenuItem = new ToolStripMenuItem();
            this.browseForWAVToolStripMenuItem = new ToolStripMenuItem();
            this.tTSToolStripMenuItem = new ToolStripMenuItem();
            this.textToSpeechcustomToolStripMenuItem = new ToolStripMenuItem();
            this.cmWAV2 = new ContextMenuStrip(this.components);
            this.defaultSoundToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripMenuItem1 = new ToolStripMenuItem();
            this.toolStripMenuItem2 = new ToolStripMenuItem();
            this.toolStripMenuItem3 = new ToolStripMenuItem();
            this.tTSToolStripMenuItem1 = new ToolStripMenuItem();
            this.textToSpeechcustomToolStripMenuItem1 = new ToolStripMenuItem();
            this.label10 = new Label();
            this.nudTimerScale = new NumericUpDown();
            this.splitContainer2 = new SplitContainer();
            this.btnAddEdit = new Button();
            this.btnRemove = new Button();
            this.label7 = new Label();
            this.btnStartWavBrowse = new Button();
            this.tbStartWav = new TextBox();
            this.cbShowRadial = new CheckBox();
            this.btnPlayStartWav = new Button();
            this.cbAbsoluteTiming = new CheckBox();
            this.nudTimerWarning = new NumericUpDown();
            this.btnWarningWavBrowse = new Button();
            this.cbRestrictToMe = new CheckBox();
            this.tbSpellName = new TextBox();
            this.tbWarningWav = new TextBox();
            this.nudRecastDelay = new NumericUpDown();
            this.label5 = new Label();
            this.btnPlayWarningWav = new Button();
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.tableLayoutPanel2 = new TableLayoutPanel();
            this.label17 = new Label();
            this.cbPanel2 = new CheckBox();
            this.cbPanel1 = new CheckBox();
            this.nudTimerRemove = new NumericUpDown();
            this.label9 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label4 = new Label();
            this.label18 = new Label();
            this.cbAllowMod = new CheckBox();
            this.label19 = new Label();
            this.label20 = new Label();
            this.label21 = new Label();
            this.label22 = new Label();
            this.tbCategory = new TextBox();
            this.label23 = new Label();
            this.cbRestrictToCategory = new CheckBox();
            this.groupBox2 = new GroupBox();
            this.pbTimerColor = new PictureBox();
            this.tbTooltip = new TextBox();
            this.label6 = new Label();
            this.cbTimersOnTop = new CheckBox();
            this.groupBox3 = new GroupBox();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.label8 = new Label();
            this.nudTimersOpacity2 = new NumericUpDown();
            this.cbbAllowPanel2 = new CheckBox();
            this.nudTimersOpacity = new NumericUpDown();
            this.nudTimerScale2 = new NumericUpDown();
            this.label11 = new Label();
            this.label12 = new Label();
            this.label13 = new Label();
            this.cbTimersClickThrough2 = new CheckBox();
            this.cbTimersClickThrough = new CheckBox();
            this.label14 = new Label();
            this.cbTimersOnTop2 = new CheckBox();
            this.label15 = new Label();
            this.label16 = new Label();
            this.checkBox1 = new CheckBox();
            this.groupBox4 = new GroupBox();
            this.btnWhiteListRemove = new Button();
            this.btnWhiteListAdd = new Button();
            this.tbWhiteList = new TextBox();
            this.lbWhiteList = new ListBox();
            this.toolTip1 = new ToolTip(this.components);
            this.tvSpells = new TreeView();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.copyAsSharableXMLToolStripMenuItem = new ToolStripMenuItem();
            this.copyAsSharableXMLToolStripMenuItem2 = new ToolStripMenuItem();
            this.copyAsSharableXMLToolStripMenuItem3 = new ToolStripMenuItem();
            this.copyAsSharableXMLToolStripMenuItem4 = new ToolStripMenuItem();
            this.tbSearch = new TextBox();
            this.btnClearSearch = new Button();
            this.cbOnlyMasterTicks = new CheckBox();
            this.label24 = new Label();
            this.cmWAV1.SuspendLayout();
            this.cmWAV2.SuspendLayout();
            this.nudTimerScale.BeginInit();
            this.splitContainer2.BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.nudTimerWarning.BeginInit();
            this.nudRecastDelay.BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.nudTimerRemove.BeginInit();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.pbTimerColor).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.nudTimersOpacity2.BeginInit();
            this.nudTimersOpacity.BeginInit();
            this.nudTimerScale2.BeginInit();
            this.groupBox4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.tmrSec.Interval = 500;
            this.tmrSec.Tick += new EventHandler(this.tmrSec_Tick);
            this.cmWAV1.Items.AddRange(new ToolStripItem[] { this.defaultSoundToolStripMenuItem1, this.noSoundToolStripMenuItem, this.systemBeepToolStripMenuItem, this.browseForWAVToolStripMenuItem, this.tTSToolStripMenuItem, this.textToSpeechcustomToolStripMenuItem });
            this.cmWAV1.Name = "cmWAV1";
            this.cmWAV1.Size = new Size(0xd3, 0x88);
            this.cmWAV1.ItemClicked += new ToolStripItemClickedEventHandler(this.cmWAV1_ItemClicked);
            this.defaultSoundToolStripMenuItem1.Name = "defaultSoundToolStripMenuItem1";
            this.defaultSoundToolStripMenuItem1.Size = new Size(210, 0x16);
            this.defaultSoundToolStripMenuItem1.Text = "Default sound";
            this.noSoundToolStripMenuItem.Name = "noSoundToolStripMenuItem";
            this.noSoundToolStripMenuItem.Size = new Size(210, 0x16);
            this.noSoundToolStripMenuItem.Text = "No sound";
            this.systemBeepToolStripMenuItem.Name = "systemBeepToolStripMenuItem";
            this.systemBeepToolStripMenuItem.Size = new Size(210, 0x16);
            this.systemBeepToolStripMenuItem.Text = "System Beep";
            this.browseForWAVToolStripMenuItem.Name = "browseForWAVToolStripMenuItem";
            this.browseForWAVToolStripMenuItem.Size = new Size(210, 0x16);
            this.browseForWAVToolStripMenuItem.Text = "Browse for WAV";
            this.tTSToolStripMenuItem.Name = "tTSToolStripMenuItem";
            this.tTSToolStripMenuItem.Size = new Size(210, 0x16);
            this.tTSToolStripMenuItem.Text = "Text to Speech";
            this.textToSpeechcustomToolStripMenuItem.Name = "textToSpeechcustomToolStripMenuItem";
            this.textToSpeechcustomToolStripMenuItem.Size = new Size(210, 0x16);
            this.textToSpeechcustomToolStripMenuItem.Text = "Text to Speech <custom>";
            this.cmWAV2.Items.AddRange(new ToolStripItem[] { this.defaultSoundToolStripMenuItem, this.toolStripMenuItem1, this.toolStripMenuItem2, this.toolStripMenuItem3, this.tTSToolStripMenuItem1, this.textToSpeechcustomToolStripMenuItem1 });
            this.cmWAV2.Name = "cmWAV1";
            this.cmWAV2.Size = new Size(0xd3, 0x88);
            this.cmWAV2.ItemClicked += new ToolStripItemClickedEventHandler(this.cmWAV2_ItemClicked);
            this.defaultSoundToolStripMenuItem.Name = "defaultSoundToolStripMenuItem";
            this.defaultSoundToolStripMenuItem.Size = new Size(210, 0x16);
            this.defaultSoundToolStripMenuItem.Text = "Default sound";
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new Size(210, 0x16);
            this.toolStripMenuItem1.Text = "No sound";
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new Size(210, 0x16);
            this.toolStripMenuItem2.Text = "System Beep";
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new Size(210, 0x16);
            this.toolStripMenuItem3.Text = "Browse for WAV";
            this.tTSToolStripMenuItem1.Name = "tTSToolStripMenuItem1";
            this.tTSToolStripMenuItem1.Size = new Size(210, 0x16);
            this.tTSToolStripMenuItem1.Text = "Text to Speech";
            this.textToSpeechcustomToolStripMenuItem1.Name = "textToSpeechcustomToolStripMenuItem1";
            this.textToSpeechcustomToolStripMenuItem1.Size = new Size(210, 0x16);
            this.textToSpeechcustomToolStripMenuItem1.Text = "Text to Speech <custom>";
            this.label10.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label10.Location = new Point(4, 0x2b);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x7a, 0x18);
            this.label10.TabIndex = 0;
            this.label10.Text = "Visual Scale Factor";
            this.label10.TextAlign = ContentAlignment.MiddleLeft;
            this.nudTimerScale.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.nudTimerScale.DecimalPlaces = 1;
            this.nudTimerScale.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            int[] bits = new int[4];
            bits[0] = 1;
            bits[3] = 0x10000;
            this.nudTimerScale.Increment = new decimal(bits);
            this.nudTimerScale.Location = new Point(0x85, 0x2e);
            int[] numArray2 = new int[4];
            numArray2[0] = 2;
            this.nudTimerScale.Maximum = new decimal(numArray2);
            int[] numArray3 = new int[4];
            numArray3[0] = 5;
            numArray3[3] = 0x10000;
            this.nudTimerScale.Minimum = new decimal(numArray3);
            this.nudTimerScale.Name = "nudTimerScale";
            this.nudTimerScale.Size = new Size(0x3a, 0x12);
            this.nudTimerScale.TabIndex = 1;
            int[] numArray4 = new int[4];
            numArray4[0] = 1;
            this.nudTimerScale.Value = new decimal(numArray4);
            this.splitContainer2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.splitContainer2.Location = new Point(12, 0x1c7);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Panel1.Controls.Add(this.btnAddEdit);
            this.splitContainer2.Panel2.Controls.Add(this.btnRemove);
            this.splitContainer2.Size = new Size(0xcd, 0x12);
            this.splitContainer2.SplitterDistance = 100;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0x23;
            this.btnAddEdit.Dock = DockStyle.Fill;
            this.btnAddEdit.Location = new Point(0, 0);
            this.btnAddEdit.Name = "btnAddEdit";
            this.btnAddEdit.Size = new Size(100, 0x12);
            this.btnAddEdit.TabIndex = 0;
            this.btnAddEdit.Text = "Add/Edit";
            this.btnAddEdit.Click += new EventHandler(this.btnAddEdit_Click);
            this.btnRemove.Dock = DockStyle.Fill;
            this.btnRemove.Location = new Point(0, 0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new Size(0x68, 0x12);
            this.btnRemove.TabIndex = 0;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new EventHandler(this.btnRemove_Click);
            this.label7.AutoSize = true;
            this.label7.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label7.Location = new Point(6, 0x10);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x71, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "Custom Timer Start Sound";
            this.btnStartWavBrowse.Location = new Point(8, 0x1f);
            this.btnStartWavBrowse.Name = "btnStartWavBrowse";
            this.btnStartWavBrowse.Size = new Size(0x18, 20);
            this.btnStartWavBrowse.TabIndex = 1;
            this.btnStartWavBrowse.Text = "...";
            this.btnStartWavBrowse.Click += new EventHandler(this.btnStartWavBrowse_Click);
            this.tbStartWav.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbStartWav.Location = new Point(0x20, 0x1f);
            this.tbStartWav.Name = "tbStartWav";
            this.tbStartWav.Size = new Size(0x23e, 20);
            this.tbStartWav.TabIndex = 2;
            this.cbShowRadial.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbShowRadial.CheckAlign = ContentAlignment.MiddleCenter;
            this.cbShowRadial.Checked = true;
            this.cbShowRadial.CheckState = CheckState.Checked;
            this.cbShowRadial.Location = new Point(0xc3, 0xa7);
            this.cbShowRadial.Name = "cbShowRadial";
            this.cbShowRadial.Size = new Size(0x8e, 14);
            this.cbShowRadial.TabIndex = 15;
            this.toolTip1.SetToolTip(this.cbShowRadial, "If to show a radial display of time \r\ncounting down, or only a text display.");
            this.btnPlayStartWav.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnPlayStartWav.Image = (Image) manager.GetObject("btnPlayStartWav.Image");
            this.btnPlayStartWav.Location = new Point(0x25f, 0x1f);
            this.btnPlayStartWav.Name = "btnPlayStartWav";
            this.btnPlayStartWav.Size = new Size(0x18, 20);
            this.btnPlayStartWav.TabIndex = 3;
            this.btnPlayStartWav.Click += new EventHandler(this.btnPlayStartWav_Click);
            this.cbAbsoluteTiming.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbAbsoluteTiming.CheckAlign = ContentAlignment.MiddleCenter;
            this.cbAbsoluteTiming.Location = new Point(0xc3, 0x7d);
            this.cbAbsoluteTiming.Name = "cbAbsoluteTiming";
            this.cbAbsoluteTiming.Size = new Size(0x8e, 14);
            this.cbAbsoluteTiming.TabIndex = 11;
            this.toolTip1.SetToolTip(this.cbAbsoluteTiming, "If this timer has any time remaining, \r\nit will not restart.");
            this.nudTimerWarning.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.nudTimerWarning.Location = new Point(0xc3, 0x36);
            this.nudTimerWarning.Name = "nudTimerWarning";
            this.nudTimerWarning.Size = new Size(0x8e, 20);
            this.nudTimerWarning.TabIndex = 5;
            int[] numArray5 = new int[4];
            numArray5[0] = 10;
            this.nudTimerWarning.Value = new decimal(numArray5);
            this.btnWarningWavBrowse.Location = new Point(8, 0x45);
            this.btnWarningWavBrowse.Name = "btnWarningWavBrowse";
            this.btnWarningWavBrowse.Size = new Size(0x18, 20);
            this.btnWarningWavBrowse.TabIndex = 5;
            this.btnWarningWavBrowse.Text = "...";
            this.btnWarningWavBrowse.Click += new EventHandler(this.btnWarningWavBrowse_Click);
            this.cbRestrictToMe.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbRestrictToMe.CheckAlign = ContentAlignment.MiddleCenter;
            this.cbRestrictToMe.Location = new Point(0xc3, 0x92);
            this.cbRestrictToMe.Name = "cbRestrictToMe";
            this.cbRestrictToMe.Size = new Size(0x8e, 14);
            this.cbRestrictToMe.TabIndex = 13;
            this.toolTip1.SetToolTip(this.cbRestrictToMe, "Skill occurances with this name will only \r\ntrigger a timer if the target or source is \r\nyou, or a name listed below.");
            this.tbSpellName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tbSpellName.Location = new Point(0xc3, 4);
            this.tbSpellName.Name = "tbSpellName";
            this.tbSpellName.Size = new Size(0x8e, 20);
            this.tbSpellName.TabIndex = 1;
            this.tbWarningWav.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbWarningWav.Location = new Point(0x20, 0x45);
            this.tbWarningWav.Name = "tbWarningWav";
            this.tbWarningWav.Size = new Size(0x23e, 20);
            this.tbWarningWav.TabIndex = 6;
            this.nudRecastDelay.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.nudRecastDelay.Location = new Point(0xc3, 0x1d);
            int[] numArray6 = new int[4];
            numArray6[0] = 0xe10;
            this.nudRecastDelay.Maximum = new decimal(numArray6);
            this.nudRecastDelay.Name = "nudRecastDelay";
            this.nudRecastDelay.Size = new Size(0x8e, 20);
            this.nudRecastDelay.TabIndex = 3;
            int[] numArray7 = new int[4];
            numArray7[0] = 30;
            this.nudRecastDelay.Value = new decimal(numArray7);
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label5.Location = new Point(6, 0x36);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x66, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "Custom Warning Sound";
            this.btnPlayWarningWav.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnPlayWarningWav.Image = (Image) manager.GetObject("btnPlayWarningWav.Image");
            this.btnPlayWarningWav.Location = new Point(0x25f, 0x44);
            this.btnPlayWarningWav.Name = "btnPlayWarningWav";
            this.btnPlayWarningWav.Size = new Size(0x18, 20);
            this.btnPlayWarningWav.TabIndex = 7;
            this.btnPlayWarningWav.Click += new EventHandler(this.btnPlayWarningWav_Click);
            this.label1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(4, 0x1a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xb8, 0x18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Timer period in seconds";
            this.label1.TextAlign = ContentAlignment.MiddleLeft;
            this.groupBox1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Location = new Point(0xde, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x167, 0x143);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Timer Specific Settings";
            this.tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56.47059f));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43.52941f));
            this.tableLayoutPanel2.Controls.Add(this.label17, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbSpellName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.nudTimerRemove, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.nudRecastDelay, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.nudTimerWarning, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.cbRestrictToCategory, 1, 12);
            this.tableLayoutPanel2.Controls.Add(this.label23, 0, 12);
            this.tableLayoutPanel2.Controls.Add(this.label22, 0, 11);
            this.tableLayoutPanel2.Controls.Add(this.label21, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.label20, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.label19, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.label18, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.tbCategory, 1, 11);
            this.tableLayoutPanel2.Controls.Add(this.cbPanel2, 1, 10);
            this.tableLayoutPanel2.Controls.Add(this.cbPanel1, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.cbAllowMod, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.cbShowRadial, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.cbRestrictToMe, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.cbAbsoluteTiming, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.cbOnlyMasterTicks, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label24, 0, 4);
            this.tableLayoutPanel2.Location = new Point(6, 0x13);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 13;
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel2.Size = new Size(0x155, 0x12a);
            this.tableLayoutPanel2.TabIndex = 15;
            this.label17.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label17.AutoSize = true;
            this.label17.Location = new Point(4, 1);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0xb8, 0x18);
            this.label17.TabIndex = 0;
            this.label17.Text = "AE/Skill/Custom Trigger Name";
            this.label17.TextAlign = ContentAlignment.MiddleLeft;
            this.cbPanel2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbPanel2.CheckAlign = ContentAlignment.MiddleCenter;
            this.cbPanel2.Location = new Point(0xc3, 230);
            this.cbPanel2.Name = "cbPanel2";
            this.cbPanel2.Size = new Size(0x8e, 14);
            this.cbPanel2.TabIndex = 0x15;
            this.cbPanel2.UseVisualStyleBackColor = true;
            this.cbPanel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbPanel1.CheckAlign = ContentAlignment.MiddleCenter;
            this.cbPanel1.Checked = true;
            this.cbPanel1.CheckState = CheckState.Checked;
            this.cbPanel1.Location = new Point(0xc3, 0xd1);
            this.cbPanel1.Name = "cbPanel1";
            this.cbPanel1.Size = new Size(0x8e, 14);
            this.cbPanel1.TabIndex = 0x13;
            this.cbPanel1.UseVisualStyleBackColor = true;
            this.nudTimerRemove.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.nudTimerRemove.Location = new Point(0xc3, 0x4f);
            int[] numArray8 = new int[4];
            this.nudTimerRemove.Maximum = new decimal(numArray8);
            int[] numArray9 = new int[4];
            numArray9[0] = 600;
            numArray9[3] = -2147483648;
            this.nudTimerRemove.Minimum = new decimal(numArray9);
            this.nudTimerRemove.Name = "nudTimerRemove";
            this.nudTimerRemove.Size = new Size(0x8e, 20);
            this.nudTimerRemove.TabIndex = 7;
            int[] numArray10 = new int[4];
            numArray10[0] = 15;
            numArray10[3] = -2147483648;
            this.nudTimerRemove.Value = new decimal(numArray10);
            this.label9.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(4, 0x4c);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0xb8, 0x18);
            this.label9.TabIndex = 6;
            this.label9.Text = "Remove timer from view at";
            this.label9.TextAlign = ContentAlignment.MiddleLeft;
            this.label3.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(4, 0x33);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0xb8, 0x18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Warning point in seconds";
            this.label3.TextAlign = ContentAlignment.MiddleLeft;
            this.label2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(4, 0x7a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xb8, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Ignore timer restarts while time left";
            this.label2.TextAlign = ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.label2, "If this timer has any time remaining, \r\nit will not restart.");
            this.label4.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(4, 0x8f);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0xb8, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Restrict timers to a \"white list\"";
            this.label4.TextAlign = ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.label4, "Skill occurances with this name will only \r\ntrigger a timer if the target or source is \r\nyou, or a name listed below.\r\n");
            this.label18.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label18.AutoSize = true;
            this.label18.Location = new Point(4, 0xa4);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0xb8, 20);
            this.label18.TabIndex = 14;
            this.label18.Text = "Show Radial Timer Display";
            this.label18.TextAlign = ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.label18, "If to show a radial display of time \r\ncounting down, or only a text display.");
            this.cbAllowMod.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbAllowMod.CheckAlign = ContentAlignment.MiddleCenter;
            this.cbAllowMod.Checked = true;
            this.cbAllowMod.CheckState = CheckState.Checked;
            this.cbAllowMod.Location = new Point(0xc3, 0xbc);
            this.cbAllowMod.Name = "cbAllowMod";
            this.cbAllowMod.Size = new Size(0x8e, 14);
            this.cbAllowMod.TabIndex = 0x11;
            this.toolTip1.SetToolTip(this.cbAllowMod, "Allow recast timer modifications such as Traumatic Swipe \r\nto affect this timer's displayed period.");
            this.label19.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label19.AutoSize = true;
            this.label19.Location = new Point(4, 0xb9);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0xb8, 20);
            this.label19.TabIndex = 0x10;
            this.label19.Text = "Allow Timer Mods to affect this";
            this.label19.TextAlign = ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.label19, "Allow recast timer modifications such as Traumatic Swipe \r\nto affect this timer's displayed period.");
            this.label20.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label20.AutoSize = true;
            this.label20.Location = new Point(4, 0xce);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0xb8, 20);
            this.label20.TabIndex = 0x12;
            this.label20.Text = "Display in Panel A";
            this.label20.TextAlign = ContentAlignment.MiddleLeft;
            this.label21.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label21.AutoSize = true;
            this.label21.Location = new Point(4, 0xe3);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0xb8, 20);
            this.label21.TabIndex = 20;
            this.label21.Text = "Display in Panel B";
            this.label21.TextAlign = ContentAlignment.MiddleLeft;
            this.label22.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label22.AutoSize = true;
            this.label22.Location = new Point(4, 0xf8);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0xb8, 0x18);
            this.label22.TabIndex = 0x16;
            this.label22.Text = "Category";
            this.label22.TextAlign = ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.label22, "Category in the treeview");
            this.tbCategory.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tbCategory.Location = new Point(0xc3, 0xfb);
            this.tbCategory.Name = "tbCategory";
            this.tbCategory.Size = new Size(0x8e, 20);
            this.tbCategory.TabIndex = 0x17;
            this.tbCategory.Text = " General";
            this.toolTip1.SetToolTip(this.tbCategory, "Category in the treeview");
            this.label23.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label23.AutoSize = true;
            this.label23.ForeColor = Color.Blue;
            this.label23.Location = new Point(4, 0x111);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0xb8, 0x18);
            this.label23.TabIndex = 0x18;
            this.label23.Text = "Restrict to category zone or mob";
            this.label23.TextAlign = ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.label23, "If the timer in question should only trigger if the mob or zone name matches with the above field");
            this.cbRestrictToCategory.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbRestrictToCategory.CheckAlign = ContentAlignment.MiddleCenter;
            this.cbRestrictToCategory.Location = new Point(0xc3, 0x114);
            this.cbRestrictToCategory.Name = "cbRestrictToCategory";
            this.cbRestrictToCategory.Size = new Size(0x8e, 0x12);
            this.cbRestrictToCategory.TabIndex = 0x19;
            this.toolTip1.SetToolTip(this.cbRestrictToCategory, "If the timer in question should only trigger if the mob or zone name matches with the above field");
            this.cbRestrictToCategory.UseVisualStyleBackColor = true;
            this.groupBox2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.pbTimerColor);
            this.groupBox2.Controls.Add(this.tbTooltip);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnPlayWarningWav);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnPlayStartWav);
            this.groupBox2.Controls.Add(this.tbWarningWav);
            this.groupBox2.Controls.Add(this.btnWarningWavBrowse);
            this.groupBox2.Controls.Add(this.tbStartWav);
            this.groupBox2.Controls.Add(this.btnStartWavBrowse);
            this.groupBox2.Location = new Point(0xde, 0x155);
            this.groupBox2.MaximumSize = new Size(0x800, 0x84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x27f, 0x84);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Timer Specific Sounds/Notes/Color";
            this.pbTimerColor.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.pbTimerColor.BackColor = Color.Blue;
            this.pbTimerColor.BorderStyle = BorderStyle.Fixed3D;
            this.pbTimerColor.Location = new Point(0x261, 0x6a);
            this.pbTimerColor.Name = "pbTimerColor";
            this.pbTimerColor.Size = new Size(20, 20);
            this.pbTimerColor.TabIndex = 0x1d;
            this.pbTimerColor.TabStop = false;
            this.toolTip1.SetToolTip(this.pbTimerColor, "Display Fill Color");
            this.pbTimerColor.Click += new EventHandler(this.pbTimerColor_Click);
            this.tbTooltip.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbTooltip.Location = new Point(8, 0x6a);
            this.tbTooltip.Name = "tbTooltip";
            this.tbTooltip.Size = new Size(0x253, 20);
            this.tbTooltip.TabIndex = 9;
            this.label6.AutoSize = true;
            this.label6.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label6.Location = new Point(6, 0x5b);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x43, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "Custom Tooltip";
            this.cbTimersOnTop.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbTimersOnTop.AutoSize = true;
            this.cbTimersOnTop.CheckAlign = ContentAlignment.MiddleCenter;
            this.cbTimersOnTop.Checked = true;
            this.cbTimersOnTop.CheckState = CheckState.Checked;
            this.cbTimersOnTop.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cbTimersOnTop.Location = new Point(0x85, 0x47);
            this.cbTimersOnTop.Name = "cbTimersOnTop";
            this.cbTimersOnTop.Size = new Size(0x3a, 14);
            this.cbTimersOnTop.TabIndex = 4;
            this.cbTimersOnTop.UseVisualStyleBackColor = true;
            this.cbTimersOnTop.CheckedChanged += new EventHandler(this.cbTimersOnTop_CheckedChanged);
            this.groupBox3.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Location = new Point(0x24b, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x112, 0xa4);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Global Settings";
            this.tableLayoutPanel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nudTimersOpacity2, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.cbbAllowPanel2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.nudTimersOpacity, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.nudTimerScale2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label12, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.nudTimerScale, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label13, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbTimersClickThrough2, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbTimersClickThrough, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbTimersOnTop2, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbTimersOnTop, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label16, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBox1, 1, 1);
            this.tableLayoutPanel1.Location = new Point(6, 0x13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
            this.tableLayoutPanel1.Size = new Size(0x105, 0x87);
            this.tableLayoutPanel1.TabIndex = 11;
            this.label8.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(4, 1);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x7a, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Setting";
            this.label8.TextAlign = ContentAlignment.MiddleCenter;
            this.nudTimersOpacity2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.nudTimersOpacity2.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            int[] numArray11 = new int[4];
            numArray11[0] = 10;
            this.nudTimersOpacity2.Increment = new decimal(numArray11);
            this.nudTimersOpacity2.Location = new Point(0xc6, 0x71);
            int[] numArray12 = new int[4];
            numArray12[0] = 10;
            this.nudTimersOpacity2.Minimum = new decimal(numArray12);
            this.nudTimersOpacity2.Name = "nudTimersOpacity2";
            this.nudTimersOpacity2.Size = new Size(0x3b, 0x12);
            this.nudTimersOpacity2.TabIndex = 9;
            int[] numArray13 = new int[4];
            numArray13[0] = 100;
            this.nudTimersOpacity2.Value = new decimal(numArray13);
            this.nudTimersOpacity2.ValueChanged += new EventHandler(this.nudTimersOpacity_ValueChanged);
            this.cbbAllowPanel2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbbAllowPanel2.AutoSize = true;
            this.cbbAllowPanel2.CheckAlign = ContentAlignment.MiddleCenter;
            this.cbbAllowPanel2.Location = new Point(0xc6, 0x19);
            this.cbbAllowPanel2.Name = "cbbAllowPanel2";
            this.cbbAllowPanel2.Size = new Size(0x3b, 14);
            this.cbbAllowPanel2.TabIndex = 3;
            this.cbbAllowPanel2.UseVisualStyleBackColor = true;
            this.cbbAllowPanel2.CheckedChanged += new EventHandler(this.cbbAllowPanel2_CheckedChanged);
            this.nudTimersOpacity.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.nudTimersOpacity.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            int[] numArray14 = new int[4];
            numArray14[0] = 10;
            this.nudTimersOpacity.Increment = new decimal(numArray14);
            this.nudTimersOpacity.Location = new Point(0x85, 0x71);
            int[] numArray15 = new int[4];
            numArray15[0] = 10;
            this.nudTimersOpacity.Minimum = new decimal(numArray15);
            this.nudTimersOpacity.Name = "nudTimersOpacity";
            this.nudTimersOpacity.Size = new Size(0x3a, 0x12);
            this.nudTimersOpacity.TabIndex = 8;
            int[] numArray16 = new int[4];
            numArray16[0] = 100;
            this.nudTimersOpacity.Value = new decimal(numArray16);
            this.nudTimersOpacity.ValueChanged += new EventHandler(this.nudTimersOpacity_ValueChanged);
            this.nudTimerScale2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.nudTimerScale2.DecimalPlaces = 1;
            this.nudTimerScale2.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            int[] numArray17 = new int[4];
            numArray17[0] = 1;
            numArray17[3] = 0x10000;
            this.nudTimerScale2.Increment = new decimal(numArray17);
            this.nudTimerScale2.Location = new Point(0xc6, 0x2e);
            int[] numArray18 = new int[4];
            numArray18[0] = 2;
            this.nudTimerScale2.Maximum = new decimal(numArray18);
            int[] numArray19 = new int[4];
            numArray19[0] = 5;
            numArray19[3] = 0x10000;
            this.nudTimerScale2.Minimum = new decimal(numArray19);
            this.nudTimerScale2.Name = "nudTimerScale2";
            this.nudTimerScale2.Size = new Size(0x3b, 0x12);
            this.nudTimerScale2.TabIndex = 2;
            int[] numArray20 = new int[4];
            numArray20[0] = 1;
            this.nudTimerScale2.Value = new decimal(numArray20);
            this.label11.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(4, 110);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x7a, 0x18);
            this.label11.TabIndex = 10;
            this.label11.Text = "Panel Opacity (%)";
            this.label11.TextAlign = ContentAlignment.MiddleLeft;
            this.label12.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x85, 1);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x3a, 20);
            this.label12.TabIndex = 1;
            this.label12.Text = "Panel A";
            this.label12.TextAlign = ContentAlignment.MiddleCenter;
            this.label13.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0xc6, 1);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x3b, 20);
            this.label13.TabIndex = 2;
            this.label13.Text = "Panel B";
            this.label13.TextAlign = ContentAlignment.MiddleCenter;
            this.cbTimersClickThrough2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbTimersClickThrough2.AutoSize = true;
            this.cbTimersClickThrough2.CheckAlign = ContentAlignment.MiddleCenter;
            this.cbTimersClickThrough2.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cbTimersClickThrough2.Location = new Point(0xc6, 0x5c);
            this.cbTimersClickThrough2.Name = "cbTimersClickThrough2";
            this.cbTimersClickThrough2.Size = new Size(0x3b, 14);
            this.cbTimersClickThrough2.TabIndex = 7;
            this.cbTimersClickThrough2.UseVisualStyleBackColor = true;
            this.cbTimersClickThrough2.CheckedChanged += new EventHandler(this.cbTimersClickThrough2_CheckedChanged);
            this.cbTimersClickThrough.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbTimersClickThrough.AutoSize = true;
            this.cbTimersClickThrough.CheckAlign = ContentAlignment.MiddleCenter;
            this.cbTimersClickThrough.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cbTimersClickThrough.Location = new Point(0x85, 0x5c);
            this.cbTimersClickThrough.Name = "cbTimersClickThrough";
            this.cbTimersClickThrough.Size = new Size(0x3a, 14);
            this.cbTimersClickThrough.TabIndex = 6;
            this.cbTimersClickThrough.UseVisualStyleBackColor = true;
            this.cbTimersClickThrough.CheckedChanged += new EventHandler(this.cbTimersClickThrough_CheckedChanged);
            this.label14.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label14.AutoSize = true;
            this.label14.Location = new Point(4, 0x44);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x7a, 20);
            this.label14.TabIndex = 3;
            this.label14.Text = "Always on Top";
            this.label14.TextAlign = ContentAlignment.MiddleLeft;
            this.cbTimersOnTop2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbTimersOnTop2.AutoSize = true;
            this.cbTimersOnTop2.CheckAlign = ContentAlignment.MiddleCenter;
            this.cbTimersOnTop2.Checked = true;
            this.cbTimersOnTop2.CheckState = CheckState.Checked;
            this.cbTimersOnTop2.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cbTimersOnTop2.Location = new Point(0xc6, 0x47);
            this.cbTimersOnTop2.Name = "cbTimersOnTop2";
            this.cbTimersOnTop2.Size = new Size(0x3b, 14);
            this.cbTimersOnTop2.TabIndex = 5;
            this.cbTimersOnTop2.UseVisualStyleBackColor = true;
            this.cbTimersOnTop2.CheckedChanged += new EventHandler(this.cbTimersOnTop_CheckedChanged);
            this.label15.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(4, 0x59);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x7a, 20);
            this.label15.TabIndex = 6;
            this.label15.Text = "Enable Click-Through";
            this.label15.TextAlign = ContentAlignment.MiddleLeft;
            this.label16.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label16.AutoSize = true;
            this.label16.Location = new Point(4, 0x16);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x7a, 20);
            this.label16.TabIndex = 8;
            this.label16.Text = "Visible";
            this.label16.TextAlign = ContentAlignment.MiddleLeft;
            this.checkBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = ContentAlignment.MiddleCenter;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = CheckState.Checked;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new Point(0x85, 0x19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x3a, 14);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.groupBox4.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.groupBox4.Controls.Add(this.btnWhiteListRemove);
            this.groupBox4.Controls.Add(this.btnWhiteListAdd);
            this.groupBox4.Controls.Add(this.tbWhiteList);
            this.groupBox4.Controls.Add(this.lbWhiteList);
            this.groupBox4.Location = new Point(0x24b, 0xb6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x112, 0x99);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Restricted Timer White List";
            this.btnWhiteListRemove.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnWhiteListRemove.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnWhiteListRemove.Location = new Point(0xd7, 0x81);
            this.btnWhiteListRemove.Name = "btnWhiteListRemove";
            this.btnWhiteListRemove.Size = new Size(0x34, 0x12);
            this.btnWhiteListRemove.TabIndex = 3;
            this.btnWhiteListRemove.Text = "Remove";
            this.btnWhiteListRemove.Click += new EventHandler(this.btnWhiteListRemove_Click);
            this.btnWhiteListAdd.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnWhiteListAdd.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnWhiteListAdd.Location = new Point(0x9d, 0x81);
            this.btnWhiteListAdd.Name = "btnWhiteListAdd";
            this.btnWhiteListAdd.Size = new Size(0x34, 0x12);
            this.btnWhiteListAdd.TabIndex = 2;
            this.btnWhiteListAdd.Text = "Add";
            this.btnWhiteListAdd.Click += new EventHandler(this.btnWhiteListAdd_Click);
            this.tbWhiteList.Location = new Point(0x8e, 0x13);
            this.tbWhiteList.Name = "tbWhiteList";
            this.tbWhiteList.Size = new Size(0x7d, 20);
            this.tbWhiteList.TabIndex = 1;
            this.tbWhiteList.Text = "Player";
            this.lbWhiteList.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lbWhiteList.FormattingEnabled = true;
            this.lbWhiteList.IntegralHeight = false;
            this.lbWhiteList.Location = new Point(7, 0x13);
            this.lbWhiteList.Name = "lbWhiteList";
            this.lbWhiteList.Size = new Size(0x81, 0x80);
            this.lbWhiteList.Sorted = true;
            this.lbWhiteList.TabIndex = 0;
            this.lbWhiteList.SelectedIndexChanged += new EventHandler(this.lbWhiteList_SelectedIndexChanged);
            this.tvSpells.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tvSpells.CheckBoxes = true;
            this.tvSpells.ContextMenuStrip = this.contextMenuStrip1;
            this.tvSpells.FullRowSelect = true;
            this.tvSpells.HideSelection = false;
            this.tvSpells.Location = new Point(12, 0x23);
            this.tvSpells.Name = "tvSpells";
            this.tvSpells.Size = new Size(0xcc, 0x19e);
            this.tvSpells.TabIndex = 0x24;
            this.tvSpells.AfterCheck += new TreeViewEventHandler(this.tvSpells_AfterCheck);
            this.tvSpells.AfterSelect += new TreeViewEventHandler(this.tvSpells_AfterSelect);
            this.tvSpells.MouseDown += new MouseEventHandler(this.tvSpells_MouseDown);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.copyAsSharableXMLToolStripMenuItem, this.copyAsSharableXMLToolStripMenuItem2, this.copyAsSharableXMLToolStripMenuItem3, this.copyAsSharableXMLToolStripMenuItem4 });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x167, 0x5c);
            this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
            this.copyAsSharableXMLToolStripMenuItem.Enabled = false;
            this.copyAsSharableXMLToolStripMenuItem.Name = "copyAsSharableXMLToolStripMenuItem";
            this.copyAsSharableXMLToolStripMenuItem.Size = new Size(0x166, 0x16);
            this.copyAsSharableXMLToolStripMenuItem.Text = "Copy as Sharable XML";
            this.copyAsSharableXMLToolStripMenuItem.ToolTipText = "Paste into chat for other copies of ACT to import";
            this.copyAsSharableXMLToolStripMenuItem.Click += new EventHandler(this.copyAsSharableXMLToolStripMenuItem_Click);
            this.copyAsSharableXMLToolStripMenuItem2.Enabled = false;
            this.copyAsSharableXMLToolStripMenuItem2.Name = "copyAsSharableXMLToolStripMenuItem2";
            this.copyAsSharableXMLToolStripMenuItem2.Size = new Size(0x166, 0x16);
            this.copyAsSharableXMLToolStripMenuItem2.Text = "Copy as Double-Encoded Sharable XML";
            this.copyAsSharableXMLToolStripMenuItem2.ToolTipText = "Paste into Forums for other copies of ACT to import";
            this.copyAsSharableXMLToolStripMenuItem2.Click += new EventHandler(this.copyAsSharableXMLToolStripMenuItem2_Click);
            this.copyAsSharableXMLToolStripMenuItem3.Enabled = false;
            this.copyAsSharableXMLToolStripMenuItem3.Name = "copyAsSharableXMLToolStripMenuItem3";
            this.copyAsSharableXMLToolStripMenuItem3.Size = new Size(0x166, 0x16);
            this.copyAsSharableXMLToolStripMenuItem3.Text = "Copy as Sharable XML w/SoundData";
            this.copyAsSharableXMLToolStripMenuItem3.ToolTipText = "Adding SoundData *may* make the string too long to paste into chat";
            this.copyAsSharableXMLToolStripMenuItem3.Click += new EventHandler(this.copyAsSharableXMLToolStripMenuItem3_Click);
            this.copyAsSharableXMLToolStripMenuItem4.Enabled = false;
            this.copyAsSharableXMLToolStripMenuItem4.Name = "copyAsSharableXMLToolStripMenuItem4";
            this.copyAsSharableXMLToolStripMenuItem4.Size = new Size(0x166, 0x16);
            this.copyAsSharableXMLToolStripMenuItem4.Text = "Copy as Double-Encoded Sharable XML w/SoundData";
            this.copyAsSharableXMLToolStripMenuItem4.ToolTipText = "Adding SoundData *may* make the string too long to paste into chat";
            this.copyAsSharableXMLToolStripMenuItem4.Click += new EventHandler(this.copyAsSharableXMLToolStripMenuItem4_Click);
            this.tbSearch.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbSearch.ForeColor = SystemColors.GrayText;
            this.tbSearch.Location = new Point(12, 12);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new Size(0x9f, 20);
            this.tbSearch.TabIndex = 0x25;
            this.tbSearch.Text = "Search name or tooltip";
            this.tbSearch.TextChanged += new EventHandler(this.tbSearch_TextChanged);
            this.tbSearch.Enter += new EventHandler(this.tbSearch_Enter);
            this.tbSearch.Leave += new EventHandler(this.tbSearch_Leave);
            this.btnClearSearch.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnClearSearch.BackColor = SystemColors.Window;
            this.btnClearSearch.FlatStyle = FlatStyle.Flat;
            this.btnClearSearch.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnClearSearch.ForeColor = Color.Black;
            this.btnClearSearch.Location = new Point(0xad, 12);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new Size(0x2b, 20);
            this.btnClearSearch.TabIndex = 0x26;
            this.btnClearSearch.Text = "Clear";
            this.btnClearSearch.UseVisualStyleBackColor = false;
            this.btnClearSearch.Click += new EventHandler(this.btnClearSearch_Click);
            this.cbOnlyMasterTicks.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbOnlyMasterTicks.CheckAlign = ContentAlignment.MiddleCenter;
            this.cbOnlyMasterTicks.Location = new Point(0xc3, 0x68);
            this.cbOnlyMasterTicks.Name = "cbOnlyMasterTicks";
            this.cbOnlyMasterTicks.Size = new Size(0x8e, 14);
            this.cbOnlyMasterTicks.TabIndex = 9;
            this.label24.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label24.AutoSize = true;
            this.label24.Location = new Point(4, 0x65);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0xb8, 20);
            this.label24.TabIndex = 8;
            this.label24.Text = "Do not use DoT ticks, only restart";
            this.label24.TextAlign = ContentAlignment.MiddleLeft;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x367, 0x1e7);
            base.Controls.Add(this.btnClearSearch);
            base.Controls.Add(this.tbSearch);
            base.Controls.Add(this.tvSpells);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.splitContainer2);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            this.DoubleBuffered = true;
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            this.MaximumSize = new Size(0x800, 0x800);
            base.MinimizeBox = false;
            this.MinimumSize = new Size(0x377, 0x209);
            base.Name = "FormSpellTimers";
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Spell Timers (Options)";
            base.Closing += new CancelEventHandler(this.Form7_Closing);
            this.cmWAV1.ResumeLayout(false);
            this.cmWAV2.ResumeLayout(false);
            this.nudTimerScale.EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.nudTimerWarning.EndInit();
            this.nudRecastDelay.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.nudTimerRemove.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((ISupportInitialize) this.pbTimerColor).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.nudTimersOpacity2.EndInit();
            this.nudTimersOpacity.EndInit();
            this.nudTimerScale2.EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void lbWhiteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lbWhiteList.SelectedIndex != -1)
            {
                this.tbWhiteList.Text = (string) this.lbWhiteList.Items[this.lbWhiteList.SelectedIndex];
            }
        }

        private void linkLabelTutorial_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://home.maine.rr.com/eqaditu/ACT/Timers/");
        }

        public void NotifySpell(string Attacker, string SpellName, bool Self, string Victim, bool Success)
        {
            this.NotifySpell(Attacker, SpellName, Self, Victim, Success, new Dictionary<string, string>());
        }

        public void NotifySpell(string Attacker, string SpellName, bool Self, string Victim, bool Success, Dictionary<string, string> ExtraInfo)
        {
            this.duringNotify = true;
            if (!ExtraInfo.ContainsKey("attacker"))
            {
                ExtraInfo.Add("attacker", Attacker);
            }
            if (!ExtraInfo.ContainsKey("victim"))
            {
                ExtraInfo.Add("victim", Victim);
            }
            Attacker = Attacker.ToLower();
            Victim = Victim.ToLower();
            DateTime lastEstimatedTime = ActGlobals.oFormActMain.LastEstimatedTime;
            List<TimerData> list = new List<TimerData>();
            if (this.timerLookups.ContainsKey(SpellName))
            {
                for (int j = 0; j < this.timerLookups[SpellName].Count; j++)
                {
                    string str = this.timerLookups[SpellName][j];
                    TimerData item = this.timerDefs[str];
                    if (((!item.RestrictToCategory || (item.Category.ToLower() == Attacker)) || ((item.Category.ToLower() == Victim) || (item.Category.ToLower() == ActGlobals.oFormActMain.CurrentZone.ToLower()))) && item.ActiveInList)
                    {
                        list.Add(item);
                    }
                }
            }
            TimerData data2 = null;
            TimerData data3 = null;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].RestrictToCategory)
                {
                    data3 = list[i];
                }
                else
                {
                    data2 = list[i];
                }
            }
            TimerData spellTimerData = null;
            if (data2 != null)
            {
                spellTimerData = data2;
            }
            if (data3 != null)
            {
                spellTimerData = data3;
            }
            if ((spellTimerData != null) && ((!spellTimerData.RestrictToMe || Self) || (this.whiteList.ContainsKey(Attacker) || this.whiteList.ContainsKey(Victim))))
            {
                TimerFrame frame;
                SpellTimer timer;
                List<TimerMod> recastMods;
                if (this.timerFrames.TryGetValue(TimerFrame.GetKey(spellTimerData.Name, Attacker), out frame))
                {
                    frame.TimerData = spellTimerData;
                }
                else
                {
                    frame = new TimerFrame(Attacker, spellTimerData);
                    this.timerFrames.Add(frame.ToString(), frame);
                }
                if (frame.OneOnly && (frame.GetLargestVal(false) > 0))
                {
                    this.duringNotify = false;
                    return;
                }
                if ((lastEstimatedTime - frame.GetMostRecentTime(true)) < TimeSpan.FromSeconds(2.0))
                {
                    this.duringNotify = false;
                    return;
                }
                if (spellTimerData.Modable)
                {
                    recastMods = this.GetRecastMods(Attacker);
                }
                else
                {
                    recastMods = new List<TimerMod>();
                }
                if (((lastEstimatedTime - frame.GetMostRecentTime(true)) < TimeSpan.FromSeconds(12.0)) && !spellTimerData.OnlyMasterTicks)
                {
                    timer = new SpellTimer(false, spellTimerData.TimerValue, recastMods, lastEstimatedTime) {
                        ExtraInfo = ExtraInfo
                    };
                }
                else
                {
                    timer = new SpellTimer(true, spellTimerData.TimerValue, recastMods, lastEstimatedTime) {
                        ExtraInfo = ExtraInfo
                    };
                    frame.ExpireSounded = false;
                    frame.WarningSounded = false;
                    string startSoundData = frame.StartSoundData;
                    while (this.regexCaptureGroupLabel.IsMatch(startSoundData))
                    {
                        Match match = this.regexCaptureGroupLabel.Match(startSoundData);
                        if (timer.ExtraInfo.ContainsKey(match.Groups["var"].Value))
                        {
                            startSoundData = match.Groups["left"].Value + timer.ExtraInfo[match.Groups["var"].Value] + match.Groups["right"].Value;
                        }
                        else
                        {
                            startSoundData = match.Groups["left"].Value + match.Groups["var"].Value + match.Groups["right"].Value;
                        }
                    }
                    this.TimerSound(frame, SpellSoundState.Start, startSoundData);
                }
                frame.SpellTimers.Add(timer);
                if (this.OnSpellTimerNotify != null)
                {
                    try
                    {
                        this.OnSpellTimerNotify(frame);
                    }
                    catch (Exception exception)
                    {
                        ActGlobals.oFormActMain.WriteExceptionLog(exception, "FormSpellTimers->OnSpellTimerNotify");
                    }
                }
            }
            this.duringNotify = false;
        }

        private void nudTimersOpacity_ValueChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormSpellTimersPanel.Opacity = ((double) this.nudTimersOpacity.Value) / 100.0;
            ActGlobals.oFormSpellTimersPanel2.Opacity = ((double) this.nudTimersOpacity2.Value) / 100.0;
        }

        private void pbTimerColor_Click(object sender, EventArgs e)
        {
            PictureBox box = (PictureBox) sender;
            ColorDialog dialog = new ColorDialog {
                AllowFullOpen = true,
                Color = box.BackColor,
                FullOpen = true,
                SolidColorOnly = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                box.BackColor = dialog.Color;
            }
        }

        private void PlaySound(string SoundData)
        {
            if (SoundData != "none")
            {
                if (SoundData == "beep")
                {
                    SystemSounds.Beep.Play();
                }
                else if (SoundData.StartsWith("tts "))
                {
                    ActGlobals.oFormActMain.TTS(SoundData.Substring(3));
                }
                else if (File.Exists(SoundData))
                {
                    ActGlobals.oFormActMain.PlaySound(SoundData);
                }
                else
                {
                    ActGlobals.oFormActMain.WriteExceptionLog(new FileNotFoundException("Could not find WAV: " + SoundData, SoundData), "SpellTimers->PlaySound");
                }
            }
        }

        public void RebuildSpellTreeView()
        {
            string str = string.Empty;
            TreeNode node = null;
            str = this.tbCategory.Text.ToLower() + "|" + this.tbSpellName.Text.ToLower();
            string category = "--";
            TreeNode node2 = null;
            this.tvSpells.BeginUpdate();
            this.tvSpells.Nodes.Clear();
            this.timerLookups.Clear();
            foreach (KeyValuePair<string, TimerData> pair in this.timerDefs)
            {
                if (!this.timerLookups.ContainsKey(pair.Value.Name))
                {
                    this.timerLookups.Add(pair.Value.Name, new List<string>());
                }
                this.timerLookups[pair.Value.Name].Add(pair.Key);
                if (pair.Value.Category != category)
                {
                    node2 = new TreeNode(pair.Value.Category) {
                        Tag = "Category"
                    };
                    category = pair.Value.Category;
                    this.tvSpells.Nodes.Add(node2);
                }
                TreeNode node3 = new TreeNode(pair.Value.ToString()) {
                    Tag = pair.Value.Key,
                    Checked = pair.Value.ActiveInList
                };
                if (pair.Value.RestrictToCategory)
                {
                    node3.ForeColor = Color.Blue;
                }
                node2.Nodes.Add(node3);
                if (pair.Value.Key == str)
                {
                    node = node3;
                }
            }
            this.tvSpells.EndUpdate();
            if (node != null)
            {
                this.tvSpells.SelectedNode = node;
            }
        }

        public void ReinitDisplayPanel()
        {
            try
            {
                if (ActGlobals.oFormSpellTimersPanel.WindowState != FormWindowState.Minimized)
                {
                    this.frontBuffer = new Bitmap(ActGlobals.oFormSpellTimersPanel.pb1.Width, ActGlobals.oFormSpellTimersPanel.pb1.Height);
                    this.fbG = Graphics.FromImage(this.frontBuffer);
                    this.fbG.SmoothingMode = SmoothingMode.AntiAlias;
                }
            }
            catch
            {
            }
            try
            {
                if (ActGlobals.oFormSpellTimersPanel2.WindowState != FormWindowState.Minimized)
                {
                    this.frontBuffer2 = new Bitmap(ActGlobals.oFormSpellTimersPanel2.pb1.Width, ActGlobals.oFormSpellTimersPanel2.pb1.Height);
                    this.fbG2 = Graphics.FromImage(this.frontBuffer2);
                    this.fbG2.SmoothingMode = SmoothingMode.AntiAlias;
                }
            }
            catch
            {
            }
            this.updateDisplay = true;
        }

        public void RemoveTimerDef(TimerData oldTd)
        {
            if (this.timerDefs.ContainsKey(oldTd.Key))
            {
                this.timerDefs.Remove(oldTd.Key);
            }
        }

        public void RemoveTimerMods(string DebuffOwner)
        {
            DebuffOwner = DebuffOwner.ToLower();
            for (int i = this.timerMods.Count - 1; i >= 0; i--)
            {
                TimerMod mod = this.timerMods[i];
                if (mod.Attacker == DebuffOwner)
                {
                    this.timerMods.RemoveAt(i);
                }
            }
            foreach (KeyValuePair<string, TimerFrame> pair in this.timerFrames)
            {
                pair.Value.RemoveModOwner(DebuffOwner, ActGlobals.oFormActMain.LastKnownTime);
            }
        }

        public void SearchSpellTreeView(string SearchTerm)
        {
            string category = "--";
            TreeNode node = null;
            this.tvSpells.BeginUpdate();
            this.tvSpells.Nodes.Clear();
            foreach (KeyValuePair<string, TimerData> pair in this.timerDefs)
            {
                if (pair.Value.Name.ToLower().Contains(SearchTerm) || pair.Value.Tooltip.ToLower().Contains(SearchTerm))
                {
                    if (pair.Value.Category != category)
                    {
                        node = new TreeNode(pair.Value.Category) {
                            Tag = "Category"
                        };
                        category = pair.Value.Category;
                        this.tvSpells.Nodes.Add(node);
                    }
                    TreeNode node2 = new TreeNode(pair.Value.ToString()) {
                        Tag = pair.Value.Key,
                        Checked = pair.Value.ActiveInList
                    };
                    if (pair.Value.RestrictToCategory)
                    {
                        node2.ForeColor = Color.Blue;
                    }
                    node.Nodes.Add(node2);
                }
            }
            this.tvSpells.ExpandAll();
            this.tvSpells.EndUpdate();
        }

        public void SyncWhiteList()
        {
            this.lbWhiteList.BeginUpdate();
            this.lbWhiteList.Items.Clear();
            foreach (string str in this.whiteList.Values)
            {
                this.lbWhiteList.Items.Add(str);
            }
            this.lbWhiteList.EndUpdate();
        }

        private void tbSearch_Enter(object sender, EventArgs e)
        {
            this.tbSearch.ForeColor = SystemColors.WindowText;
            if (this.tbSearch.Text == "Search name or tooltip")
            {
                this.tbSearch.Text = string.Empty;
            }
            else
            {
                this.SearchSpellTreeView(this.tbSearch.Text.ToLower());
            }
        }

        private void tbSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbSearch.Text))
            {
                this.tbSearch.ForeColor = SystemColors.GrayText;
                this.tbSearch.Text = "Search name or tooltip";
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            this.SearchSpellTreeView(this.tbSearch.Text.ToLower());
        }

        private void TimerSound(TimerFrame Frame, SpellSoundState SpellState, string SoundData)
        {
            switch (SpellState)
            {
                case SpellSoundState.Start:
                    if (!string.IsNullOrWhiteSpace(SoundData))
                    {
                        if (SoundData == "tts")
                        {
                            SoundData = "tts " + Frame.Name + " started";
                        }
                        this.PlaySound(SoundData);
                        return;
                    }
                    if (!ActGlobals.oFormActMain.opSound.rbSndStartBeep.Checked)
                    {
                        if (ActGlobals.oFormActMain.opSound.rbSndStartWAV.Checked)
                        {
                            this.PlaySound(ActGlobals.oFormActMain.opSound.tbSndStart.Text);
                            return;
                        }
                        if (!ActGlobals.oFormActMain.opSound.rbSndStartTTS.Checked)
                        {
                            break;
                        }
                        this.PlaySound("tts " + Frame.Name + " started");
                        return;
                    }
                    this.PlaySound("beep");
                    return;

                case SpellSoundState.Warning:
                    if (!string.IsNullOrWhiteSpace(SoundData))
                    {
                        if (SoundData == "tts")
                        {
                            SoundData = "tts " + Frame.Name + " warning";
                        }
                        this.PlaySound(SoundData);
                        return;
                    }
                    if (!ActGlobals.oFormActMain.opSound.rbSndWarnBeep.Checked)
                    {
                        if (ActGlobals.oFormActMain.opSound.rbSndWarnWAV.Checked)
                        {
                            this.PlaySound(ActGlobals.oFormActMain.opSound.tbSndWarn.Text);
                            return;
                        }
                        if (!ActGlobals.oFormActMain.opSound.rbSndWarnTTS.Checked)
                        {
                            break;
                        }
                        this.PlaySound("tts " + Frame.Name + " warning");
                        return;
                    }
                    this.PlaySound("beep");
                    return;

                case SpellSoundState.Expire:
                    if (!ActGlobals.oFormActMain.opSound.rbSndTimerBeep.Checked)
                    {
                        if (ActGlobals.oFormActMain.opSound.rbSndTimerWAV.Checked)
                        {
                            this.PlaySound(ActGlobals.oFormActMain.opSound.tbSndTimer.Text);
                            return;
                        }
                        if (ActGlobals.oFormActMain.opSound.rbSndTimerTTS.Checked)
                        {
                            this.PlaySound("tts " + Frame.Name + " expired");
                        }
                        break;
                    }
                    this.PlaySound("beep");
                    return;

                default:
                    return;
            }
        }

        private void tmrSec_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.startupToggle)
                {
                    this.startupToggle = false;
                    this.cbTimersClickThrough.Checked = !this.cbTimersClickThrough.Checked;
                    this.cbTimersClickThrough2.Checked = !this.cbTimersClickThrough2.Checked;
                    this.cbTimersClickThrough.Checked = !this.cbTimersClickThrough.Checked;
                    this.cbTimersClickThrough2.Checked = !this.cbTimersClickThrough2.Checked;
                    this.ReinitDisplayPanel();
                }
            }
            catch
            {
            }
            try
            {
                if (this.rebuildSpellTreeView)
                {
                    this.rebuildSpellTreeView = false;
                    this.RebuildSpellTreeView();
                }
                for (int i = this.timerMods.Count - 1; i > -1; i--)
                {
                    if ((ActGlobals.oFormActMain.LastEstimatedTime - this.timerMods[i].LastUse) > this.timerMods[i].UseDuration)
                    {
                        this.timerMods.RemoveAt(i);
                    }
                }
                if (this.timerFrames.Count > 0)
                {
                    this.updateDisplay = true;
                }
                if (!this.duringNotify)
                {
                    foreach (KeyValuePair<string, TimerFrame> pair in this.timerFrames)
                    {
                        if ((pair.Value.GetLargestVal(false) <= pair.Value.WarningValue) && !pair.Value.WarningSounded)
                        {
                            string warningSoundData = pair.Value.WarningSoundData;
                            while (this.regexCaptureGroupLabel.IsMatch(warningSoundData))
                            {
                                Match match = this.regexCaptureGroupLabel.Match(warningSoundData);
                                if (pair.Value.SpellTimers[0].ExtraInfo.ContainsKey(match.Groups["var"].Value))
                                {
                                    warningSoundData = match.Groups["left"].Value + pair.Value.SpellTimers[0].ExtraInfo[match.Groups["var"].Value] + match.Groups["right"].Value;
                                }
                                else
                                {
                                    warningSoundData = match.Groups["left"].Value + match.Groups["var"].Value + match.Groups["right"].Value;
                                }
                            }
                            this.TimerSound(pair.Value, SpellSoundState.Warning, warningSoundData);
                            pair.Value.WarningSounded = true;
                            if (this.OnSpellTimerWarning != null)
                            {
                                try
                                {
                                    this.OnSpellTimerWarning(pair.Value);
                                }
                                catch (Exception exception)
                                {
                                    ActGlobals.oFormActMain.WriteExceptionLog(exception, "FormSpellTimers->OnSpellTimerWarning");
                                }
                            }
                            break;
                        }
                        if ((pair.Value.GetLargestVal(false) <= 0) && !pair.Value.ExpireSounded)
                        {
                            this.TimerSound(pair.Value, SpellSoundState.Expire, string.Empty);
                            pair.Value.ExpireSounded = true;
                            if (this.OnSpellTimerExpire != null)
                            {
                                try
                                {
                                    this.OnSpellTimerExpire(pair.Value);
                                }
                                catch (Exception exception2)
                                {
                                    ActGlobals.oFormActMain.WriteExceptionLog(exception2, "FormSpellTimers->OnSpellTimerExpire");
                                }
                            }
                            break;
                        }
                        for (int j = pair.Value.SpellTimers.Count - 1; j >= 0; j--)
                        {
                            if (pair.Value.SpellTimers[j].TimeLeft < pair.Value.TimerData.RemoveValue)
                            {
                                pair.Value.SpellTimers.RemoveAt(j);
                            }
                        }
                    }
                    foreach (KeyValuePair<string, TimerFrame> pair2 in this.timerFrames)
                    {
                        if ((pair2.Value.SpellTimers.Count == 0) || !pair2.Value.MasterExists)
                        {
                            this.timerFrames.Remove(pair2.Key);
                            if (this.OnSpellTimerRemoved == null)
                            {
                                break;
                            }
                            try
                            {
                                this.OnSpellTimerRemoved(pair2.Value);
                                break;
                            }
                            catch (Exception exception3)
                            {
                                ActGlobals.oFormActMain.WriteExceptionLog(exception3, "FormSpellTimers->OnSpellTimerRemoved");
                                break;
                            }
                        }
                    }
                }
                if (this.updateDisplay)
                {
                    this.UpdateDisplay(1);
                    this.UpdateDisplay(2);
                }
            }
            catch (Exception exception4)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception4, "SpellTimer Loop");
            }
        }

        internal void TriggerSelectedTimer()
        {
            if (this.tvSpells.SelectedNode != null)
            {
                string tag = (string) this.tvSpells.SelectedNode.Tag;
                if (this.timerDefs.ContainsKey(tag))
                {
                    TimerData data = this.timerDefs[tag];
                    this.NotifySpell(data.Category, data.Name, true, "YOU", true, new Dictionary<string, string>());
                }
            }
        }

        private void tvSpells_AfterCheck(object sender, TreeViewEventArgs e)
        {
            string tag = (string) e.Node.Tag;
            if (tag == "Category")
            {
                for (int i = 0; i < e.Node.Nodes.Count; i++)
                {
                    e.Node.Nodes[i].Checked = e.Node.Checked;
                }
            }
            else if (this.timerDefs.ContainsKey(tag))
            {
                TimerData data = this.timerDefs[tag];
                data.ActiveInList = e.Node.Checked;
            }
        }

        private void tvSpells_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                string tag = (string) e.Node.Tag;
                if (this.timerDefs.ContainsKey(tag))
                {
                    TimerData data = this.timerDefs[tag];
                    this.tbSpellName.Text = data.Name;
                    this.nudRecastDelay.Value = data.TimerValue;
                    this.cbRestrictToMe.Checked = data.RestrictToMe;
                    this.cbOnlyMasterTicks.Checked = data.OnlyMasterTicks;
                    this.cbAbsoluteTiming.Checked = data.AbsoluteTiming;
                    this.tbStartWav.Text = data.StartSoundData;
                    this.tbWarningWav.Text = data.WarningSoundData;
                    this.nudTimerWarning.Value = data.WarningValue;
                    this.cbShowRadial.Checked = data.RadialDisplay;
                    this.cbAllowMod.Checked = data.Modable;
                    this.tbTooltip.Text = data.Tooltip;
                    this.pbTimerColor.BackColor = data.FillColor;
                    this.cbPanel1.Checked = data.Panel1Display;
                    this.cbPanel2.Checked = data.Panel2Display;
                    this.nudTimerRemove.Value = data.RemoveValue;
                    this.tbCategory.Text = data.Category;
                    this.cbRestrictToCategory.Checked = data.RestrictToCategory;
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }

        private void tvSpells_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    TreeNode nodeAt = this.tvSpells.GetNodeAt(e.X, e.Y);
                    this.tvSpells.SelectedNode = nodeAt;
                }
            }
            catch
            {
            }
        }

        private void UpdateDisplay(int PanelNum)
        {
            Bitmap bitmap;
            if (PanelNum == 1)
            {
                ActGlobals.oFormSpellTimersPanel.ttg.Items.Clear();
            }
            else
            {
                ActGlobals.oFormSpellTimersPanel2.ttg.Items.Clear();
            }
            this.GetTimerFrames(PanelNum);
            Dictionary<string, Color> drawColors = new Dictionary<string, Color>();
            drawColors.Add("ForeColor", ActGlobals.oFormActMain.opColorMisc.ccSpellTimerForeColor.ForeColorSetting);
            drawColors.Add("BackColor", ActGlobals.oFormActMain.opColorMisc.ccSpellTimerBackColor.ForeColorSetting);
            drawColors.Add("WarningColor", ActGlobals.oFormActMain.opColorMisc.ccSpellTimerWarnColor.ForeColorSetting);
            drawColors.Add("ExpiredColor", ActGlobals.oFormActMain.opColorMisc.ccSpellTimerExpireColor.ForeColorSetting);
            if (PanelNum == 1)
            {
                bitmap = this.GenerateSpellTimerView(ActGlobals.oFormSpellTimersPanel.pb1.Width, ActGlobals.oFormSpellTimersPanel.pb1.Height, (float) this.nudTimerScale.Value, this.GetTimerFrames(1), 1, drawColors);
                this.fbG.DrawImageUnscaled(bitmap, 0, 0);
                ActGlobals.oFormSpellTimersPanel.pb1.Image = this.frontBuffer;
            }
            else
            {
                bitmap = this.GenerateSpellTimerView(ActGlobals.oFormSpellTimersPanel2.pb1.Width, ActGlobals.oFormSpellTimersPanel2.pb1.Height, (float) this.nudTimerScale2.Value, this.GetTimerFrames(2), 2, drawColors);
                this.fbG2.DrawImageUnscaled(bitmap, 0, 0);
                ActGlobals.oFormSpellTimersPanel2.pb1.Image = this.frontBuffer2;
            }
            try
            {
                if (PanelNum == 1)
                {
                    if (this.OnSpellTimerImageRefreshed != null)
                    {
                        this.OnSpellTimerImageRefreshed(ActGlobals.oFormSpellTimersPanel.pb1, new EventArgs());
                    }
                }
                else if (this.OnSpellTimerImageRefreshed != null)
                {
                    this.OnSpellTimerImageRefreshed(ActGlobals.oFormSpellTimersPanel.pb1, new EventArgs());
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, "FormSpellTimers->OnSpellTimerImageRefreshed");
            }
        }

        public bool AllowPanel2
        {
            get
            {
                return this.cbbAllowPanel2.Checked;
            }
        }

        public SortedDictionary<string, TimerData> TimerDefs
        {
            get
            {
                return this.timerDefs;
            }
        }

        public SortedDictionary<string, List<string>> TimerLookups
        {
            get
            {
                return this.timerLookups;
            }
        }

        public List<TimerMod> TimerMods
        {
            get
            {
                return this.timerMods;
            }
            set
            {
                this.timerMods = value;
            }
        }

        public Dictionary<string, string> WhiteList
        {
            get
            {
                return this.whiteList;
            }
            set
            {
                this.whiteList = value;
            }
        }

        private enum SpellSoundState
        {
            Start,
            Warning,
            Expire
        }

        public delegate Bitmap SpellTimerViewGenerator(int Width, int Height, float ScaleValue, List<TimerFrame> FrameList, int PanelNum, Dictionary<string, Color> DrawColors);
    }
}

