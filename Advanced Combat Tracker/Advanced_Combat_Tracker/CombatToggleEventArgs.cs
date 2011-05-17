namespace Advanced_Combat_Tracker
{
    using System;

    public class CombatToggleEventArgs : EventArgs
    {
        public readonly EncounterData encounter;
        public readonly int encounterDataIndex;
        public readonly int zoneDataIndex;

        public CombatToggleEventArgs(int zdIndex, int edIndex, EncounterData ed)
        {
            this.zoneDataIndex = zdIndex;
            this.encounterDataIndex = edIndex;
            this.encounter = ed;
        }
    }
}

