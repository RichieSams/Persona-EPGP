using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Net;
using System.Security.Cryptography;
using System.Collections.Specialized;

namespace EPGP
{
    public partial class guildManagement : Form
    {

        #region Variables

        // Administrator
        private Boolean loggedIn;
        private String write_password;
        private String login_name;
        private String login_pass;

        // Web Client
        private WebClient client = new WebClient();
        string k;
        byte[] result;

        // MySql Connections
        private MySqlConnection readEPGPconnection;
        private MySqlConnection writeEPGPconnection;
        private MySqlConnection lockConnection;
        private MySqlConnection readLogConnection;
        private MySqlConnection writeLogConnection;
        
        // Tables
        public delegate void refreshNew();
        public refreshNew refreshDelegate;
        private Thread refreshThread;
        private DateTime tableModTime;
        private Int32 tempIndex = 9000;
        private Boolean logging = true;
        FileSystemWatcher logWatcher = null;

        // Settings
        private const double minGP = 5.0;
        private const String settingsPath = "settings.xml";

        // Defaults
        public int settingsOverlayX = 100;
        public int settingsOverlayY = 100;
        private double settingsOverlayOpacity = 0.5;
        private String settingsRiftDir = "";
        Boolean overlayBorder = true;
        Boolean overlayToggle = false;
        private String guildID = "10010001";

        // Player
        private string currentZone;

        // Overlay
        overlay overlayForm;

        #endregion // Variables

        #region Intialize

        public guildManagement()
        {
            InitializeComponent();

            loggedIn = false;

            tableModTime = DateTime.MinValue;
        }

        private void guildManagement_Close(object sender, FormClosedEventArgs e)
        {
            if (loggedIn)
            {
                if (lockConnection.State == ConnectionState.Closed) lockConnection.Open();
                MySqlCommand unlockCommand = new MySqlCommand("UPDATE locks SET locked=0 WHERE guildID='" + guildID + "'", lockConnection);
                unlockCommand.ExecuteNonQuery();
                lockConnection.Close();
            }
            saveSettings();
            if (refreshThread != null) refreshThread.Abort();
            Application.Exit();
        }

