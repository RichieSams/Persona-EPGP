namespace Attendance
{
    partial class guildManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(guildManagement));
            this.tabArea = new System.Windows.Forms.TabControl();
            this.EPGPtab = new System.Windows.Forms.TabPage();
            this.overlayButton = new System.Windows.Forms.Button();
            this.attendanceButton = new System.Windows.Forms.Button();
            this.tenEPbutton = new System.Windows.Forms.Button();
            this.fiveEPbutton = new System.Windows.Forms.Button();
            this.EPGPspreadsheet = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabArea.SuspendLayout();
            this.EPGPtab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EPGPspreadsheet)).BeginInit();
            this.SuspendLayout();
            // 
            // tabArea
            // 
            this.tabArea.Controls.Add(this.tabPage2);
            this.tabArea.Controls.Add(this.EPGPtab);
            this.tabArea.Location = new System.Drawing.Point(0, 0);
            this.tabArea.Name = "tabArea";
            this.tabArea.SelectedIndex = 0;
            this.tabArea.Size = new System.Drawing.Size(760, 615);
            this.tabArea.TabIndex = 0;
            // 
            // EPGPtab
            // 
            this.EPGPtab.Controls.Add(this.overlayButton);
            this.EPGPtab.Controls.Add(this.attendanceButton);
            this.EPGPtab.Controls.Add(this.tenEPbutton);
            this.EPGPtab.Controls.Add(this.fiveEPbutton);
            this.EPGPtab.Controls.Add(this.EPGPspreadsheet);
            this.EPGPtab.Location = new System.Drawing.Point(4, 22);
            this.EPGPtab.Name = "EPGPtab";
            this.EPGPtab.Padding = new System.Windows.Forms.Padding(3);
            this.EPGPtab.Size = new System.Drawing.Size(752, 589);
            this.EPGPtab.TabIndex = 0;
            this.EPGPtab.Text = "EPGP";
            this.EPGPtab.UseVisualStyleBackColor = true;
            // 
            // overlayButton
            // 
            this.overlayButton.Location = new System.Drawing.Point(549, 25);
            this.overlayButton.Name = "overlayButton";
            this.overlayButton.Size = new System.Drawing.Size(167, 81);
            this.overlayButton.TabIndex = 5;
            this.overlayButton.Text = "Toggle Overlay";
            this.overlayButton.UseVisualStyleBackColor = true;
            this.overlayButton.Click += new System.EventHandler(this.overlayButton_Click);
            this.overlayButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.overlayButton_MouseUp);
            // 
            // attendanceButton
            // 
            this.attendanceButton.Location = new System.Drawing.Point(586, 350);
            this.attendanceButton.Name = "attendanceButton";
            this.attendanceButton.Size = new System.Drawing.Size(97, 33);
            this.attendanceButton.TabIndex = 4;
            this.attendanceButton.Text = "Attendance";
            this.attendanceButton.UseVisualStyleBackColor = true;
            // 
            // tenEPbutton
            // 
            this.tenEPbutton.Location = new System.Drawing.Point(586, 311);
            this.tenEPbutton.Name = "tenEPbutton";
            this.tenEPbutton.Size = new System.Drawing.Size(97, 33);
            this.tenEPbutton.TabIndex = 3;
            this.tenEPbutton.Text = "+ 10 EP";
            this.tenEPbutton.UseVisualStyleBackColor = true;
            // 
            // fiveEPbutton
            // 
            this.fiveEPbutton.Location = new System.Drawing.Point(586, 272);
            this.fiveEPbutton.Name = "fiveEPbutton";
            this.fiveEPbutton.Size = new System.Drawing.Size(97, 33);
            this.fiveEPbutton.TabIndex = 2;
            this.fiveEPbutton.Text = "+5 EP";
            this.fiveEPbutton.UseVisualStyleBackColor = true;
            this.fiveEPbutton.Click += new System.EventHandler(this.fiveEPbutton_Click);
            // 
            // EPGPspreadsheet
            // 
            this.EPGPspreadsheet.AllowUserToAddRows = false;
            this.EPGPspreadsheet.AllowUserToOrderColumns = true;
            this.EPGPspreadsheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EPGPspreadsheet.Location = new System.Drawing.Point(8, 10);
            this.EPGPspreadsheet.Name = "EPGPspreadsheet";
            this.EPGPspreadsheet.RowHeadersVisible = false;
            this.EPGPspreadsheet.Size = new System.Drawing.Size(489, 573);
            this.EPGPspreadsheet.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(752, 589);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // guildManagement
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 612);
            this.Controls.Add(this.tabArea);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "guildManagement";
            this.Text = "Persona Guild Management";
            this.Load += new System.EventHandler(this.guildManagement_Load);
            this.tabArea.ResumeLayout(false);
            this.EPGPtab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EPGPspreadsheet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabArea;
        private System.Windows.Forms.TabPage EPGPtab;
        private System.Windows.Forms.Button attendanceButton;
        private System.Windows.Forms.Button tenEPbutton;
        private System.Windows.Forms.Button fiveEPbutton;
        private System.Windows.Forms.DataGridView EPGPspreadsheet;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button overlayButton;

    }
}

