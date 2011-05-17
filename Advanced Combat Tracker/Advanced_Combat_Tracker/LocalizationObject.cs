namespace Advanced_Combat_Tracker
{
    using System;

    public class LocalizationObject
    {
        private string displayedText;
        private string localizationDescription;

        public LocalizationObject(string DisplayedText, string LocalizationDescription)
        {
            this.displayedText = DisplayedText;
            this.localizationDescription = LocalizationDescription;
        }

        public string DisplayedText
        {
            get
            {
                return this.displayedText;
            }
            set
            {
                this.displayedText = value;
            }
        }

        public string LocalizationDescription
        {
            get
            {
                return this.localizationDescription;
            }
        }
    }
}

