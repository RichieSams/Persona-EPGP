namespace Advanced_Combat_Tracker
{
    using System;

    public class TimerMod : IEquatable<TimerMod>
    {
        private string attacker;
        private DateTime lastUse;
        private string modName;
        private float modValue;
        private TimeSpan useDuration;
        private string victim;

        public TimerMod(string Attacker, string Victim, string ModName, float ModValue, DateTime LastUse, TimeSpan UseDuration)
        {
            this.modName = ModName;
            this.attacker = Attacker;
            this.victim = Victim;
            this.modValue = ModValue;
            this.lastUse = LastUse;
            this.useDuration = UseDuration;
        }

        public bool Equals(TimerMod other)
        {
            return other.ToString().Equals(this.ToString());
        }

        public override string ToString()
        {
            return string.Format("{0}>{1}({2})", this.attacker, this.victim, this.modName);
        }

        public string Attacker
        {
            get
            {
                return this.attacker;
            }
            set
            {
                this.attacker = value;
            }
        }

        public DateTime LastUse
        {
            get
            {
                return this.lastUse;
            }
            set
            {
                this.lastUse = value;
            }
        }

        public string ModName
        {
            get
            {
                return this.modName;
            }
            set
            {
                this.modName = value;
            }
        }

        public float ModValue
        {
            get
            {
                return this.modValue;
            }
            set
            {
                this.modValue = value;
            }
        }

        public TimeSpan UseDuration
        {
            get
            {
                return this.useDuration;
            }
            set
            {
                this.useDuration = value;
            }
        }

        public string Victim
        {
            get
            {
                return this.victim;
            }
            set
            {
                this.victim = value;
            }
        }
    }
}

