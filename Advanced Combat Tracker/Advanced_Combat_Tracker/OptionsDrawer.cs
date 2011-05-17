namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Windows.Forms;

    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class OptionsDrawer : UserControl
    {
        private IContainer components;
        private ImageList ilPlusMinus;
        private Label lblDrawerTitle;
        private PictureBox pbPlus;

        public OptionsDrawer()
        {
            this.InitializeComponent();
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
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(OptionsDrawer));
            this.ilPlusMinus = new ImageList(this.components);
            this.pbPlus = new PictureBox();
            this.lblDrawerTitle = new Label();
            ((ISupportInitialize) this.pbPlus).BeginInit();
            base.SuspendLayout();
            this.ilPlusMinus.ImageStream = (ImageListStreamer) manager.GetObject("ilPlusMinus.ImageStream");
            this.ilPlusMinus.TransparentColor = Color.Transparent;
            this.ilPlusMinus.Images.SetKeyName(0, "+.bmp");
            this.ilPlusMinus.Images.SetKeyName(1, "-.bmp");
            this.pbPlus.Location = new Point(4, 6);
            this.pbPlus.Name = "pbPlus";
            this.pbPlus.Size = new Size(9, 9);
            this.pbPlus.TabIndex = 0;
            this.pbPlus.TabStop = false;
            this.pbPlus.Click += new EventHandler(this.pbPlus_Click);
            this.lblDrawerTitle.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblDrawerTitle.AutoSize = true;
            this.lblDrawerTitle.Cursor = Cursors.Hand;
            this.lblDrawerTitle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblDrawerTitle.ForeColor = Color.DimGray;
            this.lblDrawerTitle.Location = new Point(0x10, 4);
            this.lblDrawerTitle.Name = "lblDrawerTitle";
            this.lblDrawerTitle.Size = new Size(0x20, 13);
            this.lblDrawerTitle.TabIndex = 5;
            this.lblDrawerTitle.Text = "Title";
            this.lblDrawerTitle.Click += new EventHandler(this.lblDrawerTitle_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.BorderStyle = BorderStyle.FixedSingle;
            base.Controls.Add(this.lblDrawerTitle);
            base.Controls.Add(this.pbPlus);
            base.Name = "OptionsDrawer";
            base.Size = new Size(0x181, 0xba);
            base.Load += new EventHandler(this.OptionsDrawer_Load);
            base.Leave += new EventHandler(this.OptionsDrawer_Leave);
            base.Enter += new EventHandler(this.OptionsDrawer_Enter);
            ((ISupportInitialize) this.pbPlus).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void lblDrawerTitle_Click(object sender, EventArgs e)
        {
            this.ToggleExpand();
        }

        private void OptionsDrawer_Enter(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Control;
            this.ForeColor = SystemColors.ControlText;
        }

        private void OptionsDrawer_Leave(object sender, EventArgs e)
        {
            this.BackColor = base.Parent.BackColor;
            this.ForeColor = Color.DimGray;
        }

        private void OptionsDrawer_Load(object sender, EventArgs e)
        {
            this.AutoSize = false;
            base.Height = 20;
            base.BorderStyle = BorderStyle.None;
            this.pbPlus.Image = this.ilPlusMinus.Images[0];
            this.ForeColor = Color.DimGray;
        }

        private void pbPlus_Click(object sender, EventArgs e)
        {
            this.ToggleExpand();
        }

        private void ToggleExpand()
        {
            if (this.AutoSize)
            {
                this.AutoSize = false;
                base.Height = 20;
                base.BorderStyle = BorderStyle.None;
                this.pbPlus.Image = this.ilPlusMinus.Images[0];
                base.Parent.Focus();
                this.BackColor = base.Parent.BackColor;
            }
            else
            {
                this.AutoSize = true;
                base.BorderStyle = BorderStyle.FixedSingle;
                this.pbPlus.Image = this.ilPlusMinus.Images[1];
                base.Focus();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true)]
        public override string Text
        {
            get
            {
                return this.lblDrawerTitle.Text;
            }
            set
            {
                this.lblDrawerTitle.Text = value;
            }
        }
    }
}

