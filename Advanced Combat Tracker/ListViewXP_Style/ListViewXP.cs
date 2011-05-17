namespace ListViewXP_Style
{
    using Advanced_Combat_Tracker;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(ListView))]
    internal class ListViewXP : ListView
    {
        private bool antiFlicker;
        private LVS_EX styles;
        private int updateCount;
        private bool updating;

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern int SendMessage(IntPtr handle, int messg, int wparam, int lparam);
        public void SetExStyles()
        {
            this.antiFlicker = true;
            this.styles = (LVS_EX) SendMessage(base.Handle, 0x1037, 0, 0);
            this.styles |= LVS_EX.LVS_EX_DOUBLEBUFFER | LVS_EX.LVS_EX_BORDERSELECT;
            SendMessage(base.Handle, 0x1036, 0, (int) this.styles);
        }

        protected override void WndProc(ref Message messg)
        {
            try
            {
                if ((this.updating && this.antiFlicker) && ((messg.Msg == 20) || (messg.Msg == 15)))
                {
                    messg.Msg = 0;
                }
                base.WndProc(ref messg);
            }
            catch (Exception exception)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(exception, messg.ToString());
            }
        }

        public void XPBeginUpdate()
        {
            base.BeginUpdate();
            this.updating = true;
            this.updateCount++;
        }

        public void XPEndUpdate()
        {
            this.updating = false;
            base.EndUpdate();
            this.updateCount--;
        }

        public void XPFlushUpdate()
        {
            while (this.updateCount > 0)
            {
                this.XPEndUpdate();
            }
        }
    }
}

