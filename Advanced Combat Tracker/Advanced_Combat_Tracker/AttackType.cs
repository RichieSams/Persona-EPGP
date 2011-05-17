namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class AttackType : IComparable, IEquatable<AttackType>, IComparable<AttackType>
    {
        private bool attacktypetypeCached;
        private bool averageDelayCached;
        private bool blockedCached;
        private AttackTypeTypeEnum cAttackTypeType;
        private float cAverageDelay;
        private int cBlocked;
        private int cCritHits;
        private long cDamage;
        private TimeSpan cDuration;
        private DateTime cEndTime;
        private int cHits;
        private int cMaxHit;
        private int cMedian;
        private int cMinHit;
        private int cMisses;
        public static Dictionary<string, ColumnDef> ColumnDefs = new Dictionary<string, ColumnDef>();
        private string cResist;
        private bool crithitsCached;
        private DateTime cStartTime;
        private int cSwings;
        private bool damageCached;
        private bool durationCached;
        private bool endtimeCached;
        private bool hitsCached;
        private bool maxhitCached;
        private bool medianCached;
        private bool minhitCached;
        private bool missesCached;
        private DamageTypeData parent;
        private bool resistCached;
        private bool starttimeCached;
        private List<MasterSwing> swings = new List<MasterSwing>();
        private bool swingsCached;
        private Dictionary<string, object> tags = new Dictionary<string, object>();
        private string type;

        public AttackType(string theAttackType, DamageTypeData Parent)
        {
            this.type = theAttackType;
            this.damageCached = false;
            this.hitsCached = false;
            this.swingsCached = false;
            this.missesCached = false;
            this.blockedCached = false;
            this.medianCached = false;
            this.starttimeCached = false;
            this.endtimeCached = false;
            this.minhitCached = false;
            this.maxhitCached = false;
            this.crithitsCached = false;
            this.parent = Parent;
            this.durationCached = false;
            this.attacktypetypeCached = false;
            this.averageDelayCached = false;
        }

        public void AddCombatAction(MasterSwing action)
        {
            this.damageCached = false;
            this.hitsCached = false;
            this.swingsCached = false;
            this.missesCached = false;
            this.blockedCached = false;
            this.medianCached = false;
            this.starttimeCached = false;
            this.endtimeCached = false;
            this.minhitCached = false;
            this.maxhitCached = false;
            this.crithitsCached = false;
            this.resistCached = false;
            this.durationCached = false;
            if (this.attacktypetypeCached && (this.cAttackTypeType == AttackTypeTypeEnum.UnknownNonMelee))
            {
                this.attacktypetypeCached = false;
            }
            this.averageDelayCached = false;
            this.swings.Add(action);
        }

        public int CompareTo(AttackType other)
        {
            int num = 0;
            if (ColumnDefs.ContainsKey(ActGlobals.mDSort))
            {
                num = ColumnDefs[ActGlobals.mDSort].SortComparer(this, other);
            }
            if ((num == 0) && ColumnDefs.ContainsKey(ActGlobals.mDSort2))
            {
                num = ColumnDefs[ActGlobals.mDSort2].SortComparer(this, other);
            }
            if (num == 0)
            {
                num = this.Damage.CompareTo(other.Damage);
            }
            return num;
        }

        public int CompareTo(object obj)
        {
            return this.CompareTo((AttackType) obj);
        }

        public bool Equals(AttackType other)
        {
            return (this.type == other.type);
        }

        public override bool Equals(object obj)
        {
            if (obj == DBNull.Value)
            {
                return false;
            }
            AttackType type = (AttackType) obj;
            string str = this.Type;
            string str2 = type.Type;
            return str.Equals(str2);
        }

        public Dictionary<string, int> GetAttackSpecials()
        {
            List<MasterSwing> list = new List<MasterSwing>(this.Items);
            list.Sort(new Comparison<MasterSwing>(MasterSwing.CompareTime));
            List<string> list2 = new List<string>();
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int i = 0; i < list.Count; i++)
            {
                MasterSwing swing = list[i];
                if (swing.Special == ActGlobals.ActLocalization.LocalizationStrings["specialAttackTerm-none"].DisplayedText)
                {
                    list2.Clear();
                }
                if (!list2.Contains(swing.Special))
                {
                    if (dictionary.ContainsKey(swing.Special))
                    {
                        Dictionary<string, int> dictionary2;
                        string str;
                        (dictionary2 = dictionary)[str = swing.Special] = dictionary2[str] + 1;
                    }
                    else
                    {
                        dictionary.Add(swing.Special, 1);
                    }
                    if (swing.Special != ActGlobals.ActLocalization.LocalizationStrings["specialAttackTerm-none"].DisplayedText)
                    {
                        if (dictionary.ContainsKey("ANY"))
                        {
                            Dictionary<string, int> dictionary3;
                            (dictionary3 = dictionary)["ANY"] = dictionary3["ANY"] + 1;
                        }
                        else
                        {
                            dictionary.Add("ANY", 1);
                        }
                        if (!list2.Contains("ONCE"))
                        {
                            if (dictionary.ContainsKey("ONCE"))
                            {
                                Dictionary<string, int> dictionary4;
                                (dictionary4 = dictionary)["ONCE"] = dictionary4["ONCE"] + 1;
                            }
                            else
                            {
                                dictionary.Add("ONCE", 1);
                            }
                            list2.Add("ONCE");
                        }
                    }
                }
                list2.Add(swing.Special);
            }
            return dictionary;
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
            long num = 0;
            for (int i = 0; i < this.swings.Count; i++)
            {
                MasterSwing swing = this.swings[i];
                num += swing.GetHashCode();
            }
            return num.GetHashCode();
        }

        public Dictionary<int, int> GetSwingTypeCounts()
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            for (int i = 0; i < this.Items.Count; i++)
            {
                MasterSwing swing = this.Items[i];
                if (dictionary.ContainsKey(swing.SwingType))
                {
                    Dictionary<int, int> dictionary2;
                    int num2;
                    (dictionary2 = dictionary)[num2 = swing.SwingType] = dictionary2[num2] + 1;
                }
                else
                {
                    dictionary.Add(swing.SwingType, 1);
                }
            }
            return dictionary;
        }

        public override string ToString()
        {
            return this.Type;
        }

        public void Trim()
        {
            this.swings.TrimExcess();
        }

        public AttackTypeTypeEnum AttackTypeType
        {
            get
            {
                if (!this.attacktypetypeCached)
                {
                    int key = -1;
                    if (CombatantData.DamageSwingTypes.Count == 1)
                    {
                        return AttackTypeTypeEnum.UnknownNonMelee;
                    }
                    key = CombatantData.DamageSwingTypes[0];
                    Dictionary<int, int> swingTypeCounts = this.GetSwingTypeCounts();
                    if ((swingTypeCounts.Count == 1) && swingTypeCounts.ContainsKey(key))
                    {
                        this.cAttackTypeType = AttackTypeTypeEnum.Melee;
                    }
                    else
                    {
                        this.cAttackTypeType = AttackTypeTypeEnum.UnknownNonMelee;
                    }
                    this.attacktypetypeCached = true;
                }
                return this.cAttackTypeType;
            }
        }

        public float Average
        {
            get
            {
                if (this.Hits > 0)
                {
                    return (((float) this.Damage) / ((float) this.Hits));
                }
                return float.NaN;
            }
        }

        public float AverageDelay
        {
            get
            {
                if (!this.averageDelayCached)
                {
                    if (!ActGlobals.calcRealAvgDelay)
                    {
                        return (((float) this.Duration.TotalSeconds) / ((float) this.Swings));
                    }
                    List<MasterSwing> list = new List<MasterSwing>(this.swings);
                    Dictionary<DateTime, DateTime> dictionary = new Dictionary<DateTime, DateTime>();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (!dictionary.ContainsKey(list[i].Time))
                        {
                            dictionary.Add(list[i].Time, list[i].Time);
                        }
                    }
                    this.cAverageDelay = ((float) this.Duration.TotalSeconds) / ((float) (dictionary.Count - 1));
                }
                return this.cAverageDelay;
            }
        }

        public int Blocked
        {
            get
            {
                if (this.blockedCached)
                {
                    return this.cBlocked;
                }
                int num = 0;
                for (int i = 0; i < this.swings.Count; i++)
                {
                    MasterSwing swing = this.swings[i];
                    if ((swing.Damage < -1) && (swing.Damage != Dnum.Death))
                    {
                        num++;
                    }
                }
                this.cBlocked = num;
                this.blockedCached = true;
                return num;
            }
        }

        public double CharDPS
        {
            get
            {
                if (this.Parent.Parent == null)
                {
                    return double.NaN;
                }
                double totalSeconds = this.Parent.Parent.Duration.TotalSeconds;
                if (totalSeconds > 0.0)
                {
                    return (((double) this.Damage) / totalSeconds);
                }
                return 0.0;
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

        public int CritHits
        {
            get
            {
                if (this.crithitsCached)
                {
                    return this.cCritHits;
                }
                int num = 0;
                for (int i = 0; i < this.swings.Count; i++)
                {
                    MasterSwing swing = this.swings[i];
                    if (ActGlobals.blockIsHit)
                    {
                        if (swing.Critical && (swing.Damage >= 0))
                        {
                            num++;
                        }
                    }
                    else if (swing.Critical && (swing.Damage > 0))
                    {
                        num++;
                    }
                }
                this.cCritHits = num;
                this.crithitsCached = true;
                return num;
            }
        }

        public float CritPerc
        {
            get
            {
                return ((((float) this.CritHits) / ((float) this.Hits)) * 100f);
            }
        }

        public long Damage
        {
            get
            {
                if (this.damageCached)
                {
                    return this.cDamage;
                }
                long num = 0;
                for (int i = 0; i < this.swings.Count; i++)
                {
                    MasterSwing swing = this.swings[i];
                    if (swing.Damage > 0)
                    {
                        num += (long) swing.Damage;
                    }
                }
                this.cDamage = num;
                this.damageCached = true;
                return num;
            }
        }

        public double DPS
        {
            get
            {
                double totalSeconds = this.Duration.TotalSeconds;
                if (totalSeconds > 0.0)
                {
                    return (((double) this.Damage) / totalSeconds);
                }
                return 0.0;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                if ((this.Parent.Parent != null) && (this.Parent.Parent.Parent.StartTimes.Count > 1))
                {
                    if (!this.durationCached)
                    {
                        AttackType type = this;
                        if (type == null)
                        {
                            return TimeSpan.Zero;
                        }
                        type.Items.Sort(new Comparison<MasterSwing>(MasterSwing.CompareTime));
                        List<DateTime> list = new List<DateTime>();
                        List<DateTime> list2 = new List<DateTime>();
                        int num = 0;
                        for (int i = 0; i < this.Parent.Parent.Parent.StartTimes.Count; i++)
                        {
                            if (num < 0)
                            {
                                num = 0;
                            }
                            if ((i >= this.Parent.Parent.Parent.EndTimes.Count) || (this.Parent.Parent.Parent.EndTimes[i] >= type.Items[num].Time))
                            {
                                for (int k = num; k < type.Items.Count; k++)
                                {
                                    if (list.Count == list2.Count)
                                    {
                                        if ((i == (this.Parent.Parent.Parent.StartTimes.Count - 1)) && ((this.Parent.Parent.Parent.EndTimes.Count + 1) == this.Parent.Parent.Parent.StartTimes.Count))
                                        {
                                            if ((type.Items[k].Time >= this.Parent.Parent.Parent.StartTimes[i]) && (type.Items[k].Time <= this.Parent.Parent.Parent.EndTime))
                                            {
                                                list.Add(type.Items[k].Time);
                                                num = k;
                                            }
                                        }
                                        else if ((type.Items[k].Time >= this.Parent.Parent.Parent.StartTimes[i]) && (type.Items[k].Time <= this.Parent.Parent.Parent.EndTimes[i]))
                                        {
                                            list.Add(type.Items[k].Time);
                                            num = k;
                                        }
                                    }
                                    if ((list.Count - 1) == list2.Count)
                                    {
                                        MasterSwing swing = null;
                                        for (int m = k; m < type.Items.Count; m++)
                                        {
                                            swing = type.Items[m];
                                            if ((m + 1) == type.Items.Count)
                                            {
                                                num = m - 1;
                                                break;
                                            }
                                            if ((this.Parent.Parent.Parent.StartTimes.Count > (i + 1)) && (type.Items[m + 1].Time >= this.Parent.Parent.Parent.StartTimes[i + 1]))
                                            {
                                                num = m - 1;
                                                break;
                                            }
                                        }
                                        list2.Add(swing.Time);
                                        break;
                                    }
                                    if ((i < this.Parent.Parent.Parent.EndTimes.Count) && (type.Items[k].Time > this.Parent.Parent.Parent.EndTimes[i]))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        if ((list.Count - 1) == list2.Count)
                        {
                            list2.Add(type.Items[type.Items.Count - 1].Time);
                        }
                        if (list.Count != list2.Count)
                        {
                            throw new Exception(string.Format("Personal Duration failure.  StartTimes: {0}/{2} EndTimes: {1}/{3}", new object[] { list.Count, list2.Count, this.Parent.Parent.Parent.StartTimes.Count, this.Parent.Parent.Parent.EndTimes.Count }));
                        }
                        TimeSpan span = new TimeSpan();
                        for (int j = 0; j < list.Count; j++)
                        {
                            span += list2[j] - list[j];
                        }
                        this.cDuration = span;
                        this.durationCached = true;
                    }
                    return this.cDuration;
                }
                if (this.EndTime > this.StartTime)
                {
                    return (TimeSpan) (this.EndTime - this.StartTime);
                }
                return TimeSpan.Zero;
            }
        }

        public string DurationS
        {
            get
            {
                if (this.Duration.Hours == 0)
                {
                    return string.Format("{0:00}:{1:00}", this.Duration.Minutes, this.Duration.Seconds);
                }
                return string.Format("{0:00}:{1:00}:{2:00}", this.Duration.Hours, this.Duration.Minutes, this.Duration.Seconds);
            }
        }

        public double EncDPS
        {
            get
            {
                if (this.Parent.Parent == null)
                {
                    return double.NaN;
                }
                double totalSeconds = this.Parent.Parent.Parent.Duration.TotalSeconds;
                if (totalSeconds > 0.0)
                {
                    return (((double) this.Damage) / totalSeconds);
                }
                return 0.0;
            }
        }

        public DateTime EndTime
        {
            get
            {
                if (this.endtimeCached)
                {
                    return this.cEndTime;
                }
                DateTime minValue = DateTime.MinValue;
                for (int i = 0; i < this.swings.Count; i++)
                {
                    MasterSwing swing = this.swings[i];
                    if (swing.Time > minValue)
                    {
                        minValue = swing.Time;
                    }
                }
                this.cEndTime = minValue;
                this.endtimeCached = true;
                return minValue;
            }
        }

        public double ExtDPS
        {
            get
            {
                return this.EncDPS;
            }
        }

        public int Hits
        {
            get
            {
                if (this.hitsCached)
                {
                    return this.cHits;
                }
                int num = 0;
                for (int i = 0; i < this.swings.Count; i++)
                {
                    MasterSwing swing = this.swings[i];
                    if (ActGlobals.blockIsHit)
                    {
                        if (swing.Damage >= 0)
                        {
                            num++;
                        }
                    }
                    else if (swing.Damage > 0)
                    {
                        num++;
                    }
                }
                this.cHits = num;
                this.hitsCached = true;
                return num;
            }
        }

        public List<MasterSwing> Items
        {
            get
            {
                return this.swings;
            }
            set
            {
                this.swings = value;
            }
        }

        public int MaxHit
        {
            get
            {
                if (this.maxhitCached)
                {
                    return this.cMaxHit;
                }
                int damage = 0;
                for (int i = 0; i < this.swings.Count; i++)
                {
                    MasterSwing swing = this.swings[i];
                    if ((swing.Damage > 0) && (swing.Damage > damage))
                    {
                        damage = (int) swing.Damage;
                    }
                }
                this.cMaxHit = damage;
                this.maxhitCached = true;
                return damage;
            }
        }

        public int Median
        {
            get
            {
                try
                {
                    if (!this.medianCached)
                    {
                        List<Dnum> list = new List<Dnum>();
                        for (int i = 0; i < this.swings.Count; i++)
                        {
                            MasterSwing swing = this.swings[i];
                            if (swing.Damage >= 0)
                            {
                                list.Add(swing.Damage);
                            }
                        }
                        try
                        {
                            list.Sort();
                        }
                        catch (Exception exception)
                        {
                            ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Empty);
                        }
                        int num2 = list.Count / 2;
                        if (list.Count > num2)
                        {
                            this.cMedian = list[num2];
                        }
                        else
                        {
                            this.cMedian = 0;
                        }
                        this.medianCached = true;
                    }
                    return this.cMedian;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public int MinHit
        {
            get
            {
                if (this.minhitCached)
                {
                    return this.cMinHit;
                }
                int damage = 0x7fffffff;
                for (int i = 0; i < this.swings.Count; i++)
                {
                    MasterSwing swing = this.swings[i];
                    if (ActGlobals.blockIsHit)
                    {
                        if ((swing.Damage >= 0) && (swing.Damage < damage))
                        {
                            damage = (int) swing.Damage;
                        }
                    }
                    else if ((swing.Damage > 0) && (swing.Damage < damage))
                    {
                        damage = (int) swing.Damage;
                    }
                }
                if (damage == 0x7fffffff)
                {
                    return 0;
                }
                this.cMinHit = damage;
                this.minhitCached = true;
                return damage;
            }
        }

        public int Misses
        {
            get
            {
                if (this.missesCached)
                {
                    return this.cMisses;
                }
                int num = 0;
                for (int i = 0; i < this.swings.Count; i++)
                {
                    MasterSwing swing = this.swings[i];
                    if (swing.Damage == Dnum.Miss)
                    {
                        num++;
                    }
                }
                this.cMisses = num;
                this.missesCached = true;
                return num;
            }
        }

        public DamageTypeData Parent
        {
            get
            {
                return this.parent;
            }
        }

        public string Resist
        {
            get
            {
                if (this.resistCached)
                {
                    return this.cResist;
                }
                string str = string.Empty;
                if (this.type == ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText)
                {
                    str = "All";
                    goto Label_00ED;
                }
                string str2 = string.Empty;
                List<string> list = new List<string>();
                for (int i = 0; i < this.swings.Count; i++)
                {
                    string item = this.swings[i].DamageType.Replace("warded/", string.Empty);
                    switch (item)
                    {
                        case "melee":
                        case "non-melee":
                        case "warded":
                            str2 = item;
                            break;

                        default:
                            if (!list.Contains(item))
                            {
                                list.Add(item);
                                goto Label_00C8;
                            }
                            break;
                    }
                }
            Label_00C8:
                if (list.Count == 1)
                {
                    str = list[0];
                }
                else if (string.IsNullOrEmpty(str2))
                {
                    str = "unknown";
                }
                else
                {
                    str = str2;
                }
            Label_00ED:
                this.cResist = str;
                this.resistCached = true;
                return str;
            }
        }

        public DateTime StartTime
        {
            get
            {
                if (this.starttimeCached)
                {
                    return this.cStartTime;
                }
                DateTime maxValue = DateTime.MaxValue;
                for (int i = 0; i < this.swings.Count; i++)
                {
                    MasterSwing swing = this.swings[i];
                    if (swing.Time < maxValue)
                    {
                        maxValue = swing.Time;
                    }
                }
                this.cStartTime = maxValue;
                this.starttimeCached = true;
                return maxValue;
            }
        }

        public int Swings
        {
            get
            {
                if (this.swingsCached)
                {
                    return this.cSwings;
                }
                int num = 0;
                for (int i = 0; i < this.swings.Count; i++)
                {
                    MasterSwing swing = this.swings[i];
                    if (swing.Damage != Dnum.Death)
                    {
                        num++;
                    }
                    else if (this.type == "Killing")
                    {
                        num++;
                    }
                }
                this.cSwings = num;
                this.swingsCached = true;
                return num;
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

        public float ToHit
        {
            get
            {
                try
                {
                    float hits = this.Hits;
                    float swings = this.Swings;
                    float num3 = hits / swings;
                    return (num3 * 100f);
                }
                catch
                {
                    return 0f;
                }
            }
        }

        public string Type
        {
            get
            {
                return this.type;
            }
        }

        public delegate Color ColorDataCallback(AttackType Data);

        public class ColumnDef
        {
            private string dataName;
            private string dataType;
            private bool defaultVisible;
            public AttackType.ColorDataCallback GetCellBackColor = Data => Color.Transparent;
            public AttackType.StringDataCallback GetCellData;
            public AttackType.ColorDataCallback GetCellForeColor = Data => Color.Transparent;
            public AttackType.StringDataCallback GetSqlData;
            private string label;
            public Comparison<AttackType> SortComparer;

            public ColumnDef(string Label, bool DefaultVisible, string SqlDataType, string SqlDataName, AttackType.StringDataCallback CellDataCallback, AttackType.StringDataCallback SqlDataCallback, Comparison<AttackType> SortComparer)
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

        public class DualComparison : IComparer<AttackType>
        {
            private string sort1;
            private string sort2;

            public DualComparison(string Sort1, string Sort2)
            {
                this.sort1 = Sort1;
                this.sort2 = Sort2;
            }

            public int Compare(AttackType Left, AttackType Right)
            {
                int num = 0;
                if (AttackType.ColumnDefs.ContainsKey(this.sort1))
                {
                    num = AttackType.ColumnDefs[this.sort1].SortComparer(Left, Right);
                }
                if ((num == 0) && AttackType.ColumnDefs.ContainsKey(this.sort2))
                {
                    num = AttackType.ColumnDefs[this.sort2].SortComparer(Left, Right);
                }
                if (num == 0)
                {
                    num = Left.Damage.CompareTo(Right.Damage);
                }
                return num;
            }
        }

        public delegate string StringDataCallback(AttackType Data);
    }
}

