using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Attendance
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new guildManagement());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message, "Exception");
            }
        }
    }
}
