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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(overlay));
            this.SuspendLayout();
            // 
            // overlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 210);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            //this.FormBorderStyle = FormBorderStyle.None;  //This wont work for some reason.
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "overlay";
            this.ShowInTaskbar = false;
            this.Text = "overlay";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
    }
}