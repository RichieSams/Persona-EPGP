namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class FormSpellTimersPanel : Form
    {
        private IContainer components;
        private string lastTooltip = "None";
        internal PictureBox pb1;
        private ToolTip toolTip1;
        internal ToolTipGrid ttg = new ToolTipGrid();
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;

        public FormSpellTimersPanel(string Name)
        {
            this.InitializeComponent();
            base.Name = Name;
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
            xml.WriteAttributeString("Form", "FormSpellTimersPanel");
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

        private void FormSpellTimersPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        private void FormSpellTimersPanel_Load(object sender, EventArgs e)
        {
            if (ActGlobals.oFormSpellTimers != null)
            {
                ActGlobals.oFormSpellTimers.ReinitDisplayPanel();
            }
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
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormSpellTimersPanel));
            this.pb1 = new PictureBox();
            this.toolTip1 = new ToolTip(this.components);
            ((ISupportInitialize) this.pb1).BeginInit();
            base.SuspendLayout();
            this.pb1.BorderStyle = BorderStyle.Fixed3D;
            this.pb1.Dock = DockStyle.Fill;
            this.pb1.Location = new Point(0, 0);
            this.pb1.Name = "pb1";
            this.pb1.Size = new Size(0xcb, 0x16f);
            this.pb1.TabIndex = 1;
            this.pb1.TabStop = false;
            this.pb1.DoubleClick += new EventHandler(this.pb1_DoubleClick);
            this.pb1.MouseMove += new MouseEventHandler(this.pb1_MouseMove);
            this.pb1.Resize += new EventHandler(this.pb1_Resize);
            this.pb1.MouseUp += new MouseEventHandler(this.pb1_MouseUp);
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.AutoPopDelay = 0xbb8;
            this.toolTip1.InitialDelay = 50;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 0;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xcb, 0x16f);
            base.Controls.Add(this.pb1);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            this.MinimumSize = new Size(0x72, 0x40);
            base.Name = "FormSpellTimersPanel";
            base.Opacity = 0.99;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Spell Timers";
            base.TopMost = true;
            base.Load += new EventHandler(this.FormSpellTimersPanel_Load);
            base.FormClosing += new FormClosingEventHandler(this.FormSpellTimersPanel_FormClosing);
            ((ISupportInitialize) this.pb1).EndInit();
            base.ResumeLayout(false);
        }

        private void pb1_DoubleClick(object sender, EventArgs e)
        {
            ActGlobals.oFormSpellTimers.TriggerSelectedTimer();
        }

        private void pb1_MouseMove(object sender, MouseEventArgs e)
        {
            string toolTipTextAt = this.ttg.GetToolTipTextAt(e.X, e.Y);
            if (this.lastTooltip != toolTipTextAt)
            {
                this.lastTooltip = toolTipTextAt;
                if (!string.IsNullOrEmpty(toolTipTextAt))
                {
                    this.toolTip1.SetToolTip(this.pb1, toolTipTextAt);
                }
                else
                {
                    this.toolTip1.SetToolTip(this.pb1, "Right-click for options.");
                }
            }
        }

        private void pb1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ActGlobals.oFormSpellTimers.Visible = !ActGlobals.oFormSpellTimers.Visible;
            }
        }

        private void pb1_Resize(object sender, EventArgs e)
        {
            if (ActGlobals.oFormSpellTimers != null)
            {
                ActGlobals.oFormSpellTimers.ReinitDisplayPanel();
            }
        }

        internal void SetClickThrough(bool enable)
        {
            if (enable)
            {
                base.ShowInTaskbar = false;
                base.FormBorderStyle = FormBorderStyle.None;
                int dwNewLong = GetWindowLong(base.Handle, -20) | 0x20;
                SetWindowLong(base.Handle, -20, dwNewLong);
            }
            else
            {
                int num2 = GetWindowLong(base.Handle, -20) ^ 0x20;
                SetWindowLong(base.Handle, -20, num2);
                base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                base.ShowInTaskbar = true;
            }
        }

        [DllImport("user32", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern int SetWindowLong(IntPtr Handle, int nIndex, int dwNewLong);
    }
}

