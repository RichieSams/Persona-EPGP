namespace Advanced_Combat_Tracker
{
    using System;

    internal class XmlShareEntry
    {
        private bool auto;
        private bool ignore;
        private DateTime lastMod;
        private DateTime lastUpdate;
        private string lastXml = string.Empty;
        private bool notify;
        private string url;

        public XmlShareEntry(string Url, bool RbIgnore, bool RbNotify, bool RbAuto)
        {
            this.url = Url;
            this.ignore = RbIgnore;
            this.notify = RbNotify;
            this.auto = RbAuto;
            this.lastMod = DateTime.MinValue;
            this.lastUpdate = DateTime.MinValue;
        }

        public override bool Equals(object obj)
        {
            if (obj == DBNull.Value)
            {
                return false;
            }
            XmlShareEntry entry = (XmlShareEntry) obj;
            return this.url.Equals(entry.url);
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return this.url;
        }

        public DateTime LastModified
        {
            get
            {
                return this.lastMod;
            }
            set
            {
                this.lastMod = value;
            }
        }

        public DateTime LastUpdated
        {
            get
            {
                return this.lastUpdate;
            }
            set
            {
                this.lastUpdate = value;
            }
        }

        public string LastXml
        {
            get
            {
                return this.lastXml;
            }
            set
            {
                this.lastXml = value;
            }
        }

        public bool New
        {
            get
            {
                return (this.lastMod > this.lastUpdate);
            }
        }

        public bool RbAuto
        {
            get
            {
                return this.auto;
            }
        }

        public bool RbIgnore
        {
            get
            {
                return this.ignore;
            }
        }

        public bool RbNotify
        {
            get
            {
                return this.notify;
            }
        }

        public string Url
        {
            get
            {
                return this.url;
            }
        }
    }
}

