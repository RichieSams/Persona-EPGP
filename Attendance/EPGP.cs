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

namespace Attendance
{
    public partial class guildManagement : Form
    {
        private String general_user_id = "persona_admin";
        private String general_password = "ilike333";
        private Boolean overlayToggle = false;
        private Boolean overlayBorder = true;
        private string currentZone = string.Empty;
        private Boolean loggedIn = false;

        private MySqlConnection connection;

        private double minGP = 5.0;

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        public guildManagement()
        {
            InitializeComponent();
        }

        private void fiveEPbutton_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (connection.State == ConnectionState.Closed) connection.Open();

            //    MySqlCommand command = null;

            //    command = new MySqlCommand("UPDATE EPGP SET ep=ep+5 WHERE present=1 OR standby=1", connection);
            //    command.ExecuteNonQuery();

            //    updateTable();
            //}
            //catch (MySqlException ex)
            //{
            //    // Didn't work
            //}
            //finally
            //{
            //    if (connection.State == ConnectionState.Open) connection.Close();
            //}
            executeSQL("UPDATE EPGP SET ep=ep+5 WHERE present=1 OR standby=1", new object[] {});
            updateTable();
        }

        private void guildManagement_Load(object sender, EventArgs e)
        {
            string MyConString = "server=personaguild.com; User Id=" + general_user_id + "; database=persona_EPGP; Password=" + general_password;
            connection = new MySqlConnection(MyConString);

            // Fill the table for the first time
            updateTable();

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

            // Watch for change in log.txt file
            FileSystemWatcher logWatcher = new FileSystemWatcher();
            logWatcher.Path = "C:\\Program Files (x86)\\RIFT Game";
            logWatcher.Filter = "Log.txt";
            logWatcher.Changed += new FileSystemEventHandler(textLogParser);
            logWatcher.Created += new FileSystemEventHandler(textLogParser);
            logWatcher.EnableRaisingEvents = true;

            // Find zone
            zoneParser();
        }

        private void Cell_Clicked(object sender, DataGridViewCellEventArgs e )
        {
            // Highlight name if the cell clicked isn't a header cell
            if (e.RowIndex != -1)
            {
                EPGPspreadsheet.Rows[e.RowIndex].Cells["Name"].Selected = true;
            }
        }

        private void updateTable()
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

        private bool executeSQL(String s, object[] param)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                MySqlCommand command = new MySqlCommand(s, connection);

                command.Prepare();

