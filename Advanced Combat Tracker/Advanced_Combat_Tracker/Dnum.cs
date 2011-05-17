namespace Advanced_Combat_Tracker
{
    using System;

    public class Dnum : IComparable
    {
        private string damageString;
        private string damageString2;
        private int num;

        public Dnum(int NumberValue)
        {
            this.num = NumberValue;
            this.damageString = string.Empty;
        }

        public Dnum(int NumberValue, string CustomDamageString)
        {
            this.num = NumberValue;
            this.damageString = CustomDamageString;
        }

        public int CompareTo(object obj)
        {
            Dnum dnum = (Dnum) obj;
            int num = this.num;
            int num2 = dnum.num;
            if ((num == -9) && (num2 == -9))
            {
                return this.damageString.CompareTo(dnum.damageString);
            }
            return num.CompareTo(num2);
        }

        public override bool Equals(object obj)
        {
            Dnum dnum = (Dnum) obj;
            if (dnum.num == this.num)
            {
                string damageString = this.DamageString;
                string str2 = dnum.DamageString;
                return damageString.Equals(str2);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.num.GetHashCode();
        }

        public static Dnum operator +(Dnum a, Dnum b)
        {
            if ((a.num > -1) && (b.num > -1))
            {
                return new Dnum(a.num + b.num);
            }
            if ((a.num < 0) && (b.num >= 0))
            {
                return new Dnum(b.num);
            }
            if ((b.num < 0) && (a.num >= 0))
            {
                return new Dnum(a.num);
            }
            return new Dnum(0);
        }

        public static bool operator ==(Dnum a, Dnum b)
        {
            return a.Equals(b);
        }

        public static implicit operator int(Dnum val)
        {
            return val.num;
        }

        public static implicit operator Dnum(int val)
        {
            if (val >= -10)
            {
                return new Dnum(val);
            }
            return new Dnum(-9);
        }

        public static bool operator !=(Dnum a, Dnum b)
        {
            return !a.Equals(b);
        }

        public override string ToString()
        {
            if ((this.num > 0) && string.IsNullOrEmpty(this.damageString))
            {
                return this.num.ToString();
            }
            switch (this.num)
            {
                case -10:
                    return "Death";

                case -5:
                    return "Block";

                case -4:
                    return "Riposte";

                case -3:
                    return "Parry";

                case -2:
                    return "Resist";

                case -1:
                    return "Miss";

                case 0:
                    return "No Damage";
            }
            return (this.damageString + this.damageString2);
        }

        public string ToString(bool ShortHand)
        {
            if (this.num > 0)
            {
                return this.num.ToString();
            }
            if (ShortHand)
            {
                switch (this.num)
                {
                    case -1:
                        return "Mss";

                    case 0:
                        return "0";

                    case -10:
                        return "Dth";
                }
                if (this.damageString.Length < 3)
                {
                    return this.damageString;
                }
                return this.damageString.Substring(0, 3);
            }
            switch (this.num)
            {
                case -1:
                    return "Miss";

                case 0:
                    return "No Damage";

                case -10:
                    return "Death";
            }
            return this.damageString;
        }

        public string DamageString
        {
            get
            {
                if (string.IsNullOrEmpty(this.damageString))
                {
                    return this.ToString();
                }
                return this.damageString;
            }
            set
            {
                this.damageString = value;
            }
        }

        public string DamageString2
        {
            get
            {
                return this.damageString2;
            }
            set
            {
                this.damageString2 = value;
            }
        }

        public static Dnum Death
        {
            get
            {
                return -10;
            }
        }

        public static Dnum Miss
        {
            get
            {
                return -1;
            }
        }

        public static Dnum NoDamage
        {
            get
            {
                return 0;
            }
        }

        public int Number
        {
            get
            {
                return this.num;
            }
        }

        public static Dnum ThreatPosition
        {
            get
            {
                return -11;
            }
        }

        public static Dnum Unknown
        {
            get
            {
                return -9;
            }
        }
    }
}

