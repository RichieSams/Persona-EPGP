namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Media;
    using System.Threading;
    using System.Windows.Forms;

    public class FormImportProgress : Form
    {
        private long batchChars;
        private int batchLines;
        private Button btnClose;
        internal Button btnLastKnownName;
        private Button btnOk;
        internal string charName = string.Empty;
        private IContainer components;
        private long fileChars;
        private int fileLines;
        internal GroupBox gbPov;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label label1;
        internal Label lblBatchCount;
        private Label lblBatchStats;
        internal Label lblFileProgress;
        private Label lblFileStats;
        private Label lblLabels;
        private Label lblLabels2;
        private Label lblMessage;
        private bool operationComplete;
        internal ProgressBar progressBar1;
        internal ProgressBar progressBar2;
        private Stopwatch swBatch = new Stopwatch();
        private Stopwatch swFile = new Stopwatch();
        private Stopwatch swOperation = new Stopwatch();
        internal TextBox tbLastKnownName;
        private System.Windows.Forms.Timer timer1000;
        internal AutoResetEvent waitCharName = new AutoResetEvent(false);

        public FormImportProgress()
        {
            this.InitializeComponent();
        }

        public void AddChars(long ByteCount)
        {
            this.fileChars += ByteCount;
            this.batchChars += ByteCount;
        }

        public void AddLines(int LineCount)
        {
            this.fileLines += LineCount;
            this.batchLines += LineCount;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.CloseThis();
        }

        private void btnLastKnownName_Click(object sender, EventArgs e)
        {
            this.charName = this.btnLastKnownName.Text;
            this.waitCharName.Set();
            this.gbPov.Visible = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.charName = this.tbLastKnownName.Text;
            this.waitCharName.Set();
            this.gbPov.Visible = false;
        }

        private void CloseThis()
        {
            if (ActGlobals.oFormActMain.importThreadAlive)
            {
                if (MessageBox.Show("Do you wish to abort the import process?", "Abort?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                ActGlobals.oFormActMain.CancelImport();
            }
            base.Hide();
            this.charName = string.Empty;
            this.SetBatch(0, 1);
            this.SetProgress(-1);
            this.operationComplete = true;
            this.swOperation.Stop();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void EndOperationStats()
        {
            this.operationComplete = true;
            this.swOperation.Stop();
            this.progressBar1.Value = 100;
            SystemSounds.Exclamation.Play();
        }

        private void FormImportProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.CloseThis();
        }

        public void FreezeStats()
        {
            this.swBatch.Stop();
            this.swFile.Stop();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormImportProgress));
            this.groupBox1 = new GroupBox();
            this.progressBar2 = new ProgressBar();
            this.progressBar1 = new ProgressBar();
            this.lblFileProgress = new Label();
            this.lblBatchCount = new Label();
            this.groupBox2 = new GroupBox();
            this.lblFileStats = new Label();
            this.lblLabels = new Label();
            this.gbPov = new GroupBox();
            this.btnLastKnownName = new Button();
            this.btnOk = new Button();
            this.label1 = new Label();
            this.tbLastKnownName = new TextBox();
            this.groupBox3 = new GroupBox();
            this.lblBatchStats = new Label();
            this.lblLabels2 = new Label();
            this.timer1000 = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new GroupBox();
            this.lblMessage = new Label();
            this.btnClose = new Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbPov.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.progressBar2);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.lblFileProgress);
            this.groupBox1.Controls.Add(this.lblBatchCount);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x296, 0x5f);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            this.progressBar2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.progressBar2.Location = new Point(9, 0x43);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new Size(0x287, 0x10);
            this.progressBar2.TabIndex = 0;
            this.progressBar1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.progressBar1.Location = new Point(9, 0x20);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x287, 0x10);
            this.progressBar1.TabIndex = 0;
            this.lblFileProgress.AutoSize = true;
            this.lblFileProgress.Location = new Point(6, 0x33);
            this.lblFileProgress.Name = "lblFileProgress";
            this.lblFileProgress.Size = new Size(0x44, 13);
            this.lblFileProgress.TabIndex = 1;
            this.lblFileProgress.Text = "Progress: 0%";
            this.lblBatchCount.AutoSize = true;
            this.lblBatchCount.Location = new Point(6, 0x10);
            this.lblBatchCount.Name = "lblBatchCount";
            this.lblBatchCount.Size = new Size(0x41, 13);
            this.lblBatchCount.TabIndex = 1;
            this.lblBatchCount.Text = "Batch 0 of 0";
            this.groupBox2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.lblFileStats);
            this.groupBox2.Controls.Add(this.lblLabels);
            this.groupBox2.Location = new Point(12, 0x71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xc3, 0x52);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File Statistics";
            this.lblFileStats.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lblFileStats.AutoEllipsis = true;
            this.lblFileStats.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblFileStats.Location = new Point(0x6b, 0x10);
            this.lblFileStats.Name = "lblFileStats";
            this.lblFileStats.Size = new Size(0x52, 0x3f);
            this.lblFileStats.TabIndex = 1;
            this.lblFileStats.Text = ".";
            this.lblLabels.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lblLabels.AutoEllipsis = true;
            this.lblLabels.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblLabels.Location = new Point(6, 0x10);
            this.lblLabels.Name = "lblLabels";
            this.lblLabels.Size = new Size(0x5f, 0x3f);
            this.lblLabels.TabIndex = 0;
            this.lblLabels.Text = "Time Spent:\r\nLines Read:\r\nLPS:\r\nCharacters Read:\r\nCPS:";
            this.lblLabels.TextAlign = ContentAlignment.TopRight;
            this.gbPov.Controls.Add(this.btnLastKnownName);
            this.gbPov.Controls.Add(this.btnOk);
            this.gbPov.Controls.Add(this.label1);
            this.gbPov.Controls.Add(this.tbLastKnownName);
            this.gbPov.ForeColor = Color.Maroon;
            this.gbPov.Location = new Point(0x1a0, 0x71);
            this.gbPov.Name = "gbPov";
            this.gbPov.Size = new Size(0x102, 0x52);
            this.gbPov.TabIndex = 1;
            this.gbPov.TabStop = false;
            this.gbPov.Text = "Point of View";
            this.gbPov.Visible = false;
            this.btnLastKnownName.Location = new Point(6, 0x2d);
            this.btnLastKnownName.Name = "btnLastKnownName";
            this.btnLastKnownName.Size = new Size(0x6d, 0x1f);
            this.btnLastKnownName.TabIndex = 3;
            this.btnLastKnownName.Text = ".";
            this.btnLastKnownName.UseVisualStyleBackColor = true;
            this.btnLastKnownName.Click += new EventHandler(this.btnLastKnownName_Click);
            this.btnOk.Location = new Point(0x7e, 0x2d);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(0x7e, 0x1f);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Use Above";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 0x16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Character Name:";
            this.tbLastKnownName.Location = new Point(0x7e, 0x13);
            this.tbLastKnownName.Name = "tbLastKnownName";
            this.tbLastKnownName.Size = new Size(0x7e, 20);
            this.tbLastKnownName.TabIndex = 0;
            this.tbLastKnownName.Text = ".";
            this.groupBox3.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox3.Controls.Add(this.lblBatchStats);
            this.groupBox3.Controls.Add(this.lblLabels2);
            this.groupBox3.Location = new Point(0xd5, 0x71);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xc3, 0x52);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Batch Statistics";
            this.lblBatchStats.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lblBatchStats.AutoEllipsis = true;
            this.lblBatchStats.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblBatchStats.Location = new Point(0x6b, 0x10);
            this.lblBatchStats.Name = "lblBatchStats";
            this.lblBatchStats.Size = new Size(0x52, 0x3f);
            this.lblBatchStats.TabIndex = 1;
            this.lblBatchStats.Text = ".";
            this.lblLabels2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lblLabels2.AutoEllipsis = true;
            this.lblLabels2.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblLabels2.Location = new Point(6, 0x10);
            this.lblLabels2.Name = "lblLabels2";
            this.lblLabels2.Size = new Size(0x5f, 0x3f);
            this.lblLabels2.TabIndex = 0;
            this.lblLabels2.Text = "Time Spent:\r\nLines Read:\r\nLPS:\r\nCharacters Read:\r\nCPS:\r\n";
            this.lblLabels2.TextAlign = ContentAlignment.TopRight;
            this.timer1000.Enabled = true;
            this.timer1000.Interval = 0x3e8;
            this.timer1000.Tick += new EventHandler(this.timer1000_Tick);
            this.groupBox4.Controls.Add(this.lblMessage);
            this.groupBox4.Controls.Add(this.btnClose);
            this.groupBox4.Location = new Point(0x1a0, 0x71);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x102, 0x52);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.lblMessage.Location = new Point(6, 15);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new Size(0xf6, 0x23);
            this.lblMessage.TabIndex = 1;
            this.btnClose.Location = new Point(0xb1, 0x35);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2ae, 0xcf);
            base.Controls.Add(this.gbPov);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.groupBox4);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(200, 200);
            base.MaximizeBox = false;
            base.Name = "FormImportProgress";
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Import Progress";
            base.TopMost = true;
            base.FormClosing += new FormClosingEventHandler(this.FormImportProgress_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.gbPov.ResumeLayout(false);
            this.gbPov.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void ResetBatchStats()
        {
            this.swBatch.Reset();
            this.batchChars = 0;
            this.batchLines = 0;
        }

        public void ResetFileStats()
        {
            this.swFile.Reset();
            this.fileChars = 0;
            this.fileLines = 0;
        }

        public void ResumeStats()
        {
            this.swBatch.Start();
            this.swFile.Start();
        }

        public void SetBatch(int Numerator, int Denominator)
        {
            float num = ((float) (Numerator - 1)) / ((float) Denominator);
            if (num > 100f)
            {
                num = 100f;
            }
            if (num < 0f)
            {
                num = 0f;
            }
            num *= 100f;
            if ((Numerator == 0) && (Denominator == 1))
            {
                ThreadInvokes.ProgressBarSetValue(this, this.progressBar1, -1);
            }
            else
            {
                ThreadInvokes.ProgressBarSetValue(this, this.progressBar1, (int) num);
            }
            ThreadInvokes.ControlSetText(this, this.lblBatchCount, string.Format("Batch {0} of {1}", Numerator, Denominator));
        }

        public void SetProgress(int Percent)
        {
            if (Percent > 100)
            {
                Percent = 100;
            }
            ThreadInvokes.ProgressBarSetValue(this, this.progressBar2, Percent);
            if (Percent < 0)
            {
                Percent = 0;
            }
            ThreadInvokes.ControlSetText(this, this.lblFileProgress, string.Format("Progress: {0}%", Percent));
        }

        public void StartOperationStats()
        {
            this.operationComplete = false;
            this.swOperation.Reset();
            this.swOperation.Start();
        }

        private void timer1000_Tick(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.UpdateStats();
            }
        }

        internal void UpdateStats()
        {
            int num = (int) (this.fileChars / 0x400);
            int num2 = (int) (((double) num) / this.swFile.Elapsed.TotalSeconds);
            int num3 = (int) (((double) this.fileLines) / this.swFile.Elapsed.TotalSeconds);
            this.lblFileStats.Text = string.Format("{0:#,0}s\n{1:#,0}\n{2:#,0} per sec\n{3:#,0} k\n{4:#,0} k/s", new object[] { this.swFile.Elapsed.TotalSeconds, this.fileLines, num3, num, num2 });
            if (this.swFile.ElapsedMilliseconds == 0)
            {
                this.lblFileStats.Text = string.Empty;
            }
            num = (int) (this.batchChars / 0x400);
            num2 = (int) (((double) num) / this.swBatch.Elapsed.TotalSeconds);
            num3 = (int) (((double) this.batchLines) / this.swBatch.Elapsed.TotalSeconds);
            this.lblBatchStats.Text = string.Format("{0:#,0}s\n{1:#,0}\n{2:#,0} per sec\n{3:#,0} k\n{4:#,0} k/s", new object[] { this.swBatch.Elapsed.TotalSeconds, this.batchLines, num3, num, num2 });
            if (this.swBatch.ElapsedMilliseconds == 0)
            {
                this.lblBatchStats.Text = string.Empty;
            }
            this.lblMessage.Text = string.Format("Operation Time Elapsed: {0:#,0}s\n", this.swOperation.Elapsed.TotalSeconds);
            if (this.operationComplete)
            {
                this.lblMessage.Text = this.lblMessage.Text + "Operation Complete";
                this.lblMessage.ForeColor = Color.Maroon;
            }
            else
            {
                this.lblMessage.ForeColor = SystemColors.ControlText;
            }
        }
    }
}

