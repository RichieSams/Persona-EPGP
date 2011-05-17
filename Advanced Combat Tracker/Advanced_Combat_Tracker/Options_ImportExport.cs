namespace Advanced_Combat_Tracker
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Options_ImportExport : UserControl
    {
        private Button btnExportSettings;
        private Button btnImportSettings;
        private IContainer components;
        private Label label9;

        public Options_ImportExport()
        {
            this.InitializeComponent();
        }

        private void btnExportSettings_Click(object sender, EventArgs e)
        {
            ActGlobals.oFormXmlSettingsIO.Show();
        }

        private void btnImportSettings_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess();
            OpenFileDialog dialog = new OpenFileDialog {
                CheckPathExists = true,
                Filter = "XML Settings File (*.xml)|*.xml",
                Title = "Import Settings to XML",
                AddExtension = true,
                ValidateNames = true,
                InitialDirectory = ActGlobals.oFormActMain.AppDataFolder.FullName
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ActGlobals.oFormActMain.LoadNewSettings(dialog.FileName);
            }
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
            this.label9 = new Label();
            this.btnExportSettings = new Button();
            this.btnImportSettings = new Button();
            base.SuspendLayout();
            this.label9.AutoSize = true;
            this.label9.Location = new Point(3, 11);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x94, 13);
            this.label9.TabIndex = 0x21;
            this.label9.Text = "Import/Export ACT Settings ...";
            this.label9.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnExportSettings.FlatStyle = FlatStyle.System;
            this.btnExportSettings.Location = new Point(0x124, 6);
            this.btnExportSettings.Name = "btnExportSettings";
            this.btnExportSettings.Size = new Size(0x4b, 0x17);
            this.btnExportSettings.TabIndex = 0x20;
            this.btnExportSettings.Text = "Export...";
            this.btnExportSettings.UseVisualStyleBackColor = false;
            this.btnExportSettings.Click += new EventHandler(this.btnExportSettings_Click);
            this.btnExportSettings.MouseHover += new EventHandler(this.control_MouseHover);
            this.btnImportSettings.FlatStyle = FlatStyle.System;
            this.btnImportSettings.Location = new Point(0xd3, 6);
            this.btnImportSettings.Name = "btnImportSettings";
            this.btnImportSettings.Size = new Size(0x4b, 0x17);
            this.btnImportSettings.TabIndex = 0x1f;
            this.btnImportSettings.Text = "Import...";
            this.btnImportSettings.UseVisualStyleBackColor = false;
            this.btnImportSettings.Click += new EventHandler(this.btnImportSettings_Click);
            this.btnImportSettings.MouseHover += new EventHandler(this.control_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.label9);
            base.Controls.Add(this.btnExportSettings);
            base.Controls.Add(this.btnImportSettings);
            base.Name = "Options_ImportExport";
            base.Size = new Size(370, 0x20);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

