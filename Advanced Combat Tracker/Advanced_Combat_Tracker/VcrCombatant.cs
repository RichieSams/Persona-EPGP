namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class VcrCombatant : IComparable<VcrCombatant>
    {
        private CombatantData combatant;
        private List<MasterSwing> itemsIn;
        private List<MasterSwing> itemsOut;
        private PointF location;
        private long maxHealth;
        private int positionMod;
        private int type;

        public VcrCombatant(CombatantData Combatant)
        {
            this.combatant = Combatant;
            this.type = Combatant.GetCombatantType();
            this.maxHealth = Combatant.GetMaxHealth();
            if (this.combatant.AllOut.ContainsKey(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
            {
                this.itemsOut = this.combatant.AllOut[ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].Items;
            }
            else
            {
                this.itemsOut = new List<MasterSwing>();
            }
            if (this.combatant.AllInc.ContainsKey(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
            {
                this.itemsIn = this.combatant.AllInc[ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].Items;
            }
            else
            {
                this.itemsIn = new List<MasterSwing>();
            }
        }

        public int CompareTo(VcrCombatant other)
        {
            return this.PositionMod.CompareTo(other.PositionMod);
        }

        public bool GetActive(DateTime Time, int UpToSecsAgo)
        {
            for (int i = 0; i < this.ItemsOut.Count; i++)
            {
                if ((this.ItemsOut[i].Time <= Time) && ((Time - this.ItemsOut[i].Time) <= TimeSpan.FromSeconds((double) UpToSecsAgo)))
                {
                    return true;
                }
            }
            return false;
        }

        public List<string> GetActiveTargets(DateTime Time, int UpToSecsAgo)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < this.ItemsOut.Count; i++)
            {
                if (((this.ItemsOut[i].Time <= Time) && ((Time - this.ItemsOut[i].Time) <= TimeSpan.FromSeconds((double) UpToSecsAgo))) && !list.Contains(this.ItemsOut[i].Victim))
                {
                    list.Add(this.ItemsOut[i].Victim);
                }
            }
            return list;
        }

        public int GetCompiledAmount(DateTime Time, int UpToSecsAgo, string Target, string DamageTypeData)
        {
            List<MasterSwing> list = new List<MasterSwing>();
            if (this.combatant.Items.ContainsKey(DamageTypeData) && this.combatant.Items[DamageTypeData].Items.ContainsKey(ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText))
            {
                list = new List<MasterSwing>(this.combatant.Items[DamageTypeData].Items[ActGlobals.ActLocalization.LocalizationStrings["attackTypeTerm-all"].DisplayedText].Items);
            }
            int num = 0;
            TimeSpan span = TimeSpan.FromSeconds((double) UpToSecsAgo);
            for (int i = 0; i < list.Count; i++)
            {
                if (((list[i].Victim == Target) && (list[i].Damage > 0)) && ((list[i].Time <= Time) && ((Time - list[i].Time) <= span)))
                {
                    num += list[i].Damage;
                }
            }
            return num;
        }

        public float GetDamageTaken(DateTime Time)
        {
            int num = 0;
            for (int i = 0; i < this.ItemsIn.Count; i++)
            {
                foreach (int num3 in CombatantData.DamageSwingTypes)
                {
                    if (((this.ItemsIn[i].SwingType == num3) && (this.ItemsIn[i].Damage > 0)) && (this.ItemsIn[i].Time <= Time))
                    {
                        num += this.ItemsIn[i].Damage;
                    }
                }
            }
            if ((this.DamageTaken != 0) && (num != 0))
            {
                return (((float) num) / ((float) this.DamageTaken));
            }
            return 0f;
        }

        public float GetHealsTaken(DateTime Time)
        {
            int num = 0;
            for (int i = 0; i < this.ItemsIn.Count; i++)
            {
                foreach (int num3 in CombatantData.HealingSwingTypes)
                {
                    if (((this.ItemsIn[i].SwingType == num3) && (this.ItemsIn[i].Damage > 0)) && (this.ItemsIn[i].Time <= Time))
                    {
                        num += this.ItemsIn[i].Damage;
                    }
                }
            }
            if ((this.HealsTaken != 0) && (num != 0))
            {
                return (((float) num) / ((float) this.HealsTaken));
            }
            return 0f;
        }

        public float GetHealth(DateTime Time)
        {
            long maxHealth = this.MaxHealth;
            for (int i = 0; i < this.ItemsIn.Count; i++)
            {
                if ((CombatantData.DamageSwingTypes.Contains(this.ItemsIn[i].SwingType) && (this.ItemsIn[i].Damage > 0)) && (this.ItemsIn[i].Time <= Time))
                {
                    maxHealth -= (long) this.ItemsIn[i].Damage;
                }
                if ((CombatantData.HealingSwingTypes.Contains(this.ItemsIn[i].SwingType) && (this.ItemsIn[i].Damage > 0)) && (this.ItemsIn[i].Time <= Time))
                {
                    maxHealth += (long) this.ItemsIn[i].Damage;
                }
                if (maxHealth > this.MaxHealth)
                {
                    maxHealth = this.MaxHealth;
                }
            }
            if ((this.MaxHealth != 0) && (maxHealth != 0))
            {
                return (((float) maxHealth) / ((float) this.MaxHealth));
            }
            return 0f;
        }

        public int GetInstancesAlive(DateTime Time)
        {
            int instances = this.Instances;
            for (int i = 0; i < this.ItemsIn.Count; i++)
            {
                if ((this.ItemsIn[i].Damage == Dnum.Death) && (this.ItemsIn[i].Time <= Time))
                {
                    instances--;
                }
            }
            return instances;
        }

        public long DamageTaken
        {
            get
            {
                return this.combatant.DamageTaken;
            }
        }

        public long HealsTaken
        {
            get
            {
                return this.combatant.HealsTaken;
            }
        }

        public int Instances
        {
            get
            {
                this.ItemsIn.Sort(new Comparison<MasterSwing>(MasterSwing.CompareTime));
                int num = 0;
                int num2 = 0;
                for (int i = 0; i < this.ItemsIn.Count; i++)
                {
                    if (this.ItemsIn[i].Damage == Dnum.Death)
                    {
                        num++;
                        num2 = i;
                    }
                }
                if (num2 < (this.ItemsIn.Count - 1))
                {
                    num++;
                }
                if (num == 0)
                {
                    num = 1;
                }
                return num;
            }
        }

        public List<MasterSwing> ItemsIn
        {
            get
            {
                return this.itemsIn;
            }
        }

        public List<MasterSwing> ItemsOut
        {
            get
            {
                return this.itemsOut;
            }
        }

        public PointF Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
            }
        }

        public long MaxHealth
        {
            get
            {
                return this.maxHealth;
            }
            set
            {
                this.maxHealth = value;
            }
        }

        public string Name
        {
            get
            {
                return this.combatant.Name;
            }
        }

        public int PositionMod
        {
            get
            {
                return this.positionMod;
            }
            set
            {
                this.positionMod = value;
            }
        }

        public int Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
    }
}