        private void guildManagement_Load(object sender, EventArgs e)
        {
            // Set up general connections to pull EPGP and log tables
            string readEPGPConString = "server=personaguild.com; User Id=persona_read; database=persona_EPGP; Password=kr7OI=&J&,!F2BrHsC";
            string readLogConString = "server=personaguild.com; User Id=persona_read; database=persona_log; Password=kr7OI=&J&,!F2BrHsC";
            readEPGPconnection = new MySqlConnection(readEPGPConString);
            readLogConnection = new MySqlConnection(readLogConString);

            // Fill the EPGP table for the first time and set mod time
            refreshNewerTable();

            // Formats columns to fit
            EPGPspreadsheet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Lock the table to editing
            EPGPspreadsheet.ReadOnly = true;

            // Formatting
            try
            {
                EPGPspreadsheet.Columns["Name"].Width = 75;
                EPGPspreadsheet.Columns["EP"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                EPGPspreadsheet.Columns["EP"].DefaultCellStyle.Format = "0.00";
                EPGPspreadsheet.Columns["EP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                EPGPspreadsheet.Columns["GP"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                EPGPspreadsheet.Columns["GP"].DefaultCellStyle.Format = "0.00";
                EPGPspreadsheet.Columns["GP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                EPGPspreadsheet.Columns["PR"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                EPGPspreadsheet.Columns["PR"].DefaultCellStyle.Format = "0.00";
                EPGPspreadsheet.Columns["PR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                EPGPspreadsheet.Columns["LGP"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                EPGPspreadsheet.Columns["LGP"].DefaultCellStyle.Format = "0.00";
                EPGPspreadsheet.Columns["LGP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                EPGPspreadsheet.Columns["LPR"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                EPGPspreadsheet.Columns["LPR"].DefaultCellStyle.Format = "0.00";
                EPGPspreadsheet.Columns["LPR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                EPGPspreadsheet.Columns["Present"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                EPGPspreadsheet.Columns["Standby"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                EPGPspreadsheet.Columns["Posted"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                EPGPspreadsheet.Columns["Raider"].Visible = false;

                raiderNameColor();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You do not have access to the database. Ask the administrator for permission.", "Permission Denied");
                this.Close();
            }

            // Highlight name on cell click
            EPGPspreadsheet.CellClick += Cell_Clicked;

            // Link
            lbl_webLink.Links[0].LinkData = lbl_webLink.Text;

            // Refresh Table Timer
            refreshDelegate = new refreshNew(refreshNewerTable);
            refreshTable refreshTbl = new refreshTable();
            refreshThread = new Thread(delegate()
                {
                    refreshTbl.refresh(this);
                });
            refreshThread.Start();

            // This currently throws a SQL error when not on our computers
            // Fill log table for the first time 
            updateLogTable();

        }

        private void guildManagement_Activated(object sender, EventArgs e)
        {
            this.Activated -= guildManagement_Activated;
            // Work-around to make the 'select rift directory' form show up correctly

            // Load settings
            loadSettings();

            // Watch for change in log.txt file
            try
            {
                logWatcher = new FileSystemWatcher();
                logWatcher.Path = this.settingsRiftDir;// "C:\\Program Files (x86)\\RIFT Game";
                logWatcher.Filter = "Log.txt";
                logWatcher.Changed += new FileSystemEventHandler(textLogParser);
                logWatcher.Created += new FileSystemEventHandler(textLogParser);
                logWatcher.EnableRaisingEvents = true;
            }
            catch (Exception)
            {
                MessageBox.Show("RIFT is not installed. In game functionality is blocked. Install RIFT and restart Persona Guild Management to use in game functionality.", "RIFT not installed");
                logWatcher = null;
            }

            // Moved here so it's can reference to overlayForm
            // Find zone
            zoneParser();

            // Moved here so rift directory is found
            // Display time of last raid.xml creation
            xmlAge();

            // Set text in settings to current Rift directory
            lbl_currentDirValue.Text = settingsRiftDir;
            lbl_currentDirValue.Left = (this.settingsTab.Width / 2) - (lbl_currentDirValue.Width / 2);

            // Enable timers
            // Had to move them here so they don't try to reference to something that isn't created yet
            logWatch.Enabled = true;
            overlayReset.Enabled = true;
        }

        private void versionCheck()
        {

        }

        #endregion // Intialize

        #region Info

        private void logWatchElapse(object sender, ElapsedEventArgs e)
        {
            lbl_logWarningValue.Text = "/log might not be running";
            lbl_logWarningValue.ForeColor = Color.Red;
            lbl_logWarningValue.Left = (this.infoTab.Width / 2) - (lbl_logWarningValue.Width / 2);
        }

        private void overlayResetElapse(object sender, EventArgs e)
        {
            overlayForm.lbl_overlayName.Text = "";
            overlayForm.lbl_overlayPR.Text = "";
        }

        private void lbl_webLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo(e.Link.LinkData.ToString());
            Process.Start(info);
        }

        private void alphaSortButton_Click(object sender, EventArgs e)
        {
            BindingSource bs = (BindingSource)EPGPspreadsheet.DataSource;
            bs.Sort = "Name ASC";
            raiderNameColor();
            EPGPspreadsheet.Focus();
        }

        private void PRsortButton_Click(object sender, EventArgs e)
        {
            BindingSource bs = (BindingSource)EPGPspreadsheet.DataSource;
            bs.Sort = "Present DESC, Standby DESC, PR DESC, Name ASC";
            raiderNameColor();
            EPGPspreadsheet.Focus();
        }

        private void LPRsortButton_Click(object sender, EventArgs e)
        {
            BindingSource bs = (BindingSource)EPGPspreadsheet.DataSource;
            bs.Sort = "Present DESC, Standby DESC, LPR DESC, Name ASC";
            raiderNameColor();
            EPGPspreadsheet.Focus();
        }

        #endregion // Info

        #region Admin

        private void txt_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginFunction();
                e.SuppressKeyPress = true;
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            loginFunction();
        }

        private bool loginFunction()
        {
            login_name = txt_name.Text;
            login_pass = txt_pass.Text;

            // Hashing of password for security
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] interHash = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(login_pass + login_name));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < interHash.Length; i++)
            {
                sb.Append(interHash[i].ToString("x2"));
            }
            string interStr = sb.ToString();
            byte[] finalHash = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(login_pass + interStr));
            sb.Clear();
            for (int i = 0; i < finalHash.Length; i++)
            {
                sb.Append(finalHash[i].ToString("x2"));
            }
            string passMD5 = sb.ToString();

            // Set values to pass to php file
            NameValueCollection nvcLoginInfo = new NameValueCollection();
            nvcLoginInfo.Add("username", login_name);
            nvcLoginInfo.Add("pass", passMD5);

            // Call php
            result = client.UploadValues("http://personaguild.com/pgm/login.php", nvcLoginInfo);
            k = Encoding.UTF8.GetString(result, 0, result.Length);

            // Check echos
            String[] returnArgs = k.Split(',');
            switch (returnArgs[0])
            {
                case "true":
                    var enc_write_password = Convert.FromBase64String(returnArgs[1]);

                    var encoding = new UTF8Encoding();
                    var IV = encoding.GetBytes("45287112549354892144548565456541");
                    var Key = encoding.GetBytes("anjueolkdiwpoida");

                    using (var rj = new RijndaelManaged())
                    {
                        try
                        {
                            rj.Padding = PaddingMode.PKCS7;
                            rj.Mode = CipherMode.CBC;
                            rj.KeySize = 256;
                            rj.BlockSize = 256;
                            rj.Key = Key;
                            rj.IV = IV;
                            var ms = new MemoryStream(enc_write_password);

                            using (var cs = new CryptoStream(ms, rj.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                            {
                                using (var sr = new StreamReader(cs))
                                {
                                    String decryptedStr = sr.ReadLine();
                                    String[] splitDecStr = decryptedStr.Split(',');
                                    write_password = splitDecStr[0];
                                    guildID = splitDecStr[1];
                                }
                            }
                        }
                        finally
                        {
                            rj.Clear();
                        }
                    }

                    // Create Lock Connection
                    string lockConString = "server=personaguild.com; User Id=persona_write; database=persona_pgm; Password=" + write_password;
                    lockConnection = new MySqlConnection(lockConString);

                    // Try to grab lock
                    if (lockConnection.State == ConnectionState.Closed) lockConnection.Open();
                    MySqlCommand checkCommand = new MySqlCommand("SELECT locked FROM guilds WHERE guildID='" + guildID + "' AND locked=0", lockConnection);
                    MySqlCommand lockCommand = new MySqlCommand("UPDATE guilds SET locked=1 WHERE guildID='" + guildID + "'", lockConnection);
                    MySqlDataReader dataReader;
                    dataReader = checkCommand.ExecuteReader();
                    // If query doesn't return null, lock sql
                    if (dataReader.Read())
                    {
                        dataReader.Close();
                        lockCommand.ExecuteNonQuery();

                        // Show admin buttons, unlock editing of app table, and format logged in text
                        lbl_admin_epfunc.Show();
                        attendanceButton.Show();
                        lbl_raidxmlTitle.Show();
                        lbl_raidxmlDate.Show();
                        fiveEPbutton.Show();
                        tenEPbutton.Show();
                        lbl_admin_users.Show();
                        addUserButton.Show();
                        deleteUserButton.Show();
                        undoButton.Show();
                        onTimeButton.Show();
                        raiderStatusButton.Show();
                        EPGPspreadsheet.ReadOnly = false;
                        EPGPspreadsheet.Columns["Name"].ReadOnly = true;
                        EPGPspreadsheet.Columns["PR"].ReadOnly = true;
                        EPGPspreadsheet.Columns["LPR"].ReadOnly = true;
                        EPGPspreadsheet.CellValidating += EPGPspreadsheet_CellValidating;
                        lbl_loggedIn.Text = "Logged In as " + login_name;
                        lbl_loggedIn.ForeColor = Color.Green;
                        loggedIn = true;

                        // Set up write connections
                        string writeEPGPconString = "server=personaguild.com; User Id=persona_write; database=persona_log; Password=" + write_password;
                        string writeLogConString = "server=personaguild.com; User Id=persona_write; database=persona_log; Password=" + write_password;
                        writeLogConnection = new MySqlConnection(writeLogConString);
                        writeEPGPconnection = new MySqlConnection(writeEPGPconString);

                        // Stop Refreshing since we are the only ones editing
                        refreshThread.Abort();
                    }
                    // Otherwise, notify user and break
                    else
                    {
                        MessageBox.Show("An officer is already logged in.", "Can't log in");
                        login_name = string.Empty;
                        login_pass = string.Empty;
                        break;
                    }
                    break;

                case "false":
                    MessageBox.Show("Invalid login information", "Login failed");
                    login_name = string.Empty;
                    login_pass = string.Empty;
                    break;

                case "FAILURE":
                    MessageBox.Show("login.php threw an error. Please contact an administrator with the circumstances that caused this error.", "PHP file error");
                    break;
            }
           
            // Clear login text boxes
            txt_name.Text = "";
            txt_pass.Text = "";
            
            return loggedIn;
        }

        private void attendanceButton_Click(object sender, EventArgs e)
        {
            string[] raidArray = new string[20];
            XmlTextReader reader = new XmlTextReader(settingsRiftDir + "\\raid.xml");
            int tempInt = 0;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "Name")
                    {
                        if (reader.Read())
                        {
                            raidArray[tempInt++] = reader.Value;
                        }
                    }
                }
            }

