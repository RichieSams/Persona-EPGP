namespace Advanced_Combat_Tracker
{
    using System;

    public class HistoryRecord : IComparable<HistoryRecord>, IEquatable<HistoryRecord>
    {
        private string charName;
        private DateTime endTime;
        private string label;
        private DateTime startTime;
        private int type;

        public HistoryRecord(int Type, DateTime StartTime, DateTime EndTime, string Label, string CharName)
        {
            this.type = Type;
            this.startTime = StartTime;
            this.endTime = EndTime;
            this.label = Label;
            this.charName = CharName;
        }

        public int CompareTo(HistoryRecord other)
        {
            return this.StartTime.CompareTo(other.StartTime);
        }

        public bool Equals(HistoryRecord other)
        {
            return this.StartTime.Equals(other.StartTime);
        }

        public override bool Equals(object obj)
        {
            if (obj == DBNull.Value)
            {
                return false;
            }
            if (obj == null)
            {
                return false;
            }
            HistoryRecord record = (HistoryRecord) obj;
            return this.StartTime.Equals(record.StartTime);
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            string str;
            string str2;
            TimeSpan span = (TimeSpan) (this.EndTime - this.StartTime);
            if (span.TotalHours > 1.0)
            {
                str2 = string.Format("{0:00}:{1:00}:{2:00}", (int) span.TotalHours, span.Minutes, span.Seconds);
            }
            else
            {
                str2 = string.Format("{0:00}:{1:00}", span.Minutes, span.Seconds);
            }
            if (this.Type == 1)
            {
                str = "     ";
            }
            else
            {
                str = string.Empty;
            }
            return string.Format("{0}{1} - {2} {3} [{4}]", new object[] { str, this.Label, this.StartTime.ToShortDateString(), this.StartTime.ToLongTimeString(), str2 });
        }

        public string CharName
        {
            get
            {
                return this.charName;
            }
            set
            {
                this.charName = value;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return (TimeSpan) (this.EndTime - this.StartTime);
            }
        }

        public DateTime EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                this.endTime = value;
            }
        }

        public string Label
        {
            get
            {
                return this.label;
            }
            set
            {
                this.label = value;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }
            set
            {
                this.startTime = value;
            }
        }

        public int Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
    }
}

