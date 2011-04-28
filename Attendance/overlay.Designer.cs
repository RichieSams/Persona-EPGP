namespace Attendance
{
    partial class overlay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(overlay));
            this.lbl_overlayName = new System.Windows.Forms.Label();
            this.lbl_overlayPR = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_overlayName
            // 
            this.lbl_overlayName.AutoSize = true;
            this.lbl_overlayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_overlayName.Location = new System.Drawing.Point(27, 18);
            this.lbl_overlayName.Name = "lbl_overlayName";
            this.lbl_overlayName.Size = new System.Drawing.Size(98, 17);
            this.lbl_overlayName.TabIndex = 0;
            this.lbl_overlayName.Text = "Blazzinsouljah";
            // 
            // lbl_overlayPR
            // 
            this.lbl_overlayPR.AutoSize = true;
            this.lbl_overlayPR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_overlayPR.Location = new System.Drawing.Point(133, 18);
            this.lbl_overlayPR.Name = "lbl_overlayPR";
            this.lbl_overlayPR.Size = new System.Drawing.Size(44, 17);
            this.lbl_overlayPR.TabIndex = 1;
            this.lbl_overlayPR.Text = "21.65";
            // 
            // overlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(219, 172);
            this.Controls.Add(this.lbl_overlayPR);
            this.Controls.Add(this.lbl_overlayName);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "overlay";
            this.Opacity = 0.5D;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_overlayName;
        public System.Windows.Forms.Label lbl_overlayPR;
    }
}