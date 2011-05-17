namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class FormMiniParse : Form
    {
        internal CheckBox cbbDisplayGraph;
        private ContextMenu cmFormat;
        private Container components;
        internal Graphics fbG;
        internal int formatIndex = -1;
        internal Bitmap frontBuffer;
        internal float graphBottomVal = -1f;
        internal float graphTopVal = -1f;
        internal PictureBox pb1;
        internal RichTextBox rtb2;
        private ToolTip ttGraphingMode = new ToolTip();
        private ToolTip ttPictureBox1 = new ToolTip();
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;

        public FormMiniParse()
        {
            this.InitializeComponent();
            this.frontBuffer = new Bitmap(this.pb1.Width, this.pb1.Height);
            this.fbG = Graphics.FromImage(this.frontBuffer);
            this.fbG.SmoothingMode = SmoothingMode.AntiAlias;
            this.fbG.Clear(SystemColors.ControlLight);
        }

        private void btnDisplayFlip_Click(object sender, EventArgs e)
        {
            if (this.cbbDisplayGraph.Checked)
            {
                this.pb1.Visible = true;
                this.rtb2.Visible = false;
            }
            else
            {
                this.pb1.Visible = false;
                this.rtb2.Visible = true;
            }
            ActGlobals.oFormActMain.UpdateMiniEnc();
        }

        private void btnDisplayFlip_MouseHover(object sender, EventArgs e)
        {
            this.ttGraphingMode.SetToolTip(this.cbbDisplayGraph, "Switch between text and graph modes");
        }

        private void cmFormat_ClickItem(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem) sender;
            this.formatIndex = item.Index;
        }

        private void cmFormat_Popup(object sender, EventArgs e)
        {
            this.cmFormat.MenuItems.Clear();
            foreach (TextExportFormatOptions options in ActGlobals.oFormActMain.TextExportFormats)
            {
                MenuItem item = new MenuItem(options.ToString(), new EventHandler(this.cmFormat_ClickItem));
                this.cmFormat.MenuItems.Add(item);
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
            xml.WriteAttributeString("Form", "FormMiniParse");
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

        private void Form2_Closing(object sender, CancelEventArgs e)
        {
            base.Hide();
            e.Cancel = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            base.Hide();
        }

        [DllImport("user32", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern int GetWindowLong(IntPtr Handle, int nIndex);
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormMiniParse));
            this.rtb2 = new RichTextBox();
            this.cmFormat = new ContextMenu();
            this.pb1 = new PictureBox();
            this.cbbDisplayGraph = new CheckBox();
            ((ISupportInitialize) this.pb1).BeginInit();
            base.SuspendLayout();
            this.rtb2.BackColor = Color.Black;
            this.rtb2.BorderStyle = BorderStyle.None;
            this.rtb2.ContextMenu = this.cmFormat;
            this.rtb2.Cursor = Cursors.Arrow;
            this.rtb2.Dock = DockStyle.Fill;
            this.rtb2.ForeColor = Color.Yellow;
            this.rtb2.Location = new Point(4, 4);
            this.rtb2.Name = "rtb2";
            this.rtb2.ReadOnly = true;
            this.rtb2.ScrollBars = RichTextBoxScrollBars.None;
            this.rtb2.Size = new Size(0x88, 0xd0);
            this.rtb2.TabIndex = 0;
            this.rtb2.Text = "";
            this.rtb2.WordWrap = false;
            this.cmFormat.Popup += new EventHandler(this.cmFormat_Popup);
            this.pb1.BackColor = SystemColors.Control;
            this.pb1.ContextMenu = this.cmFormat;
            this.pb1.Cursor = Cursors.Cross;
            this.pb1.Dock = DockStyle.Fill;
            this.pb1.Location = new Point(4, 4);
            this.pb1.Name = "pb1";
            this.pb1.Size = new Size(0x88, 0xd0);
            this.pb1.TabIndex = 1;
            this.pb1.TabStop = false;
            this.pb1.Visible = false;
            this.pb1.MouseMove += new MouseEventHandler(this.pb1_MouseMove);
            this.pb1.Resize += new EventHandler(this.pb1_Resize);
            this.cbbDisplayGraph.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.cbbDisplayGraph.Appearance = Appearance.Button;
            this.cbbDisplayGraph.BackColor = Color.Red;
            this.cbbDisplayGraph.FlatStyle = FlatStyle.Popup;
            this.cbbDisplayGraph.Location = new Point(0x88, 0);
            this.cbbDisplayGraph.Name = "cbbDisplayGraph";
            this.cbbDisplayGraph.Size = new Size(8, 8);
            this.cbbDisplayGraph.TabIndex = 3;
            this.cbbDisplayGraph.UseVisualStyleBackColor = false;
            this.cbbDisplayGraph.CheckedChanged += new EventHandler(this.btnDisplayFlip_Click);
            this.cbbDisplayGraph.MouseHover += new EventHandler(this.btnDisplayFlip_MouseHover);
            this.AutoScaleBaseSize = new Size(5, 13);
            this.BackColor = Color.Black;
            base.ClientSize = new Size(0x90, 0xd8);
            base.Controls.Add(this.cbbDisplayGraph);
            base.Controls.Add(this.pb1);
            base.Controls.Add(this.rtb2);
            this.ForeColor = Color.Yellow;
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            this.MinimumSize = new Size(0x80, 0x40);
            base.Name = "FormMiniParse";
            base.Padding = new Padding(4);
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Mini Parse";
            base.TopMost = true;
            base.TransparencyKey = Color.FromArgb(0xff, 1, 0xff);
            base.Closing += new CancelEventHandler(this.Form2_Closing);
            base.Load += new EventHandler(this.Form2_Load);
            ((ISupportInitialize) this.pb1).EndInit();
            base.ResumeLayout(false);
        }

        private void pb1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.graphTopVal > -1f)
            {
                this.ttPictureBox1.Active = true;
            }
            else
            {
                this.ttPictureBox1.Active = false;
            }
            float num = (this.graphTopVal - this.graphBottomVal) / ((float) (this.pb1.Height - 4));
            float num2 = this.graphTopVal - (num * e.Y);
            this.ttPictureBox1.SetToolTip(this.pb1, string.Concat(new object[] { "Val: ", (int) num2, " Y: ", e.Y }));
        }

        private void pb1_Resize(object sender, EventArgs e)
        {
            try
            {
                if (base.WindowState != FormWindowState.Minimized)
                {
                    this.frontBuffer = new Bitmap(this.pb1.Width, this.pb1.Height);
                    this.fbG = Graphics.FromImage(this.frontBuffer);
                    this.fbG.SmoothingMode = SmoothingMode.AntiAlias;
                    this.fbG.Clear(SystemColors.ControlLight);
                }
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
            }
        }

        public void SetClickThrough(bool enable)
        {
            if (enable)
            {
                base.FormBorderStyle = FormBorderStyle.None;
                int dwNewLong = GetWindowLong(base.Handle, -20) | 0x20;
                SetWindowLong(base.Handle, -20, dwNewLong);
            }
            else
            {
                int num2 = GetWindowLong(base.Handle, -20) ^ 0x20;
                SetWindowLong(base.Handle, -20, num2);
                base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            }
        }

        [DllImport("user32", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern int SetWindowLong(IntPtr Handle, int nIndex, int dwNewLong);
    }
}

