namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Windows.Forms;

    internal class Options_FileHTML : UserControl
    {
        private Button btnClearHTML;
        private Button btnExFTPTest;
        private Button btnExportUIBrowser;
        internal CheckBox cbCurrentGraph;
        internal CheckBox cbCurrentTable;
        internal CheckBox cbExGraph;
        internal CheckBox cbExHTML;
        internal CheckBox cbExHTMLFTP;
        internal CheckBox cbHTMLCullEncounters;
        internal CheckBox cbHtmlTimers;
        private IContainer components;
        private GroupBox groupBox15;
        private GroupBox groupBox16;
        private Label label1;
        private Label label15;
        private Label label18;
        private Label label25;
        private Label label28;
        private Label label29;
        private Label label30;
        private Label label31;
        private Label label32;
        private Label label33;
        private Label label34;
        private Label lblGraphXY;
        private LinkLabel linkLabelHTMLTutorial;
        internal NumericUpDown nudCGraphDelay;
        internal NumericUpDown nudExFTPPort;
        internal NumericUpDown nudGraphX;
        internal NumericUpDown nudGraphY;
        internal NumericUpDown nudHTMLCullingCount;
        internal RadioButton rbExFTPActive;
        internal RadioButton rbExFTPPassive;
        internal TextBox tbExFTPPass;
        internal TextBox tbExFTPPath;
        internal TextBox tbExFTPServer;
        internal TextBox tbExFTPUser;
        private Dictionary<string, LocalizationObject> Trans = ActGlobals.ActLocalization.LocalizationStrings;

        public Options_FileHTML()
        {
            this.InitializeComponent();
        }

        private void btnClearHTML_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cbExHTMLFTP.Checked)
                {
                    ActGlobals.oFormActMain.htmlStartIndex = 1;
                    ActGlobals.oFormActMain.htmlIndex = 0;
                    ActGlobals.oFormActMain.htmlEntries.Clear();
                }
                else
                {
                    ActGlobals.oFormActMain.htmlStartIndex = ActGlobals.oFormActMain.htmlIndex + 1;
                }
                ActGlobals.oFormActMain.indexFs.Seek(0, SeekOrigin.Begin);
                ActGlobals.oFormActMain.indexFs.SetLength(0);
                StreamWriter writer = new StreamWriter(ActGlobals.oFormActMain.indexFs);
                writer.WriteLine("<html><head><META http-equiv='Content-Type' content='text/html; charset=utf-8'></head>");
                writer.WriteLine("<BODY BGCOLOR='#000000' TEXT='#FFFFFF' ALINK='#9999FF' LINK='#9999FF' VLINK='#990099'>");
                writer.WriteLine("<h4><a href=\"index.html\">Return to Index</a> - <a href=\"current.htm\">Current Encounter</a></h4><br><hr><br></BODY></HTML>");
                writer.Flush();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, this.Trans["messageBoxTitle-error"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }

        private void btnExFTPTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.Trans["messageBox-ftpConnectionTest"].DisplayedText);
            if (string.IsNullOrEmpty(this.tbExFTPPath.Text))
            {
                this.tbExFTPPath.Text = "/";
            }
            if (!this.tbExFTPPath.Text.EndsWith("/"))
            {
                this.tbExFTPPath.Text = this.tbExFTPPath.Text + "/";
            }
            if (!this.tbExFTPPath.Text.StartsWith("/"))
            {
                this.tbExFTPPath.Text = "/" + this.tbExFTPPath.Text;
            }
            if (string.IsNullOrEmpty(this.tbExFTPUser.Text))
            {
                this.tbExFTPUser.Text = "anonymous";
                this.tbExFTPPass.Text = "anonymous";
            }
            string statusDescription = string.Empty;
            string str2 = string.Empty;
            TimeSpan span = new TimeSpan(0);
            try
            {
                DateTime now = DateTime.Now;
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream, Encoding.ASCII);
                writer.WriteLine("This is just a test file.");
                writer.Flush();
                writer.BaseStream.Seek(0, SeekOrigin.Begin);
                str2 = str2 + "\nUploading file: ";
                str2 = str2 + ActGlobals.oFormActMain.FTPUploadFile("test.txt", stream, false);
                str2 = str2 + "\n\nDeleting file: ";
                str2 = str2 + ActGlobals.oFormActMain.FTPDeleteFile("test.txt");
                span = (TimeSpan) (DateTime.Now - now);
            }
            catch (WebException exception)
            {
                statusDescription = ((FtpWebResponse) exception.Response).StatusDescription;
            }
            catch (Exception exception2)
            {
                statusDescription = exception2.Message;
            }
            if (string.IsNullOrEmpty(statusDescription))
            {
                MessageBox.Show(str2 + this.Trans["messageBox-ftpTestSuccess"].DisplayedText + "Full test: " + span.TotalSeconds.ToString("0.00") + " seconds.", this.Trans["messageBoxTitle-ftpTestSuccess"].DisplayedText);
            }
            else
            {
                MessageBox.Show(str2 + "\n\n=============================\n" + statusDescription, this.Trans["messageBoxTitle-ftpTestFail"].DisplayedText);
            }
        }

        private void btnExportUIBrowser_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this.Trans["messageBox-uiExport1"].DisplayedText, "1 of 2", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result != DialogResult.Cancel)
            {
                DialogResult result2 = MessageBox.Show(this.Trans["messageBox-uiExport2"].DisplayedText, "2 of 2", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result2 != DialogResult.Cancel)
                {
                    bool replaceHomepage = false;
                    if (result == DialogResult.Yes)
                    {
                        replaceHomepage = true;
                    }
                    bool exportXml = false;
                    if (result2 == DialogResult.Yes)
                    {
                        exportXml = true;
                    }
                    if (replaceHomepage || exportXml)
                    {
                        ActGlobals.oFormActMain.UIFix(replaceHomepage, exportXml);
                    }
                }
            }
        }

        private void cbExHTMLFTP_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbExHTMLFTP.Checked)
            {
                this.cbCurrentGraph.Checked = false;
                this.cbCurrentGraph.Enabled = false;
                this.cbCurrentTable.Checked = false;
                this.cbCurrentTable.Enabled = false;
                this.cbHtmlTimers.Checked = false;
                this.cbHtmlTimers.Enabled = false;
            }
            else
            {
                this.cbCurrentGraph.Enabled = true;
                this.cbCurrentTable.Enabled = true;
                this.cbHtmlTimers.Enabled = true;
            }
        }

        private void cbHtmlTimers_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbHtmlTimers.Checked)
            {
                ActGlobals.oFormActMain.timerHtmFs.Seek(0, SeekOrigin.Begin);
                ActGlobals.oFormActMain.timerHtmFs.SetLength(0);
                StreamWriter writer = new StreamWriter(ActGlobals.oFormActMain.timerHtmFs);
                writer.WriteLine("<html>");
                writer.WriteLine("<BODY BGCOLOR='#000000' TEXT='#FFFFFF' ALINK='#9999FF' LINK='#9999FF' VLINK='#990099'>");
                writer.WriteLine("<center><a href=\"index.html\"><img src=\"timers.png\" id=\"timerPic\"></a></center>");
                writer.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\">");
                writer.WriteLine("function reloadImage()");
                writer.WriteLine("{");
                writer.WriteLine("   img = document.getElementById('timerPic');");
                writer.WriteLine("   img.src = 'timers.png?' + Math.random();");
                writer.WriteLine("}");
                writer.WriteLine("</script>");
                writer.WriteLine("<script type=\"text/javascript\" language=\"JavaScript\">");
                writer.WriteLine("{");
                writer.WriteLine("   setInterval('reloadImage()', 500);");
                writer.WriteLine("}");
                writer.WriteLine("</script>");
                writer.WriteLine("</BODY></HTML>");
                writer.Flush();
            }
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
            this.groupBox15 = new GroupBox();
            this.nudCGraphDelay = new NumericUpDown();
            this.label1 = new Label();
            this.label25 = new Label();
            this.linkLabelHTMLTutorial = new LinkLabel();
            this.btnClearHTML = new Button();
            this.cbHTMLCullEncounters = new CheckBox();
            this.nudHTMLCullingCount = new NumericUpDown();
            this.groupBox16 = new GroupBox();
            this.btnExFTPTest = new Button();
            this.label34 = new Label();
            this.rbExFTPActive = new RadioButton();
            this.label33 = new Label();
            this.label28 = new Label();
            this.cbExHTMLFTP = new CheckBox();
            this.tbExFTPServer = new TextBox();
            this.label29 = new Label();
            this.tbExFTPPath = new TextBox();
            this.label30 = new Label();
            this.tbExFTPUser = new TextBox();
            this.label31 = new Label();
            this.tbExFTPPass = new TextBox();
            this.label32 = new Label();
            this.nudExFTPPort = new NumericUpDown();
            this.rbExFTPPassive = new RadioButton();
            this.btnExportUIBrowser = new Button();
            this.cbHtmlTimers = new CheckBox();
            this.cbExGraph = new CheckBox();
            this.cbCurrentTable = new CheckBox();
            this.cbCurrentGraph = new CheckBox();
            this.cbExHTML = new CheckBox();
            this.label15 = new Label();
            this.nudGraphY = new NumericUpDown();
            this.nudGraphX = new NumericUpDown();
            this.label18 = new Label();
            this.lblGraphXY = new Label();
            this.groupBox15.SuspendLayout();
            this.nudCGraphDelay.BeginInit();
            this.nudHTMLCullingCount.BeginInit();
            this.groupBox16.SuspendLayout();
            this.nudExFTPPort.BeginInit();
            this.nudGraphY.BeginInit();
            this.nudGraphX.BeginInit();
            base.SuspendLayout();
            this.groupBox15.Controls.Add(this.nudCGraphDelay);
            this.groupBox15.Controls.Add(this.label1);
            this.groupBox15.Controls.Add(this.label25);
            this.groupBox15.Controls.Add(this.linkLabelHTMLTutorial);
            this.groupBox15.Controls.Add(this.btnClearHTML);
            this.groupBox15.Controls.Add(this.cbHTMLCullEncounters);
            this.groupBox15.Controls.Add(this.nudHTMLCullingCount);
            this.groupBox15.Controls.Add(this.groupBox16);
            this.groupBox15.Controls.Add(this.btnExportUIBrowser);
            this.groupBox15.Controls.Add(this.cbHtmlTimers);
            this.groupBox15.Controls.Add(this.cbExGraph);
            this.groupBox15.Controls.Add(this.cbCurrentTable);
            this.groupBox15.Controls.Add(this.cbCurrentGraph);
            this.groupBox15.Controls.Add(this.cbExHTML);
            this.groupBox15.Controls.Add(this.label15);
            this.groupBox15.Controls.Add(this.nudGraphY);
            this.groupBox15.Controls.Add(this.nudGraphX);
            this.groupBox15.Controls.Add(this.label18);
            this.groupBox15.Controls.Add(this.lblGraphXY);
            this.groupBox15.Location = new Point(3, 3);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new Size(560, 300);
            this.groupBox15.TabIndex = 2;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Fullscreen HTML Viewing";
            this.nudCGraphDelay.Location = new Point(0x38, 0x54);
            int[] bits = new int[4];
            bits[0] = 120;
            this.nudCGraphDelay.Maximum = new decimal(bits);
            this.nudCGraphDelay.Name = "nudCGraphDelay";
            this.nudCGraphDelay.Size = new Size(0x2b, 20);
            this.nudCGraphDelay.TabIndex = 7;
            int[] numArray2 = new int[4];
            numArray2[0] = 5;
            this.nudCGraphDelay.Value = new decimal(numArray2);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x69, 0x57);
            this.label1.Name = "label1";
            this.label1.Size = new Size(50, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "seconds:";
            this.label25.AutoSize = true;
            this.label25.Location = new Point(5, 0x57);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x22, 13);
            this.label25.TabIndex = 6;
            this.label25.Text = "Every";
            this.linkLabelHTMLTutorial.AutoSize = true;
            this.linkLabelHTMLTutorial.Location = new Point(0xc6, 0);
            this.linkLabelHTMLTutorial.Name = "linkLabelHTMLTutorial";
            this.linkLabelHTMLTutorial.Size = new Size(0x2a, 13);
            this.linkLabelHTMLTutorial.TabIndex = 0x34;
            this.linkLabelHTMLTutorial.TabStop = true;
            this.linkLabelHTMLTutorial.Text = "Tutorial";
            this.linkLabelHTMLTutorial.TextAlign = ContentAlignment.TopCenter;
            this.linkLabelHTMLTutorial.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabelHTMLTutorial_LinkClicked);
            this.linkLabelHTMLTutorial.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnClearHTML.BackColor = SystemColors.Control;
            this.btnClearHTML.Location = new Point(0x1b9, 0x34);
            this.btnClearHTML.Name = "btnClearHTML";
            this.btnClearHTML.Size = new Size(100, 20);
            this.btnClearHTML.TabIndex = 5;
            this.btnClearHTML.Text = "Clear All";
            this.btnClearHTML.UseVisualStyleBackColor = true;
            this.btnClearHTML.Click += new EventHandler(this.btnClearHTML_Click);
            this.btnClearHTML.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbHTMLCullEncounters.AutoSize = true;
            this.cbHTMLCullEncounters.Location = new Point(8, 0x36);
            this.cbHTMLCullEncounters.Name = "cbHTMLCullEncounters";
            this.cbHTMLCullEncounters.Size = new Size(0x16d, 0x11);
            this.cbHTMLCullEncounters.TabIndex = 3;
            this.cbHTMLCullEncounters.Text = "Automatically cull the number of listed encounters to a specified amount:";
            this.cbHTMLCullEncounters.MouseHover += new EventHandler(this.control_MouseHover);
            this.nudHTMLCullingCount.Location = new Point(0x181, 0x34);
            int[] numArray3 = new int[4];
            numArray3[0] = 2;
            this.nudHTMLCullingCount.Minimum = new decimal(numArray3);
            this.nudHTMLCullingCount.Name = "nudHTMLCullingCount";
            this.nudHTMLCullingCount.Size = new Size(0x30, 20);
            this.nudHTMLCullingCount.TabIndex = 4;
            int[] numArray4 = new int[4];
            numArray4[0] = 20;
            this.nudHTMLCullingCount.Value = new decimal(numArray4);
            this.groupBox16.Controls.Add(this.nudExFTPPort);
            this.groupBox16.Controls.Add(this.rbExFTPActive);
            this.groupBox16.Controls.Add(this.tbExFTPServer);
            this.groupBox16.Controls.Add(this.tbExFTPPath);
            this.groupBox16.Controls.Add(this.tbExFTPUser);
            this.groupBox16.Controls.Add(this.tbExFTPPass);
            this.groupBox16.Controls.Add(this.btnExFTPTest);
            this.groupBox16.Controls.Add(this.label34);
            this.groupBox16.Controls.Add(this.label33);
            this.groupBox16.Controls.Add(this.label28);
            this.groupBox16.Controls.Add(this.cbExHTMLFTP);
            this.groupBox16.Controls.Add(this.label29);
            this.groupBox16.Controls.Add(this.label30);
            this.groupBox16.Controls.Add(this.label31);
            this.groupBox16.Controls.Add(this.label32);
            this.groupBox16.Controls.Add(this.rbExFTPPassive);
            this.groupBox16.Location = new Point(8, 0xa9);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new Size(0x220, 0x7a);
            this.groupBox16.TabIndex = 15;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "FTP Settings";
            this.btnExFTPTest.BackColor = SystemColors.Control;
            this.btnExFTPTest.Location = new Point(0x180, 0x53);
            this.btnExFTPTest.Name = "btnExFTPTest";
            this.btnExFTPTest.Size = new Size(0x95, 0x1c);
            this.btnExFTPTest.TabIndex = 10;
            this.btnExFTPTest.Text = "Test these settings";
            this.btnExFTPTest.UseVisualStyleBackColor = true;
            this.btnExFTPTest.Click += new EventHandler(this.btnExFTPTest_Click);
            this.btnExFTPTest.MouseHover += new EventHandler(this.control_MouseHover);
            this.label34.Location = new Point(0x15b, 8);
            this.label34.Name = "label34";
            this.label34.Size = new Size(0xbd, 0x10);
            this.label34.TabIndex = 0x2f;
            this.label34.Text = "Advanced Users Only";
            this.label34.TextAlign = ContentAlignment.TopRight;
            this.rbExFTPActive.AutoSize = true;
            this.rbExFTPActive.Location = new Point(0x130, 0x55);
            this.rbExFTPActive.Margin = new Padding(3, 0, 3, 0);
            this.rbExFTPActive.Name = "rbExFTPActive";
            this.rbExFTPActive.Size = new Size(0x37, 0x11);
            this.rbExFTPActive.TabIndex = 8;
            this.rbExFTPActive.Text = "Active";
            this.rbExFTPActive.MouseHover += new EventHandler(this.control_MouseHover);
            this.label33.AutoSize = true;
            this.label33.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label33.Location = new Point(0x130, 0x48);
            this.label33.Name = "label33";
            this.label33.Size = new Size(0x4a, 12);
            this.label33.TabIndex = 7;
            this.label33.Text = "Connection Type";
            this.label33.MouseHover += new EventHandler(this.control_MouseHover);
            this.label28.Location = new Point(8, 0x20);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0xd8, 0x10);
            this.label28.TabIndex = 0x48;
            this.label28.Text = "Server Address";
            this.label28.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbExHTMLFTP.AutoSize = true;
            this.cbExHTMLFTP.Location = new Point(8, 0x10);
            this.cbExHTMLFTP.Name = "cbExHTMLFTP";
            this.cbExHTMLFTP.Size = new Size(0xc4, 0x11);
            this.cbExHTMLFTP.TabIndex = 0;
            this.cbExHTMLFTP.Text = "Send EQ2 HTML files to FTP server";
            this.cbExHTMLFTP.CheckedChanged += new EventHandler(this.cbExHTMLFTP_CheckedChanged);
            this.cbExHTMLFTP.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbExFTPServer.Location = new Point(8, 0x30);
            this.tbExFTPServer.Name = "tbExFTPServer";
            this.tbExFTPServer.Size = new Size(0xd8, 20);
            this.tbExFTPServer.TabIndex = 1;
            this.tbExFTPServer.Text = "ftp.server.com";
            this.tbExFTPServer.MouseHover += new EventHandler(this.control_MouseHover);
            this.label29.Location = new Point(0xe8, 0x20);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0x130, 0x10);
            this.label29.TabIndex = 0x4a;
            this.label29.Text = "Set Initial Path upon login";
            this.label29.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbExFTPPath.Location = new Point(0xe8, 0x30);
            this.tbExFTPPath.Name = "tbExFTPPath";
            this.tbExFTPPath.Size = new Size(0x130, 20);
            this.tbExFTPPath.TabIndex = 2;
            this.tbExFTPPath.Text = "/Initial Path/";
            this.tbExFTPPath.MouseHover += new EventHandler(this.control_MouseHover);
            this.label30.AutoSize = true;
            this.label30.Location = new Point(8, 0x48);
            this.label30.Name = "label30";
            this.label30.Size = new Size(60, 13);
            this.label30.TabIndex = 0x4c;
            this.label30.Text = "User Name";
            this.label30.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbExFTPUser.Location = new Point(8, 0x58);
            this.tbExFTPUser.Name = "tbExFTPUser";
            this.tbExFTPUser.Size = new Size(0x60, 20);
            this.tbExFTPUser.TabIndex = 3;
            this.tbExFTPUser.Text = "Username";
            this.tbExFTPUser.MouseHover += new EventHandler(this.control_MouseHover);
            this.label31.AutoSize = true;
            this.label31.Location = new Point(0x70, 0x48);
            this.label31.Name = "label31";
            this.label31.Size = new Size(0x35, 13);
            this.label31.TabIndex = 0x4e;
            this.label31.Text = "Password";
            this.label31.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbExFTPPass.Location = new Point(0x70, 0x58);
            this.tbExFTPPass.Name = "tbExFTPPass";
            this.tbExFTPPass.Size = new Size(0x60, 20);
            this.tbExFTPPass.TabIndex = 4;
            this.tbExFTPPass.Text = "Password";
            this.tbExFTPPass.MouseHover += new EventHandler(this.control_MouseHover);
            this.label32.AutoSize = true;
            this.label32.Location = new Point(0xe8, 0x48);
            this.label32.Name = "label32";
            this.label32.Size = new Size(0x42, 13);
            this.label32.TabIndex = 5;
            this.label32.Text = "Remote Port";
            this.label32.MouseHover += new EventHandler(this.control_MouseHover);
            this.nudExFTPPort.Location = new Point(0xe8, 0x58);
            int[] numArray5 = new int[4];
            numArray5[0] = 0xffff;
            this.nudExFTPPort.Maximum = new decimal(numArray5);
            int[] numArray6 = new int[4];
            numArray6[0] = 1;
            this.nudExFTPPort.Minimum = new decimal(numArray6);
            this.nudExFTPPort.Name = "nudExFTPPort";
            this.nudExFTPPort.Size = new Size(0x38, 20);
            this.nudExFTPPort.TabIndex = 6;
            int[] numArray7 = new int[4];
            numArray7[0] = 0x15;
            this.nudExFTPPort.Value = new decimal(numArray7);
            this.rbExFTPPassive.AutoSize = true;
            this.rbExFTPPassive.Checked = true;
            this.rbExFTPPassive.Location = new Point(0x130, 0x66);
            this.rbExFTPPassive.Margin = new Padding(3, 0, 3, 0);
            this.rbExFTPPassive.Name = "rbExFTPPassive";
            this.rbExFTPPassive.Size = new Size(0x3e, 0x11);
            this.rbExFTPPassive.TabIndex = 9;
            this.rbExFTPPassive.TabStop = true;
            this.rbExFTPPassive.Text = "Passive";
            this.rbExFTPPassive.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnExportUIBrowser.BackColor = SystemColors.Control;
            this.btnExportUIBrowser.Location = new Point(0x130, 0x10);
            this.btnExportUIBrowser.Name = "btnExportUIBrowser";
            this.btnExportUIBrowser.Size = new Size(0xfb, 0x18);
            this.btnExportUIBrowser.TabIndex = 2;
            this.btnExportUIBrowser.Text = "Set EQ2 homepage and/or replace UI";
            this.btnExportUIBrowser.UseVisualStyleBackColor = true;
            this.btnExportUIBrowser.Click += new EventHandler(this.btnExportUIBrowser_Click);
            this.btnExportUIBrowser.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbHtmlTimers.AutoSize = true;
            this.cbHtmlTimers.Location = new Point(8, 0x91);
            this.cbHtmlTimers.Name = "cbHtmlTimers";
            this.cbHtmlTimers.Size = new Size(0x119, 0x11);
            this.cbHtmlTimers.TabIndex = 14;
            this.cbHtmlTimers.Text = "Every second generate a Timers Window HTML page";
            this.cbHtmlTimers.CheckedChanged += new EventHandler(this.cbHtmlTimers_CheckedChanged);
            this.cbHtmlTimers.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbExGraph.AutoSize = true;
            this.cbExGraph.Location = new Point(0x20, 0x20);
            this.cbExGraph.Name = "cbExGraph";
            this.cbExGraph.Size = new Size(0x92, 0x11);
            this.cbExGraph.TabIndex = 1;
            this.cbExGraph.Text = "Include Encounter graph ";
            this.cbExGraph.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbCurrentTable.AutoSize = true;
            this.cbCurrentTable.Location = new Point(0xac, 0x5e);
            this.cbCurrentTable.Name = "cbCurrentTable";
            this.cbCurrentTable.Size = new Size(0xb8, 0x11);
            this.cbCurrentTable.TabIndex = 9;
            this.cbCurrentTable.Text = "Generate a main encounter table.";
            this.cbCurrentTable.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbCurrentGraph.AutoSize = true;
            this.cbCurrentGraph.Location = new Point(0xac, 0x4e);
            this.cbCurrentGraph.Name = "cbCurrentGraph";
            this.cbCurrentGraph.Size = new Size(0xe5, 0x11);
            this.cbCurrentGraph.TabIndex = 8;
            this.cbCurrentGraph.Text = "Generate a graph of the current encounter.";
            this.cbCurrentGraph.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbExHTML.AutoSize = true;
            this.cbExHTML.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.cbExHTML.Location = new Point(8, 0x10);
            this.cbExHTML.Name = "cbExHTML";
            this.cbExHTML.Size = new Size(0x120, 0x11);
            this.cbExHTML.TabIndex = 0;
            this.cbExHTML.Text = "Export static HTML files to view with /browser";
            this.cbExHTML.MouseHover += new EventHandler(this.control_MouseHover);
            this.label15.AutoSize = true;
            this.label15.Location = new Point(5, 0x79);
            this.label15.Name = "label15";
            this.label15.Size = new Size(14, 13);
            this.label15.TabIndex = 11;
            this.label15.Text = "X";
            this.label15.TextAlign = ContentAlignment.BottomLeft;
            this.label15.MouseHover += new EventHandler(this.control_MouseHover);
            int[] numArray8 = new int[4];
            numArray8[0] = 8;
            this.nudGraphY.Increment = new decimal(numArray8);
            this.nudGraphY.Location = new Point(0x63, 0x75);
            int[] numArray9 = new int[4];
            numArray9[0] = 0x640;
            this.nudGraphY.Maximum = new decimal(numArray9);
            int[] numArray10 = new int[4];
            numArray10[0] = 0x80;
            this.nudGraphY.Minimum = new decimal(numArray10);
            this.nudGraphY.Name = "nudGraphY";
            this.nudGraphY.Size = new Size(0x30, 20);
            this.nudGraphY.TabIndex = 12;
            int[] numArray11 = new int[4];
            numArray11[0] = 0x100;
            this.nudGraphY.Value = new decimal(numArray11);
            int[] numArray12 = new int[4];
            numArray12[0] = 8;
            this.nudGraphX.Increment = new decimal(numArray12);
            this.nudGraphX.Location = new Point(0x19, 0x75);
            int[] numArray13 = new int[4];
            numArray13[0] = 0x640;
            this.nudGraphX.Maximum = new decimal(numArray13);
            int[] numArray14 = new int[4];
            numArray14[0] = 0x80;
            this.nudGraphX.Minimum = new decimal(numArray14);
            this.nudGraphX.Name = "nudGraphX";
            this.nudGraphX.Size = new Size(0x30, 20);
            this.nudGraphX.TabIndex = 0x3d;
            int[] numArray15 = new int[4];
            numArray15[0] = 640;
            this.nudGraphX.Value = new decimal(numArray15);
            this.label18.AutoSize = true;
            this.label18.Location = new Point(0x4f, 0x79);
            this.label18.Name = "label18";
            this.label18.Size = new Size(14, 13);
            this.label18.TabIndex = 0x3e;
            this.label18.Text = "Y";
            this.label18.TextAlign = ContentAlignment.BottomLeft;
            this.label18.MouseHover += new EventHandler(this.control_MouseHover);
            this.lblGraphXY.AutoSize = true;
            this.lblGraphXY.Location = new Point(0x99, 0x79);
            this.lblGraphXY.Name = "lblGraphXY";
            this.lblGraphXY.Size = new Size(0xec, 13);
            this.lblGraphXY.TabIndex = 13;
            this.lblGraphXY.Text = "Desired size in pixels of the HTML graph images.";
            this.lblGraphXY.TextAlign = ContentAlignment.BottomLeft;
            this.lblGraphXY.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.groupBox15);
            base.Name = "Options_FileHTML";
            base.Size = new Size(0x236, 0x132);
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.nudCGraphDelay.EndInit();
            this.nudHTMLCullingCount.EndInit();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.nudExFTPPort.EndInit();
            this.nudGraphY.EndInit();
            this.nudGraphX.EndInit();
            base.ResumeLayout(false);
        }

        private void linkLabelHTMLTutorial_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://advancedcombattracker.com/HTMLWindow/");
        }
    }
}

