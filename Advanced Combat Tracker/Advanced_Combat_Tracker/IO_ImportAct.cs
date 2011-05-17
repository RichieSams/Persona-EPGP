namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    internal class IO_ImportAct : UserControl
    {
        internal Button btnImportAct;
        internal CheckBox cbParseCt;
        private IContainer components;
        internal GroupBox groupBox1;
        internal Label lblActImportStatus;

        public IO_ImportAct()
        {
            this.InitializeComponent();
        }

        private void btnImportAct_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.actFileThread = new Thread(new ThreadStart(ActGlobals.oFormActMain.ImportACT));
            ActGlobals.oFormActMain.actFileThread.Priority = ThreadPriority.Normal;
            ActGlobals.oFormActMain.actFileThread.Name = "ACT Import Thread";
            ActGlobals.oFormActMain.actFileThread.SetApartmentState(ApartmentState.STA);
            ActGlobals.oFormActMain.actFileThread.IsBackground = true;
            ActGlobals.oFormActMain.actFileThread.Start();
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
            this.lblActImportStatus = new Label();
            this.cbParseCt = new CheckBox();
            this.btnImportAct = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.lblActImportStatus);
            this.groupBox1.Location = new Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1e8, 0x61);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            this.groupBox1.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblActImportStatus.Dock = DockStyle.Fill;
            this.lblActImportStatus.Location = new Point(3, 0x10);
            this.lblActImportStatus.Name = "lblActImportStatus";
            this.lblActImportStatus.Size = new Size(0x1e2, 0x4e);
            this.lblActImportStatus.TabIndex = 0;
            this.lblActImportStatus.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbParseCt.AutoSize = true;
            this.cbParseCt.Location = new Point(3, 0x6a);
            this.cbParseCt.Name = "cbParseCt";
            this.cbParseCt.Size = new Size(0x1ba, 0x11);
            this.cbParseCt.TabIndex = 1;
            this.cbParseCt.Text = "When the imported file includes log text, parse it for Custom Triggers (tabbed results only)";
            this.cbParseCt.UseVisualStyleBackColor = true;
            this.cbParseCt.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnImportAct.Location = new Point(0x159, 0x81);
            this.btnImportAct.Name = "btnImportAct";
            this.btnImportAct.Size = new Size(0x92, 0x17);
            this.btnImportAct.TabIndex = 2;
            this.btnImportAct.Text = "Import ACT File...";
            this.btnImportAct.UseVisualStyleBackColor = true;
            this.btnImportAct.Click += new EventHandler(this.btnImportAct_Click);
            this.btnImportAct.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.btnImportAct);
            base.Controls.Add(this.cbParseCt);
            base.Controls.Add(this.groupBox1);
            base.Name = "IO_ImportAct";
            base.Size = new Size(0x1ee, 0x9b);
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

