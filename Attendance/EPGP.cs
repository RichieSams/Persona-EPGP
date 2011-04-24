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

        // DECAY
        // "UPDATE EPGP SET ep=ep*0.93, gp=GREATEST(5.0, gp*0.93)"
        // private double minGP = 5.0;
        // "UPDATE EPGP SET ep=ep*0.93, gp=GREATEST("+minGP+", gp*0.93)"

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

        }

        private void guildManagement_Load(object sender, EventArgs e)
        {
            string MyConString = "server=personaguild.com; User Id="+user_id+"; database=persona_EPGP; Password="+password;
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            connection.Open();

            command.CommandText = "select name as Name, ep as EP, gp as GP, ep/gp as PR, present as Present, standby as Standby from EPGP Order by Present DESC, Standby DESC, PR DESC";
            adapter.SelectCommand = command;
            DataTable table = new DataTable();
            adapter.Fill(table);
            BindingSource bs = new BindingSource();
            bs.DataSource = table;
            this.EPGPspreadsheet.DataSource = bs;

            connection.Close();

            // Formats columns to fit
            EPGPspreadsheet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Change things about editing a table
            EPGPspreadsheet.Columns["Name"].ReadOnly = true;
            EPGPspreadsheet.Columns["PR"].ReadOnly = true;
            table.ColumnChanged += Column_Changed;
        }

        private static void Column_Changed(object sender, DataColumnChangeEventArgs e)
        {
            #region UI Change
            // Get Name
            String name = (String)e.Row["Name"];
            // Check to see which column is being changed
            if (e.Column.ColumnName.Equals("EP") || e.Column.ColumnName.Equals("GP"))
            {
                e.Row["PR"] = (Double)e.Row["EP"] / (Double)e.Row["GP"];
            }
            if (e.Column.ColumnName.Equals("Present"))
            {
                if ((Boolean)e.Row["Present"] == true)
                {
                    if ((Boolean)e.Row["Present"] == true) e.Row["Standby"] = false;
                }
            }
            if (e.Column.ColumnName.Equals("Standby"))
            {
                if ((Boolean)e.Row["Standby"] == true)
                {
                    if ((Boolean)e.Row["Standby"] == true) e.Row["Present"] = false;
                }
            }
            #endregion
            #region SQL Change

            #endregion
        }

        overlay overlayForm = new overlay();

        private void overlayButton_Click(object sender, EventArgs e)
        {
            
            if (!overlayToggle)
            {
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

        private void loginName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginFunction();
            }
        }




        private void loginFunction()
        {

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
                        this.lbl_test.Text = reader.Value;
                        raidArray[tempInt] = reader.Value;
                        tempInt++;
                    }
                }
            }
            //for (int i = 0; i < tempInt; i++)
            //{
                //this.lbl_test.Text = "Hi";
            //}
        }
    }
}
