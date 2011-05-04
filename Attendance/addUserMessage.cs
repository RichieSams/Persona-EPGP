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
    public partial class addUserMessage : Form
    {
        public addUserMessage()
        {
            InitializeComponent();

        }

        public string MemberName
        {
            get { return txt_addUser.Text; }
        }

        // Press 'OK' if enter is pressed
        private void txt_addUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                okButton.PerformClick();
                e.SuppressKeyPress = true;
            }
        }


    }
}
