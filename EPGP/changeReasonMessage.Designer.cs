namespace EPGP
{
    partial class changeReasonMessage
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
            this.lbl_changeReason = new System.Windows.Forms.Label();
            this.reasonOkButton = new System.Windows.Forms.Button();
            this.txt_changeReason = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl_changeReason
            // 
            this.lbl_changeReason.AutoSize = true;
            this.lbl_changeReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_changeReason.Location = new System.Drawing.Point(18, 22);
            this.lbl_changeReason.Name = "lbl_changeReason";
            this.lbl_changeReason.Size = new System.Drawing.Size(249, 13);
            this.lbl_changeReason.TabIndex = 1;
            this.lbl_changeReason.Text = "Type the reason for the change (Boots, Chest, etc.)";
            // 
            // reasonOkButton
            // 
            this.reasonOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.reasonOkButton.Location = new System.Drawing.Point(107, 86);
            this.reasonOkButton.Name = "reasonOkButton";
            this.reasonOkButton.Size = new System.Drawing.Size(75, 25);
            this.reasonOkButton.TabIndex = 4;
            this.reasonOkButton.Text = "OK";
            this.reasonOkButton.UseVisualStyleBackColor = true;
            // 
            // txt_changeReason
            // 
            this.txt_changeReason.Location = new System.Drawing.Point(59, 52);
            this.txt_changeReason.Name = "txt_changeReason";
            this.txt_changeReason.Size = new System.Drawing.Size(170, 20);
            this.txt_changeReason.TabIndex = 3;
            this.txt_changeReason.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_changeReason_KeyDown);
            // 
            // changeReasonMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 132);
            this.Controls.Add(this.reasonOkButton);
            this.Controls.Add(this.txt_changeReason);
            this.Controls.Add(this.lbl_changeReason);
            this.Name = "changeReasonMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reason for Change";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_changeReason;
        private System.Windows.Forms.Button reasonOkButton;
        private System.Windows.Forms.TextBox txt_changeReason;
    }
}