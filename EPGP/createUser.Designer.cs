namespace EPGP
{
    partial class createUser
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
            this.createButton = new System.Windows.Forms.Button();
            this.lbl_pass = new System.Windows.Forms.Label();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.lbl_name = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.lbl_email = new System.Windows.Forms.Label();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.hiddenOKbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(100, 130);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(75, 25);
            this.createButton.TabIndex = 3;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // lbl_pass
            // 
            this.lbl_pass.AutoSize = true;
            this.lbl_pass.Location = new System.Drawing.Point(27, 57);
            this.lbl_pass.Name = "lbl_pass";
            this.lbl_pass.Size = new System.Drawing.Size(56, 13);
            this.lbl_pass.TabIndex = 7;
            this.lbl_pass.Text = "Password:";
            // 
            // txt_pass
            // 
            this.txt_pass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_pass.Location = new System.Drawing.Point(113, 55);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.Size = new System.Drawing.Size(135, 20);
            this.txt_pass.TabIndex = 1;
            this.txt_pass.UseSystemPasswordChar = true;
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(27, 27);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(58, 13);
            this.lbl_name.TabIndex = 10;
            this.lbl_name.Text = "Username:";
            // 
            // txt_name
            // 
            this.txt_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_name.Location = new System.Drawing.Point(113, 25);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(135, 20);
            this.txt_name.TabIndex = 0;
            // 
            // lbl_email
            // 
            this.lbl_email.AutoSize = true;
            this.lbl_email.Location = new System.Drawing.Point(27, 87);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(35, 13);
            this.lbl_email.TabIndex = 12;
            this.lbl_email.Text = "Email:";
            // 
            // txt_email
            // 
            this.txt_email.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_email.Location = new System.Drawing.Point(113, 85);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(135, 20);
            this.txt_email.TabIndex = 2;
            // 
            // hiddenOKbutton
            // 
            this.hiddenOKbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.hiddenOKbutton.Location = new System.Drawing.Point(346, 27);
            this.hiddenOKbutton.Name = "hiddenOKbutton";
            this.hiddenOKbutton.Size = new System.Drawing.Size(30, 30);
            this.hiddenOKbutton.TabIndex = 13;
            this.hiddenOKbutton.Text = "OK";
            this.hiddenOKbutton.UseVisualStyleBackColor = true;
            // 
            // createUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 182);
            this.Controls.Add(this.hiddenOKbutton);
            this.Controls.Add(this.lbl_email);
            this.Controls.Add(this.txt_email);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.lbl_pass);
            this.Controls.Add(this.txt_pass);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.txt_name);
            this.Name = "createUser";
            this.Text = "Create User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Label lbl_pass;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.Button hiddenOKbutton;
    }
}