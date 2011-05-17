namespace Advanced_Combat_Tracker
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class CustomTrigger
    {
        private bool active;
        private string category;
        private DateTime lastAudioAlert;
        private Regex rE;
        private string regex;
        private bool restrictToCategory;
        private TabPage resultsTabPage;
        private string soundData;
        private int soundType;
        private bool tabbed;
        private int tabbedAge;
        private int tabbedCurrentIndex;
        private bool timer;
        private string timerName;

        public CustomTrigger(string cRegex, string cCategory)
        {
            this.soundData = string.Empty;
            this.timerName = string.Empty;
            this.active = true;
            this.tabbedCurrentIndex = -1;
            this.lastAudioAlert = DateTime.MinValue;
            this.category = " General";
            this.regex = cRegex;
            this.rE = new Regex(this.regex, RegexOptions.Compiled);
            this.category = cCategory;
        }

        public CustomTrigger(string cRegex, int cSoundType, string cSoundData, bool cTimer, string cTimerName, bool cTabbed)
        {
            this.soundData = string.Empty;
            this.timerName = string.Empty;
            this.active = true;
            this.tabbedCurrentIndex = -1;
            this.lastAudioAlert = DateTime.MinValue;
            this.category = " General";
            this.regex = cRegex;
            this.soundData = cSoundData;
            this.timerName = cTimerName;
            this.soundType = cSoundType;
            this.timer = cTimer;
            this.active = true;
            this.tabbed = cTabbed;
            this.rE = new Regex(this.regex, RegexOptions.Compiled);
        }

        public bool Active
        {
            get
            {
                return this.active;
            }
            set
            {
                this.active = value;
            }
        }

        public string Category
        {
            get
            {
                if (string.IsNullOrEmpty(this.category))
                {
                    return " General";
                }
                return this.category;
            }
            set
            {
                this.category = value;
            }
        }

        public string Key
        {
            get
            {
                return (this.category + "|" + this.regex);
            }
        }

        public DateTime LastAudioAlert
        {
            get
            {
                return this.lastAudioAlert;
            }
            set
            {
                this.lastAudioAlert = value;
            }
        }

        public Regex RegEx
        {
            get
            {
                return this.rE;
            }
        }

        public bool RestrictToCategoryZone
        {
            get
            {
                return this.restrictToCategory;
            }
            set
            {
                this.restrictToCategory = value;
            }
        }

        public TabPage ResultsTab
        {
            get
            {
                return this.resultsTabPage;
            }
            set
            {
                this.resultsTabPage = value;
            }
        }

        public string ShortRegexString
        {
            get
            {
                return this.regex;
            }
            set
            {
                this.regex = value;
            }
        }

        public string SoundData
        {
            get
            {
                return this.soundData;
            }
            set
            {
                this.soundData = value;
            }
        }

        public int SoundType
        {
            get
            {
                return this.soundType;
            }
            set
            {
                this.soundType = value;
            }
        }

        public string SoundTypeString
        {
            get
            {
                switch (this.SoundType)
                {
                    case 1:
                        return "Beep";

                    case 2:
                        return "Sound";

                    case 3:
                        return "TTS";
                }
                return "None";
            }
        }

        public bool Tabbed
        {
            get
            {
                return this.tabbed;
            }
            set
            {
                this.tabbed = value;
            }
        }

        public int TabbedAge
        {
            get
            {
                if (this.tabbedAge < 7)
                {
                    return this.tabbedAge;
                }
                return 6;
            }
            set
            {
                this.tabbedAge = value;
            }
        }

        public int TabbedCurrentIndex
        {
            get
            {
                return this.tabbedCurrentIndex;
            }
            set
            {
                this.tabbedCurrentIndex = value;
            }
        }

        public bool Timer
        {
            get
            {
                return this.timer;
            }
            set
            {
                this.timer = value;
            }
        }

        public string TimerName
        {
            get
            {
                return this.timerName;
            }
            set
            {
                this.timerName = value;
            }
        }

        public string TimerString
        {
            get
            {
                if (!this.Timer && !this.Tabbed)
                {
                    return "<None>";
                }
                return this.TimerName;
            }
        }

        public ListView TriggerListView
        {
            get
            {
                if ((this.ResultsTab != null) && (this.ResultsTab.Controls.Count != 0))
                {
                    return (ListView) this.ResultsTab.Controls[0];
                }
                return null;
            }
        }

        public TextBox TriggerSearchBox
        {
            get
            {
                GroupBox box = (GroupBox) this.ResultsTab.Controls[2];
                return (TextBox) box.Controls[0];
            }
        }
    }
}

