namespace Advanced_Combat_Tracker
{
    using System;
    using System.Drawing;

    public class TimerData : IEquatable<TimerData>
    {
        private bool absolute;
        private bool active;
        private string category;
        private Color fillColor;
        private bool modable;
        private string name;
        private bool onlyMasterTicks;
        private bool panel1;
        private bool panel2;
        private bool radial;
        private int removeValue;
        private bool restrictToCategory;
        private bool restrictToMe;
        private string startSoundData;
        private int timerValue;
        private string tooltip;
        private string warningSoundData;
        private int warningValue;

        public TimerData(string Name, string Category)
        {
            this.name = string.Empty;
            this.timerValue = 30;
            this.startSoundData = string.Empty;
            this.warningSoundData = string.Empty;
            this.warningValue = 10;
            this.radial = true;
            this.active = true;
            this.modable = true;
            this.tooltip = string.Empty;
            this.fillColor = Color.Blue;
            this.panel1 = true;
            this.removeValue = -15;
            this.category = " General";
            this.name = Name;
            this.category = Category;
        }

        public TimerData(string Name, bool OnlyMasterTicks, int TimerValue, bool RestrictToMe, bool AbsoluteTiming, string StartSoundData, string WarningSoundData, int WarningValue, bool RadialDisplay)
        {
            this.name = string.Empty;
            this.timerValue = 30;
            this.startSoundData = string.Empty;
            this.warningSoundData = string.Empty;
            this.warningValue = 10;
            this.radial = true;
            this.active = true;
            this.modable = true;
            this.tooltip = string.Empty;
            this.fillColor = Color.Blue;
            this.panel1 = true;
            this.removeValue = -15;
            this.category = " General";
            this.name = Name;
            this.timerValue = TimerValue;
            this.restrictToMe = RestrictToMe;
            this.absolute = AbsoluteTiming;
            this.startSoundData = StartSoundData;
            this.warningSoundData = WarningSoundData;
            this.warningValue = WarningValue;
            this.radial = RadialDisplay;
            this.onlyMasterTicks = OnlyMasterTicks;
            this.modable = true;
            this.tooltip = string.Format("{0} - {1}s.", Name, TimerValue);
        }

        public TimerData(string Name, bool OnlyMasterTicks, int TimerValue, bool RestrictToMe, bool AbsoluteTiming, string StartSoundData, string WarningSoundData, int WarningValue, bool RadialDisplay, bool Modable, string Tooltip)
        {
            this.name = string.Empty;
            this.timerValue = 30;
            this.startSoundData = string.Empty;
            this.warningSoundData = string.Empty;
            this.warningValue = 10;
            this.radial = true;
            this.active = true;
            this.modable = true;
            this.tooltip = string.Empty;
            this.fillColor = Color.Blue;
            this.panel1 = true;
            this.removeValue = -15;
            this.category = " General";
            this.name = Name;
            this.timerValue = TimerValue;
            this.restrictToMe = RestrictToMe;
            this.absolute = AbsoluteTiming;
            this.startSoundData = StartSoundData;
            this.warningSoundData = WarningSoundData;
            this.warningValue = WarningValue;
            this.radial = RadialDisplay;
            this.onlyMasterTicks = OnlyMasterTicks;
            this.modable = Modable;
            this.tooltip = Tooltip;
        }

        public TimerData(string Name, bool OnlyMasterTicks, int TimerValue, bool RestrictToMe, bool AbsoluteTiming, string StartSoundData, string WarningSoundData, int WarningValue, bool RadialDisplay, bool Modable, string Tooltip, Color FillColor)
        {
            this.name = string.Empty;
            this.timerValue = 30;
            this.startSoundData = string.Empty;
            this.warningSoundData = string.Empty;
            this.warningValue = 10;
            this.radial = true;
            this.active = true;
            this.modable = true;
            this.tooltip = string.Empty;
            this.fillColor = Color.Blue;
            this.panel1 = true;
            this.removeValue = -15;
            this.category = " General";
            this.name = Name;
            this.timerValue = TimerValue;
            this.restrictToMe = RestrictToMe;
            this.absolute = AbsoluteTiming;
            this.startSoundData = StartSoundData;
            this.warningSoundData = WarningSoundData;
            this.warningValue = WarningValue;
            this.radial = RadialDisplay;
            this.onlyMasterTicks = OnlyMasterTicks;
            this.modable = Modable;
            this.tooltip = Tooltip;
            this.fillColor = FillColor;
        }

