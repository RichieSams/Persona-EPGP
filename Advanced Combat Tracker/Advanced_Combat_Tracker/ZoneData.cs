namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;

    public class ZoneData : IComparable<ZoneData>
    {
        private EncounterData activeEncounter;
        private List<EncounterData> items;
        private bool populateAll;
        private DateTime startTime;
        private Dictionary<string, object> tags = new Dictionary<string, object>();
        private string zoneName;

        public ZoneData(DateTime Start, string ZoneName, bool PopulateAll, bool FullSelective, bool IgnoreEnemies)
        {
            this.startTime = Start;
            this.zoneName = ZoneName;
            this.items = new List<EncounterData>();
            this.populateAll = PopulateAll;
            if (this.populateAll)
            {
                if (FullSelective)
                {
                    this.items.Add(new EncounterData(ActGlobals.charName, ActGlobals.ActLocalization.LocalizationStrings["mergedEncounterTerm-all"].DisplayedText, IgnoreEnemies, this));
                }
                else
                {
                    this.items.Add(new EncounterData(ActGlobals.charName, ActGlobals.ActLocalization.LocalizationStrings["mergedEncounterTerm-all"].DisplayedText, this));
                }
            }
        }

        public void AddCombatAction(MasterSwing action)
        {
            if (this.populateAll)
            {
                if (!this.Items[0].Active)
                {
                    this.Items[0].StartTimes.Add(action.Time);
                    this.Items[0].Active = true;
                }
                this.Items[0].AddCombatAction(action);
            }
            if (!this.Items[this.Items.Count - 1].Active)
            {
                this.Items[this.Items.Count - 1].StartTimes.Add(action.Time);
                this.Items[this.Items.Count - 1].Active = true;
            }
            this.Items[this.Items.Count - 1].AddCombatAction(action);
        }

        public int CompareTo(ZoneData other)
        {
            return this.StartTime.CompareTo(other.StartTime);
        }

        public override string ToString()
        {
            if (this.ZoneName == "Import")
            {
                return string.Format("Import/Merge - [{0}]", this.Items.Count);
            }
            if (!this.populateAll)
            {
                return string.Format("{0} - [{2}] {1}", this.ZoneName, this.StartTime.ToLongTimeString(), this.Items.Count);
            }
            return string.Format("{0} - [{2}] {1}", this.ZoneName, this.StartTime.ToLongTimeString(), this.Items.Count - 1);
        }

        public EncounterData ActiveEncounter
        {
            get
            {
                return this.activeEncounter;
            }
            set
            {
                this.activeEncounter = value;
            }
        }

        public List<EncounterData> Items
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

        public bool PopulateAll
        {
            get
            {
                return this.populateAll;
            }
            set
            {
                this.populateAll = value;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }
            set
            {
                this.startTime = value;
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

        public string ZoneName
        {
            get
            {
                return this.zoneName;
            }
            set
            {
                this.zoneName = value;
            }
        }
    }
}

