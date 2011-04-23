namespace Attendance
{
    partial class EPGP
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
            this.EPGPtab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.name_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EP_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GP_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PR_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.present_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fiveEPbutton = new System.Windows.Forms.Button();
            this.tenEPbutton = new System.Windows.Forms.Button();
            this.attendanceButton = new System.Windows.Forms.Button();
            this.EPGPtab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // EPGPtab
            // 
            this.EPGPtab.Controls.Add(this.tabPage1);
            this.EPGPtab.Controls.Add(this.tabPage2);
            this.EPGPtab.Location = new System.Drawing.Point(0, 0);
            this.EPGPtab.Name = "EPGPtab";
            this.EPGPtab.SelectedIndex = 0;
            this.EPGPtab.Size = new System.Drawing.Size(759, 615);
            this.EPGPtab.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.attendanceButton);
            this.tabPage1.Controls.Add(this.tenEPbutton);
            this.tabPage1.Controls.Add(this.fiveEPbutton);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(751, 589);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "EPGP";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(751, 589);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name_column,
            this.EP_column,
            this.GP_column,
            this.PR_column,
            this.present_column});
            this.dataGridView1.Location = new System.Drawing.Point(35, 8);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(544, 573);
            this.dataGridView1.TabIndex = 1;
            // 
            // name_column
            // 
            this.name_column.HeaderText = "Name";
            this.name_column.Name = "name_column";
            this.name_column.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.name_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // EP_column
            // 
            this.EP_column.HeaderText = "EP";
            this.EP_column.Name = "EP_column";
            this.EP_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // GP_column
            // 
            this.GP_column.HeaderText = "GP";
            this.GP_column.Name = "GP_column";
            this.GP_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // PR_column
            // 
            this.PR_column.HeaderText = "PR";
            this.PR_column.Name = "PR_column";
            this.PR_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // present_column
            // 
            this.present_column.HeaderText = "Present";
            this.present_column.Name = "present_column";
            this.present_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // fiveEPbutton
            // 
            this.fiveEPbutton.Location = new System.Drawing.Point(618, 8);
            this.fiveEPbutton.Name = "fiveEPbutton";
            this.fiveEPbutton.Size = new System.Drawing.Size(97, 33);
            this.fiveEPbutton.TabIndex = 2;
            this.fiveEPbutton.Text = "+5 EP";
            this.fiveEPbutton.UseVisualStyleBackColor = true;
            // 
            // tenEPbutton
            // 
            this.tenEPbutton.Location = new System.Drawing.Point(618, 47);
            this.tenEPbutton.Name = "tenEPbutton";
            this.tenEPbutton.Size = new System.Drawing.Size(97, 33);
            this.tenEPbutton.TabIndex = 3;
            this.tenEPbutton.Text = "+ 10 EP";
            this.tenEPbutton.UseVisualStyleBackColor = true;
            // 
            // attendanceButton
            // 
            this.attendanceButton.Location = new System.Drawing.Point(618, 86);
            this.attendanceButton.Name = "attendanceButton";
            this.attendanceButton.Size = new System.Drawing.Size(97, 33);
            this.attendanceButton.TabIndex = 4;
            this.attendanceButton.Text = "Attendance";
            this.attendanceButton.UseVisualStyleBackColor = true;
            // 
            // EPGP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 614);
            this.Controls.Add(this.EPGPtab);
            this.Name = "EPGP";
            this.Text = "Persona Guild Management";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.EPGPtab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl EPGPtab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button attendanceButton;
        private System.Windows.Forms.Button tenEPbutton;
        private System.Windows.Forms.Button fiveEPbutton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn name_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn EP_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn GP_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn PR_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn present_column;
        private System.Windows.Forms.TabPage tabPage2;

    }
}

