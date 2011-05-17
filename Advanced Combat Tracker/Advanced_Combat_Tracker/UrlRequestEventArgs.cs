namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;

    public class UrlRequestEventArgs : EventArgs
    {
        public readonly Dictionary<string, string> headers;
        private byte[] returnBinary;
        private string returnContentType;
        private bool returnIsText;
        private string returnText;
        public readonly string url;
        private bool urlHandled;
        public readonly Dictionary<string, string> urlVars;

        public UrlRequestEventArgs(string Url, Dictionary<string, string> Headers, Dictionary<string, string> UrlVars)
        {
            this.url = Url;
            this.headers = Headers;
            this.urlVars = UrlVars;
        }

        public void SetBinaryData(byte[] Data, string ReturnContentType)
        {
            this.returnBinary = Data;
            this.returnContentType = ReturnContentType;
            this.urlHandled = true;
            this.returnIsText = false;
        }

        public void SetTextData(string Data, string ReturnContentType)
        {
            this.returnText = Data;
            this.returnContentType = ReturnContentType;
            this.urlHandled = true;
            this.returnIsText = true;
        }

        public byte[] ReturnBinary
        {
            get
            {
                return this.returnBinary;
            }
        }

        public string ReturnContentType
        {
            get
            {
                return this.returnContentType;
            }
        }

        public bool ReturnIsText
        {
            get
            {
                return this.returnIsText;
            }
        }

        public string ReturnText
        {
            get
            {
                return this.returnText;
            }
        }

        public bool UrlHandled
        {
            get
            {
                return this.urlHandled;
            }
        }
    }
}

