namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Security;
    using System.Windows.Forms;

    internal class Options_Misc : UserControl
    {
        private Button btnCloseLog;
        private Button btnCpuAffinityApply;
        private Button btnOpenLog;
        private Button btnShowPerfWiz;
        private Button btnShowStartupWiz;
        internal CheckBox cbAutoLoadLogs;
        internal CheckBox cbbPauseLog;
        internal CheckBox cbClipAutoConnect;
        internal CheckBox cbClipConnect;
        internal CheckBox cbGCollectOnClear;
        internal CheckBox cbMinimizeToIcon;
        internal CheckBox cbRecordLogs;
        internal CheckBox cbRestrictToAll;
        internal CheckBox cbZoneAllListing;
        private IContainer components;
        internal ComboBox ddlCpuAffinity;
        internal ComboBox ddlLogPriority;
        internal GroupBox gbFile;
        private GroupBox groupBox11;
        private Label label17;
        private Label label68;
        private Label label69;
        internal Label lblClipStatus;
        internal Label lblLogFile;
        private Label lblLogPriority;
        private Label lblSplitFile;
        internal NumericUpDown nudLogFileSplit;
        internal TextBox tbClipIP;
        private Dictionary<string, LocalizationObject> Trans = ActGlobals.ActLocalization.LocalizationStrings;

        public Options_Misc()
        {
            this.InitializeComponent();
        }

        private void btnCloseLog_Click(object sender, EventArgs e)
        {
            try
            {
                ActGlobals.oFormActMain.SetCharName(false);
                ActGlobals.oFormActMain.readThreadAborting = true;
                ActGlobals.oFormActMain.LogFilePath = string.Empty;
            }
            catch
            {
            }
        }

        internal void btnCpuAffinityApply_Click(object sender, EventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            FormActMain.ProcDef selectedItem = (FormActMain.ProcDef) this.ddlCpuAffinity.SelectedItem;
            if (selectedItem.ProcBitfield != IntPtr.Zero)
            {
                currentProcess.ProcessorAffinity = selectedItem.ProcBitfield;
            }
        }

        private void btnOpenLog_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult cancel = DialogResult.Cancel;
            dialog.Filter = "Game Log Files|" + ActGlobals.oFormActMain.LogFileFilter + "|Text Files (*.txt)|*.txt|Log Files (*.log)|*.log|Any File (*.*)|*.*";
            try
            {
                if ((ActGlobals.oFormActMain.folderLogs != null) && ActGlobals.oFormActMain.folderLogs.Exists)
                {
                    dialog.InitialDirectory = ActGlobals.oFormActMain.folderLogs.FullName;
                }
                cancel = dialog.ShowDialog();
            }
            catch (SecurityException exception)
            {
                MessageBox.Show(this.Trans["messageBoxTitle-localSecurityPolicy"].DisplayedText + exception.Message, this.Trans["messageBoxTitle-localSecurityPolicy"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                ActGlobals.oFormActMain.WriteExceptionLog(exception, "SecurityException");
                return;
            }
            if (cancel == DialogResult.OK)
            {
                ActGlobals.oFormActMain.LogFilePath = dialog.FileName;
                ActGlobals.oFormActMain.OpenLog(true, true);
                this.gbFile.BackColor = SystemColors.Control;
            }
        }

        private void btnShowPerfWiz_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormPerformanceWizard.ShowWizard();
        }

        private void btnShowStartupWiz_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormStartupWizard.Show();
        }

        private void cbAutoLoad_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.ResetCheckLogs();
        }

        private void cbbPauseLog_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.ReadThreadLock = this.cbbPauseLog.Checked;
            ActGlobals.oFormActMain.readThreadDataAvailable = true;
        }

        private void cbClipAutoConnect_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbClipAutoConnect.Checked)
            {
                this.cbClipConnect.Checked = true;
            }
        }

        private void cbClipConnect_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.cbClipConnect_CheckedChanged();
        }

        private void cbMinimizeToIcon_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.notifyIcon1.Visible = this.cbMinimizeToIcon.Checked;
        }

        private void cbRestrictToAll_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.restrictToAll = this.cbRestrictToAll.Checked;
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Options_Misc));
            this.cbRestrictToAll = new CheckBox();
            this.cbRecordLogs = new CheckBox();
            this.cbMinimizeToIcon = new CheckBox();
            this.ddlCpuAffinity = new ComboBox();
            this.label69 = new Label();
            this.lblLogPriority = new Label();
            this.ddlLogPriority = new ComboBox();
            this.btnCpuAffinityApply = new Button();
            this.cbZoneAllListing = new CheckBox();
            this.btnShowPerfWiz = new Button();
            this.btnShowStartupWiz = new Button();
            this.cbGCollectOnClear = new CheckBox();
            this.gbFile = new GroupBox();
            this.cbbPauseLog = new CheckBox();
            this.nudLogFileSplit = new NumericUpDown();
            this.lblSplitFile = new Label();
            this.cbAutoLoadLogs = new CheckBox();
            this.lblLogFile = new Label();
            this.btnCloseLog = new Button();
            this.btnOpenLog = new Button();
            this.groupBox11 = new GroupBox();
            this.lblClipStatus = new Label();
            this.cbClipAutoConnect = new CheckBox();
            this.tbClipIP = new TextBox();
            this.cbClipConnect = new CheckBox();
            this.label68 = new Label();
            this.label17 = new Label();
            this.gbFile.SuspendLayout();
            this.nudLogFileSplit.BeginInit();
            this.groupBox11.SuspendLayout();
            base.SuspendLayout();
            this.cbRestrictToAll.AutoSize = true;
            this.cbRestrictToAll.Location = new Point(9, 0xf1);
            this.cbRestrictToAll.Margin = new Padding(3, 1, 3, 1);
            this.cbRestrictToAll.Name = "cbRestrictToAll";
            this.cbRestrictToAll.Size = new Size(500, 0x11);
            this.cbRestrictToAll.TabIndex = 7;
            this.cbRestrictToAll.Text = "Only populate the 'All' entry for DamageType categories except those marked (Ref).  (Not retroactive)";
            this.cbRestrictToAll.CheckedChanged += new EventHandler(this.cbRestrictToAll_CheckedChanged);
            this.cbRestrictToAll.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbRecordLogs.AutoSize = true;
            this.cbRecordLogs.Checked = true;
            this.cbRecordLogs.CheckState = CheckState.Checked;
            this.cbRecordLogs.Location = new Point(9, 0x117);
            this.cbRecordLogs.Margin = new Padding(3, 1, 3, 1);
            this.cbRecordLogs.Name = "cbRecordLogs";
            this.cbRecordLogs.Size = new Size(0x156, 0x11);
            this.cbRecordLogs.TabIndex = 0x20;
            this.cbRecordLogs.Text = "Record all log lines while parsing.  (View Logs context menu option)";
            this.cbRecordLogs.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbMinimizeToIcon.AutoSize = true;
            this.cbMinimizeToIcon.Location = new Point(9, 0x12a);
            this.cbMinimizeToIcon.Margin = new Padding(3, 1, 3, 1);
            this.cbMinimizeToIcon.Name = "cbMinimizeToIcon";
            this.cbMinimizeToIcon.Size = new Size(0x9d, 0x11);
            this.cbMinimizeToIcon.TabIndex = 0x21;
            this.cbMinimizeToIcon.Text = "Minimize ACT to a tray icon.";
            this.cbMinimizeToIcon.CheckedChanged += new EventHandler(this.cbMinimizeToIcon_CheckedChanged);
            this.cbMinimizeToIcon.MouseHover += new EventHandler(this.control_MouseHover);
            this.ddlCpuAffinity.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlCpuAffinity.FormattingEnabled = true;
            this.ddlCpuAffinity.Location = new Point(0x8a, 0xcf);
            this.ddlCpuAffinity.Name = "ddlCpuAffinity";
            this.ddlCpuAffinity.Size = new Size(180, 0x15);
            this.ddlCpuAffinity.TabIndex = 0x24;
            this.ddlCpuAffinity.MouseHover += new EventHandler(this.control_MouseHover);
            this.label69.AutoSize = true;
            this.label69.Location = new Point(6, 210);
            this.label69.Name = "label69";
            this.label69.Size = new Size(0x6b, 13);
            this.label69.TabIndex = 0x27;
            this.label69.Text = "Process CPU Affinity:";
            this.label69.TextAlign = ContentAlignment.MiddleLeft;
            this.label69.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblLogPriority.AutoSize = true;
            this.lblLogPriority.Location = new Point(6, 0xb7);
            this.lblLogPriority.Name = "lblLogPriority";
            this.lblLogPriority.Size = new Size(0xa6, 13);
            this.lblLogPriority.TabIndex = 0x26;
            this.lblLogPriority.Text = "CPU Priority of normal log parsing:";
            this.lblLogPriority.TextAlign = ContentAlignment.MiddleLeft;
            this.lblLogPriority.MouseHover += new EventHandler(this.control_MouseHover);
            this.ddlLogPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlLogPriority.Items.AddRange(new object[] { "Above Normal", "Normal", "Below Normal", "Lowest" });
            this.ddlLogPriority.Location = new Point(0xc1, 180);
            this.ddlLogPriority.Name = "ddlLogPriority";
            this.ddlLogPriority.Size = new Size(0xb7, 0x15);
            this.ddlLogPriority.TabIndex = 0x23;
            this.ddlLogPriority.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnCpuAffinityApply.Location = new Point(0x144, 0xcf);
            this.btnCpuAffinityApply.Name = "btnCpuAffinityApply";
            this.btnCpuAffinityApply.Size = new Size(0x33, 0x16);
            this.btnCpuAffinityApply.TabIndex = 0x25;
            this.btnCpuAffinityApply.Text = "Apply";
            this.btnCpuAffinityApply.UseVisualStyleBackColor = true;
            this.btnCpuAffinityApply.Click += new EventHandler(this.btnCpuAffinityApply_Click);
            this.btnCpuAffinityApply.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbZoneAllListing.AutoSize = true;
            this.cbZoneAllListing.Checked = true;
            this.cbZoneAllListing.CheckState = CheckState.Checked;
            this.cbZoneAllListing.Location = new Point(9, 260);
            this.cbZoneAllListing.Margin = new Padding(3, 1, 3, 1);
            this.cbZoneAllListing.Name = "cbZoneAllListing";
            this.cbZoneAllListing.Size = new Size(0x217, 0x11);
            this.cbZoneAllListing.TabIndex = 7;
            this.cbZoneAllListing.Text = "Populate an 'All' encounter which includes all data from separate encounters within a zone.  (Not retroactive)";
            this.cbZoneAllListing.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnShowPerfWiz.Location = new Point(0xd0, 3);
            this.btnShowPerfWiz.Name = "btnShowPerfWiz";
            this.btnShowPerfWiz.Size = new Size(0xc7, 0x17);
            this.btnShowPerfWiz.TabIndex = 0x29;
            this.btnShowPerfWiz.Text = "Show Performance Wizard";
            this.btnShowPerfWiz.UseVisualStyleBackColor = true;
            this.btnShowPerfWiz.Click += new EventHandler(this.btnShowPerfWiz_Click);
            this.btnShowPerfWiz.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnShowStartupWiz.Location = new Point(3, 3);
            this.btnShowStartupWiz.Name = "btnShowStartupWiz";
            this.btnShowStartupWiz.Size = new Size(0xc7, 0x17);
            this.btnShowStartupWiz.TabIndex = 40;
            this.btnShowStartupWiz.Text = "Show Startup Wizard";
            this.btnShowStartupWiz.UseVisualStyleBackColor = true;
            this.btnShowStartupWiz.Click += new EventHandler(this.btnShowStartupWiz_Click);
            this.btnShowStartupWiz.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbGCollectOnClear.AutoSize = true;
            this.cbGCollectOnClear.Location = new Point(9, 0x13d);
            this.cbGCollectOnClear.Margin = new Padding(3, 1, 3, 1);
            this.cbGCollectOnClear.Name = "cbGCollectOnClear";
            this.cbGCollectOnClear.Size = new Size(0x1f5, 0x11);
            this.cbGCollectOnClear.TabIndex = 0x2a;
            this.cbGCollectOnClear.Text = "Immediately attempt to free all unused memory from RAM when clearing encounters (may freeze ACT)";
            this.cbGCollectOnClear.UseVisualStyleBackColor = true;
            this.cbGCollectOnClear.MouseHover += new EventHandler(this.control_MouseHover);
            this.gbFile.Controls.Add(this.cbbPauseLog);
            this.gbFile.Controls.Add(this.nudLogFileSplit);
            this.gbFile.Controls.Add(this.lblSplitFile);
            this.gbFile.Controls.Add(this.cbAutoLoadLogs);
            this.gbFile.Controls.Add(this.lblLogFile);
            this.gbFile.Controls.Add(this.btnCloseLog);
            this.gbFile.Controls.Add(this.btnOpenLog);
            this.gbFile.Location = new Point(3, 0x20);
            this.gbFile.Name = "gbFile";
            this.gbFile.Size = new Size(0x217, 0x88);
            this.gbFile.TabIndex = 0x2b;
            this.gbFile.TabStop = false;
            this.gbFile.Text = "Log File";
            this.cbbPauseLog.Appearance = Appearance.Button;
            this.cbbPauseLog.FlatStyle = FlatStyle.System;
            this.cbbPauseLog.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cbbPauseLog.Location = new Point(0x54, 0x43);
            this.cbbPauseLog.Name = "cbbPauseLog";
            this.cbbPauseLog.Size = new Size(0x48, 0x13);
            this.cbbPauseLog.TabIndex = 1;
            this.cbbPauseLog.Text = "Pause Log";
            this.cbbPauseLog.TextAlign = ContentAlignment.BottomCenter;
            this.cbbPauseLog.UseVisualStyleBackColor = false;
            this.cbbPauseLog.CheckedChanged += new EventHandler(this.cbbPauseLog_CheckedChanged);
            this.cbbPauseLog.MouseHover += new EventHandler(this.control_MouseHover);
            this.nudLogFileSplit.Location = new Point(0x1e1, 110);
            int[] bits = new int[4];
            bits[0] = 0x400;
            this.nudLogFileSplit.Maximum = new decimal(bits);
            this.nudLogFileSplit.Name = "nudLogFileSplit";
            this.nudLogFileSplit.Size = new Size(0x30, 20);
            this.nudLogFileSplit.TabIndex = 4;
            int[] numArray2 = new int[4];
            numArray2[0] = 0x40;
            this.nudLogFileSplit.Value = new decimal(numArray2);
            this.nudLogFileSplit.ValueChanged += new EventHandler(this.nudLogFileSplit_ValueChanged);
            this.lblSplitFile.AutoSize = true;
            this.lblSplitFile.Location = new Point(6, 0x70);
            this.lblSplitFile.Name = "lblSplitFile";
            this.lblSplitFile.Size = new Size(0x1bb, 13);
            this.lblSplitFile.TabIndex = 4;
            this.lblSplitFile.Text = "When opening or closing log files, attempt to rename the log if over this value (in megabytes):";
            this.lblSplitFile.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbAutoLoadLogs.AutoSize = true;
            this.cbAutoLoadLogs.Checked = true;
            this.cbAutoLoadLogs.CheckState = CheckState.Checked;
            this.cbAutoLoadLogs.Location = new Point(6, 0x5c);
            this.cbAutoLoadLogs.Name = "cbAutoLoadLogs";
            this.cbAutoLoadLogs.Size = new Size(0xc5, 0x11);
            this.cbAutoLoadLogs.TabIndex = 3;
            this.cbAutoLoadLogs.Text = "Auto-load recently changed log files.";
            this.cbAutoLoadLogs.CheckedChanged += new EventHandler(this.cbAutoLoad_CheckedChanged);
            this.cbAutoLoadLogs.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblLogFile.BorderStyle = BorderStyle.Fixed3D;
            this.lblLogFile.Location = new Point(8, 0x10);
            this.lblLogFile.Name = "lblLogFile";
            this.lblLogFile.Padding = new Padding(2);
            this.lblLogFile.Size = new Size(0x209, 0x30);
            this.lblLogFile.TabIndex = 3;
            this.lblLogFile.Text = "No log file selected.  (Remember to turn /log ON)";
            this.lblLogFile.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnCloseLog.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnCloseLog.Location = new Point(0xa2, 0x43);
            this.btnCloseLog.Name = "btnCloseLog";
            this.btnCloseLog.Size = new Size(0x48, 0x13);
            this.btnCloseLog.TabIndex = 2;
            this.btnCloseLog.Text = "Close Log";
            this.btnCloseLog.Click += new EventHandler(this.btnCloseLog_Click);
            this.btnCloseLog.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnOpenLog.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnOpenLog.Location = new Point(6, 0x43);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new Size(0x48, 0x13);
            this.btnOpenLog.TabIndex = 0;
            this.btnOpenLog.Text = "Open Log";
            this.btnOpenLog.Click += new EventHandler(this.btnOpenLog_Click);
            this.btnOpenLog.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox11.Controls.Add(this.lblClipStatus);
            this.groupBox11.Controls.Add(this.cbClipAutoConnect);
            this.groupBox11.Controls.Add(this.tbClipIP);
            this.groupBox11.Controls.Add(this.cbClipConnect);
            this.groupBox11.Controls.Add(this.label68);
            this.groupBox11.Controls.Add(this.label17);
            this.groupBox11.Location = new Point(3, 0x165);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new Size(0x217, 0x83);
            this.groupBox11.TabIndex = 0x2c;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Cross Computer Clipboard Sharing";
            this.lblClipStatus.BorderStyle = BorderStyle.Fixed3D;
            this.lblClipStatus.Font = new Font("Microsoft Sans Serif", 8f);
            this.lblClipStatus.Location = new Point(6, 0x59);
            this.lblClipStatus.Name = "lblClipStatus";
            this.lblClipStatus.Size = new Size(0x120, 0x24);
            this.lblClipStatus.TabIndex = 5;
            this.lblClipStatus.Text = "Not Connected.";
            this.lblClipStatus.TextAlign = ContentAlignment.MiddleCenter;
            this.lblClipStatus.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbClipAutoConnect.Location = new Point(0x12e, 0x59);
            this.cbClipAutoConnect.Name = "cbClipAutoConnect";
            this.cbClipAutoConnect.Size = new Size(0xc0, 0x10);
            this.cbClipAutoConnect.TabIndex = 0;
            this.cbClipAutoConnect.Text = "Connect to this IP automatically.";
            this.cbClipAutoConnect.CheckedChanged += new EventHandler(this.cbClipAutoConnect_CheckedChanged);
            this.cbClipAutoConnect.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbClipIP.Location = new Point(0x12e, 0x69);
            this.tbClipIP.Name = "tbClipIP";
            this.tbClipIP.Size = new Size(0x8a, 20);
            this.tbClipIP.TabIndex = 1;
            this.tbClipIP.Text = "192.168.0.2";
            this.tbClipIP.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbClipConnect.CheckAlign = ContentAlignment.MiddleRight;
            this.cbClipConnect.Location = new Point(0x1be, 0x69);
            this.cbClipConnect.Name = "cbClipConnect";
            this.cbClipConnect.Size = new Size(0x51, 0x15);
            this.cbClipConnect.TabIndex = 2;
            this.cbClipConnect.Text = "Connect";
            this.cbClipConnect.TextAlign = ContentAlignment.MiddleRight;
            this.cbClipConnect.CheckedChanged += new EventHandler(this.cbClipConnect_CheckedChanged);
            this.cbClipConnect.MouseHover += new EventHandler(this.control_MouseHover);
            this.label68.Location = new Point(8, 0x1d);
            this.label68.Name = "label68";
            this.label68.Size = new Size(0x209, 0x39);
            this.label68.TabIndex = 6;
            this.label68.Text = manager.GetString("label68.Text");
            this.label68.MouseHover += new EventHandler(this.control_MouseHover);
            this.label17.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label17.ForeColor = Color.DarkRed;
            this.label17.Location = new Point(7, 15);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x1e9, 0x11);
            this.label17.TabIndex = 6;
            this.label17.Text = "This feature is for users running ACT on a different machine than the game.";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.groupBox11);
            base.Controls.Add(this.gbFile);
            base.Controls.Add(this.cbGCollectOnClear);
            base.Controls.Add(this.btnShowPerfWiz);
            base.Controls.Add(this.btnShowStartupWiz);
            base.Controls.Add(this.ddlCpuAffinity);
            base.Controls.Add(this.label69);
            base.Controls.Add(this.lblLogPriority);
            base.Controls.Add(this.ddlLogPriority);
            base.Controls.Add(this.btnCpuAffinityApply);
            base.Controls.Add(this.cbMinimizeToIcon);
            base.Controls.Add(this.cbRecordLogs);
            base.Controls.Add(this.cbZoneAllListing);
            base.Controls.Add(this.cbRestrictToAll);
            base.Name = "Options_Misc";
            base.Size = new Size(0x223, 0x1eb);
            this.gbFile.ResumeLayout(false);
            this.gbFile.PerformLayout();
            this.nudLogFileSplit.EndInit();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void nudLogFileSplit_ValueChanged(object sender, EventArgs e)
        {
            if (this.nudLogFileSplit.Value == 0M)
            {
                this.lblSplitFile.Enabled = false;
            }
            else
            {
                this.lblSplitFile.Enabled = true;
            }
        }
    }
}

