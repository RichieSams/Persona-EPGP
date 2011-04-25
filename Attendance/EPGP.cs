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

namespace Attendance
{
    public partial class guildManagement : Form
    {
        private String user_id = "persona_admin";
        private String password = "ilike333";
        private Boolean overlayToggle = false;
        private Boolean overlayBorder = true;

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
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                MySqlCommand command = null;

                command = new MySqlCommand("UPDATE EPGP SET ep=ep+5 WHERE present=1 OR standby=1", connection);
                command.ExecuteNonQuery();

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

        private void guildManagement_Load(object sender, EventArgs e)
        {
            string MyConString = "server=personaguild.com; User Id=" + user_id + "; database=persona_EPGP; Password=" + password;
            connection = new MySqlConnection(MyConString);

            updateTable();

            // Formats columns to fit
            EPGPspreadsheet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Change things about editing a table
            EPGPspreadsheet.ReadOnly = true;
            // Formatting
            EPGPspreadsheet.Columns["EP"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            EPGPspreadsheet.Columns["EP"].DefaultCellStyle.Format = "0.00";
            EPGPspreadsheet.Columns["EP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            EPGPspreadsheet.Columns["GP"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            EPGPspreadsheet.Columns["GP"].DefaultCellStyle.Format = "0.00";
            EPGPspreadsheet.Columns["GP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            EPGPspreadsheet.Columns["PR"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            EPGPspreadsheet.Columns["PR"].DefaultCellStyle.Format = "0.00";
            EPGPspreadsheet.Columns["PR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            EPGPspreadsheet.Columns["Present"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            EPGPspreadsheet.Columns["Standby"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            // Select whole row
            EPGPspreadsheet.CellClick += Cell_Clicked;

        }


        private void Cell_Clicked(object sender, DataGridViewCellEventArgs e )
        {
            EPGPspreadsheet.Rows[e.RowIndex].Cells["Name"].Selected = true;
        }

        private void updateTable()
        {
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                MySqlCommand command = connection.CreateCommand();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                command.CommandText = "SELECT name as Name, ep as EP, gp as GP, ep/gp as PR, present as Present, standby as Standby FROM EPGP ORDER BY Present DESC, Standby DESC, PR DESC";
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

                MySqlCommand command = null;

                //command = new MySqlCommand("UPDATE EPGP SET ep=" + e.Row["EP"] + " WHERE name='" + name + "'", connection);
                
                if (command != null)
                    command.ExecuteNonQuery();

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
            if (e.Column.ColumnName.Equals("EP") || e.Column.ColumnName.Equals("GP"))
            {
                DataTable table = e.Column.Table;
                table.ColumnChanged -= Column_Changed;
                if (e.Column.ColumnName.Equals("GP"))
                    e.Row["GP"] = Math.Max((double)e.Row["GP"], minGP);
                e.Row["PR"] = (Double)e.Row["EP"] / (Double)e.Row["GP"];
                resortTable(e.Column.Table);
                // SQL
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    MySqlCommand command = null;
                    
                    if (e.Column.ColumnName.Equals("EP"))
                        command = new MySqlCommand("UPDATE EPGP SET ep=" + e.Row["EP"] + " WHERE name='" + name + "'", connection);
                    if (e.Column.ColumnName.Equals("GP"))
                        command = new MySqlCommand("UPDATE EPGP SET ep=" + e.Row["GP"] + " WHERE name='" + name + "'", connection);
                    if (command != null) 
                        command.ExecuteNonQuery();
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
            else if (e.Column.ColumnName.Equals("Present"))
            {
                DataTable table = e.Column.Table;
                table.ColumnChanged -= Column_Changed;
                if ((Boolean)e.Row["Present"] == true)
                {
                    if ((Boolean)e.Row["Present"] == true) e.Row["Standby"] = false;
                }
                resortTable(e.Column.Table);
                // SQL
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    MySqlCommand command = new MySqlCommand("UPDATE EPGP SET present=" + ((bool)e.Row["Present"] ? 1 : 0) + "," +
                                                                            "standby=" + ((bool)e.Row["Standby"] ? 1 : 0) +
                                                            " WHERE name='" + name+"'", connection);
                    command.ExecuteNonQuery();
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
            else if (e.Column.ColumnName.Equals("Standby"))
            {
                DataTable table = e.Column.Table;
                table.ColumnChanged -= Column_Changed;
                if ((Boolean)e.Row["Standby"] == true)
                {
                    if ((Boolean)e.Row["Standby"] == true) e.Row["Present"] = false;
                }
                resortTable(e.Column.Table);
                // SQL
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    MySqlCommand command = new MySqlCommand("UPDATE EPGP SET present=" + ((bool)e.Row["Present"] ? 1 : 0) + "," +
                                                                            "standby=" + ((bool)e.Row["Standby"] ? 1 : 0) +
                                                            " WHERE name='" + name + "'", connection);
                    command.ExecuteNonQuery();
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
        }

        private void resortTable(DataTable dt)
        {
            DataTable table = new DataTable();
            DataRow[] data = (from r in dt.AsEnumerable()
                              orderby r["Present"] descending, r["Standby"] descending, r["PR"] descending
                              select r).ToArray();
            table = data.CopyToDataTable();
            BindingSource bs = new BindingSource();
            bs.DataSource = table;
            this.EPGPspreadsheet.DataSource = bs;
            table.ColumnChanged += Column_Changed;
        }

        overlay overlayForm = new overlay();

        private void overlayButton_Click(object sender, EventArgs e)
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
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            loginFunction();
        }

        private void loginFunction()
        {
            fiveEPbutton.Show();
            tenEPbutton.Show();
            attendanceButton.Show();
        }

        private void attendanceButton_Click(object sender, EventArgs e)
        {
            string[] raidArray = new string[20];
            XmlTextReader reader = new XmlTextReader("raid.xml");
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

            // Change SQL
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

    }
}
