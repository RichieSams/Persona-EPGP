namespace Advanced_Combat_Tracker
{
    using System;

    public class TimelineEvent : IComparable
    {
        private string caption;
        private DateTime time;
        private int typeId;

        public TimelineEvent(DateTime Time, string Caption, int Type)
        {
            this.time = Time;
            this.caption = Caption;
            this.typeId = Type;
        }

        public int CompareTo(object obj)
        {
            TimelineEvent event2 = (TimelineEvent) obj;
            return this.Time.CompareTo(event2.Time);
        }

        public string Caption
        {
            get
            {
                return this.caption;
            }
            set
            {
                this.caption = value;
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

        public int TypeId
        {
            get
            {
                return this.typeId;
            }
            set
            {
                this.typeId = value;
            }
        }
    }
}

