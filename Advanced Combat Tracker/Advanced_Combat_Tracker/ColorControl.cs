namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class ColorControl : UserControl
    {
        private IContainer components;
        private ColorSettingEventDelegate ForeColorSettingChanged;
        private Label lblText;
        private PictureBox pbForeColor;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true)]
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

        public ColorControl()
        {
            this.InitializeComponent();
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
            this.lblText = new Label();
            this.pbForeColor = new PictureBox();
            ((ISupportInitialize) this.pbForeColor).BeginInit();
            base.SuspendLayout();
            this.lblText.AutoSize = true;
            this.lblText.Location = new Point(0x21, 9);
            this.lblText.Name = "lblText";
            this.lblText.Size = new Size(0x57, 13);
            this.lblText.TabIndex = 5;
            this.lblText.Text = "Color Description";
            this.lblText.MouseHover += new EventHandler(this.control_mousehover);
            this.pbForeColor.BackColor = Color.Black;
            this.pbForeColor.BorderStyle = BorderStyle.Fixed3D;
            this.pbForeColor.Cursor = Cursors.Hand;
            this.pbForeColor.Location = new Point(3, 3);
            this.pbForeColor.Name = "pbForeColor";
            this.pbForeColor.Size = new Size(0x18, 0x18);
            this.pbForeColor.TabIndex = 4;
            this.pbForeColor.TabStop = false;
            this.pbForeColor.BackColorChanged += new EventHandler(this.pbForeColor_BackColorChanged);
            this.pbForeColor.Click += new EventHandler(this.pbForeColor_Click);
            this.pbForeColor.MouseHover += new EventHandler(this.control_mousehover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.lblText);
            base.Controls.Add(this.pbForeColor);
            base.Name = "ColorControl";
            base.Size = new Size(0x7b, 30);
            ((ISupportInitialize) this.pbForeColor).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void pbForeColor_BackColorChanged(object sender, EventArgs e)
        {
            if (this.ForeColorSettingChanged != null)
            {
                this.ForeColorSettingChanged(this.pbForeColor.BackColor);
            }
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
    }
}

