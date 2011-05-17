namespace Advanced_Combat_Tracker
{
    using System;

    public class HitDist : IComparable
    {
        private int count;
        private int damage;

        public HitDist(int Damage)
        {
            this.damage = Damage;
            this.count = 1;
        }

        public int CompareTo(object obj)
        {
            HitDist dist = (HitDist) obj;
            int damage = this.Damage;
            int num2 = dist.Damage;
            return damage.CompareTo(num2);
        }

        public override bool Equals(object obj)
        {
            HitDist dist = (HitDist) obj;
            int damage = this.Damage;
            int num2 = dist.Damage;
            return damage.Equals(num2);
        }

        public override int GetHashCode()
        {
            return (this.damage + " - " + this.count).GetHashCode();
        }

        public int Count
        {
            get
            {
                return this.count;
            }
            set
            {
                this.count = value;
            }
        }

        public int Damage
        {
            get
            {
                return this.damage;
            }
        }
    }
}

