namespace Advanced_Combat_Tracker
{
    using System;
    using System.Diagnostics;

    public class ClipboardEventArgs : EventArgs
    {
        private string content;
        private bool copyLocal;

        public ClipboardEventArgs(string ClipContent, bool CopyLocal)
        {
            this.content = ClipContent;
            this.copyLocal = CopyLocal;
        }

        public string CallerName
        {
            get
            {
                StackTrace trace = new StackTrace();
                return trace.GetFrame(3).GetMethod().Name;
            }
        }

        public string ClipContent
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }

        public bool CopyLocal
        {
            get
            {
                return this.copyLocal;
            }
            set
            {
                this.copyLocal = value;
            }
        }
    }
}