            // Update SQL
            try
            {
                if (writeEPGPconnection.State == ConnectionState.Closed) writeEPGPconnection.Open();

                MySqlCommand command = writeEPGPconnection.CreateCommand();
                MySqlTransaction trans = writeEPGPconnection.BeginTransaction();

                command.Connection = writeEPGPconnection;
                command.Transaction = trans;

                foreach (String s in raidArray)
                {
                    if (s != "")
                    {
                        command.CommandText = "INSERT INTO EPGP (name, present) VALUES ('" + s + "', 1) ON DUPLICATE KEY UPDATE present=1,standby=0";
                        command.ExecuteNonQuery();
                    }
                }
                trans.Commit();

                updateTable();
            }
            catch (MySqlException)
            {
                // Didn't work
            }
            finally
            {
                if (writeEPGPconnection.State == ConnectionState.Open) writeEPGPconnection.Close();
            }
            xmlAge();
            EPGPspreadsheet.Focus();
        }

        private void onTimeButton_Click(object sender, EventArgs e)
        {
            if (executeSQLUpdate("UPDATE EPGP SET ep=ep+5 WHERE present=1 OR standby=1", new object[] { }) && executeSQLUpdate("UPDATE EPGP SET ep=ep-50 WHERE raider=1 AND posted=0 AND present=0 AND standby=0", new object[] { }))
            {
                updateTable();
                // Log
                string presentCSV = "";
                string absentCSV = "";
                foreach (DataGridViewRow row in EPGPspreadsheet.Rows)
                {
                    if (((Boolean)row.Cells["Present"].Value == true) || ((Boolean)row.Cells["Standby"].Value == true))
                    {
                        presentCSV += row.Cells["Name"].Value.ToString() + ",";
                        continue;
                    }
                    if (((Boolean)row.Cells["Raider"].Value == true) && ((Boolean)row.Cells["Present"].Value == false) && ((Boolean)row.Cells["Standby"].Value == false) && ((Boolean)row.Cells["Posted"].Value == false))
                    {
                        absentCSV += row.Cells["Name"].Value.ToString() + ",";
                    }
                }
                if (writeLogConnection.State == ConnectionState.Closed) writeLogConnection.Open();
                string currentDate = DateTime.Today.Month.ToString() + "/" + DateTime.Today.Day.ToString() + "/" + DateTime.Today.Year.ToString();
                if (presentCSV != "")
                {
                    string presentSQLstring = "INSERT INTO log (name, number, type, reason, date, officer) VALUES ('" + presentCSV + "', '5', 'EP', 'On Time', '" + currentDate + "', '" + login_name + "')";
                    MySqlCommand presentCommand = new MySqlCommand(presentSQLstring, writeLogConnection);
                    presentCommand.ExecuteNonQuery();
                }
                if (absentCSV != "")
                {
                    string absentSQLstring = "INSERT INTO log (name, number, type, reason, date, officer) VALUES ('" + absentCSV + "', '-50', 'EP', 'Absent', '" + currentDate + "', '" + login_name + "')";
                    MySqlCommand absentCommand = new MySqlCommand(absentSQLstring, writeLogConnection);
                    absentCommand.ExecuteNonQuery();
                }
                if (writeLogConnection.State == ConnectionState.Open) writeLogConnection.Close();
                // Update log table
                updateLogTable();
            }
            EPGPspreadsheet.Focus();
        }

        private void fiveEPbutton_Click(object sender, EventArgs e)
        {
            if (executeSQLUpdate("UPDATE EPGP SET ep=ep+5 WHERE present=1 OR standby=1", new object[] { }))
            {
                updateTable();
                // Log
                changeReasonMessage changeReasonPopup = new changeReasonMessage();
                var result = changeReasonPopup.ShowDialog(this);
                string reason = string.Empty;
                if ((result == DialogResult.OK) && (changeReasonPopup.Reason != ""))
                {
                    reason = changeReasonPopup.Reason;
                    if (reason.IndexOf("'") > 0)
                    {
                        reason = reason.Remove(reason.IndexOf("'"), 1);
                    }
                }
                else
                {
                    reason = "Default";
                    MessageBox.Show("Default reason used", "Reason");
                }
                string memberCSV = "";
                foreach (DataGridViewRow row in EPGPspreadsheet.Rows)
                {
                    if (((Boolean)row.Cells["Present"].Value == true) || ((Boolean)row.Cells["Standby"].Value == true))
                    {
                        memberCSV += row.Cells["Name"].Value.ToString() + ",";
                    }
                }
                if (memberCSV == "")
                {
                    MessageBox.Show("No one is present or on standby.", "Error");
                    return;
                }
                if (writeLogConnection.State == ConnectionState.Closed) writeLogConnection.Open();
                string currentDate = DateTime.Today.Month.ToString() + "/" + DateTime.Today.Day.ToString() + "/" + DateTime.Today.Year.ToString();
                string logSQLstring = "INSERT INTO log (name, number, type, reason, date, officer) VALUES ('" + memberCSV + "', '5', 'EP', '" + reason + "', '" + currentDate + "', '" + login_name + "')";
                MySqlCommand logCommand = new MySqlCommand(logSQLstring, writeLogConnection);
                logCommand.ExecuteNonQuery();
                if (writeLogConnection.State == ConnectionState.Open) writeLogConnection.Close();
                // Update log table
                updateLogTable();
            }
            EPGPspreadsheet.Focus();
        }