        public TimerData(string Name, bool OnlyMasterTicks, int TimerValue, bool RestrictToMe, bool AbsoluteTiming, string StartSoundData, string WarningSoundData, int WarningValue, bool RadialDisplay, bool Modable, string Tooltip, Color FillColor, bool Panel1, bool Panel2)
        {
            this.name = string.Empty;
            this.timerValue = 30;
            this.startSoundData = string.Empty;
            this.warningSoundData = string.Empty;
            this.warningValue = 10;
            this.radial = true;
            this.active = true;
            this.modable = true;
            this.tooltip = string.Empty;
            this.fillColor = Color.Blue;
            this.panel1 = true;
            this.removeValue = -15;
            this.category = " General";
            this.name = Name;
            this.timerValue = TimerValue;
            this.restrictToMe = RestrictToMe;
            this.absolute = AbsoluteTiming;
            this.startSoundData = StartSoundData;
            this.warningSoundData = WarningSoundData;
            this.warningValue = WarningValue;
            this.radial = RadialDisplay;
            this.onlyMasterTicks = OnlyMasterTicks;
            this.modable = Modable;
            this.tooltip = Tooltip;
            this.fillColor = FillColor;
            this.panel1 = Panel1;
            this.panel2 = Panel2;
        }

        public bool Equals(TimerData other)
        {
            return this.Key.Equals(other.Key);
        }

        public override bool Equals(object obj)
        {
            TimerData data = (TimerData) obj;
            return this.Key.Equals(data.Key);
        }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }

        public override string ToString()
        {
            return string.Concat(new object[] { "[", this.timerValue, "] ", this.name });
        }

        public bool AbsoluteTiming
        {
            get
            {
                return this.absolute;
            }
            set
            {
                this.absolute = value;
            }
        }

        public bool ActiveInList
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

        public string Category
        {
            get
            {
                if (string.IsNullOrEmpty(this.category))
                {
                    return " General";
                }
                return this.category;
            }
            set
            {
                this.category = value;
            }
        }

        public Color FillColor
        {
            get
            {
                return this.fillColor;
            }
            set
            {
                this.fillColor = value;
            }
        }

        public string Key
        {
            get
            {
                return (this.Category.ToLower() + "|" + this.name.ToLower());
            }
        }

        public bool Modable
        {
            get
            {
                return this.modable;
            }
            set
            {
                this.modable = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public bool OnlyMasterTicks
        {
            get
            {
                return this.onlyMasterTicks;
            }
            set
            {
                this.onlyMasterTicks = value;
            }
        }

        public bool Panel1Display
        {
            get
            {
                return this.panel1;
            }
            set
            {
                this.panel1 = value;
            }
        }

        public bool Panel2Display
        {
            get
            {
                return this.panel2;
            }
            set
            {
                this.panel2 = value;
            }
        }

        public bool RadialDisplay
        {
            get
            {
                return this.radial;
            }
            set
            {
                this.radial = value;
            }
        }

        public int RemoveValue
        {
            get
            {
                return this.removeValue;
            }
            set
            {
                this.removeValue = value;
            }
        }

        public bool RestrictToCategory
        {
            get
            {
                return this.restrictToCategory;
            }
            set
            {
                this.restrictToCategory = value;
            }
        }

        public bool RestrictToMe
        {
            get
            {
                return this.restrictToMe;
            }
            set
            {
                this.restrictToMe = value;
            }
        }

        public string StartSoundData
        {
            get
            {
                return this.startSoundData;
            }
            set
            {
                this.startSoundData = value;
            }
        }

        public int TimerValue
        {
            get
            {
                return this.timerValue;
            }
            set
            {
                this.timerValue = value;
            }
        }

        public string Tooltip
        {
            get
            {
                return this.tooltip;
            }
            set
            {
                this.tooltip = value;
            }
        }

        public string WarningSoundData
        {
            get
            {
                return this.warningSoundData;
            }
            set
            {
                this.warningSoundData = value;
            }
        }

        public int WarningValue
        {
            get
            {
                return this.warningValue;
            }
            set
            {
                this.warningValue = value;
            }
        }
    }
}

