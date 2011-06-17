using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace EPGP
{
    public partial class createUser : Form
    {
        String name = string.Empty;
        String pass = string.Empty;
        String email = string.Empty;

        public createUser()
        {
            InitializeComponent();
        }

        public void GetUserInfo(out String login_name, out String login_pass)
        {
            login_name = name;
            login_pass = pass;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            name = txt_name.Text;
            pass = txt_pass.Text;
            email = txt_email.Text;

            // Data validation
            if ((name != "") && (pass != "") && (email != ""))
            {
                if (name.Length < 26)
                {
                    if (email.IndexOf('@') != -1)
                    {
                        if (email.Length < 46)
                        {
                            // Hashing of password for security
                            MD5 md5 = new MD5CryptoServiceProvider();
                            byte[] interHash = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(pass + name));
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < interHash.Length; i++)
                            {
                                sb.Append(interHash[i].ToString("x2"));
                            }
                            String interStr = sb.ToString();
                            byte[] finalHash = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(pass + interStr));
                            sb.Clear();
                            for (int i = 0; i < finalHash.Length; i++)
                            {
                                sb.Append(finalHash[i].ToString("x2"));
                            }
                            String passMD5 = sb.ToString();

                            // Create MySQL connection
                            String userConString = "server=personaguild.com; User Id=persona_read; database=persona_pgm; Password=kr7OI=&J&,!F2BrHsC";
                            MySqlConnection userConnection = new MySqlConnection(userConString);

                            // Create user
                            userConnection.Open();
                            MySqlCommand createGuildEntryCommand = new MySqlCommand("INSERT INTO users (userName, pass, email) VALUES ('" + name + "', '" + passMD5 + "', '" + email + "')", userConnection);
                            createGuildEntryCommand.ExecuteNonQuery();
                            userConnection.Close();

                            // Return to calling app
                            hiddenOKbutton.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("Email must be less than 46 characters", "Email too long");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid email format", "Invalid email format");
                    }
                }
                else
                {
                    MessageBox.Show("Username must be less than 26 characters", "Username too long");
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled out", "Empty fields");
            }
        }
    }
}
