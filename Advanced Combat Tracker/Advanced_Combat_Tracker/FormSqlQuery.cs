namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Odbc;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class FormSqlQuery : Form
    {
        private Button btnClearText;
        private Button btnNonQuery;
        private Button btnQuery;
        private ContextMenuStrip cmsSqlCommand;
        private IContainer components;
        private DataSet dataset = new DataSet();
        private DataGridView dgv1;
        private Panel panel1;
        private RichTextBox rtbMessages;
        private RichTextBox rtbSql;
        private OdbcDataAdapter sqlAdapter;
        private OdbcConnection sqlConnection;
        private string sqlConnectionString = string.Empty;
        private TreeView tvSQL;

        public FormSqlQuery()
        {
            this.InitializeComponent();
        }

        private void btnClearSql_Click(object sender, EventArgs e)
        {
            this.rtbSql.Text = string.Empty;
            this.rtbMessages.Text = string.Empty;
            this.dgv1.DataSource = new object();
        }

        private void btnCommitTable_Click(object sender, EventArgs e)
        {
            OdbcCommandBuilder builder = new OdbcCommandBuilder(this.sqlAdapter);
            if (this.dataset.HasChanges())
            {
                DataSet changes = this.dataset.GetChanges();
                if (changes.HasErrors)
                {
                    foreach (DataRow row in changes.Tables[0].Rows)
                    {
                        if (row.HasErrors)
                        {
                            StringBuilder builder2 = new StringBuilder();
                            foreach (object obj2 in row.ItemArray)
                            {
                                builder2.AppendFormat("{0} |", obj2.ToString());
                            }
                            this.rtbMessages.AppendText(string.Format("\n{0} - {1} has the following error: {2} -- and has been ignored.", DateTime.Now.ToLongTimeString(), builder2.ToString(), row.RowError));
                            row.RejectChanges();
                        }
                    }
                }
                this.dataset.Merge(changes, true);
                builder.GetUpdateCommand();
                int num = this.sqlAdapter.Update(changes);
                this.dataset.AcceptChanges();
                this.rtbMessages.AppendText(string.Format("\n{0} - Rows changed: {1}", DateTime.Now.ToLongTimeString(), num));
            }
            else
            {
                this.rtbMessages.AppendText(string.Format("\n{0} - No Changes.", DateTime.Now.ToLongTimeString()));
            }
        }

        private void btnNonQuery_Click(object sender, EventArgs e)
        {
            this.cmsSqlCommand.Items.Add(this.rtbSql.Text);
            if (this.cmsSqlCommand.Items.Count > 10)
            {
                this.cmsSqlCommand.Items.RemoveAt(0);
            }
            this.sqlConnection = new OdbcConnection(this.sqlConnectionString);
            if (this.sqlConnection.State == ConnectionState.Closed)
            {
                this.sqlConnection.Open();
            }
            OdbcCommand command = this.sqlConnection.CreateCommand();
            command.CommandText = this.rtbSql.Text;
            int num = 0;
            try
            {
                num = command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                this.rtbMessages.AppendText(string.Format("\n{0} - {1}", DateTime.Now.ToLongTimeString(), exception.Message));
            }
            this.rtbMessages.AppendText(string.Format("\n{0} - Rows: {1}", DateTime.Now.ToLongTimeString(), num));
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.cmsSqlCommand.Items.Add(this.rtbSql.Text);
            if (this.cmsSqlCommand.Items.Count > 10)
            {
                this.cmsSqlCommand.Items.RemoveAt(0);
            }
            this.sqlConnection = new OdbcConnection(this.sqlConnectionString);
            if (this.sqlConnection.State == ConnectionState.Closed)
            {
                this.sqlConnection.Open();
            }
            int num = 0;
            try
            {
                this.sqlAdapter = new OdbcDataAdapter(this.rtbSql.Text, this.sqlConnection);
                this.dataset = new DataSet();
                num = this.sqlAdapter.Fill(this.dataset);
                this.dgv1.DataSource = this.dataset.Tables[0];
            }
            catch (Exception exception)
            {
                this.rtbMessages.AppendText(string.Format("\n{0} - {1}", DateTime.Now.ToLongTimeString(), exception.Message));
            }
            this.rtbMessages.AppendText(string.Format("\n{0} - Rows: {1}", DateTime.Now.ToLongTimeString(), num));
        }

        private void cmsSqlCommand_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.rtbSql.Text = e.ClickedItem.Text;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ExportControlTextXML(Stream Output)
        {
            XmlTextWriter xml = new XmlTextWriter(Output, Encoding.UTF8) {
                Formatting = Formatting.Indented,
                Indentation = 4,
                Namespaces = false
            };
            xml.WriteStartDocument();
            xml.WriteStartElement("", "ControlText", "");
            xml.WriteAttributeString("Form", "FormSqlQuery");
            ActGlobals.oFormActMain.ExportControlChilderenText(xml, this, "root");
            xml.WriteEndElement();
            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();
        }

        public void ExportControlTextXML(string FilePath)
        {
            this.ExportControlTextXML(new FileInfo(FilePath).OpenWrite());
        }

        private void Form13_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        public void ImportControlTextXML(Stream Input)
        {
            XmlTextReader reader = new XmlTextReader(Input);
            try
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        try
                        {
                            if (reader.LocalName == "Control")
                            {
                                bool found = false;
                                Control c = this;
                                string attribute = reader.GetAttribute("UniqueName");
                                string controlText = reader.GetAttribute("Text");
                                if (!ActGlobals.oFormActMain.ImportControlChilderenText(attribute, controlText, found, c))
                                {
                                    throw new ArgumentException(string.Format("Control {0} could not be located in the windows form.", attribute));
                                }
                            }
                            continue;
                        }
                        catch (Exception exception)
                        {
                            ActGlobals.oFormActMain.WriteExceptionLog(exception, string.Format(ActGlobals.ActLocalization.LocalizationStrings["messageBox-xmlReadError"].DisplayedText, reader.LineNumber, reader.LocalName, exception.Message));
                            continue;
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                MessageBox.Show(string.Format(ActGlobals.ActLocalization.LocalizationStrings["messageBox-xmlSyntaxError"].DisplayedText, exception2.Message), ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-xmlPrefError"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            reader.Close();
        }

        public void ImportControlTextXML(string FilePath)
        {
            this.ImportControlTextXML(new FileInfo(FilePath).OpenRead());
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            TreeNode node = new TreeNode("*");
            TreeNode node2 = new TreeNode("count(*)");
            TreeNode node3 = new TreeNode("SELECT * FROM encounter_table");
            TreeNode node4 = new TreeNode("SELECT", new TreeNode[] { node, node2, node3 });
            TreeNode node5 = new TreeNode("encid");
            TreeNode node6 = new TreeNode("title");
            TreeNode node7 = new TreeNode("starttime");
            TreeNode node8 = new TreeNode("endtime");
            TreeNode node9 = new TreeNode("duration");
            TreeNode node10 = new TreeNode("damage");
            TreeNode node11 = new TreeNode("encdps");
            TreeNode node12 = new TreeNode("zone");
            TreeNode node13 = new TreeNode("encounter_table", new TreeNode[] { node5, node6, node7, node8, node9, node10, node11, node12 });
            TreeNode node14 = new TreeNode("encid");
            TreeNode node15 = new TreeNode("name");
            TreeNode node16 = new TreeNode("ally");
            TreeNode node17 = new TreeNode("starttime");
            TreeNode node18 = new TreeNode("endtime");
            TreeNode node19 = new TreeNode("duration");
            TreeNode node20 = new TreeNode("damage");
            TreeNode node21 = new TreeNode("damageperc");
            TreeNode node22 = new TreeNode("kills");
            TreeNode node23 = new TreeNode("healed");
            TreeNode node24 = new TreeNode("healedperc");
            TreeNode node25 = new TreeNode("critheals");
            TreeNode node26 = new TreeNode("heals");
            TreeNode node27 = new TreeNode("powerdrain");
            TreeNode node28 = new TreeNode("dps");
            TreeNode node29 = new TreeNode("encdps");
            TreeNode node30 = new TreeNode("enchps");
            TreeNode node31 = new TreeNode("hits");
            TreeNode node32 = new TreeNode("crithits");
            TreeNode node33 = new TreeNode("blocked");
            TreeNode node34 = new TreeNode("misses");
            TreeNode node35 = new TreeNode("swings");
            TreeNode node36 = new TreeNode("healstaken");
            TreeNode node37 = new TreeNode("damagetaken");
            TreeNode node38 = new TreeNode("deaths");
            TreeNode node39 = new TreeNode("tohit");
            TreeNode node40 = new TreeNode("combatant_table", new TreeNode[] { 
                node14, node15, node16, node17, node18, node19, node20, node21, node22, node23, node24, node25, node26, node27, node28, node29, 
                node30, node31, node32, node33, node34, node35, node36, node37, node38, node39
             });
            TreeNode node41 = new TreeNode("encid");
            TreeNode node42 = new TreeNode("combatant");
            TreeNode node43 = new TreeNode("grouping");
            TreeNode node44 = new TreeNode("type");
            TreeNode node45 = new TreeNode("starttime");
            TreeNode node46 = new TreeNode("endtime");
            TreeNode node47 = new TreeNode("duration");
            TreeNode node48 = new TreeNode("damage");
            TreeNode node49 = new TreeNode("dps");
            TreeNode node50 = new TreeNode("average");
            TreeNode node51 = new TreeNode("median");
            TreeNode node52 = new TreeNode("minhit");
            TreeNode node53 = new TreeNode("maxhit");
            TreeNode node54 = new TreeNode("hits");
            TreeNode node55 = new TreeNode("crithits");
            TreeNode node56 = new TreeNode("blocked");
            TreeNode node57 = new TreeNode("misses");
            TreeNode node58 = new TreeNode("swings");
            TreeNode node59 = new TreeNode("tohit");
            TreeNode node60 = new TreeNode("averagedelay");
            TreeNode node61 = new TreeNode("damagetype_table", new TreeNode[] { 
                node41, node42, node43, node44, node45, node46, node47, node48, node49, node50, node51, node52, node53, node54, node55, node56, 
                node57, node58, node59, node60
             });
            TreeNode node62 = new TreeNode("encid");
            TreeNode node63 = new TreeNode("attacker");
            TreeNode node64 = new TreeNode("victim");
            TreeNode node65 = new TreeNode("swingtype");
            TreeNode node66 = new TreeNode("type");
            TreeNode node67 = new TreeNode("starttime");
            TreeNode node68 = new TreeNode("endtime");
            TreeNode node69 = new TreeNode("duration");
            TreeNode node70 = new TreeNode("damage");
            TreeNode node71 = new TreeNode("dps");
            TreeNode node72 = new TreeNode("average");
            TreeNode node73 = new TreeNode("median");
            TreeNode node74 = new TreeNode("minhit");
            TreeNode node75 = new TreeNode("maxhit");
            TreeNode node76 = new TreeNode("resist");
            TreeNode node77 = new TreeNode("hits");
            TreeNode node78 = new TreeNode("crithits");
            TreeNode node79 = new TreeNode("blocked");
            TreeNode node80 = new TreeNode("misses");
            TreeNode node81 = new TreeNode("swings");
            TreeNode node82 = new TreeNode("tohit");
            TreeNode node83 = new TreeNode("averagedelay");
            TreeNode node84 = new TreeNode("attacktype_table", new TreeNode[] { 
                node62, node63, node64, node65, node66, node67, node68, node69, node70, node71, node72, node73, node74, node75, node76, node77, 
                node78, node79, node80, node81, node82, node83
             });
            TreeNode node85 = new TreeNode("encid");
            TreeNode node86 = new TreeNode("time");
            TreeNode node87 = new TreeNode("attacker");
            TreeNode node88 = new TreeNode("swingtype");
            TreeNode node89 = new TreeNode("attacktype");
            TreeNode node90 = new TreeNode("victim");
            TreeNode node91 = new TreeNode("damagetype");
            TreeNode node92 = new TreeNode("damage");
            TreeNode node93 = new TreeNode("damagestring");
            TreeNode node94 = new TreeNode("critical");
            TreeNode node95 = new TreeNode("swing_table", new TreeNode[] { node85, node86, node87, node88, node89, node90, node91, node92, node93, node94 });
            TreeNode node96 = new TreeNode("FROM");
            TreeNode node97 = new TreeNode("swingtype = 1");
            TreeNode node98 = new TreeNode("swingtype = 2");
            TreeNode node99 = new TreeNode("swingtype < 3");
            TreeNode node100 = new TreeNode("swingtype = 3");
            TreeNode node101 = new TreeNode("swingtype = 10");
            TreeNode node102 = new TreeNode("encid = '########'");
            TreeNode node103 = new TreeNode("WHERE", new TreeNode[] { node97, node98, node99, node100, node101, node102 });
            TreeNode node104 = new TreeNode("AND");
            TreeNode node105 = new TreeNode("OR");
            TreeNode node106 = new TreeNode("ASC");
            TreeNode node107 = new TreeNode("DESC");
            TreeNode node108 = new TreeNode("ORDER BY", new TreeNode[] { node106, node107 });
            TreeNode node109 = new TreeNode("Right-Click to insert an SQL segment", new TreeNode[] { node4, node13, node40, node61, node84, node95, node96, node103, node104, node105, node108 });
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormSqlQuery));
            this.tvSQL = new TreeView();
            this.panel1 = new Panel();
            this.btnClearText = new Button();
            this.btnQuery = new Button();
            this.btnNonQuery = new Button();
            this.rtbMessages = new RichTextBox();
            this.rtbSql = new RichTextBox();
            this.cmsSqlCommand = new ContextMenuStrip(this.components);
            this.dgv1 = new DataGridView();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.dgv1).BeginInit();
            base.SuspendLayout();
            this.tvSQL.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tvSQL.Location = new Point(12, 12);
            this.tvSQL.Name = "tvSQL";
            node.Name = "Node2";
            node.Text = "*";
            node2.Name = "Node0";
            node2.Text = "count(*)";
            node3.Name = "Node3";
            node3.Text = "SELECT * FROM encounter_table";
            node4.Name = "Node1";
            node4.Text = "SELECT";
            node5.Name = "Node2";
            node5.Text = "encid";
            node6.Name = "Node3";
            node6.Text = "title";
            node7.Name = "Node4";
            node7.Text = "starttime";
            node8.Name = "Node5";
            node8.Text = "endtime";
            node9.Name = "Node6";
            node9.Text = "duration";
            node10.Name = "Node7";
            node10.Text = "damage";
            node11.Name = "Node8";
            node11.Text = "encdps";
            node12.Name = "Node9";
            node12.Text = "zone";
            node13.Name = "Node1";
            node13.Text = "encounter_table";
            node14.Name = "Node8";
            node14.Text = "encid";
            node15.Name = "Node9";
            node15.Text = "name";
            node16.Name = "Node10";
            node16.Text = "ally";
            node17.Name = "Node11";
            node17.Text = "starttime";
            node18.Name = "Node12";
            node18.Text = "endtime";
            node19.Name = "Node13";
            node19.Text = "duration";
            node20.Name = "Node14";
            node20.Text = "damage";
            node21.Name = "Node15";
            node21.Text = "damageperc";
            node22.Name = "Node33";
            node22.Text = "kills";
            node23.Name = "Node31";
            node23.Text = "healed";
            node24.Name = "Node32";
            node24.Text = "healedperc";
            node25.Name = "Node16";
            node25.Text = "critheals";
            node26.Name = "Node17";
            node26.Text = "heals";
            node27.Name = "Node18";
            node27.Text = "powerdrain";
            node28.Name = "Node19";
            node28.Text = "dps";
            node29.Name = "Node20";
            node29.Text = "encdps";
            node30.Name = "Node21";
            node30.Text = "enchps";
            node31.Name = "Node22";
            node31.Text = "hits";
            node32.Name = "Node23";
            node32.Text = "crithits";
            node33.Name = "Node24";
            node33.Text = "blocked";
            node34.Name = "Node25";
            node34.Text = "misses";
            node35.Name = "Node26";
            node35.Text = "swings";
            node36.Name = "Node27";
            node36.Text = "healstaken";
            node37.Name = "Node28";
            node37.Text = "damagetaken";
            node38.Name = "Node29";
            node38.Text = "deaths";
            node39.Name = "Node30";
            node39.Text = "tohit";
            node40.Name = "Node4";
            node40.Text = "combatant_table";
            node41.Name = "Node34";
            node41.Text = "encid";
            node42.Name = "Node35";
            node42.Text = "combatant";
            node43.Name = "Node36";
            node43.Text = "grouping";
            node44.Name = "Node37";
            node44.Text = "type";
            node45.Name = "Node38";
            node45.Text = "starttime";
            node46.Name = "Node39";
            node46.Text = "endtime";
            node47.Name = "Node40";
            node47.Text = "duration";
            node48.Name = "Node41";
            node48.Text = "damage";
            node49.Name = "Node42";
            node49.Text = "dps";
            node50.Name = "Node43";
            node50.Text = "average";
            node51.Name = "Node44";
            node51.Text = "median";
            node52.Name = "Node45";
            node52.Text = "minhit";
            node53.Name = "Node46";
            node53.Text = "maxhit";
            node54.Name = "Node47";
            node54.Text = "hits";
            node55.Name = "Node48";
            node55.Text = "crithits";
            node56.Name = "Node49";
            node56.Text = "blocked";
            node57.Name = "Node50";
            node57.Text = "misses";
            node58.Name = "Node51";
            node58.Text = "swings";
            node59.Name = "Node52";
            node59.Text = "tohit";
            node60.Name = "Node53";
            node60.Text = "averagedelay";
            node61.Name = "Node5";
            node61.Text = "damagetype_table";
            node62.Name = "Node54";
            node62.Text = "encid";
            node63.Name = "Node55";
            node63.Text = "attacker";
            node64.Name = "Node56";
            node64.Text = "victim";
            node65.Name = "Node57";
            node65.Text = "swingtype";
            node66.Name = "Node58";
            node66.Text = "type";
            node67.Name = "Node59";
            node67.Text = "starttime";
            node68.Name = "Node60";
            node68.Text = "endtime";
            node69.Name = "Node61";
            node69.Text = "duration";
            node70.Name = "Node62";
            node70.Text = "damage";
            node71.Name = "Node63";
            node71.Text = "dps";
            node72.Name = "Node64";
            node72.Text = "average";
            node73.Name = "Node65";
            node73.Text = "median";
            node74.Name = "Node66";
            node74.Text = "minhit";
            node75.Name = "Node67";
            node75.Text = "maxhit";
            node76.Name = "Node68";
            node76.Text = "resist";
            node77.Name = "Node69";
            node77.Text = "hits";
            node78.Name = "Node70";
            node78.Text = "crithits";
            node79.Name = "Node71";
            node79.Text = "blocked";
            node80.Name = "Node72";
            node80.Text = "misses";
            node81.Name = "Node73";
            node81.Text = "swings";
            node82.Name = "Node74";
            node82.Text = "tohit";
            node83.Name = "Node75";
            node83.Text = "averagedelay";
            node84.Name = "Node6";
            node84.Text = "attacktype_table";
            node85.Name = "Node76";
            node85.Text = "encid";
            node86.Name = "Node77";
            node86.Text = "time";
            node87.Name = "Node78";
            node87.Text = "attacker";
            node88.Name = "Node79";
            node88.Text = "swingtype";
            node89.Name = "Node80";
            node89.Text = "attacktype";
            node90.Name = "Node81";
            node90.Text = "victim";
            node91.Name = "Node82";
            node91.Text = "damagetype";
            node92.Name = "Node83";
            node92.Text = "damage";
            node93.Name = "Node84";
            node93.Text = "damagestring";
            node94.Name = "Node85";
            node94.Text = "critical";
            node95.Name = "Node7";
            node95.Text = "swing_table";
            node96.Name = "Node86";
            node96.Text = "FROM";
            node97.Name = "Node92";
            node97.Text = "swingtype = 1";
            node97.ToolTipText = "Melee hits";
            node98.Name = "Node93";
            node98.Text = "swingtype = 2";
            node98.ToolTipText = "Non-melee hits";
            node99.Name = "Node94";
            node99.Text = "swingtype < 3";
            node99.ToolTipText = "All Damage";
            node100.Name = "Node95";
            node100.Text = "swingtype = 3";
            node100.ToolTipText = "Healing";
            node101.Name = "Node96";
            node101.Text = "swingtype = 10";
            node101.ToolTipText = "Power Drain";
            node102.Name = "Node0";
            node102.Text = "encid = '########'";
            node103.Name = "Node87";
            node103.Text = "WHERE";
            node104.Name = "Node88";
            node104.Text = "AND";
            node105.Name = "Node97";
            node105.Text = "OR";
            node106.Name = "Node90";
            node106.Text = "ASC";
            node107.Name = "Node91";
            node107.Text = "DESC";
            node108.Name = "Node89";
            node108.Text = "ORDER BY";
            node109.Name = "Node0";
            node109.Text = "Right-Click to insert an SQL segment";
            this.tvSQL.Nodes.AddRange(new TreeNode[] { node109 });
            this.tvSQL.Size = new Size(0xdf, 0x1de);
            this.tvSQL.TabIndex = 0;
            this.tvSQL.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.tvSQL_NodeMouseClick);
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.panel1.Controls.Add(this.btnClearText);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.btnNonQuery);
            this.panel1.Controls.Add(this.rtbMessages);
            this.panel1.Controls.Add(this.rtbSql);
            this.panel1.Controls.Add(this.dgv1);
            this.panel1.Location = new Point(0xf1, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x22e, 0x1de);
            this.panel1.TabIndex = 1;
            this.btnClearText.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnClearText.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnClearText.Location = new Point(0x1db, 0x1c6);
            this.btnClearText.Name = "btnClearText";
            this.btnClearText.Size = new Size(0x53, 0x17);
            this.btnClearText.TabIndex = 3;
            this.btnClearText.Text = "Clear All";
            this.btnClearText.UseVisualStyleBackColor = true;
            this.btnClearText.Click += new EventHandler(this.btnClearSql_Click);
            this.btnQuery.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnQuery.Location = new Point(0x1db, 0x1b0);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new Size(0x53, 0x16);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.btnNonQuery.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnNonQuery.Location = new Point(0x1db, 410);
            this.btnNonQuery.Name = "btnNonQuery";
            this.btnNonQuery.Size = new Size(0x53, 0x16);
            this.btnNonQuery.TabIndex = 2;
            this.btnNonQuery.Text = "Non-Query";
            this.btnNonQuery.UseVisualStyleBackColor = true;
            this.btnNonQuery.Click += new EventHandler(this.btnNonQuery_Click);
            this.rtbMessages.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.rtbMessages.HideSelection = false;
            this.rtbMessages.Location = new Point(0, 0x14e);
            this.rtbMessages.Name = "rtbMessages";
            this.rtbMessages.ReadOnly = true;
            this.rtbMessages.Size = new Size(0x22e, 0x45);
            this.rtbMessages.TabIndex = 1;
            this.rtbMessages.Text = string.Empty;
            this.rtbSql.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.rtbSql.ContextMenuStrip = this.cmsSqlCommand;
            this.rtbSql.Font = new Font("Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.rtbSql.Location = new Point(0, 0x199);
            this.rtbSql.Name = "rtbSql";
            this.rtbSql.Size = new Size(0x1d4, 0x45);
            this.rtbSql.TabIndex = 1;
            this.rtbSql.Text = string.Empty;
            this.cmsSqlCommand.Name = "cmsSqlCommand";
            this.cmsSqlCommand.Size = new Size(0x3d, 4);
            this.cmsSqlCommand.ItemClicked += new ToolStripItemClickedEventHandler(this.cmsSqlCommand_ItemClicked);
            this.dgv1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.dgv1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Location = new Point(0, 0);
            this.dgv1.Name = "dgv1";
            this.dgv1.ReadOnly = true;
            this.dgv1.Size = new Size(0x22e, 0x148);
            this.dgv1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x32b, 0x1f6);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.tvSQL);
            this.DoubleBuffered = true;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "FormSqlQuery";
            this.Text = "FormSqlQuery";
            base.FormClosing += new FormClosingEventHandler(this.Form13_FormClosing);
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize) this.dgv1).EndInit();
            base.ResumeLayout(false);
        }

        public void ShowSqlView(string ConnectionString)
        {
            this.sqlConnectionString = ConnectionString;
            OdbcConnection connection = new OdbcConnection(this.sqlConnectionString);
            try
            {
                connection.Open();
                this.Text = string.Format("ServerVersion: {0}, Database: {1}, DataSource: {2}", connection.ServerVersion, connection.Database, connection.DataSource);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, ActGlobals.ActLocalization.LocalizationStrings["messageBoxTitle-odbcQueryError"].DisplayedText, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
            base.Show();
        }

        private void tvSQL_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.rtbSql.AppendText(string.Format("{0} ", e.Node.Text));
            }
            this.rtbSql.Focus();
        }
    }
}

