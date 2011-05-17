namespace Advanced_Combat_Tracker
{
    using System;

    public class VcrEvent : IEquatable<VcrEvent>, IComparable<VcrEvent>
    {
        private int amount;
        private string target;
        private DateTime time;
        private int type;

        public VcrEvent(DateTime Time, int Type, int Amount, string Target)
        {
            this.time = Time;
            this.type = Type;
            this.amount = Amount;
            this.target = Target;
        }

        public int CompareTo(VcrEvent other)
        {
            int num = this.Time.CompareTo(other.Time);
            if (num != 0)
            {
                return num;
            }
            return this.Type.CompareTo(other.Type);
        }

        public bool Equals(VcrEvent other)
        {
            return (((this.Type == other.Type) && (this.Time == other.Time)) && (this.Target == other.target));
        }

        public int Amount
        {
            get
            {
                return this.amount;
            }
            set
            {
                this.amount = value;
            }
        }

        public string Target
        {
            get
            {
                return this.target;
            }
            set
            {
                this.target = value;
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
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
    }
}

