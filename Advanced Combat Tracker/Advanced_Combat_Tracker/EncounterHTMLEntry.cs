namespace Advanced_Combat_Tracker
{
    using System;

    public class EncounterHTMLEntry
    {
        private string damage;
        private string dps;
        private string duration;
        private string end;
        private string start;
        private string title;

        public EncounterHTMLEntry(string Title, string Start, string End, string Duration, string Damage, string DPS)
        {
            this.title = Title;
            this.start = Start;
            this.end = End;
            this.duration = Duration;
            this.damage = Damage;
            this.dps = DPS;
        }

        public string Damage
        {
            get
            {
                return this.damage;
            }
        }

        public string DPS
        {
            get
            {
                return this.dps;
            }
        }

        public string Duration
        {
            get
            {
                return this.duration;
            }
        }

        public string End
        {
            get
            {
                return this.end;
            }
        }

        public string Start
        {
            get
            {
                return this.start;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
        }
    }
}

