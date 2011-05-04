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

namespace Attendance
{
    public partial class guildManagement : Form
    {

        #region Variables

        // Administrator
        private const String general_user_id = "persona_admin";
        private const String general_password = "ilike333";
        private Boolean loggedIn;
        private string officerName;

        // MySql Connections
        private MySqlConnection connection;
        private MySqlConnection lockConnection;
        
        // Table
        public delegate void refreshNew();
        public refreshNew refreshDelegate;
        private Thread refreshThread;
        private DateTime tableModTime;

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
        }

        private void guildManagement_Close(object sender, FormClosedEventArgs e)
        {
            if (lockConnection != null && lockConnection.State == ConnectionState.Open) lockConnection.Close();
            //settingsOverlayX = overlayForm.Location.X;
            //settingsOverlayY = overlayForm.Location.Y;
            saveSettings();
            refreshThread.Abort();
            Application.Exit();
        }

        private void guildManagement_Load(object sender, EventArgs e)
        {
            // Set up general connection to pull table
            string MyConString = "server=personaguild.com; User Id=" + general_user_id + "; database=persona_EPGP; Password=" + general_password;
            connection = new MySqlConnection(MyConString);

            // Fill the table for the first time and set mod time
            refreshNewerTable();

            // Formats columns to fit
            EPGPspreadsheet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Lock the table to editing
            EPGPspreadsheet.ReadOnly = true;
            // Formatting
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

            // Watch for change in log.txt file
            FileSystemWatcher logWatcher = new FileSystemWatcher();
            logWatcher.Path = "C:\\Program Files (x86)\\RIFT Game";
            logWatcher.Filter = "Log.txt";
            logWatcher.Changed += new FileSystemEventHandler(textLogParser);
            logWatcher.Created += new FileSystemEventHandler(textLogParser);
            logWatcher.EnableRaisingEvents = true;

            // Load settings
            loadSettings();

            // Find zone
            zoneParser();
        }

        #endregion // Intialize

        #region Info

