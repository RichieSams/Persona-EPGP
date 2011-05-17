namespace Advanced_Combat_Tracker
{
    using System;
    using System.Windows.Forms;

    public interface IActPluginV1
    {
        void DeInitPlugin();
        void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText);
    }
}

