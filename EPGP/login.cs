using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EPGP
{
    public partial class login : Form
    {
        private guildManagement gm;
        private String login_name = string.Empty;
        private String login_pass = string.Empty;

        public login(guildManagement gm)
        {
            InitializeComponent();

            this.gm = gm;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            login_name = txt_name.Text;
            login_pass = txt_pass.Text;
            if (gm.loginFunction(login_name, login_pass))
            {
                hiddenOKbutton.PerformClick();
            }
            else
            {
                login_name = login_pass = txt_name.Text = txt_pass.Text = string.Empty;
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            // Hide the login popup so as to not confuse users
            this.Hide();
            // Create add user popup
            createUser createUserPopup = new createUser();
            var result = createUserPopup.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                // Get user info then login
                createUserPopup.GetUserInfo(out login_name, out login_pass);
                gm.loginFunction(login_name, login_pass);
                hiddenOKbutton.PerformClick();
            }
        }

        private void txt_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginButton.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
    }
}
