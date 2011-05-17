namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class DamageTypeData : IEquatable<DamageTypeData>, IComparable<DamageTypeData>
    {
        private SortedList<string, AttackType> attackTypes = new SortedList<string, AttackType>();
        private TimeSpan cDuration;
        public static Dictionary<string, ColumnDef> ColumnDefs = new Dictionary<string, ColumnDef>();
        private bool durationCached;
        private bool outgoing;
        private CombatantData parent;
        private string tag;
        private Dictionary<string, object> tags = new Dictionary<string, object>();

        public DamageTypeData(bool Outgoing, string Tag, CombatantData Parent)
        {
            this.parent = Parent;
            this.tag = Tag;
            this.outgoing = Outgoing;
            this.durationCached = false;
        }

        public void AddCombatAction(MasterSwing action, string theAttackTypeListed)
        {
            AttackType type;
            this.durationCached = false;
            if (!this.attackTypes.TryGetValue(theAttackTypeListed, out type))
            {
                type = new AttackType(theAttackTypeListed, this);
                this.attackTypes.Add(theAttackTypeListed, type);
            }
            type.AddCombatAction(action);
        }

        public int CompareTo(DamageTypeData other)
        {
            return this.Damage.CompareTo(other.Damage);
        }

        public bool Equals(DamageTypeData other)
        {
            return this.Type.Equals(other.Type);
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
            for (int i = 0; i < this.attackTypes.Values.Count; i++)
            {
                num += this.attackTypes.Values[i].GetHashCode();
            }
            return num.GetHashCode();
        }

        public override string ToString()
        {
            return this.tag;
        }

        public void Trim()
        {
            this.attackTypes.TrimExcess();
            for (int i = 0; i < this.attackTypes.Values.Count; i++)
            {
                this.attackTypes.Values[i].Trim();
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
                return 0f;
            }
        }

        public float AverageDelay
        {
            get
            {
                if (this.Items.ContainsKey(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
                {
                    return this.Items[ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].AverageDelay;
                }
                return float.NaN;
            }
        }

        public int Blocked
        {
            get
            {
                AttackType type;
                if (this.attackTypes.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                {
                    return type.Blocked;
                }
                return 0;
            }
        }

        public double CharDPS
        {
            get
            {
                AttackType type;
                if (this.attackTypes.TryGetValue("All", out type))
                {
                    return type.CharDPS;
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
                AttackType type;
                if (this.attackTypes.TryGetValue("All", out type))
                {
                    return type.CritHits;
                }
                return 0;
            }
        }

        public float CritPerc
        {
            get
            {
                AttackType type;
                if (this.attackTypes.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                {
                    return type.CritPerc;
                }
                return 0f;
            }
        }

        public long Damage
        {
            get
            {
                AttackType type;
                if (this.attackTypes.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                {
                    return type.Damage;
                }
                return 0;
            }
        }

        public double DPS
        {
            get
            {
                AttackType type;
                if (this.attackTypes.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                {
                    return type.DPS;
                }
                return 0.0;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                if ((this.Parent != null) && (this.Parent.Parent.StartTimes.Count > 1))
                {
                    if (!this.durationCached)
                    {
                        AttackType type;
                        this.attackTypes.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type);
                        if (type == null)
                        {
                            return TimeSpan.Zero;
                        }
                        type.Items.Sort(new Comparison<MasterSwing>(MasterSwing.CompareTime));
                        List<DateTime> list = new List<DateTime>();
                        List<DateTime> list2 = new List<DateTime>();
                        int num = 0;
                        for (int i = 0; i < this.Parent.Parent.StartTimes.Count; i++)
                        {
                            if (num < 0)
                            {
                                num = 0;
                            }
                            if ((i >= this.Parent.Parent.EndTimes.Count) || (this.Parent.Parent.EndTimes[i] >= type.Items[num].Time))
                            {
                                for (int k = num; k < type.Items.Count; k++)
                                {
                                    if (list.Count == list2.Count)
                                    {
                                        if ((i == (this.Parent.Parent.StartTimes.Count - 1)) && ((this.Parent.Parent.EndTimes.Count + 1) == this.Parent.Parent.StartTimes.Count))
                                        {
                                            if ((type.Items[k].Time >= this.Parent.Parent.StartTimes[i]) && (type.Items[k].Time <= this.Parent.Parent.EndTime))
                                            {
                                                list.Add(type.Items[k].Time);
                                                num = k;
                                            }
                                        }
                                        else if ((type.Items[k].Time >= this.Parent.Parent.StartTimes[i]) && (type.Items[k].Time <= this.Parent.Parent.EndTimes[i]))
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
                                            if ((this.Parent.Parent.StartTimes.Count > (i + 1)) && (type.Items[m + 1].Time >= this.Parent.Parent.StartTimes[i + 1]))
                                            {
                                                num = m - 1;
                                                break;
                                            }
                                        }
                                        list2.Add(swing.Time);
                                        break;
                                    }
                                    if ((i < this.Parent.Parent.EndTimes.Count) && (type.Items[k].Time > this.Parent.Parent.EndTimes[i]))
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
                            throw new Exception(string.Format("Personal Duration failure.  StartTimes: {0}/{2} EndTimes: {1}/{3}", new object[] { list.Count, list2.Count, this.Parent.Parent.StartTimes.Count, this.Parent.Parent.EndTimes.Count }));
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
                AttackType type;
                if (this.attackTypes.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                {
                    return type.EncDPS;
                }
                return 0.0;
            }
        }

        public DateTime EndTime
        {
            get
            {
                AttackType type;
                if (this.attackTypes.TryGetValue("All", out type))
                {
                    return type.EndTime;
                }
                return DateTime.MinValue;
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
                AttackType type;
                if (this.attackTypes.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                {
                    return type.Hits;
                }
                return 0;
            }
        }

        public SortedList<string, AttackType> Items
        {
            get
            {
                return this.attackTypes;
            }
            set
            {
                this.attackTypes = value;
            }
        }

        public int MaxHit
        {
            get
            {
                AttackType type;
                if (this.attackTypes.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                {
                    return type.MaxHit;
                }
                return 0;
            }
        }

        public int Median
        {
            get
            {
                AttackType type;
                if (this.attackTypes.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                {
                    return type.Median;
                }
                return 0;
            }
        }

        public int MinHit
        {
            get
            {
                AttackType type;
                if (this.attackTypes.TryGetValue("All", out type))
                {
                    return type.MinHit;
                }
                return 0;
            }
        }

        public int Misses
        {
            get
            {
                AttackType type;
                if (this.attackTypes.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                {
                    return type.Misses;
                }
                return 0;
            }
        }

        public bool Outgoing
        {
            get
            {
                return this.outgoing;
            }
            set
            {
                this.outgoing = value;
            }
        }

        public CombatantData Parent
        {
            get
            {
                return this.parent;
            }
        }

        public DateTime StartTime
        {
            get
            {
                AttackType type;
                if (this.attackTypes.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                {
                    return type.StartTime;
                }
                return DateTime.MaxValue;
            }
        }

        public int Swings
        {
            get
            {
                AttackType type;
                if (this.attackTypes.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                {
                    return type.Swings;
                }
                return 0;
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
                return this.ToString();
            }
        }

        public delegate Color ColorDataCallback(DamageTypeData Data);

        public class ColumnDef
        {
            private string dataName;
            private string dataType;
            private bool defaultVisible;
            public DamageTypeData.ColorDataCallback GetCellBackColor = Data => Color.Transparent;
            public DamageTypeData.StringDataCallback GetCellData;
            public DamageTypeData.ColorDataCallback GetCellForeColor = Data => Color.Transparent;
            public DamageTypeData.StringDataCallback GetSqlData;
            private string label;

            public ColumnDef(string Label, bool DefaultVisible, string SqlDataType, string SqlDataName, DamageTypeData.StringDataCallback CellDataCallback, DamageTypeData.StringDataCallback SqlDataCallback)
            {
                this.label = Label;
                this.defaultVisible = DefaultVisible;
                this.dataType = SqlDataType;
                this.dataName = SqlDataName;
                this.GetCellData = CellDataCallback;
                this.GetSqlData = SqlDataCallback;
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

        public delegate string StringDataCallback(DamageTypeData Data);
    }
}

