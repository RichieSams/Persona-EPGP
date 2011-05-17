namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;

    public class SpellTimer
    {
        public Dictionary<string, string> ExtraInfo = new Dictionary<string, string>();
        private bool masterTimer;
        private DateTime startTime;
        private int timerDuration;
        private List<TimerMod> timerMods;

        public SpellTimer(bool MasterTimer, int TimerDuration, List<TimerMod> TimerMods, DateTime TimerStart)
        {
            this.masterTimer = MasterTimer;
            this.startTime = TimerStart;
            this.timerDuration = TimerDuration;
            this.timerMods = TimerMods;
        }

        public void RemoveModOwner(string DebuffOwner, DateTime DeathTime)
        {
            for (int i = this.timerMods.Count - 1; i >= 0; i--)
            {
                TimerMod mod = this.timerMods[i];
                if ((mod.Attacker == DebuffOwner.ToLower()) && ((DeathTime - this.StartTime) < TimeSpan.FromSeconds(2.0)))
                {
                    this.timerMods.RemoveAt(i);
                }
            }
        }

        public void RemoveModTarget(string DebuffTarget, DateTime DispellTime)
        {
            for (int i = this.timerMods.Count - 1; i >= 0; i--)
            {
                TimerMod mod = this.timerMods[i];
                if ((mod.Victim == DebuffTarget.ToLower()) && ((DispellTime - this.StartTime) < TimeSpan.FromSeconds(1.0)))
                {
                    this.timerMods.RemoveAt(i);
                }
            }
        }

        public bool MasterTimer
        {
            get
            {
                return this.masterTimer;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }
        }

        public int TimeLeft
        {
            get
            {
                TimeSpan span = (TimeSpan) (ActGlobals.oFormActMain.LastEstimatedTime - this.startTime);
                return (this.TimerFinalDuration - ((int) span.TotalSeconds));
            }
        }

        public int TimerFinalDuration
        {
            get
            {
                return (int) (this.timerDuration * (1f + this.TimerModValue));
            }
        }

        public float TimerModValue
        {
            get
            {
                float num = 0f;
                List<string> list = new List<string>();
                for (int i = 0; i < this.timerMods.Count; i++)
                {
                    TimerMod mod = this.timerMods[i];
                    if (!list.Contains(mod.ModName))
                    {
                        num += mod.ModValue;
                        list.Add(mod.ModName);
                    }
                }
                return num;
            }
        }
    }
}

