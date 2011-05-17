namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class MasterSwing : IComparable, IComparable<MasterSwing>
    {
        private string attacker;
        private string attackType;
        public static Dictionary<string, ColumnDef> ColumnDefs = new Dictionary<string, ColumnDef>();
        private bool critical;
        private Dnum damage;
        private string damageType;
        private EncounterData parent;
        private string special;
        private int swingType;
        private Dictionary<string, object> tags;
        private DateTime time;
        private int timeSorter;
        private string victim;

        public MasterSwing(int SwingType, bool Critical, Dnum damage, DateTime Time, int TimeSorter, string theAttackType, string Attacker, string theDamageType, string Victim)
        {
            this.tags = new Dictionary<string, object>();
            this.time = Time;
            this.damage = damage;
            this.attacker = Attacker;
            this.victim = Victim;
            this.attackType = theAttackType;
            this.damageType = theDamageType;
            this.critical = Critical;
            this.timeSorter = TimeSorter;
            this.swingType = SwingType;
            this.special = ActGlobals.ActLocalization.LocalizationStrings["specialAttackTerm-none"].DisplayedText;
        }

        public MasterSwing(int SwingType, bool Critical, string Special, Dnum damage, DateTime Time, int TimeSorter, string theAttackType, string Attacker, string theDamageType, string Victim)
        {
            this.tags = new Dictionary<string, object>();
            this.time = Time;
            this.damage = damage;
            this.attacker = Attacker;
            this.victim = Victim;
            this.attackType = theAttackType;
            this.damageType = theDamageType;
            this.critical = Critical;
            this.timeSorter = TimeSorter;
            this.swingType = SwingType;
            this.special = Special;
        }

        internal static int CompareTime(MasterSwing Left, MasterSwing Right)
        {
            int num = 0;
            num = Left.TimeSorter.CompareTo(Right.TimeSorter);
            if (num == 0)
            {
                num = Left.Time.CompareTo(Right.Time);
            }
            return num;
        }

        public int CompareTo(MasterSwing other)
        {
            int num = 0;
            if (ColumnDefs.ContainsKey(ActGlobals.aTSort))
            {
                num = ColumnDefs[ActGlobals.aTSort].SortComparer(this, other);
            }
            if ((num == 0) && ColumnDefs.ContainsKey(ActGlobals.aTSort2))
            {
                num = ColumnDefs[ActGlobals.aTSort2].SortComparer(this, other);
            }
            if (num != 0)
            {
                return num;
            }
            if (this.TimeSorter != other.TimeSorter)
            {
                return this.TimeSorter.CompareTo(other.TimeSorter);
            }
            return this.Time.CompareTo(other.Time);
        }

        public int CompareTo(object obj)
        {
            return this.CompareTo((MasterSwing) obj);
        }

        public override bool Equals(object obj)
        {
            MasterSwing swing = (MasterSwing) obj;
            string str = this.ToString();
            string str2 = swing.ToString();
            return str.Equals(str2);
        }

        public string GetColumnByName(string name)
        {
            if (ColumnDefs.ContainsKey(name))
            {
                return ColumnDefs[name].GetCellData(this);
            }
            return string.Empty;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", new object[] { this.Time.ToString("s"), this.Damage, this.Attacker, this.Special, this.AttackType, this.DamageType, this.Victim });
        }

        public string Attacker
        {
            get
            {
                return this.attacker;
            }
        }

        public string AttackType
        {
            get
            {
                return this.attackType;
            }
        }

        public string[] ColCollection
        {
            get
            {
                string[] strArray = new string[ColumnDefs.Count];
                int index = 0;
                foreach (KeyValuePair<string, ColumnDef> pair in ColumnDefs)
                {
                    strArray[index] = pair.Value.GetSqlData(this);
                    index++;
                }
                return strArray;
            }
        }

        public static string[] ColHeaderCollection
        {
            get
            {
                string[] strArray = new string[ColumnDefs.Count];
                int index = 0;
                foreach (KeyValuePair<string, ColumnDef> pair in ColumnDefs)
                {
                    strArray[index] = pair.Value.SqlDataName;
                    index++;
                }
                return strArray;
            }
        }

        public static string ColHeaderString
        {
            get
            {
                return string.Join(",", ColHeaderCollection);
            }
        }

        public static string[] ColTypeCollection
        {
            get
            {
                string[] strArray = new string[ColumnDefs.Count];
                int index = 0;
                foreach (KeyValuePair<string, ColumnDef> pair in ColumnDefs)
                {
                    strArray[index] = pair.Value.SqlDataType;
                    index++;
                }
                return strArray;
            }
        }

        public bool Critical
        {
            get
            {
                return this.critical;
            }
            set
            {
                this.critical = value;
            }
        }

        public Dnum Damage
        {
            get
            {
                return this.damage;
            }
        }

        public string DamageType
        {
            get
            {
                return this.damageType;
            }
        }

        public EncounterData ParentEncounter
        {
            get
            {
                return this.parent;
            }
            set
            {
                this.parent = value;
            }
        }

        public string Special
        {
            get
            {
                return this.special;
            }
        }

        public int SwingType
        {
            get
            {
                return this.swingType;
            }
        }

        public Dictionary<string, object> Tags
        {
            get
            {
                return this.tags;
            }
            set
            {
                this.tags = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return this.time;
            }
        }

        public int TimeSorter
        {
            get
            {
                return this.timeSorter;
            }
        }

        public string Victim
        {
            get
            {
                return this.victim;
            }
        }

        public delegate Color ColorDataCallback(MasterSwing Data);

        public class ColumnDef
        {
            private string dataName;
            private string dataType;
            private bool defaultVisible;
            public MasterSwing.ColorDataCallback GetCellBackColor = Data => Color.Transparent;
            public MasterSwing.StringDataCallback GetCellData;
            public MasterSwing.ColorDataCallback GetCellForeColor = Data => Color.Transparent;
            public MasterSwing.StringDataCallback GetSqlData;
            private string label;
            public Comparison<MasterSwing> SortComparer;

            public ColumnDef(string Label, bool DefaultVisible, string SqlDataType, string SqlDataName, MasterSwing.StringDataCallback CellDataCallback, MasterSwing.StringDataCallback SqlDataCallback, Comparison<MasterSwing> SortComparer)
            {
                this.label = Label;
                this.defaultVisible = DefaultVisible;
                this.dataType = SqlDataType;
                this.dataName = SqlDataName;
                this.GetCellData = CellDataCallback;
                this.GetSqlData = SqlDataCallback;
                this.SortComparer = SortComparer;
            }

            public bool DefaultVisible
            {
                get
                {
                    return this.defaultVisible;
                }
            }

            public string Label
            {
                get
                {
                    return this.label;
                }
            }

            public string SqlDataName
            {
                get
                {
                    return this.dataName;
                }
            }

            public string SqlDataType
            {
                get
                {
                    return this.dataType;
                }
            }
        }

        public class DualComparison : IComparer<MasterSwing>
        {
            private string sort1;
            private string sort2;

            public DualComparison(string Sort1, string Sort2)
            {
                this.sort1 = Sort1;
                this.sort2 = Sort2;
            }

            public int Compare(MasterSwing Left, MasterSwing Right)
            {
                int num = 0;
                if (MasterSwing.ColumnDefs.ContainsKey(this.sort1))
                {
                    num = MasterSwing.ColumnDefs[this.sort1].SortComparer(Left, Right);
                }
                if ((num == 0) && MasterSwing.ColumnDefs.ContainsKey(this.sort2))
                {
                    num = MasterSwing.ColumnDefs[this.sort2].SortComparer(Left, Right);
                }
                if (num == 0)
                {
                    num = Left.Time.CompareTo(Right.Time);
                }
                return num;
            }
        }

        public delegate string StringDataCallback(MasterSwing Data);
    }
}

