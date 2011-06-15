namespace EPGP
{
    partial class getGuild
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
            this.cb_usOrEuro = new System.Windows.Forms.ComboBox();
            this.cb_shard = new System.Windows.Forms.ComboBox();
            this.cb_guild = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.createGuildButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_usOrEuro
            // 
            this.cb_usOrEuro.FormattingEnabled = true;
            this.cb_usOrEuro.Items.AddRange(new object[] {
            "US",
            "European"});
            this.cb_usOrEuro.Location = new System.Drawing.Point(62, 29);
            this.cb_usOrEuro.Name = "cb_usOrEuro";
            this.cb_usOrEuro.Size = new System.Drawing.Size(135, 21);
            this.cb_usOrEuro.TabIndex = 0;
            this.cb_usOrEuro.Text = "US or European";
            this.cb_usOrEuro.SelectedIndexChanged += new System.EventHandler(this.cb_usOrEuro_SelectedIndexChanged);
            // 
            // cb_shard
            // 
            this.cb_shard.FormattingEnabled = true;
            this.cb_shard.Location = new System.Drawing.Point(62, 72);
            this.cb_shard.Name = "cb_shard";
            this.cb_shard.Size = new System.Drawing.Size(135, 21);
            this.cb_shard.TabIndex = 1;
            this.cb_shard.Text = "Shard";
            this.cb_shard.SelectedIndexChanged += new System.EventHandler(this.cb_shard_SelectedIndexChanged);
            // 
            // cb_guild
            // 
            this.cb_guild.FormattingEnabled = true;
            this.cb_guild.Location = new System.Drawing.Point(62, 115);
            this.cb_guild.Name = "cb_guild";
            this.cb_guild.Size = new System.Drawing.Size(135, 21);
            this.cb_guild.TabIndex = 2;
            this.cb_guild.Text = "Guild";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(13, 158);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(105, 25);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "Select guild";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // createGuildButton
            // 
            this.createGuildButton.Location = new System.Drawing.Point(140, 158);
            this.createGuildButton.Name = "createGuildButton";
            this.createGuildButton.Size = new System.Drawing.Size(105, 25);
            this.createGuildButton.TabIndex = 4;
            this.createGuildButton.Text = "Create new guild";
            this.createGuildButton.UseVisualStyleBackColor = true;
            // 
            // getGuild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 212);
            this.Controls.Add(this.createGuildButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cb_guild);
            this.Controls.Add(this.cb_shard);
            this.Controls.Add(this.cb_usOrEuro);
            this.Name = "getGuild";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Guild";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_usOrEuro;
        private System.Windows.Forms.ComboBox cb_shard;
        private System.Windows.Forms.ComboBox cb_guild;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button createGuildButton;
    }
}