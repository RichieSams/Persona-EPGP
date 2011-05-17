namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ActLoader : Form
    {
        private IContainer components;
        internal Label lblLoading;
        private ProgressBar progressBar1;
        private Timer timer1;

        public ActLoader(string[] args)
        {
            this.InitializeComponent();
            base.Show();
            this.lblLoading.Text = "Creating Windows...";
            Application.DoEvents();
            base.TopMost = false;
            Application.DoEvents();
            this.progressBar1.Style = ProgressBarStyle.Continuous;
            ActGlobals.oActLoader = this;
            ActGlobals.oFormActMain = new FormActMain(args);
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormMiniParse = new FormMiniParse();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormUpdater = new FormUpdater();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormCombatantSearch = new FormCombatantSearch();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormResistsDeathReport = new FormResistsDeathReport();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormSpellRecastCalc = new FormSpellRecastCalc();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormSpellTimersPanel = new FormSpellTimersPanel("FormSpellTimersPanel");
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormSpellTimersPanel2 = new FormSpellTimersPanel("FormSpellTimersPanel2");
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormSpellTimers = new FormSpellTimers();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormEncounterLogs = new FormEncounterLogs();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormPerformanceWizard = new FormPerformanceWizard();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormXmlSettingsIO = new FormXmlSettingsIO();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormTimeLine = new FormTimeLine();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormEncounterVcr = new FormEncounterVcr();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormSqlQuery = new FormSqlQuery();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormByCombatantLookup = new FormByCombatantLookup();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormStartupWizard = new FormStartupWizard();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormAlliesEdit = new FormAlliesEdit();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormAvoidanceReport = new FormAvoidanceReport();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormExportFormat = new FormExportFormat();
            this.progressBar1.PerformStep();
            Application.DoEvents();
            ActGlobals.oFormImportProgress = new FormImportProgress();
            this.progressBar1.PerformStep();
            this.lblLoading.Text = "Loading Settings/Plugins...";
            Application.DoEvents();
            ActGlobals.oFormActMain.InitACT();
            this.progressBar1.Value = this.progressBar1.Maximum;
            Application.DoEvents();
            this.StartTimers();
        }

        private void ActLoader_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch
            {
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

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ActLoader));
            this.progressBar1 = new ProgressBar();
            this.lblLoading = new Label();
            this.timer1 = new Timer(this.components);
            base.SuspendLayout();
            this.progressBar1.Location = new Point(12, 0x1d);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x1ce, 12);
            this.progressBar1.Step = 3;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new Point(13, 12);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new Size(0x10, 13);
            this.lblLoading.TabIndex = 1;
            this.lblLoading.Text = "...";
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1e6, 0x39);
            base.Controls.Add(this.lblLoading);
            base.Controls.Add(this.progressBar1);
            this.Cursor = Cursors.AppStarting;
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ActLoader";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Loading ACT...";
            base.TopMost = true;
            base.FormClosing += new FormClosingEventHandler(this.ActLoader_FormClosing);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.DoEvents();
            Application.Run(new ActLoader(args));
        }

        private void StartTimers()
        {
            this.timer1.Enabled = true;
            ActGlobals.oFormActMain.tmrTen.Enabled = true;
            ActGlobals.oFormActMain.tmrTenths.Enabled = true;
            ActGlobals.oFormActMain.tmrTick.Enabled = true;
            ActGlobals.oFormUpdater.tmrUIupdate.Enabled = true;
            ActGlobals.oFormSpellTimers.tmrSec.Enabled = true;
            ActGlobals.oFormEncounterVcr.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.Show();
            base.Hide();
            this.timer1.Enabled = false;
        }
    }
}

