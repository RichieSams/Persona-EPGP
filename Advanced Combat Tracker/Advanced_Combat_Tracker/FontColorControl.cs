namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class FontColorControl : UserControl
    {
        private ColorSettingEventDelegate BackColorSettingChanged;
        private Button btnFontDisplay;
        private IContainer components;
        private bool fontChangable = true;
        private FontSettingEventDelegate FontSettingChanged;
        private ColorSettingEventDelegate ForeColorSettingChanged;
        private Label lblBackColor;
        private Label lblForeColor;
        private Label lblText;
        private PictureBox pbBackColor;
        private PictureBox pbForeColor;

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public event ColorSettingEventDelegate BackColorSettingChanged
        {
            add
            {
                ColorSettingEventDelegate delegate3;
                ColorSettingEventDelegate backColorSettingChanged = this.BackColorSettingChanged;
                do
                {
                    delegate3 = backColorSettingChanged;
                    ColorSettingEventDelegate delegate4 = (ColorSettingEventDelegate) Delegate.Combine(delegate3, value);
                    backColorSettingChanged = Interlocked.CompareExchange<ColorSettingEventDelegate>(ref this.BackColorSettingChanged, delegate4, delegate3);
                }
                while (backColorSettingChanged != delegate3);
            }
            remove
            {
                ColorSettingEventDelegate delegate3;
                ColorSettingEventDelegate backColorSettingChanged = this.BackColorSettingChanged;
                do
                {
                    delegate3 = backColorSettingChanged;
                    ColorSettingEventDelegate delegate4 = (ColorSettingEventDelegate) Delegate.Remove(delegate3, value);
                    backColorSettingChanged = Interlocked.CompareExchange<ColorSettingEventDelegate>(ref this.BackColorSettingChanged, delegate4, delegate3);
                }
                while (backColorSettingChanged != delegate3);
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public event FontSettingEventDelegate FontSettingChanged
        {
            add
            {
                FontSettingEventDelegate delegate3;
                FontSettingEventDelegate fontSettingChanged = this.FontSettingChanged;
                do
                {
                    delegate3 = fontSettingChanged;
                    FontSettingEventDelegate delegate4 = (FontSettingEventDelegate) Delegate.Combine(delegate3, value);
                    fontSettingChanged = Interlocked.CompareExchange<FontSettingEventDelegate>(ref this.FontSettingChanged, delegate4, delegate3);
                }
                while (fontSettingChanged != delegate3);
            }
            remove
            {
                FontSettingEventDelegate delegate3;
                FontSettingEventDelegate fontSettingChanged = this.FontSettingChanged;
                do
                {
                    delegate3 = fontSettingChanged;
                    FontSettingEventDelegate delegate4 = (FontSettingEventDelegate) Delegate.Remove(delegate3, value);
                    fontSettingChanged = Interlocked.CompareExchange<FontSettingEventDelegate>(ref this.FontSettingChanged, delegate4, delegate3);
                }
                while (fontSettingChanged != delegate3);
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public event ColorSettingEventDelegate ForeColorSettingChanged
        {
            add
            {
                ColorSettingEventDelegate delegate3;
                ColorSettingEventDelegate foreColorSettingChanged = this.ForeColorSettingChanged;
                do
                {
                    delegate3 = foreColorSettingChanged;
                    ColorSettingEventDelegate delegate4 = (ColorSettingEventDelegate) Delegate.Combine(delegate3, value);
                    foreColorSettingChanged = Interlocked.CompareExchange<ColorSettingEventDelegate>(ref this.ForeColorSettingChanged, delegate4, delegate3);
                }
                while (foreColorSettingChanged != delegate3);
            }
            remove
            {
                ColorSettingEventDelegate delegate3;
                ColorSettingEventDelegate foreColorSettingChanged = this.ForeColorSettingChanged;
                do
                {
                    delegate3 = foreColorSettingChanged;
                    ColorSettingEventDelegate delegate4 = (ColorSettingEventDelegate) Delegate.Remove(delegate3, value);
                    foreColorSettingChanged = Interlocked.CompareExchange<ColorSettingEventDelegate>(ref this.ForeColorSettingChanged, delegate4, delegate3);
                }
                while (foreColorSettingChanged != delegate3);
            }
        }

        public FontColorControl()
        {
            this.InitializeComponent();
        }

        private void btnFontDisplay_Click(object sender, EventArgs e)
        {
            if (this.fontChangable)
            {
                FontDialog dialog = new FontDialog {
                    Font = this.btnFontDisplay.Font,
                    ShowColor = false,
                    ShowEffects = false,
                    AllowVectorFonts = false,
                    AllowVerticalFonts = false,
                    FontMustExist = true,
                    ScriptsOnly = true
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.btnFontDisplay.Font = dialog.Font;
                }
                dialog.Dispose();
            }
        }

        private void btnFontDisplay_FontChanged(object sender, EventArgs e)
        {
            if (this.FontSettingChanged != null)
            {
                this.FontSettingChanged(this.btnFontDisplay.Font);
            }
        }

        private void control_mousehover(object sender, EventArgs e)
        {
            this.OnMouseHover(e);
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
            this.btnFontDisplay = new Button();
            this.pbBackColor = new PictureBox();
            this.lblForeColor = new Label();
            this.pbForeColor = new PictureBox();
            this.lblBackColor = new Label();
            this.lblText = new Label();
            ((ISupportInitialize) this.pbBackColor).BeginInit();
            ((ISupportInitialize) this.pbForeColor).BeginInit();
            base.SuspendLayout();
            this.btnFontDisplay.BackColor = Color.White;
            this.btnFontDisplay.FlatStyle = FlatStyle.Flat;
            this.btnFontDisplay.ForeColor = Color.Black;
            this.btnFontDisplay.Location = new Point(3, 3);
            this.btnFontDisplay.Name = "btnFontDisplay";
            this.btnFontDisplay.Size = new Size(0x59, 0x2c);
            this.btnFontDisplay.TabIndex = 0;
            this.btnFontDisplay.Text = "Font";
            this.btnFontDisplay.UseVisualStyleBackColor = false;
            this.btnFontDisplay.FontChanged += new EventHandler(this.btnFontDisplay_FontChanged);
            this.btnFontDisplay.Click += new EventHandler(this.btnFontDisplay_Click);
            this.btnFontDisplay.MouseHover += new EventHandler(this.control_mousehover);
            this.pbBackColor.BackColor = Color.White;
            this.pbBackColor.BorderStyle = BorderStyle.Fixed3D;
            this.pbBackColor.Cursor = Cursors.Hand;
            this.pbBackColor.Location = new Point(120, 0x11);
            this.pbBackColor.Name = "pbBackColor";
            this.pbBackColor.Size = new Size(0x18, 0x18);
            this.pbBackColor.TabIndex = 1;
            this.pbBackColor.TabStop = false;
            this.pbBackColor.BackColorChanged += new EventHandler(this.pbBackColor_BackColorChanged);
            this.pbBackColor.Click += new EventHandler(this.pbBackColor_Click);
            this.pbBackColor.MouseHover += new EventHandler(this.control_mousehover);
            this.lblForeColor.AutoSize = true;
            this.lblForeColor.BorderStyle = BorderStyle.FixedSingle;
            this.lblForeColor.Location = new Point(0x98, 7);
            this.lblForeColor.Name = "lblForeColor";
            this.lblForeColor.Padding = new Padding(1);
            this.lblForeColor.Size = new Size(0x37, 0x11);
            this.lblForeColor.TabIndex = 1;
            this.lblForeColor.Text = "Forecolor";
            this.lblForeColor.TextAlign = ContentAlignment.MiddleCenter;
            this.lblForeColor.MouseHover += new EventHandler(this.control_mousehover);
            this.pbForeColor.BackColor = Color.Black;
            this.pbForeColor.BorderStyle = BorderStyle.Fixed3D;
            this.pbForeColor.Cursor = Cursors.Hand;
            this.pbForeColor.Location = new Point(110, 7);
            this.pbForeColor.Name = "pbForeColor";
            this.pbForeColor.Size = new Size(0x18, 0x18);
            this.pbForeColor.TabIndex = 1;
            this.pbForeColor.TabStop = false;
            this.pbForeColor.BackColorChanged += new EventHandler(this.pbForeColor_BackColorChanged);
            this.pbForeColor.Click += new EventHandler(this.pbForeColor_Click);
            this.pbForeColor.MouseHover += new EventHandler(this.control_mousehover);
            this.lblBackColor.AutoSize = true;
            this.lblBackColor.BorderStyle = BorderStyle.FixedSingle;
            this.lblBackColor.Location = new Point(0xa2, 0x17);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Padding = new Padding(1);
            this.lblBackColor.Size = new Size(0x3b, 0x11);
            this.lblBackColor.TabIndex = 2;
            this.lblBackColor.Text = "Backcolor";
            this.lblBackColor.TextAlign = ContentAlignment.MiddleCenter;
            this.lblBackColor.MouseHover += new EventHandler(this.control_mousehover);
            this.lblText.AutoSize = true;
            this.lblText.Location = new Point(0xf3, 0x13);
            this.lblText.Name = "lblText";
            this.lblText.Size = new Size(0x57, 13);
            this.lblText.TabIndex = 3;
            this.lblText.Text = "Color Description";
            this.lblText.MouseHover += new EventHandler(this.control_mousehover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.lblText);
            base.Controls.Add(this.lblForeColor);
            base.Controls.Add(this.pbForeColor);
            base.Controls.Add(this.pbBackColor);
            base.Controls.Add(this.btnFontDisplay);
            base.Controls.Add(this.lblBackColor);
            base.Name = "FontColorControl";
            base.Size = new Size(0x14d, 50);
            ((ISupportInitialize) this.pbBackColor).EndInit();
            ((ISupportInitialize) this.pbForeColor).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void pbBackColor_BackColorChanged(object sender, EventArgs e)
        {
            if (this.BackColorSettingChanged != null)
            {
                this.BackColorSettingChanged(this.pbBackColor.BackColor);
            }
            this.btnFontDisplay.BackColor = this.pbBackColor.BackColor;
        }

        private void pbBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog {
                Color = this.pbBackColor.BackColor,
                SolidColorOnly = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.pbBackColor.BackColor = dialog.Color;
            }
            dialog.Dispose();
        }

        private void pbForeColor_BackColorChanged(object sender, EventArgs e)
        {
            if (this.ForeColorSettingChanged != null)
            {
                this.ForeColorSettingChanged(this.pbForeColor.BackColor);
            }
            this.btnFontDisplay.ForeColor = this.pbForeColor.BackColor;
        }

        private void pbForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog {
                Color = this.pbForeColor.BackColor,
                SolidColorOnly = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.pbForeColor.BackColor = dialog.Color;
            }
            dialog.Dispose();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true)]
        public Color BackColorSetting
        {
            get
            {
                return this.pbBackColor.BackColor;
            }
            set
            {
                this.pbBackColor.BackColor = value;
            }
        }

        public bool FontChangable
        {
            get
            {
                return this.fontChangable;
            }
            set
            {
                this.fontChangable = value;
                if (this.fontChangable)
                {
                    this.btnFontDisplay.Text = "Change Font";
                }
                else
                {
                    this.btnFontDisplay.Text = "Sample";
                }
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Font FontSetting
        {
            get
            {
                return this.btnFontDisplay.Font;
            }
            set
            {
                this.btnFontDisplay.Font = value;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ForeColorSetting
        {
            get
            {
                return this.pbForeColor.BackColor;
            }
            set
            {
                this.pbForeColor.BackColor = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true)]
        public override string Text
        {
            get
            {
                return this.lblText.Text;
            }
            set
            {
                this.lblText.Text = value;
            }
        }

        public delegate void ColorSettingEventDelegate(Color NewColor);

        public delegate void FontSettingEventDelegate(Font NewFont);
    }
}

