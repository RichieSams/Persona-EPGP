namespace Advanced_Combat_Tracker
{
    using System;

    public class TextExportFormatOptions : IEquatable<TextExportFormatOptions>
    {
        private string alliesFormat;
        private string playerFormat;
        private bool showAlliedInfo;
        private bool showOnlyAllies;
        private string sorting;

        public TextExportFormatOptions(string PlayerFormat, string Sorting, bool ShowOnlyAllies, bool ShowAlliedInfo, string AlliesFormat)
        {
            this.playerFormat = PlayerFormat;
            this.sorting = Sorting;
            this.showOnlyAllies = ShowOnlyAllies;
            this.showAlliedInfo = ShowAlliedInfo;
            this.alliesFormat = AlliesFormat;
        }

        public bool Equals(TextExportFormatOptions other)
        {
            return this.GetHashCode().Equals(other.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (obj == DBNull.Value)
            {
                return false;
            }
            TextExportFormatOptions other = (TextExportFormatOptions) obj;
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return string.Format("{0}{1}{2}{3}{4}", new object[] { this.playerFormat, this.alliesFormat, this.sorting, this.ShowOnlyAllies, this.showAlliedInfo }).GetHashCode();
        }

        public override string ToString()
        {
            return ("(" + this.sorting + ") \"" + this.playerFormat + "\"");
        }

        public string AlliesFormat
        {
            get
            {
                return this.alliesFormat;
            }
        }

        public string PlayerFormat
        {
            get
            {
                return this.playerFormat;
            }
        }

        public bool ShowAlliedInfo
        {
            get
            {
                return this.showAlliedInfo;
            }
        }

        public bool ShowOnlyAllies
        {
            get
            {
                return this.showOnlyAllies;
            }
        }

        public string Sorting
        {
            get
            {
                return this.sorting;
            }
        }
    }
}