                for (int i = 1; i <= param.Length; i++)
                {
                    command.Parameters.AddWithValue("@" + i, param[i-1]);
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

        private void Column_Changed(object sender, DataColumnChangeEventArgs e)
        {
            // Get Name
            String name = (String)e.Row["Name"];
            // Check to see which column is being changed
            if (e.Column.ColumnName.Equals("EP") || e.Column.ColumnName.Equals("GP") || e.Column.ColumnName.Equals("LGP"))
            {
                DataTable table = e.Column.Table;
                table.ColumnChanged -= Column_Changed;
                if (e.Column.ColumnName.Equals("GP"))
                    e.Row["GP"] = Math.Max((double)e.Row["GP"], minGP);
                e.Row["PR"] = (Double)e.Row["EP"] / (Double)e.Row["GP"];
                resortTable(e.Column.Table);
                if (e.Column.ColumnName.Equals("EP"))
                    executeSQL("UPDATE EPGP SET ep=@1 WHERE name=@2", new object[] { (double)e.Row["EP"], name });
                if (e.Column.ColumnName.Equals("GP"))
                    executeSQL("UPDATE EPGP SET gp=@1 WHERE name=@2", new object[] { (double)e.Row["GP"], name });
                if (e.Column.ColumnName.Equals("LGP"))
                    executeSQL("UPDATE EPGP SET lgp=@1 WHERE name=@2", new object[] { (double)e.Row["LGP"], name });
                
            }
            else if (e.Column.ColumnName.Equals("Present"))
            {
                DataTable table = e.Column.Table;
                table.ColumnChanged -= Column_Changed;
                if ((Boolean)e.Row["Present"] == true)
                {
                    if ((Boolean)e.Row["Present"] == true) e.Row["Standby"] = false;
                }
                resortTable(e.Column.Table);
                executeSQL("UPDATE EPGP SET present=@1,standby=@2 WHERE name=@3", new object[] { ((bool)e.Row["Present"] ? 1 : 0), ((bool)e.Row["Standby"] ? 1 : 0), name });
            }
            else if (e.Column.ColumnName.Equals("Standby"))
            {
                DataTable table = e.Column.Table;
                table.ColumnChanged -= Column_Changed;
                if ((Boolean)e.Row["Standby"] == true)
                {
                    if ((Boolean)e.Row["Standby"] == true) e.Row["Present"] = false;
                }
                resortTable(e.Column.Table);
                executeSQL("UPDATE EPGP SET present=@1,standby=@2 WHERE name=@3", new object[] {((bool)e.Row["Present"] ? 1 : 0), ((bool)e.Row["Standby"] ? 1 : 0), name});
            }
        }

        private void resortTable(DataTable dt)
        {
            DataTable table = new DataTable();
            DataRow[] data;
            // Auto sort if not logged in
            if (!loggedIn)
            {
                data = (from r in dt.AsEnumerable() orderby r["Present"] descending, r["Standby"] descending, r["PR"] descending select r).ToArray();
            }
            else
            {
                data = (from r in dt.AsEnumerable() select r).ToArray();
            }
            table = data.CopyToDataTable();
            BindingSource bs = new BindingSource();
            bs.DataSource = table;
            this.EPGPspreadsheet.DataSource = bs;
            table.ColumnChanged += Column_Changed;
        }

        overlay overlayForm = new overlay();

        public void overlayButton_Click(object sender, EventArgs e)
        {
            if (overlayForm.Visible == false)
            {
                if (overlayForm.IsDisposed) overlayForm = new overlay();
                overlayForm.Show();
                overlayToggle = true;
            }
            else
            {
                overlayForm.Hide();
                overlayToggle = false;
            }
            this.Focus();
        }

        private void overlayButton_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (overlayBorder)
                {
                    overlayForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    overlayBorder = false;
                }
                else
                {
                    overlayForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                    overlayBorder = true;
                }
            }

        }

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

        private void loginFunction()
        {
            try
            {
                //try to connect to SQL with login info
                string login_name = txt_name.Text;
                string login_pass = txt_pass.Text;
                string MyConString = "server=personaguild.com; User Id=persona_" + login_name + "; database=persona_EPGP; Password=" + login_pass;
                connection = new MySqlConnection(MyConString);
                connection.Open();
                // Grab lock
                //if successful, show admin buttons and unlock table
                fiveEPbutton.Show();
                tenEPbutton.Show();
                attendanceButton.Show();
                lbl_sort.Show();
                PRsortButton.Show();
                alphaSortButton.Show();
                EPGPspreadsheet.ReadOnly = false;
                EPGPspreadsheet.Columns["Name"].ReadOnly = true;
                EPGPspreadsheet.Columns["PR"].ReadOnly = true;
                EPGPspreadsheet.Columns["LPR"].ReadOnly = true;
                loggedIn = true;
            }
            catch (MySqlException ex)
            {
                //Show popup that login failed
                MessageBox.Show("Login failed");
            }
            finally
            {
                //clear login fields and close connection
                txt_name.Text = "";
                txt_pass.Text = "";
                if (connection.State == ConnectionState.Open) connection.Close();
            }
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
                    if ((logList[1] == " need")||(logList[1] == " greed"))
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

        private void PRsortButton_Click(object sender, EventArgs e)
        {
            BindingSource bs = EPGPspreadsheet.DataSource as BindingSource;
            DataTable table = bs.DataSource as DataTable;
            table.ColumnChanged -= Column_Changed;
            bs.Sort = "Present DESC, Standby DESC, PR DESC";
            this.EPGPspreadsheet.DataSource = bs;
            bs = EPGPspreadsheet.DataSource as BindingSource;
            table.ColumnChanged += Column_Changed;
        }

        private void alphaSortButton_Click(object sender, EventArgs e)
        {
            BindingSource bs = EPGPspreadsheet.DataSource as BindingSource;
            DataTable table = bs.DataSource as DataTable;
            table.ColumnChanged -= Column_Changed;
            bs.Sort = "Name ASC";
            this.EPGPspreadsheet.DataSource = bs;
            bs = EPGPspreadsheet.DataSource as BindingSource;
            table.ColumnChanged += Column_Changed;
        }
    }
}
