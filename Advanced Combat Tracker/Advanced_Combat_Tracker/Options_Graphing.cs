namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_Graphing : UserControl
    {
        internal CheckBox cbGraphRollingAvg;
        internal CheckBox cbOnlyGraphAllies;
        internal CheckBox cbSimpleGraphTotals;
        internal CheckedListBox clbSoloGraphTypes;
        private IContainer components;
        internal ComboBox ddlGraphPriority;
        private GroupBox groupBox2;
        private Label label1;
        private Label label11;
        private Label label92;
        internal NumericUpDown nudGraphAvg;
        internal RadioButton rbGraphAdv;
        internal RadioButton rbGraphSimple;

        public Options_Graphing()
        {
            this.InitializeComponent();
        }

        private void control_MouseHover(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.control_MouseHover(sender, e);
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
            this.groupBox2 = new GroupBox();
            this.nudGraphAvg = new NumericUpDown();
            this.label92 = new Label();
            this.label11 = new Label();
            this.ddlGraphPriority = new ComboBox();
            this.cbGraphRollingAvg = new CheckBox();
            this.rbGraphAdv = new RadioButton();
            this.cbSimpleGraphTotals = new CheckBox();
            this.cbOnlyGraphAllies = new CheckBox();
            this.rbGraphSimple = new RadioButton();
            this.clbSoloGraphTypes = new CheckedListBox();
            this.label1 = new Label();
            this.groupBox2.SuspendLayout();
            this.nudGraphAvg.BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.clbSoloGraphTypes);
            this.groupBox2.Controls.Add(this.nudGraphAvg);
            this.groupBox2.Controls.Add(this.label92);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.ddlGraphPriority);
            this.groupBox2.Controls.Add(this.cbGraphRollingAvg);
            this.groupBox2.Location = new Point(3, 0x3b);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(560, 0xc2);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Advanced Graph Options";
            this.groupBox2.MouseHover += new EventHandler(this.control_MouseHover);
            this.nudGraphAvg.Location = new Point(0x19c, 0x94);
            int[] bits = new int[4];
            bits[0] = 120;
            this.nudGraphAvg.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.nudGraphAvg.Minimum = new decimal(numArray2);
            this.nudGraphAvg.Name = "nudGraphAvg";
            this.nudGraphAvg.Size = new Size(0x40, 20);
            this.nudGraphAvg.TabIndex = 0x2d;
            int[] numArray3 = new int[4];
            numArray3[0] = 10;
            this.nudGraphAvg.Value = new decimal(numArray3);
            this.label92.AutoSize = true;
            this.label92.Location = new Point(6, 0x98);
            this.label92.Name = "label92";
            this.label92.Size = new Size(380, 13);
            this.label92.TabIndex = 0x2c;
            this.label92.Text = "For line graphs, set the plot points to include the average of the last N seconds:";
            this.label92.MouseHover += new EventHandler(this.control_MouseHover);
            this.label11.AutoSize = true;
            this.label11.Location = new Point(6, 0x85);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0xa1, 13);
            this.label11.TabIndex = 0x2a;
            this.label11.Text = "CPU Priority of graph generation:";
            this.label11.TextAlign = ContentAlignment.MiddleLeft;
            this.label11.MouseHover += new EventHandler(this.control_MouseHover);
            this.ddlGraphPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlGraphPriority.Items.AddRange(new object[] { "Above Normal", "Normal", "Below Normal", "Lowest" });
            this.ddlGraphPriority.Location = new Point(0xc1, 0x80);
            this.ddlGraphPriority.Name = "ddlGraphPriority";
            this.ddlGraphPriority.Size = new Size(160, 0x15);
            this.ddlGraphPriority.TabIndex = 2;
            this.ddlGraphPriority.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbGraphRollingAvg.AutoSize = true;
            this.cbGraphRollingAvg.Checked = true;
            this.cbGraphRollingAvg.CheckState = CheckState.Checked;
            this.cbGraphRollingAvg.Location = new Point(7, 0xab);
            this.cbGraphRollingAvg.Name = "cbGraphRollingAvg";
            this.cbGraphRollingAvg.Size = new Size(0x193, 0x11);
            this.cbGraphRollingAvg.TabIndex = 0x2e;
            this.cbGraphRollingAvg.Text = "Graph lines show a rolling average instead of grouped together segments of time";
            this.cbGraphRollingAvg.UseVisualStyleBackColor = true;
            this.cbGraphRollingAvg.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbGraphAdv.AutoSize = true;
            this.rbGraphAdv.Location = new Point(3, 0x1a);
            this.rbGraphAdv.Name = "rbGraphAdv";
            this.rbGraphAdv.Size = new Size(0x107, 0x11);
            this.rbGraphAdv.TabIndex = 12;
            this.rbGraphAdv.Text = "Advanced DPS over time graphs.  (CPU Intensive)";
            this.rbGraphAdv.CheckedChanged += new EventHandler(this.rbGraphSimple_CheckedChanged);
            this.rbGraphAdv.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbSimpleGraphTotals.AutoSize = true;
            this.cbSimpleGraphTotals.Checked = true;
            this.cbSimpleGraphTotals.CheckState = CheckState.Checked;
            this.cbSimpleGraphTotals.Location = new Point(360, 3);
            this.cbSimpleGraphTotals.Name = "cbSimpleGraphTotals";
            this.cbSimpleGraphTotals.Size = new Size(0x89, 0x11);
            this.cbSimpleGraphTotals.TabIndex = 9;
            this.cbSimpleGraphTotals.Text = "Show Totals && Average";
            this.cbSimpleGraphTotals.MouseHover += new EventHandler(this.control_MouseHover);
            this.cbOnlyGraphAllies.AutoSize = true;
            this.cbOnlyGraphAllies.Checked = true;
            this.cbOnlyGraphAllies.CheckState = CheckState.Checked;
            this.cbOnlyGraphAllies.Location = new Point(0xf7, 3);
            this.cbOnlyGraphAllies.Name = "cbOnlyGraphAllies";
            this.cbOnlyGraphAllies.Size = new Size(0x65, 0x11);
            this.cbOnlyGraphAllies.TabIndex = 8;
            this.cbOnlyGraphAllies.Text = "Only show allies";
            this.cbOnlyGraphAllies.MouseHover += new EventHandler(this.control_MouseHover);
            this.rbGraphSimple.AutoSize = true;
            this.rbGraphSimple.Checked = true;
            this.rbGraphSimple.Location = new Point(3, 3);
            this.rbGraphSimple.Name = "rbGraphSimple";
            this.rbGraphSimple.Size = new Size(0xe8, 0x11);
            this.rbGraphSimple.TabIndex = 7;
            this.rbGraphSimple.TabStop = true;
            this.rbGraphSimple.Text = "Simple bar graphs depicting the current sort.";
            this.rbGraphSimple.CheckedChanged += new EventHandler(this.rbGraphSimple_CheckedChanged);
            this.rbGraphSimple.MouseHover += new EventHandler(this.control_MouseHover);
            this.clbSoloGraphTypes.FormattingEnabled = true;
            this.clbSoloGraphTypes.IntegralHeight = false;
            this.clbSoloGraphTypes.Location = new Point(6, 0x13);
            this.clbSoloGraphTypes.Name = "clbSoloGraphTypes";
            this.clbSoloGraphTypes.Size = new Size(0x15b, 0x67);
            this.clbSoloGraphTypes.TabIndex = 0x2f;
            this.label1.Location = new Point(0x167, 0x13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xc3, 0x67);
            this.label1.TabIndex = 0x30;
            this.label1.Text = "DamageType categories to include in combatant line graphs.";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.rbGraphAdv);
            base.Controls.Add(this.cbSimpleGraphTotals);
            base.Controls.Add(this.cbOnlyGraphAllies);
            base.Controls.Add(this.rbGraphSimple);
            base.Name = "OpGraphing";
            base.Size = new Size(0x236, 0x100);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.nudGraphAvg.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void rbGraphSimple_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbGraphSimple.Checked)
            {
                ActGlobals.oFormActMain.GenerateEncounterGraph = new FormActMain.EncounterGraphGenerator(ActGlobals.oFormActMain.GenEncounterGraph);
            }
            else if (this.rbGraphAdv.Checked)
            {
                ActGlobals.oFormActMain.GenerateEncounterGraph = new FormActMain.EncounterGraphGenerator(ActGlobals.oFormActMain.GenEncounterGraphAdv);
            }
        }
    }
}

