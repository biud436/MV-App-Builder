namespace Cordova.Forms
{
    partial class SettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.darkSectionPanelExcludeUnusedResourcesOption = new DarkUI.Controls.DarkSectionPanel();
            this.darkButtonSetOutputFolderPath = new DarkUI.Controls.DarkButton();
            this.darkTextBoxOutputFolderPath = new DarkUI.Controls.DarkTextBox();
            this.darkLabelOutputFolderPath = new DarkUI.Controls.DarkLabel();
            this.darkButtonOK = new DarkUI.Controls.DarkButton();
            this.darkCheckBoxFalse = new DarkUI.Controls.DarkCheckBox();
            this.darkCheckBoxTrue = new DarkUI.Controls.DarkCheckBox();
            this.darkCheckBoxDisabled = new DarkUI.Controls.DarkCheckBox();
            this.darkCheckBoxEnabled = new DarkUI.Controls.DarkCheckBox();
            this.darkCheckBoxM4a = new DarkUI.Controls.DarkCheckBox();
            this.darkCheckBoxOgg = new DarkUI.Controls.DarkCheckBox();
            this.darkLabelUse = new DarkUI.Controls.DarkLabel();
            this.darkLabelRemainTree = new DarkUI.Controls.DarkLabel();
            this.darkLabelAudioFileFormat = new DarkUI.Controls.DarkLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.darkSectionPanelExcludeUnusedResourcesOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // darkSectionPanelExcludeUnusedResourcesOption
            // 
            resources.ApplyResources(this.darkSectionPanelExcludeUnusedResourcesOption, "darkSectionPanelExcludeUnusedResourcesOption");
            this.darkSectionPanelExcludeUnusedResourcesOption.Controls.Add(this.darkButtonSetOutputFolderPath);
            this.darkSectionPanelExcludeUnusedResourcesOption.Controls.Add(this.darkTextBoxOutputFolderPath);
            this.darkSectionPanelExcludeUnusedResourcesOption.Controls.Add(this.darkLabelOutputFolderPath);
            this.darkSectionPanelExcludeUnusedResourcesOption.Controls.Add(this.darkButtonOK);
            this.darkSectionPanelExcludeUnusedResourcesOption.Controls.Add(this.darkCheckBoxFalse);
            this.darkSectionPanelExcludeUnusedResourcesOption.Controls.Add(this.darkCheckBoxTrue);
            this.darkSectionPanelExcludeUnusedResourcesOption.Controls.Add(this.darkCheckBoxDisabled);
            this.darkSectionPanelExcludeUnusedResourcesOption.Controls.Add(this.darkCheckBoxEnabled);
            this.darkSectionPanelExcludeUnusedResourcesOption.Controls.Add(this.darkCheckBoxM4a);
            this.darkSectionPanelExcludeUnusedResourcesOption.Controls.Add(this.darkCheckBoxOgg);
            this.darkSectionPanelExcludeUnusedResourcesOption.Controls.Add(this.darkLabelUse);
            this.darkSectionPanelExcludeUnusedResourcesOption.Controls.Add(this.darkLabelRemainTree);
            this.darkSectionPanelExcludeUnusedResourcesOption.Controls.Add(this.darkLabelAudioFileFormat);
            this.darkSectionPanelExcludeUnusedResourcesOption.Name = "darkSectionPanelExcludeUnusedResourcesOption";
            this.darkSectionPanelExcludeUnusedResourcesOption.SectionHeader = "Unused Exclude Resources Option";
            // 
            // darkButtonSetOutputFolderPath
            // 
            resources.ApplyResources(this.darkButtonSetOutputFolderPath, "darkButtonSetOutputFolderPath");
            this.darkButtonSetOutputFolderPath.Name = "darkButtonSetOutputFolderPath";
            this.darkButtonSetOutputFolderPath.Click += new System.EventHandler(this.darkButtonSetOutputFolderPath_Click);
            // 
            // darkTextBoxOutputFolderPath
            // 
            resources.ApplyResources(this.darkTextBoxOutputFolderPath, "darkTextBoxOutputFolderPath");
            this.darkTextBoxOutputFolderPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.darkTextBoxOutputFolderPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkTextBoxOutputFolderPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkTextBoxOutputFolderPath.Name = "darkTextBoxOutputFolderPath";
            // 
            // darkLabelOutputFolderPath
            // 
            resources.ApplyResources(this.darkLabelOutputFolderPath, "darkLabelOutputFolderPath");
            this.darkLabelOutputFolderPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabelOutputFolderPath.Name = "darkLabelOutputFolderPath";
            // 
            // darkButtonOK
            // 
            resources.ApplyResources(this.darkButtonOK, "darkButtonOK");
            this.darkButtonOK.Name = "darkButtonOK";
            this.darkButtonOK.Click += new System.EventHandler(this.darkButtonOK_Click);
            // 
            // darkCheckBoxFalse
            // 
            resources.ApplyResources(this.darkCheckBoxFalse, "darkCheckBoxFalse");
            this.darkCheckBoxFalse.Name = "darkCheckBoxFalse";
            this.darkCheckBoxFalse.CheckedChanged += new System.EventHandler(this.darkCheckBoxFalse_CheckedChanged);
            // 
            // darkCheckBoxTrue
            // 
            resources.ApplyResources(this.darkCheckBoxTrue, "darkCheckBoxTrue");
            this.darkCheckBoxTrue.Name = "darkCheckBoxTrue";
            this.darkCheckBoxTrue.CheckedChanged += new System.EventHandler(this.darkCheckBoxTrue_CheckedChanged);
            // 
            // darkCheckBoxDisabled
            // 
            resources.ApplyResources(this.darkCheckBoxDisabled, "darkCheckBoxDisabled");
            this.darkCheckBoxDisabled.Name = "darkCheckBoxDisabled";
            this.darkCheckBoxDisabled.CheckedChanged += new System.EventHandler(this.darkCheckBoxDisabled_CheckedChanged);
            // 
            // darkCheckBoxEnabled
            // 
            resources.ApplyResources(this.darkCheckBoxEnabled, "darkCheckBoxEnabled");
            this.darkCheckBoxEnabled.Name = "darkCheckBoxEnabled";
            this.darkCheckBoxEnabled.CheckedChanged += new System.EventHandler(this.darkCheckBoxEnabled_CheckedChanged);
            // 
            // darkCheckBoxM4a
            // 
            resources.ApplyResources(this.darkCheckBoxM4a, "darkCheckBoxM4a");
            this.darkCheckBoxM4a.Name = "darkCheckBoxM4a";
            this.darkCheckBoxM4a.CheckedChanged += new System.EventHandler(this.darkCheckBoxM4a_CheckedChanged);
            // 
            // darkCheckBoxOgg
            // 
            resources.ApplyResources(this.darkCheckBoxOgg, "darkCheckBoxOgg");
            this.darkCheckBoxOgg.Name = "darkCheckBoxOgg";
            this.darkCheckBoxOgg.CheckedChanged += new System.EventHandler(this.darkCheckBoxOgg_CheckedChanged);
            // 
            // darkLabelUse
            // 
            resources.ApplyResources(this.darkLabelUse, "darkLabelUse");
            this.darkLabelUse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabelUse.Name = "darkLabelUse";
            // 
            // darkLabelRemainTree
            // 
            resources.ApplyResources(this.darkLabelRemainTree, "darkLabelRemainTree");
            this.darkLabelRemainTree.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabelRemainTree.Name = "darkLabelRemainTree";
            // 
            // darkLabelAudioFileFormat
            // 
            resources.ApplyResources(this.darkLabelAudioFileFormat, "darkLabelAudioFileFormat");
            this.darkLabelAudioFileFormat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabelAudioFileFormat.Name = "darkLabelAudioFileFormat";
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // SettingsDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.darkSectionPanelExcludeUnusedResourcesOption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsDialog_FormClosed);
            this.Shown += new System.EventHandler(this.SettingsDialog_Load);
            this.darkSectionPanelExcludeUnusedResourcesOption.ResumeLayout(false);
            this.darkSectionPanelExcludeUnusedResourcesOption.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkSectionPanel darkSectionPanelExcludeUnusedResourcesOption;
        private DarkUI.Controls.DarkLabel darkLabelUse;
        private DarkUI.Controls.DarkLabel darkLabelRemainTree;
        private DarkUI.Controls.DarkLabel darkLabelAudioFileFormat;
        private DarkUI.Controls.DarkButton darkButtonOK;
        private DarkUI.Controls.DarkCheckBox darkCheckBoxFalse;
        private DarkUI.Controls.DarkCheckBox darkCheckBoxTrue;
        private DarkUI.Controls.DarkCheckBox darkCheckBoxDisabled;
        private DarkUI.Controls.DarkCheckBox darkCheckBoxEnabled;
        private DarkUI.Controls.DarkCheckBox darkCheckBoxM4a;
        private DarkUI.Controls.DarkCheckBox darkCheckBoxOgg;
        private DarkUI.Controls.DarkButton darkButtonSetOutputFolderPath;
        private DarkUI.Controls.DarkTextBox darkTextBoxOutputFolderPath;
        private DarkUI.Controls.DarkLabel darkLabelOutputFolderPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}