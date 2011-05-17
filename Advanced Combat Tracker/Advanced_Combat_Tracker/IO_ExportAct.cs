namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    internal class IO_ExportAct : UserControl
    {
        internal Button btnExportAct;
        internal CheckBox cbExportLogText;
        private IContainer components;
        internal GroupBox groupBox1;
        internal Label lblActExportStatus;

        public IO_ExportAct()
        {
            this.InitializeComponent();
        }

        private void btnExportAct_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.actFileThread = new Thread(new ThreadStart(ActGlobals.oFormActMain.ExportACT));
            ActGlobals.oFormActMain.actFileThread.Priority = ThreadPriority.Normal;
            ActGlobals.oFormActMain.actFileThread.Name = "ACT Export Thread";
            ActGlobals.oFormActMain.actFileThread.SetApartmentState(ApartmentState.STA);
            ActGlobals.oFormActMain.actFileThread.IsBackground = true;
            ActGlobals.oFormActMain.actFileThread.Start();
        }

        private void btnExportAct_MouseHover(object sender, EventArgs e)
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
            this.cbExportLogText = new CheckBox();
            this.groupBox1 = new GroupBox();
            this.lblActExportStatus = new Label();
            this.btnExportAct = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.cbExportLogText.AutoSize = true;
            this.cbExportLogText.Checked = true;
            this.cbExportLogText.CheckState = CheckState.Checked;
            this.cbExportLogText.Location = new Point(3, 0x6a);
            this.cbExportLogText.Name = "cbExportLogText";
            this.cbExportLogText.Size = new Size(0x15b, 0x11);
            this.cbExportLogText.TabIndex = 4;
            this.cbExportLogText.Text = "When exporting, include the log lines found in the View Logs feature";
            this.cbExportLogText.UseVisualStyleBackColor = true;
            this.cbExportLogText.MouseHover += new EventHandler(this.btnExportAct_MouseHover);
            this.groupBox1.Controls.Add(this.lblActExportStatus);
            this.groupBox1.Location = new Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1e8, 0x61);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            this.groupBox1.MouseHover += new EventHandler(this.btnExportAct_MouseHover);
            this.lblActExportStatus.Dock = DockStyle.Fill;
            this.lblActExportStatus.Location = new Point(3, 0x10);
            this.lblActExportStatus.Name = "lblActExportStatus";
            this.lblActExportStatus.Size = new Size(0x1e2, 0x4e);
            this.lblActExportStatus.TabIndex = 0;
            this.lblActExportStatus.MouseHover += new EventHandler(this.btnExportAct_MouseHover);
            this.btnExportAct.Location = new Point(0x159, 0x81);
            this.btnExportAct.Name = "btnExportAct";
            this.btnExportAct.Size = new Size(0x92, 0x17);
            this.btnExportAct.TabIndex = 5;
            this.btnExportAct.Text = "Export ACT File...";
            this.btnExportAct.UseVisualStyleBackColor = true;
            this.btnExportAct.Click += new EventHandler(this.btnExportAct_Click);
            this.btnExportAct.MouseHover += new EventHandler(this.btnExportAct_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.cbExportLogText);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnExportAct);
            base.Name = "IO_ExportAct";
            base.Size = new Size(0x1ee, 0x9b);
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

