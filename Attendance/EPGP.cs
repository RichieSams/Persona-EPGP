using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Attendance
{
    public partial class guildManagement : Form
    {
        private String user_id = "persona_admin";
        private String password = "ilike333";
        private Boolean overlayToggle = false;

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

            command.CommandText = "select name as Name, ep as EP, gp as GP, ep/gp as PR from EPGP Order by PR desc";
            adapter.SelectCommand = command;
            DataTable table = new DataTable();
            adapter.Fill(table);
            BindingSource bs = new BindingSource();
            bs.DataSource = table;
            table.Columns.Add("Present?", typeof(Boolean));
            this.EPGPspreadsheet.DataSource = bs;

            connection.Close();

        }

        overlay overlayForm = new overlay();

        private void overlayButton_Click(object sender, EventArgs e)
        {
            
            if (!overlayToggle)
            {
                if (overlayForm == null)
                {
                    //overlay overlayForm = new overlay();
                }
                overlayForm.Show();
                overlayToggle = true;
            }
            else
            {
                overlayForm.Hide();
                overlayToggle = false;
            }
        }

    }
}
