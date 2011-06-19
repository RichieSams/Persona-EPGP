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
        #region Variables

        private MySqlConnection guildConnection;
        private String[] usShards = {"Aedraxis", "Akylios", "Alsbeth", "Amardis", "Arcanis", "Ashstone", "Asphodel", "Atrophinius", "Belmont", "Briarcliff", "Byriel", "Carrion", "Corthana", "Crucia", "Dayblind", "Deepstrike", "Deepwood", "Dimroot", "Emberlord", "Endless", "Epoch", "Estrael", "Faeblight", "Faemist", "Freeholme", "Galena", "Gnarlwood", "Greenscale", "Greybriar", "Hammerlord", "Harrow", "Kaleida", "Keenblade", "Kelpmere", "Laethys", "Lotham", "Millrush", "Molinar", "Neddra", "Nyx", "Perspice", "Plutonus", "Rasmolov", "Reclaimer", "Regulos", "Rocklift", "Seastone", "Shadefallen", "Shatterbone", "Silkweb", "Snarebrush", "Spitescar", "Stonecrest", "Sunrest", "Tearfall", "Threesprings", "Todrin", "Wolfsbane"};
        private String[] euroShards = {"Akala", "Argent", "Blightweald", "Bloodiron", "Brisesol", "Brutmutter", "Brutwacht", "Centius", "Cestus", "Cinderon", "Cloudborne", "Feenring", "Felsspitze", "Firesand", "Granitstaub", "Grimnir", "Heatherfield", "Icewatch", "Immerwacht", "Imperium", "Maidenfalls", "Mordant", "Overlook", "Phynnious", "Quarrystone", "Quicksilver", "Refuge", "Rhazade", "Riptalon", "Rubicon", "Sagespire", "Scarhide", "Shivermere", "Sparkwing", "Spross-Passage", "Steampike", "Tahkaat", "Tempete", "Trubkopf", "Whitefall", "Zareph"};
        private DataTable table = new DataTable();
        private String selectedGuildName = string.Empty;
        private String selectedShardID = string.Empty;
        private String selectedGuildID = string.Empty;
        private Boolean creating = false;
        private String login_name;

        private guildManagement gm;

        #endregion // Variables

        public getGuild(String mode, guildManagement gm, String name)
        {
            InitializeComponent();

            this.gm = gm;
            login_name = name;

            switch (mode)
            {
                case "ViewFind":
                    cb_guild.Show();
                    selectGuildButton.Show();
                    createGuildPopupButton.Show();
                    break;
                
                case "ModFind":
                    cb_guild.Show();
                    askAdminButton.Show();
                    createGuildPopupButton.Show();
                    break;

                case "Create":
                    txt_createGuildName.Show();
                    createGuildButton.Show();
                    creating = true;
                    this.Text = "Create guild";
                    break;
            }
        }

        private void getGuild_Load(object sender, EventArgs e)
        {
            // Set up MySQL connection
            String guildConString = "server=personaguild.com; User Id=persona_read; database=persona_pgm; Password=kr7OI=&J&,!F2BrHsC";
            guildConnection = new MySqlConnection(guildConString);
        }

        public void GetGuildInfo(out String guildName, out String shardID, out String guildID)
        {
            // Only use this method if a guild has already been selected or created, otherwise it will return empty strings.
            guildName = selectedGuildName;
            shardID = selectedShardID;
            guildID = selectedGuildID;
        }

        private void cb_usOrEuro_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear out the shard combobox and insert items according to the region selection
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
            // Don't bother with query if creating a guild since the guild combobox is hidden
            if (!creating)
            {
                // Clear out the guilds combobox, the datatable and any guild selection
                cb_guild.Items.Clear();
                cb_guild.Text = "Guild";
                table.Clear();
                selectedGuildName = string.Empty;
                // Find all the guilds on the specified shard
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                guildConnection.Open();
                MySqlCommand findGuildCommand = new MySqlCommand("SELECT g.guildName, g.shardID, g.guildID FROM guilds AS g INNER JOIN shards AS s ON g.shardID=s.shardID AND s.shardName='" + cb_shard.SelectedItem.ToString() + "'", guildConnection);
                adapter.SelectCommand = findGuildCommand;
                adapter.Fill(table);
                guildConnection.Close();

                if (table != null)
                {
                    // Add each guild name to the combobox
                    foreach (DataRow row in table.Rows)
                    {
                        cb_guild.Items.Add(row["guildName"]);
                    }
                }
            }
        }

        private void cb_guild_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Save the selection for later use
            selectedGuildName = cb_guild.SelectedItem.ToString();
        }

        private void selectGuildButton_Click(object sender, EventArgs e)
        {
            // If a guild is selected, save it's shardID and guildID, then return to the main app
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
            // Transform form to create a guild instead of pick one
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
            // Guild name validation
            selectedGuildName = txt_createGuildName.Text;
            if ((cb_usOrEuro.SelectedItem != null) && (cb_shard.SelectedItem != null))
            {
                if (selectedGuildName != string.Empty)
                {
                    if (selectedGuildName.Length < 26)
                    {
                        if (selectedGuildName.All(c => Char.IsLetterOrDigit(c) || c == ' '))
                        {
                                // Get the shardID of the selected shard
                                MySqlDataAdapter adapter = new MySqlDataAdapter();
                                guildConnection.Open();
                                MySqlCommand findShardIDcommand = new MySqlCommand("SELECT shardID FROM shards WHERE shardName='" + cb_shard.SelectedItem.ToString() + "'", guildConnection);
                                adapter.SelectCommand = findShardIDcommand;
                                adapter.Fill(table);
                                selectedShardID = table.Rows[0]["shardID"].ToString();
                                // Create the guild
                                MySqlCommand createGuildEntryCommand = new MySqlCommand("INSERT INTO guilds (guildName, shardID, admin) VALUES ('" + selectedGuildName + "', '" + selectedShardID + "', '" + login_name + "')", guildConnection);
                                createGuildEntryCommand.ExecuteNonQuery();
                                table.Clear();
                                // Get the guildID of the new guild
                                MySqlCommand findGuildIDcommand = new MySqlCommand("SELECT guildID FROM guilds WHERE guildName='" + selectedGuildName + "'", guildConnection);
                                adapter.SelectCommand = findGuildIDcommand;
                                adapter.Fill(table);
                                selectedGuildID = table.Rows[0]["guildID"].ToString();
                                // Create a database for the guild
                                MySqlCommand createGuildTableCommand = new MySqlCommand("CREATE TABLE EPGP_" + selectedShardID + selectedGuildID + " LIKE EPGP_template", guildConnection);
                                createGuildTableCommand.ExecuteNonQuery();
                                guildConnection.Close();
                                // Return to the main app
                                hiddenOKbutton.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("Guild name must only contain letters and/or spaces", "Invalid guild name");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Guild name must be less than 25 characters", "Too long");
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
