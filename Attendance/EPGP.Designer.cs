﻿namespace Attendance
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
            this.mainTab = new System.Windows.Forms.TabPage();
            this.lbl_logReminder = new System.Windows.Forms.Label();
            this.lbl_rightClickText = new System.Windows.Forms.Label();
            this.lbl_leftClickText = new System.Windows.Forms.Label();
            this.lbl_GPminValue = new System.Windows.Forms.Label();
            this.lbl_GPminTitle = new System.Windows.Forms.Label();
            this.lbl_decayValue = new System.Windows.Forms.Label();
            this.lbl_decayTitle = new System.Windows.Forms.Label();
            this.lbl_EPgainsValues = new System.Windows.Forms.Label();
            this.lbl_EPgainsNames = new System.Windows.Forms.Label();
            this.lbl_EPgainsTitle = new System.Windows.Forms.Label();
            this.lbl_itemCostValues = new System.Windows.Forms.Label();
            this.lbl_itemCostNames = new System.Windows.Forms.Label();
            this.lbl_itemCostTitle = new System.Windows.Forms.Label();
            this.overlayButton = new System.Windows.Forms.Button();
            this.attendanceButton = new System.Windows.Forms.Button();
            this.tenEPbutton = new System.Windows.Forms.Button();
            this.fiveEPbutton = new System.Windows.Forms.Button();
            this.EPGPspreadsheet = new System.Windows.Forms.DataGridView();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.lbl_pass = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.PRsortButton = new System.Windows.Forms.Button();
            this.alphaSortButton = new System.Windows.Forms.Button();
            this.lbl_sort = new System.Windows.Forms.Label();
            this.tabArea.SuspendLayout();
            this.mainTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EPGPspreadsheet)).BeginInit();
            this.SuspendLayout();
            // 
            // tabArea
            // 
            this.tabArea.Controls.Add(this.mainTab);
            this.tabArea.Location = new System.Drawing.Point(2, 1);
            this.tabArea.Name = "tabArea";
            this.tabArea.SelectedIndex = 0;
            this.tabArea.Size = new System.Drawing.Size(755, 610);
            this.tabArea.TabIndex = 0;
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.lbl_sort);
            this.mainTab.Controls.Add(this.alphaSortButton);
            this.mainTab.Controls.Add(this.PRsortButton);
            this.mainTab.Controls.Add(this.lbl_logReminder);
            this.mainTab.Controls.Add(this.lbl_rightClickText);
            this.mainTab.Controls.Add(this.lbl_leftClickText);
            this.mainTab.Controls.Add(this.lbl_GPminValue);
            this.mainTab.Controls.Add(this.lbl_GPminTitle);
            this.mainTab.Controls.Add(this.lbl_decayValue);
            this.mainTab.Controls.Add(this.lbl_decayTitle);
            this.mainTab.Controls.Add(this.lbl_EPgainsValues);
            this.mainTab.Controls.Add(this.lbl_EPgainsNames);
            this.mainTab.Controls.Add(this.lbl_EPgainsTitle);
            this.mainTab.Controls.Add(this.lbl_itemCostValues);
            this.mainTab.Controls.Add(this.lbl_itemCostNames);
            this.mainTab.Controls.Add(this.lbl_itemCostTitle);
            this.mainTab.Controls.Add(this.overlayButton);
            this.mainTab.Controls.Add(this.attendanceButton);
            this.mainTab.Controls.Add(this.tenEPbutton);
            this.mainTab.Controls.Add(this.fiveEPbutton);
            this.mainTab.Controls.Add(this.EPGPspreadsheet);
            this.mainTab.Location = new System.Drawing.Point(4, 22);
            this.mainTab.Name = "mainTab";
            this.mainTab.Padding = new System.Windows.Forms.Padding(3);
            this.mainTab.Size = new System.Drawing.Size(747, 584);
            this.mainTab.TabIndex = 0;
            this.mainTab.Text = "Main";
            this.mainTab.UseVisualStyleBackColor = true;
            // 
            // lbl_logReminder
            // 
            this.lbl_logReminder.AutoSize = true;
            this.lbl_logReminder.Location = new System.Drawing.Point(552, 48);
            this.lbl_logReminder.Name = "lbl_logReminder";
            this.lbl_logReminder.Size = new System.Drawing.Size(155, 13);
            this.lbl_logReminder.TabIndex = 18;
            this.lbl_logReminder.Text = "Remember to type /log in-game";
            // 
            // lbl_rightClickText
            // 
            this.lbl_rightClickText.AutoSize = true;
            this.lbl_rightClickText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rightClickText.Location = new System.Drawing.Point(634, 30);
            this.lbl_rightClickText.Name = "lbl_rightClickText";
            this.lbl_rightClickText.Size = new System.Drawing.Size(108, 12);
            this.lbl_rightClickText.TabIndex = 17;
            this.lbl_rightClickText.Text = "Right click: Toggle border";
            // 
            // lbl_leftClickText
            // 
            this.lbl_leftClickText.AutoSize = true;
            this.lbl_leftClickText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_leftClickText.Location = new System.Drawing.Point(634, 11);
            this.lbl_leftClickText.Name = "lbl_leftClickText";
            this.lbl_leftClickText.Size = new System.Drawing.Size(109, 12);
            this.lbl_leftClickText.TabIndex = 16;
            this.lbl_leftClickText.Text = "Left click: Toggle window";
            // 
            // lbl_GPminValue
            // 
            this.lbl_GPminValue.AutoSize = true;
            this.lbl_GPminValue.Location = new System.Drawing.Point(665, 375);
            this.lbl_GPminValue.Name = "lbl_GPminValue";
            this.lbl_GPminValue.Size = new System.Drawing.Size(31, 13);
            this.lbl_GPminValue.TabIndex = 15;
            this.lbl_GPminValue.Text = "5 GP";
            // 
            // lbl_GPminTitle
            // 
            this.lbl_GPminTitle.AutoSize = true;
            this.lbl_GPminTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_GPminTitle.Location = new System.Drawing.Point(646, 358);
            this.lbl_GPminTitle.Name = "lbl_GPminTitle";
            this.lbl_GPminTitle.Size = new System.Drawing.Size(80, 13);
            this.lbl_GPminTitle.TabIndex = 14;
            this.lbl_GPminTitle.Text = "GP Minimum:";
            // 
            // lbl_decayValue
            // 
            this.lbl_decayValue.AutoSize = true;
            this.lbl_decayValue.Location = new System.Drawing.Point(525, 375);
            this.lbl_decayValue.Name = "lbl_decayValue";
            this.lbl_decayValue.Size = new System.Drawing.Size(94, 13);
            this.lbl_decayValue.TabIndex = 13;
            this.lbl_decayValue.Text = "7% every Tuesday";
            // 
            // lbl_decayTitle
            // 
            this.lbl_decayTitle.AutoSize = true;
            this.lbl_decayTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_decayTitle.Location = new System.Drawing.Point(533, 358);
            this.lbl_decayTitle.Name = "lbl_decayTitle";
            this.lbl_decayTitle.Size = new System.Drawing.Size(90, 13);
            this.lbl_decayTitle.TabIndex = 12;
            this.lbl_decayTitle.Text = "EP/GP Decay:";
            // 
            // lbl_EPgainsValues
            // 
            this.lbl_EPgainsValues.AutoSize = true;
            this.lbl_EPgainsValues.Location = new System.Drawing.Point(661, 278);
            this.lbl_EPgainsValues.Name = "lbl_EPgainsValues";
            this.lbl_EPgainsValues.Size = new System.Drawing.Size(53, 52);
            this.lbl_EPgainsValues.TabIndex = 11;
            this.lbl_EPgainsValues.Text = "5 EP\r\n10 EP\r\n5 EP\r\n10 EP/hr.";
            // 
            // lbl_EPgainsNames
            // 
            this.lbl_EPgainsNames.AutoSize = true;
            this.lbl_EPgainsNames.Location = new System.Drawing.Point(535, 278);
            this.lbl_EPgainsNames.Name = "lbl_EPgainsNames";
            this.lbl_EPgainsNames.Size = new System.Drawing.Size(124, 52);
            this.lbl_EPgainsNames.TabIndex = 10;
            this.lbl_EPgainsNames.Text = "On time w/ consumables\r\nBoss kill, 1st time\r\nBoss kill, farm\r\nProgression";
            // 
            // lbl_EPgainsTitle
            // 
            this.lbl_EPgainsTitle.AutoSize = true;
            this.lbl_EPgainsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EPgainsTitle.Location = new System.Drawing.Point(597, 261);
            this.lbl_EPgainsTitle.Name = "lbl_EPgainsTitle";
            this.lbl_EPgainsTitle.Size = new System.Drawing.Size(63, 13);
            this.lbl_EPgainsTitle.TabIndex = 9;
            this.lbl_EPgainsTitle.Text = "EP Gains:";
            // 
            // lbl_itemCostValues
            // 
            this.lbl_itemCostValues.AutoSize = true;
            this.lbl_itemCostValues.Location = new System.Drawing.Point(635, 110);
            this.lbl_itemCostValues.Name = "lbl_itemCostValues";
            this.lbl_itemCostValues.Size = new System.Drawing.Size(52, 130);
            this.lbl_itemCostValues.TabIndex = 8;
            this.lbl_itemCostValues.Text = "5 GP\r\n10 GP\r\n5 GP\r\n5 GP\r\n5 GP\r\n5 GP\r\n10 GP\r\n10 GP\r\n5 GP\r\n2x normal";
            // 
            // lbl_itemCostNames
            // 
            this.lbl_itemCostNames.AutoSize = true;
            this.lbl_itemCostNames.Location = new System.Drawing.Point(566, 110);
            this.lbl_itemCostNames.Name = "lbl_itemCostNames";
            this.lbl_itemCostNames.Size = new System.Drawing.Size(65, 130);
            this.lbl_itemCostNames.TabIndex = 7;
            this.lbl_itemCostNames.Text = "Armor\r\n2H\r\nMain Hands\r\nOff Hands\r\nRanged\r\nRings\r\nTrinkets\r\nGreaters\r\nLessers\r\nLeg" +
                "endaries\r\n";
            // 
            // lbl_itemCostTitle
            // 
            this.lbl_itemCostTitle.AutoSize = true;
            this.lbl_itemCostTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_itemCostTitle.Location = new System.Drawing.Point(595, 93);
            this.lbl_itemCostTitle.Name = "lbl_itemCostTitle";
            this.lbl_itemCostTitle.Size = new System.Drawing.Size(70, 13);
            this.lbl_itemCostTitle.TabIndex = 6;
            this.lbl_itemCostTitle.Text = "Item Costs:";
            // 
            // overlayButton
            // 
            this.overlayButton.Location = new System.Drawing.Point(504, 10);
            this.overlayButton.Name = "overlayButton";
            this.overlayButton.Size = new System.Drawing.Size(127, 35);
            this.overlayButton.TabIndex = 6;
            this.overlayButton.Text = "Toggle Overlay";
            this.overlayButton.UseVisualStyleBackColor = true;
            this.overlayButton.Click += new System.EventHandler(this.overlayButton_Click);
            this.overlayButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.overlayButton_MouseUp);
            // 
            // attendanceButton
            // 
            this.attendanceButton.Location = new System.Drawing.Point(519, 508);
            this.attendanceButton.Name = "attendanceButton";
            this.attendanceButton.Size = new System.Drawing.Size(97, 33);
            this.attendanceButton.TabIndex = 6;
            this.attendanceButton.Text = "Attendance";
            this.attendanceButton.UseVisualStyleBackColor = true;
            this.attendanceButton.Visible = false;
            this.attendanceButton.Click += new System.EventHandler(this.attendanceButton_Click);
            // 
            // tenEPbutton
            // 
            this.tenEPbutton.Location = new System.Drawing.Point(519, 469);
            this.tenEPbutton.Name = "tenEPbutton";
            this.tenEPbutton.Size = new System.Drawing.Size(97, 33);
            this.tenEPbutton.TabIndex = 6;
            this.tenEPbutton.Text = "+ 10 EP";
            this.tenEPbutton.UseVisualStyleBackColor = true;
            this.tenEPbutton.Visible = false;
            // 
            // fiveEPbutton
            // 
            this.fiveEPbutton.Location = new System.Drawing.Point(519, 430);
            this.fiveEPbutton.Name = "fiveEPbutton";
            this.fiveEPbutton.Size = new System.Drawing.Size(97, 33);
            this.fiveEPbutton.TabIndex = 6;
            this.fiveEPbutton.Text = "+5 EP";
            this.fiveEPbutton.UseVisualStyleBackColor = true;
            this.fiveEPbutton.Visible = false;
            this.fiveEPbutton.Click += new System.EventHandler(this.fiveEPbutton_Click);
            // 
            // EPGPspreadsheet
            // 
            this.EPGPspreadsheet.AllowUserToAddRows = false;
            this.EPGPspreadsheet.AllowUserToOrderColumns = true;
            this.EPGPspreadsheet.AllowUserToResizeColumns = false;
            this.EPGPspreadsheet.AllowUserToResizeRows = false;
            this.EPGPspreadsheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EPGPspreadsheet.Location = new System.Drawing.Point(8, 10);
            this.EPGPspreadsheet.Name = "EPGPspreadsheet";
            this.EPGPspreadsheet.RowHeadersVisible = false;
            this.EPGPspreadsheet.Size = new System.Drawing.Size(489, 573);
            this.EPGPspreadsheet.TabIndex = 1;
            // 
            // txt_name
            // 
            this.txt_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_name.Location = new System.Drawing.Point(425, 0);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(105, 20);
            this.txt_name.TabIndex = 1;
            // 
            // txt_pass
            // 
            this.txt_pass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_pass.Location = new System.Drawing.Point(586, 0);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.Size = new System.Drawing.Size(105, 20);
            this.txt_pass.TabIndex = 2;
            this.txt_pass.UseSystemPasswordChar = true;
            this.txt_pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_pass_KeyDown);
            // 
            // lbl_pass
            // 
            this.lbl_pass.AutoSize = true;
            this.lbl_pass.Location = new System.Drawing.Point(530, 4);
            this.lbl_pass.Name = "lbl_pass";
            this.lbl_pass.Size = new System.Drawing.Size(56, 13);
            this.lbl_pass.TabIndex = 3;
            this.lbl_pass.Text = "Password:";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(367, 4);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(58, 13);
            this.lbl_name.TabIndex = 4;
            this.lbl_name.Text = "Username:";
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(691, -1);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(65, 22);
            this.loginButton.TabIndex = 3;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // PRsortButton
            // 
            this.PRsortButton.Location = new System.Drawing.Point(640, 454);
            this.PRsortButton.Name = "PRsortButton";
            this.PRsortButton.Size = new System.Drawing.Size(100, 30);
            this.PRsortButton.TabIndex = 19;
            this.PRsortButton.Text = "PR";
            this.PRsortButton.UseVisualStyleBackColor = true;
            this.PRsortButton.Visible = false;
            this.PRsortButton.Click += new System.EventHandler(this.PRsortButton_Click);
            // 
            // alphaSortButton
            // 
            this.alphaSortButton.Location = new System.Drawing.Point(640, 493);
            this.alphaSortButton.Name = "alphaSortButton";
            this.alphaSortButton.Size = new System.Drawing.Size(100, 30);
            this.alphaSortButton.TabIndex = 20;
            this.alphaSortButton.Text = "Alphabetical";
            this.alphaSortButton.UseVisualStyleBackColor = true;
            this.alphaSortButton.Visible = false;
            this.alphaSortButton.Click += new System.EventHandler(this.alphaSortButton_Click);
            // 
            // lbl_sort
            // 
            this.lbl_sort.AutoSize = true;
            this.lbl_sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_sort.Location = new System.Drawing.Point(647, 434);
            this.lbl_sort.Name = "lbl_sort";
            this.lbl_sort.Size = new System.Drawing.Size(87, 13);
            this.lbl_sort.TabIndex = 21;
            this.lbl_sort.Text = "Sort Table by:";
            this.lbl_sort.Visible = false;
            // 
            // guildManagement
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 612);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.lbl_pass);
            this.Controls.Add(this.txt_pass);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.tabArea);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "guildManagement";
            this.Text = "Persona Guild Management";
            this.Load += new System.EventHandler(this.guildManagement_Load);
            this.tabArea.ResumeLayout(false);
            this.mainTab.ResumeLayout(false);
            this.mainTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EPGPspreadsheet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabArea;
        private System.Windows.Forms.TabPage mainTab;
        private System.Windows.Forms.Button attendanceButton;
        private System.Windows.Forms.Button tenEPbutton;
        private System.Windows.Forms.Button fiveEPbutton;
        private System.Windows.Forms.DataGridView EPGPspreadsheet;
        private System.Windows.Forms.Button overlayButton;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.Label lbl_pass;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label lbl_itemCostNames;
        private System.Windows.Forms.Label lbl_itemCostTitle;
        private System.Windows.Forms.Label lbl_itemCostValues;
        private System.Windows.Forms.Label lbl_EPgainsNames;
        private System.Windows.Forms.Label lbl_EPgainsTitle;
        private System.Windows.Forms.Label lbl_decayTitle;
        private System.Windows.Forms.Label lbl_EPgainsValues;
        private System.Windows.Forms.Label lbl_GPminValue;
        private System.Windows.Forms.Label lbl_GPminTitle;
        private System.Windows.Forms.Label lbl_decayValue;
        private System.Windows.Forms.Label lbl_rightClickText;
        private System.Windows.Forms.Label lbl_leftClickText;
        private System.Windows.Forms.Label lbl_logReminder;
        private System.Windows.Forms.Label lbl_sort;
        private System.Windows.Forms.Button alphaSortButton;
        private System.Windows.Forms.Button PRsortButton;
    }
}

