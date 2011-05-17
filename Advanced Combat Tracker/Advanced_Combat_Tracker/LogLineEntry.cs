namespace Advanced_Combat_Tracker
{
    using System;

    public class LogLineEntry
    {
        private int gts;
        private string logLine;
        private int parsedType;
        private bool searchSelected;
        private DateTime time;

        public LogLineEntry(DateTime Time, string LogLine, int ParsedType, int GlobalTimeSorter)
        {
            this.logLine = LogLine;
            this.parsedType = ParsedType;
            this.searchSelected = false;
            this.time = Time;
            this.gts = GlobalTimeSorter;
        }

        public int GlobalTimeSorter
        {
            get
            {
                return this.gts;
            }
        }

        public string LogLine
        {
            get
            {
                return this.logLine;
            }
        }

        public bool SearchSelected
        {
            get
            {
                return this.searchSelected;
            }
            set
            {
                this.searchSelected = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return this.time;
            }
            set
            {
                this.time = value;
            }
        }

        public int Type
        {
            get
            {
                return this.parsedType;
            }
        }
    }
}

