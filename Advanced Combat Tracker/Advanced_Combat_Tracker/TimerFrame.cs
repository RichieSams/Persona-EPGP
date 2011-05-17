namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;

    public class TimerFrame : IEquatable<TimerFrame>, IComparable<TimerFrame>
    {
        private string combatant;
        private bool expireSounded;
        private Advanced_Combat_Tracker.TimerData timerData;
        private List<SpellTimer> timers = new List<SpellTimer>();
        private bool warningSounded;

        public TimerFrame(string Combatant, Advanced_Combat_Tracker.TimerData SpellTimerData)
        {
            this.combatant = Combatant;
            this.timerData = SpellTimerData;
        }

        public int CompareTo(TimerFrame other)
        {
            int num = 0;
            num = this.Combatant.CompareTo(other.Combatant);
            if (num == 0)
            {
                num = this.GetMostRecentTime(false).CompareTo(other.GetMostRecentTime(false));
            }
            return num;
        }

        public bool Equals(TimerFrame other)
        {
            return this.ToString().Equals(other.ToString());
        }

        public static string GetKey(string SpellName, string Combatant)
        {
            return string.Format("{0} - {1}", SpellName, Combatant);
        }

        public int GetLargestVal(bool IncludeNonMaster)
        {
            int timeLeft = -2147483648;
            for (int i = 0; i < this.timers.Count; i++)
            {
                if ((this.timers[i].MasterTimer || IncludeNonMaster) && (this.timers[i].TimeLeft > timeLeft))
                {
                    timeLeft = this.timers[i].TimeLeft;
                }
            }
            if (timeLeft == -2147483648)
            {
                return 0;
            }
            return timeLeft;
        }

        public DateTime GetMostRecentTime(bool IncludeNonMaster)
        {
            DateTime minValue = DateTime.MinValue;
            for (int i = 0; i < this.timers.Count; i++)
            {
                if ((this.timers[i].MasterTimer || IncludeNonMaster) && (this.timers[i].StartTime > minValue))
                {
                    minValue = this.timers[i].StartTime;
                }
            }
            return minValue;
        }

        public int GetSmallestVal(bool IncludeNonMaster)
        {
            int timeLeft = 0x7fffffff;
            for (int i = 0; i < this.timers.Count; i++)
            {
                if ((this.timers[i].MasterTimer || IncludeNonMaster) && (this.timers[i].TimeLeft < timeLeft))
                {
                    timeLeft = this.timers[i].TimeLeft;
                }
            }
            if (timeLeft == -2147483648)
            {
                return 0;
            }
            return timeLeft;
        }

        public void RemoveModOwner(string DebuffOwner, DateTime DeathTime)
        {
            for (int i = 0; i < this.timers.Count; i++)
            {
                if (this.timers[i].MasterTimer)
                {
                    this.timers[i].RemoveModOwner(DebuffOwner, DeathTime);
                }
            }
        }

        public void RemoveModTarget(string DebuffTarget, DateTime DispellTime)
        {
            for (int i = 0; i < this.timers.Count; i++)
            {
                if (this.timers[i].MasterTimer)
                {
                    this.timers[i].RemoveModTarget(DebuffTarget, DispellTime);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.Name, this.Combatant);
        }

        public string Combatant
        {
            get
            {
                return this.combatant;
            }
            set
            {
                this.combatant = value;
            }
        }

        public bool ExpireSounded
        {
            get
            {
                return this.expireSounded;
            }
            set
            {
                this.expireSounded = value;
            }
        }

        public bool MasterExists
        {
            get
            {
                for (int i = 0; i < this.timers.Count; i++)
                {
                    if (this.timers[i].MasterTimer)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public string Name
        {
            get
            {
                return this.timerData.Name;
            }
        }

        public bool OneOnly
        {
            get
            {
                return this.timerData.AbsoluteTiming;
            }
        }

        public bool RadialDisplay
        {
            get
            {
                return this.timerData.RadialDisplay;
            }
        }

        public bool RestrictToMe
        {
            get
            {
                return this.timerData.RestrictToMe;
            }
        }

        public List<SpellTimer> SpellTimers
        {
            get
            {
                return this.timers;
            }
            set
            {
                this.timers = value;
            }
        }

        public string StartSoundData
        {
            get
            {
                return this.timerData.StartSoundData;
            }
        }

        public Advanced_Combat_Tracker.TimerData TimerData
        {
            get
            {
                return this.timerData;
            }
            set
            {
                this.timerData = value;
            }
        }

        public int[] TimerVals
        {
            get
            {
                List<int> list = new List<int>(this.timers.Count);
                for (int i = 0; i < this.timers.Count; i++)
                {
                    list.Add(this.timers[i].TimeLeft);
                }
                list.Sort();
                return list.ToArray();
            }
        }

        public float TopModAmount
        {
            get
            {
                for (int i = this.timers.Count - 1; i >= 0; i--)
                {
                    if (this.timers[i].MasterTimer)
                    {
                        if (this.timers[i].TimerModValue != 0f)
                        {
                            return this.timers[i].TimerModValue;
                        }
                        return 0f;
                    }
                }
                return 0f;
            }
        }

        public string WarningSoundData
        {
            get
            {
                return this.timerData.WarningSoundData;
            }
        }

        public bool WarningSounded
        {
            get
            {
                return this.warningSounded;
            }
            set
            {
                this.warningSounded = value;
            }
        }

        public int WarningValue
        {
            get
            {
                return this.timerData.WarningValue;
            }
        }
    }
}

