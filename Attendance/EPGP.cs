using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Attendance
{
    public partial class guildManagement : Form
    {
        public guildManagement()
        {
            InitializeComponent();
        }

        System.Data.SqlClient.SqlConnection con;
        DataSet ds1;

        private void fiveEPbutton_Click(object sender, EventArgs e)
        {

        }

        private void guildManagement_Load(object sender, EventArgs e)
        {
            con = new System.Data.SqlClient.SqlConnection();
            ds1 = new DataSet();
            con.ConnectionString = " ";  //need to find what to use for the connection string

            con.Open();


            con.Close();


        }
    }
}
