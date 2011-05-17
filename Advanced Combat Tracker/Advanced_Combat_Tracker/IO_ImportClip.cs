namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    internal class IO_ImportClip : UserControl
    {
        internal Button btnImportClip;
        private Button btnRefreshClipStats;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label lblClipStats;
        private RichTextBox rtb1;

        public IO_ImportClip()
        {
            this.InitializeComponent();
        }

        private void btnImportClip_Click(object sender, EventArgs e)
        {
            ThreadInvokes.ControlSetVisible(ActGlobals.oFormImportProgress, ActGlobals.oFormImportProgress.gbPov, true);
            ThreadInvokes.ControlSetText(ActGlobals.oFormImportProgress, ActGlobals.oFormImportProgress.btnLastKnownName, ActGlobals.charName);
            ThreadInvokes.ControlSetText(ActGlobals.oFormImportProgress, ActGlobals.oFormImportProgress.tbLastKnownName, ActGlobals.oFormActMain.opDataCorrectionMisc.tbCharName.Text);
            ThreadInvokes.ControlSetVisible(ActGlobals.oFormImportProgress, ActGlobals.oFormImportProgress, true);
            ActGlobals.oFormImportProgress.ResetBatchStats();
            ActGlobals.oFormImportProgress.SetBatch(0, 1);
            ActGlobals.oFormImportProgress.SetProgress(-1);
            Application.DoEvents();
            ActGlobals.oFormActMain.importThread = new Thread(new ThreadStart(ActGlobals.oFormActMain.ImportClip));
            ActGlobals.oFormActMain.importThread.Name = "Clipboard Importer";
            ActGlobals.oFormActMain.importThread.IsBackground = true;
            ActGlobals.oFormActMain.importThread.SetApartmentState(ApartmentState.STA);
            ActGlobals.oFormActMain.importThread.Start();
        }

        private void btnRefreshClipStats_Click(object sender, EventArgs e)
        {
            string text = string.Empty;
            if (Clipboard.ContainsText())
            {
                text = Clipboard.GetText();
                if (text.Length > 0x1388)
                {
                    this.rtb1.Text = text.Substring(0, 0x1388);
                }
                else
                {
                    this.rtb1.Text = text;
                }
            }
            else
            {
                this.rtb1.Text = "The clipboard did not contain text data.";
            }
            this.lblClipStats.Text = string.Format("The Windows clipboard contains {0:#,0} characters.", text.Length);
        }

        private void control_MouseHover(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.controlIO_MouseHover(sender, e);
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
            this.groupBox1 = new GroupBox();
            this.rtb1 = new RichTextBox();
            this.groupBox2 = new GroupBox();
            this.lblClipStats = new Label();
            this.btnRefreshClipStats = new Button();
            this.btnImportClip = new Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.rtb1);
            this.groupBox1.Location = new Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x242, 0x95);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Snippet";
            this.groupBox1.MouseHover += new EventHandler(this.control_MouseHover);
            this.rtb1.Dock = DockStyle.Fill;
            this.rtb1.Location = new Point(3, 0x10);
            this.rtb1.Name = "rtb1";
            this.rtb1.ReadOnly = true;
            this.rtb1.Size = new Size(0x23c, 130);
            this.rtb1.TabIndex = 0;
            this.rtb1.Text = "";
            this.rtb1.WordWrap = false;
            this.rtb1.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox2.Controls.Add(this.lblClipStats);
            this.groupBox2.Location = new Point(6, 0x9e);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x1a2, 0x35);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Statistics";
            this.groupBox2.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblClipStats.Dock = DockStyle.Fill;
            this.lblClipStats.Location = new Point(3, 0x10);
            this.lblClipStats.Name = "lblClipStats";
            this.lblClipStats.Size = new Size(0x19c, 0x22);
            this.lblClipStats.TabIndex = 0;
            this.lblClipStats.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnRefreshClipStats.Location = new Point(430, 0x9e);
            this.btnRefreshClipStats.Name = "btnRefreshClipStats";
            this.btnRefreshClipStats.Size = new Size(0x94, 0x17);
            this.btnRefreshClipStats.TabIndex = 3;
            this.btnRefreshClipStats.Text = "Examine Clipboard";
            this.btnRefreshClipStats.UseVisualStyleBackColor = true;
            this.btnRefreshClipStats.Click += new EventHandler(this.btnRefreshClipStats_Click);
            this.btnRefreshClipStats.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnImportClip.Location = new Point(430, 0xbb);
            this.btnImportClip.Name = "btnImportClip";
            this.btnImportClip.Size = new Size(0x94, 0x18);
            this.btnImportClip.TabIndex = 0;
            this.btnImportClip.Text = "Import Clipboard...";
            this.btnImportClip.Click += new EventHandler(this.btnImportClip_Click);
            this.btnImportClip.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.btnImportClip);
            base.Controls.Add(this.btnRefreshClipStats);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "IO_ImportClip";
            base.Size = new Size(0x248, 0xd6);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
        }
    }
}

