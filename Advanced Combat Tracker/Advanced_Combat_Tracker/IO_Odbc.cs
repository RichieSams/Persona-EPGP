namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    internal class IO_Odbc : UserControl
    {
        private Button btnCancel;
        private Button btnExportOdbc;
        private Button btnShowSqlWindow;
        private IContainer components;
        private Thread exportThread;
        internal volatile bool exportThreadAlive;
        private GroupBox groupBox1;
        internal Label lblOdbcStatus;
        private System.Windows.Forms.Timer timer500;

        public IO_Odbc()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.exportThread.Abort();
            }
            catch
            {
            }
        }

        private void btnExportOdbc_Click(object sender, EventArgs e)
        {
            this.exportThread = new Thread(new ThreadStart(ActGlobals.oFormActMain.ThreadOdbcExportBatch));
            this.exportThread.IsBackground = true;
            this.exportThread.Priority = ThreadPriority.Normal;
            this.exportThread.Name = "HTML Export Thread";
            this.exportThread.SetApartmentState(ApartmentState.STA);
            this.exportThread.Start();
        }

        private void btnShowSqlWindow_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormSqlQuery.ShowSqlView(ActGlobals.oFormActMain.opOdbc.tbOdbcConnectionString.Text);
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
            this.groupBox1 = new GroupBox();
            this.btnShowSqlWindow = new Button();
            this.btnExportOdbc = new Button();
            this.lblOdbcStatus = new Label();
            this.timer500 = new System.Windows.Forms.Timer(this.components);
            this.btnCancel = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.lblOdbcStatus);
            this.groupBox1.Location = new Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1c8, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            this.btnShowSqlWindow.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnShowSqlWindow.Location = new Point(0xb5, 0x6d);
            this.btnShowSqlWindow.Name = "btnShowSqlWindow";
            this.btnShowSqlWindow.Size = new Size(130, 0x16);
            this.btnShowSqlWindow.TabIndex = 3;
            this.btnShowSqlWindow.Text = "View SQL Window";
            this.btnShowSqlWindow.Click += new EventHandler(this.btnShowSqlWindow_Click);
            this.btnExportOdbc.Location = new Point(0x13d, 0x6d);
            this.btnExportOdbc.Name = "btnExportOdbc";
            this.btnExportOdbc.Size = new Size(0x8e, 0x16);
            this.btnExportOdbc.TabIndex = 2;
            this.btnExportOdbc.Text = "Export encounter(s)";
            this.btnExportOdbc.Click += new EventHandler(this.btnExportOdbc_Click);
            this.lblOdbcStatus.Dock = DockStyle.Fill;
            this.lblOdbcStatus.Location = new Point(3, 0x10);
            this.lblOdbcStatus.Name = "lblOdbcStatus";
            this.lblOdbcStatus.Size = new Size(450, 0x51);
            this.lblOdbcStatus.TabIndex = 0;
            this.timer500.Enabled = true;
            this.timer500.Interval = 500;
            this.timer500.Tick += new EventHandler(this.timer500_Tick);
            this.btnCancel.Location = new Point(0x177, 0x47);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.btnShowSqlWindow);
            base.Controls.Add(this.btnExportOdbc);
            base.Controls.Add(this.groupBox1);
            base.Name = "IO_Odbc";
            base.Size = new Size(0x1ce, 0x86);
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void timer500_Tick(object sender, EventArgs e)
        {
            this.btnCancel.Visible = this.exportThreadAlive;
        }
    }
}

