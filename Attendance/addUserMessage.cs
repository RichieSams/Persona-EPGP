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

    }
}
