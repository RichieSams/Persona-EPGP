﻿namespace EPGP
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
            this.logWatch = new System.Timers.Timer();
            this.tabArea = new System.Windows.Forms.TabControl();
            this.infoTab = new System.Windows.Forms.TabPage();
            this.lbl_member = new System.Windows.Forms.Label();
            this.lbl_raider = new System.Windows.Forms.Label();
            this.lbl_blackKey = new System.Windows.Forms.Label();
            this.lbl_blueKey = new System.Windows.Forms.Label();
            this.lbl_keyTitle = new System.Windows.Forms.Label();
            this.lbl_logWarningValue = new System.Windows.Forms.Label();
            this.lbl_logWarningTitle = new System.Windows.Forms.Label();
            this.lbl_currentZoneValue = new System.Windows.Forms.Label();
            this.lbl_currentZoneTitle = new System.Windows.Forms.Label();
            this.lbl_webLink = new System.Windows.Forms.LinkLabel();
            this.lbl_learnMore = new System.Windows.Forms.Label();
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
            this.adminTab = new System.Windows.Forms.TabPage();
            this.raiderStatusButton = new System.Windows.Forms.Button();
            this.onTimeButton = new System.Windows.Forms.Button();
            this.undoButton = new System.Windows.Forms.Button();
            this.lbl_raidxmlDate = new System.Windows.Forms.Label();
            this.lbl_admin_users = new System.Windows.Forms.Label();
            this.lbl_raidxmlTitle = new System.Windows.Forms.Label();
            this.lbl_admin_epfunc = new System.Windows.Forms.Label();
            this.lbl_admin_login = new System.Windows.Forms.Label();
            this.deleteUserButton = new System.Windows.Forms.Button();
            this.addUserButton = new System.Windows.Forms.Button();
            this.lbl_loggedIn = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.attendanceButton = new System.Windows.Forms.Button();
            this.lbl_pass = new System.Windows.Forms.Label();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.lbl_name = new System.Windows.Forms.Label();
            this.tenEPbutton = new System.Windows.Forms.Button();
            this.fiveEPbutton = new System.Windows.Forms.Button();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.settingsTab = new System.Windows.Forms.TabPage();
            this.lbl_currentGuildValue = new System.Windows.Forms.Label();
            this.lbl_currentGuildTitle = new System.Windows.Forms.Label();
            this.getGuildButton = new System.Windows.Forms.Button();
            this.lbl_currentDirValue = new System.Windows.Forms.Label();
            this.lbl_currentDirTitle = new System.Windows.Forms.Label();
            this.getDirButton = new System.Windows.Forms.Button();
            this.lbl_opacity = new System.Windows.Forms.Label();
            this.txt_opacity = new System.Windows.Forms.TextBox();
            this.opacitySlider = new System.Windows.Forms.TrackBar();
            this.lbl_logReminder = new System.Windows.Forms.Label();
            this.lbl_rightClickText = new System.Windows.Forms.Label();
            this.lbl_leftClickText = new System.Windows.Forms.Label();
            this.overlayButton = new System.Windows.Forms.Button();
            this.logTab = new System.Windows.Forms.TabPage();
            this.logSpreadsheet = new DevExpress.XtraTreeList.TreeList();
            this.Member = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Number = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Type = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Reason = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.lbl_sort1 = new System.Windows.Forms.Label();
            this.alphaSortButton = new System.Windows.Forms.Button();
            this.PRsortButton = new System.Windows.Forms.Button();
            this.EPGPspreadsheet = new System.Windows.Forms.DataGridView();
            this.btn_refreshTbl = new System.Windows.Forms.Button();
            this.overlayReset = new System.Timers.Timer();
            this.LPRsortButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logWatch)).BeginInit();
            this.tabArea.SuspendLayout();
            this.infoTab.SuspendLayout();
            this.adminTab.SuspendLayout();
            this.settingsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacitySlider)).BeginInit();
            this.logTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logSpreadsheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EPGPspreadsheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.overlayReset)).BeginInit();
            this.SuspendLayout();
            // 
            // logWatch
            // 
            this.logWatch.Interval = 60000D;
            this.logWatch.SynchronizingObject = this;
            this.logWatch.Elapsed += new System.Timers.ElapsedEventHandler(this.logWatchElapse);
            // 
            // tabArea
            // 
            this.tabArea.Controls.Add(this.infoTab);
            this.tabArea.Controls.Add(this.adminTab);
            this.tabArea.Controls.Add(this.settingsTab);
            this.tabArea.Controls.Add(this.logTab);
            this.tabArea.Location = new System.Drawing.Point(496, 4);
            this.tabArea.Name = "tabArea";
            this.tabArea.SelectedIndex = 0;
            this.tabArea.Size = new System.Drawing.Size(260, 603);
            this.tabArea.TabIndex = 0;
            // 
            // infoTab
            // 
            this.infoTab.Controls.Add(this.lbl_member);
            this.infoTab.Controls.Add(this.lbl_raider);
            this.infoTab.Controls.Add(this.lbl_blackKey);
            this.infoTab.Controls.Add(this.lbl_blueKey);
            this.infoTab.Controls.Add(this.lbl_keyTitle);
            this.infoTab.Controls.Add(this.lbl_logWarningValue);
            this.infoTab.Controls.Add(this.lbl_logWarningTitle);
            this.infoTab.Controls.Add(this.lbl_currentZoneValue);
            this.infoTab.Controls.Add(this.lbl_currentZoneTitle);
            this.infoTab.Controls.Add(this.lbl_webLink);
            this.infoTab.Controls.Add(this.lbl_learnMore);
            this.infoTab.Controls.Add(this.lbl_GPminValue);
            this.infoTab.Controls.Add(this.lbl_GPminTitle);
            this.infoTab.Controls.Add(this.lbl_decayValue);
            this.infoTab.Controls.Add(this.lbl_decayTitle);
            this.infoTab.Controls.Add(this.lbl_EPgainsValues);
            this.infoTab.Controls.Add(this.lbl_EPgainsNames);
            this.infoTab.Controls.Add(this.lbl_EPgainsTitle);
            this.infoTab.Controls.Add(this.lbl_itemCostValues);
            this.infoTab.Controls.Add(this.lbl_itemCostNames);
            this.infoTab.Controls.Add(this.lbl_itemCostTitle);
            this.infoTab.Location = new System.Drawing.Point(4, 22);
            this.infoTab.Name = "infoTab";
            this.infoTab.Padding = new System.Windows.Forms.Padding(3);
            this.infoTab.Size = new System.Drawing.Size(252, 577);
            this.infoTab.TabIndex = 0;
            this.infoTab.Text = "Info";
            this.infoTab.UseVisualStyleBackColor = true;
            // 
            // lbl_member
            // 
            this.lbl_member.AutoSize = true;
            this.lbl_member.Location = new System.Drawing.Point(112, 372);
            this.lbl_member.Name = "lbl_member";
            this.lbl_member.Size = new System.Drawing.Size(45, 13);
            this.lbl_member.TabIndex = 36;
            this.lbl_member.Text = "Member";
            // 
            // lbl_raider
            // 
            this.lbl_raider.AutoSize = true;
            this.lbl_raider.Location = new System.Drawing.Point(112, 354);
            this.lbl_raider.Name = "lbl_raider";
            this.lbl_raider.Size = new System.Drawing.Size(38, 13);
            this.lbl_raider.TabIndex = 35;
            this.lbl_raider.Text = "Raider";
            // 
            // lbl_blackKey
            // 
            this.lbl_blackKey.AutoSize = true;
            this.lbl_blackKey.BackColor = System.Drawing.Color.Black;
            this.lbl_blackKey.Location = new System.Drawing.Point(96, 372);
            this.lbl_blackKey.Name = "lbl_blackKey";
            this.lbl_blackKey.Size = new System.Drawing.Size(10, 13);
            this.lbl_blackKey.TabIndex = 34;
            this.lbl_blackKey.Text = " ";
            // 
            // lbl_blueKey
            // 
            this.lbl_blueKey.AutoSize = true;
            this.lbl_blueKey.BackColor = System.Drawing.Color.Blue;
            this.lbl_blueKey.Location = new System.Drawing.Point(96, 354);
            this.lbl_blueKey.Name = "lbl_blueKey";
            this.lbl_blueKey.Size = new System.Drawing.Size(10, 13);
            this.lbl_blueKey.TabIndex = 33;
            this.lbl_blueKey.Text = " ";
            // 
            // lbl_keyTitle
            // 
            this.lbl_keyTitle.AutoSize = true;
            this.lbl_keyTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_keyTitle.Location = new System.Drawing.Point(110, 335);
            this.lbl_keyTitle.Name = "lbl_keyTitle";
            this.lbl_keyTitle.Size = new System.Drawing.Size(32, 13);
            this.lbl_keyTitle.TabIndex = 32;
            this.lbl_keyTitle.Text = "Key:";
            // 
            // lbl_logWarningValue
            // 
            this.lbl_logWarningValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_logWarningValue.AutoSize = true;
            this.lbl_logWarningValue.ForeColor = System.Drawing.Color.Red;
            this.lbl_logWarningValue.Location = new System.Drawing.Point(64, 539);
            this.lbl_logWarningValue.Name = "lbl_logWarningValue";
            this.lbl_logWarningValue.Size = new System.Drawing.Size(125, 13);
            this.lbl_logWarningValue.TabIndex = 31;
            this.lbl_logWarningValue.Text = "/log might not be running";
            // 
            // lbl_logWarningTitle
            // 
            this.lbl_logWarningTitle.AutoSize = true;
            this.lbl_logWarningTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_logWarningTitle.Location = new System.Drawing.Point(84, 518);
            this.lbl_logWarningTitle.Name = "lbl_logWarningTitle";
            this.lbl_logWarningTitle.Size = new System.Drawing.Size(85, 13);
            this.lbl_logWarningTitle.TabIndex = 30;
            this.lbl_logWarningTitle.Text = "Text Logging:";
            // 
            // lbl_currentZoneValue
            // 
            this.lbl_currentZoneValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_currentZoneValue.AutoSize = true;
            this.lbl_currentZoneValue.Location = new System.Drawing.Point(95, 481);
            this.lbl_currentZoneValue.Name = "lbl_currentZoneValue";
            this.lbl_currentZoneValue.Size = new System.Drawing.Size(63, 13);
            this.lbl_currentZoneValue.TabIndex = 29;
            this.lbl_currentZoneValue.Text = "Placeholder";
            // 
            // lbl_currentZoneTitle
            // 
            this.lbl_currentZoneTitle.AutoSize = true;
            this.lbl_currentZoneTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_currentZoneTitle.Location = new System.Drawing.Point(84, 462);
            this.lbl_currentZoneTitle.Name = "lbl_currentZoneTitle";
            this.lbl_currentZoneTitle.Size = new System.Drawing.Size(85, 13);
            this.lbl_currentZoneTitle.TabIndex = 28;
            this.lbl_currentZoneTitle.Text = "Current Zone:";
            // 
            // lbl_webLink
            // 
            this.lbl_webLink.AutoSize = true;
            this.lbl_webLink.LinkArea = new System.Windows.Forms.LinkArea(0, 34);
            this.lbl_webLink.Location = new System.Drawing.Point(29, 423);
            this.lbl_webLink.Name = "lbl_webLink";
            this.lbl_webLink.Size = new System.Drawing.Size(194, 13);
            this.lbl_webLink.TabIndex = 24;
            this.lbl_webLink.TabStop = true;
            this.lbl_webLink.Text = "http://www.epgpweb.com/help/system";
            this.lbl_webLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_webLink_LinkClicked);
            // 
            // lbl_learnMore
            // 
            this.lbl_learnMore.AutoSize = true;
            this.lbl_learnMore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_learnMore.Location = new System.Drawing.Point(89, 408);
            this.lbl_learnMore.Name = "lbl_learnMore";
            this.lbl_learnMore.Size = new System.Drawing.Size(75, 13);
            this.lbl_learnMore.TabIndex = 22;
            this.lbl_learnMore.Text = "Learn More:";
            // 
            // lbl_GPminValue
            // 
            this.lbl_GPminValue.AutoSize = true;
            this.lbl_GPminValue.Location = new System.Drawing.Point(165, 301);
            this.lbl_GPminValue.Name = "lbl_GPminValue";
            this.lbl_GPminValue.Size = new System.Drawing.Size(31, 13);
            this.lbl_GPminValue.TabIndex = 15;
            this.lbl_GPminValue.Text = "5 GP";
            // 
            // lbl_GPminTitle
            // 
            this.lbl_GPminTitle.AutoSize = true;
            this.lbl_GPminTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_GPminTitle.Location = new System.Drawing.Point(146, 284);
            this.lbl_GPminTitle.Name = "lbl_GPminTitle";
            this.lbl_GPminTitle.Size = new System.Drawing.Size(80, 13);
            this.lbl_GPminTitle.TabIndex = 14;
            this.lbl_GPminTitle.Text = "GP Minimum:";
            // 
            // lbl_decayValue
            // 
            this.lbl_decayValue.AutoSize = true;
            this.lbl_decayValue.Location = new System.Drawing.Point(25, 301);
            this.lbl_decayValue.Name = "lbl_decayValue";
            this.lbl_decayValue.Size = new System.Drawing.Size(94, 13);
            this.lbl_decayValue.TabIndex = 13;
            this.lbl_decayValue.Text = "7% every Tuesday";
            // 
            // lbl_decayTitle
            // 
            this.lbl_decayTitle.AutoSize = true;
            this.lbl_decayTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_decayTitle.Location = new System.Drawing.Point(33, 284);
            this.lbl_decayTitle.Name = "lbl_decayTitle";
            this.lbl_decayTitle.Size = new System.Drawing.Size(90, 13);
            this.lbl_decayTitle.TabIndex = 12;
            this.lbl_decayTitle.Text = "EP/GP Decay:";
            // 
            // lbl_EPgainsValues
            // 
            this.lbl_EPgainsValues.AutoSize = true;
            this.lbl_EPgainsValues.Location = new System.Drawing.Point(163, 211);
            this.lbl_EPgainsValues.Name = "lbl_EPgainsValues";
            this.lbl_EPgainsValues.Size = new System.Drawing.Size(53, 52);
            this.lbl_EPgainsValues.TabIndex = 11;
            this.lbl_EPgainsValues.Text = "5 EP\r\n10 EP\r\n5 EP\r\n10 EP/hr.";
            // 
            // lbl_EPgainsNames
            // 
            this.lbl_EPgainsNames.AutoSize = true;
            this.lbl_EPgainsNames.Location = new System.Drawing.Point(37, 211);
            this.lbl_EPgainsNames.Name = "lbl_EPgainsNames";
            this.lbl_EPgainsNames.Size = new System.Drawing.Size(124, 52);
            this.lbl_EPgainsNames.TabIndex = 10;
            this.lbl_EPgainsNames.Text = "On time w/ consumables\r\nBoss kill, 1st time\r\nBoss kill, farm\r\nProgression";
            // 
            // lbl_EPgainsTitle
            // 
            this.lbl_EPgainsTitle.AutoSize = true;
            this.lbl_EPgainsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EPgainsTitle.Location = new System.Drawing.Point(95, 194);
            this.lbl_EPgainsTitle.Name = "lbl_EPgainsTitle";
            this.lbl_EPgainsTitle.Size = new System.Drawing.Size(63, 13);
            this.lbl_EPgainsTitle.TabIndex = 9;
            this.lbl_EPgainsTitle.Text = "EP Gains:";
            // 
            // lbl_itemCostValues
            // 
            this.lbl_itemCostValues.AutoSize = true;
            this.lbl_itemCostValues.Location = new System.Drawing.Point(139, 32);
            this.lbl_itemCostValues.Name = "lbl_itemCostValues";
            this.lbl_itemCostValues.Size = new System.Drawing.Size(49, 143);
            this.lbl_itemCostValues.TabIndex = 8;
            this.lbl_itemCostValues.Text = "5 GP\r\n10 GP\r\n5 GP\r\n5 GP\r\n5 GP\r\n5 GP\r\n10 GP\r\n10 GP\r\n5 GP\r\n100 LGP\r\n200 LGP";
            // 
            // lbl_itemCostNames
            // 
            this.lbl_itemCostNames.AutoSize = true;
            this.lbl_itemCostNames.Location = new System.Drawing.Point(64, 32);
            this.lbl_itemCostNames.Name = "lbl_itemCostNames";
            this.lbl_itemCostNames.Size = new System.Drawing.Size(74, 143);
            this.lbl_itemCostNames.TabIndex = 7;
            this.lbl_itemCostNames.Text = "Armor\r\n1H\r\n2H\r\nOff Hands\r\nRanged\r\nRings\r\nTrinkets\r\nGreaters\r\nLessers\r\nLegendary 1" +
                "H\r\nLegendary 2H\r\n";
            // 
            // lbl_itemCostTitle
            // 
            this.lbl_itemCostTitle.AutoSize = true;
            this.lbl_itemCostTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_itemCostTitle.Location = new System.Drawing.Point(91, 15);
            this.lbl_itemCostTitle.Name = "lbl_itemCostTitle";
            this.lbl_itemCostTitle.Size = new System.Drawing.Size(70, 13);
            this.lbl_itemCostTitle.TabIndex = 6;
            this.lbl_itemCostTitle.Text = "Item Costs:";
            // 
            // adminTab
            // 
            this.adminTab.Controls.Add(this.raiderStatusButton);
            this.adminTab.Controls.Add(this.onTimeButton);
            this.adminTab.Controls.Add(this.undoButton);
            this.adminTab.Controls.Add(this.lbl_raidxmlDate);
            this.adminTab.Controls.Add(this.lbl_admin_users);
            this.adminTab.Controls.Add(this.lbl_raidxmlTitle);
            this.adminTab.Controls.Add(this.lbl_admin_epfunc);
            this.adminTab.Controls.Add(this.lbl_admin_login);
            this.adminTab.Controls.Add(this.deleteUserButton);
            this.adminTab.Controls.Add(this.addUserButton);
            this.adminTab.Controls.Add(this.lbl_loggedIn);
            this.adminTab.Controls.Add(this.loginButton);
            this.adminTab.Controls.Add(this.attendanceButton);
            this.adminTab.Controls.Add(this.lbl_pass);
            this.adminTab.Controls.Add(this.txt_pass);
            this.adminTab.Controls.Add(this.lbl_name);
            this.adminTab.Controls.Add(this.tenEPbutton);
            this.adminTab.Controls.Add(this.fiveEPbutton);
            this.adminTab.Controls.Add(this.txt_name);
            this.adminTab.Location = new System.Drawing.Point(4, 22);
            this.adminTab.Name = "adminTab";
            this.adminTab.Size = new System.Drawing.Size(252, 577);
            this.adminTab.TabIndex = 1;
            this.adminTab.Text = "Admin";
            this.adminTab.UseVisualStyleBackColor = true;
            // 
            // raiderStatusButton
            // 
            this.raiderStatusButton.Location = new System.Drawing.Point(69, 396);
            this.raiderStatusButton.Name = "raiderStatusButton";
            this.raiderStatusButton.Size = new System.Drawing.Size(115, 30);
            this.raiderStatusButton.TabIndex = 35;
            this.raiderStatusButton.Text = "Raider Status Toggle";
            this.raiderStatusButton.UseVisualStyleBackColor = true;
            this.raiderStatusButton.Visible = false;
            this.raiderStatusButton.Click += new System.EventHandler(this.raiderStatusButton_Click);
            // 
            // onTimeButton
            // 
            this.onTimeButton.Location = new System.Drawing.Point(132, 157);
            this.onTimeButton.Name = "onTimeButton";
            this.onTimeButton.Size = new System.Drawing.Size(100, 30);
            this.onTimeButton.TabIndex = 34;
            this.onTimeButton.Text = "On Time";
            this.onTimeButton.UseVisualStyleBackColor = true;
            this.onTimeButton.Visible = false;
            this.onTimeButton.Click += new System.EventHandler(this.onTimeButton_Click);
            // 
            // undoButton
            // 
            this.undoButton.Location = new System.Drawing.Point(76, 288);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(100, 30);
            this.undoButton.TabIndex = 33;
            this.undoButton.Text = "Undo";
            this.undoButton.UseVisualStyleBackColor = true;
            this.undoButton.Visible = false;
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // lbl_raidxmlDate
            // 
            this.lbl_raidxmlDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_raidxmlDate.AutoSize = true;
            this.lbl_raidxmlDate.Location = new System.Drawing.Point(54, 221);
            this.lbl_raidxmlDate.Name = "lbl_raidxmlDate";
            this.lbl_raidxmlDate.Size = new System.Drawing.Size(145, 13);
            this.lbl_raidxmlDate.TabIndex = 32;
            this.lbl_raidxmlDate.Text = "0 days 0 hours 0 minutes ago";
            this.lbl_raidxmlDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_raidxmlDate.Visible = false;
            // 
            // lbl_admin_users
            // 
            this.lbl_admin_users.AutoSize = true;
            this.lbl_admin_users.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_admin_users.Location = new System.Drawing.Point(14, 338);
            this.lbl_admin_users.Name = "lbl_admin_users";
            this.lbl_admin_users.Size = new System.Drawing.Size(39, 13);
            this.lbl_admin_users.TabIndex = 31;
            this.lbl_admin_users.Text = "Users";
            this.lbl_admin_users.Visible = false;
            // 
            // lbl_raidxmlTitle
            // 
            this.lbl_raidxmlTitle.AutoSize = true;
            this.lbl_raidxmlTitle.Location = new System.Drawing.Point(61, 197);
            this.lbl_raidxmlTitle.Name = "lbl_raidxmlTitle";
            this.lbl_raidxmlTitle.Size = new System.Drawing.Size(130, 13);
            this.lbl_raidxmlTitle.TabIndex = 30;
            this.lbl_raidxmlTitle.Text = "Raid.xml was last created:\r\n";
            this.lbl_raidxmlTitle.Visible = false;
            // 
            // lbl_admin_epfunc
            // 
            this.lbl_admin_epfunc.AutoSize = true;
            this.lbl_admin_epfunc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_admin_epfunc.Location = new System.Drawing.Point(14, 133);
            this.lbl_admin_epfunc.Name = "lbl_admin_epfunc";
            this.lbl_admin_epfunc.Size = new System.Drawing.Size(82, 13);
            this.lbl_admin_epfunc.TabIndex = 29;
            this.lbl_admin_epfunc.Text = "EP Functions";
            this.lbl_admin_epfunc.Visible = false;
            // 
            // lbl_admin_login
            // 
            this.lbl_admin_login.AutoSize = true;
            this.lbl_admin_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_admin_login.Location = new System.Drawing.Point(14, 11);
            this.lbl_admin_login.Name = "lbl_admin_login";
            this.lbl_admin_login.Size = new System.Drawing.Size(38, 13);
            this.lbl_admin_login.TabIndex = 28;
            this.lbl_admin_login.Text = "Login";
            // 
            // deleteUserButton
            // 
            this.deleteUserButton.Location = new System.Drawing.Point(132, 360);
            this.deleteUserButton.Name = "deleteUserButton";
            this.deleteUserButton.Size = new System.Drawing.Size(100, 30);
            this.deleteUserButton.TabIndex = 24;
            this.deleteUserButton.Text = "Delete User";
            this.deleteUserButton.UseVisualStyleBackColor = true;
            this.deleteUserButton.Visible = false;
            this.deleteUserButton.Click += new System.EventHandler(this.deleteUserButton_Click);
            // 
            // addUserButton
            // 
            this.addUserButton.Location = new System.Drawing.Point(21, 360);
            this.addUserButton.Name = "addUserButton";
            this.addUserButton.Size = new System.Drawing.Size(100, 30);
            this.addUserButton.TabIndex = 25;
            this.addUserButton.Text = "Add User";
            this.addUserButton.UseVisualStyleBackColor = true;
            this.addUserButton.Visible = false;
            this.addUserButton.Click += new System.EventHandler(this.addUserButton_Click);
            // 
            // lbl_loggedIn
            // 
            this.lbl_loggedIn.AutoSize = true;
            this.lbl_loggedIn.ForeColor = System.Drawing.Color.Red;
            this.lbl_loggedIn.Location = new System.Drawing.Point(32, 95);
            this.lbl_loggedIn.Name = "lbl_loggedIn";
            this.lbl_loggedIn.Size = new System.Drawing.Size(75, 13);
            this.lbl_loggedIn.TabIndex = 10;
            this.lbl_loggedIn.Text = "Not Logged In";
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(169, 90);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(65, 22);
            this.loginButton.TabIndex = 3;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // attendanceButton
            // 
            this.attendanceButton.Location = new System.Drawing.Point(21, 157);
            this.attendanceButton.Name = "attendanceButton";
            this.attendanceButton.Size = new System.Drawing.Size(100, 30);
            this.attendanceButton.TabIndex = 9;
            this.attendanceButton.Text = "Attendance";
            this.attendanceButton.UseVisualStyleBackColor = true;
            this.attendanceButton.Visible = false;
            this.attendanceButton.Click += new System.EventHandler(this.attendanceButton_Click);
            // 
            // lbl_pass
            // 
            this.lbl_pass.AutoSize = true;
            this.lbl_pass.Location = new System.Drawing.Point(20, 64);
            this.lbl_pass.Name = "lbl_pass";
            this.lbl_pass.Size = new System.Drawing.Size(56, 13);
            this.lbl_pass.TabIndex = 3;
            this.lbl_pass.Text = "Password:";
            // 
            // txt_pass
            // 
            this.txt_pass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_pass.Location = new System.Drawing.Point(114, 62);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.Size = new System.Drawing.Size(120, 20);
            this.txt_pass.TabIndex = 2;
            this.txt_pass.UseSystemPasswordChar = true;
            this.txt_pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_pass_KeyDown);
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(20, 34);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(58, 13);
            this.lbl_name.TabIndex = 4;
            this.lbl_name.Text = "Username:";
            // 
            // tenEPbutton
            // 
            this.tenEPbutton.Location = new System.Drawing.Point(132, 252);
            this.tenEPbutton.Name = "tenEPbutton";
            this.tenEPbutton.Size = new System.Drawing.Size(100, 30);
            this.tenEPbutton.TabIndex = 8;
            this.tenEPbutton.Text = "+ 10 EP";
            this.tenEPbutton.UseVisualStyleBackColor = true;
            this.tenEPbutton.Visible = false;
            this.tenEPbutton.Click += new System.EventHandler(this.tenEPbutton_Click);
            // 
            // fiveEPbutton
            // 
            this.fiveEPbutton.Location = new System.Drawing.Point(21, 252);
            this.fiveEPbutton.Name = "fiveEPbutton";
            this.fiveEPbutton.Size = new System.Drawing.Size(100, 30);
            this.fiveEPbutton.TabIndex = 7;
            this.fiveEPbutton.Text = "+5 EP";
            this.fiveEPbutton.UseVisualStyleBackColor = true;
            this.fiveEPbutton.Visible = false;
            this.fiveEPbutton.Click += new System.EventHandler(this.fiveEPbutton_Click);
            // 
            // txt_name
            // 
            this.txt_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_name.Location = new System.Drawing.Point(114, 32);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(120, 20);
            this.txt_name.TabIndex = 1;
            // 
            // settingsTab
            // 
            this.settingsTab.Controls.Add(this.testButton);
            this.settingsTab.Controls.Add(this.lbl_currentGuildValue);
            this.settingsTab.Controls.Add(this.lbl_currentGuildTitle);
            this.settingsTab.Controls.Add(this.getGuildButton);
            this.settingsTab.Controls.Add(this.lbl_currentDirValue);
            this.settingsTab.Controls.Add(this.lbl_currentDirTitle);
            this.settingsTab.Controls.Add(this.getDirButton);
            this.settingsTab.Controls.Add(this.lbl_opacity);
            this.settingsTab.Controls.Add(this.txt_opacity);
            this.settingsTab.Controls.Add(this.opacitySlider);
            this.settingsTab.Controls.Add(this.lbl_logReminder);
            this.settingsTab.Controls.Add(this.lbl_rightClickText);
            this.settingsTab.Controls.Add(this.lbl_leftClickText);
            this.settingsTab.Controls.Add(this.overlayButton);
            this.settingsTab.Location = new System.Drawing.Point(4, 22);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Size = new System.Drawing.Size(252, 577);
            this.settingsTab.TabIndex = 2;
            this.settingsTab.Text = "Settings";
            this.settingsTab.UseVisualStyleBackColor = true;
            // 
            // lbl_currentGuildValue
            // 
            this.lbl_currentGuildValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_currentGuildValue.AutoSize = true;
            this.lbl_currentGuildValue.Location = new System.Drawing.Point(92, 261);
            this.lbl_currentGuildValue.Name = "lbl_currentGuildValue";
            this.lbl_currentGuildValue.Size = new System.Drawing.Size(68, 13);
            this.lbl_currentGuildValue.TabIndex = 31;
            this.lbl_currentGuildValue.Text = "Display Guild";
            this.lbl_currentGuildValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_currentGuildTitle
            // 
            this.lbl_currentGuildTitle.AutoSize = true;
            this.lbl_currentGuildTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_currentGuildTitle.Location = new System.Drawing.Point(77, 241);
            this.lbl_currentGuildTitle.Name = "lbl_currentGuildTitle";
            this.lbl_currentGuildTitle.Size = new System.Drawing.Size(98, 13);
            this.lbl_currentGuildTitle.TabIndex = 30;
            this.lbl_currentGuildTitle.Text = "Current Guild is:";
            // 
            // getGuildButton
            // 
            this.getGuildButton.Location = new System.Drawing.Point(76, 283);
            this.getGuildButton.Name = "getGuildButton";
            this.getGuildButton.Size = new System.Drawing.Size(100, 30);
            this.getGuildButton.TabIndex = 29;
            this.getGuildButton.Text = "Change";
            this.getGuildButton.UseVisualStyleBackColor = true;
            // 
            // lbl_currentDirValue
            // 
            this.lbl_currentDirValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_currentDirValue.AutoSize = true;
            this.lbl_currentDirValue.Location = new System.Drawing.Point(41, 165);
            this.lbl_currentDirValue.Name = "lbl_currentDirValue";
            this.lbl_currentDirValue.Size = new System.Drawing.Size(171, 13);
            this.lbl_currentDirValue.TabIndex = 28;
            this.lbl_currentDirValue.Text = "C:\\Program Files (x86)\\RIFT Game";
            this.lbl_currentDirValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_currentDirTitle
            // 
            this.lbl_currentDirTitle.AutoSize = true;
            this.lbl_currentDirTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_currentDirTitle.Location = new System.Drawing.Point(51, 145);
            this.lbl_currentDirTitle.Name = "lbl_currentDirTitle";
            this.lbl_currentDirTitle.Size = new System.Drawing.Size(150, 13);
            this.lbl_currentDirTitle.TabIndex = 27;
            this.lbl_currentDirTitle.Text = "Current RIFT directory is:";
            // 
            // getDirButton
            // 
            this.getDirButton.Location = new System.Drawing.Point(76, 187);
            this.getDirButton.Name = "getDirButton";
            this.getDirButton.Size = new System.Drawing.Size(100, 30);
            this.getDirButton.TabIndex = 26;
            this.getDirButton.Text = "Change";
            this.getDirButton.UseVisualStyleBackColor = true;
            this.getDirButton.Click += new System.EventHandler(this.getDirButton_Click);
            // 
            // lbl_opacity
            // 
            this.lbl_opacity.AutoSize = true;
            this.lbl_opacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_opacity.Location = new System.Drawing.Point(69, 84);
            this.lbl_opacity.Name = "lbl_opacity";
            this.lbl_opacity.Size = new System.Drawing.Size(101, 13);
            this.lbl_opacity.TabIndex = 25;
            this.lbl_opacity.Text = "Overlay Opacity:";
            // 
            // txt_opacity
            // 
            this.txt_opacity.Location = new System.Drawing.Point(209, 106);
            this.txt_opacity.Name = "txt_opacity";
            this.txt_opacity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_opacity.Size = new System.Drawing.Size(25, 20);
            this.txt_opacity.TabIndex = 24;
            this.txt_opacity.Text = "50";
            this.txt_opacity.TextChanged += new System.EventHandler(this.txt_opacity_TextChanged);
            // 
            // opacitySlider
            // 
            this.opacitySlider.AutoSize = false;
            this.opacitySlider.BackColor = System.Drawing.Color.White;
            this.opacitySlider.Location = new System.Drawing.Point(9, 104);
            this.opacitySlider.Maximum = 100;
            this.opacitySlider.Name = "opacitySlider";
            this.opacitySlider.Size = new System.Drawing.Size(190, 45);
            this.opacitySlider.TabIndex = 23;
            this.opacitySlider.TickFrequency = 0;
            this.opacitySlider.Value = 50;
            this.opacitySlider.ValueChanged += new System.EventHandler(this.opacitySlider_ValueChanged);
            // 
            // lbl_logReminder
            // 
            this.lbl_logReminder.AutoSize = true;
            this.lbl_logReminder.Location = new System.Drawing.Point(42, 42);
            this.lbl_logReminder.Name = "lbl_logReminder";
            this.lbl_logReminder.Size = new System.Drawing.Size(155, 13);
            this.lbl_logReminder.TabIndex = 21;
            this.lbl_logReminder.Text = "Remember to type /log in-game";
            // 
            // lbl_rightClickText
            // 
            this.lbl_rightClickText.AutoSize = true;
            this.lbl_rightClickText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rightClickText.Location = new System.Drawing.Point(136, 24);
            this.lbl_rightClickText.Name = "lbl_rightClickText";
            this.lbl_rightClickText.Size = new System.Drawing.Size(108, 12);
            this.lbl_rightClickText.TabIndex = 20;
            this.lbl_rightClickText.Text = "Right click: Toggle border";
            // 
            // lbl_leftClickText
            // 
            this.lbl_leftClickText.AutoSize = true;
            this.lbl_leftClickText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_leftClickText.Location = new System.Drawing.Point(136, 5);
            this.lbl_leftClickText.Name = "lbl_leftClickText";
            this.lbl_leftClickText.Size = new System.Drawing.Size(109, 12);
            this.lbl_leftClickText.TabIndex = 19;
            this.lbl_leftClickText.Text = "Left click: Toggle window";
            // 
            // overlayButton
            // 
            this.overlayButton.Location = new System.Drawing.Point(6, 4);
            this.overlayButton.Name = "overlayButton";
            this.overlayButton.Size = new System.Drawing.Size(127, 35);
            this.overlayButton.TabIndex = 18;
            this.overlayButton.Text = "Toggle Overlay";
            this.overlayButton.UseVisualStyleBackColor = true;
            this.overlayButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.overlayButton_Click);
            // 
            // logTab
            // 
            this.logTab.Controls.Add(this.logSpreadsheet);
            this.logTab.Location = new System.Drawing.Point(4, 22);
            this.logTab.Name = "logTab";
            this.logTab.Padding = new System.Windows.Forms.Padding(3);
            this.logTab.Size = new System.Drawing.Size(252, 577);
            this.logTab.TabIndex = 3;
            this.logTab.Text = "Change Log";
            this.logTab.UseVisualStyleBackColor = true;
            // 
            // logSpreadsheet
            // 
            this.logSpreadsheet.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.Member,
            this.Number,
            this.Type,
            this.Reason});
            this.logSpreadsheet.Location = new System.Drawing.Point(0, 1);
            this.logSpreadsheet.Name = "logSpreadsheet";
            this.logSpreadsheet.OptionsBehavior.Editable = false;
            this.logSpreadsheet.OptionsBehavior.ResizeNodes = false;
            this.logSpreadsheet.OptionsMenu.EnableColumnMenu = false;
            this.logSpreadsheet.OptionsMenu.EnableFooterMenu = false;
            this.logSpreadsheet.OptionsView.ShowIndicator = false;
            this.logSpreadsheet.ParentFieldName = "parentID";
            this.logSpreadsheet.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowOnlyInEditor;
            this.logSpreadsheet.Size = new System.Drawing.Size(250, 574);
            this.logSpreadsheet.TabIndex = 0;
            this.logSpreadsheet.TreeLevelWidth = 12;
            this.logSpreadsheet.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.Dark;
            this.logSpreadsheet.UseDisabledStatePainter = false;
            // 
            // Member
            // 
            this.Member.AppearanceCell.Options.UseTextOptions = true;
            this.Member.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.Member.AppearanceHeader.Options.UseTextOptions = true;
            this.Member.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Member.FieldName = "Member";
            this.Member.Name = "Member";
            this.Member.OptionsColumn.AllowEdit = false;
            this.Member.OptionsColumn.AllowMove = false;
            this.Member.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.Member.OptionsColumn.AllowSize = false;
            this.Member.OptionsColumn.AllowSort = false;
            this.Member.OptionsColumn.FixedWidth = true;
            this.Member.OptionsColumn.ReadOnly = true;
            this.Member.OptionsColumn.ShowInCustomizationForm = false;
            this.Member.Visible = true;
            this.Member.VisibleIndex = 0;
            this.Member.Width = 90;
            // 
            // Number
            // 
            this.Number.AppearanceCell.Options.UseTextOptions = true;
            this.Number.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Number.AppearanceHeader.Options.UseTextOptions = true;
            this.Number.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Number.FieldName = "Number";
            this.Number.Name = "Number";
            this.Number.OptionsColumn.AllowEdit = false;
            this.Number.OptionsColumn.AllowMove = false;
            this.Number.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.Number.OptionsColumn.AllowSize = false;
            this.Number.OptionsColumn.AllowSort = false;
            this.Number.OptionsColumn.FixedWidth = true;
            this.Number.OptionsColumn.ReadOnly = true;
            this.Number.OptionsColumn.ShowInCustomizationForm = false;
            this.Number.Visible = true;
            this.Number.VisibleIndex = 1;
            this.Number.Width = 44;
            // 
            // Type
            // 
            this.Type.AppearanceCell.Options.UseTextOptions = true;
            this.Type.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Type.AppearanceHeader.Options.UseTextOptions = true;
            this.Type.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Type.FieldName = "Type";
            this.Type.Name = "Type";
            this.Type.OptionsColumn.AllowEdit = false;
            this.Type.OptionsColumn.AllowMove = false;
            this.Type.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.Type.OptionsColumn.AllowSize = false;
            this.Type.OptionsColumn.AllowSort = false;
            this.Type.OptionsColumn.FixedWidth = true;
            this.Type.OptionsColumn.ReadOnly = true;
            this.Type.OptionsColumn.ShowInCustomizationForm = false;
            this.Type.Visible = true;
            this.Type.VisibleIndex = 2;
            this.Type.Width = 32;
            // 
            // Reason
            // 
            this.Reason.AppearanceCell.Options.UseTextOptions = true;
            this.Reason.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.Reason.AppearanceHeader.Options.UseTextOptions = true;
            this.Reason.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Reason.FieldName = "Reason";
            this.Reason.Name = "Reason";
            this.Reason.OptionsColumn.AllowEdit = false;
            this.Reason.OptionsColumn.AllowMove = false;
            this.Reason.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.Reason.OptionsColumn.AllowSize = false;
            this.Reason.OptionsColumn.AllowSort = false;
            this.Reason.OptionsColumn.ReadOnly = true;
            this.Reason.OptionsColumn.ShowInCustomizationForm = false;
            this.Reason.Visible = true;
            this.Reason.VisibleIndex = 3;
            this.Reason.Width = 27;
            // 
            // lbl_sort1
            // 
            this.lbl_sort1.AutoSize = true;
            this.lbl_sort1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_sort1.Location = new System.Drawing.Point(2, 589);
            this.lbl_sort1.Name = "lbl_sort1";
            this.lbl_sort1.Size = new System.Drawing.Size(48, 13);
            this.lbl_sort1.TabIndex = 27;
            this.lbl_sort1.Text = "Sort By";
            // 
            // alphaSortButton
            // 
            this.alphaSortButton.Location = new System.Drawing.Point(56, 583);
            this.alphaSortButton.Name = "alphaSortButton";
            this.alphaSortButton.Size = new System.Drawing.Size(80, 25);
            this.alphaSortButton.TabIndex = 26;
            this.alphaSortButton.Text = "Alphabetical";
            this.alphaSortButton.UseVisualStyleBackColor = true;
            this.alphaSortButton.Click += new System.EventHandler(this.alphaSortButton_Click);
            // 
            // PRsortButton
            // 
            this.PRsortButton.Location = new System.Drawing.Point(142, 583);
            this.PRsortButton.Name = "PRsortButton";
            this.PRsortButton.Size = new System.Drawing.Size(80, 25);
            this.PRsortButton.TabIndex = 25;
            this.PRsortButton.Text = "PR";
            this.PRsortButton.UseVisualStyleBackColor = true;
            this.PRsortButton.Click += new System.EventHandler(this.PRsortButton_Click);
            // 
            // EPGPspreadsheet
            // 
            this.EPGPspreadsheet.AllowUserToAddRows = false;
            this.EPGPspreadsheet.AllowUserToOrderColumns = true;
            this.EPGPspreadsheet.AllowUserToResizeColumns = false;
            this.EPGPspreadsheet.AllowUserToResizeRows = false;
            this.EPGPspreadsheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EPGPspreadsheet.Location = new System.Drawing.Point(4, 4);
            this.EPGPspreadsheet.Name = "EPGPspreadsheet";
            this.EPGPspreadsheet.RowHeadersVisible = false;
            this.EPGPspreadsheet.Size = new System.Drawing.Size(490, 575);
            this.EPGPspreadsheet.TabIndex = 1;
            // 
            // btn_refreshTbl
            // 
            this.btn_refreshTbl.Location = new System.Drawing.Point(395, 583);
            this.btn_refreshTbl.Name = "btn_refreshTbl";
            this.btn_refreshTbl.Size = new System.Drawing.Size(100, 25);
            this.btn_refreshTbl.TabIndex = 22;
            this.btn_refreshTbl.Text = "Refresh Table";
            this.btn_refreshTbl.UseVisualStyleBackColor = true;
            this.btn_refreshTbl.Click += new System.EventHandler(this.btn_refreshTbl_Click);
            // 
            // overlayReset
            // 
            this.overlayReset.Interval = 10000D;
            this.overlayReset.SynchronizingObject = this;
            this.overlayReset.Elapsed += new System.Timers.ElapsedEventHandler(this.overlayResetElapse);
            // 
            // LPRsortButton
            // 
            this.LPRsortButton.Location = new System.Drawing.Point(228, 583);
            this.LPRsortButton.Name = "LPRsortButton";
            this.LPRsortButton.Size = new System.Drawing.Size(80, 25);
            this.LPRsortButton.TabIndex = 28;
            this.LPRsortButton.Text = "LPR";
            this.LPRsortButton.UseVisualStyleBackColor = true;
            this.LPRsortButton.Click += new System.EventHandler(this.LPRsortButton_Click);
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(54, 409);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(151, 37);
            this.testButton.TabIndex = 32;
            this.testButton.Text = "test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // guildManagement
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 612);
            this.Controls.Add(this.LPRsortButton);
            this.Controls.Add(this.btn_refreshTbl);
            this.Controls.Add(this.tabArea);
            this.Controls.Add(this.PRsortButton);
            this.Controls.Add(this.alphaSortButton);
            this.Controls.Add(this.lbl_sort1);
            this.Controls.Add(this.EPGPspreadsheet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "guildManagement";
            this.Text = "Persona Guild Management";
            this.Activated += new System.EventHandler(this.guildManagement_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.guildManagement_Close);
            this.Load += new System.EventHandler(this.guildManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logWatch)).EndInit();
            this.tabArea.ResumeLayout(false);
            this.infoTab.ResumeLayout(false);
            this.infoTab.PerformLayout();
            this.adminTab.ResumeLayout(false);
            this.adminTab.PerformLayout();
            this.settingsTab.ResumeLayout(false);
            this.settingsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacitySlider)).EndInit();
            this.logTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logSpreadsheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EPGPspreadsheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.overlayReset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Timers.Timer logWatch;
        private System.Windows.Forms.TabControl tabArea;
        private System.Windows.Forms.TabPage infoTab;
        private System.Windows.Forms.DataGridView EPGPspreadsheet;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.Label lbl_pass;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label lbl_itemCostNames;
        private System.Windows.Forms.Label lbl_itemCostTitle;
        private System.Windows.Forms.Label lbl_itemCostValues;
        private System.Windows.Forms.Label lbl_decayTitle;
        private System.Windows.Forms.Label lbl_EPgainsValues;
        private System.Windows.Forms.Label lbl_GPminValue;
        private System.Windows.Forms.Label lbl_GPminTitle;
        private System.Windows.Forms.Label lbl_decayValue;
        private System.Windows.Forms.TabPage adminTab;
        private System.Windows.Forms.Button attendanceButton;
        private System.Windows.Forms.Button tenEPbutton;
        private System.Windows.Forms.Button fiveEPbutton;
        private System.Windows.Forms.TabPage settingsTab;
        private System.Windows.Forms.Label lbl_logReminder;
        private System.Windows.Forms.Label lbl_rightClickText;
        private System.Windows.Forms.Label lbl_leftClickText;
        private System.Windows.Forms.Button overlayButton;
        private System.Windows.Forms.Label lbl_learnMore;
        private System.Windows.Forms.Label lbl_EPgainsNames;
        private System.Windows.Forms.Label lbl_EPgainsTitle;
        private System.Windows.Forms.LinkLabel lbl_webLink;
        private System.Windows.Forms.Button btn_refreshTbl;
        private System.Windows.Forms.Label lbl_loggedIn;
        private System.Windows.Forms.Label lbl_sort1;
        private System.Windows.Forms.Button alphaSortButton;
        private System.Windows.Forms.Button PRsortButton;
        private System.Windows.Forms.Label lbl_opacity;
        private System.Windows.Forms.TextBox txt_opacity;
        private System.Windows.Forms.TrackBar opacitySlider;
        private System.Windows.Forms.Button deleteUserButton;
        private System.Windows.Forms.Button addUserButton;
        private System.Windows.Forms.Label lbl_currentZoneValue;
        private System.Windows.Forms.Label lbl_currentZoneTitle;
        private System.Windows.Forms.Label lbl_admin_users;
        private System.Windows.Forms.Label lbl_raidxmlTitle;
        private System.Windows.Forms.Label lbl_admin_epfunc;
        private System.Windows.Forms.Label lbl_admin_login;
        private System.Windows.Forms.Label lbl_raidxmlDate;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.TabPage logTab;
        private DevExpress.XtraTreeList.TreeList logSpreadsheet;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Member;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Number;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Type;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Reason;
        private System.Windows.Forms.Label lbl_currentDirValue;
        private System.Windows.Forms.Label lbl_currentDirTitle;
        private System.Windows.Forms.Button getDirButton;
        private System.Windows.Forms.Label lbl_logWarningValue;
        private System.Windows.Forms.Label lbl_logWarningTitle;
        private System.Timers.Timer overlayReset;
        private System.Windows.Forms.Button onTimeButton;
        private System.Windows.Forms.Button raiderStatusButton;
        private System.Windows.Forms.Label lbl_member;
        private System.Windows.Forms.Label lbl_raider;
        private System.Windows.Forms.Label lbl_blackKey;
        private System.Windows.Forms.Label lbl_blueKey;
        private System.Windows.Forms.Label lbl_keyTitle;
        private System.Windows.Forms.Button LPRsortButton;
        private System.Windows.Forms.Label lbl_currentGuildValue;
        private System.Windows.Forms.Label lbl_currentGuildTitle;
        private System.Windows.Forms.Button getGuildButton;
        private System.Windows.Forms.Button testButton;
    }
}
