namespace Advanced_Combat_Tracker
{
    using System;

    public class StrFloat : IComparable, IEquatable<StrFloat>
    {
        private string name;
        private float val;

        public StrFloat(string Name, float Val)
        {
            this.name = Name;
            this.val = Val;
        }

        public int CompareTo(object obj)
        {
            StrFloat num = (StrFloat) obj;
            float val = this.val;
            float num3 = num.val;
            return val.CompareTo(num3);
        }

        public bool Equals(StrFloat other)
        {
            return ((this.name == other.name) && (this.val == other.val));
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public float Val
        {
            get
            {
                return this.val;
            }
        }
    }
}

