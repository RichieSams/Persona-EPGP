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
    public partial class changeReasonMessage : Form
    {
        public changeReasonMessage()
        {
            InitializeComponent();
        }

        public string Reason
        {
            get { return txt_changeReason.Text; }
        }

        private void txt_changeReason_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                reasonOkButton.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
    }
}
