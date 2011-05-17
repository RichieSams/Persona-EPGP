namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class CombatantData : IComparable, IEquatable<CombatantData>, IComparable<CombatantData>
    {
        private SortedList<string, int> allies;
        private int cDeaths;
        private TimeSpan cDuration;
        private DateTime cEndTime;
        private int cKills;
        public static Dictionary<string, ColumnDef> ColumnDefs = new Dictionary<string, ColumnDef>();
        private DateTime cStartTime;
        private int cThreatDelta;
        private string cThreatStr;
        public static List<int> DamageSwingTypes = new List<int>();
        public static string DamageTypeDataIncomingDamage = string.Empty;
        public static string DamageTypeDataIncomingHealing = string.Empty;
        public static string DamageTypeDataNonSkillDamage = string.Empty;
        public static string DamageTypeDataOutgoingDamage = string.Empty;
        public static string DamageTypeDataOutgoingHealing = string.Empty;
        private bool deathsCached;
        private bool durationCached;
        private bool endTimeCached;
        public static Dictionary<string, TextExportFormatter> ExportVariables = new Dictionary<string, TextExportFormatter>();
        public static List<int> HealingSwingTypes = new List<int>();
        private DamageTypeData incAll;
        public static Dictionary<string, DamageTypeDef> IncomingDamageTypeDataObjects = new Dictionary<string, DamageTypeDef>();
        private Dictionary<string, DamageTypeData> items;
        private bool killsCached;
        private string name;
        private DamageTypeData outAll;
        public static Dictionary<string, DamageTypeDef> OutgoingDamageTypeDataObjects = new Dictionary<string, DamageTypeDef>();
        private EncounterData parent;
        private bool startTimeCached;
        public static SortedDictionary<int, List<string>> SwingTypeToDamageTypeDataLinksIncoming = new SortedDictionary<int, List<string>>();
        public static SortedDictionary<int, List<string>> SwingTypeToDamageTypeDataLinksOutgoing = new SortedDictionary<int, List<string>>();
        private Dictionary<string, object> tags = new Dictionary<string, object>();
        private bool threatCached;

        public CombatantData(string combatantName, EncounterData Parent)
        {
            this.name = combatantName;
            this.items = new Dictionary<string, DamageTypeData>();
            foreach (KeyValuePair<string, DamageTypeDef> pair in OutgoingDamageTypeDataObjects)
            {
                this.outAll = new DamageTypeData(true, pair.Key, this);
                this.items.Add(pair.Key, this.outAll);
            }
            foreach (KeyValuePair<string, DamageTypeDef> pair2 in IncomingDamageTypeDataObjects)
            {
                this.incAll = new DamageTypeData(false, pair2.Key, this);
                this.items.Add(pair2.Key, this.incAll);
            }
            this.allies = new SortedList<string, int>();
            this.parent = Parent;
            this.durationCached = false;
            this.deathsCached = false;
            this.killsCached = false;
            this.startTimeCached = false;
            this.endTimeCached = false;
            this.threatCached = false;
        }

        public void AddCombatAction(MasterSwing action)
        {
            this.durationCached = false;
            this.startTimeCached = false;
            this.endTimeCached = false;
            string combatant = action.Victim.ToUpper();
            if (SwingTypeToDamageTypeDataLinksOutgoing.ContainsKey(action.SwingType))
            {
                List<string> list = SwingTypeToDamageTypeDataLinksOutgoing[action.SwingType];
                for (int i = 0; i < list.Count; i++)
                {
                    DamageTypeData data = this.items[list[i]];
                    int allyValue = OutgoingDamageTypeDataObjects[data.Type].AllyValue;
                    this.ModAlly(combatant, allyValue);
                    this.items[list[i]].AddCombatAction(action, ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText);
                    if (!ActGlobals.restrictToAll)
                    {
                        this.items[list[i]].AddCombatAction(action, action.AttackType);
                    }
                }
                this.outAll.AddCombatAction(action, ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText);
                this.outAll.AddCombatAction(action, action.AttackType);
            }
        }

        public void AddReverseCombatAction(MasterSwing action)
        {
            this.durationCached = false;
            this.deathsCached = false;
            this.startTimeCached = false;
            this.endTimeCached = false;
            string combatant = action.Attacker.ToUpper();
            if (SwingTypeToDamageTypeDataLinksIncoming.ContainsKey(action.SwingType))
            {
                List<string> list = SwingTypeToDamageTypeDataLinksIncoming[action.SwingType];
                for (int i = 0; i < list.Count; i++)
                {
                    DamageTypeData data = this.items[list[i]];
                    int allyValue = IncomingDamageTypeDataObjects[data.Type].AllyValue;
                    this.ModAlly(combatant, allyValue);
                    this.items[list[i]].AddCombatAction(action, ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText);
                    if (!ActGlobals.restrictToAll)
                    {
                        this.items[list[i]].AddCombatAction(action, action.AttackType);
                    }
                }
                this.incAll.AddCombatAction(action, ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText);
                this.incAll.AddCombatAction(action, action.AttackType);
            }
        }

        internal static int CompareDamageTakenTime(CombatantData Left, CombatantData Right)
        {
            int num = 0;
            num = Left.DamageTaken.CompareTo(Right.DamageTaken);
            if (num == 0)
            {
                num = Left.Name.CompareTo(Right.Name);
            }
            return num;
        }

        public int CompareTo(CombatantData other)
        {
            int num = 0;
            if (ColumnDefs.ContainsKey(ActGlobals.eDSort))
            {
                num = ColumnDefs[ActGlobals.eDSort].SortComparer(this, other);
            }
            if ((num == 0) && ColumnDefs.ContainsKey(ActGlobals.eDSort2))
            {
                num = ColumnDefs[ActGlobals.eDSort2].SortComparer(this, other);
            }
            if (num == 0)
            {
                num = this.Damage.CompareTo(other.Damage);
            }
            return num;
        }

        public int CompareTo(object obj)
        {
            return this.CompareTo((CombatantData) obj);
        }

        public bool Equals(CombatantData other)
        {
            return (this.name.ToLower() == other.name.ToLower());
        }

        public override bool Equals(object obj)
        {
            CombatantData data = (CombatantData) obj;
            string str = this.Name.ToLower();
            string str2 = data.Name.ToLower();
            return str.Equals(str2);
        }

        public AttackType GetAttackType(string AttackTypeName, string Type)
        {
            AttackType type;
            if (this.Items[Type].Items.TryGetValue(AttackTypeName, out type))
            {
                return type;
            }
            return null;
        }

        public string GetColumnByName(string name)
        {
            if (ColumnDefs.ContainsKey(name))
            {
                return ColumnDefs[name].GetCellData(this);
            }
            return string.Empty;
        }

        public int GetCombatantType()
        {
            if (!this.parent.GetAllies().Contains(this))
            {
                return 0;
            }
            long damage = this.Items[DamageTypeDataOutgoingDamage].Damage;
            long num2 = this.Items[DamageTypeDataOutgoingHealing].Damage;
            long num3 = this.Items[DamageTypeDataNonSkillDamage].Damage;
            long num4 = this.Items[DamageTypeDataIncomingHealing].Damage;
            if ((num4 > (damage / 3)) && (damage > num2))
            {
                if (this.Name.Contains(" "))
                {
                    return 4;
                }
                return 1;
            }
            if ((num2 > (damage / 3)) && (num2 > num4))
            {
                return 2;
            }
            if (num3 > (damage / 10))
            {
                return 3;
            }
            return 4;
        }

        public override int GetHashCode()
        {
            long num = 0;
            foreach (DamageTypeData data in this.items.Values)
            {
                num += data.GetHashCode();
            }
            return num.GetHashCode();
        }

        public string GetMaxHeal(bool ShowType, bool CountWards)
        {
            MasterSwing swing = null;
            AttackType attackType = this.GetAttackType(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, DamageTypeDataOutgoingHealing);
            if (attackType != null)
            {
                for (int i = 0; i < attackType.Items.Count; i++)
                {
                    if ((CountWards || (attackType.Items[i].DamageType != "Absorption")) && ((swing == null) || (attackType.Items[i].Damage > swing.Damage)))
                    {
                        swing = attackType.Items[i];
                    }
                }
            }
            if (swing == null)
            {
                return string.Empty;
            }
            if (ShowType)
            {
                return string.Format("{0}-{1}", swing.AttackType, swing.Damage);
            }
            return string.Format("{0}", swing.Damage);
        }

        public long GetMaxHealth()
        {
            AttackType type;
            if (!this.AllInc.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
            {
                return (this.DamageTaken - this.HealsTaken);
            }
            List<MasterSwing> list = new List<MasterSwing>(type.Items);
            list.Sort(new Comparison<MasterSwing>(MasterSwing.CompareTime));
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (DamageSwingTypes.Contains(list[i].SwingType) && (list[i].Damage > 0))
                {
                    num2 -= list[i].Damage;
                }
                if (HealingSwingTypes.Contains(list[i].SwingType) && (list[i].Damage > 0))
                {
                    num2 += list[i].Damage;
                }
                if (num2 > 0)
                {
                    num2 = 0;
                }
                if (num2 < num)
                {
                    num = num2;
                }
            }
            return (long) Math.Abs(num);
        }

        public string GetMaxHit(bool ShowType)
        {
            MasterSwing swing = null;
            AttackType attackType = this.GetAttackType(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, DamageTypeDataOutgoingDamage);
            if (attackType != null)
            {
                for (int i = 0; i < attackType.Items.Count; i++)
                {
                    if ((swing == null) || (attackType.Items[i].Damage > swing.Damage))
                    {
                        swing = attackType.Items[i];
                    }
                }
            }
            if (swing == null)
            {
                return string.Empty;
            }
            if (ShowType)
            {
                return string.Format("{0}-{1}", swing.AttackType, swing.Damage);
            }
            return string.Format("{0}", swing.Damage);
        }

        public int GetThreatDelta(string DamageTypeDataLabel)
        {
            if (!this.threatCached)
            {
                AttackType type;
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                if (this.items[DamageTypeDataLabel].Items.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                {
                    for (int i = 0; i < type.Items.Count; i++)
                    {
                        MasterSwing swing = type.Items[i];
                        if (swing.Damage > 0)
                        {
                            if (swing.DamageType == "Increase")
                            {
                                num += swing.Damage;
                            }
                            else
                            {
                                num2 += swing.Damage;
                            }
                        }
                        else if (swing.Damage == Dnum.ThreatPosition)
                        {
                            int index = swing.Damage.DamageString.IndexOf(' ');
                            int num7 = int.Parse(swing.Damage.DamageString.Substring(0, index));
                            if (swing.DamageType == "Increase")
                            {
                                num3 += num7;
                            }
                            else
                            {
                                num4 += num7;
                            }
                        }
                    }
                }
                this.cThreatDelta = 0;
                this.cThreatDelta += num;
                this.cThreatDelta -= num2;
                this.threatCached = true;
                this.cThreatStr = string.Format("+({2}){0}/-({3}){1}", new object[] { num, num2, num3, num4 });
            }
            return this.cThreatDelta;
        }

        public string GetThreatStr(string DamageTypeDataLabel)
        {
            if (!this.threatCached)
            {
                AttackType type;
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                if (this.items[DamageTypeDataLabel].Items.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                {
                    for (int i = 0; i < type.Items.Count; i++)
                    {
                        MasterSwing swing = type.Items[i];
                        if (swing.Damage > 0)
                        {
                            if (swing.DamageType == "Increase")
                            {
                                num += swing.Damage;
                            }
                            else
                            {
                                num2 += swing.Damage;
                            }
                        }
                        else if (swing.Damage == Dnum.ThreatPosition)
                        {
                            int index = swing.Damage.DamageString.IndexOf(' ');
                            int num7 = int.Parse(swing.Damage.DamageString.Substring(0, index));
                            if (swing.DamageType == "Increase")
                            {
                                num3 += num7;
                            }
                            else
                            {
                                num4 += num7;
                            }
                        }
                    }
                }
                this.cThreatDelta = 0;
                this.cThreatDelta += num;
                this.cThreatDelta -= num2;
                this.threatCached = true;
                this.cThreatStr = string.Format("+({2}){0}/-({3}){1}", new object[] { num, num2, num3, num4 });
            }
            return this.cThreatStr;
        }

        public void ModAlly(string Combatant, int Mod)
        {
            if ((this.Name != "Unknown") && (Combatant != "UNKNOWN"))
            {
                SortedList<string, int> list;
                string str;
                if (!this.allies.ContainsKey(Combatant))
                {
                    this.allies.Add(Combatant, 0);
                }
                (list = this.allies)[str = Combatant] = list[str] + Mod;
                this.parent.SetAlliesUncached();
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public void Trim()
        {
            foreach (KeyValuePair<string, DamageTypeData> pair in this.items)
            {
                pair.Value.Trim();
            }
        }

        public SortedList<string, int> Allies
        {
            get
            {
                return this.allies;
            }
            set
            {
                this.allies = value;
            }
        }

        public SortedList<string, AttackType> AllInc
        {
            get
            {
                return this.incAll.Items;
            }
        }

        public SortedList<string, AttackType> AllOut
        {
            get
            {
                return this.outAll.Items;
            }
        }

        public int Blocked
        {
            get
            {
                return this.items[DamageTypeDataOutgoingDamage].Blocked;
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

        public float CritDamPerc
        {
            get
            {
                return this.items[DamageTypeDataOutgoingDamage].CritPerc;
            }
        }

        public float CritHealPerc
        {
            get
            {
                return this.items[DamageTypeDataOutgoingHealing].CritPerc;
            }
        }

        public int CritHeals
        {
            get
            {
                return this.items[DamageTypeDataOutgoingHealing].CritHits;
            }
        }

        public int CritHits
        {
            get
            {
                return this.items[DamageTypeDataOutgoingDamage].CritHits;
            }
        }

        public int CureDispels
        {
            get
            {
                return this.items["Cure/Dispel (Out)"].Swings;
            }
        }

        public long Damage
        {
            get
            {
                return this.items[DamageTypeDataOutgoingDamage].Damage;
            }
        }

        public string DamagePercent
        {
            get
            {
                string str = "--";
                if (this.parent.GetAllies().Contains(this))
                {
                    int num = (int) ((((float) this.Damage) / ((float) this.parent.Damage)) * 100f);
                    if ((num > -1) && (num < 0x65))
                    {
                        str = num.ToString() + "%";
                    }
                }
                return str;
            }
        }

        public long DamageTaken
        {
            get
            {
                return this.items[DamageTypeDataIncomingDamage].Damage;
            }
        }

        public int Deaths
        {
            get
            {
                if (!this.deathsCached)
                {
                    AttackType type;
                    if (!this.AllInc.TryGetValue("Killing", out type))
                    {
                        if (this.AllInc.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                        {
                            for (int i = 0; i < type.Items.Count; i++)
                            {
                                if (type.Items[i].Damage == Dnum.Death)
                                {
                                    this.cDeaths++;
                                }
                            }
                        }
                        this.cDeaths = 0;
                    }
                    else
                    {
                        this.cDeaths = type.Items.Count;
                    }
                    this.deathsCached = true;
                }
                return this.cDeaths;
            }
        }

        public double DPS
        {
            get
            {
                return (((double) this.Damage) / this.Duration.TotalSeconds);
            }
        }

        public TimeSpan Duration
        {
            get
            {
                if (this.parent.StartTimes.Count > 1)
                {
                    if (!this.durationCached)
                    {
                        AttackType type = null;
                        if (!this.AllOut.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type))
                        {
                            return TimeSpan.Zero;
                        }
                        type.Items.Sort(new Comparison<MasterSwing>(MasterSwing.CompareTime));
                        List<DateTime> list = new List<DateTime>();
                        List<DateTime> list2 = new List<DateTime>();
                        int num = 0;
                        int num2 = (this.parent.StartTimes.Count > this.parent.EndTimes.Count) ? this.parent.EndTimes.Count : this.parent.StartTimes.Count;
                        for (int i = 0; i < num2; i++)
                        {
                            if (num < 0)
                            {
                                num = 0;
                            }
                            if ((i >= this.parent.EndTimes.Count) || (this.parent.EndTimes[i] >= type.Items[num].Time))
                            {
                                for (int k = num; k < type.Items.Count; k++)
                                {
                                    if (list.Count == list2.Count)
                                    {
                                        if ((i == (this.parent.StartTimes.Count - 1)) && ((this.parent.EndTimes.Count + 1) == this.parent.StartTimes.Count))
                                        {
                                            if ((type.Items[k].Time >= this.parent.StartTimes[i]) && (type.Items[k].Time <= this.parent.EndTime))
                                            {
                                                list.Add(type.Items[k].Time);
                                                num = k;
                                            }
                                        }
                                        else if ((type.Items[k].Time >= this.parent.StartTimes[i]) && (type.Items[k].Time <= this.parent.EndTimes[i]))
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
                                            if ((this.parent.StartTimes.Count > (i + 1)) && (type.Items[m + 1].Time >= this.parent.StartTimes[i + 1]))
                                            {
                                                num = m - 1;
                                                break;
                                            }
                                        }
                                        list2.Add(swing.Time);
                                        break;
                                    }
                                    if ((i < this.parent.EndTimes.Count) && (type.Items[k].Time > this.parent.EndTimes[i]))
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
                            throw new Exception(string.Format("Personal Duration failure.  StartTimes: {0}/{2} EndTimes: {1}/{3}", new object[] { list.Count, list2.Count, this.parent.StartTimes.Count, this.parent.EndTimes.Count }));
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
                return (((double) this.Damage) / this.Parent.Duration.TotalSeconds);
            }
        }

        public DateTime EncEndTime
        {
            get
            {
                return this.parent.EndTime;
            }
        }

        public double EncHPS
        {
            get
            {
                double totalSeconds = this.parent.Duration.TotalSeconds;
                return (((double) this.Healed) / totalSeconds);
            }
        }

        public DateTime EncStartTime
        {
            get
            {
                return this.parent.StartTime;
            }
        }

        public DateTime EndTime
        {
            get
            {
                if (!this.endTimeCached)
                {
                    this.cEndTime = this.outAll.EndTime;
                    this.endTimeCached = true;
                }
                return this.cEndTime;
            }
        }

        public double ExtDPS
        {
            get
            {
                return this.EncDPS;
            }
        }

        public double ExtHPS
        {
            get
            {
                return this.EncHPS;
            }
        }

        public long Healed
        {
            get
            {
                return this.items[DamageTypeDataOutgoingHealing].Damage;
            }
        }

        public string HealedPercent
        {
            get
            {
                string str = "--";
                if (this.parent.GetAllies().Contains(this))
                {
                    int num = (int) ((((float) this.Healed) / ((float) this.parent.Healed)) * 100f);
                    if ((num > -1) && (num < 0x65))
                    {
                        str = num.ToString() + "%";
                    }
                }
                return str;
            }
        }

        public int Heals
        {
            get
            {
                return this.items[DamageTypeDataOutgoingHealing].Swings;
            }
        }

        public long HealsTaken
        {
            get
            {
                return this.items["Healed (Inc)"].Damage;
            }
        }

        public int Hits
        {
            get
            {
                return this.items[DamageTypeDataOutgoingDamage].Hits;
            }
        }

        public Dictionary<string, DamageTypeData> Items
        {
            get
            {
                return this.items;
            }
            set
            {
                this.items = value;
            }
        }

        public int Kills
        {
            get
            {
                if (!this.killsCached)
                {
                    AttackType type = null;
                    if (!this.AllOut.TryGetValue("Killing", out type))
                    {
                        this.AllOut.TryGetValue(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, out type);
                    }
                    if (type == null)
                    {
                        this.cKills = 0;
                        this.killsCached = true;
                        return this.cKills;
                    }
                    bool flag = (this.Parent != null) && !this.Parent.GetAllies(true).Contains(this);
                    this.cKills = 0;
                    for (int i = 0; i < type.Items.Count; i++)
                    {
                        if (type.Items[i].Damage == Dnum.Death)
                        {
                            if (flag || !type.Items[i].Victim.Contains(" "))
                            {
                                this.cKills++;
                            }
                            this.cKills++;
                        }
                    }
                    this.killsCached = true;
                }
                return this.cKills;
            }
        }

        public int Misses
        {
            get
            {
                return this.items[DamageTypeDataOutgoingDamage].Misses;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public EncounterData Parent
        {
            get
            {
                return this.parent;
            }
        }

        public long PowerDamage
        {
            get
            {
                return this.items["Power Drain (Out)"].Damage;
            }
        }

        public long PowerReplenish
        {
            get
            {
                return this.items["Power Replenish (Out)"].Damage;
            }
        }

        public DateTime ShortEndTime
        {
            get
            {
                return this.items[DamageTypeDataOutgoingDamage].EndTime;
            }
        }

        public DateTime StartTime
        {
            get
            {
                if (!this.startTimeCached)
                {
                    this.cStartTime = this.outAll.StartTime;
                    this.startTimeCached = true;
                }
                return this.cStartTime;
            }
        }

        public int Swings
        {
            get
            {
                return this.items[DamageTypeDataOutgoingDamage].Swings;
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

        public delegate Color ColorDataCallback(CombatantData Data);

        public class ColumnDef
        {
            private string dataName;
            private string dataType;
            private bool defaultVisible;
            public CombatantData.ColorDataCallback GetCellBackColor = Data => Color.Transparent;
            public CombatantData.StringDataCallback GetCellData;
            public CombatantData.ColorDataCallback GetCellForeColor = Data => Color.Transparent;
            public CombatantData.StringDataCallback GetSqlData;
            private string label;
            public Comparison<CombatantData> SortComparer;

            public ColumnDef(string Label, bool DefaultVisible, string SqlDataType, string SqlDataName, CombatantData.StringDataCallback CellDataCallback, CombatantData.StringDataCallback SqlDataCallback, Comparison<CombatantData> SortComparer)
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

        public class DamageTypeDef
        {
            private int allyValue;
            private string label;
            private Color typeColor;

            public DamageTypeDef(string Label, int AllyValue, Color TypeColor)
            {
                this.label = Label;
                this.allyValue = AllyValue;
                this.typeColor = TypeColor;
            }

            public int AllyValue
            {
                get
                {
                    return this.allyValue;
                }
            }

            public string Label
            {
                get
                {
                    return this.label;
                }
            }

            public Color TypeColor
            {
                get
                {
                    return this.typeColor;
                }
            }
        }

        public class DualComparison : IComparer<CombatantData>
        {
            private string sort1;
            private string sort2;

            public DualComparison(string Sort1, string Sort2)
            {
                this.sort1 = Sort1;
                this.sort2 = Sort2;
            }

            public int Compare(CombatantData Left, CombatantData Right)
            {
                int num = 0;
                if (CombatantData.ColumnDefs.ContainsKey(this.sort1))
                {
                    num = CombatantData.ColumnDefs[this.sort1].SortComparer(Left, Right);
                }
                if ((num == 0) && CombatantData.ColumnDefs.ContainsKey(this.sort2))
                {
                    num = CombatantData.ColumnDefs[this.sort2].SortComparer(Left, Right);
                }
                if (num == 0)
                {
                    num = Left.Damage.CompareTo(Right.Damage);
                }
                return num;
            }
        }

        public delegate string ExportStringDataCallback(CombatantData Data, string ExtraFormat);

        public delegate string StringDataCallback(CombatantData Data);

        public class TextExportFormatter
        {
            private string description;
            public CombatantData.ExportStringDataCallback GetExportString;
            private string label;
            private string name;

            public TextExportFormatter(string Name, string Label, string Description, CombatantData.ExportStringDataCallback FormatterCallback)
            {
                this.name = Name;
                this.label = Label;
                this.description = Description;
                this.GetExportString = FormatterCallback;
            }

            public string Description
            {
                get
                {
                    return this.description;
                }
            }

            public string Label
            {
                get
                {
                    return this.label;
                }
            }

            public string Name
            {
                get
                {
                    return this.name;
                }
            }
        }
    }
}

