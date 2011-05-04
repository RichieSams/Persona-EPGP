using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace Attendance
{
    public partial class overlay : Form
    {

        private guildManagement gm;

        public overlay(guildManagement gm, int x, int y, double opacity)
        {
            InitializeComponent();

            this.gm = gm;
            this.Location = new Point(x, y);
            this.Opacity = opacity;
            this.LocationChanged += overlay_LocationChanged;
        }

        private void overlay_LocationChanged(object sender, EventArgs e)
        {
            overlay overlay = (overlay)sender;

            // Update settings
            gm.settingsOverlayX = overlay.Location.X;
            gm.settingsOverlayY = overlay.Location.Y;
            gm.saveSettings();
        }
    }
}
