namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_XmlSubs : UserControl
    {
        private Button btnSubAdd;
        private Button btnSubCheckNow;
        private Button btnSubQuery;
        private Button btnSubRemove;
        private Button btnSubUpdateChecked;
        internal CheckedListBox clbSubs;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label2;
        private LinkLabel linkSubBrowser;
        private RadioButton rbSubAuto;
        private RadioButton rbSubIgnore;
        private RadioButton rbSubNotify;
        private TableLayoutPanel tableLayoutPanel1;
        internal TextBox tbSubInfo;
        private TextBox tbSubUrl;

        public Options_XmlSubs()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.SubAddEntry(new XmlShareEntry(this.tbSubUrl.Text, this.rbSubIgnore.Checked, this.rbSubNotify.Checked, this.rbSubAuto.Checked));
            ActGlobals.oFormActMain.SubUpdateListbox(false);
        }

        private void btnCheckNow_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.SubUpdateEntries();
            ActGlobals.oFormActMain.SubUpdateListbox(true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.clbSubs.SelectedIndex != -1)
            {
                string key = (string) this.clbSubs.Items[this.clbSubs.SelectedIndex];
                ActGlobals.oFormActMain.SubRemoveEntry(key);
                ActGlobals.oFormActMain.SubUpdateListbox(false);
            }
        }

        private void btnSubQuery_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.SubQuery(this.tbSubUrl.Text);
        }

        private void btnUpdateChecked_Click(object sender, EventArgs e)
        {
            List<string> entries = new List<string>();
            for (int i = 0; i < this.clbSubs.Items.Count; i++)
            {
                if (this.clbSubs.GetItemCheckState(i) == CheckState.Checked)
                {
                    entries.Add(this.clbSubs.Items[i].ToString());
                }
            }
            ActGlobals.oFormActMain.SubDoXmlImports(entries, true);
            ActGlobals.oFormActMain.SubUpdateListbox(false);
        }

        private void clbSubs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.clbSubs.SelectedIndex != -1)
            {
                string str = this.clbSubs.Items[this.clbSubs.SelectedIndex].ToString();
                XmlShareEntry entry = ActGlobals.oFormActMain.subEntries[str];
                this.tbSubInfo.Text = string.Format("Last Modified: {0} {1}\r\nLast Updated: {2} {3}\r\n", new object[] { entry.LastModified.ToShortDateString(), entry.LastModified.ToShortTimeString(), entry.LastUpdated.ToShortDateString(), entry.LastUpdated.ToShortTimeString() });
                this.tbSubUrl.Text = entry.Url;
                if (!string.IsNullOrEmpty(entry.LastXml))
                {
                    ActGlobals.oFormActMain.SubAddQueryNodeCounts(entry.LastXml);
                }
                if (entry.RbAuto)
                {
                    this.rbSubAuto.Checked = true;
                }
                if (entry.RbIgnore)
                {
                    this.rbSubIgnore.Checked = true;
                }
                if (entry.RbNotify)
                {
                    this.rbSubNotify.Checked = true;
                }
            }
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.btnSubAdd = new Button();
            this.btnSubRemove = new Button();
            this.btnSubCheckNow = new Button();
            this.btnSubUpdateChecked = new Button();
            this.clbSubs = new CheckedListBox();
            this.groupBox2 = new GroupBox();
            this.linkSubBrowser = new LinkLabel();
            this.btnSubQuery = new Button();
            this.groupBox3 = new GroupBox();
            this.tbSubInfo = new TextBox();
            this.label2 = new Label();
            this.rbSubAuto = new RadioButton();
            this.rbSubNotify = new RadioButton();
            this.rbSubIgnore = new RadioButton();
            this.tbSubUrl = new TextBox();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.clbSubs);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = DockStyle.Fill;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(610, 0x1d1);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "XML Config Subscriptions";
            this.tableLayoutPanel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            this.tableLayoutPanel1.Controls.Add(this.btnSubAdd, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSubRemove, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSubCheckNow, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSubUpdateChecked, 3, 0);
            this.tableLayoutPanel1.Location = new Point(6, 0x121);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Size = new Size(0x256, 0x1d);
            this.tableLayoutPanel1.TabIndex = 1;
            this.btnSubAdd.Dock = DockStyle.Fill;
            this.btnSubAdd.Location = new Point(3, 3);
            this.btnSubAdd.Name = "btnSubAdd";
            this.btnSubAdd.Size = new Size(0x8f, 0x17);
            this.btnSubAdd.TabIndex = 0;
            this.btnSubAdd.Text = "Add/Edit";
            this.btnSubAdd.UseVisualStyleBackColor = true;
            this.btnSubAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnSubAdd.MouseHover += new EventHandler(this.xmlSub_MouseHover);
            this.btnSubRemove.Dock = DockStyle.Fill;
            this.btnSubRemove.Location = new Point(0x98, 3);
            this.btnSubRemove.Name = "btnSubRemove";
            this.btnSubRemove.Size = new Size(0x8f, 0x17);
            this.btnSubRemove.TabIndex = 1;
            this.btnSubRemove.Text = "Remove";
            this.btnSubRemove.UseVisualStyleBackColor = true;
            this.btnSubRemove.Click += new EventHandler(this.btnRemove_Click);
            this.btnSubRemove.MouseHover += new EventHandler(this.xmlSub_MouseHover);
            this.btnSubCheckNow.Dock = DockStyle.Fill;
            this.btnSubCheckNow.Location = new Point(0x12d, 3);
            this.btnSubCheckNow.Name = "btnSubCheckNow";
            this.btnSubCheckNow.Size = new Size(0x8f, 0x17);
            this.btnSubCheckNow.TabIndex = 2;
            this.btnSubCheckNow.Text = "Check Now";
            this.btnSubCheckNow.UseVisualStyleBackColor = true;
            this.btnSubCheckNow.Click += new EventHandler(this.btnCheckNow_Click);
            this.btnSubCheckNow.MouseHover += new EventHandler(this.xmlSub_MouseHover);
            this.btnSubUpdateChecked.Dock = DockStyle.Fill;
            this.btnSubUpdateChecked.Location = new Point(450, 3);
            this.btnSubUpdateChecked.Name = "btnSubUpdateChecked";
            this.btnSubUpdateChecked.Size = new Size(0x91, 0x17);
            this.btnSubUpdateChecked.TabIndex = 3;
            this.btnSubUpdateChecked.Text = "Update Checked";
            this.btnSubUpdateChecked.UseVisualStyleBackColor = true;
            this.btnSubUpdateChecked.Click += new EventHandler(this.btnUpdateChecked_Click);
            this.btnSubUpdateChecked.MouseHover += new EventHandler(this.xmlSub_MouseHover);
            this.clbSubs.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.clbSubs.FormattingEnabled = true;
            this.clbSubs.IntegralHeight = false;
            this.clbSubs.Location = new Point(6, 0x13);
            this.clbSubs.Name = "clbSubs";
            this.clbSubs.Size = new Size(0x256, 0x108);
            this.clbSubs.Sorted = true;
            this.clbSubs.TabIndex = 0;
            this.clbSubs.SelectedIndexChanged += new EventHandler(this.clbSubs_SelectedIndexChanged);
            this.clbSubs.MouseHover += new EventHandler(this.xmlSub_MouseHover);
            this.groupBox2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.groupBox2.Controls.Add(this.linkSubBrowser);
            this.groupBox2.Controls.Add(this.btnSubQuery);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.rbSubAuto);
            this.groupBox2.Controls.Add(this.rbSubNotify);
            this.groupBox2.Controls.Add(this.rbSubIgnore);
            this.groupBox2.Controls.Add(this.tbSubUrl);
            this.groupBox2.Location = new Point(6, 0x144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x256, 0x87);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Subscription Details";
            this.linkSubBrowser.AutoSize = true;
            this.linkSubBrowser.Location = new Point(0x5d, 110);
            this.linkSubBrowser.Name = "linkSubBrowser";
            this.linkSubBrowser.Size = new Size(0x51, 13);
            this.linkSubBrowser.TabIndex = 6;
            this.linkSubBrowser.TabStop = true;
            this.linkSubBrowser.Text = "View in browser";
            this.linkSubBrowser.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkSubBrowser_LinkClicked);
            this.linkSubBrowser.MouseHover += new EventHandler(this.xmlSub_MouseHover);
            this.btnSubQuery.Location = new Point(12, 0x69);
            this.btnSubQuery.Name = "btnSubQuery";
            this.btnSubQuery.Size = new Size(0x4b, 0x17);
            this.btnSubQuery.TabIndex = 5;
            this.btnSubQuery.Text = "Query URL";
            this.btnSubQuery.UseVisualStyleBackColor = true;
            this.btnSubQuery.Click += new EventHandler(this.btnSubQuery_Click);
            this.btnSubQuery.MouseHover += new EventHandler(this.xmlSub_MouseHover);
            this.groupBox3.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox3.Controls.Add(this.tbSubInfo);
            this.groupBox3.Location = new Point(0xca, 0x2d);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(390, 0x53);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Import Info Query";
            this.tbSubInfo.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tbSubInfo.BorderStyle = BorderStyle.None;
            this.tbSubInfo.Location = new Point(6, 20);
            this.tbSubInfo.Multiline = true;
            this.tbSubInfo.Name = "tbSubInfo";
            this.tbSubInfo.ReadOnly = true;
            this.tbSubInfo.ScrollBars = ScrollBars.Vertical;
            this.tbSubInfo.Size = new Size(0x17a, 0x39);
            this.tbSubInfo.TabIndex = 0;
            this.tbSubInfo.MouseHover += new EventHandler(this.xmlSub_MouseHover);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 0x16);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "URL";
            this.rbSubAuto.AutoSize = true;
            this.rbSubAuto.Location = new Point(9, 0x55);
            this.rbSubAuto.Margin = new Padding(3, 3, 3, 0);
            this.rbSubAuto.Name = "rbSubAuto";
            this.rbSubAuto.Size = new Size(0xae, 0x11);
            this.rbSubAuto.TabIndex = 4;
            this.rbSubAuto.Text = "Import Updated Files on Startup";
            this.rbSubAuto.UseVisualStyleBackColor = true;
            this.rbSubAuto.MouseHover += new EventHandler(this.xmlSub_MouseHover);
            this.rbSubNotify.AutoSize = true;
            this.rbSubNotify.Checked = true;
            this.rbSubNotify.Location = new Point(9, 0x41);
            this.rbSubNotify.Margin = new Padding(3, 3, 3, 0);
            this.rbSubNotify.Name = "rbSubNotify";
            this.rbSubNotify.Size = new Size(0x84, 0x11);
            this.rbSubNotify.TabIndex = 3;
            this.rbSubNotify.TabStop = true;
            this.rbSubNotify.Text = "Notify of Updated Files";
            this.rbSubNotify.UseVisualStyleBackColor = true;
            this.rbSubNotify.MouseHover += new EventHandler(this.xmlSub_MouseHover);
            this.rbSubIgnore.AutoSize = true;
            this.rbSubIgnore.Location = new Point(9, 0x2d);
            this.rbSubIgnore.Margin = new Padding(3, 3, 3, 0);
            this.rbSubIgnore.Name = "rbSubIgnore";
            this.rbSubIgnore.Size = new Size(0x7b, 0x11);
            this.rbSubIgnore.TabIndex = 2;
            this.rbSubIgnore.Text = "Ignore Updated Files";
            this.rbSubIgnore.UseVisualStyleBackColor = true;
            this.rbSubIgnore.MouseHover += new EventHandler(this.xmlSub_MouseHover);
            this.tbSubUrl.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbSubUrl.Location = new Point(0x29, 0x13);
            this.tbSubUrl.Name = "tbSubUrl";
            this.tbSubUrl.Size = new Size(0x227, 20);
            this.tbSubUrl.TabIndex = 1;
            this.tbSubUrl.MouseHover += new EventHandler(this.xmlSub_MouseHover);
            base.Controls.Add(this.groupBox1);
            base.Name = "Options";
            base.Size = new Size(610, 0x1d1);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            base.ResumeLayout(false);
        }

        private void linkSubBrowser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(this.tbSubUrl.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, this.tbSubUrl.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void xmlSub_MouseHover(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.lblHelpText.Text = ActGlobals.ActLocalization.LocalizationStrings["helpPanel-xmlSubscription"].DisplayedText;
        }
    }
}

