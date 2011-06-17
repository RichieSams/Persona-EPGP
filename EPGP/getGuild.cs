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
        private String[] euroShards = {"Akala", "Argent", "Blightweald", "Bloodiron", "Brisesol", "Brutmutter", "Brutwacht", "Centius", "Cestus", "Cinderon", "Cloudborne", "Feenring", "Felsspitze", "Firesand", "Granitstaub", "Grimnir", "Heatherfield", "Icewatch", "Immerwacht", "Imperium", "Maidenfalls", "Mordant", "Overlook", "Phynnious", "Quarrystone", "Quicksilver", "Refuge", "Rhazade", "Riptalon", "Rubicon", "Sagespire", "Scarhide", "Shivermere", "Sparkwing", "Spross-Passage", "Steampike", "Tahkaat", "Tempete", "Trubkopf", "Whitefall", "Zareph"};
        private DataTable table = new DataTable();
        private String selectedGuildName = string.Empty;
        private String selectedShardID = string.Empty;
        private String selectedGuildID = string.Empty;
        private Boolean creating = false;

        public getGuild()
        {
            InitializeComponent();
        }

        private void getGuild_Load(object sender, EventArgs e)
        {
            String guildConString = "server=personaguild.com; User Id=persona_read; database=persona_pgm; Password=kr7OI=&J&,!F2BrHsC";
            guildConnection = new MySqlConnection(guildConString);
        }

        public void GetGuildInfo(out String guildName, out String shardID, out String guildID)
        {
            guildName = selectedGuildName;
            shardID = selectedShardID;
            guildID = selectedGuildID;
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
            if (!creating)
            {
                cb_guild.Items.Clear();
                cb_guild.Text = "Guild";
                table.Clear();
                selectedGuildName = string.Empty;
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

        private void cb_guild_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedGuildName = cb_guild.SelectedItem.ToString();
        }

        private void selectGuildButton_Click(object sender, EventArgs e)
        {
            if (selectedGuildName != string.Empty)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row["guildName"].ToString() == selectedGuildName)
                    {
                        selectedShardID = row["shardID"].ToString();
                        selectedGuildID = row["guildID"].ToString();
                    }
                }
                hiddenOKbutton.PerformClick();
            }
            else
            {
                MessageBox.Show("You must select a guild from the dropdown lists or create one", "No guild selected");
            }
        }

        private void createGuildPopupButton_Click(object sender, EventArgs e)
        {
            creating = true;
            cb_guild.Hide();
            selectGuildButton.Hide();
            createGuildPopupButton.Hide();
            txt_createGuildName.Show();
            createGuildButton.Show();
            this.Text = "Create guild";
        }

        private void createGuildButton_Click(object sender, EventArgs e)
        {
            selectedGuildName = txt_createGuildName.Text;
            if ((cb_usOrEuro.SelectedItem != null) && (cb_shard.SelectedItem != null))
            {
                if (selectedGuildName != string.Empty)
                {
                    if (selectedGuildName.All(c => Char.IsLetterOrDigit(c) || c == ' '))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        guildConnection.Open();
                        MySqlCommand findShardIDcommand = new MySqlCommand("SELECT shardID FROM shards WHERE shardName='" + cb_shard.SelectedItem.ToString() + "'", guildConnection);
                        adapter.SelectCommand = findShardIDcommand;
                        adapter.Fill(table);
                        selectedShardID = table.Rows[0]["shardID"].ToString();
                        MySqlCommand createGuildEntryCommand = new MySqlCommand("INSERT INTO guilds (guildName, shardID) VALUES ('" + selectedGuildName + "', '" + selectedShardID + "')", guildConnection);
                        createGuildEntryCommand.ExecuteNonQuery();
                        table.Clear();
                        MySqlCommand findGuildIDcommand = new MySqlCommand("SELECT guildID FROM guilds WHERE guildName='" + selectedGuildName + "'", guildConnection);
                        adapter.SelectCommand = findGuildIDcommand;
                        adapter.Fill(table);
                        selectedGuildID = table.Rows[0]["guildID"].ToString();
                        MySqlCommand createGuildTableCommand = new MySqlCommand("CREATE TABLE EPGP_" + selectedShardID + selectedGuildID + " LIKE EPGP_template", guildConnection);
                        createGuildTableCommand.ExecuteNonQuery();
                        guildConnection.Close();
                        hiddenOKbutton.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Guild name must only contain letters and/or spaces", "Invalid guild name");
                    }
                }
                else
                {
                    MessageBox.Show("You must enter a name for your guild", "No name entered");
                }
            }
            else
            {
                MessageBox.Show("You must select a shard", "No shard selected");
            }
        }

        private void txt_createGuildName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                createGuildButton.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        
    }
}
