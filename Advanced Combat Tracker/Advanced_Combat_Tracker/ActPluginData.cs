namespace Advanced_Combat_Tracker
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    public class ActPluginData
    {
        public Button btnXButton;
        public CheckBox cbEnabled;
        public Label lblPluginStatus;
        public Label lblPluginTitle;
        public FileInfo pluginFile;
        public IActPluginV1 pluginObj;
        public string pluginVersion;
        public Panel pPluginInfo;
        public TabPage tpPluginSpace;
    }
}

