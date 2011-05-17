namespace Advanced_Combat_Tracker
{
    using System;

    public class LogLineEventArgs : EventArgs
    {
        public readonly DateTime detectedTime;
        public int detectedType;
        public readonly string detectedZone;
        public readonly bool inCombat;
        public string logLine;

        public LogLineEventArgs(string LogLine, int DetectedType, DateTime DetectedTime, string DetectedZone, bool InCombat)
        {
            this.logLine = LogLine;
            this.detectedType = DetectedType;
            this.detectedTime = DetectedTime;
            this.detectedZone = DetectedZone;
            this.inCombat = InCombat;
        }
    }
}

