namespace Advanced_Combat_Tracker
{
    using System;

    public class CombatActionEventArgs : EventArgs
    {
        public string attacker;
        public bool cancelAction;
        public bool critical;
        public Dnum damage;
        public string special;
        public int swingType;
        public string theAttackType;
        public string theDamageType;
        public DateTime time;
        public int timeSorter;
        public string victim;

        public CombatActionEventArgs(int SwingType, bool Critical, string Attacker, string TheAttackType, Dnum Damage, DateTime Time, int TimeSorter, string Victim, string TheDamageType)
        {
            this.swingType = SwingType;
            this.critical = Critical;
            this.attacker = Attacker;
            this.theAttackType = TheAttackType;
            this.damage = Damage;
            this.time = Time;
            this.timeSorter = TimeSorter;
            this.victim = Victim;
            this.theDamageType = TheDamageType;
            this.special = ActGlobals.ActLocalization.LocalizationStrings["specialAttackTerm-none"].DisplayedText;
        }

        public CombatActionEventArgs(int SwingType, bool Critical, string Special, string Attacker, string TheAttackType, Dnum Damage, DateTime Time, int TimeSorter, string Victim, string TheDamageType)
        {
            this.swingType = SwingType;
            this.critical = Critical;
            this.attacker = Attacker;
            this.theAttackType = TheAttackType;
            this.damage = Damage;
            this.time = Time;
            this.timeSorter = TimeSorter;
            this.victim = Victim;
            this.theDamageType = TheDamageType;
            this.special = Special;
        }
    }
}

