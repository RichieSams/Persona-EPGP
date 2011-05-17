namespace Advanced_Combat_Tracker
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Security;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class FormStartupWizard : Form
    {
        private Button btnBack;
        private Button btnClose;
        private Button btnGetPluginTitles;
        private Button btnNext;
        private Button btnUsePlugin;
        private CheckBox cbAutoLogSwitch;
        private CheckBox cbAutoVerCheck;
        private IContainer components;
        private ComboBox ddlPluginListing;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lblLogsStatus;
        private Label lblPluginName;
        private Label lblRecentLog;
        private Label lblRegistryStatus;
        private NumericUpDown nudLogSplitSize;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private WebBrowser webBrowser1;

        public FormStartupWizard()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex > 0)
            {
                this.tabControl1.SelectedIndex--;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.cbAutoVerCheck.Checked = ActGlobals.oFormActMain.cbAutoCheck.Checked;
            this.nudLogSplitSize.Value = ActGlobals.oFormActMain.opMisc.nudLogFileSplit.Value;
            base.Hide();
        }

        private void btnGetPluginTitles_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            string[] strArray = client.DownloadString("http://advancedcombattracker.com/plugininfo.php?parsing").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            this.ddlPluginListing.Items.Clear();
            for (int i = 0; i < strArray.Length; i++)
            {
                PluginDownloadInfo item = new PluginDownloadInfo(int.Parse(strArray[i]));
                this.ddlPluginListing.Items.Add(item);
            }
            if (this.ddlPluginListing.Items.Count > 0)
            {
                this.ddlPluginListing.SelectedIndex = 0;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex < 3)
            {
                this.tabControl1.SelectedIndex++;
            }
        }

        private void btnUsePlugin_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ddlPluginListing.SelectedIndex != -1)
                {
                    PluginDownloadInfo info = (PluginDownloadInfo) this.ddlPluginListing.Items[this.ddlPluginListing.SelectedIndex];
                    FileInfo info2 = ActGlobals.oFormActMain.PluginDownload(info.Id);
                    FileInfo info3 = new FileInfo(Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName + @"\Plugins", info2.Name));
                    if (info3.Exists)
                    {
                        info3.Delete();
                    }
                    info2.MoveTo(info3.FullName);
                    if (info3.Extension.ToLower() == ".zip")
                    {
                        foreach (FileInfo info4 in ActGlobals.oFormActMain.UnZip(info3.FullName, info3.DirectoryName))
                        {
                            if ((!(info4.Extension.ToLower() == ".cs") && !(info4.Extension.ToLower() == ".vb")) && !(info4.Extension.ToLower() == ".dll"))
                            {
                                continue;
                            }
                            ActPluginData data = null;
                            foreach (ActPluginData data2 in ActGlobals.oFormActMain.ActPlugins)
                            {
                                if (data2.pluginFile.FullName == info4.FullName)
                                {
                                    data = data2;
                                }
                            }
                            if (data == null)
                            {
                                data = ActGlobals.oFormActMain.AddPluginPanel(info4.FullName, false);
                            }
                            else
                            {
                                data.cbEnabled.Checked = false;
                            }
                            data.cbEnabled.Checked = true;
                            Application.DoEvents();
                            if (data.cbEnabled.Checked)
                            {
                                MessageBox.Show("The parsing plugin has been added and started.", "Parser Applied", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            return;
                        }
                        MessageBox.Show("The downloaded file did not contain a valid plugin", "Unknown file type", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (((info3.Extension.ToLower() == ".cs") || (info3.Extension.ToLower() == ".vb")) || (info3.Extension.ToLower() == ".dll"))
                    {
                        ActPluginData data3 = null;
                        foreach (ActPluginData data4 in ActGlobals.oFormActMain.ActPlugins)
                        {
                            if (data4.pluginFile.FullName == info3.FullName)
                            {
                                data3 = data4;
                            }
                        }
                        if (data3 == null)
                        {
                            data3 = ActGlobals.oFormActMain.AddPluginPanel(info3.FullName, false);
                        }
                        else
                        {
                            data3.cbEnabled.Checked = false;
                        }
                        data3.cbEnabled.Checked = true;
                        Application.DoEvents();
                        if (data3.cbEnabled.Checked)
                        {
                            MessageBox.Show("The parsing plugin has been added and started.", "Parser Applied", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The downloaded file was not of the expected type for a plugin.", "Unknown file type", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
                MessageBox.Show(exception.Message, "Error applying plugin", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void cbAutoLogSwitch_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.opMisc.cbAutoLoadLogs.Checked = this.cbAutoLogSwitch.Checked;
        }

        private void cbAutoVerCheck_CheckedChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.cbAutoCheck.Checked = this.cbAutoVerCheck.Checked;
        }

        private void CheckForEnabledParsers()
        {
            if (ActGlobals.oFormActMain.GetBeforeLogLineEventUsed())
            {
                MessageBox.Show("Simple detection suggests that you may already have a parsing plugin enabled.  Check the Plugins tab before adding another.", "Plugin event in-use", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void ddlPluginListing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlPluginListing.SelectedIndex != -1)
            {
                PluginDownloadInfo info = (PluginDownloadInfo) this.ddlPluginListing.Items[this.ddlPluginListing.SelectedIndex];
                this.lblPluginName.Text = string.Format("Added: {1} - Modified: {2} {3}", new object[] { info.Title, info.AddedDate.ToShortDateString(), info.ModifiedDate.ToShortDateString(), info.ModifiedDate.ToShortTimeString() });
                this.webBrowser1.AllowNavigation = true;
                this.webBrowser1.Navigate("http://advancedcombattracker.com/plugininfo.php?desc=" + info.Id);
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

        public void ExportControlTextXML(Stream Output)
        {
            XmlTextWriter xml = new XmlTextWriter(Output, Encoding.UTF8) {
                Formatting = Formatting.Indented,
                Indentation = 4,
                Namespaces = false
            };
            xml.WriteStartDocument();
            xml.WriteStartElement("", "ControlText", "");
            xml.WriteAttributeString("Form", "FormStartupWizard");
            ActGlobals.oFormActMain.ExportControlChilderenText(xml, this, "root");
            xml.WriteEndElement();
            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();
        }

        public void ExportControlTextXML(string FilePath)
        {
            this.ExportControlTextXML(new FileInfo(FilePath).OpenWrite());
        }

        private void FindLogFile()
        {
            this.cbAutoLogSwitch.Checked = ActGlobals.oFormActMain.opMisc.cbAutoLoadLogs.Checked;
            this.btnBack.Enabled = true;
            this.btnNext.Enabled = false;
            this.lblLogsStatus.Text = string.Empty;
            this.lblRecentLog.Text = string.Empty;
            this.lblRegistryStatus.Text = string.Empty;
            if (MessageBox.Show("Will ACT be used for EverQuest II?", "EQ2?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                MessageBox.Show("Please manually select your game log file.", "Select log file", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                FileInfo info7 = null;
                OpenFileDialog dialog3 = new OpenFileDialog {
                    Filter = "Game Log Files|" + ActGlobals.oFormActMain.LogFileFilter + "|Text Files (*.txt)|*.txt|Log Files (*.log)|*.log|Any File (*.*)|*.*"
                };
                try
                {
                    if (dialog3.ShowDialog() != DialogResult.OK)
                    {
                        this.tabControl1.SelectedIndex = 0;
                        return;
                    }
                    info7 = new FileInfo(dialog3.FileName);
                }
                catch (SecurityException exception2)
                {
                    MessageBox.Show(ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText + exception2.Message, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                this.lblRecentLog.Text = string.Format("{0}\\{1}\nFile size: {2:0,0}  Last Modified: {3}", new object[] { info7.Directory.Name, info7.Name, info7.Length, info7.LastWriteTime.ToString("F") });
                ActGlobals.oFormActMain.LogFilePath = info7.FullName;
                ActGlobals.oFormActMain.OpenLog(true, true);
            }
            else
            {
                bool flag = false;
                string fullName = string.Empty;
                List<DirectoryInfo> list = new List<DirectoryInfo>();
                try
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\EverQuest2.exe");
                    if (key == null)
                    {
                        key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\LaunchPad.exe");
                    }
                    if (key != null)
                    {
                        list.Add(new DirectoryInfo(key.GetValue("Path").ToString()));
                    }
                }
                catch
                {
                }
                list.Add(new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Sony Online Entertainment\Installed Games\EverQuest II Streaming")));
                list.Add(new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Sony Online Entertainment\Installed Games\EverQuest II Extended")));
                list.Add(new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Sony Online Entertainment\Installed Games\EverQuest II Streaming")));
                list.Add(new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Sony Online Entertainment\Installed Games\EverQuest II Extended")));
                foreach (DirectoryInfo info in list)
                {
                    if (info.Exists)
                    {
                        FileInfo info2 = new FileInfo(Path.Combine(info.FullName, "EverQuest2.exe"));
                        if (info2.Exists)
                        {
                            switch (MessageBox.Show(string.Format(ActGlobals.ActLocalization.LocalizationStrings["messageBox-eq2FolderWizard"].DisplayedText, info.FullName), ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-eq2FolderWizard"].DisplayedText, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Cancel:
                                    this.tabControl1.SelectedIndex = 0;
                                    return;

                                case DialogResult.Yes:
                                    flag = true;
                                    fullName = info.FullName;
                                    break;
                            }
                            this.lblRegistryStatus.Text = fullName;
                        }
                    }
                }
                while (!flag)
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog {
                        Description = "Select your EQ2 installation folder",
                        ShowNewFolderButton = false
                    };
                    if (dialog.ShowDialog() == DialogResult.Cancel)
                    {
                        this.tabControl1.SelectedIndex = 0;
                        return;
                    }
                    fullName = dialog.SelectedPath;
                    FileInfo info3 = new FileInfo(dialog.SelectedPath + @"\EverQuest2.exe");
                    if (info3.Exists)
                    {
                        flag = true;
                    }
                    else
                    {
                        switch (MessageBox.Show(ActGlobals.ActLocalization.LocalizationStrings["messageBox-eq2NotFound"].DisplayedText, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-eq2NotFound"].DisplayedText, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation))
                        {
                            case DialogResult.Abort:
                                this.tabControl1.SelectedIndex = 0;
                                return;

                            case DialogResult.Ignore:
                            {
                                flag = true;
                                continue;
                            }
                        }
                    }
                }
                this.lblRegistryStatus.Text = fullName;
                DirectoryInfo info4 = new DirectoryInfo(fullName + @"\logs");
                if (info4.Exists)
                {
                    this.lblLogsStatus.ForeColor = System.Drawing.Color.DarkGreen;
                    this.lblLogsStatus.Text = "EQ2 Logs folder found.";
                    FileInfo info5 = null;
                    List<FileInfo> list2 = new List<FileInfo>();
                    foreach (DirectoryInfo info6 in info4.GetDirectories("*", SearchOption.TopDirectoryOnly))
                    {
                        list2.AddRange(info6.GetFiles("eq2log_*.txt", SearchOption.TopDirectoryOnly));
                    }
                    for (int i = 0; i < list2.Count; i++)
                    {
                        if (info5 == null)
                        {
                            info5 = list2[i];
                        }
                        if (list2[i].LastWriteTime > info5.LastWriteTime)
                        {
                            info5 = list2[i];
                        }
                    }
                    if (info5 == null)
                    {
                        MessageBox.Show(ActGlobals.ActLocalization.LocalizationStrings["messageBox-openLogWizardNoLogs"].DisplayedText, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-openLogWizardNoLogs"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        this.lblRecentLog.Text = string.Format("{0}\\{1}\nFile size: {2:0,0}  Last Modified: {3}", new object[] { info5.Directory.Name, info5.Name, info5.Length, info5.LastWriteTime.ToString("F") });
                        switch (MessageBox.Show(info5.Name + ActGlobals.ActLocalization.LocalizationStrings["messageBox-openLogWizard"].DisplayedText, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-openLogWizard"].DisplayedText, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            case DialogResult.Cancel:
                                this.tabControl1.SelectedIndex = 0;
                                return;

                            case DialogResult.No:
                            {
                                OpenFileDialog dialog2 = new OpenFileDialog {
                                    Filter = "Game Log Files|" + ActGlobals.oFormActMain.LogFileFilter + "|Text Files (*.txt)|*.txt|Log Files (*.log)|*.log|Any File (*.*)|*.*"
                                };
                                try
                                {
                                    dialog2.InitialDirectory = info5.Directory.FullName;
                                    if (dialog2.ShowDialog() != DialogResult.OK)
                                    {
                                        this.tabControl1.SelectedIndex = 0;
                                        return;
                                    }
                                    info5 = new FileInfo(dialog2.FileName);
                                }
                                catch (SecurityException exception)
                                {
                                    MessageBox.Show(ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText + exception.Message, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-localSecurityPolicy"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                    return;
                                }
                                break;
                            }
                        }
                        this.lblRecentLog.Text = string.Format("{0}\\{1}\nFile size: {2:0,0}  Last Modified: {3}", new object[] { info5.Directory.Name, info5.Name, info5.Length, info5.LastWriteTime.ToString("F") });
                        ActGlobals.oFormActMain.LogFilePath = info5.FullName;
                        ActGlobals.oFormActMain.OpenLog(true, true);
                    }
                }
                else
                {
                    this.lblLogsStatus.ForeColor = System.Drawing.Color.DarkRed;
                    this.lblLogsStatus.Text = "EQ2 Logs folder not found.";
                    MessageBox.Show(ActGlobals.ActLocalization.LocalizationStrings["messageBox-openLogWizardNoFolder"].DisplayedText, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-openLogWizardNoFolder"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.tabControl1.SelectedIndex = 0;
                }
            }
        }

        private void Form16_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        public void ImportControlTextXML(Stream Input)
        {
            XmlTextReader reader = new XmlTextReader(Input);
            try
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        try
                        {
                            if (reader.LocalName == "Control")
                            {
                                bool found = false;
                                Control c = this;
                                string attribute = reader.GetAttribute("UniqueName");
                                string controlText = reader.GetAttribute("Text");
                                if (!ActGlobals.oFormActMain.ImportControlChilderenText(attribute, controlText, found, c))
                                {
                                    throw new ArgumentException(string.Format("Control {0} could not be located in the windows form.", attribute));
                                }
                            }
                            continue;
                        }
                        catch (Exception exception)
                        {
                            ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Format(ActGlobals.ActLocalization.LocalizationStrings["messageBox-xmlReadError"].DisplayedText, reader.LineNumber, reader.LocalName, exception.Message));
                            continue;
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                MessageBox.Show(string.Format(ActGlobals.ActLocalization.LocalizationStrings["messageBox-xmlSyntaxError"].DisplayedText, exception2.Message), ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-xmlPrefError"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            reader.Close();
        }

        public void ImportControlTextXML(string FilePath)
        {
            this.ImportControlTextXML(new FileInfo(FilePath).OpenRead());
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormStartupWizard));
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.label1 = new Label();
            this.tabPage4 = new TabPage();
            this.label7 = new Label();
            this.btnUsePlugin = new Button();
            this.lblPluginName = new Label();
            this.ddlPluginListing = new ComboBox();
            this.btnGetPluginTitles = new Button();
            this.webBrowser1 = new WebBrowser();
            this.tabPage2 = new TabPage();
            this.label3 = new Label();
            this.cbAutoLogSwitch = new CheckBox();
            this.lblRecentLog = new Label();
            this.lblLogsStatus = new Label();
            this.lblRegistryStatus = new Label();
            this.tabPage3 = new TabPage();
            this.label6 = new Label();
            this.label5 = new Label();
            this.nudLogSplitSize = new NumericUpDown();
            this.label4 = new Label();
            this.label2 = new Label();
            this.cbAutoVerCheck = new CheckBox();
            this.btnNext = new Button();
            this.btnBack = new Button();
            this.btnClose = new Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.nudLogSplitSize.BeginInit();
            base.SuspendLayout();
            this.tabControl1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tabControl1.Appearance = TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x215, 0x158);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new Point(4, 0x19);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x20d, 0x13b);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Start";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.label1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.label1.Location = new Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x207, 0x135);
            this.label1.TabIndex = 0;
            this.label1.Text = "This wizard will assist you in selecting a parsing plugin, configuring updates and selecting a log file.";
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.btnUsePlugin);
            this.tabPage4.Controls.Add(this.lblPluginName);
            this.tabPage4.Controls.Add(this.ddlPluginListing);
            this.tabPage4.Controls.Add(this.btnGetPluginTitles);
            this.tabPage4.Controls.Add(this.webBrowser1);
            this.tabPage4.Location = new Point(4, 0x19);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new Size(0x20d, 0x13b);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Parsing Plugin";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.label7.Location = new Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x20a, 0x42);
            this.label7.TabIndex = 5;
            this.label7.Text = manager.GetString("label7.Text");
            this.btnUsePlugin.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnUsePlugin.Location = new Point(0x18e, 0x5f);
            this.btnUsePlugin.Name = "btnUsePlugin";
            this.btnUsePlugin.Size = new Size(0x74, 0x17);
            this.btnUsePlugin.TabIndex = 4;
            this.btnUsePlugin.Text = "2: Use this plugin";
            this.btnUsePlugin.UseVisualStyleBackColor = true;
            this.btnUsePlugin.Click += new EventHandler(this.btnUsePlugin_Click);
            this.lblPluginName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblPluginName.BorderStyle = BorderStyle.FixedSingle;
            this.lblPluginName.Location = new Point(0, 0x5f);
            this.lblPluginName.Name = "lblPluginName";
            this.lblPluginName.Size = new Size(0x188, 0x17);
            this.lblPluginName.TabIndex = 3;
            this.lblPluginName.TextAlign = ContentAlignment.MiddleCenter;
            this.ddlPluginListing.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.ddlPluginListing.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlPluginListing.FormattingEnabled = true;
            this.ddlPluginListing.Location = new Point(0xc1, 0x47);
            this.ddlPluginListing.Name = "ddlPluginListing";
            this.ddlPluginListing.Size = new Size(0x141, 0x15);
            this.ddlPluginListing.TabIndex = 2;
            this.ddlPluginListing.SelectedIndexChanged += new EventHandler(this.ddlPluginListing_SelectedIndexChanged);
            this.btnGetPluginTitles.Location = new Point(0, 0x45);
            this.btnGetPluginTitles.Name = "btnGetPluginTitles";
            this.btnGetPluginTitles.Size = new Size(0xbb, 0x17);
            this.btnGetPluginTitles.TabIndex = 1;
            this.btnGetPluginTitles.Text = "1: Get available parsing plugins";
            this.btnGetPluginTitles.UseVisualStyleBackColor = true;
            this.btnGetPluginTitles.Click += new EventHandler(this.btnGetPluginTitles_Click);
            this.webBrowser1.AllowNavigation = false;
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.webBrowser1.Location = new Point(3, 0x7c);
            this.webBrowser1.MinimumSize = new Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new Size(0x207, 0xbc);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new Uri("about:blank", UriKind.Absolute);
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            this.webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cbAutoLogSwitch);
            this.tabPage2.Controls.Add(this.lblRecentLog);
            this.tabPage2.Controls.Add(this.lblLogsStatus);
            this.tabPage2.Controls.Add(this.lblRegistryStatus);
            this.tabPage2.Location = new Point(4, 0x19);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x20d, 0x13b);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log File";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.label3.Location = new Point(0, 0x8a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x205, 0x21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Any time you switch characters ACT can detect a different log file being used and switch to it automatically.";
            this.cbAutoLogSwitch.AutoSize = true;
            this.cbAutoLogSwitch.Checked = true;
            this.cbAutoLogSwitch.CheckState = CheckState.Checked;
            this.cbAutoLogSwitch.Location = new Point(0, 0x72);
            this.cbAutoLogSwitch.Name = "cbAutoLogSwitch";
            this.cbAutoLogSwitch.Size = new Size(0xc5, 0x11);
            this.cbAutoLogSwitch.TabIndex = 4;
            this.cbAutoLogSwitch.Text = "Auto-load recently changed log files.";
            this.cbAutoLogSwitch.UseVisualStyleBackColor = true;
            this.cbAutoLogSwitch.CheckedChanged += new EventHandler(this.cbAutoLogSwitch_CheckedChanged);
            this.lblRecentLog.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblRecentLog.BorderStyle = BorderStyle.Fixed3D;
            this.lblRecentLog.Location = new Point(3, 0x4a);
            this.lblRecentLog.Margin = new Padding(3);
            this.lblRecentLog.Name = "lblRecentLog";
            this.lblRecentLog.Size = new Size(0x207, 0x22);
            this.lblRecentLog.TabIndex = 0;
            this.lblRecentLog.TextAlign = ContentAlignment.MiddleLeft;
            this.lblLogsStatus.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblLogsStatus.BorderStyle = BorderStyle.Fixed3D;
            this.lblLogsStatus.Location = new Point(3, 0x2d);
            this.lblLogsStatus.Margin = new Padding(3);
            this.lblLogsStatus.Name = "lblLogsStatus";
            this.lblLogsStatus.Size = new Size(0x207, 0x17);
            this.lblLogsStatus.TabIndex = 0;
            this.lblLogsStatus.TextAlign = ContentAlignment.MiddleLeft;
            this.lblRegistryStatus.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblRegistryStatus.BorderStyle = BorderStyle.Fixed3D;
            this.lblRegistryStatus.Location = new Point(3, 6);
            this.lblRegistryStatus.Margin = new Padding(3);
            this.lblRegistryStatus.Name = "lblRegistryStatus";
            this.lblRegistryStatus.Size = new Size(0x207, 0x21);
            this.lblRegistryStatus.TabIndex = 0;
            this.lblRegistryStatus.TextAlign = ContentAlignment.MiddleLeft;
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.nudLogSplitSize);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.cbAutoVerCheck);
            this.tabPage3.Location = new Point(4, 0x19);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(3);
            this.tabPage3.Size = new Size(0x20d, 0x13b);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Startup Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.label6.Location = new Point(3, 0x6b);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x202, 0x71);
            this.label6.TabIndex = 5;
            this.label6.Text = manager.GetString("label6.Text");
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0xdb, 0x56);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x94, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "megabytes.  (Zero will disable)";
            this.nudLogSplitSize.Location = new Point(0xa5, 0x54);
            int[] bits = new int[4];
            bits[0] = 0x400;
            this.nudLogSplitSize.Maximum = new decimal(bits);
            this.nudLogSplitSize.Name = "nudLogSplitSize";
            this.nudLogSplitSize.Size = new Size(0x30, 20);
            this.nudLogSplitSize.TabIndex = 3;
            int[] numArray2 = new int[4];
            numArray2[0] = 0x40;
            this.nudLogSplitSize.Value = new decimal(numArray2);
            this.nudLogSplitSize.ValueChanged += new EventHandler(this.nudLogSplitSize_ValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(3, 0x56);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x9c, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Attempt to split the log file every";
            this.label2.Location = new Point(3, 0x1a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x202, 50);
            this.label2.TabIndex = 1;
            this.label2.Text = manager.GetString("label2.Text");
            this.cbAutoVerCheck.AutoSize = true;
            this.cbAutoVerCheck.Checked = true;
            this.cbAutoVerCheck.CheckState = CheckState.Checked;
            this.cbAutoVerCheck.Location = new Point(6, 6);
            this.cbAutoVerCheck.Name = "cbAutoVerCheck";
            this.cbAutoVerCheck.Size = new Size(120, 0x11);
            this.cbAutoVerCheck.TabIndex = 0;
            this.cbAutoVerCheck.Text = "Auto Version Check";
            this.cbAutoVerCheck.UseVisualStyleBackColor = true;
            this.cbAutoVerCheck.CheckedChanged += new EventHandler(this.cbAutoVerCheck_CheckedChanged);
            this.btnNext.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnNext.Location = new Point(0x175, 0x15a);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x4b, 0x17);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnBack.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnBack.Enabled = false;
            this.btnBack.Location = new Point(0x124, 0x15a);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(0x4b, 0x17);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "< Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new EventHandler(this.btnBack_Click);
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnClose.Location = new Point(0x1c6, 0x15a);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x215, 0x174);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnBack);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.tabControl1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new Size(0x21b, 0x18c);
            base.Name = "FormStartupWizard";
            this.Text = "Startup Wizard";
            base.TopMost = true;
            base.FormClosing += new FormClosingEventHandler(this.Form16_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.nudLogSplitSize.EndInit();
            base.ResumeLayout(false);
        }

        private void nudLogSplitSize_ValueChanged(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.opMisc.nudLogFileSplit.Value = this.nudLogSplitSize.Value;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.tabControl1.SelectedIndex)
            {
                case 1:
                    this.CheckForEnabledParsers();
                    this.btnBack.Enabled = true;
                    this.btnNext.Enabled = true;
                    return;

                case 2:
                    this.FindLogFile();
                    this.btnBack.Enabled = true;
                    this.btnNext.Enabled = true;
                    return;

                case 3:
                    this.btnBack.Enabled = true;
                    this.btnNext.Enabled = false;
                    return;

                case 4:
                    this.btnBack.Enabled = true;
                    this.btnNext.Enabled = false;
                    return;
            }
            this.btnBack.Enabled = false;
            this.btnNext.Enabled = true;
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.webBrowser1.AllowNavigation = false;
        }
    }
}

