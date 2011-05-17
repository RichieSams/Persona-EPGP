namespace Advanced_Combat_Tracker
{
    using System;

    internal class MacroExportSetting
    {
        public bool Active = true;
        public bool AlignToContent;
        public string ExportChannel;
        public string ExportFile;
        public int ExportMaxLines;
        public int ExportPresetIndex;

        public MacroExportSetting(string ExportChannel, int ClipFormattingIndex, int MaximumExportedLines, bool AlignColumns, string ExportFile)
        {
            this.ExportChannel = ExportChannel.TrimStart(new char[] { '/', ' ' });
            this.ExportPresetIndex = ClipFormattingIndex;
            this.ExportMaxLines = MaximumExportedLines;
            this.AlignToContent = AlignColumns;
            this.ExportFile = ExportFile;
        }

        public override bool Equals(object obj)
        {
            if (obj == DBNull.Value)
            {
                return false;
            }
            return this.ToString().Equals(((MacroExportSetting) obj).ToString());
        }

        private string GetExportFormatterString()
        {
            try
            {
                if (this.ExportPresetIndex > -1)
                {
                    return ActGlobals.oFormActMain.TextExportFormats[this.ExportPresetIndex].ToString();
                }
                return "[Current Default]";
            }
            catch
            {
                return string.Empty;
            }
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} < {1} < {2}", this.ExportFile, this.ExportChannel, this.GetExportFormatterString());
        }
    }
}

