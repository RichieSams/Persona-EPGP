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
            this.tabArea = new System.Windows.Forms.TabControl();
            this.EPGPtab = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.EPGPspreadsheet = new System.Windows.Forms.DataGridView();
            this.fiveEPbutton = new System.Windows.Forms.Button();
            this.tenEPbutton = new System.Windows.Forms.Button();
            this.attendanceButton = new System.Windows.Forms.Button();
            this.name_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EP_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GP_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PR_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.present_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabArea.SuspendLayout();
            this.EPGPtab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EPGPspreadsheet)).BeginInit();
            this.SuspendLayout();
            // 
            // tabArea
            // 
            this.tabArea.Controls.Add(this.EPGPtab);
            this.tabArea.Controls.Add(this.tabPage2);
            this.tabArea.Location = new System.Drawing.Point(0, 0);
            this.tabArea.Name = "tabArea";
            this.tabArea.SelectedIndex = 0;
            this.tabArea.Size = new System.Drawing.Size(760, 615);
            this.tabArea.TabIndex = 0;
            // 
            // EPGPtab
            // 
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
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(749, 589);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // EPGPspreadsheet
            // 
            this.EPGPspreadsheet.AllowUserToOrderColumns = true;
            this.EPGPspreadsheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EPGPspreadsheet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name_column,
            this.EP_column,
            this.GP_column,
            this.PR_column,
            this.present_column});
            this.EPGPspreadsheet.Location = new System.Drawing.Point(69, 10);
            this.EPGPspreadsheet.Name = "EPGPspreadsheet";
            this.EPGPspreadsheet.Size = new System.Drawing.Size(489, 573);
            this.EPGPspreadsheet.TabIndex = 1;
            // 
            // fiveEPbutton
            // 
            this.fiveEPbutton.Location = new System.Drawing.Point(607, 10);
            this.fiveEPbutton.Name = "fiveEPbutton";
            this.fiveEPbutton.Size = new System.Drawing.Size(97, 33);
            this.fiveEPbutton.TabIndex = 2;
            this.fiveEPbutton.Text = "+5 EP";
            this.fiveEPbutton.UseVisualStyleBackColor = true;
            // 
            // tenEPbutton
            // 
            this.tenEPbutton.Location = new System.Drawing.Point(607, 49);
            this.tenEPbutton.Name = "tenEPbutton";
            this.tenEPbutton.Size = new System.Drawing.Size(97, 33);
            this.tenEPbutton.TabIndex = 3;
            this.tenEPbutton.Text = "+ 10 EP";
            this.tenEPbutton.UseVisualStyleBackColor = true;
            // 
            // attendanceButton
            // 
            this.attendanceButton.Location = new System.Drawing.Point(607, 88);
            this.attendanceButton.Name = "attendanceButton";
            this.attendanceButton.Size = new System.Drawing.Size(97, 33);
            this.attendanceButton.TabIndex = 4;
            this.attendanceButton.Text = "Attendance";
            this.attendanceButton.UseVisualStyleBackColor = true;
            // 
            // name_column
            // 
            this.name_column.HeaderText = "Name";
            this.name_column.Name = "name_column";
            this.name_column.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.name_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.name_column.Width = 120;
            // 
            // EP_column
            // 
            this.EP_column.HeaderText = "EP";
            this.EP_column.Name = "EP_column";
            this.EP_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.EP_column.Width = 75;
            // 
            // GP_column
            // 
            this.GP_column.HeaderText = "GP";
            this.GP_column.Name = "GP_column";
            this.GP_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.GP_column.Width = 75;
            // 
            // PR_column
            // 
            this.PR_column.HeaderText = "PR";
            this.PR_column.Name = "PR_column";
            this.PR_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.PR_column.Width = 75;
            // 
            // present_column
            // 
            this.present_column.HeaderText = "Present";
            this.present_column.Name = "present_column";
            this.present_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // guildManagement
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 612);
            this.Controls.Add(this.tabArea);
            this.Name = "guildManagement";
            this.Text = "Persona Guild Management";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn name_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn EP_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn GP_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn PR_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn present_column;

    }
}

