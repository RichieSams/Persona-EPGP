namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Odbc;
    using System.Diagnostics;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    internal class Options_Odbc : UserControl
    {
        private Button btnOdbcDropTables;
        private Button btnOdbcTestConnection;
        private Button btnOdbcValidateTables;
        internal CheckBox cbCurrentOdbc;
        internal CheckBox cbExOdbc;
        internal CheckBox cbSqlSafeMode;
        private IContainer components;
        private GroupBox groupBox25;
        private Label label53;
        private Label label54;
        private LinkLabel linkLabelConnectionString;
        private LinkLabel linkLabelOdbcControl;
        internal NumericUpDown nudCOdbcDelay;
        internal RadioButton rbOdbcEx1;
        internal RadioButton rbOdbcEx2;
        internal RadioButton rbOdbcEx3;
        internal RadioButton rbOdbcEx4;
        internal RadioButton rbOdbcEx5;
        internal RichTextBox rtbOdbc;
        private TableLayoutPanel tableLayoutPanel1;
        internal TextBox tbOdbcConnectionString;
        private Dictionary<string, LocalizationObject> Trans = ActGlobals.ActLocalization.LocalizationStrings;

        public Options_Odbc()
        {
            this.InitializeComponent();
        }

        private void btnOdbcDropTables_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this.Trans["messageBox-odbcDropTables"].DisplayedText, this.Trans["messageBoxTitle-odbcDropTables"].DisplayedText, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    OdbcConnection odbcConnection = new OdbcConnection(this.tbOdbcConnectionString.Text);
                    odbcConnection.CreateCommand();
                    try
                    {
                        this.DropOdbcTable(odbcConnection, "encounter_table");
                        ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'encounter_table' dropped successfully.");
                    }
                    catch (OdbcException exception)
                    {
                        ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, exception.Message);
                        ActGlobals.oFormActMain.WriteExceptionLog(exception, ActGlobals.oFormActMain.lastSql);
                    }
                    try
                    {
                        this.DropOdbcTable(odbcConnection, "combatant_table");
                        ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'combatant_table' dropped successfully.");
                    }
                    catch (OdbcException exception2)
                    {
                        ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, exception2.Message);
                        ActGlobals.oFormActMain.WriteExceptionLog(exception2, ActGlobals.oFormActMain.lastSql);
                    }
                    try
                    {
                        this.DropOdbcTable(odbcConnection, "current_table");
                        ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'current_table' dropped successfully.");
                    }
                    catch (OdbcException exception3)
                    {
                        ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, exception3.Message);
                        ActGlobals.oFormActMain.WriteExceptionLog(exception3, ActGlobals.oFormActMain.lastSql);
                    }
                    try
                    {
                        this.DropOdbcTable(odbcConnection, "damagetype_table");
                        ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'damagetype_table' dropped successfully.");
                    }
                    catch (OdbcException exception4)
                    {
                        ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, exception4.Message);
                        ActGlobals.oFormActMain.WriteExceptionLog(exception4, ActGlobals.oFormActMain.lastSql);
                    }
                    try
                    {
                        this.DropOdbcTable(odbcConnection, "attacktype_table");
                        ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'attacktype_table' dropped successfully.");
                    }
                    catch (OdbcException exception5)
                    {
                        ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, exception5.Message);
                        ActGlobals.oFormActMain.WriteExceptionLog(exception5, ActGlobals.oFormActMain.lastSql);
                    }
                    try
                    {
                        this.DropOdbcTable(odbcConnection, "swing_table");
                        ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'swing_table' dropped successfully.");
                    }
                    catch (OdbcException exception6)
                    {
                        ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, exception6.Message);
                        ActGlobals.oFormActMain.WriteExceptionLog(exception6, ActGlobals.oFormActMain.lastSql);
                    }
                }
                catch (OdbcException exception7)
                {
                    ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, exception7.Message);
                    ActGlobals.oFormActMain.WriteExceptionLog(exception7, ActGlobals.oFormActMain.lastSql);
                }
                catch (Exception exception8)
                {
                    MessageBox.Show(exception8.Message, this.Trans["messageBoxTitle-error"].DisplayedText);
                }
                this.cbExOdbc.Checked = false;
                this.cbCurrentOdbc.Checked = false;
            }
        }

        private void btnOdbcTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                OdbcConnection selectConnection = new OdbcConnection(this.tbOdbcConnectionString.Text);
                OdbcDataAdapter adapter = new OdbcDataAdapter("SELECT 1;", selectConnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows[0].ItemArray[0].ToString() == "1")
                {
                    ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "ODBC Connection Succeeded");
                }
            }
            catch (OdbcException exception)
            {
                ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, exception.Message);
                ActGlobals.oFormActMain.WriteExceptionLog(exception, "ODBC Connection Test");
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, this.Trans["messageBoxTitle-error"].DisplayedText);
            }
        }

        private void btnOdbcValidateTables_Click(object sender, EventArgs e)
        {
            try
            {
                OdbcConnection odbcConnection = new OdbcConnection(this.tbOdbcConnectionString.Text);
                try
                {
                    this.ValidateOdbcTable(odbcConnection, "encounter_table", EncounterData.ColHeaderCollection, EncounterData.ColTypeCollection);
                    ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'encounter_table' validated successfully.");
                }
                catch (OdbcException)
                {
                    this.CreateOdbcTable(odbcConnection, "encounter_table", EncounterData.ColHeaderCollection, EncounterData.ColTypeCollection);
                    ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'encounter_table' created successfully.");
                }
                try
                {
                    this.ValidateOdbcTable(odbcConnection, "combatant_table", CombatantData.ColHeaderCollection, CombatantData.ColTypeCollection);
                    ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'combatant_table' validated successfully.");
                }
                catch (OdbcException)
                {
                    this.CreateOdbcTable(odbcConnection, "combatant_table", CombatantData.ColHeaderCollection, CombatantData.ColTypeCollection);
                    ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'combatant_table' created successfully.");
                }
                try
                {
                    this.ValidateOdbcTable(odbcConnection, "current_table", CombatantData.ColHeaderCollection, CombatantData.ColTypeCollection);
                    ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'current_table' validated successfully.");
                }
                catch (OdbcException)
                {
                    this.CreateOdbcTable(odbcConnection, "current_table", CombatantData.ColHeaderCollection, CombatantData.ColTypeCollection);
                    ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'current_table' created successfully.");
                }
                try
                {
                    this.ValidateOdbcTable(odbcConnection, "damagetype_table", DamageTypeData.ColHeaderCollection, DamageTypeData.ColTypeCollection);
                    ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'damagetype_table' validated successfully.");
                }
                catch (OdbcException)
                {
                    this.CreateOdbcTable(odbcConnection, "damagetype_table", DamageTypeData.ColHeaderCollection, DamageTypeData.ColTypeCollection);
                    ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'damagetype_table' created successfully.");
                }
                try
                {
                    this.ValidateOdbcTable(odbcConnection, "attacktype_table", AttackType.ColHeaderCollection, AttackType.ColTypeCollection);
                    ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'attacktype_table' validated successfully.");
                }
                catch (OdbcException)
                {
                    this.CreateOdbcTable(odbcConnection, "attacktype_table", AttackType.ColHeaderCollection, AttackType.ColTypeCollection);
                    ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'attacktype_table' created successfully.");
                }
                try
                {
                    this.ValidateOdbcTable(odbcConnection, "swing_table", MasterSwing.ColHeaderCollection, MasterSwing.ColTypeCollection);
                    ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'swing_table' validated successfully.");
                }
                catch (OdbcException)
                {
                    this.CreateOdbcTable(odbcConnection, "swing_table", MasterSwing.ColHeaderCollection, MasterSwing.ColTypeCollection);
                    ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, "'swing_table' created successfully.");
                }
            }
            catch (OdbcException exception)
            {
                ThreadInvokes.RichTextBoxAppendDateTimeLine(ActGlobals.oFormActMain, this.rtbOdbc, exception.Message);
                ActGlobals.oFormActMain.WriteExceptionLog(exception, ActGlobals.oFormActMain.lastSql);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, this.Trans["messageBoxTitle-error"].DisplayedText);
            }
        }

        private void control_MouseHover(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.control_MouseHover(sender, e);
        }

        private void CreateOdbcTable(OdbcConnection odbcConnection, string TableName, string[] ColHeaders, string[] ColTypes)
        {
            if (odbcConnection.State == ConnectionState.Closed)
            {
                odbcConnection.Open();
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0} {1}", ColHeaders[0].ToLower(), ColTypes[0]);
            for (int i = 1; i < ColHeaders.Length; i++)
            {
                builder.AppendFormat(", {0} {1}", ColHeaders[i].ToLower(), ColTypes[i]);
            }
            if (odbcConnection.ConnectionString.Contains("PostgreSQL"))
            {
                builder.Replace("INT1", "INT4");
                builder.Replace("INT3", "INT4");
            }
            if (odbcConnection.ConnectionString.Contains("Microsoft Access Driver"))
            {
                builder.Replace("INT1", "INT");
                builder.Replace("INT3", "INT");
                builder.Replace("INT4", "INT");
            }
            if ((odbcConnection.ConnectionString.Contains("{SQL Server}") || odbcConnection.ConnectionString.Contains("SQL Native Client")) || odbcConnection.ConnectionString.Contains("SQL Server Native Client"))
            {
                builder.Replace("INT1", "INT");
                builder.Replace("INT3", "INT");
                builder.Replace("INT4", "INT");
                builder.Replace("FLOAT4", "FLOAT");
                builder.Replace("TIMESTAMP", "DATETIME");
            }
            OdbcCommand command = odbcConnection.CreateCommand();
            command.CommandText = string.Format("CREATE TABLE {0} ({1});", TableName, builder.ToString());
            ActGlobals.oFormActMain.lastSql = command.CommandText;
            command.ExecuteNonQuery();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DropOdbcTable(OdbcConnection odbcConnection, string TableName)
        {
            if (odbcConnection.State == ConnectionState.Closed)
            {
                odbcConnection.Open();
            }
            OdbcCommand command = odbcConnection.CreateCommand();
            command.CommandText = string.Format("DROP TABLE {0};", TableName);
            ActGlobals.oFormActMain.lastSql = command.CommandText;
            command.ExecuteNonQuery();
        }

        private void InitializeComponent()
        {
            this.groupBox25 = new GroupBox();
            this.cbCurrentOdbc = new CheckBox();
            this.nudCOdbcDelay = new NumericUpDown();
            this.linkLabelOdbcControl = new LinkLabel();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.btnOdbcTestConnection = new Button();
            this.btnOdbcValidateTables = new Button();
            this.btnOdbcDropTables = new Button();
            this.label54 = new Label();
            this.rbOdbcEx5 = new RadioButton();
            this.rbOdbcEx4 = new RadioButton();
            this.rbOdbcEx3 = new RadioButton();
            this.rbOdbcEx2 = new RadioButton();
            this.rbOdbcEx1 = new RadioButton();
            this.rtbOdbc = new RichTextBox();
            this.linkLabelConnectionString = new LinkLabel();
            this.tbOdbcConnectionString = new TextBox();
            this.label53 = new Label();
            this.cbSqlSafeMode = new CheckBox();
            this.cbExOdbc = new CheckBox();
            this.groupBox25.SuspendLayout();
            this.nudCOdbcDelay.BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox25.Controls.Add(this.cbCurrentOdbc);
            this.groupBox25.Controls.Add(this.nudCOdbcDelay);
            this.groupBox25.Controls.Add(this.linkLabelOdbcControl);
            this.groupBox25.Controls.Add(this.tableLayoutPanel1);
            this.groupBox25.Controls.Add(this.label54);
            this.groupBox25.Controls.Add(this.rbOdbcEx5);
            this.groupBox25.Controls.Add(this.rbOdbcEx4);
            this.groupBox25.Controls.Add(this.rbOdbcEx3);
            this.groupBox25.Controls.Add(this.rbOdbcEx2);
            this.groupBox25.Controls.Add(this.rbOdbcEx1);
            this.groupBox25.Controls.Add(this.rtbOdbc);
            this.groupBox25.Controls.Add(this.linkLabelConnectionString);
            this.groupBox25.Controls.Add(this.tbOdbcConnectionString);
            this.groupBox25.Controls.Add(this.label53);
            this.groupBox25.Controls.Add(this.cbSqlSafeMode);
            this.groupBox25.Controls.Add(this.cbExOdbc);
            this.groupBox25.Location = new Point(3, 3);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Size = new Size(0x2cd, 0xc1);
            this.groupBox25.TabIndex = 4;
            this.groupBox25.TabStop = false;
            this.groupBox25.Text = "ODBC Database";
            this.groupBox25.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbCurrentOdbc.Location = new Point(8, 0x8e);
            this.cbCurrentOdbc.Name = "cbCurrentOdbc";
            this.cbCurrentOdbc.Size = new Size(0x128, 0x10);
            this.cbCurrentOdbc.TabIndex = 8;
            this.cbCurrentOdbc.Text = "Export a table of the current encounter every N seconds.";
            this.cbCurrentOdbc.MouseHover += new EventHandler(this.control_MouseHover);
            this.nudCOdbcDelay.Location = new Point(310, 0x8b);
            int[] bits = new int[4];
            bits[0] = 120;
            this.nudCOdbcDelay.Maximum = new decimal(bits);
            this.nudCOdbcDelay.Name = "nudCOdbcDelay";
            this.nudCOdbcDelay.Size = new Size(0x30, 20);
            this.nudCOdbcDelay.TabIndex = 9;
            int[] numArray2 = new int[4];
            numArray2[0] = 10;
            this.nudCOdbcDelay.Value = new decimal(numArray2);
            this.linkLabelOdbcControl.AutoSize = true;
            this.linkLabelOdbcControl.Location = new Point(0x61, 0);
            this.linkLabelOdbcControl.Name = "linkLabelOdbcControl";
            this.linkLabelOdbcControl.Size = new Size(0x87, 13);
            this.linkLabelOdbcControl.TabIndex = 0;
            this.linkLabelOdbcControl.TabStop = true;
            this.linkLabelOdbcControl.Text = "Open ODBC Control Applet";
            this.linkLabelOdbcControl.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabelOdbcControl_LinkClicked);
            this.linkLabelOdbcControl.MouseHover += new EventHandler(this.control_MouseHover);
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.87359f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 41.93661f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.1898f));
            this.tableLayoutPanel1.Controls.Add(this.btnOdbcTestConnection, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnOdbcValidateTables, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnOdbcDropTables, 2, 0);
            this.tableLayoutPanel1.Location = new Point(310, 0x26);
            this.tableLayoutPanel1.Margin = new Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Size = new Size(0x191, 0x1b);
            this.tableLayoutPanel1.TabIndex = 0x36;
            this.btnOdbcTestConnection.BackColor = SystemColors.Control;
            this.btnOdbcTestConnection.Dock = DockStyle.Fill;
            this.btnOdbcTestConnection.Location = new Point(0, 0);
            this.btnOdbcTestConnection.Margin = new Padding(0, 0, 0, 3);
            this.btnOdbcTestConnection.Name = "btnOdbcTestConnection";
            this.btnOdbcTestConnection.Size = new Size(0x7b, 0x18);
            this.btnOdbcTestConnection.TabIndex = 0;
            this.btnOdbcTestConnection.Text = "Test Connection";
            this.btnOdbcTestConnection.UseVisualStyleBackColor = false;
            this.btnOdbcTestConnection.Click += new EventHandler(this.btnOdbcTestConnection_Click);
            this.btnOdbcTestConnection.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnOdbcValidateTables.BackColor = SystemColors.Control;
            this.btnOdbcValidateTables.Dock = DockStyle.Fill;
            this.btnOdbcValidateTables.Location = new Point(0x7b, 0);
            this.btnOdbcValidateTables.Margin = new Padding(0, 0, 0, 3);
            this.btnOdbcValidateTables.Name = "btnOdbcValidateTables";
            this.btnOdbcValidateTables.Size = new Size(0xa8, 0x18);
            this.btnOdbcValidateTables.TabIndex = 1;
            this.btnOdbcValidateTables.Text = "Validate Table Setup";
            this.btnOdbcValidateTables.UseVisualStyleBackColor = false;
            this.btnOdbcValidateTables.Click += new EventHandler(this.btnOdbcValidateTables_Click);
            this.btnOdbcValidateTables.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnOdbcDropTables.BackColor = SystemColors.Control;
            this.btnOdbcDropTables.Dock = DockStyle.Fill;
            this.btnOdbcDropTables.Location = new Point(0x123, 0);
            this.btnOdbcDropTables.Margin = new Padding(0, 0, 0, 3);
            this.btnOdbcDropTables.Name = "btnOdbcDropTables";
            this.btnOdbcDropTables.Size = new Size(110, 0x18);
            this.btnOdbcDropTables.TabIndex = 2;
            this.btnOdbcDropTables.Text = "Drop Tables";
            this.btnOdbcDropTables.UseVisualStyleBackColor = false;
            this.btnOdbcDropTables.Click += new EventHandler(this.btnOdbcDropTables_Click);
            this.btnOdbcDropTables.MouseHover += new EventHandler(this.control_MouseHover);
            this.label54.AutoSize = true;
            this.label54.Location = new Point(4, 0x29);
            this.label54.Name = "label54";
            this.label54.Size = new Size(0xa1, 13);
            this.label54.TabIndex = 90;
            this.label54.Text = "Select the database table depth:";
            this.label54.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbOdbcEx5.AutoSize = true;
            this.rbOdbcEx5.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.rbOdbcEx5.Location = new Point(8, 120);
            this.rbOdbcEx5.Name = "rbOdbcEx5";
            this.rbOdbcEx5.Size = new Size(0xd1, 0x10);
            this.rbOdbcEx5.TabIndex = 7;
            this.rbOdbcEx5.Text = "Export down to specific swings (large DB req)";
            this.rbOdbcEx5.UseVisualStyleBackColor = true;
            this.rbOdbcEx5.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbOdbcEx4.AutoSize = true;
            this.rbOdbcEx4.Checked = true;
            this.rbOdbcEx4.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.rbOdbcEx4.Location = new Point(8, 0x68);
            this.rbOdbcEx4.Name = "rbOdbcEx4";
            this.rbOdbcEx4.Size = new Size(0xa2, 0x10);
            this.rbOdbcEx4.TabIndex = 6;
            this.rbOdbcEx4.TabStop = true;
            this.rbOdbcEx4.Text = "Export down to AttackType tables";
            this.rbOdbcEx4.UseVisualStyleBackColor = true;
            this.rbOdbcEx4.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbOdbcEx3.AutoSize = true;
            this.rbOdbcEx3.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.rbOdbcEx3.Location = new Point(8, 0x58);
            this.rbOdbcEx3.Name = "rbOdbcEx3";
            this.rbOdbcEx3.Size = new Size(0xc1, 0x10);
            this.rbOdbcEx3.TabIndex = 5;
            this.rbOdbcEx3.Text = "Export down to combatant DamageTypes";
            this.rbOdbcEx3.UseVisualStyleBackColor = true;
            this.rbOdbcEx3.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbOdbcEx2.AutoSize = true;
            this.rbOdbcEx2.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.rbOdbcEx2.Location = new Point(8, 0x48);
            this.rbOdbcEx2.Name = "rbOdbcEx2";
            this.rbOdbcEx2.Size = new Size(0x9e, 0x10);
            this.rbOdbcEx2.TabIndex = 4;
            this.rbOdbcEx2.Text = "Export down to Encounter details";
            this.rbOdbcEx2.UseVisualStyleBackColor = true;
            this.rbOdbcEx2.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbOdbcEx1.AutoSize = true;
            this.rbOdbcEx1.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.rbOdbcEx1.Location = new Point(8, 0x38);
            this.rbOdbcEx1.Name = "rbOdbcEx1";
            this.rbOdbcEx1.Size = new Size(0x9f, 0x10);
            this.rbOdbcEx1.TabIndex = 3;
            this.rbOdbcEx1.Text = "Export Master Encounter list only";
            this.rbOdbcEx1.UseVisualStyleBackColor = true;
            this.rbOdbcEx1.MouseHover += new EventHandler(this.control_MouseHover);
            this.rtbOdbc.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.rtbOdbc.Location = new Point(310, 0x43);
            this.rtbOdbc.Name = "rtbOdbc";
            this.rtbOdbc.ReadOnly = true;
            this.rtbOdbc.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            this.rtbOdbc.Size = new Size(0x191, 0x42);
            this.rtbOdbc.TabIndex = 12;
            this.rtbOdbc.Text = "";
            this.rtbOdbc.MouseHover += new EventHandler(this.control_MouseHover);
            this.linkLabelConnectionString.AutoSize = true;
            this.linkLabelConnectionString.Location = new Point(8, 0xa6);
            this.linkLabelConnectionString.Name = "linkLabelConnectionString";
            this.linkLabelConnectionString.Size = new Size(0x5e, 13);
            this.linkLabelConnectionString.TabIndex = 10;
            this.linkLabelConnectionString.TabStop = true;
            this.linkLabelConnectionString.Text = "Connection String:";
            this.linkLabelConnectionString.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabelConnectionString_LinkClicked);
            this.linkLabelConnectionString.MouseHover += new EventHandler(this.control_MouseHover);
            this.tbOdbcConnectionString.Font = new Font("Courier New", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.tbOdbcConnectionString.Location = new Point(0x6b, 0xa2);
            this.tbOdbcConnectionString.Name = "tbOdbcConnectionString";
            this.tbOdbcConnectionString.Size = new Size(0x25c, 0x15);
            this.tbOdbcConnectionString.TabIndex = 11;
            this.tbOdbcConnectionString.Text = "DRIVER={MySQL ODBC 5.1 Driver};SERVER=data.domain.com;PORT=3306;DATABASE=myDatabase; USER=myUsername;PASSWORD=myPassword;OPTION=3;";
            this.tbOdbcConnectionString.MouseHover += new EventHandler(this.control_MouseHover);
            this.label53.AutoSize = true;
            this.label53.Location = new Point(0x259, 9);
            this.label53.Name = "label53";
            this.label53.Size = new Size(110, 13);
            this.label53.TabIndex = 0x58;
            this.label53.Text = "Advanced Users Only";
            this.label53.TextAlign = ContentAlignment.TopRight;
            this.cbSqlSafeMode.AutoSize = true;
            this.cbSqlSafeMode.Checked = true;
            this.cbSqlSafeMode.CheckState = CheckState.Checked;
            this.cbSqlSafeMode.Location = new Point(310, 0x12);
            this.cbSqlSafeMode.Name = "cbSqlSafeMode";
            this.cbSqlSafeMode.Size = new Size(0x113, 0x11);
            this.cbSqlSafeMode.TabIndex = 2;
            this.cbSqlSafeMode.Text = "Send one row per INSERT statement. (Debug mode)";
            this.cbSqlSafeMode.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbExOdbc.AutoSize = true;
            this.cbExOdbc.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.cbExOdbc.Location = new Point(8, 0x12);
            this.cbExOdbc.Name = "cbExOdbc";
            this.cbExOdbc.Size = new Size(220, 0x11);
            this.cbExOdbc.TabIndex = 1;
            this.cbExOdbc.Text = "Export data to an ODBC Database";
            this.cbExOdbc.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.groupBox25);
            base.Name = "Options_Odbc";
            base.Size = new Size(0x2d3, 0xc7);
            this.groupBox25.ResumeLayout(false);
            this.groupBox25.PerformLayout();
            this.nudCOdbcDelay.EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void linkLabelConnectionString_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.connectionstrings.com/");
        }

        private void linkLabelOdbcControl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("odbcad32.exe");
        }

        private void ValidateOdbcTable(OdbcConnection odbcConnection, string TableName, string[] ColHeaders, string[] ColTypes)
        {
            if (odbcConnection.State == ConnectionState.Closed)
            {
                odbcConnection.Open();
            }
            OdbcCommand command = odbcConnection.CreateCommand();
            OdbcDataAdapter adapter = new OdbcDataAdapter(string.Format("SELECT * FROM {0} LIMIT 1;", TableName), odbcConnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            for (int i = 0; i < ColHeaders.Length; i++)
            {
                string str = ColHeaders[i];
                bool flag = false;
                foreach (DataColumn column in dataSet.Tables[0].Columns)
                {
                    if (column.ColumnName == str.ToLower())
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    string str2;
                    if (i == 0)
                    {
                        str2 = "FIRST";
                    }
                    else
                    {
                        str2 = string.Format("AFTER {0}", ColHeaders[i - 1].ToLower());
                    }
                    if (odbcConnection.ConnectionString.Contains("Microsoft Access Driver"))
                    {
                        str2 = string.Empty;
                    }
                    command.CommandText = string.Format("ALTER TABLE {0} ADD COLUMN {1} {2} {3};", new object[] { TableName, str.ToLower(), ColTypes[i], str2 });
                    ActGlobals.oFormActMain.lastSql = command.CommandText;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

