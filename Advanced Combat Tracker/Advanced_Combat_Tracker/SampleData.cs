namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;

    public class SampleData
    {
        private List<StrFloat> dpsSamples;
        private DateTime start;

        public SampleData(DateTime StartTime)
        {
            this.start = StartTime;
            this.dpsSamples = new List<StrFloat>();
        }

        public List<StrFloat> DPSSamples
        {
            get
            {
                return this.dpsSamples;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return this.start;
            }
        }
    }
}

