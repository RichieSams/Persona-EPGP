using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EPGP
{
    public partial class getGuild : Form
    {
        private MySqlConnection guildConnection;
        private String[] usShards = {"Aedraxis", "Akylios", "Alsbeth", "Amardis", "Arcanis", "Ashstone", "Asphodel", "Atrophinius", "Belmont", "Briarcliff", "Byriel", "Carrion", "Corthana", "Crucia", "Dayblind", "Deepstrike", "Deepwood", "Dimroot", "Emberlord", "Endless", "Epoch", "Estrael", "Faeblight", "Faemist", "Freeholme", "Galena", "Gnarlwood", "Greenscale", "Greybriar", "Hammerlord", "Harrow", "Kaleida", "Keenblade", "Kelpmere", "Laethys", "Lotham", "Millrush", "Molinar", "Neddra", "Nyx", "Perspice", "Plutonus", "Rasmolov", "Reclaimer", "Regulos", "Rocklift", "Seastone", "Shadefallen", "Shatterbone", "Silkweb", "Snarebrush", "Spitescar", "Stonecrest", "Sunrest", "Tearfall", "Threesprings", "Todrin", "Wolfsbane"};
        private String[] euroShards = { "Akala", "Argent", "Blightweald", "Bloodiron", "Brisesol", "Brutmutter", "Brutwacht", "Centius", "Cestus", "Cinderon", "Cloudborne", "Feenring", "Felsspitze", "Firesand", "Granitstaub", "Grimnir", "Heatherfield", "Icewatch", "Immerwacht", "Imperium", "Maidenfalls", "Mordant", "Overlook", "Phynnious", "Quarrystone", "Quicksilver", "Refuge", "Rhazade", "Riptalon", "Rubicon", "Sagespire", "Scarhide", "Shivermere", "Sparkwing", "Spross-Passage", "Steampike", "Tahkaat", "Tempête", "Trübkopf", "Whitefall", "Zareph" };
        private DataTable table = new DataTable();

        public getGuild()
        {
            InitializeComponent();
        }

        public string Guild
        {
            get 
            {
                string guildConString = "server=personaguild.com; User Id=persona_read; database=persona_pgm; Password=kr7OI=&J&,!F2BrHsC";
                guildConnection = new MySqlConnection(guildConString);
                MySqlCommand guildCommand = new MySqlCommand("");
                return cb_usOrEuro.Items[0].ToString();
            }
        }

        private void cb_usOrEuro_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_shard.Items.Clear();
            if (cb_usOrEuro.SelectedItem.ToString() == "US")
            {
                cb_shard.Items.AddRange(usShards);
            }
            else if (cb_usOrEuro.SelectedItem.ToString() == "European")
            {
                cb_shard.Items.AddRange(euroShards);
            }
        }

        private void cb_shard_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_guild.Items.Clear();
            table.Clear();
            string guildConString = "server=personaguild.com; User Id=persona_read; database=persona_pgm; Password=kr7OI=&J&,!F2BrHsC";
            guildConnection = new MySqlConnection(guildConString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            guildConnection.Open();
            MySqlCommand findGuildCommand = new MySqlCommand("SELECT g.guildName, g.shardID, g.guildID FROM guilds AS g INNER JOIN shards AS s ON g.shardID=s.shardID AND s.shardName='" + cb_shard.SelectedItem.ToString() + "'", guildConnection);
            adapter.SelectCommand = findGuildCommand;
            adapter.Fill(table);
            guildConnection.Close();

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    cb_guild.Items.Add(row["guildName"]);
                }
            }
        }
    }
}
