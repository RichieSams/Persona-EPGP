namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Media;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    public class FormUpdater : Form
    {
        private Button btnCancel;
        private Button btnDownload;
        private Button btnUpdate;
        private Thread checkThread;
        private IContainer components;
        private bool isCanceled;
        private Label label1;
        private Label label2;
        private Label lblThisVer;
        private Label lblUpdateStatus;
        private Label lblWebVer;
        private ProgressBar pBar;
        private bool showWindow;
        internal System.Windows.Forms.Timer tmrUIupdate;
        private WebBrowser webBrowser1;

        public FormUpdater()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.btnDownload.Enabled = false;
            this.btnUpdate.Enabled = false;
            this.isCanceled = true;
            base.Hide();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblUpdateStatus.ForeColor = SystemColors.ControlText;
                this.lblUpdateStatus.Text = "Downloading update...  connecting...";
                this.pBar.Value = 0;
                Application.DoEvents();
                try
                {
                    MemoryStream stream2;
                    WebClient client = new WebClient();
                    client.Headers.Add(HttpRequestHeader.UserAgent, "ACT-Parser");
                    DirectoryInfo info = new DirectoryInfo(Path.GetTempPath() + @"\AdvancedCombatTracker");
                    info.Create();
                    BinaryReader reader = new BinaryReader(client.OpenRead("http://advancedcombattracker.com/versioncheck.php?dl=" + this.lblWebVer.Text));
                    string s = client.ResponseHeaders["Content-Length"];
                    if (s != null)
                    {
                        this.pBar.Maximum = int.Parse(s);
                        stream2 = new MemoryStream(this.pBar.Maximum);
                    }
                    else
                    {
                        stream2 = new MemoryStream();
                    }
                    FileStream stream = new FileInfo(info.FullName + @"\update-" + this.lblWebVer.Text + ".exe").OpenWrite();
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.SetLength(0);
                    while (!this.isCanceled)
                    {
                        byte[] buffer = reader.ReadBytes(0x1000);
                        stream2.Write(buffer, 0, buffer.Length);
                        if (buffer.Length < 0x1000)
                        {
                            break;
                        }
                        if (this.pBar.Maximum != 1)
                        {
                            this.pBar.Value += buffer.Length;
                            this.lblUpdateStatus.Text = string.Concat(new object[] { "Downloading update: ", this.pBar.Value, " of ", this.pBar.Maximum, " bytes." });
                        }
                        else
                        {
                            this.lblUpdateStatus.Text = "Downloading update: " + stream2.Position + " bytes read...";
                        }
                        Application.DoEvents();
                    }
                    try
                    {
                        reader.Close();
                    }
                    catch
                    {
                    }
                    stream2.WriteTo(stream);
                    stream.Flush();
                    stream.Close();
                }
                catch (WebException exception)
                {
                    this.lblUpdateStatus.Text = string.Concat(new object[] { "There was an error in downloading the update:\n", exception.Message, " ", exception.Status });
                    ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
                    return;
                }
                catch (SystemException exception2)
                {
                    this.lblUpdateStatus.Text = "There was an error updating:\n" + exception2.Message;
                    ActGlobals.oFormActMain.WriteExceptionLog(exception2, string.Empty);
                    return;
                }
                this.pBar.Value = this.pBar.Maximum;
                SystemSounds.Beep.Play();
                this.lblUpdateStatus.Text = "Download is complete!  Click Update to continue.";
                this.btnUpdate.Enabled = true;
            }
            catch (Exception exception3)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception3, string.Empty);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(ActGlobals.ActLocalization.LocalizationStrings["messageBox-updateAct"].DisplayedText, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-updateAct"].DisplayedText, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
                {
                    Process currentProcess = Process.GetCurrentProcess();
                    DirectoryInfo info = new DirectoryInfo(Path.GetTempPath() + @"\AdvancedCombatTracker");
                    ActGlobals.oFormActMain.FinalizeACT();
                    Process.Start(info.FullName + @"\update-" + this.lblWebVer.Text + ".exe", string.Format("/S \"/exename={0}\" /D={1}", Path.GetFileName(currentProcess.MainModule.FileName), Path.GetDirectoryName(currentProcess.MainModule.FileName)));
                    Application.Exit();
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }

        public void Check()
        {
            this.checkThread = new Thread(new ThreadStart(this.CheckThreadStart));
            this.checkThread.Priority = ThreadPriority.BelowNormal;
            this.checkThread.Name = "Version Check Thread";
            this.checkThread.Start();
        }

        private void CheckThreadStart()
        {
            try
            {
                string fileVersion;
                string str2;
                this.isCanceled = false;
                ThreadInvokes.ControlSetText(this, this.lblUpdateStatus, "Checking version from web...");
                Application.DoEvents();
                try
                {
                    WebClient client = new WebClient();
                    client.Headers.Add(HttpRequestHeader.UserAgent, "ACT-Parser");
                    str2 = new StreamReader(client.OpenRead("http://advancedcombattracker.com/versioncheck.php?v3")).ReadToEnd();
                    fileVersion = Process.GetCurrentProcess().MainModule.FileVersionInfo.FileVersion;
                }
                catch (ThreadAbortException)
                {
                    return;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(ActGlobals.ActLocalization.LocalizationStrings["messageBox-updateCheckFail"].DisplayedText + exception.Message, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-updateCheckFail"].DisplayedText);
                    ThreadInvokes.ControlSetText(this, this.lblUpdateStatus, ActGlobals.ActLocalization.LocalizationStrings["messageBox-updateCheckFail"].DisplayedText + exception.Message);
                    ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
                    return;
                }
                ThreadInvokes.ControlSetText(this, this.lblThisVer, fileVersion);
                ThreadInvokes.ControlSetText(this, this.lblWebVer, str2);
                this.webBrowser1.Navigate("http://advancedcombattracker.com/versioncheck.php?last=5");
                if (fileVersion == str2)
                {
                    ThreadInvokes.ControlSetText(this, this.lblUpdateStatus, "No updates available.");
                }
                else
                {
                    this.showWindow = true;
                    SystemSounds.Beep.Play();
                    this.lblUpdateStatus.ForeColor = Color.DarkRed;
                    ThreadInvokes.ControlSetText(this, this.lblUpdateStatus, "Update to " + str2 + " available.\nPress Download to retrieve.");
                    ThreadInvokes.ControlSetEnabled(this, this.btnDownload, true);
                }
            }
            catch (Exception exception2)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception2, string.Empty);
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
            xml.WriteAttributeString("Form", "FormUpdater");
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

        private void Form3_Closing(object sender, CancelEventArgs e)
        {
            this.btnDownload.Enabled = false;
            this.btnUpdate.Enabled = false;
            base.Hide();
            e.Cancel = true;
            this.isCanceled = true;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
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
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormUpdater));
            this.label1 = new Label();
            this.label2 = new Label();
            this.lblThisVer = new Label();
            this.lblWebVer = new Label();
            this.btnCancel = new Button();
            this.btnUpdate = new Button();
            this.btnDownload = new Button();
            this.lblUpdateStatus = new Label();
            this.tmrUIupdate = new System.Windows.Forms.Timer(this.components);
            this.pBar = new ProgressBar();
            this.webBrowser1 = new WebBrowser();
            base.SuspendLayout();
            this.label1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label1.Location = new Point(8, 0x15b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x68, 0x10);
            this.label1.TabIndex = 2;
            this.label1.Text = "This Version:";
            this.label2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label2.Location = new Point(8, 0x16b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x68, 0x10);
            this.label2.TabIndex = 2;
            this.label2.Text = "Current Version:";
            this.lblThisVer.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblThisVer.Location = new Point(0x70, 0x15b);
            this.lblThisVer.Name = "lblThisVer";
            this.lblThisVer.Size = new Size(100, 0x10);
            this.lblThisVer.TabIndex = 3;
            this.lblThisVer.Text = "1.0.0.0";
            this.lblWebVer.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblWebVer.Location = new Point(0x70, 0x16b);
            this.lblWebVer.Name = "lblWebVer";
            this.lblWebVer.Size = new Size(100, 0x10);
            this.lblWebVer.TabIndex = 3;
            this.lblWebVer.Text = "1.0.0.0";
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x20c, 0x18f);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x48, 0x18);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnUpdate.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new Point(0x20c, 0x177);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(0x48, 0x18);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
            this.btnDownload.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnDownload.Location = new Point(0x20c, 0x15f);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new Size(0x48, 0x18);
            this.btnDownload.TabIndex = 4;
            this.btnDownload.Text = "Download";
            this.btnDownload.Click += new EventHandler(this.btnDownload_Click);
            this.lblUpdateStatus.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblUpdateStatus.Location = new Point(8, 0x17f);
            this.lblUpdateStatus.Name = "lblUpdateStatus";
            this.lblUpdateStatus.Size = new Size(0x1fc, 40);
            this.lblUpdateStatus.TabIndex = 5;
            this.tmrUIupdate.Interval = 0x3e8;
            this.tmrUIupdate.Tick += new EventHandler(this.tmrUIupdate_Tick);
            this.pBar.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.pBar.Location = new Point(8, 0x14f);
            this.pBar.Maximum = 1;
            this.pBar.Name = "pBar";
            this.pBar.Size = new Size(0x24c, 8);
            this.pBar.TabIndex = 6;
            this.webBrowser1.AllowNavigation = false;
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new Point(8, 12);
            this.webBrowser1.MinimumSize = new Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new Size(0x24c, 0x13d);
            this.webBrowser1.TabIndex = 7;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x25c, 0x1ab);
            base.Controls.Add(this.webBrowser1);
            base.Controls.Add(this.pBar);
            base.Controls.Add(this.lblUpdateStatus);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.lblThisVer);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.lblWebVer);
            base.Controls.Add(this.btnUpdate);
            base.Controls.Add(this.btnDownload);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.Name = "FormUpdater";
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Update";
            base.TopMost = true;
            base.Closing += new CancelEventHandler(this.Form3_Closing);
            base.Load += new EventHandler(this.Form3_Load);
            base.ResumeLayout(false);
        }

        private void tmrUIupdate_Tick(object sender, EventArgs e)
        {
            if (this.showWindow)
            {
                base.Show();
                this.showWindow = false;
            }
        }
    }
}

