namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class EncounterData
    {
        private bool active;
        private bool alliesCached;
        private DateTime alliesLastCall;
        private List<CombatantData> cAllies;
        private string cEncId;
        private string charName;
        public static Dictionary<string, ColumnDef> ColumnDefs = new Dictionary<string, ColumnDef>();
        private SortedList<string, CombatantData> combatants;
        private bool encIdCached;
        private List<DateTime> endTimes;
        public static Dictionary<string, TextExportFormatter> ExportVariables = new Dictionary<string, TextExportFormatter>();
        private bool ignoreEnemies;
        private List<LogLineEntry> logLines;
        private ZoneData parent;
        private bool sParsing;
        private List<DateTime> startTimes;
        private Dictionary<string, object> tags;
        private string title;
        private string zoneName;

        public EncounterData(string CharName, string ZoneName, ZoneData Parent)
        {
            this.combatants = new SortedList<string, CombatantData>();
            this.title = "Encounter";
            this.logLines = new List<LogLineEntry>();
            this.alliesLastCall = DateTime.Now;
            this.startTimes = new List<DateTime>();
            this.endTimes = new List<DateTime>();
            this.tags = new Dictionary<string, object>();
            this.sParsing = false;
            this.ignoreEnemies = false;
            this.charName = CharName;
            this.zoneName = ZoneName;
            this.parent = Parent;
        }

        public EncounterData(string CharName, string ZoneName, bool IgnoreEnemies, ZoneData Parent)
        {
            this.combatants = new SortedList<string, CombatantData>();
            this.title = "Encounter";
            this.logLines = new List<LogLineEntry>();
            this.alliesLastCall = DateTime.Now;
            this.startTimes = new List<DateTime>();
            this.endTimes = new List<DateTime>();
            this.tags = new Dictionary<string, object>();
            this.sParsing = true;
            this.ignoreEnemies = IgnoreEnemies;
            this.charName = CharName;
            this.zoneName = ZoneName;
            this.parent = Parent;
        }

        public void AddCombatAction(MasterSwing action)
        {
            action.ParentEncounter = this;
            this.encIdCached = false;
            string player = action.Attacker.ToUpper();
            string str2 = action.Victim.ToUpper();
            if ((!this.sParsing || ActGlobals.oFormActMain.SelectiveListGetSelected(player)) || (ActGlobals.oFormActMain.SelectiveListGetSelected(str2) && !this.ignoreEnemies))
            {
                CombatantData data;
                if (!this.combatants.TryGetValue(player, out data))
                {
                    data = new CombatantData(action.Attacker, this);
                    this.combatants.Add(player, data);
                }
                data.AddCombatAction(action);
            }
            if ((!this.sParsing || ActGlobals.oFormActMain.SelectiveListGetSelected(str2)) || (ActGlobals.oFormActMain.SelectiveListGetSelected(str2) && !this.ignoreEnemies))
            {
                this.AddReverseCombatAction(action);
            }
        }

        private void AddReverseCombatAction(MasterSwing action)
        {
            CombatantData data;
            string key = action.Victim.ToUpper();
            if (!this.combatants.TryGetValue(key, out data))
            {
                data = new CombatantData(action.Victim, this);
                this.combatants.Add(key, data);
            }
            data.AddReverseCombatAction(action);
        }

        public void EndCombat(bool Finalize)
        {
            this.Active = false;
            this.EndTimes.Add(this.EndTime);
            if (Finalize)
            {
                this.Trim();
                this.Title = this.GetStrongestEnemy(ActGlobals.charName);
            }
        }

        public override bool Equals(object obj)
        {
            EncounterData data = (EncounterData) obj;
            string str = this.ToString();
            string str2 = data.ToString();
            return str.Equals(str2);
        }

        private string GermanTrimGrammar(string combatant)
        {
            string str;
            combatant = GermanTrimPrefix(combatant);
            do
            {
                str = combatant;
                if (combatant.EndsWith("en"))
                {
                    combatant = combatant.Substring(0, combatant.Length - 2);
                }
                if (combatant.EndsWith("er"))
                {
                    combatant = combatant.Substring(0, combatant.Length - 2);
                }
                if (combatant.EndsWith("e"))
                {
                    combatant = combatant.Substring(0, combatant.Length - 1);
                }
                if (combatant.EndsWith("s"))
                {
                    combatant = combatant.Substring(0, combatant.Length - 1);
                }
                if (combatant.EndsWith("'"))
                {
                    combatant = combatant.Substring(0, combatant.Length - 1);
                }
                if (combatant.EndsWith(" "))
                {
                    combatant = combatant.Substring(0, combatant.Length - 1);
                }
                combatant = combatant.Replace("en ", " ");
                combatant = combatant.Replace("er ", " ");
                combatant = combatant.Replace("e ", " ");
                combatant = combatant.Replace("s ", " ");
                combatant = combatant.Replace("  ", " ");
            }
            while (combatant != str);
            return combatant.Trim();
        }

        private static string GermanTrimPrefix(string combatant)
        {
            if (combatant.ToLower().StartsWith("der "))
            {
                combatant = combatant.Substring(4);
            }
            if (combatant.ToLower().StartsWith("des "))
            {
                combatant = combatant.Substring(4);
            }
            if (combatant.ToLower().StartsWith("das "))
            {
                combatant = combatant.Substring(4);
            }
            if (combatant.ToLower().StartsWith("dem "))
            {
                combatant = combatant.Substring(4);
            }
            if (combatant.ToLower().StartsWith("den "))
            {
                combatant = combatant.Substring(4);
            }
            if (combatant.ToLower().StartsWith("die "))
            {
                combatant = combatant.Substring(4);
            }
            if (combatant.ToLower().StartsWith("der "))
            {
                combatant = combatant.Substring(4);
            }
            if (combatant.ToLower().StartsWith("des "))
            {
                combatant = combatant.Substring(4);
            }
            if (combatant.ToLower().StartsWith("das "))
            {
                combatant = combatant.Substring(4);
            }
            if (combatant.ToLower().StartsWith("dem "))
            {
                combatant = combatant.Substring(4);
            }
            if (combatant.ToLower().StartsWith("den "))
            {
                combatant = combatant.Substring(4);
            }
            if (combatant.ToLower().StartsWith("die "))
            {
                combatant = combatant.Substring(4);
            }
            combatant = combatant.Trim();
            return combatant;
        }

        public List<CombatantData> GetAllies()
        {
            return this.GetAllies(false);
        }

        public List<CombatantData> GetAllies(bool allowLimited)
        {
            if ((!this.alliesCached && ((DateTime.Now.Second != this.alliesLastCall.Second) || !allowLimited)) && (((this.cAllies == null) || !this.Active) || (this.Title != ActGlobals.ActLocalization.LocalizationStrings["mergedEncounterTerm-all"].DisplayedText)))
            {
                if (this.GetIgnoreEnemies())
                {
                    return new List<CombatantData>(this.Items.Values);
                }
                CombatantData combatant = this.GetCombatant(this.CharName);
                if (combatant == null)
                {
                    return new List<CombatantData>();
                }
                SortedList<string, AllyObject> list = new SortedList<string, AllyObject>();
                list.Add(combatant.Name.ToUpper(), new AllyObject(combatant));
                bool flag = true;
                while (flag)
                {
                    flag = false;
                    for (int j = 0; j < list.Count; j++)
                    {
                        for (int k = 0; k < list.Values[j].cd.Allies.Count; k++)
                        {
                            string key = list.Values[j].cd.Allies.Keys[k];
                            int num3 = list.Values[j].cd.Allies.Values[k];
                            if (!list.ContainsKey(key))
                            {
                                CombatantData data2 = this.GetCombatant(key);
                                if (data2 == null)
                                {
                                    continue;
                                }
                                list.Add(key, new AllyObject(data2));
                                flag = true;
                            }
                            if (list.Values[j].allyVal > 0)
                            {
                                AllyObject local1 = list[key];
                                local1.allyVal += num3;
                            }
                            else
                            {
                                AllyObject local2 = list[key];
                                local2.allyVal -= num3;
                            }
                        }
                    }
                }
                List<CombatantData> list2 = new List<CombatantData>();
                bool flag2 = list[combatant.Name.ToUpper()].allyVal < 0;
                foreach (KeyValuePair<string, AllyObject> pair in list)
                {
                    if (flag2)
                    {
                        if (pair.Value.allyVal < 0)
                        {
                            list2.Add(pair.Value.cd);
                        }
                    }
                    else if (pair.Value.allyVal > 0)
                    {
                        list2.Add(pair.Value.cd);
                    }
                }
                for (int i = list2.Count - 1; i >= 0; i--)
                {
                    if (list2[i] == null)
                    {
                        list2.RemoveAt(i);
                    }
                }
                this.cAllies = list2;
                this.alliesCached = true;
                this.alliesLastCall = DateTime.Now;
            }
            return this.cAllies;
        }

        public string GetColumnByName(string name)
        {
            if (ColumnDefs.ContainsKey(name))
            {
                return ColumnDefs[name].GetCellData(this);
            }
            return string.Empty;
        }

        public CombatantData GetCombatant(string Name)
        {
            CombatantData data;
            if ((Name != null) && this.combatants.TryGetValue(Name.ToUpper(), out data))
            {
                return data;
            }
            return null;
        }

        public int GetEncounterSuccessLevel()
        {
            if (this.sParsing && this.ignoreEnemies)
            {
                return 0;
            }
            List<CombatantData> allies = this.GetAllies();
            if (allies.Count == 0)
            {
                return 0;
            }
            CombatantData combatant = this.GetCombatant(this.GetStrongestEnemy(this.charName));
            if (combatant == null)
            {
                return 0;
            }
            bool flag = false;
            if (combatant.Deaths > 0)
            {
                flag = true;
            }
            bool flag2 = false;
            for (int i = 0; i < allies.Count; i++)
            {
                CombatantData data2 = allies[i];
                if (((data2.Deaths == 0) && (data2.Name != "Unknown")) && !data2.Name.Contains(" "))
                {
                    flag2 = true;
                    break;
                }
            }
            if (flag && flag2)
            {
                return 1;
            }
            if (!flag && !flag2)
            {
                return 3;
            }
            return 2;
        }

        public override int GetHashCode()
        {
            long num = 0;
            List<CombatantData> list = new List<CombatantData>(this.Items.Values);
            for (int i = 0; i < list.Count; i++)
            {
                CombatantData data = list[i];
                num += data.GetHashCode();
            }
            return num.GetHashCode();
        }

        public bool GetIgnoreEnemies()
        {
            return this.ignoreEnemies;
        }

        public bool GetIsSelective()
        {
            return this.sParsing;
        }

        public string GetMaxHeal(bool ShowType, bool CountWards)
        {
            List<CombatantData> allies;
            if (this.ignoreEnemies)
            {
                allies = new List<CombatantData>(this.Items.Values);
            }
            else
            {
                allies = this.GetAllies();
            }
            MasterSwing swing = null;
            string name = string.Empty;
            for (int i = 0; i < allies.Count; i++)
            {
                CombatantData data = allies[i];
                AttackType attackType = data.GetAttackType(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, CombatantData.DamageTypeDataOutgoingHealing);
                if (attackType != null)
                {
                    for (int j = 0; j < attackType.Items.Count; j++)
                    {
                        if ((CountWards || (attackType.Items[j].DamageType != "Absorption")) && ((swing == null) || (attackType.Items[j].Damage > swing.Damage)))
                        {
                            swing = attackType.Items[j];
                            name = data.Name;
                        }
                    }
                }
            }
            if (swing == null)
            {
                return string.Empty;
            }
            if (ShowType)
            {
                return string.Format("{0}-{1}-{2}", name, swing.AttackType, swing.Damage);
            }
            return string.Format("{0}-{1}", name, swing.Damage);
        }

        public string GetMaxHit(bool ShowType)
        {
            List<CombatantData> allies;
            if (this.ignoreEnemies)
            {
                allies = new List<CombatantData>(this.Items.Values);
            }
            else
            {
                allies = this.GetAllies();
            }
            MasterSwing swing = null;
            string name = string.Empty;
            for (int i = 0; i < allies.Count; i++)
            {
                CombatantData data = allies[i];
                AttackType attackType = data.GetAttackType(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText, CombatantData.DamageTypeDataOutgoingDamage);
                if (attackType != null)
                {
                    for (int j = 0; j < attackType.Items.Count; j++)
                    {
                        if ((swing == null) || (attackType.Items[j].Damage > swing.Damage))
                        {
                            swing = attackType.Items[j];
                            name = data.Name;
                        }
                    }
                }
            }
            if (swing == null)
            {
                return string.Empty;
            }
            if (ShowType)
            {
                return string.Format("{0}-{1}-{2}", name, swing.AttackType, swing.Damage);
            }
            return string.Format("{0}-{1}", name, swing.Damage);
        }

        public string GetStrongestEnemy(string combatant)
        {
            if (this.sParsing && this.ignoreEnemies)
            {
                return "Encounter";
            }
            List<CombatantData> list = new List<CombatantData>(this.combatants.Values);
            List<CombatantData> allies = this.GetAllies();
            if (allies.Count == 0)
            {
                return "Encounter";
            }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                CombatantData item = list[i];
                if (allies.Contains(item))
                {
                    list.RemoveAt(i);
                }
            }
            List<StrFloat> list3 = new List<StrFloat>();
            for (int j = 0; j < list.Count; j++)
            {
                float damageTaken;
                CombatantData data2 = list[j];
                if (data2.Deaths > 0)
                {
                    damageTaken = data2.DamageTaken / ((long) data2.Deaths);
                }
                else
                {
                    damageTaken = data2.DamageTaken;
                }
                list3.Add(new StrFloat(data2.Name, damageTaken));
            }
            list3.Sort();
            list3.Reverse();
            if (list3.Count > 0)
            {
                return list3[0].Name;
            }
            return null;
        }

        public void SetAllies(List<CombatantData> allies)
        {
            this.cAllies = allies;
            this.alliesCached = true;
        }

        public void SetAlliesUncached()
        {
            this.alliesCached = false;
        }

        public override string ToString()
        {
            if (this.StartTime == DateTime.MaxValue)
            {
                return string.Format("{0} - [{1}]", this.Title, this.DurationS);
            }
            TimeSpan span = (TimeSpan) (DateTime.Now - this.StartTime);
            if (span.TotalHours > 12.0)
            {
                return string.Format("{0} - [{1}] ({3}) {2}", new object[] { this.Title, this.DurationS, this.StartTime.ToLongTimeString(), this.StartTime.ToShortDateString() });
            }
            return string.Format("{0} - [{1}] {2}", this.Title, this.DurationS, this.StartTime.ToLongTimeString());
        }

        public void Trim()
        {
            this.combatants.TrimExcess();
            for (int i = 0; i < this.combatants.Count; i++)
            {
                this.combatants.Values[i].Trim();
            }
        }

        public bool Active
        {
            get
            {
                return this.active;
            }
            set
            {
                this.active = value;
            }
        }

        public int AlliedDeaths
        {
            get
            {
                int num = 0;
                List<CombatantData> allies = null;
                if (this.ignoreEnemies)
                {
                    allies = new List<CombatantData>(this.Items.Values);
                }
                else
                {
                    allies = this.GetAllies();
                }
                for (int i = 0; i < allies.Count; i++)
                {
                    CombatantData data = allies[i];
                    if (!data.Name.Contains(" "))
                    {
                        num += data.Deaths;
                    }
                }
                return num;
            }
        }

        public int AlliedKills
        {
            get
            {
                int num = 0;
                List<CombatantData> allies = null;
                if (this.ignoreEnemies)
                {
                    allies = new List<CombatantData>(this.Items.Values);
                }
                else
                {
                    allies = this.GetAllies();
                }
                for (int i = 0; i < allies.Count; i++)
                {
                    CombatantData data = allies[i];
                    num += data.Kills;
                }
                return num;
            }
        }

        public string CharName
        {
            get
            {
                return this.charName;
            }
            set
            {
                this.charName = value;
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

        public long Damage
        {
            get
            {
                long num = 0;
                List<CombatantData> allies = null;
                if (this.ignoreEnemies)
                {
                    allies = new List<CombatantData>(this.Items.Values);
                }
                else
                {
                    allies = this.GetAllies();
                }
                for (int i = 0; i < allies.Count; i++)
                {
                    num += allies[i].Damage;
                }
                return num;
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
                if (this.StartTimes.Count > 1)
                {
                    try
                    {
                        TimeSpan span = new TimeSpan();
                        for (int i = 0; i < this.StartTimes.Count; i++)
                        {
                            if (this.EndTimes.Count == i)
                            {
                                span += this.EndTime - this.StartTimes[i];
                            }
                            else
                            {
                                span += this.EndTimes[i] - this.StartTimes[i];
                            }
                        }
                        return span;
                    }
                    catch
                    {
                        return TimeSpan.Zero;
                    }
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

        public string EncId
        {
            get
            {
                if (!this.encIdCached)
                {
                    this.cEncId = this.GetHashCode().ToString("x8");
                    this.encIdCached = true;
                }
                return this.cEncId;
            }
        }

        public DateTime EndTime
        {
            get
            {
                if (!ActGlobals.longDuration)
                {
                    return this.ShortEndTime;
                }
                DateTime minValue = DateTime.MinValue;
                for (int i = 0; i < this.combatants.Count; i++)
                {
                    CombatantData data = this.combatants.Values[i];
                    if (data.EndTime > minValue)
                    {
                        minValue = data.EndTime;
                    }
                }
                return minValue;
            }
        }

        public List<DateTime> EndTimes
        {
            get
            {
                int index = this.endTimes.IndexOf(DateTime.MinValue);
                if (index != -1)
                {
                    this.endTimes.RemoveAt(index);
                }
                return this.endTimes;
            }
            set
            {
                this.endTimes = value;
            }
        }

        public long Healed
        {
            get
            {
                long num = 0;
                List<CombatantData> allies = null;
                if (this.ignoreEnemies)
                {
                    allies = new List<CombatantData>(this.Items.Values);
                }
                else
                {
                    allies = this.GetAllies();
                }
                for (int i = 0; i < allies.Count; i++)
                {
                    CombatantData data = allies[i];
                    num += data.Healed;
                }
                return num;
            }
        }

        public SortedList<string, CombatantData> Items
        {
            get
            {
                return this.combatants;
            }
            set
            {
                this.combatants = value;
            }
        }

        public List<LogLineEntry> LogLines
        {
            get
            {
                return this.logLines;
            }
            set
            {
                this.logLines = value;
            }
        }

        public int NumAllies
        {
            get
            {
                return this.GetAllies().Count;
            }
        }

        public int NumCombatants
        {
            get
            {
                return this.Items.Count;
            }
        }

        public int NumEnemies
        {
            get
            {
                return (this.NumCombatants - this.NumAllies);
            }
        }

        public ZoneData Parent
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

        public DateTime ShortEndTime
        {
            get
            {
                DateTime minValue = DateTime.MinValue;
                List<CombatantData> allies = null;
                if (this.ignoreEnemies)
                {
                    allies = new List<CombatantData>(this.Items.Values);
                }
                else
                {
                    allies = this.GetAllies();
                }
                if (allies.Count == 0)
                {
                    allies = new List<CombatantData>(this.Items.Values);
                }
                for (int i = 0; i < allies.Count; i++)
                {
                    CombatantData data = allies[i];
                    if (data.ShortEndTime > minValue)
                    {
                        minValue = data.ShortEndTime;
                    }
                }
                return minValue;
            }
        }

        public DateTime StartTime
        {
            get
            {
                DateTime maxValue = DateTime.MaxValue;
                for (int i = 0; i < this.combatants.Count; i++)
                {
                    CombatantData data = this.combatants.Values[i];
                    if (data.StartTime < maxValue)
                    {
                        maxValue = data.StartTime;
                    }
                }
                return maxValue;
            }
        }

        public List<DateTime> StartTimes
        {
            get
            {
                int index = this.startTimes.IndexOf(DateTime.MaxValue);
                if (index != -1)
                {
                    this.startTimes.RemoveAt(index);
                }
                return this.startTimes;
            }
            set
            {
                this.startTimes = value;
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

        public string Title
        {
            get
            {
                if (this.zoneName == ActGlobals.ActLocalization.LocalizationStrings["mergedEncounterTerm-all"].DisplayedText)
                {
                    return ActGlobals.ActLocalization.LocalizationStrings["mergedEncounterTerm-all"].DisplayedText;
                }
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        public string ZoneName
        {
            get
            {
                if ((this.zoneName == ActGlobals.ActLocalization.LocalizationStrings["mergedEncounterTerm-all"].DisplayedText) && (this.parent != null))
                {
                    return this.parent.ZoneName;
                }
                return this.zoneName;
            }
            set
            {
                this.zoneName = value;
            }
        }

        private class AllyObject
        {
            public int allyVal;
            public CombatantData cd;

            public AllyObject(CombatantData combatant)
            {
                this.cd = combatant;
                this.allyVal = 0;
            }

            public override string ToString()
            {
                return this.allyVal.ToString();
            }
        }

        public delegate Color ColorDataCallback(EncounterData Data);

        public class ColumnDef
        {
            private string dataName;
            private string dataType;
            private bool defaultVisible;
            public EncounterData.ColorDataCallback GetCellBackColor = Data => Color.Transparent;
            public EncounterData.StringDataCallback GetCellData;
            public EncounterData.ColorDataCallback GetCellForeColor = Data => Color.Transparent;
            public EncounterData.StringDataCallback GetSqlData;
            private string label;

            public ColumnDef(string Label, bool DefaultVisible, string SqlDataType, string SqlDataName, EncounterData.StringDataCallback CellDataCallback, EncounterData.StringDataCallback SqlDataCallback)
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

        public delegate string ExportStringDataCallback(EncounterData Data, List<CombatantData> SelectiveAllies, string ExtraFormat);

        public delegate string StringDataCallback(EncounterData Data);

        public class TextExportFormatter
        {
            private string description;
            public EncounterData.ExportStringDataCallback GetExportString;
            private string label;
            private string name;

            public TextExportFormatter(string Name, string Label, string Description, EncounterData.ExportStringDataCallback FormatterCallback)
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

