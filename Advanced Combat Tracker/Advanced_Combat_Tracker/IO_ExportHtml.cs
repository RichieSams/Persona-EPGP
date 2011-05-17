namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    internal class IO_ExportHtml : UserControl
    {
        private Button btnCancel;
        private Button btnExportHtml;
        private IContainer components;
        private Thread exportThread;
        internal volatile bool exportThreadAlive;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label15;
        private Label label18;
        private Label lblGraphXY;
        internal Label lblHtmlExportStatus;
        internal NumericUpDown nudExportGraphX;
        internal NumericUpDown nudExportGraphY;
        private System.Windows.Forms.Timer timer500;
        internal TreeView tvHTMLExport;

        public IO_ExportHtml()
        {
            this.InitializeComponent();
            this.tvHTMLExport.ExpandAll();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.exportThread.Abort();
            }
            catch
            {
            }
        }

        private void btnExportHtml_Click(object sender, EventArgs e)
        {
            this.exportThread = new Thread(new ThreadStart(ActGlobals.oFormActMain.ThreadExportHTML));
            this.exportThread.IsBackground = true;
            this.exportThread.Priority = ThreadPriority.Normal;
            this.exportThread.Name = "HTML Export Thread";
            this.exportThread.SetApartmentState(ApartmentState.STA);
            this.exportThread.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            TreeNode node = new TreeNode("Attack Type Tables");
            TreeNode node2 = new TreeNode("Damage Types Tables", new TreeNode[] { node });
            TreeNode node3 = new TreeNode("Combatant Tables", new TreeNode[] { node2 });
            TreeNode node4 = new TreeNode("Encounter Tables", new TreeNode[] { node3 });
            TreeNode node5 = new TreeNode("Attack Type Graphs");
            TreeNode node6 = new TreeNode("Damage Type Graphs", new TreeNode[] { node5 });
            TreeNode node7 = new TreeNode("Combatant Graphs", new TreeNode[] { node6 });
            TreeNode node8 = new TreeNode("Encounter Graphs", new TreeNode[] { node7 });
            this.tvHTMLExport = new TreeView();
            this.groupBox1 = new GroupBox();
            this.btnExportHtml = new Button();
            this.groupBox2 = new GroupBox();
            this.lblHtmlExportStatus = new Label();
            this.groupBox3 = new GroupBox();
            this.label15 = new Label();
            this.nudExportGraphY = new NumericUpDown();
            this.nudExportGraphX = new NumericUpDown();
            this.label18 = new Label();
            this.lblGraphXY = new Label();
            this.btnCancel = new Button();
            this.timer500 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.nudExportGraphY.BeginInit();
            this.nudExportGraphX.BeginInit();
            base.SuspendLayout();
            this.tvHTMLExport.CheckBoxes = true;
            this.tvHTMLExport.Location = new Point(6, 0x13);
            this.tvHTMLExport.Name = "tvHTMLExport";
            node.Checked = true;
            node.Name = "aTTables";
            node.Text = "Attack Type Tables";
            node2.Checked = true;
            node2.Name = "dTTables";
            node2.Text = "Damage Types Tables";
            node3.Checked = true;
            node3.Name = "cDTables";
            node3.Text = "Combatant Tables";
            node4.Checked = true;
            node4.Name = "eDTables";
            node4.Text = "Encounter Tables";
            node5.Checked = true;
            node5.Name = "aTGraphs";
            node5.Text = "Attack Type Graphs";
            node6.Checked = true;
            node6.Name = "dTGraphs";
            node6.Text = "Damage Type Graphs";
            node7.Checked = true;
            node7.Name = "cDGraphs";
            node7.Text = "Combatant Graphs";
            node8.Checked = true;
            node8.Name = "eDGraphs";
            node8.Text = "Encounter Graphs";
            this.tvHTMLExport.Nodes.AddRange(new TreeNode[] { node4, node8 });
            this.tvHTMLExport.Size = new Size(0xef, 0x8f);
            this.tvHTMLExport.TabIndex = 1;
            this.tvHTMLExport.AfterCheck += new TreeViewEventHandler(this.tvHTMLExport_AfterCheck);
            this.groupBox1.Controls.Add(this.tvHTMLExport);
            this.groupBox1.Location = new Point(3, 0x6d);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xfb, 0xa8);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export Depth Options";
            this.btnExportHtml.Location = new Point(260, 0xfe);
            this.btnExportHtml.Name = "btnExportHtml";
            this.btnExportHtml.Size = new Size(0x111, 0x17);
            this.btnExportHtml.TabIndex = 3;
            this.btnExportHtml.Text = "Export with these options to HTML...";
            this.btnExportHtml.UseVisualStyleBackColor = true;
            this.btnExportHtml.Click += new EventHandler(this.btnExportHtml_Click);
            this.groupBox2.Controls.Add(this.lblHtmlExportStatus);
            this.groupBox2.Location = new Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(530, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            this.lblHtmlExportStatus.Dock = DockStyle.Fill;
            this.lblHtmlExportStatus.Location = new Point(3, 0x10);
            this.lblHtmlExportStatus.Name = "lblHtmlExportStatus";
            this.lblHtmlExportStatus.Size = new Size(0x20c, 0x51);
            this.lblHtmlExportStatus.TabIndex = 0;
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.nudExportGraphY);
            this.groupBox3.Controls.Add(this.nudExportGraphX);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.lblGraphXY);
            this.groupBox3.Location = new Point(260, 0x6d);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(270, 0x42);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Other Settings";
            this.label15.AutoSize = true;
            this.label15.Location = new Point(6, 0x26);
            this.label15.Name = "label15";
            this.label15.Size = new Size(14, 13);
            this.label15.TabIndex = 0x3f;
            this.label15.Text = "X";
            this.label15.TextAlign = ContentAlignment.BottomLeft;
            int[] bits = new int[4];
            bits[0] = 8;
            this.nudExportGraphY.Increment = new decimal(bits);
            this.nudExportGraphY.Location = new Point(100, 0x22);
            int[] numArray2 = new int[4];
            numArray2[0] = 0x640;
            this.nudExportGraphY.Maximum = new decimal(numArray2);
            int[] numArray3 = new int[4];
            numArray3[0] = 0x80;
            this.nudExportGraphY.Minimum = new decimal(numArray3);
            this.nudExportGraphY.Name = "nudExportGraphY";
            this.nudExportGraphY.Size = new Size(0x30, 20);
            this.nudExportGraphY.TabIndex = 0x40;
            int[] numArray4 = new int[4];
            numArray4[0] = 0x100;
            this.nudExportGraphY.Value = new decimal(numArray4);
            int[] numArray5 = new int[4];
            numArray5[0] = 8;
            this.nudExportGraphX.Increment = new decimal(numArray5);
            this.nudExportGraphX.Location = new Point(0x1a, 0x22);
            int[] numArray6 = new int[4];
            numArray6[0] = 0x640;
            this.nudExportGraphX.Maximum = new decimal(numArray6);
            int[] numArray7 = new int[4];
            numArray7[0] = 0x80;
            this.nudExportGraphX.Minimum = new decimal(numArray7);
            this.nudExportGraphX.Name = "nudExportGraphX";
            this.nudExportGraphX.Size = new Size(0x30, 20);
            this.nudExportGraphX.TabIndex = 0x42;
            int[] numArray8 = new int[4];
            numArray8[0] = 640;
            this.nudExportGraphX.Value = new decimal(numArray8);
            this.label18.AutoSize = true;
            this.label18.Location = new Point(80, 0x26);
            this.label18.Name = "label18";
            this.label18.Size = new Size(14, 13);
            this.label18.TabIndex = 0x43;
            this.label18.Text = "Y";
            this.label18.TextAlign = ContentAlignment.BottomLeft;
            this.lblGraphXY.AutoSize = true;
            this.lblGraphXY.Location = new Point(6, 0x10);
            this.lblGraphXY.Name = "lblGraphXY";
            this.lblGraphXY.Size = new Size(0xec, 13);
            this.lblGraphXY.TabIndex = 0x41;
            this.lblGraphXY.Text = "Desired size in pixels of the HTML graph images.";
            this.lblGraphXY.TextAlign = ContentAlignment.BottomLeft;
            this.btnCancel.Location = new Point(0x1a7, 0xe1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(110, 0x17);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.timer500.Enabled = true;
            this.timer500.Interval = 500;
            this.timer500.Tick += new EventHandler(this.timer500_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.btnExportHtml);
            base.Controls.Add(this.groupBox1);
            base.Name = "IO_ExportHtml";
            base.Size = new Size(0x218, 280);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.nudExportGraphY.EndInit();
            this.nudExportGraphX.EndInit();
            base.ResumeLayout(false);
        }

        private void timer500_Tick(object sender, EventArgs e)
        {
            this.btnCancel.Visible = this.exportThreadAlive;
        }

        private void tvHTMLExport_AfterCheck(object sender, TreeViewEventArgs e)
        {
            this.tvHTMLExport.AfterCheck -= new TreeViewEventHandler(this.tvHTMLExport_AfterCheck);
            this.tvHTMLSetChecks(e.Node, e.Node.Checked);
            this.tvHTMLExport.AfterCheck += new TreeViewEventHandler(this.tvHTMLExport_AfterCheck);
        }

        private void tvHTMLSetChecks(TreeNode tN, bool isChecked)
        {
            if (isChecked)
            {
                switch (tN.Name)
                {
                    case "eDTables":
                        tN.Checked = true;
                        return;

                    case "cDTables":
                        tN.Checked = true;
                        this.tvHTMLSetChecks(this.tvHTMLExport.Nodes[0], true);
                        return;

                    case "dTTables":
                        tN.Checked = true;
                        this.tvHTMLSetChecks(this.tvHTMLExport.Nodes[0].Nodes[0], true);
                        return;

                    case "aTTables":
                        tN.Checked = true;
                        this.tvHTMLSetChecks(this.tvHTMLExport.Nodes[0].Nodes[0].Nodes[0], true);
                        return;

                    case "eDGraphs":
                        tN.Checked = true;
                        this.tvHTMLExport.Nodes[0].Checked = true;
                        return;

                    case "cDGraphs":
                        tN.Checked = true;
                        this.tvHTMLSetChecks(this.tvHTMLExport.Nodes[0].Nodes[0], true);
                        return;

                    case "dTGraphs":
                        tN.Checked = true;
                        this.tvHTMLSetChecks(this.tvHTMLExport.Nodes[0].Nodes[0].Nodes[0], true);
                        return;

                    case "aTGraphs":
                        tN.Checked = true;
                        this.tvHTMLSetChecks(this.tvHTMLExport.Nodes[0].Nodes[0].Nodes[0].Nodes[0], true);
                        return;
                }
            }
            else
            {
                string name = tN.Name;
                if (name != null)
                {
                    if (!(name == "eDTables"))
                    {
                        if (!(name == "cDTables"))
                        {
                            if (!(name == "dTTables"))
                            {
                                if (name == "aTTables")
                                {
                                    tN.Checked = false;
                                    this.tvHTMLExport.Nodes[1].Nodes[0].Nodes[0].Nodes[0].Checked = false;
                                }
                                return;
                            }
                            tN.Checked = false;
                            this.tvHTMLSetChecks(this.tvHTMLExport.Nodes[0].Nodes[0].Nodes[0].Nodes[0], false);
                            this.tvHTMLExport.Nodes[1].Nodes[0].Nodes[0].Checked = false;
                            return;
                        }
                    }
                    else
                    {
                        tN.Checked = false;
                        this.tvHTMLSetChecks(this.tvHTMLExport.Nodes[0].Nodes[0], false);
                        this.tvHTMLExport.Nodes[1].Checked = false;
                        return;
                    }
                    tN.Checked = false;
                    this.tvHTMLSetChecks(this.tvHTMLExport.Nodes[0].Nodes[0].Nodes[0], false);
                    this.tvHTMLExport.Nodes[1].Nodes[0].Checked = false;
                }
            }
        }
    }
}

