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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.name_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EP_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GP_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PR_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.present_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
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
            this.dataGridView1.Location = new System.Drawing.Point(107, 21);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(544, 573);
            this.dataGridView1.TabIndex = 0;
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
            // EPGP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 614);
            this.Controls.Add(this.dataGridView1);
            this.Name = "EPGP";
            this.Text = "Persona Guild Management";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn name_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn EP_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn GP_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn PR_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn present_column;
    }
}