        private void lbl_webLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo(e.Link.LinkData.ToString());
            Process.Start(info);
        }

        private void alphaSortButton_Click(object sender, EventArgs e)
        {
            BindingSource bs = (BindingSource)EPGPspreadsheet.DataSource;
            //DataTable table = (DataTable)bs.DataSource;
            //table.ColumnChanged -= Column_Changed; // Don't need these you can delete when you see
            bs.Sort = "Name ASC";
            //table.ColumnChanged += Column_Changed; // Don't need these you can delete when you see
        }

        private void PRsortButton_Click(object sender, EventArgs e)
        {
            BindingSource bs = (BindingSource)EPGPspreadsheet.DataSource;
            //DataTable table = (DataTable)bs.DataSource;
            //table.ColumnChanged -= Column_Changed; // Don't need these you can delete when you see
            bs.Sort = "Present DESC, Standby DESC, PR DESC";
            //table.ColumnChanged += Column_Changed; // Don't need these you can delete when you see
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
            try
            {
                string login_name = txt_name.Text;
                string login_pass = txt_pass.Text;
                string lockConString = "server=personaguild.com; User Id=persona_" + login_name + "; database=persona_lock; Password=" + login_pass;
                lockConnection = new MySqlConnection(lockConString);
                // Try to connect to SQL using login info
                lockConnection.Open();
                // Try to grab lock
                MySqlCommand lockCommand = new MySqlCommand("SHOW OPEN TABLES FROM persona_lock LIKE '%locktable%'", lockConnection);
                MySqlDataReader dr;
                dr = lockCommand.ExecuteReader();
                // If query returns null, lock sql
                dr.Read();
                if (Convert.ToInt32(dr["In_use"]) == 0)
                {
                    dr.Close();
                    lockCommand.CommandText = "LOCK TABLES locktable WRITE";
                    lockCommand.ExecuteNonQuery();
                }
                else
                {
                    //Show popup that login failed
                    MessageBox.Show("An officer is already logged in");
                    return loggedIn;
                }

                // If successful, save name of the officer, show admin buttons, unlock table, and format logged in text
                officerName = login_name;
                fiveEPbutton.Show();
                tenEPbutton.Show();
                attendanceButton.Show();
                lbl_sort2.Show();
                PRsortButton2.Show();
                alphaSortButton2.Show();
                EPGPspreadsheet.ReadOnly = false;
                EPGPspreadsheet.Columns["Name"].ReadOnly = true;
                EPGPspreadsheet.Columns["PR"].ReadOnly = true;
                EPGPspreadsheet.Columns["LPR"].ReadOnly = true;
                EPGPspreadsheet.CellValidating += EPGPspreadsheet_CellValidating;
                lbl_loggedIn.Text = "Logged In";
                lbl_loggedIn.ForeColor = Color.Green;
                loggedIn = true;
                // Stop Refreshing since we are the only ones editing
                refreshThread.Abort();
            }
            catch (MySqlException ex)
            {
                //Show popup that login failed
                MessageBox.Show("Invalid login information");
            }
            finally
            {
                // Clear login text boxes
                txt_name.Text = "";
                txt_pass.Text = "";
            }
            return loggedIn;
        }

        private void attendanceButton_Click(object sender, EventArgs e)
        {
            string[] raidArray = new string[20];
            XmlTextReader reader = new XmlTextReader("C:\\Program Files (x86)\\RIFT Game\\raid.xml");
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
                if (connection.State == ConnectionState.Closed) connection.Open();

                MySqlCommand command = connection.CreateCommand();
                MySqlTransaction trans = connection.BeginTransaction();

                command.Connection = connection;
                command.Transaction = trans;

                foreach (String s in raidArray)
                {
                    command.CommandText = "INSERT INTO EPGP (`name`, `present`) VALUES ('" + s + "', 1) ON DUPLICATE KEY UPDATE present=1,standby=0";
                    command.ExecuteNonQuery();
                }
                trans.Commit();

                updateTable();
            }
            catch (MySqlException ex)
            {
                // Didn't work
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }
        }

        private void fiveEPbutton_Click(object sender, EventArgs e)
        {
            if (executeSQLUpdate("UPDATE EPGP SET ep=ep+5 WHERE present=1 OR standby=1", new object[] { }))
            {
                updateTable();
            }
        }

        private void tenEPbutton_Click(object sender, EventArgs e)
        {
            executeSQLUpdate("UPDATE EPGP SET ep=ep+10 WHERE present=1 OR standby=1", new object[] { });
            updateTable();
        }

        #endregion // Admin

        #region Settings

        public void overlayButton_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (overlayForm.Visible == false)
                {
                    if (overlayForm.IsDisposed)
                        overlayForm = new overlay(settingsOverlayX, settingsOverlayY, settingsOverlayOpacity);
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

        private bool saveSettings()
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
                            xml.WriteString(overlayForm.FormBorderStyle.ToString());
                        xml.WriteEndElement();
                        xml.WriteStartElement("Toggle");
                            xml.WriteString(overlayForm.Visible.ToString());
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

        private bool loadSettings()
        {
            if (File.Exists(settingsPath))
            {
                XmlTextReader xml = new XmlTextReader(settingsPath);
                

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
                        
                    }
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
            overlayForm = new overlay(settingsOverlayX, settingsOverlayY, settingsOverlayOpacity);

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
                MessageBox.Show("Invalid entry. Enter an integer between 0 - 100");
                txt_opacity.Text = "50";
                opacitySlider.Value = 50;
                settingsOverlayOpacity = 0.5;
                overlayForm.Opacity = settingsOverlayOpacity;
            }
            opacitySlider.ValueChanged += opacitySlider_ValueChanged;
        }
        
        // Update the text box value on slide, but only save when mouse is released
        private void opacitySlider_ValueChanged(object sender, EventArgs e)
        {
            txt_opacity.TextChanged -= txt_opacity_TextChanged;
            txt_opacity.Text = opacitySlider.Value.ToString();
            settingsOverlayOpacity = Convert.ToDouble(opacitySlider.Value) / 100;
            overlayForm.Opacity = settingsOverlayOpacity;
            txt_opacity.TextChanged += txt_opacity_TextChanged;
        }

        #endregion // Settings

        #region Log Parser

        private void textLogParser(object source, FileSystemEventArgs e)
        {
            // Only do overlay text if the user is in a raid zone
            if ((currentZone == "Greenscale's Blight") || (currentZone == "River of Souls") || (currentZone == "Freemarch"))
            {
                FileStream fs = new FileStream("C:\\Program Files (x86)\\RIFT Game\\log.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(fs);
                string linesBlock;
                // Try to get the last 1024 bytes of data from the file
                try
                {
                    reader.BaseStream.Seek(-1024, SeekOrigin.End);
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
                }

                // Non-raid speech lines are ignored           /////Idea: create channel for EPGP so the program doesn't have to read raid chatter
                if (lastLine.IndexOf("[Guild]") != -1) //change to [Raid] for release
                {
                    // Trim off the time stamp
                    string overlayString = lastLine.Substring(10, lastLine.Length - 10);
                    // Split into name and text
                    string[] logList = overlayString.Split(':');
                    // Check for phrase
                    if ((logList[1] == " need") || (logList[1] == " greed"))
                    {
                        int tempInt = 0;
                        string tempString = string.Empty;
                        // Trim off [raid] and the brackets around the name
                        logList[0] = logList[0].Substring(8, logList[0].Length - 9); //change to 7 and 8 when actually using [Raid] and not [Guild]
                        // Cycle through the names on the spreadsheet
                        while (tempInt < EPGPspreadsheet.RowCount)
                        {
                            tempString = EPGPspreadsheet.Rows[tempInt].Cells["Name"].Value.ToString();
                            if (tempString == logList[0])
                            {
                                // If the label is empty, enter in the text
                                if (overlayForm.lbl_overlayName.Text == null)
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
                                    if (logList[1] == " greed")
                                    {
                                        overlayForm.lbl_overlayPR.Text = "Off spec";
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
                                    if (logList[1] == " greed")
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

        private void zoneParser()
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
            overlayForm.lbl_test.Text = currentZone;
        }

        #endregion // Log Parser

        #region SQL

        private bool executeSQLUpdate(String s, object[] param)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                MySqlCommand command = new MySqlCommand(s, connection);

                command.Prepare();
                for (int i = 1; i <= param.Length; i++)
                {
                    command.Parameters.AddWithValue("@" + i, param[i - 1]);
                }

                if (command != null) command.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {
                // Didn't work
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }
        }

        #endregion // SQL

        #region Table

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
                    if ((Boolean)e.Row["Present"] == true) e.Row["Standby"] = false;
                }
                success = executeSQLUpdate("UPDATE EPGP SET present=@1,standby=@2 WHERE name=@3", new object[] { ((bool)e.Row["Present"] ? 1 : 0), ((bool)e.Row["Standby"] ? 1 : 0), name });
            }
            else if (e.Column.ColumnName.Equals("Standby"))
            {
                if ((Boolean)e.Row["Standby"] == true)
                {
                    if ((Boolean)e.Row["Standby"] == true) e.Row["Present"] = false;
                }
                success = executeSQLUpdate("UPDATE EPGP SET present=@1,standby=@2 WHERE name=@3", new object[] { ((bool)e.Row["Present"] ? 1 : 0), ((bool)e.Row["Standby"] ? 1 : 0), name });
            }
            if (success)
            {
                resortTable(table);
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
                catch (FormatException ex)
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
                if (connection.State == ConnectionState.Closed) connection.Open();

                MySqlCommand command = connection.CreateCommand();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                command.CommandText = "SELECT name as Name, ep as EP, gp as GP, ep/gp as PR, lgp as LGP, ep/lgp as LPR, present as Present, standby as Standby FROM EPGP ORDER BY Present DESC, Standby DESC, PR DESC";
                adapter.SelectCommand = command;
                DataTable table = new DataTable();
                adapter.Fill(table);
                BindingSource bs = new BindingSource();
                bs.DataSource = table;
                this.EPGPspreadsheet.DataSource = bs;
                table.ColumnChanged += Column_Changed;

                return true;
            }
            catch (MySqlException ex)
            {
                // Didn't work
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }
        }

        private void resortTable(DataTable dt)
        {
            // Auto sort if not logged in
            if (!loggedIn)
            {
                BindingSource bs = (BindingSource)EPGPspreadsheet.DataSource;
                bs.Sort = "Present DESC, Standby DESC, PR DESC";
            }
        }

        private DateTime getTableLastModTime()
        {
            if (connection.State == ConnectionState.Closed) connection.Open();

            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT UPDATE_TIME FROM information_schema.tables WHERE TABLE_SCHEMA = 'persona_EPGP' AND TABLE_NAME = 'EPGP'";

            MySqlDataReader dr = command.ExecuteReader();
            dr.Read();
            MySql.Data.Types.MySqlDateTime sqlDt = dr.GetMySqlDateTime("UPDATE_TIME");

            dr.Close();
            connection.Close();

            return sqlDt.GetDateTime();
        }

        private void refreshNewerTable()
        {
            try
            {
                DateTime dt = getTableLastModTime();

                if (dt.CompareTo(tableModTime) > 0 || tableModTime == null)
                {
                    // Table has been modified since
                    tableModTime = dt;
                    updateTable();
                }
            }
            catch (MySqlException ex)
            {
                // Didn't Work
                ex.GetBaseException();
            }
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
