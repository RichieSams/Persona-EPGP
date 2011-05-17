namespace Advanced_Combat_Tracker
{
    using System;

    public class WebIndexLink
    {
        private string linkDescription;
        private string linkLabel;
        private string url;

        public WebIndexLink(string Url, string LinkLabel, string LinkDescription)
        {
            this.url = Url;
            this.linkLabel = LinkLabel;
            this.linkDescription = LinkDescription;
        }

        public string LinkDescription
        {
            get
            {
                return this.linkDescription;
            }
            set
            {
                this.linkDescription = value;
            }
        }

        public string LinkLabel
        {
            get
            {
                return this.linkLabel;
            }
            set
            {
                this.linkLabel = value;
            }
        }

        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }
    }
}

