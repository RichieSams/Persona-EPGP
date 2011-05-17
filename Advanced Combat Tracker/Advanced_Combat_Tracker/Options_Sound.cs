namespace Advanced_Combat_Tracker
{
    using Advanced_Combat_Tracker.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Media;
    using System.Security;
    using System.Windows.Forms;

    internal class Options_Sound : UserControl
    {
        private Button btnSndExportBrowse;
        private Button btnSndExportPlay;
        private Button btnSndStartBrowse;
        private Button btnSndStartPlay;
        private Button btnSndTimerBrowse;
        private Button btnSndTimerPlay;
        private Button btnSndWarnBrowse;
        private Button btnSndWarnPlay;
        private IContainer components;
        private Label label12;
        private Label label93;
        private Label lblExportSound;
        private Label lblTimerExpireSound;
        private Label lblTimerStartSound;
        private Label lblTimerWarningSound;
        private Panel pAudioDevice;
        internal RadioButton rbSndExportBeep;
        internal RadioButton rbSndExportNone;
        internal RadioButton rbSndExportTTS;
        internal RadioButton rbSndExportWAV;
        internal RadioButton rbSndPlugin;
        internal RadioButton rbSndStartBeep;
        internal RadioButton rbSndStartNone;
        internal RadioButton rbSndStartTTS;
        internal RadioButton rbSndStartWAV;
        internal RadioButton rbSndTimerBeep;
        internal RadioButton rbSndTimerNone;
        internal RadioButton rbSndTimerTTS;
        internal RadioButton rbSndTimerWAV;
        internal RadioButton rbSndWarnBeep;
        internal RadioButton rbSndWarnNone;
        internal RadioButton rbSndWarnTTS;
        internal RadioButton rbSndWarnWAV;
        internal RadioButton rbSndWinApi;
        internal RadioButton rbSndWmpApi;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        internal TrackBar tbarTtsVol;
        internal TrackBar tbarWavVol;
        internal TextBox tbSndExport;
        internal TextBox tbSndStart;
        internal TextBox tbSndTimer;
        internal TextBox tbSndWarn;

        public Options_Sound()
        {
            this.InitializeComponent();
        }

        private void btnSndExportBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult cancel = DialogResult.Cancel;
            if ((ActGlobals.oFormActMain.folderMedia != null) && ActGlobals.oFormActMain.folderMedia.Exists)
            {
                dialog.InitialDirectory = ActGlobals.oFormActMain.folderMedia.FullName;
            }
            dialog.Filter = "Waveform Files (*.wav)|*.wav";
            try
            {
                cancel = dialog.ShowDialog();
            }
            catch (SecurityException exception)
            {
                MessageBox.Show(ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText + exception.Message, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                ActGlobals.oFormActMain.WriteExceptionLog(exception, "SecurityException");
                return;
            }
            if (cancel == DialogResult.OK)
            {
                ActGlobals.oFormActMain.folderMedia = new DirectoryInfo(Path.GetDirectoryName(dialog.FileName));
                this.tbSndExport.Text = dialog.FileName;
                this.rbSndExportWAV.Checked = true;
            }
        }

        private void btnSndExportPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.rbSndExportTTS.Checked)
                {
                    ActGlobals.oFormActMain.TTS(this.tbSndExport.Text);
                }
                if (this.rbSndExportWAV.Checked)
                {
                    ActGlobals.oFormActMain.PlaySound(this.tbSndExport.Text);
                }
                if (this.rbSndExportBeep.Checked)
                {
                    SystemSounds.Beep.Play();
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
                SystemSounds.Beep.Play();
            }
        }

        private void btnSndStartBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult cancel = DialogResult.Cancel;
            if ((ActGlobals.oFormActMain.folderMedia != null) && ActGlobals.oFormActMain.folderMedia.Exists)
            {
                dialog.InitialDirectory = ActGlobals.oFormActMain.folderMedia.FullName;
            }
            dialog.Filter = "Waveform Files (*.wav)|*.wav";
            try
            {
                cancel = dialog.ShowDialog();
            }
            catch (SecurityException exception)
            {
                MessageBox.Show(ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText + exception.Message, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                ActGlobals.oFormActMain.WriteExceptionLog(exception, "SecurityException");
                return;
            }
            if (cancel == DialogResult.OK)
            {
                ActGlobals.oFormActMain.folderMedia = new DirectoryInfo(Path.GetDirectoryName(dialog.FileName));
                this.tbSndStart.Text = dialog.FileName;
                this.rbSndStartWAV.Checked = true;
            }
        }

        private void btnSndStartPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.rbSndStartTTS.Checked)
                {
                    ActGlobals.oFormActMain.TTS("Spellname started");
                }
                if (this.rbSndStartWAV.Checked)
                {
                    ActGlobals.oFormActMain.PlaySound(this.tbSndStart.Text);
                }
                if (this.rbSndStartBeep.Checked)
                {
                    SystemSounds.Beep.Play();
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
                SystemSounds.Beep.Play();
            }
        }

        private void btnSndTimerBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult cancel = DialogResult.Cancel;
            if ((ActGlobals.oFormActMain.folderMedia != null) && ActGlobals.oFormActMain.folderMedia.Exists)
            {
                dialog.InitialDirectory = ActGlobals.oFormActMain.folderMedia.FullName;
            }
            dialog.Filter = "Waveform Files (*.wav)|*.wav";
            try
            {
                cancel = dialog.ShowDialog();
            }
            catch (SecurityException exception)
            {
                MessageBox.Show(ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText + exception.Message, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                ActGlobals.oFormActMain.WriteExceptionLog(exception, "SecurityException");
                return;
            }
            if (cancel == DialogResult.OK)
            {
                ActGlobals.oFormActMain.folderMedia = new DirectoryInfo(Path.GetDirectoryName(dialog.FileName));
                this.tbSndTimer.Text = dialog.FileName;
                this.rbSndTimerWAV.Checked = true;
            }
        }

        private void btnSndTimerPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.rbSndTimerTTS.Checked)
                {
                    ActGlobals.oFormActMain.TTS("Spellname expired");
                }
                if (this.rbSndTimerWAV.Checked)
                {
                    ActGlobals.oFormActMain.PlaySound(this.tbSndTimer.Text);
                }
                if (this.rbSndTimerBeep.Checked)
                {
                    SystemSounds.Beep.Play();
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
                SystemSounds.Beep.Play();
            }
        }

        private void btnSndWarnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult cancel = DialogResult.Cancel;
            if ((ActGlobals.oFormActMain.folderMedia != null) && ActGlobals.oFormActMain.folderMedia.Exists)
            {
                dialog.InitialDirectory = ActGlobals.oFormActMain.folderMedia.FullName;
            }
            dialog.Filter = "Waveform Files (*.wav)|*.wav";
            try
            {
                cancel = dialog.ShowDialog();
            }
            catch (SecurityException exception)
            {
                MessageBox.Show(ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText + exception.Message, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                ActGlobals.oFormActMain.WriteExceptionLog(exception, "SecurityException");
                return;
            }
            if (cancel == DialogResult.OK)
            {
                ActGlobals.oFormActMain.folderMedia = new DirectoryInfo(Path.GetDirectoryName(dialog.FileName));
                this.tbSndWarn.Text = dialog.FileName;
                this.rbSndWarnWAV.Checked = true;
            }
        }

        private void btnSndWarnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.rbSndWarnTTS.Checked)
                {
                    ActGlobals.oFormActMain.TTS("Spellname warning");
                }
                if (this.rbSndWarnWAV.Checked)
                {
                    ActGlobals.oFormActMain.PlaySound(this.tbSndWarn.Text);
                }
                if (this.rbSndWarnBeep.Checked)
                {
                    SystemSounds.Beep.Play();
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
                SystemSounds.Beep.Play();
            }
        }

        private void control_MouseHover(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.control_MouseHover(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnSndExportBrowse = new Button();
            this.btnSndExportPlay = new Button();
            this.rbSndExportTTS = new RadioButton();
            this.rbSndExportWAV = new RadioButton();
            this.lblExportSound = new Label();
            this.rbSndExportBeep = new RadioButton();
            this.rbSndExportNone = new RadioButton();
            this.tbSndExport = new TextBox();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.tableLayoutPanel2 = new TableLayoutPanel();
            this.rbSndStartTTS = new RadioButton();
            this.rbSndStartNone = new RadioButton();
            this.rbSndStartWAV = new RadioButton();
            this.rbSndStartBeep = new RadioButton();
            this.btnSndStartPlay = new Button();
            this.btnSndStartBrowse = new Button();
            this.tbSndStart = new TextBox();
            this.lblTimerStartSound = new Label();
            this.btnSndWarnBrowse = new Button();
            this.btnSndWarnPlay = new Button();
            this.rbSndWarnTTS = new RadioButton();
            this.rbSndWarnWAV = new RadioButton();
            this.lblTimerWarningSound = new Label();
            this.rbSndWarnBeep = new RadioButton();
            this.rbSndWarnNone = new RadioButton();
            this.tbSndWarn = new TextBox();
            this.tableLayoutPanel3 = new TableLayoutPanel();
            this.btnSndTimerBrowse = new Button();
            this.btnSndTimerPlay = new Button();
            this.rbSndTimerTTS = new RadioButton();
            this.rbSndTimerWAV = new RadioButton();
            this.lblTimerExpireSound = new Label();
            this.rbSndTimerBeep = new RadioButton();
            this.rbSndTimerNone = new RadioButton();
            this.tbSndTimer = new TextBox();
            this.tableLayoutPanel4 = new TableLayoutPanel();
            this.pAudioDevice = new Panel();
            this.rbSndPlugin = new RadioButton();
            this.rbSndWmpApi = new RadioButton();
            this.rbSndWinApi = new RadioButton();
            this.tbarWavVol = new TrackBar();
            this.label93 = new Label();
            this.tbarTtsVol = new TrackBar();
            this.label12 = new Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.pAudioDevice.SuspendLayout();
            this.tbarWavVol.BeginInit();
            this.tbarTtsVol.BeginInit();
            base.SuspendLayout();
            this.btnSndExportBrowse.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnSndExportBrowse.Location = new Point(0x2d6, 3);
            this.btnSndExportBrowse.Name = "btnSndExportBrowse";
            this.btnSndExportBrowse.Size = new Size(0x18, 0x17);
            this.btnSndExportBrowse.TabIndex = 5;
            this.btnSndExportBrowse.Text = "...";
            this.btnSndExportBrowse.Click += new EventHandler(this.btnSndExportBrowse_Click);
            this.btnSndExportBrowse.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnSndExportPlay.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnSndExportPlay.Image = Resources.play;
            this.btnSndExportPlay.Location = new Point(0x2f4, 3);
            this.btnSndExportPlay.Name = "btnSndExportPlay";
            this.btnSndExportPlay.Size = new Size(40, 0x17);
            this.btnSndExportPlay.TabIndex = 6;
            this.btnSndExportPlay.Click += new EventHandler(this.btnSndExportPlay_Click);
            this.btnSndExportPlay.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndExportTTS.AutoSize = true;
            this.rbSndExportTTS.FlatStyle = FlatStyle.System;
            this.rbSndExportTTS.Location = new Point(0x127, 3);
            this.rbSndExportTTS.Name = "rbSndExportTTS";
            this.rbSndExportTTS.Size = new Size(0x37, 0x12);
            this.rbSndExportTTS.TabIndex = 3;
            this.rbSndExportTTS.Text = "TTS:";
            this.rbSndExportTTS.CheckedChanged += new EventHandler(this.tbSndExport_Update);
            this.rbSndExportTTS.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndExportWAV.AutoSize = true;
            this.rbSndExportWAV.FlatStyle = FlatStyle.System;
            this.rbSndExportWAV.Location = new Point(230, 3);
            this.rbSndExportWAV.Name = "rbSndExportWAV";
            this.rbSndExportWAV.Size = new Size(0x3b, 0x12);
            this.rbSndExportWAV.TabIndex = 2;
            this.rbSndExportWAV.Text = "WAV:";
            this.rbSndExportWAV.CheckedChanged += new EventHandler(this.tbSndExport_Update);
            this.rbSndExportWAV.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblExportSound.Location = new Point(3, 0);
            this.lblExportSound.Name = "lblExportSound";
            this.lblExportSound.Size = new Size(0x60, 0x18);
            this.lblExportSound.TabIndex = 0;
            this.lblExportSound.Text = "Export sound:";
            this.lblExportSound.TextAlign = ContentAlignment.MiddleLeft;
            this.lblExportSound.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndExportBeep.AutoSize = true;
            this.rbSndExportBeep.FlatStyle = FlatStyle.System;
            this.rbSndExportBeep.Location = new Point(0xa8, 3);
            this.rbSndExportBeep.Name = "rbSndExportBeep";
            this.rbSndExportBeep.Size = new Size(0x38, 0x12);
            this.rbSndExportBeep.TabIndex = 1;
            this.rbSndExportBeep.Text = "Beep";
            this.rbSndExportBeep.CheckedChanged += new EventHandler(this.tbSndExport_Update);
            this.rbSndExportBeep.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndExportNone.AutoSize = true;
            this.rbSndExportNone.Checked = true;
            this.rbSndExportNone.FlatStyle = FlatStyle.System;
            this.rbSndExportNone.Location = new Point(0x69, 3);
            this.rbSndExportNone.Name = "rbSndExportNone";
            this.rbSndExportNone.Size = new Size(0x39, 0x12);
            this.rbSndExportNone.TabIndex = 0;
            this.rbSndExportNone.TabStop = true;
            this.rbSndExportNone.Text = "None";
            this.rbSndExportNone.CheckedChanged += new EventHandler(this.tbSndExport_Update);
            this.rbSndExportNone.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbSndExport.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbSndExport.Enabled = false;
            this.tbSndExport.Location = new Point(0x164, 3);
            this.tbSndExport.Name = "tbSndExport";
            this.tbSndExport.Size = new Size(0x16c, 20);
            this.tbSndExport.TabIndex = 4;
            this.tbSndExport.TextChanged += new EventHandler(this.tbSndExport_Update);
            this.tbSndExport.MouseHover += new EventHandler(this.control_MouseHover);
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tbSndExport, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.rbSndExportTTS, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.rbSndExportWAV, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.rbSndExportBeep, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.rbSndExportNone, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblExportSound, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSndExportBrowse, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSndExportPlay, 7, 0);
            this.tableLayoutPanel1.Location = new Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.Size = new Size(0x31f, 0x1d);
            this.tableLayoutPanel1.TabIndex = 0x16;
            this.tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.rbSndStartTTS, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.rbSndStartNone, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.rbSndStartWAV, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.rbSndStartBeep, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSndStartPlay, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSndStartBrowse, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbSndStart, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTimerStartSound, 0, 0);
            this.tableLayoutPanel2.Location = new Point(3, 0x20);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel2.Size = new Size(0x31f, 0x1d);
            this.tableLayoutPanel2.TabIndex = 0x16;
            this.rbSndStartTTS.AutoSize = true;
            this.rbSndStartTTS.FlatStyle = FlatStyle.System;
            this.rbSndStartTTS.Location = new Point(0x127, 3);
            this.rbSndStartTTS.Name = "rbSndStartTTS";
            this.rbSndStartTTS.Size = new Size(0x37, 0x12);
            this.rbSndStartTTS.TabIndex = 3;
            this.rbSndStartTTS.Text = "TTS:";
            this.rbSndStartTTS.CheckedChanged += new EventHandler(this.tbSndStart_Update);
            this.rbSndStartTTS.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndStartNone.AutoSize = true;
            this.rbSndStartNone.Checked = true;
            this.rbSndStartNone.FlatStyle = FlatStyle.System;
            this.rbSndStartNone.Location = new Point(0x69, 3);
            this.rbSndStartNone.Name = "rbSndStartNone";
            this.rbSndStartNone.Size = new Size(0x39, 0x12);
            this.rbSndStartNone.TabIndex = 0;
            this.rbSndStartNone.TabStop = true;
            this.rbSndStartNone.Text = "None";
            this.rbSndStartNone.CheckedChanged += new EventHandler(this.tbSndStart_Update);
            this.rbSndStartNone.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndStartWAV.AutoSize = true;
            this.rbSndStartWAV.FlatStyle = FlatStyle.System;
            this.rbSndStartWAV.Location = new Point(230, 3);
            this.rbSndStartWAV.Name = "rbSndStartWAV";
            this.rbSndStartWAV.Size = new Size(0x3b, 0x12);
            this.rbSndStartWAV.TabIndex = 2;
            this.rbSndStartWAV.Text = "WAV:";
            this.rbSndStartWAV.CheckedChanged += new EventHandler(this.tbSndStart_Update);
            this.rbSndStartWAV.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndStartBeep.AutoSize = true;
            this.rbSndStartBeep.FlatStyle = FlatStyle.System;
            this.rbSndStartBeep.Location = new Point(0xa8, 3);
            this.rbSndStartBeep.Name = "rbSndStartBeep";
            this.rbSndStartBeep.Size = new Size(0x38, 0x12);
            this.rbSndStartBeep.TabIndex = 1;
            this.rbSndStartBeep.Text = "Beep";
            this.rbSndStartBeep.CheckedChanged += new EventHandler(this.tbSndStart_Update);
            this.rbSndStartBeep.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnSndStartPlay.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnSndStartPlay.Image = Resources.play;
            this.btnSndStartPlay.Location = new Point(0x2f4, 3);
            this.btnSndStartPlay.Name = "btnSndStartPlay";
            this.btnSndStartPlay.Size = new Size(40, 0x17);
            this.btnSndStartPlay.TabIndex = 6;
            this.btnSndStartPlay.Click += new EventHandler(this.btnSndStartPlay_Click);
            this.btnSndStartPlay.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnSndStartBrowse.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnSndStartBrowse.Location = new Point(0x2d6, 3);
            this.btnSndStartBrowse.Name = "btnSndStartBrowse";
            this.btnSndStartBrowse.Size = new Size(0x18, 0x17);
            this.btnSndStartBrowse.TabIndex = 5;
            this.btnSndStartBrowse.Text = "...";
            this.btnSndStartBrowse.Click += new EventHandler(this.btnSndStartBrowse_Click);
            this.btnSndStartBrowse.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbSndStart.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbSndStart.Enabled = false;
            this.tbSndStart.Location = new Point(0x164, 3);
            this.tbSndStart.Name = "tbSndStart";
            this.tbSndStart.Size = new Size(0x16c, 20);
            this.tbSndStart.TabIndex = 4;
            this.tbSndStart.TextChanged += new EventHandler(this.tbSndStart_Update);
            this.tbSndStart.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblTimerStartSound.Location = new Point(3, 0);
            this.lblTimerStartSound.Name = "lblTimerStartSound";
            this.lblTimerStartSound.Size = new Size(0x60, 0x18);
            this.lblTimerStartSound.TabIndex = 8;
            this.lblTimerStartSound.Text = "Timer started:";
            this.lblTimerStartSound.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTimerStartSound.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnSndWarnBrowse.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnSndWarnBrowse.Location = new Point(0x2d6, 3);
            this.btnSndWarnBrowse.Name = "btnSndWarnBrowse";
            this.btnSndWarnBrowse.Size = new Size(0x18, 0x17);
            this.btnSndWarnBrowse.TabIndex = 5;
            this.btnSndWarnBrowse.Text = "...";
            this.btnSndWarnBrowse.Click += new EventHandler(this.btnSndWarnBrowse_Click);
            this.btnSndWarnBrowse.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnSndWarnPlay.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnSndWarnPlay.Image = Resources.play;
            this.btnSndWarnPlay.Location = new Point(0x2f4, 3);
            this.btnSndWarnPlay.Name = "btnSndWarnPlay";
            this.btnSndWarnPlay.Size = new Size(40, 0x17);
            this.btnSndWarnPlay.TabIndex = 6;
            this.btnSndWarnPlay.Click += new EventHandler(this.btnSndWarnPlay_Click);
            this.btnSndWarnPlay.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndWarnTTS.AutoSize = true;
            this.rbSndWarnTTS.FlatStyle = FlatStyle.System;
            this.rbSndWarnTTS.Location = new Point(0x127, 3);
            this.rbSndWarnTTS.Name = "rbSndWarnTTS";
            this.rbSndWarnTTS.Size = new Size(0x37, 0x12);
            this.rbSndWarnTTS.TabIndex = 3;
            this.rbSndWarnTTS.Text = "TTS:";
            this.rbSndWarnTTS.CheckedChanged += new EventHandler(this.tbSndWarn_Update);
            this.rbSndWarnTTS.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndWarnWAV.AutoSize = true;
            this.rbSndWarnWAV.FlatStyle = FlatStyle.System;
            this.rbSndWarnWAV.Location = new Point(230, 3);
            this.rbSndWarnWAV.Name = "rbSndWarnWAV";
            this.rbSndWarnWAV.Size = new Size(0x3b, 0x12);
            this.rbSndWarnWAV.TabIndex = 2;
            this.rbSndWarnWAV.Text = "WAV:";
            this.rbSndWarnWAV.CheckedChanged += new EventHandler(this.tbSndWarn_Update);
            this.rbSndWarnWAV.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblTimerWarningSound.Location = new Point(3, 0);
            this.lblTimerWarningSound.Name = "lblTimerWarningSound";
            this.lblTimerWarningSound.Size = new Size(0x60, 0x18);
            this.lblTimerWarningSound.TabIndex = 0x10;
            this.lblTimerWarningSound.Text = "Timer warning:";
            this.lblTimerWarningSound.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTimerWarningSound.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndWarnBeep.AutoSize = true;
            this.rbSndWarnBeep.Checked = true;
            this.rbSndWarnBeep.FlatStyle = FlatStyle.System;
            this.rbSndWarnBeep.Location = new Point(0xa8, 3);
            this.rbSndWarnBeep.Name = "rbSndWarnBeep";
            this.rbSndWarnBeep.Size = new Size(0x38, 0x12);
            this.rbSndWarnBeep.TabIndex = 1;
            this.rbSndWarnBeep.TabStop = true;
            this.rbSndWarnBeep.Text = "Beep";
            this.rbSndWarnBeep.CheckedChanged += new EventHandler(this.tbSndWarn_Update);
            this.rbSndWarnBeep.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndWarnNone.AutoSize = true;
            this.rbSndWarnNone.FlatStyle = FlatStyle.System;
            this.rbSndWarnNone.Location = new Point(0x69, 3);
            this.rbSndWarnNone.Name = "rbSndWarnNone";
            this.rbSndWarnNone.Size = new Size(0x39, 0x12);
            this.rbSndWarnNone.TabIndex = 0;
            this.rbSndWarnNone.Text = "None";
            this.rbSndWarnNone.CheckedChanged += new EventHandler(this.tbSndWarn_Update);
            this.rbSndWarnNone.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbSndWarn.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbSndWarn.Enabled = false;
            this.tbSndWarn.Location = new Point(0x164, 3);
            this.tbSndWarn.Name = "tbSndWarn";
            this.tbSndWarn.Size = new Size(0x16c, 20);
            this.tbSndWarn.TabIndex = 4;
            this.tbSndWarn.TextChanged += new EventHandler(this.tbSndWarn_Update);
            this.tbSndWarn.MouseHover += new EventHandler(this.control_MouseHover);
            this.tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 8;
            this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.rbSndWarnTTS, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.rbSndWarnNone, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.rbSndWarnWAV, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.rbSndWarnBeep, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.tbSndWarn, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblTimerWarningSound, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSndWarnPlay, 7, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSndWarnBrowse, 6, 0);
            this.tableLayoutPanel3.Location = new Point(3, 0x3d);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel3.Size = new Size(0x31f, 0x1d);
            this.tableLayoutPanel3.TabIndex = 0x16;
            this.btnSndTimerBrowse.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnSndTimerBrowse.Location = new Point(0x2d6, 3);
            this.btnSndTimerBrowse.Name = "btnSndTimerBrowse";
            this.btnSndTimerBrowse.Size = new Size(0x18, 0x17);
            this.btnSndTimerBrowse.TabIndex = 5;
            this.btnSndTimerBrowse.Text = "...";
            this.btnSndTimerBrowse.Click += new EventHandler(this.btnSndTimerBrowse_Click);
            this.btnSndTimerBrowse.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnSndTimerPlay.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnSndTimerPlay.Image = Resources.play;
            this.btnSndTimerPlay.Location = new Point(0x2f4, 3);
            this.btnSndTimerPlay.Name = "btnSndTimerPlay";
            this.btnSndTimerPlay.Size = new Size(40, 0x17);
            this.btnSndTimerPlay.TabIndex = 6;
            this.btnSndTimerPlay.Click += new EventHandler(this.btnSndTimerPlay_Click);
            this.btnSndTimerPlay.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndTimerTTS.AutoSize = true;
            this.rbSndTimerTTS.FlatStyle = FlatStyle.System;
            this.rbSndTimerTTS.Location = new Point(0x127, 3);
            this.rbSndTimerTTS.Name = "rbSndTimerTTS";
            this.rbSndTimerTTS.Size = new Size(0x37, 0x12);
            this.rbSndTimerTTS.TabIndex = 3;
            this.rbSndTimerTTS.Text = "TTS:";
            this.rbSndTimerTTS.CheckedChanged += new EventHandler(this.tbSndTimer_Update);
            this.rbSndTimerTTS.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndTimerWAV.AutoSize = true;
            this.rbSndTimerWAV.FlatStyle = FlatStyle.System;
            this.rbSndTimerWAV.Location = new Point(230, 3);
            this.rbSndTimerWAV.Name = "rbSndTimerWAV";
            this.rbSndTimerWAV.Size = new Size(0x3b, 0x12);
            this.rbSndTimerWAV.TabIndex = 2;
            this.rbSndTimerWAV.Text = "WAV:";
            this.rbSndTimerWAV.CheckedChanged += new EventHandler(this.tbSndTimer_Update);
            this.rbSndTimerWAV.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblTimerExpireSound.Location = new Point(3, 0);
            this.lblTimerExpireSound.Name = "lblTimerExpireSound";
            this.lblTimerExpireSound.Size = new Size(0x60, 0x18);
            this.lblTimerExpireSound.TabIndex = 0x18;
            this.lblTimerExpireSound.Text = "Timer expiration:";
            this.lblTimerExpireSound.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTimerExpireSound.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndTimerBeep.AutoSize = true;
            this.rbSndTimerBeep.FlatStyle = FlatStyle.System;
            this.rbSndTimerBeep.Location = new Point(0xa8, 3);
            this.rbSndTimerBeep.Name = "rbSndTimerBeep";
            this.rbSndTimerBeep.Size = new Size(0x38, 0x12);
            this.rbSndTimerBeep.TabIndex = 1;
            this.rbSndTimerBeep.Text = "Beep";
            this.rbSndTimerBeep.CheckedChanged += new EventHandler(this.tbSndTimer_Update);
            this.rbSndTimerBeep.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndTimerNone.AutoSize = true;
            this.rbSndTimerNone.Checked = true;
            this.rbSndTimerNone.FlatStyle = FlatStyle.System;
            this.rbSndTimerNone.Location = new Point(0x69, 3);
            this.rbSndTimerNone.Name = "rbSndTimerNone";
            this.rbSndTimerNone.Size = new Size(0x39, 0x12);
            this.rbSndTimerNone.TabIndex = 0;
            this.rbSndTimerNone.TabStop = true;
            this.rbSndTimerNone.Text = "None";
            this.rbSndTimerNone.CheckedChanged += new EventHandler(this.tbSndTimer_Update);
            this.rbSndTimerNone.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbSndTimer.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbSndTimer.Enabled = false;
            this.tbSndTimer.Location = new Point(0x164, 3);
            this.tbSndTimer.Name = "tbSndTimer";
            this.tbSndTimer.Size = new Size(0x16c, 20);
            this.tbSndTimer.TabIndex = 4;
            this.tbSndTimer.TextChanged += new EventHandler(this.tbSndTimer_Update);
            this.tbSndTimer.MouseHover += new EventHandler(this.control_MouseHover);
            this.tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 8;
            this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.rbSndTimerTTS, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.rbSndTimerNone, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.rbSndTimerWAV, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.rbSndTimerBeep, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.tbSndTimer, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblTimerExpireSound, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnSndTimerPlay, 7, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnSndTimerBrowse, 6, 0);
            this.tableLayoutPanel4.Location = new Point(3, 90);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel4.Size = new Size(0x31f, 0x1d);
            this.tableLayoutPanel4.TabIndex = 0x16;
            this.pAudioDevice.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.pAudioDevice.Controls.Add(this.rbSndPlugin);
            this.pAudioDevice.Controls.Add(this.rbSndWmpApi);
            this.pAudioDevice.Controls.Add(this.rbSndWinApi);
            this.pAudioDevice.Controls.Add(this.tbarWavVol);
            this.pAudioDevice.Controls.Add(this.label93);
            this.pAudioDevice.Controls.Add(this.tbarTtsVol);
            this.pAudioDevice.Controls.Add(this.label12);
            this.pAudioDevice.Location = new Point(3, 0x83);
            this.pAudioDevice.Name = "pAudioDevice";
            this.pAudioDevice.Size = new Size(0x31f, 100);
            this.pAudioDevice.TabIndex = 0x17;
            this.rbSndPlugin.AutoSize = true;
            this.rbSndPlugin.Location = new Point(3, 0x2d);
            this.rbSndPlugin.Margin = new Padding(3, 3, 3, 0);
            this.rbSndPlugin.Name = "rbSndPlugin";
            this.rbSndPlugin.Size = new Size(0xcd, 0x11);
            this.rbSndPlugin.TabIndex = 14;
            this.rbSndPlugin.Text = "No internal sound (Plugin or no sound)";
            this.rbSndPlugin.UseVisualStyleBackColor = true;
            this.rbSndPlugin.CheckedChanged += new EventHandler(this.rbSndPlugin_CheckedChanged);
            this.rbSndPlugin.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndWmpApi.AutoSize = true;
            this.rbSndWmpApi.Checked = true;
            this.rbSndWmpApi.Location = new Point(3, 0x18);
            this.rbSndWmpApi.Margin = new Padding(3, 3, 3, 0);
            this.rbSndWmpApi.Name = "rbSndWmpApi";
            this.rbSndWmpApi.Size = new Size(0x160, 0x11);
            this.rbSndWmpApi.TabIndex = 14;
            this.rbSndWmpApi.TabStop = true;
            this.rbSndWmpApi.Text = "Use WMP API Sound (Volume control, multiple sounds, slight latency)";
            this.rbSndWmpApi.UseVisualStyleBackColor = true;
            this.rbSndWmpApi.CheckedChanged += new EventHandler(this.rbSndWmpApi_CheckedChanged);
            this.rbSndWmpApi.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbSndWinApi.AutoSize = true;
            this.rbSndWinApi.Location = new Point(3, 3);
            this.rbSndWinApi.Margin = new Padding(3, 3, 3, 0);
            this.rbSndWinApi.Name = "rbSndWinApi";
            this.rbSndWinApi.Size = new Size(0x156, 0x11);
            this.rbSndWinApi.TabIndex = 14;
            this.rbSndWinApi.Text = "Use Windows API Sound  (No volume control, one sound at a time)";
            this.rbSndWinApi.UseVisualStyleBackColor = true;
            this.rbSndWinApi.CheckedChanged += new EventHandler(this.rbSndWinApi_CheckedChanged);
            this.rbSndWinApi.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbarWavVol.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.tbarWavVol.Location = new Point(0x2ba, 0x12);
            this.tbarWavVol.Maximum = 100;
            this.tbarWavVol.Name = "tbarWavVol";
            this.tbarWavVol.Orientation = Orientation.Vertical;
            this.tbarWavVol.Size = new Size(0x2d, 0x52);
            this.tbarWavVol.TabIndex = 12;
            this.tbarWavVol.TickFrequency = 10;
            this.tbarWavVol.TickStyle = TickStyle.Both;
            this.tbarWavVol.Value = 100;
            this.tbarWavVol.MouseHover += new EventHandler(this.control_MouseHover);
            this.label93.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.label93.Location = new Point(0x2eb, 0);
            this.label93.Name = "label93";
            this.label93.Size = new Size(0x36, 15);
            this.label93.TabIndex = 13;
            this.label93.Text = "TTS Vol";
            this.label93.TextAlign = ContentAlignment.MiddleCenter;
            this.label93.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbarTtsVol.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.tbarTtsVol.Location = new Point(0x2f1, 0x12);
            this.tbarTtsVol.Maximum = 100;
            this.tbarTtsVol.Name = "tbarTtsVol";
            this.tbarTtsVol.Orientation = Orientation.Vertical;
            this.tbarTtsVol.Size = new Size(0x2d, 0x52);
            this.tbarTtsVol.TabIndex = 12;
            this.tbarTtsVol.TickFrequency = 10;
            this.tbarTtsVol.TickStyle = TickStyle.Both;
            this.tbarTtsVol.Value = 100;
            this.tbarTtsVol.MouseHover += new EventHandler(this.control_MouseHover);
            this.label12.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.label12.Location = new Point(0x2b4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x36, 15);
            this.label12.TabIndex = 13;
            this.label12.Text = "Wav Vol";
            this.label12.TextAlign = ContentAlignment.MiddleCenter;
            this.label12.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.pAudioDevice);
            base.Controls.Add(this.tableLayoutPanel4);
            base.Controls.Add(this.tableLayoutPanel3);
            base.Controls.Add(this.tableLayoutPanel2);
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "Options_Sound";
            base.Size = new Size(0x325, 0xea);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.pAudioDevice.ResumeLayout(false);
            this.pAudioDevice.PerformLayout();
            this.tbarWavVol.EndInit();
            this.tbarTtsVol.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void rbSndPlugin_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbSndPlugin.Checked)
            {
                ActGlobals.oFormActMain.PlaySoundMethod = new FormActMain.PlaySoundDelegate(ActGlobals.oFormActMain.PlaySoundNull);
                this.tbarTtsVol.Enabled = true;
                this.tbarWavVol.Enabled = true;
            }
        }

        private void rbSndWinApi_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbSndWinApi.Checked)
            {
                ActGlobals.oFormActMain.PlaySoundMethod = new FormActMain.PlaySoundDelegate(ActGlobals.oFormActMain.PlaySoundWinApi);
                this.tbarTtsVol.Enabled = false;
                this.tbarWavVol.Enabled = false;
            }
        }

        private void rbSndWmpApi_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbSndWmpApi.Checked)
            {
                ActGlobals.oFormActMain.PlaySoundMethod = new FormActMain.PlaySoundDelegate(ActGlobals.oFormActMain.PlaySoundWmpApi);
                this.tbarTtsVol.Enabled = true;
                this.tbarWavVol.Enabled = true;
            }
        }

        private void tbSndExport_Update(object sender, EventArgs e)
        {
            if (this.rbSndExportWAV.Checked || this.rbSndExportTTS.Checked)
            {
                this.tbSndExport.Enabled = true;
            }
            else
            {
                this.tbSndExport.Enabled = false;
            }
        }

        private void tbSndStart_Update(object sender, EventArgs e)
        {
            if (this.rbSndStartTTS.Checked)
            {
                this.tbSndStart.Enabled = false;
            }
            if (this.rbSndStartWAV.Checked)
            {
                this.tbSndStart.Enabled = true;
            }
            if (this.rbSndStartBeep.Checked)
            {
                this.tbSndStart.Enabled = false;
            }
            if (this.rbSndStartNone.Checked)
            {
                this.tbSndStart.Enabled = false;
            }
        }

        private void tbSndTimer_Update(object sender, EventArgs e)
        {
            if (this.rbSndTimerTTS.Checked)
            {
                this.tbSndTimer.Enabled = false;
            }
            if (this.rbSndTimerWAV.Checked)
            {
                this.tbSndTimer.Enabled = true;
            }
            if (this.rbSndTimerBeep.Checked)
            {
                this.tbSndTimer.Enabled = false;
            }
            if (this.rbSndTimerNone.Checked)
            {
                this.tbSndTimer.Enabled = false;
            }
        }

        private void tbSndWarn_Update(object sender, EventArgs e)
        {
            if (this.rbSndWarnTTS.Checked)
            {
                this.tbSndWarn.Enabled = false;
            }
            if (this.rbSndWarnWAV.Checked)
            {
                this.tbSndWarn.Enabled = true;
            }
            if (this.rbSndWarnBeep.Checked)
            {
                this.tbSndWarn.Enabled = false;
            }
            if (this.rbSndWarnNone.Checked)
            {
                this.tbSndWarn.Enabled = false;
            }
        }
    }
}

