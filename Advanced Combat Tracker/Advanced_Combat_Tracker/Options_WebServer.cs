namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    internal class Options_WebServer : UserControl
    {
        private Button btnEchoAll;
        private Button btnEchoRecent;
        internal CheckBox cbWebServerEnabled;
        internal CheckBox cbWebServerShowReq;
        private IContainer components;
        private GroupBox groupBox28;
        internal Label lblWebServerConnections;
        private Label lblWebServerPort;
        internal NumericUpDown nudWebServerPort;
        internal RichTextBox rtbWebServerLog;

        public Options_WebServer()
        {
            this.InitializeComponent();
        }

        private void btnEchoAll_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < FormActMain.ActWebConnection.TotalIPs.Count; i++)
            {
                builder.AppendFormat("{0} | ", FormActMain.ActWebConnection.TotalIPs[i]);
            }
            if (builder.Length != 0)
            {
                ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbWebServerLog, builder.ToString(0, builder.Length - 3));
            }
        }

        private void btnEchoRecent_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < FormActMain.ActWebConnection.LastIPs.Count; i++)
            {
                builder.AppendFormat("{0} | ", FormActMain.ActWebConnection.LastIPs[i]);
            }
            if (builder.Length != 0)
            {
                ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbWebServerLog, builder.ToString(0, builder.Length - 3));
            }
        }

        private void cbWebServerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.cbTimersServerEnabled_CheckedChanged();
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
            this.groupBox28 = new GroupBox();
            this.btnEchoRecent = new Button();
            this.btnEchoAll = new Button();
            this.cbWebServerShowReq = new CheckBox();
            this.rtbWebServerLog = new RichTextBox();
            this.lblWebServerPort = new Label();
            this.cbWebServerEnabled = new CheckBox();
            this.lblWebServerConnections = new Label();
            this.nudWebServerPort = new NumericUpDown();
            this.groupBox28.SuspendLayout();
            this.nudWebServerPort.BeginInit();
            base.SuspendLayout();
            this.groupBox28.Controls.Add(this.btnEchoRecent);
            this.groupBox28.Controls.Add(this.btnEchoAll);
            this.groupBox28.Controls.Add(this.cbWebServerShowReq);
            this.groupBox28.Controls.Add(this.rtbWebServerLog);
            this.groupBox28.Controls.Add(this.lblWebServerPort);
            this.groupBox28.Controls.Add(this.cbWebServerEnabled);
            this.groupBox28.Controls.Add(this.lblWebServerConnections);
            this.groupBox28.Controls.Add(this.nudWebServerPort);
            this.groupBox28.Location = new Point(3, 3);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new Size(0x2c3, 0xa7);
            this.groupBox28.TabIndex = 3;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "HTML Interface Web Server";
            this.btnEchoRecent.Location = new Point(0x1e1, 0x87);
            this.btnEchoRecent.Name = "btnEchoRecent";
            this.btnEchoRecent.Size = new Size(0xd6, 0x17);
            this.btnEchoRecent.TabIndex = 5;
            this.btnEchoRecent.Text = "Echo All Recent Clients";
            this.btnEchoRecent.UseVisualStyleBackColor = true;
            this.btnEchoRecent.Click += new EventHandler(this.btnEchoRecent_Click);
            this.btnEchoRecent.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnEchoAll.Location = new Point(0x1e1, 0x6a);
            this.btnEchoAll.Name = "btnEchoAll";
            this.btnEchoAll.Size = new Size(0xd6, 0x17);
            this.btnEchoAll.TabIndex = 5;
            this.btnEchoAll.Text = "Echo All Clients";
            this.btnEchoAll.UseVisualStyleBackColor = true;
            this.btnEchoAll.Click += new EventHandler(this.btnEchoAll_Click);
            this.btnEchoAll.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbWebServerShowReq.AutoSize = true;
            this.cbWebServerShowReq.Location = new Point(0x1e1, 20);
            this.cbWebServerShowReq.Name = "cbWebServerShowReq";
            this.cbWebServerShowReq.Size = new Size(0x8d, 0x11);
            this.cbWebServerShowReq.TabIndex = 4;
            this.cbWebServerShowReq.Text = "Show all HTTP requests";
            this.cbWebServerShowReq.UseVisualStyleBackColor = true;
            this.cbWebServerShowReq.MouseHover += new EventHandler(this.control_MouseHover);
            this.rtbWebServerLog.BackColor = SystemColors.Control;
            this.rtbWebServerLog.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.rtbWebServerLog.Location = new Point(8, 0x2c);
            this.rtbWebServerLog.Name = "rtbWebServerLog";
            this.rtbWebServerLog.ReadOnly = true;
            this.rtbWebServerLog.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            this.rtbWebServerLog.Size = new Size(0x1d3, 0x72);
            this.rtbWebServerLog.TabIndex = 3;
            this.rtbWebServerLog.Text = "";
            this.rtbWebServerLog.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblWebServerPort.AutoSize = true;
            this.lblWebServerPort.Location = new Point(0xd0, 0x15);
            this.lblWebServerPort.Name = "lblWebServerPort";
            this.lblWebServerPort.Size = new Size(0x6c, 13);
            this.lblWebServerPort.TabIndex = 1;
            this.lblWebServerPort.Text = "Server Listening Port:";
            this.lblWebServerPort.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbWebServerEnabled.AutoSize = true;
            this.cbWebServerEnabled.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.cbWebServerEnabled.Location = new Point(8, 20);
            this.cbWebServerEnabled.Name = "cbWebServerEnabled";
            this.cbWebServerEnabled.Size = new Size(0x88, 0x11);
            this.cbWebServerEnabled.TabIndex = 0;
            this.cbWebServerEnabled.Text = "Enable Web Server";
            this.cbWebServerEnabled.UseVisualStyleBackColor = true;
            this.cbWebServerEnabled.CheckedChanged += new EventHandler(this.cbWebServerEnabled_CheckedChanged);
            this.cbWebServerEnabled.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblWebServerConnections.BorderStyle = BorderStyle.Fixed3D;
            this.lblWebServerConnections.Location = new Point(0x1e1, 0x2c);
            this.lblWebServerConnections.Name = "lblWebServerConnections";
            this.lblWebServerConnections.Size = new Size(0xd6, 0x3b);
            this.lblWebServerConnections.TabIndex = 2;
            this.lblWebServerConnections.Text = "Server Status";
            this.lblWebServerConnections.MouseHover += new EventHandler(this.control_MouseHover);
            this.nudWebServerPort.Location = new Point(340, 0x12);
            int[] bits = new int[4];
            bits[0] = 0xffff;
            this.nudWebServerPort.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.nudWebServerPort.Minimum = new decimal(numArray2);
            this.nudWebServerPort.Name = "nudWebServerPort";
            this.nudWebServerPort.Size = new Size(0x3e, 20);
            this.nudWebServerPort.TabIndex = 2;
            int[] numArray3 = new int[4];
            numArray3[0] = 80;
            this.nudWebServerPort.Value = new decimal(numArray3);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.groupBox28);
            base.Name = "Options_WebServer";
            base.Size = new Size(0x2c9, 0xad);
            this.groupBox28.ResumeLayout(false);
            this.groupBox28.PerformLayout();
            this.nudWebServerPort.EndInit();
            base.ResumeLayout(false);
        }
    }
}