        private void tenEPbutton_Click(object sender, EventArgs e)
        {
            if (executeSQLUpdate("UPDATE EPGP SET ep=ep+10 WHERE present=1 OR standby=1", new object[] { }))
            {
                updateTable();
                // Log
                changeReasonMessage changeReasonPopup = new changeReasonMessage();
                var result = changeReasonPopup.ShowDialog(this);
                string reason = string.Empty;
                if ((result == DialogResult.OK) && (changeReasonPopup.Reason != ""))
                {
                    reason = changeReasonPopup.Reason;
                    if (reason.IndexOf("'") > 0)
                    {
                        reason = reason.Remove(reason.IndexOf("'"), 1);
                    }
                }
                else
                {
                    reason = "Default";
                    MessageBox.Show("Default reason used", "Reason");
                }
                string memberCSV = "";
                foreach (DataGridViewRow row in EPGPspreadsheet.Rows)
                {
                    if (((Boolean)row.Cells["Present"].Value == true) || ((Boolean)row.Cells["Standby"].Value == true))
                    {
                        memberCSV += row.Cells["Name"].Value.ToString() + ",";
                    }
                }
                if (memberCSV == "")
                {
                    MessageBox.Show("No one is present or on standby.", "Error");
                    return;
                }
                if (writeLogConnection.State == ConnectionState.Closed) writeLogConnection.Open();
                string currentDate = DateTime.Today.Month.ToString() + "/" + DateTime.Today.Day.ToString() + "/" + DateTime.Today.Year.ToString();
                string logSQLstring = "INSERT INTO log (name, number, type, reason, date, officer) VALUES ('" + memberCSV + "', '10', 'EP', '" + reason + "', '" + currentDate + "', '" + login_name + "')";
                MySqlCommand logCommand = new MySqlCommand(logSQLstring, writeLogConnection);
                logCommand.ExecuteNonQuery();
                if (writeLogConnection.State == ConnectionState.Open) writeLogConnection.Close();
                // Update log table
                updateLogTable();
            }
            EPGPspreadsheet.Focus();
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            addUserMessage addUserPopup = new addUserMessage();
            var result = addUserPopup.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                try
                {
                    if (writeEPGPconnection.State == ConnectionState.Closed) writeEPGPconnection.Open();
                    MySqlCommand addCommand = new MySqlCommand("INSERT INTO EPGP (name) VALUES ('" + addUserPopup.MemberName + "')", writeEPGPconnection);
                    addCommand.ExecuteNonQuery();
                    if (writeEPGPconnection.State == ConnectionState.Open) writeEPGPconnection.Close();
                    updateTable();
                }
                catch (MySqlException)
                {
                    MessageBox.Show("Member already exists.", "Add User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            EPGPspreadsheet.Focus();
        }

        private void deleteUserButton_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to delete " + EPGPspreadsheet.Rows[EPGPspreadsheet.SelectedCells[0].RowIndex].Cells["Name"].Value.ToString() + "?";
            var result = MessageBox.Show(message, "Delete User", MessageBoxButtons.YesNo);

            // If the no button was pressed, return
            if (result == DialogResult.No)
            {
                return;
            }
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (writeEPGPconnection.State == ConnectionState.Closed) writeEPGPconnection.Open();
                    MySqlCommand deleteCommand = new MySqlCommand("DELETE FROM EPGP WHERE name = '" + EPGPspreadsheet.Rows[EPGPspreadsheet.SelectedCells[0].RowIndex].Cells["Name"].Value.ToString() + "'", writeEPGPconnection);
                    deleteCommand.ExecuteNonQuery();
                    if (writeEPGPconnection.State == ConnectionState.Open) writeEPGPconnection.Close();
                    EPGPspreadsheet.Rows.Remove(EPGPspreadsheet.CurrentRow);
                }
                catch (MySqlException)
                {
                    MessageBox.Show("Delete failed.", "Delete User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            EPGPspreadsheet.Focus();
        }

        private void raiderStatusButton_Click(object sender, EventArgs e)
        {
            if ((Boolean)EPGPspreadsheet.Rows[EPGPspreadsheet.SelectedCells[0].RowIndex].Cells["Raider"].Value == true)
            {
                EPGPspreadsheet.Rows[EPGPspreadsheet.SelectedCells[0].RowIndex].Cells["Raider"].Value = false;
                EPGPspreadsheet.Rows[EPGPspreadsheet.SelectedCells[0].RowIndex].Cells["Name"].Style.ForeColor = Color.Black;
            }
            else
            {
                EPGPspreadsheet.Rows[EPGPspreadsheet.SelectedCells[0].RowIndex].Cells["Raider"].Value = true;
                EPGPspreadsheet.Rows[EPGPspreadsheet.SelectedCells[0].RowIndex].Cells["Name"].Style.ForeColor = Color.Blue;
            }
            EPGPspreadsheet.Focus();
        }

        private void xmlAge()
        {
            TimeSpan xmlAgeTime = DateTime.Now.Subtract(File.GetLastWriteTime(settingsRiftDir + "\\raid.xml"));
            lbl_raidxmlDate.Text = xmlAgeTime.Days.ToString() + " days "  + xmlAgeTime.Hours.ToString() + " hours " + xmlAgeTime.Minutes.ToString() + " minutes ago";
            lbl_raidxmlDate.Left = (this.adminTab.Width / 2) - (lbl_raidxmlDate.Width / 2);
            if ((xmlAgeTime.Hours < 4) && (xmlAgeTime.Days == 0))
            {
                lbl_raidxmlDate.ForeColor = Color.Green;
            }
            else
            {
                lbl_raidxmlDate.ForeColor = Color.Red;
            }
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            if (writeLogConnection.State == ConnectionState.Closed) writeLogConnection.Open();
            MySqlCommand undoCommand = new MySqlCommand("SELECT * FROM log ORDER BY id DESC LIMIT 1", writeLogConnection);
            adapter.SelectCommand = undoCommand;
            adapter.Fill(table);
            if (writeLogConnection.State == ConnectionState.Open) writeLogConnection.Close();
            DataRow SQLrow = table.Rows[0];
            if (SQLrow["name"].ToString().IndexOf(',') == -1)
            {
                if (SQLrow["name"].ToString() != "Everyone")
                {
                    foreach (DataGridViewRow DGVrow in EPGPspreadsheet.Rows)
                    {
                        if (SQLrow["name"].ToString() == DGVrow.Cells["Name"].Value.ToString())
                        {
                            DGVrow.Cells[SQLrow["type"].ToString()].Value = Double.Parse(DGVrow.Cells[SQLrow["type"].ToString()].Value.ToString()) - Double.Parse(SQLrow["number"].ToString());
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Last change was a raid-wide decay; Can't undo", "Can't Undo");
                }
            }
            else
            {
                // Temporarily disable logging
                logging = false;
                string[] members = SQLrow["name"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (String str in members)
                {
                    foreach (DataGridViewRow DGVrow in EPGPspreadsheet.Rows)
                    {
                        if (str == DGVrow.Cells["Name"].Value.ToString())
                        {
                            DGVrow.Cells[SQLrow["type"].ToString()].Value = Double.Parse(DGVrow.Cells[SQLrow["type"].ToString()].Value.ToString()) - Double.Parse(SQLrow["number"].ToString());
                            break;
                        }
                    }
                }
                // Manually log 
                if (writeLogConnection.State == ConnectionState.Closed) writeLogConnection.Open();
                string currentDate = DateTime.Today.Month.ToString() + "/" + DateTime.Today.Day.ToString() + "/" + DateTime.Today.Year.ToString();
                string logSQLstring = "INSERT INTO log (name, number, type, reason, date, officer) VALUES ('" + SQLrow["name"] + "', '-" + SQLrow["number"] + "', 'EP', 'Raid-wide', '" + currentDate + "', '" + login_name + "')";
                MySqlCommand logCommand = new MySqlCommand(logSQLstring, writeLogConnection);
                logCommand.ExecuteNonQuery();
                if (writeLogConnection.State == ConnectionState.Open) writeLogConnection.Close();
                // Update log table
                updateLogTable();
                // Re-enable logging
                logging = true;
            }
        }

        #endregion // Admin

        #region Settings

        private void getDirButton_Click(object sender, EventArgs e)
        {
            getRiftDir();
            lbl_currentDirValue.Text = settingsRiftDir;
            lbl_currentDirValue.Left = (this.settingsTab.Width / 2) - (lbl_currentDirValue.Width / 2);
        }

        private void overlayButton_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (overlayForm.Visible == false)
                {
                    if (overlayForm.IsDisposed)
                        overlayForm = new overlay(this, settingsOverlayX, settingsOverlayY, settingsOverlayOpacity);
                    overlayForm.Show();
                }
                else
                {
                    overlayForm.Hide();
                }
                this.Focus();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (overlayForm.FormBorderStyle == System.Windows.Forms.FormBorderStyle.FixedSingle)
                {
                    overlayForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                }
                else
                {
                    overlayForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                }
            }
        }

        public bool saveSettings()
        {
            XmlTextWriter xml = new XmlTextWriter(settingsPath, null);
            xml.Formatting = Formatting.Indented;

            xml.WriteStartDocument();
                xml.WriteStartElement("EPGPTool");
                    xml.WriteStartElement("Overlay");
                        xml.WriteStartElement("X");
                            xml.WriteString(settingsOverlayX.ToString());
                        xml.WriteEndElement();
                        xml.WriteStartElement("Y");
                            xml.WriteString(settingsOverlayY.ToString());
                        xml.WriteEndElement();
                        xml.WriteStartElement("Opacity");
                            xml.WriteString(settingsOverlayOpacity.ToString());
                        xml.WriteEndElement();
                        xml.WriteStartElement("Border");
                            // If it's null then that means it wasn't created and needs to be set to default
                            if (overlayForm == null || overlayForm.FormBorderStyle == System.Windows.Forms.FormBorderStyle.FixedSingle)
                            {
                                xml.WriteString("FixedSingle");
                            }
                            else
                            {
                                xml.WriteString("None");
                            }
                        xml.WriteEndElement();
                        xml.WriteStartElement("Toggle");
                            if (overlayForm == null)
                            {
                                xml.WriteString("False");
                            }
                            else
                            {
                                xml.WriteString(overlayForm.Visible.ToString());
                            }
                        xml.WriteEndElement();
                    xml.WriteEndElement();
                    xml.WriteStartElement("RiftDirectory");
                        if (!settingsRiftDir.Equals(""))
                        {
                            xml.WriteString(settingsRiftDir);
                        }
                        else
                        {
                            xml.WriteWhitespace("");
                        }
                    xml.WriteEndElement();
                xml.WriteEndElement();
            xml.WriteEndDocument();

            xml.Close();

            return true;
        }

        public bool loadSettings()
        {
            if (File.Exists(settingsPath))
            {
                XmlTextReader xml = new XmlTextReader(settingsPath);

                try
                {
                    while (xml.Read())
                    {
                        if (xml.NodeType == XmlNodeType.Element)
                        {
                            if (xml.Name == "Overlay")
                            {
                                while (xml.Read() && !(xml.NodeType == XmlNodeType.EndElement && xml.Name == "Overlay"))
                                {
                                    if (xml.NodeType == XmlNodeType.Element)
                                    {
                                        if (xml.Name == "X")
                                        {
                                            if (xml.Read())
                                            {
                                                settingsOverlayX = Convert.ToInt32(xml.Value);
                                            }
                                        }
                                        if (xml.Name == "Y")
                                        {
                                            if (xml.Read())
                                            {
                                                settingsOverlayY = Convert.ToInt32(xml.Value);
                                            }
                                        }
                                        if (xml.Name == "Opacity")
                                        {
                                            if (xml.Read())
                                            {
                                                settingsOverlayOpacity = Convert.ToDouble(xml.Value);
                                                opacitySlider.ValueChanged -= opacitySlider_ValueChanged;
                                                txt_opacity.TextChanged -= txt_opacity_TextChanged;
                                                opacitySlider.Value = Convert.ToInt32(settingsOverlayOpacity * 100);
                                                txt_opacity.Text = Convert.ToInt32(settingsOverlayOpacity * 100).ToString();
                                                opacitySlider.ValueChanged += opacitySlider_ValueChanged;
                                            }
                                        }
                                        if (xml.Name == "Border")
                                        {
                                            if (xml.Read())
                                            {
                                                if (xml.Value == "None")
                                                {
                                                    overlayBorder = false;
                                                }
                                                if (xml.Value == "FixedSingle")
                                                {
                                                    overlayBorder = true;
                                                }
                                            }
                                        }
                                        if (xml.Name == "Toggle")
                                        {
                                            if (xml.Read())
                                            {
                                                if (xml.Value == "True")
                                                {
                                                    overlayToggle = true;
                                                }
                                                if (xml.Value == "False")
                                                {
                                                    overlayToggle = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (xml.Name == "RiftDirectory")
                            {
                                xml.Read();
                                settingsRiftDir = xml.Value;
                            }
                            else if (xml.Name == "GuildID")
                            {
                                xml.Read();

                            }

                        }
                    }
                }
                catch (Exception)
                {
                    // Reset Settings
                    xml.Close();
                    saveSettings();
                }
                xml.Close();
               

                // No directory loaded make them pick again
                if (settingsRiftDir.Equals(""))
                {
                    // Save if they pick a directory
                    if (getRiftDir())
                        saveSettings();
                }
            }
            else
            {
                getRiftDir();
                // Save Defaults
                saveSettings();
            }

            // Create overlay form
            overlayForm = new overlay(this, settingsOverlayX, settingsOverlayY, settingsOverlayOpacity);

            // Set border style
            if (overlayBorder)
            {
                overlayForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            }
            else
            {
                overlayForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            }

            // Set toggle
            if (overlayToggle)
            {
                overlayForm.Show();
            }
            else
            {
                overlayForm.Hide();
            }

            // Remove Text
            overlayForm.lbl_overlayName.Text = "";
            overlayForm.lbl_overlayPR.Text = "";

            return true;
        }

        private bool getRiftDir()
        {
            // Get Rift Directory
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select your RIFT directory";
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                settingsRiftDir = fbd.SelectedPath;
                try
                {
                    logWatcher = new FileSystemWatcher();
                    logWatcher.Path = this.settingsRiftDir;
                    logWatcher.Filter = "Log.txt";
                    logWatcher.Changed += new FileSystemEventHandler(textLogParser);
                    logWatcher.Created += new FileSystemEventHandler(textLogParser);
                    logWatcher.EnableRaisingEvents = true;
                }
                catch (Exception)
                {
                    // Log didn't start
                    logWatcher = null;
                }
                return true;
            }
            else
            {
                MessageBox.Show(this, "EPGPTool will not function correctly until you choose the correct RIFT directory.\nGo to the settings tab to change rift directory.", "RIFT Directory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void txt_opacity_TextChanged(object sender, EventArgs e)
        {
            opacitySlider.ValueChanged -= opacitySlider_ValueChanged;
            // Data validation to make sure value is an int
            try
            {
                // If text has a value, set the value of the slider to the same and save to the settings file
                if (txt_opacity.Text != "")
                {
                    // Data validation to make sure the value is between 0 and 100
                    if ((Convert.ToInt32(txt_opacity.Text) > 100) || (Convert.ToInt32(txt_opacity.Text) < 0))
                    {
                        MessageBox.Show("Invalid entry. Enter an integer between 0 - 100");
                        txt_opacity.Text = "50";
                        opacitySlider.Value = 50;
                        settingsOverlayOpacity = 0.5;
                        overlayForm.Opacity = settingsOverlayOpacity;
                        opacitySlider.ValueChanged += opacitySlider_ValueChanged;
                        return;
                    }
                    else
                    {
                        opacitySlider.Value = Convert.ToInt32(txt_opacity.Text);
                        settingsOverlayOpacity = Convert.ToDouble(opacitySlider.Value) / 100;
                        overlayForm.Opacity = settingsOverlayOpacity;
                    }
                    
                }
            }
            catch
            {
                MessageBox.Show("Enter an integer between 0 - 100", "Invalid entry");
                txt_opacity.Text = "50";
                opacitySlider.Value = 50;
                settingsOverlayOpacity = 0.5;
                overlayForm.Opacity = settingsOverlayOpacity;
            }
            opacitySlider.ValueChanged += opacitySlider_ValueChanged;
        }
        
        // Update the text box value on slide
        private void opacitySlider_ValueChanged(object sender, EventArgs e)
        {
            txt_opacity.TextChanged -= txt_opacity_TextChanged;
            txt_opacity.Text = opacitySlider.Value.ToString();
            settingsOverlayOpacity = Convert.ToDouble(opacitySlider.Value) / 100;
            overlayForm.Opacity = settingsOverlayOpacity;
            txt_opacity.TextChanged += txt_opacity_TextChanged;
        }

        #endregion // Settings

        #region Txt log and zone parsers

        private void textLogParser(object source, FileSystemEventArgs e)
        {
            // Change logWatch text to say /log is running
            if (lbl_logWarningValue.Text != "/log is running")
            {
                lbl_logWarningValue.Text = "/log is running";
                lbl_logWarningValue.ForeColor = Color.Green;
                lbl_logWarningValue.Left = (this.infoTab.Width / 2) - (lbl_logWarningValue.Width / 2);
            }

            // Reset logWatch timer
            if (logWatch.Enabled == true)
            {
                logWatch.Stop();
                logWatch.Start();
            }

            // Try to open and parse log.txt file
            try
            {
                FileStream fs = new FileStream(settingsRiftDir + "\\log.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(fs);
                string linesBlock;
                // Try to get the last 1024 bytes of data from the file
                try
                {
                    reader.BaseStream.Seek(-500, SeekOrigin.End);
                    linesBlock = reader.ReadToEnd();
                }
                // If it fails, instead get the entire file
                catch (IOException)
                {
                    linesBlock = reader.ReadToEnd();
                }
                fs.Close();
                reader.Close();
                // Split into lines and get the last line
                string[] lines = linesBlock.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string lastLine = lines[lines.Length - 1];

                // Extract the zone from the channel name and set it to the current zone
                if (lastLine.IndexOf("[1. ") != -1)
                {
                    string zoneString = lastLine.Substring(10, lastLine.Length - 10);
                    currentZone = zoneString.Substring(zoneString.IndexOf("[1. ") + 4, zoneString.IndexOf("]") - zoneString.IndexOf("[") - 4);
                    lbl_currentZoneValue.Text = currentZone;
                    lbl_currentZoneValue.Left = (this.infoTab.Width / 2) - (lbl_currentZoneValue.Width / 2);
                }

                // Only do overlay text if the user is in a raid zone
                if ((currentZone == "Greenscale's Blight") || (currentZone == "River of Souls"))
                {
                    // Non-raid speech lines are ignored           /////Idea: create channel for EPGP so the program doesn't have to read raid chatter
                    if (lastLine.IndexOf("[Raid]") != -1)
                    {
                        // Trim off the time stamp
                        string overlayString = lastLine.Substring(10, lastLine.Length - 10);
                        // Split into name and text
                        string[] logList = overlayString.Split(':');
                        // Check for phrase
                        if ((logList[1].IndexOf("need") > 0) || (logList[1].IndexOf("off spec") > 0) || (logList[1].IndexOf("offspec") > 0))
                        {
                            // Reset timer
                            overlayReset.Stop();
                            overlayReset.Start();

                            int tempInt = 0;
                            string tempString = string.Empty;
                            // Trim off [raid] and the brackets around the name
                            logList[0] = logList[0].Substring(7, logList[0].Length - 8);
                            // Cycle through the names on the spreadsheet
                            while (tempInt < EPGPspreadsheet.RowCount)
                            {
                                tempString = EPGPspreadsheet.Rows[tempInt].Cells["Name"].Value.ToString();
                                if (tempString == logList[0])
                                {
                                    // If the label is empty, enter in the text
                                    if (overlayForm.lbl_overlayName.Text == "")
                                    {
                                        overlayForm.lbl_overlayName.Text = logList[0];
                                        if (logList[1] == " need")
                                        {
                                            // Try to format PR to only 2 decimals
                                            try
                                            {
                                                overlayForm.lbl_overlayPR.Text = EPGPspreadsheet.Rows[tempInt].Cells["PR"].Value.ToString().Substring(0, EPGPspreadsheet.Rows[tempInt].Cells["PR"].Value.ToString().IndexOf('.') + 3);
                                            }
                                            // If it fails, use the whole thing
                                            catch (ArgumentOutOfRangeException)
                                            {
                                                overlayForm.lbl_overlayPR.Text = EPGPspreadsheet.Rows[tempInt].Cells["PR"].Value.ToString();
                                            }
                                        }
                                        if ((logList[1] == " off spec") || (logList[1] == " offspec"))
                                        {
                                            overlayForm.lbl_overlayPR.Text = "Off Spec";
                                        }
                                    }
                                    // Otherwise, append a newline and the text to the end of the string
                                    else
                                    {
                                        string test = EPGPspreadsheet.Rows[tempInt].Cells["PR"].Value.ToString();
                                        overlayForm.lbl_overlayName.Text += "\n" + logList[0];
                                        if (logList[1] == " need")
                                        {
                                            // Try to format PR to only 2 decimals
                                            try
                                            {
                                                overlayForm.lbl_overlayPR.Text += "\n" + EPGPspreadsheet.Rows[tempInt].Cells["PR"].Value.ToString().Substring(0, EPGPspreadsheet.Rows[tempInt].Cells["PR"].Value.ToString().IndexOf('.') + 3);
                                            }
                                            // If it fails, use the whole thing
                                            catch (ArgumentOutOfRangeException)
                                            {
                                                overlayForm.lbl_overlayPR.Text += "\n" + EPGPspreadsheet.Rows[tempInt].Cells["PR"].Value.ToString();
                                            }
                                        }
                                        if ((logList[1] == " off spec") || (logList[1] == " offspec"))
                                        {
                                            overlayForm.lbl_overlayPR.Text += "\nOff Spec";
                                        }
                                    }
                                }
                                tempInt++;
                            }
                        }
                    }
                }
            }
            catch (IOException)
            {
                return;
            }
        }

        private void zoneParser()
        {
            try
            {
                FileStream fs = File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RIFT\\recents.cfg");
                StreamReader reader = new StreamReader(fs);
                string linesBlock;
                int userIndex = -1;
                linesBlock = reader.ReadToEnd();
                fs.Close();
                reader.Close();
                // Split into lines
                string[] lines = linesBlock.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                Boolean correctSection = false;
                string loginTime = " 000000000000000000";
                string userNumber = "";

                // Read lines to find the last login section
                for (int i = 0; i < (lines.Length - 1); i++)
                {
                    if (lines[i] == "[Realm]")
                    {
                        correctSection = false;
                        break;
                    }

                    if (correctSection == true)
                    {
                        string[] tempString = lines[i].Split('=');
                        if (tempString[1].CompareTo(loginTime) > 0)
                        {
                            loginTime = tempString[1];
                            userNumber = tempString[0];
                        }
                    }

                    if (lines[i] == "[LastLoginTime]") correctSection = true;
                }

                // Read lines to find the Description section
                for (int i = 0; i < (lines.Length - 1); i++)
                {
                    if (lines[i] == "[LastLoginTime]")
                    {
                        correctSection = false;
                        break;
                    }

                    if (correctSection == true)
                    {
                        string[] tempString = lines[i].Split('=');
                        if (tempString[0] == userNumber)
                        {
                            userIndex = i;
                        }
                    }

                    if (lines[i] == "[Description]") correctSection = true;

                }
                currentZone = lines[userIndex].Substring(lines[userIndex].IndexOf("\t") + 1, lines[userIndex].IndexOf(" (") - lines[userIndex].IndexOf("\t") - 1);
                lbl_currentZoneValue.Text = currentZone;
                lbl_currentZoneValue.Left = (this.infoTab.Width / 2) - (lbl_currentZoneValue.Width / 2);
            }
            catch (IOException)
            {
                return;
            }
        }

        #endregion // Txt log and zone parsers

        #region Log table

        private void updateLogTable()
        {
            try
            {
                // Create DataTable from SQL
                DataTable logTable = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                if (readLogConnection.State == ConnectionState.Closed) readLogConnection.Open();
                MySqlCommand logTableCommand = new MySqlCommand("SELECT id as ID, parent_id as parentID, name as Member, number as Number, type as Type, reason as Reason FROM log ORDER BY id DESC LIMIT 20", readLogConnection);
                adapter.SelectCommand = logTableCommand;
                adapter.Fill(logTable);
                if (readLogConnection.State == ConnectionState.Open) readLogConnection.Close();
                int i = 0;
                while (i < logTable.Rows.Count)
                {
                    DataRow row = logTable.Rows[i];
                    if (row["Member"].ToString().IndexOf(',') != -1)
                    {
                        string[] membersArray = row["Member"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        logTable.Rows.Add(Int32.Parse(row["ID"].ToString()), 0, "Raid", Int32.Parse(row["Number"].ToString()), row["Type"].ToString(), row["Reason"].ToString());
                        foreach (String s in membersArray)
                        {
                            logTable.Rows.Add(tempIndex++, Int32.Parse(row["ID"].ToString()), s, null, "", "");
                        }
                        logTable.Rows[i].Delete();
                    }
                    i++;
                }
                BindingSource bs = new BindingSource();
                bs.DataSource = logTable;
                bs.Sort = "ID DESC";
                logSpreadsheet.DataSource = bs;
            }
            catch (MySqlException)
            {
                MessageBox.Show("Could not retrieve log table information.", "Log Error");
            }
        }

        #endregion // Log table

        #region SQL

        private bool executeSQLUpdate(String s, object[] param)
        {
            try
            {
                if (writeEPGPconnection.State == ConnectionState.Closed) writeEPGPconnection.Open();

                MySqlCommand command = new MySqlCommand(s, writeEPGPconnection);

                command.Prepare();
                for (int i = 1; i <= param.Length; i++)
                {
                    command.Parameters.AddWithValue("@" + i, param[i - 1]);
                }

                if (command != null) command.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException)
            {
                // Didn't work
                return false;
            }
            finally
            {
                if (writeEPGPconnection.State == ConnectionState.Open) writeEPGPconnection.Close();
            }
        }

        #endregion // SQL

        #region Table

        private void raiderNameColor()
        {
            foreach (DataGridViewRow row in EPGPspreadsheet.Rows)
            {
                if ((Boolean)row.Cells["Raider"].Value == true)
                {
                    row.Cells["Name"].Style.ForeColor = Color.Blue;
                }
            }
        }

        private void Cell_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Highlight name if the cell clicked isn't a header cell
                EPGPspreadsheet.Rows[e.RowIndex].Cells["Name"].Selected = true;
            }
        }

        private void Column_Changed(object sender, DataColumnChangeEventArgs e)
        {
            // Get Name
            String name = (String)e.Row["Name"];
            DataTable table = e.Column.Table;
            table.ColumnChanged -= Column_Changed;
            bool success = false;
            // Check to see which column is being changed
            if (e.Column.ColumnName.Equals("EP") || e.Column.ColumnName.Equals("GP") || e.Column.ColumnName.Equals("LGP"))
            {
                if (e.Column.ColumnName.Equals("GP"))
                    e.Row["GP"] = Math.Max((double)e.Row["GP"], minGP);
                if (e.Column.ColumnName.Equals("LGP"))
                    e.Row["LGP"] = Math.Max((double)e.Row["LGP"], minGP);
                e.Row["PR"] = (double)e.Row["EP"] / (double)e.Row["GP"];
                e.Row["LPR"] = (double)e.Row["EP"] / (double)e.Row["LGP"];
                if (e.Column.ColumnName.Equals("EP"))
                    success = executeSQLUpdate("UPDATE EPGP SET ep=@1 WHERE name=@2", new object[] { (double)e.Row["EP"], name });
                if (e.Column.ColumnName.Equals("GP"))
                    success = executeSQLUpdate("UPDATE EPGP SET gp=@1 WHERE name=@2", new object[] { (double)e.Row["GP"], name });
                if (e.Column.ColumnName.Equals("LGP"))
                    success = executeSQLUpdate("UPDATE EPGP SET lgp=@1 WHERE name=@2", new object[] { (double)e.Row["LGP"], name });
            }
            else if (e.Column.ColumnName.Equals("Present"))
            {
                if ((Boolean)e.Row["Present"] == true)
                {
                    e.Row["Standby"] = false;
                }
                success = executeSQLUpdate("UPDATE EPGP SET present=@1,standby=@2 WHERE name=@3", new object[] { ((bool)e.Row["Present"] ? 1 : 0), ((bool)e.Row["Standby"] ? 1 : 0), name });
            }
            else if (e.Column.ColumnName.Equals("Standby"))
            {
                if ((Boolean)e.Row["Standby"] == true)
                {
                    e.Row["Present"] = false;
                }
                success = executeSQLUpdate("UPDATE EPGP SET present=@1,standby=@2 WHERE name=@3", new object[] { ((bool)e.Row["Present"] ? 1 : 0), ((bool)e.Row["Standby"] ? 1 : 0), name });
            }
            else if (e.Column.ColumnName.Equals("Posted"))
            {
                success = executeSQLUpdate("UPDATE EPGP SET posted=@1 WHERE name=@2", new object[] { ((bool)e.Row["Posted"] ? 1 : 0), name });
            }
            else if (e.Column.ColumnName.Equals("Raider"))
            {
                success = executeSQLUpdate("UPDATE EPGP SET raider=@1 WHERE name=@2", new object[] { ((bool)e.Row["Raider"] ? 1 : 0), name });
            }
            if (success)
            {
                resortTable(table);
                overlayForm.lbl_overlayName.Text = "";
                overlayForm.lbl_overlayPR.Text = "";
                // Logging
                if ((logging) && (!e.Column.ColumnName.Equals("Present")) && (!e.Column.ColumnName.Equals("Standby")) && (!e.Column.ColumnName.Equals("Posted")) && (!e.Column.ColumnName.Equals("Raider")))
                {
                    changeReasonMessage changeReasonPopup = new changeReasonMessage();
                    var result = changeReasonPopup.ShowDialog(this);
                    string reason = string.Empty;
                    if ((result == DialogResult.OK) && (changeReasonPopup.Reason != ""))
                    {
                        reason = changeReasonPopup.Reason;
                        if (reason.IndexOf("'") > 0)
                        {
                            reason = reason.Remove(reason.IndexOf("'"), 1);
                        }
                    }
                    else
                    {
                        reason = "Default";
                        MessageBox.Show("Default reason used", "Reason");
                    }
                    string currentDate = DateTime.Today.Month.ToString() + "/" + DateTime.Today.Day.ToString() + "/" + DateTime.Today.Year.ToString();
                    if (writeLogConnection.State == ConnectionState.Closed) writeLogConnection.Open();
                    string logSQLstring = "INSERT INTO log (name, number, type, reason, date, officer) VALUES ('" + e.Row["Name"].ToString() + "', '" + ((double)e.ProposedValue - (double)e.Row[e.Column.Ordinal, DataRowVersion.Current]).ToString() + "', '" + e.Column.ColumnName.ToString() + "', '" + reason + "', '" + currentDate + "', '" + login_name + "')";
                    MySqlCommand logCommand = new MySqlCommand(logSQLstring, writeLogConnection);
                    logCommand.ExecuteNonQuery();
                    if (writeLogConnection.State == ConnectionState.Open) writeLogConnection.Close();
                    // Update log table
                    updateLogTable();
                }
            }
            else
            {
                table.RejectChanges();
                MessageBox.Show("Change failed.", "Change");
            }

            table.ColumnChanged += Column_Changed;
        }

        private void EPGPspreadsheet_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataTable dt = (DataTable)((BindingSource)EPGPspreadsheet.DataSource).DataSource;
            if (dt.Columns[e.ColumnIndex].DataType == typeof(double))
            {
                try
                {
                    Double.Parse(e.FormattedValue.ToString());
                }
                catch (FormatException)
                {
                    MessageBox.Show("Only numberic values are allowed.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        private void btn_refreshTbl_Click(object sender, EventArgs e)
        {
            refreshNewerTable();
        }

        private bool updateTable()
        {
            try
            {
                if (readEPGPconnection.State == ConnectionState.Closed) readEPGPconnection.Open();

                MySqlCommand command = readEPGPconnection.CreateCommand();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySql.Data.Types.MySqlDateTime sqlDt = new MySql.Data.Types.MySqlDateTime(tableModTime);
                String sqlTimeStamp = "'" + sqlDt.Year + "-" + sqlDt.Month + "-" + sqlDt.Day + " " + sqlDt.Hour + ":" + sqlDt.Minute + ":" + sqlDt.Second + "'";

                command.CommandText = "SELECT name as Name, ep as EP, gp as GP, ep/gp as PR, lgp as LGP, ep/lgp as LPR, present as Present, standby as Standby, posted as Posted, raider as Raider "+
                                      "FROM EPGP "+
                                      "WHERE lastUpdateTime>"+sqlTimeStamp+" "+
                                      "ORDER BY Present DESC, Standby DESC, PR DESC, Name ASC";
                adapter.SelectCommand = command;
                DataTable table = new DataTable();
                adapter.Fill(table);
                // Set Primary Key
                table.PrimaryKey = new DataColumn[]{table.Columns["Name"]};
                BindingSource bs = null;
                if (this.EPGPspreadsheet.DataSource == null)
                {
                    // First load on table
                    bs = new BindingSource();
                    bs.DataSource = table;
                    this.EPGPspreadsheet.DataSource = bs;
                }
                else
                {
                    // Some part of table was modified
                    bs = (BindingSource)this.EPGPspreadsheet.DataSource;
                    DataTable curTbl = (DataTable)bs.DataSource;
                    curTbl.Merge(table, false);
                    resortTable(curTbl);
                }
                table.ColumnChanged += Column_Changed;

                raiderNameColor();

                return true;
            }
            catch (MySqlException)
            {
                // Didn't work
                return false;
            }
            finally
            {
                if (readEPGPconnection.State == ConnectionState.Open) readEPGPconnection.Close();
            }
        }

        private void resortTable(DataTable dt)
        {
            // Auto sort if not logged in
            if (!loggedIn)
            {
                BindingSource bs = (BindingSource)EPGPspreadsheet.DataSource;
                bs.Sort = "Present DESC, Standby DESC, PR DESC, Name ASC";
            }
        }

        private DateTime getTableLastModTime()
        {
            if (readEPGPconnection.State == ConnectionState.Closed) readEPGPconnection.Open();

            MySqlCommand command = readEPGPconnection.CreateCommand();

            command.CommandText = "SELECT UPDATE_TIME FROM information_schema.tables WHERE TABLE_SCHEMA = 'persona_EPGP' AND TABLE_NAME = 'EPGP'";

            MySqlDataReader dr = command.ExecuteReader();
            dr.Read();
            MySql.Data.Types.MySqlDateTime sqlDt = dr.GetMySqlDateTime("UPDATE_TIME");

            dr.Close();
            readEPGPconnection.Close();

            return sqlDt.GetDateTime();
        }

        private void refreshNewerTable()
        {
            try
            {
                DateTime dt = getTableLastModTime();

                if (dt.CompareTo(tableModTime) > 0 || tableModTime == null)
                {
                    updateTable();
                    // Table has been modified since
                    tableModTime = dt;
                    updateLogTable();
                }
            }
            catch (MySqlException ex)
            {
                // Didn't Work
                ex.GetBaseException();
            }
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            getGuild getGuildWindow = new getGuild();
            getGuildWindow.Show();
        }
    }

    public class refreshTable
    {

        public void refresh(guildManagement gm)
        {
            Random r = new Random();
            while (true)
            {
                Thread.Sleep(r.Next(25000, 35000));

                if (!gm.IsDisposed) {
                    gm.Invoke(gm.refreshDelegate);
                }
            }
        }

    }

    #endregion // Table

}
