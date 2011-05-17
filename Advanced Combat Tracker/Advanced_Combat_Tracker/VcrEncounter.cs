namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;

    public class VcrEncounter
    {
        private EncounterData encounter;
        private List<VcrCombatant> items;

        public VcrEncounter(EncounterData Encounter)
        {
            this.encounter = Encounter;
            this.items = new List<VcrCombatant>();
        }

        public VcrCombatant GetCombatant(string Name)
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.Items[i].Name.ToLower() == Name.ToLower())
                {
                    return this.Items[i];
                }
            }
            return null;
        }

        public int GetCombatantIndex(string Name)
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.Items[i].Name == Name)
                {
                    return i;
                }
            }
            return -1;
        }

        public EncounterData Encounter
        {
            get
            {
                return this.encounter;
            }
            set
            {
                this.encounter = value;
            }
        }

        public List<VcrCombatant> Items
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
    }
}

