namespace Advanced_Combat_Tracker
{
    using System;
    using System.Net;

    internal class PluginDownloadInfo
    {
        private DateTime addedDate = DateTime.MinValue;
        private string desc = "---";
        private int id;
        private DateTime modifiedDate = DateTime.MinValue;
        private string title = "---";

        public PluginDownloadInfo(int Id)
        {
            this.id = Id;
        }

        public override string ToString()
        {
            return string.Concat(new object[] { "(", this.id, ") ", this.Title });
        }

        public DateTime AddedDate
        {
            get
            {
                if (this.addedDate == DateTime.MinValue)
                {
                    try
                    {
                        WebClient client = new WebClient();
                        DateTime time = DateTime.Parse(client.DownloadString("http://advancedcombattracker.com/versioncheck.php?pluginadded=" + this.id));
                        this.addedDate = new DateTime(time.AddHours(6.0).Ticks, DateTimeKind.Utc);
                    }
                    catch
                    {
                    }
                }
                return this.addedDate;
            }
        }

        public string Description
        {
            get
            {
                if (this.desc == "---")
                {
                    try
                    {
                        this.desc = new WebClient().DownloadString("http://advancedcombattracker.com/plugininfo.php?desc=" + this.id);
                    }
                    catch
                    {
                    }
                }
                return this.desc;
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                if (this.modifiedDate == DateTime.MinValue)
                {
                    try
                    {
                        this.modifiedDate = ActGlobals.oFormActMain.PluginGetRemoteDateUtc(this.id);
                    }
                    catch
                    {
                    }
                }
                return this.modifiedDate;
            }
        }

        public string Title
        {
            get
            {
                if (this.title == "---")
                {
                    try
                    {
                        this.title = new WebClient().DownloadString("http://advancedcombattracker.com/plugininfo.php?title=" + this.id);
                    }
                    catch
                    {
                    }
                }
                return this.title;
            }
        }
    }
}

