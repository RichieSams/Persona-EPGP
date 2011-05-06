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
            this.infoTab = new System.Windows.Forms.TabPage();
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
            this.lbl_raidxmlDate = new System.Windows.Forms.Label();
            this.lbl_admin_users = new System.Windows.Forms.Label();
            this.lbl_raidxmlTitle = new System.Windows.Forms.Label();
            this.lbl_admin_epfunc = new System.Windows.Forms.Label();
            this.lbl_admin_login = new System.Windows.Forms.Label();
            this.deleteUserButton = new System.Windows.Forms.Button();
            this.addUserButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
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
            this.lbl_opacity = new System.Windows.Forms.Label();
            this.txt_opacity = new System.Windows.Forms.TextBox();
            this.opacitySlider = new System.Windows.Forms.TrackBar();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lbl_logReminder = new System.Windows.Forms.Label();
            this.lbl_rightClickText = new System.Windows.Forms.Label();
            this.lbl_leftClickText = new System.Windows.Forms.Label();
            this.overlayButton = new System.Windows.Forms.Button();
            this.lbl_sort1 = new System.Windows.Forms.Label();
            this.alphaSortButton1 = new System.Windows.Forms.Button();
            this.PRsortButton1 = new System.Windows.Forms.Button();
            this.EPGPspreadsheet = new System.Windows.Forms.DataGridView();
            this.btn_refreshTbl = new System.Windows.Forms.Button();
            this.tabArea.SuspendLayout();
            this.infoTab.SuspendLayout();
            this.adminTab.SuspendLayout();
            this.settingsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacitySlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EPGPspreadsheet)).BeginInit();
            this.SuspendLayout();
            // 
            // tabArea
            // 
            this.tabArea.Controls.Add(this.infoTab);
            this.tabArea.Controls.Add(this.adminTab);
            this.tabArea.Controls.Add(this.settingsTab);
            this.tabArea.Location = new System.Drawing.Point(496, 5);
            this.tabArea.Name = "tabArea";
            this.tabArea.SelectedIndex = 0;
            this.tabArea.Size = new System.Drawing.Size(260, 603);
            this.tabArea.TabIndex = 0;
            // 
            // infoTab
            // 
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
            // lbl_currentZoneValue
            // 
            this.lbl_currentZoneValue.AutoSize = true;
            this.lbl_currentZoneValue.Location = new System.Drawing.Point(95, 535);
            this.lbl_currentZoneValue.Name = "lbl_currentZoneValue";
            this.lbl_currentZoneValue.Size = new System.Drawing.Size(63, 13);
            this.lbl_currentZoneValue.TabIndex = 29;
            this.lbl_currentZoneValue.Text = "Placeholder";
            // 
            // lbl_currentZoneTitle
            // 
            this.lbl_currentZoneTitle.AutoSize = true;
            this.lbl_currentZoneTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_currentZoneTitle.Location = new System.Drawing.Point(84, 516);
            this.lbl_currentZoneTitle.Name = "lbl_currentZoneTitle";
            this.lbl_currentZoneTitle.Size = new System.Drawing.Size(85, 13);
            this.lbl_currentZoneTitle.TabIndex = 28;
            this.lbl_currentZoneTitle.Text = "Current Zone:";
            // 
            // lbl_webLink
            // 
            this.lbl_webLink.AutoSize = true;
            this.lbl_webLink.LinkArea = new System.Windows.Forms.LinkArea(0, 34);
            this.lbl_webLink.Location = new System.Drawing.Point(29, 359);
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
            this.lbl_learnMore.Location = new System.Drawing.Point(89, 344);
            this.lbl_learnMore.Name = "lbl_learnMore";
            this.lbl_learnMore.Size = new System.Drawing.Size(75, 13);
            this.lbl_learnMore.TabIndex = 22;
            this.lbl_learnMore.Text = "Learn More:";
            // 
            // lbl_GPminValue
            // 
            this.lbl_GPminValue.AutoSize = true;
            this.lbl_GPminValue.Location = new System.Drawing.Point(165, 307);
            this.lbl_GPminValue.Name = "lbl_GPminValue";
            this.lbl_GPminValue.Size = new System.Drawing.Size(31, 13);
            this.lbl_GPminValue.TabIndex = 15;
            this.lbl_GPminValue.Text = "5 GP";
            // 
            // lbl_GPminTitle
            // 
            this.lbl_GPminTitle.AutoSize = true;
            this.lbl_GPminTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_GPminTitle.Location = new System.Drawing.Point(146, 290);
            this.lbl_GPminTitle.Name = "lbl_GPminTitle";
            this.lbl_GPminTitle.Size = new System.Drawing.Size(80, 13);
            this.lbl_GPminTitle.TabIndex = 14;
            this.lbl_GPminTitle.Text = "GP Minimum:";
            // 
            // lbl_decayValue
            // 
            this.lbl_decayValue.AutoSize = true;
            this.lbl_decayValue.Location = new System.Drawing.Point(25, 307);
            this.lbl_decayValue.Name = "lbl_decayValue";
            this.lbl_decayValue.Size = new System.Drawing.Size(94, 13);
            this.lbl_decayValue.TabIndex = 13;
            this.lbl_decayValue.Text = "7% every Tuesday";
            // 
            // lbl_decayTitle
            // 
            this.lbl_decayTitle.AutoSize = true;
            this.lbl_decayTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_decayTitle.Location = new System.Drawing.Point(33, 290);
            this.lbl_decayTitle.Name = "lbl_decayTitle";
            this.lbl_decayTitle.Size = new System.Drawing.Size(90, 13);
            this.lbl_decayTitle.TabIndex = 12;
            this.lbl_decayTitle.Text = "EP/GP Decay:";
            // 
            // lbl_EPgainsValues
            // 
            this.lbl_EPgainsValues.AutoSize = true;
            this.lbl_EPgainsValues.Location = new System.Drawing.Point(163, 210);
            this.lbl_EPgainsValues.Name = "lbl_EPgainsValues";
            this.lbl_EPgainsValues.Size = new System.Drawing.Size(53, 52);
            this.lbl_EPgainsValues.TabIndex = 11;
            this.lbl_EPgainsValues.Text = "5 EP\r\n10 EP\r\n5 EP\r\n10 EP/hr.";
            // 
            // lbl_EPgainsNames
            // 
            this.lbl_EPgainsNames.AutoSize = true;
            this.lbl_EPgainsNames.Location = new System.Drawing.Point(37, 210);
            this.lbl_EPgainsNames.Name = "lbl_EPgainsNames";
            this.lbl_EPgainsNames.Size = new System.Drawing.Size(124, 52);
            this.lbl_EPgainsNames.TabIndex = 10;
            this.lbl_EPgainsNames.Text = "On time w/ consumables\r\nBoss kill, 1st time\r\nBoss kill, farm\r\nProgression";
            // 
            // lbl_EPgainsTitle
            // 
            this.lbl_EPgainsTitle.AutoSize = true;
            this.lbl_EPgainsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EPgainsTitle.Location = new System.Drawing.Point(95, 193);
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
            this.adminTab.Controls.Add(this.lbl_raidxmlDate);
            this.adminTab.Controls.Add(this.lbl_admin_users);
            this.adminTab.Controls.Add(this.lbl_raidxmlTitle);
            this.adminTab.Controls.Add(this.lbl_admin_epfunc);
            this.adminTab.Controls.Add(this.lbl_admin_login);
            this.adminTab.Controls.Add(this.deleteUserButton);
            this.adminTab.Controls.Add(this.addUserButton);
            this.adminTab.Controls.Add(this.textBox1);
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
            // lbl_raidxmlDate
            // 
            this.lbl_raidxmlDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_raidxmlDate.AutoSize = true;
            this.lbl_raidxmlDate.Location = new System.Drawing.Point(54, 221);
            this.lbl_raidxmlDate.Name = "lbl_raidxmlDate";
            this.lbl_raidxmlDate.Size = new System.Drawing.Size(145, 13);
            this.lbl_raidxmlDate.TabIndex = 32;
            this.lbl_raidxmlDate.Text = "0 days 0 hours 0 minutes ago";
            this.lbl_raidxmlDate.Visible = false;
            // 
            // lbl_admin_users
            // 
            this.lbl_admin_users.AutoSize = true;
            this.lbl_admin_users.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_admin_users.Location = new System.Drawing.Point(14, 303);
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
            this.deleteUserButton.Location = new System.Drawing.Point(134, 325);
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
            this.addUserButton.Location = new System.Drawing.Point(23, 325);
            this.addUserButton.Name = "addUserButton";
            this.addUserButton.Size = new System.Drawing.Size(100, 30);
            this.addUserButton.TabIndex = 25;
            this.addUserButton.Text = "Add User";
            this.addUserButton.UseVisualStyleBackColor = true;
            this.addUserButton.Visible = false;
            this.addUserButton.Click += new System.EventHandler(this.addUserButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(17, 423);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(216, 130);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "-Add Log Stuff (Showing Log, Undo, etc)\r\n-Attach Raid filebrowser and get info to" +
                " check last modified date then warn if old";
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
            this.attendanceButton.Location = new System.Drawing.Point(74, 157);
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
            this.tenEPbutton.Location = new System.Drawing.Point(134, 252);
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
            this.fiveEPbutton.Location = new System.Drawing.Point(23, 252);
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
            this.settingsTab.Controls.Add(this.lbl_opacity);
            this.settingsTab.Controls.Add(this.txt_opacity);
            this.settingsTab.Controls.Add(this.opacitySlider);
            this.settingsTab.Controls.Add(this.textBox2);
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
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(18, 199);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(216, 130);
            this.textBox2.TabIndex = 22;
            this.textBox2.Text = "-More overlay options (anything else you can think of?)\r\n-Info to show log, comba" +
                "tlog (what do you mean?)\r\n-Timer Refresh (currently hardcoded at 25-35 seconds r" +
                "andomly)\r\n";
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
            // alphaSortButton1
            // 
            this.alphaSortButton1.Location = new System.Drawing.Point(56, 583);
            this.alphaSortButton1.Name = "alphaSortButton1";
            this.alphaSortButton1.Size = new System.Drawing.Size(80, 25);
            this.alphaSortButton1.TabIndex = 26;
            this.alphaSortButton1.Text = "Alphabetical";
            this.alphaSortButton1.UseVisualStyleBackColor = true;
            this.alphaSortButton1.Click += new System.EventHandler(this.alphaSortButton_Click);
            // 
            // PRsortButton1
            // 
            this.PRsortButton1.Location = new System.Drawing.Point(142, 583);
            this.PRsortButton1.Name = "PRsortButton1";
            this.PRsortButton1.Size = new System.Drawing.Size(80, 25);
            this.PRsortButton1.TabIndex = 25;
            this.PRsortButton1.Text = "PR";
            this.PRsortButton1.UseVisualStyleBackColor = true;
            this.PRsortButton1.Click += new System.EventHandler(this.PRsortButton_Click);
            // 
            // EPGPspreadsheet
            // 
            this.EPGPspreadsheet.AllowUserToAddRows = false;
            this.EPGPspreadsheet.AllowUserToOrderColumns = true;
            this.EPGPspreadsheet.AllowUserToResizeColumns = false;
            this.EPGPspreadsheet.AllowUserToResizeRows = false;
            this.EPGPspreadsheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EPGPspreadsheet.Location = new System.Drawing.Point(5, 5);
            this.EPGPspreadsheet.Name = "EPGPspreadsheet";
            this.EPGPspreadsheet.RowHeadersVisible = false;
            this.EPGPspreadsheet.Size = new System.Drawing.Size(489, 575);
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
            // guildManagement
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 612);
            this.Controls.Add(this.btn_refreshTbl);
            this.Controls.Add(this.tabArea);
            this.Controls.Add(this.PRsortButton1);
            this.Controls.Add(this.alphaSortButton1);
            this.Controls.Add(this.lbl_sort1);
            this.Controls.Add(this.EPGPspreadsheet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "guildManagement";
            this.Text = "Persona Guild Management";
            this.Activated += new System.EventHandler(this.guildManagement_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.guildManagement_Close);
            this.Load += new System.EventHandler(this.guildManagement_Load);
            this.tabArea.ResumeLayout(false);
            this.infoTab.ResumeLayout(false);
            this.infoTab.PerformLayout();
            this.adminTab.ResumeLayout(false);
            this.adminTab.PerformLayout();
            this.settingsTab.ResumeLayout(false);
            this.settingsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacitySlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EPGPspreadsheet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lbl_sort1;
        private System.Windows.Forms.Button alphaSortButton1;
        private System.Windows.Forms.Button PRsortButton1;
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
    }
}

