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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(getGuild));
            this.cb_usOrEuro = new System.Windows.Forms.ComboBox();
            this.cb_shard = new System.Windows.Forms.ComboBox();
            this.cb_guild = new System.Windows.Forms.ComboBox();
            this.selectGuildButton = new System.Windows.Forms.Button();
            this.createGuildPopupButton = new System.Windows.Forms.Button();
            this.txt_createGuildName = new System.Windows.Forms.TextBox();
            this.createGuildButton = new System.Windows.Forms.Button();
            this.hiddenOKbutton = new System.Windows.Forms.Button();
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
            this.cb_guild.SelectedIndexChanged += new System.EventHandler(this.cb_guild_SelectedIndexChanged);
            // 
            // selectGuildButton
            // 
            this.selectGuildButton.Location = new System.Drawing.Point(13, 158);
            this.selectGuildButton.Name = "selectGuildButton";
            this.selectGuildButton.Size = new System.Drawing.Size(105, 25);
            this.selectGuildButton.TabIndex = 3;
            this.selectGuildButton.Text = "Select guild";
            this.selectGuildButton.UseVisualStyleBackColor = true;
            this.selectGuildButton.Click += new System.EventHandler(this.selectGuildButton_Click);
            // 
            // createGuildPopupButton
            // 
            this.createGuildPopupButton.Location = new System.Drawing.Point(140, 158);
            this.createGuildPopupButton.Name = "createGuildPopupButton";
            this.createGuildPopupButton.Size = new System.Drawing.Size(105, 25);
            this.createGuildPopupButton.TabIndex = 4;
            this.createGuildPopupButton.Text = "Create new guild";
            this.createGuildPopupButton.UseVisualStyleBackColor = true;
            this.createGuildPopupButton.Click += new System.EventHandler(this.createGuildPopupButton_Click);
            // 
            // txt_createGuildName
            // 
            this.txt_createGuildName.Location = new System.Drawing.Point(62, 115);
            this.txt_createGuildName.Name = "txt_createGuildName";
            this.txt_createGuildName.Size = new System.Drawing.Size(135, 20);
            this.txt_createGuildName.TabIndex = 5;
            this.txt_createGuildName.Visible = false;
            this.txt_createGuildName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_createGuildName_KeyDown);
            // 
            // createGuildButton
            // 
            this.createGuildButton.Location = new System.Drawing.Point(77, 158);
            this.createGuildButton.Name = "createGuildButton";
            this.createGuildButton.Size = new System.Drawing.Size(105, 25);
            this.createGuildButton.TabIndex = 6;
            this.createGuildButton.Text = "Create guild";
            this.createGuildButton.UseVisualStyleBackColor = true;
            this.createGuildButton.Visible = false;
            this.createGuildButton.Click += new System.EventHandler(this.createGuildButton_Click);
            // 
            // hiddenOKbutton
            // 
            this.hiddenOKbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.hiddenOKbutton.Location = new System.Drawing.Point(275, 250);
            this.hiddenOKbutton.Name = "hiddenOKbutton";
            this.hiddenOKbutton.Size = new System.Drawing.Size(30, 30);
            this.hiddenOKbutton.TabIndex = 7;
            this.hiddenOKbutton.Text = "OK";
            this.hiddenOKbutton.UseVisualStyleBackColor = true;
            // 
            // getGuild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 212);
            this.Controls.Add(this.hiddenOKbutton);
            this.Controls.Add(this.createGuildButton);
            this.Controls.Add(this.txt_createGuildName);
            this.Controls.Add(this.createGuildPopupButton);
            this.Controls.Add(this.selectGuildButton);
            this.Controls.Add(this.cb_guild);
            this.Controls.Add(this.cb_shard);
            this.Controls.Add(this.cb_usOrEuro);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "getGuild";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Guild";
            this.Load += new System.EventHandler(this.getGuild_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_usOrEuro;
        private System.Windows.Forms.ComboBox cb_shard;
        private System.Windows.Forms.ComboBox cb_guild;
        private System.Windows.Forms.Button selectGuildButton;
        private System.Windows.Forms.Button createGuildPopupButton;
        private System.Windows.Forms.TextBox txt_createGuildName;
        private System.Windows.Forms.Button createGuildButton;
        private System.Windows.Forms.Button hiddenOKbutton;
    }
}