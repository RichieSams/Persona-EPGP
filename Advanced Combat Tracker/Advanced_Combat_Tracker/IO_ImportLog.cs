namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Security;
    using System.Threading;
    using System.Windows.Forms;

    internal class IO_ImportLog : UserControl
    {
        internal Button btnImportFile;
        internal MonthCalendar calImportFrom;
        internal MonthCalendar calImportTo;
        internal CheckBox cbContToEndCombat;
        internal CheckBox cbImportCustomTriggers;
        private IContainer components;
        internal DateTimePicker dtImportFromTime;
        internal DateTimePicker dtImportToTime;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label lblImportBFile;
        private Label lblImportEFile;
        internal RadioButton rbImportFromBegining;
        internal RadioButton rbImportFromDateTime;
        internal RadioButton rbImportToDateTime;
        internal RadioButton rbImportToEnd;
        private Dictionary<string, LocalizationObject> Trans = ActGlobals.ActLocalization.LocalizationStrings;

        public IO_ImportLog()
        {
            this.InitializeComponent();
        }

        private void btnImportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "Game Log Files|" + ActGlobals.oFormActMain.LogFileFilter + "|Text Files (*.txt)|*.txt|Log Files (*.log)|*.log|Any File (*.*)|*.*"
            };
            DialogResult cancel = DialogResult.Cancel;
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
                ActGlobals.oFormActMain.WriteExceptionLog(exception, this.Trans["messageBoxTitle-localSecurityPolicy"].DisplayedText);
                return;
            }
            if (cancel == DialogResult.OK)
            {
                ActGlobals.oFormActMain.folderLogs = new DirectoryInfo(Path.GetDirectoryName(dialog.FileName));
                if (ActGlobals.oFormActMain.CharacterFileNameRegex.IsMatch(dialog.FileName) && ActGlobals.oFormActMain.LogPathHasCharName)
                {
                    ActGlobals.oFormActMain.opDataCorrectionMisc.tbCharName.Text = ActGlobals.oFormActMain.CharacterFileNameRegex.Replace(dialog.FileName, "$1");
                }
                ThreadInvokes.ControlSetVisible(ActGlobals.oFormImportProgress, ActGlobals.oFormImportProgress.gbPov, true);
                ThreadInvokes.ControlSetText(ActGlobals.oFormImportProgress, ActGlobals.oFormImportProgress.btnLastKnownName, ActGlobals.charName);
                ThreadInvokes.ControlSetText(ActGlobals.oFormImportProgress, ActGlobals.oFormImportProgress.tbLastKnownName, ActGlobals.oFormActMain.opDataCorrectionMisc.tbCharName.Text);
                ThreadInvokes.ControlSetVisible(ActGlobals.oFormImportProgress, ActGlobals.oFormImportProgress, true);
                ActGlobals.oFormImportProgress.ResetBatchStats();
                ActGlobals.oFormImportProgress.SetBatch(0, 1);
                ActGlobals.oFormImportProgress.SetProgress(-1);
                Application.DoEvents();
                ActGlobals.oFormActMain.importStream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                ActGlobals.oFormActMain.importStream.Seek(0, SeekOrigin.Begin);
                Application.DoEvents();
                ActGlobals.oFormActMain.importThread = new Thread(new ThreadStart(ActGlobals.oFormActMain.ImportFile));
                ActGlobals.oFormActMain.importThread.Name = "File Importer";
                ActGlobals.oFormActMain.importThread.IsBackground = true;
                ActGlobals.oFormActMain.importThread.SetApartmentState(ApartmentState.STA);
                ActGlobals.oFormActMain.importThread.Start();
            }
        }

        private void calImportFrom_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (this.calImportFrom.SelectionStart > this.calImportTo.SelectionStart)
            {
                this.calImportTo.SelectionStart = this.calImportFrom.SelectionStart;
            }
            this.rbImportFromDateTime.Checked = true;
            if (this.rbImportToDateTime.Checked)
            {
                this.calImportTo.SelectionStart = this.calImportFrom.SelectionStart;
                this.dtImportToTime.Value = this.dtImportFromTime.Value;
            }
        }

        private void calImportTo_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (this.calImportFrom.SelectionStart > this.calImportTo.SelectionStart)
            {
                this.calImportFrom.SelectionStart = this.calImportTo.SelectionStart;
            }
            this.rbImportToDateTime.Checked = true;
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

        private void dtImportFromTime_ValueChanged(object sender, EventArgs e)
        {
            this.rbImportFromDateTime.Checked = true;
            DateTime time = new DateTime(this.calImportFrom.SelectionStart.Year, this.calImportFrom.SelectionStart.Month, this.calImportFrom.SelectionStart.Day, this.dtImportFromTime.Value.Hour, this.dtImportFromTime.Value.Minute, this.dtImportFromTime.Value.Second);
            DateTime time2 = new DateTime(this.calImportTo.SelectionStart.Year, this.calImportTo.SelectionStart.Month, this.calImportTo.SelectionStart.Day, this.dtImportToTime.Value.Hour, this.dtImportToTime.Value.Minute, this.dtImportToTime.Value.Second);
            if (time > time2)
            {
                this.calImportTo.SelectionStart = time;
                this.dtImportToTime.Value = time;
            }
        }

        private void dtImportToTime_ValueChanged(object sender, EventArgs e)
        {
            this.rbImportToDateTime.Checked = true;
            DateTime time = new DateTime(this.calImportFrom.SelectionStart.Year, this.calImportFrom.SelectionStart.Month, this.calImportFrom.SelectionStart.Day, this.dtImportFromTime.Value.Hour, this.dtImportFromTime.Value.Minute, this.dtImportFromTime.Value.Second);
            DateTime time2 = new DateTime(this.calImportTo.SelectionStart.Year, this.calImportTo.SelectionStart.Month, this.calImportTo.SelectionStart.Day, this.dtImportToTime.Value.Hour, this.dtImportToTime.Value.Minute, this.dtImportToTime.Value.Second);
            if (time2 < time)
            {
                this.calImportFrom.SelectionStart = time2;
                this.dtImportFromTime.Value = time2;
            }
        }

        private void InitializeComponent()
        {
            this.lblImportEFile = new Label();
            this.rbImportToDateTime = new RadioButton();
            this.rbImportToEnd = new RadioButton();
            this.lblImportBFile = new Label();
            this.rbImportFromDateTime = new RadioButton();
            this.rbImportFromBegining = new RadioButton();
            this.dtImportFromTime = new DateTimePicker();
            this.cbImportCustomTriggers = new CheckBox();
            this.btnImportFile = new Button();
            this.groupBox1 = new GroupBox();
            this.groupBox2 = new GroupBox();
            this.calImportFrom = new MonthCalendar();
            this.groupBox3 = new GroupBox();
            this.groupBox4 = new GroupBox();
            this.calImportTo = new MonthCalendar();
            this.dtImportToTime = new DateTimePicker();
            this.cbContToEndCombat = new CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            base.SuspendLayout();
            this.lblImportEFile.BorderStyle = BorderStyle.FixedSingle;
            this.lblImportEFile.Location = new Point(0x19, 0x105);
            this.lblImportEFile.Name = "lblImportEFile";
            this.lblImportEFile.Padding = new Padding(10, 0, 0, 0);
            this.lblImportEFile.Size = new Size(0xfb, 0x1a);
            this.lblImportEFile.TabIndex = 3;
            this.lblImportEFile.Text = "End of File";
            this.lblImportEFile.TextAlign = ContentAlignment.MiddleLeft;
            this.lblImportEFile.Click += new EventHandler(this.lblImportEFile_Click);
            this.lblImportEFile.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbImportToDateTime.Location = new Point(6, 0x13);
            this.rbImportToDateTime.Margin = new Padding(3, 0, 0, 0);
            this.rbImportToDateTime.Name = "rbImportToDateTime";
            this.rbImportToDateTime.Size = new Size(0x10, 0xe5);
            this.rbImportToDateTime.TabIndex = 0;
            this.rbImportToDateTime.UseVisualStyleBackColor = true;
            this.rbImportToDateTime.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbImportToEnd.Checked = true;
            this.rbImportToEnd.Location = new Point(6, 0x105);
            this.rbImportToEnd.Margin = new Padding(3, 1, 0, 0);
            this.rbImportToEnd.Name = "rbImportToEnd";
            this.rbImportToEnd.Size = new Size(0x10, 0x1a);
            this.rbImportToEnd.TabIndex = 1;
            this.rbImportToEnd.TabStop = true;
            this.rbImportToEnd.UseVisualStyleBackColor = true;
            this.rbImportToEnd.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblImportBFile.BorderStyle = BorderStyle.FixedSingle;
            this.lblImportBFile.Location = new Point(0x19, 0x105);
            this.lblImportBFile.Name = "lblImportBFile";
            this.lblImportBFile.Padding = new Padding(10, 0, 0, 0);
            this.lblImportBFile.Size = new Size(0xfb, 0x1a);
            this.lblImportBFile.TabIndex = 2;
            this.lblImportBFile.Text = "Begining of File";
            this.lblImportBFile.TextAlign = ContentAlignment.MiddleLeft;
            this.lblImportBFile.Click += new EventHandler(this.lblImportBFile_Click);
            this.lblImportBFile.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbImportFromDateTime.Location = new Point(6, 0x13);
            this.rbImportFromDateTime.Margin = new Padding(3, 0, 0, 0);
            this.rbImportFromDateTime.Name = "rbImportFromDateTime";
            this.rbImportFromDateTime.Size = new Size(0x10, 0xe5);
            this.rbImportFromDateTime.TabIndex = 0;
            this.rbImportFromDateTime.TextAlign = ContentAlignment.MiddleCenter;
            this.rbImportFromDateTime.UseVisualStyleBackColor = true;
            this.rbImportFromDateTime.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbImportFromBegining.Checked = true;
            this.rbImportFromBegining.Location = new Point(6, 0x105);
            this.rbImportFromBegining.Margin = new Padding(3, 0, 0, 0);
            this.rbImportFromBegining.Name = "rbImportFromBegining";
            this.rbImportFromBegining.Size = new Size(0x10, 0x1a);
            this.rbImportFromBegining.TabIndex = 1;
            this.rbImportFromBegining.TabStop = true;
            this.rbImportFromBegining.UseVisualStyleBackColor = true;
            this.rbImportFromBegining.MouseHover += new EventHandler(this.control_MouseHover);
            this.dtImportFromTime.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.dtImportFromTime.Checked = false;
            this.dtImportFromTime.CustomFormat = "";
            this.dtImportFromTime.Format = DateTimePickerFormat.Time;
            this.dtImportFromTime.Location = new Point(12, 0x13);
            this.dtImportFromTime.Name = "dtImportFromTime";
            this.dtImportFromTime.ShowUpDown = true;
            this.dtImportFromTime.Size = new Size(0xe3, 20);
            this.dtImportFromTime.TabIndex = 1;
            this.dtImportFromTime.ValueChanged += new EventHandler(this.dtImportFromTime_ValueChanged);
            this.cbImportCustomTriggers.AutoSize = true;
            this.cbImportCustomTriggers.Location = new Point(3, 0x139);
            this.cbImportCustomTriggers.Name = "cbImportCustomTriggers";
            this.cbImportCustomTriggers.Size = new Size(0x143, 0x11);
            this.cbImportCustomTriggers.TabIndex = 1;
            this.cbImportCustomTriggers.Text = "When importing, parse for Custom Triggers (tabbed results only)";
            this.cbImportCustomTriggers.UseVisualStyleBackColor = true;
            this.cbImportCustomTriggers.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnImportFile.Location = new Point(0x1d3, 0x149);
            this.btnImportFile.Name = "btnImportFile";
            this.btnImportFile.Size = new Size(0x70, 0x18);
            this.btnImportFile.TabIndex = 8;
            this.btnImportFile.Text = "Select File...";
            this.btnImportFile.Click += new EventHandler(this.btnImportFile_Click);
            this.btnImportFile.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.lblImportBFile);
            this.groupBox1.Controls.Add(this.rbImportFromDateTime);
            this.groupBox1.Controls.Add(this.rbImportFromBegining);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x11d, 0x130);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log File Start Position";
            this.groupBox1.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.calImportFrom);
            this.groupBox2.Controls.Add(this.dtImportFromTime);
            this.groupBox2.Location = new Point(0x19, 0x13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xfb, 0xe5);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "By Date/Time";
            this.groupBox2.MouseHover += new EventHandler(this.control_MouseHover);
            this.calImportFrom.Location = new Point(12, 0x33);
            this.calImportFrom.Margin = new Padding(9, 9, 9, 0);
            this.calImportFrom.MaxSelectionCount = 1;
            this.calImportFrom.Name = "calImportFrom";
            this.calImportFrom.TabIndex = 0;
            this.calImportFrom.DateChanged += new DateRangeEventHandler(this.calImportFrom_DateChanged);
            this.calImportFrom.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox3.Controls.Add(this.lblImportEFile);
            this.groupBox3.Controls.Add(this.rbImportToDateTime);
            this.groupBox3.Controls.Add(this.rbImportToEnd);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new Point(0x126, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x11d, 0x130);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log File End Position";
            this.groupBox3.MouseHover += new EventHandler(this.control_MouseHover);
            this.groupBox4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.groupBox4.Controls.Add(this.calImportTo);
            this.groupBox4.Controls.Add(this.dtImportToTime);
            this.groupBox4.Location = new Point(0x19, 0x13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0xfb, 0xe5);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "By Date/Time";
            this.groupBox4.MouseHover += new EventHandler(this.control_MouseHover);
            this.calImportTo.Location = new Point(12, 0x33);
            this.calImportTo.Margin = new Padding(9, 9, 9, 0);
            this.calImportTo.MaxSelectionCount = 1;
            this.calImportTo.Name = "calImportTo";
            this.calImportTo.TabIndex = 0;
            this.calImportTo.DateChanged += new DateRangeEventHandler(this.calImportTo_DateChanged);
            this.calImportTo.MouseHover += new EventHandler(this.control_MouseHover);
            this.dtImportToTime.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.dtImportToTime.Checked = false;
            this.dtImportToTime.CustomFormat = "";
            this.dtImportToTime.Format = DateTimePickerFormat.Time;
            this.dtImportToTime.Location = new Point(12, 0x13);
            this.dtImportToTime.Name = "dtImportToTime";
            this.dtImportToTime.ShowUpDown = true;
            this.dtImportToTime.Size = new Size(0xe3, 20);
            this.dtImportToTime.TabIndex = 1;
            this.dtImportToTime.ValueChanged += new EventHandler(this.dtImportToTime_ValueChanged);
            this.cbContToEndCombat.AutoSize = true;
            this.cbContToEndCombat.Checked = true;
            this.cbContToEndCombat.CheckState = CheckState.Checked;
            this.cbContToEndCombat.Location = new Point(3, 0x150);
            this.cbContToEndCombat.Name = "cbContToEndCombat";
            this.cbContToEndCombat.Size = new Size(0x146, 0x11);
            this.cbContToEndCombat.TabIndex = 9;
            this.cbContToEndCombat.Text = "When importing by time, don't stop importing until end of combat";
            this.cbContToEndCombat.UseVisualStyleBackColor = true;
            this.cbContToEndCombat.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.cbContToEndCombat);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.btnImportFile);
            base.Controls.Add(this.cbImportCustomTriggers);
            base.Controls.Add(this.groupBox1);
            base.Name = "IO_ImportLog";
            base.Size = new Size(0x246, 0x164);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void lblImportBFile_Click(object sender, EventArgs e)
        {
            this.rbImportFromBegining.Checked = true;
        }

        private void lblImportEFile_Click(object sender, EventArgs e)
        {
            this.rbImportToEnd.Checked = true;
        }
    }
}

