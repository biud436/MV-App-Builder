﻿namespace Cordova.Forms
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox1 = new System.Windows.Forms.RichTextBox();
            this.labelFolderName = new System.Windows.Forms.Label();
            this.labelPackageName = new System.Windows.Forms.Label();
            this.labelGameName = new System.Windows.Forms.Label();
            this.labelKeyPath = new System.Windows.Forms.Label();
            this.labelKeyAlias = new System.Windows.Forms.Label();
            this.labelKeyPassword = new System.Windows.Forms.Label();
            this.label_keyOU = new System.Windows.Forms.Label();
            this.label_keyO = new System.Windows.Forms.Label();
            this.label_keyL = new System.Windows.Forms.Label();
            this.label_keyS = new System.Windows.Forms.Label();
            this.textBoxFolderName = new DarkUI.Controls.DarkTextBox();
            this.textBoxPackageName = new System.Windows.Forms.TextBox();
            this.textBoxGameName = new System.Windows.Forms.TextBox();
            this.textBoxKeyPath = new System.Windows.Forms.TextBox();
            this.textBoxKeyAlias = new System.Windows.Forms.TextBox();
            this.textBoxPassWord = new System.Windows.Forms.TextBox();
            this.textBox_keyOU = new System.Windows.Forms.TextBox();
            this.textBox_keyO = new System.Windows.Forms.TextBox();
            this.textBox_keyL = new System.Windows.Forms.TextBox();
            this.textBox_keyS = new System.Windows.Forms.TextBox();
            this.textBox_keyC = new System.Windows.Forms.TextBox();
            this.label_keyC = new System.Windows.Forms.Label();
            this.buttonBuild = new DarkUI.Controls.DarkButton();
            this.labelOrientation = new System.Windows.Forms.Label();
            this.labelFullscreen = new System.Windows.Forms.Label();
            this.labelMinSdkVersion = new System.Windows.Forms.Label();
            this.labelTargetSdkVersion = new System.Windows.Forms.Label();
            this.comboBoxOrientation = new System.Windows.Forms.ComboBox();
            this.comboBoxFullscreen = new System.Windows.Forms.ComboBox();
            this.comboBoxMinSdkVersion = new System.Windows.Forms.ComboBox();
            this.comboBoxTargetSdkVersion = new System.Windows.Forms.ComboBox();
            this.labelSettingGameFolder = new System.Windows.Forms.Label();
            this.textBoxSettingGameFolder = new System.Windows.Forms.TextBox();
            this.buttonOpenFileBrowser = new System.Windows.Forms.Button();
            this.labelBuildMode = new System.Windows.Forms.Label();
            this.comboBoxBuildMode = new System.Windows.Forms.ComboBox();
            this.labelPluginName = new System.Windows.Forms.Label();
            this.buttonAddPlugin = new DarkUI.Controls.DarkButton();
            this.textBoxPluginName = new System.Windows.Forms.TextBox();
            this.listBoxPlugins = new System.Windows.Forms.ListBox();
            this.buttonDeletePlugin = new DarkUI.Controls.DarkButton();
            this.timerBackground = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panelPlugins = new DarkUI.Controls.DarkSectionPanel();
            this.buttonSettings = new DarkUI.Controls.DarkButton();
            this.labelCompileSdkVersion = new System.Windows.Forms.Label();
            this.comboBoxCompileSdkVersion = new System.Windows.Forms.ComboBox();
            this.panelBuildLog = new DarkUI.Controls.DarkSectionPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panelPlugins.SuspendLayout();
            this.panelBuildLog.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            this.textBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseClick);
            // 
            // labelFolderName
            // 
            resources.ApplyResources(this.labelFolderName, "labelFolderName");
            this.labelFolderName.BackColor = System.Drawing.Color.Transparent;
            this.labelFolderName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelFolderName.Name = "labelFolderName";
            // 
            // labelPackageName
            // 
            resources.ApplyResources(this.labelPackageName, "labelPackageName");
            this.labelPackageName.BackColor = System.Drawing.Color.Transparent;
            this.labelPackageName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelPackageName.Name = "labelPackageName";
            // 
            // labelGameName
            // 
            resources.ApplyResources(this.labelGameName, "labelGameName");
            this.labelGameName.BackColor = System.Drawing.Color.Transparent;
            this.labelGameName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelGameName.Name = "labelGameName";
            // 
            // labelKeyPath
            // 
            resources.ApplyResources(this.labelKeyPath, "labelKeyPath");
            this.labelKeyPath.BackColor = System.Drawing.Color.Transparent;
            this.labelKeyPath.ForeColor = System.Drawing.SystemColors.Control;
            this.labelKeyPath.Name = "labelKeyPath";
            // 
            // labelKeyAlias
            // 
            resources.ApplyResources(this.labelKeyAlias, "labelKeyAlias");
            this.labelKeyAlias.BackColor = System.Drawing.Color.Transparent;
            this.labelKeyAlias.ForeColor = System.Drawing.SystemColors.Control;
            this.labelKeyAlias.Name = "labelKeyAlias";
            // 
            // labelKeyPassword
            // 
            resources.ApplyResources(this.labelKeyPassword, "labelKeyPassword");
            this.labelKeyPassword.BackColor = System.Drawing.Color.Transparent;
            this.labelKeyPassword.ForeColor = System.Drawing.SystemColors.Control;
            this.labelKeyPassword.Name = "labelKeyPassword";
            // 
            // label_keyOU
            // 
            resources.ApplyResources(this.label_keyOU, "label_keyOU");
            this.label_keyOU.BackColor = System.Drawing.Color.Transparent;
            this.label_keyOU.ForeColor = System.Drawing.SystemColors.Control;
            this.label_keyOU.Name = "label_keyOU";
            // 
            // label_keyO
            // 
            resources.ApplyResources(this.label_keyO, "label_keyO");
            this.label_keyO.BackColor = System.Drawing.Color.Transparent;
            this.label_keyO.ForeColor = System.Drawing.SystemColors.Control;
            this.label_keyO.Name = "label_keyO";
            // 
            // label_keyL
            // 
            resources.ApplyResources(this.label_keyL, "label_keyL");
            this.label_keyL.BackColor = System.Drawing.Color.Transparent;
            this.label_keyL.ForeColor = System.Drawing.SystemColors.Control;
            this.label_keyL.Name = "label_keyL";
            // 
            // label_keyS
            // 
            resources.ApplyResources(this.label_keyS, "label_keyS");
            this.label_keyS.BackColor = System.Drawing.Color.Transparent;
            this.label_keyS.ForeColor = System.Drawing.SystemColors.Control;
            this.label_keyS.Name = "label_keyS";
            // 
            // textBoxFolderName
            // 
            resources.ApplyResources(this.textBoxFolderName, "textBoxFolderName");
            this.textBoxFolderName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.textBoxFolderName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFolderName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.textBoxFolderName.Name = "textBoxFolderName";
            this.textBoxFolderName.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxPackageName
            // 
            resources.ApplyResources(this.textBoxPackageName, "textBoxPackageName");
            this.textBoxPackageName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPackageName.Name = "textBoxPackageName";
            this.textBoxPackageName.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxGameName
            // 
            resources.ApplyResources(this.textBoxGameName, "textBoxGameName");
            this.textBoxGameName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxGameName.Name = "textBoxGameName";
            this.textBoxGameName.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxKeyPath
            // 
            resources.ApplyResources(this.textBoxKeyPath, "textBoxKeyPath");
            this.textBoxKeyPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxKeyPath.Name = "textBoxKeyPath";
            this.textBoxKeyPath.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxKeyAlias
            // 
            resources.ApplyResources(this.textBoxKeyAlias, "textBoxKeyAlias");
            this.textBoxKeyAlias.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxKeyAlias.Name = "textBoxKeyAlias";
            this.textBoxKeyAlias.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxPassWord
            // 
            resources.ApplyResources(this.textBoxPassWord, "textBoxPassWord");
            this.textBoxPassWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPassWord.Name = "textBoxPassWord";
            this.textBoxPassWord.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyOU
            // 
            resources.ApplyResources(this.textBox_keyOU, "textBox_keyOU");
            this.textBox_keyOU.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_keyOU.Name = "textBox_keyOU";
            this.textBox_keyOU.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyO
            // 
            resources.ApplyResources(this.textBox_keyO, "textBox_keyO");
            this.textBox_keyO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_keyO.Name = "textBox_keyO";
            this.textBox_keyO.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyL
            // 
            resources.ApplyResources(this.textBox_keyL, "textBox_keyL");
            this.textBox_keyL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_keyL.Name = "textBox_keyL";
            this.textBox_keyL.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyS
            // 
            resources.ApplyResources(this.textBox_keyS, "textBox_keyS");
            this.textBox_keyS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_keyS.Name = "textBox_keyS";
            this.textBox_keyS.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyC
            // 
            resources.ApplyResources(this.textBox_keyC, "textBox_keyC");
            this.textBox_keyC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_keyC.Name = "textBox_keyC";
            this.textBox_keyC.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // label_keyC
            // 
            resources.ApplyResources(this.label_keyC, "label_keyC");
            this.label_keyC.BackColor = System.Drawing.Color.Transparent;
            this.label_keyC.ForeColor = System.Drawing.SystemColors.Control;
            this.label_keyC.Name = "label_keyC";
            // 
            // buttonBuild
            // 
            resources.ApplyResources(this.buttonBuild, "buttonBuild");
            this.buttonBuild.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Click += new System.EventHandler(this.buttonBuild_Click);
            // 
            // labelOrientation
            // 
            resources.ApplyResources(this.labelOrientation, "labelOrientation");
            this.labelOrientation.BackColor = System.Drawing.Color.Transparent;
            this.labelOrientation.ForeColor = System.Drawing.SystemColors.Control;
            this.labelOrientation.Name = "labelOrientation";
            // 
            // labelFullscreen
            // 
            resources.ApplyResources(this.labelFullscreen, "labelFullscreen");
            this.labelFullscreen.BackColor = System.Drawing.Color.Transparent;
            this.labelFullscreen.ForeColor = System.Drawing.SystemColors.Control;
            this.labelFullscreen.Name = "labelFullscreen";
            // 
            // labelMinSdkVersion
            // 
            resources.ApplyResources(this.labelMinSdkVersion, "labelMinSdkVersion");
            this.labelMinSdkVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelMinSdkVersion.ForeColor = System.Drawing.SystemColors.Control;
            this.labelMinSdkVersion.Name = "labelMinSdkVersion";
            // 
            // labelTargetSdkVersion
            // 
            resources.ApplyResources(this.labelTargetSdkVersion, "labelTargetSdkVersion");
            this.labelTargetSdkVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelTargetSdkVersion.ForeColor = System.Drawing.SystemColors.Control;
            this.labelTargetSdkVersion.Name = "labelTargetSdkVersion";
            // 
            // comboBoxOrientation
            // 
            resources.ApplyResources(this.comboBoxOrientation, "comboBoxOrientation");
            this.comboBoxOrientation.DisplayMember = " ";
            this.comboBoxOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOrientation.FormattingEnabled = true;
            this.comboBoxOrientation.Items.AddRange(new object[] {
            resources.GetString("comboBoxOrientation.Items"),
            resources.GetString("comboBoxOrientation.Items1"),
            resources.GetString("comboBoxOrientation.Items2")});
            this.comboBoxOrientation.Name = "comboBoxOrientation";
            this.comboBoxOrientation.ValueMember = " ";
            this.comboBoxOrientation.SelectedIndexChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // comboBoxFullscreen
            // 
            resources.ApplyResources(this.comboBoxFullscreen, "comboBoxFullscreen");
            this.comboBoxFullscreen.DisplayMember = " ";
            this.comboBoxFullscreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFullscreen.FormattingEnabled = true;
            this.comboBoxFullscreen.Items.AddRange(new object[] {
            resources.GetString("comboBoxFullscreen.Items"),
            resources.GetString("comboBoxFullscreen.Items1")});
            this.comboBoxFullscreen.Name = "comboBoxFullscreen";
            this.comboBoxFullscreen.ValueMember = " ";
            this.comboBoxFullscreen.SelectedIndexChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // comboBoxMinSdkVersion
            // 
            resources.ApplyResources(this.comboBoxMinSdkVersion, "comboBoxMinSdkVersion");
            this.comboBoxMinSdkVersion.DisplayMember = " ";
            this.comboBoxMinSdkVersion.FormattingEnabled = true;
            this.comboBoxMinSdkVersion.Items.AddRange(new object[] {
            resources.GetString("comboBoxMinSdkVersion.Items"),
            resources.GetString("comboBoxMinSdkVersion.Items1"),
            resources.GetString("comboBoxMinSdkVersion.Items2"),
            resources.GetString("comboBoxMinSdkVersion.Items3"),
            resources.GetString("comboBoxMinSdkVersion.Items4"),
            resources.GetString("comboBoxMinSdkVersion.Items5"),
            resources.GetString("comboBoxMinSdkVersion.Items6"),
            resources.GetString("comboBoxMinSdkVersion.Items7"),
            resources.GetString("comboBoxMinSdkVersion.Items8"),
            resources.GetString("comboBoxMinSdkVersion.Items9"),
            resources.GetString("comboBoxMinSdkVersion.Items10"),
            resources.GetString("comboBoxMinSdkVersion.Items11"),
            resources.GetString("comboBoxMinSdkVersion.Items12"),
            resources.GetString("comboBoxMinSdkVersion.Items13"),
            resources.GetString("comboBoxMinSdkVersion.Items14"),
            resources.GetString("comboBoxMinSdkVersion.Items15"),
            resources.GetString("comboBoxMinSdkVersion.Items16"),
            resources.GetString("comboBoxMinSdkVersion.Items17"),
            resources.GetString("comboBoxMinSdkVersion.Items18"),
            resources.GetString("comboBoxMinSdkVersion.Items19"),
            resources.GetString("comboBoxMinSdkVersion.Items20"),
            resources.GetString("comboBoxMinSdkVersion.Items21")});
            this.comboBoxMinSdkVersion.Name = "comboBoxMinSdkVersion";
            this.comboBoxMinSdkVersion.ValueMember = " ";
            this.comboBoxMinSdkVersion.SelectedIndexChanged += new System.EventHandler(this.comboBoxMinSdkVersion_SelectedIndexChanged);
            this.comboBoxMinSdkVersion.TextChanged += new System.EventHandler(this.comboBoxMinSdkVersion_TextChanged);
            // 
            // comboBoxTargetSdkVersion
            // 
            resources.ApplyResources(this.comboBoxTargetSdkVersion, "comboBoxTargetSdkVersion");
            this.comboBoxTargetSdkVersion.DisplayMember = " ";
            this.comboBoxTargetSdkVersion.FormattingEnabled = true;
            this.comboBoxTargetSdkVersion.Items.AddRange(new object[] {
            resources.GetString("comboBoxTargetSdkVersion.Items"),
            resources.GetString("comboBoxTargetSdkVersion.Items1"),
            resources.GetString("comboBoxTargetSdkVersion.Items2"),
            resources.GetString("comboBoxTargetSdkVersion.Items3"),
            resources.GetString("comboBoxTargetSdkVersion.Items4"),
            resources.GetString("comboBoxTargetSdkVersion.Items5"),
            resources.GetString("comboBoxTargetSdkVersion.Items6"),
            resources.GetString("comboBoxTargetSdkVersion.Items7"),
            resources.GetString("comboBoxTargetSdkVersion.Items8"),
            resources.GetString("comboBoxTargetSdkVersion.Items9"),
            resources.GetString("comboBoxTargetSdkVersion.Items10"),
            resources.GetString("comboBoxTargetSdkVersion.Items11"),
            resources.GetString("comboBoxTargetSdkVersion.Items12"),
            resources.GetString("comboBoxTargetSdkVersion.Items13"),
            resources.GetString("comboBoxTargetSdkVersion.Items14"),
            resources.GetString("comboBoxTargetSdkVersion.Items15"),
            resources.GetString("comboBoxTargetSdkVersion.Items16"),
            resources.GetString("comboBoxTargetSdkVersion.Items17"),
            resources.GetString("comboBoxTargetSdkVersion.Items18"),
            resources.GetString("comboBoxTargetSdkVersion.Items19"),
            resources.GetString("comboBoxTargetSdkVersion.Items20"),
            resources.GetString("comboBoxTargetSdkVersion.Items21")});
            this.comboBoxTargetSdkVersion.Name = "comboBoxTargetSdkVersion";
            this.comboBoxTargetSdkVersion.ValueMember = " ";
            this.comboBoxTargetSdkVersion.SelectedIndexChanged += new System.EventHandler(this.comboBoxTargetSdkVersion_SelectedIndexChanged);
            // 
            // labelSettingGameFolder
            // 
            resources.ApplyResources(this.labelSettingGameFolder, "labelSettingGameFolder");
            this.labelSettingGameFolder.BackColor = System.Drawing.Color.Transparent;
            this.labelSettingGameFolder.ForeColor = System.Drawing.SystemColors.Control;
            this.labelSettingGameFolder.Name = "labelSettingGameFolder";
            // 
            // textBoxSettingGameFolder
            // 
            resources.ApplyResources(this.textBoxSettingGameFolder, "textBoxSettingGameFolder");
            this.textBoxSettingGameFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSettingGameFolder.Name = "textBoxSettingGameFolder";
            this.textBoxSettingGameFolder.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // buttonOpenFileBrowser
            // 
            resources.ApplyResources(this.buttonOpenFileBrowser, "buttonOpenFileBrowser");
            this.buttonOpenFileBrowser.Name = "buttonOpenFileBrowser";
            this.buttonOpenFileBrowser.UseVisualStyleBackColor = true;
            this.buttonOpenFileBrowser.Click += new System.EventHandler(this.buttonFolderBrowser_Click);
            // 
            // labelBuildMode
            // 
            resources.ApplyResources(this.labelBuildMode, "labelBuildMode");
            this.labelBuildMode.BackColor = System.Drawing.Color.Transparent;
            this.labelBuildMode.ForeColor = System.Drawing.SystemColors.Control;
            this.labelBuildMode.Name = "labelBuildMode";
            // 
            // comboBoxBuildMode
            // 
            resources.ApplyResources(this.comboBoxBuildMode, "comboBoxBuildMode");
            this.comboBoxBuildMode.DisplayMember = " ";
            this.comboBoxBuildMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBuildMode.FormattingEnabled = true;
            this.comboBoxBuildMode.Items.AddRange(new object[] {
            resources.GetString("comboBoxBuildMode.Items"),
            resources.GetString("comboBoxBuildMode.Items1")});
            this.comboBoxBuildMode.Name = "comboBoxBuildMode";
            this.comboBoxBuildMode.ValueMember = " ";
            this.comboBoxBuildMode.SelectedIndexChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // labelPluginName
            // 
            resources.ApplyResources(this.labelPluginName, "labelPluginName");
            this.labelPluginName.BackColor = System.Drawing.Color.Transparent;
            this.labelPluginName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelPluginName.Name = "labelPluginName";
            // 
            // buttonAddPlugin
            // 
            resources.ApplyResources(this.buttonAddPlugin, "buttonAddPlugin");
            this.buttonAddPlugin.Name = "buttonAddPlugin";
            this.buttonAddPlugin.Click += new System.EventHandler(this.buttonAddPlugin_Click);
            // 
            // textBoxPluginName
            // 
            resources.ApplyResources(this.textBoxPluginName, "textBoxPluginName");
            this.textBoxPluginName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPluginName.Name = "textBoxPluginName";
            // 
            // listBoxPlugins
            // 
            resources.ApplyResources(this.listBoxPlugins, "listBoxPlugins");
            this.listBoxPlugins.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxPlugins.FormattingEnabled = true;
            this.listBoxPlugins.Items.AddRange(new object[] {
            resources.GetString("listBoxPlugins.Items")});
            this.listBoxPlugins.Name = "listBoxPlugins";
            this.listBoxPlugins.SelectedIndexChanged += new System.EventHandler(this.listBoxPlugins_SelectedIndexChanged);
            // 
            // buttonDeletePlugin
            // 
            resources.ApplyResources(this.buttonDeletePlugin, "buttonDeletePlugin");
            this.buttonDeletePlugin.Name = "buttonDeletePlugin";
            this.buttonDeletePlugin.Click += new System.EventHandler(this.buttonDeletePlugin_Click);
            // 
            // timerBackground
            // 
            this.timerBackground.Tick += new System.EventHandler(this.timerBackground_Tick);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Name = "label1";
            // 
            // panelPlugins
            // 
            resources.ApplyResources(this.panelPlugins, "panelPlugins");
            this.panelPlugins.BackColor = System.Drawing.Color.Transparent;
            this.panelPlugins.Controls.Add(this.buttonSettings);
            this.panelPlugins.Controls.Add(this.listBoxPlugins);
            this.panelPlugins.Controls.Add(this.label1);
            this.panelPlugins.Controls.Add(this.labelPluginName);
            this.panelPlugins.Controls.Add(this.buttonDeletePlugin);
            this.panelPlugins.Controls.Add(this.textBoxPluginName);
            this.panelPlugins.Controls.Add(this.buttonAddPlugin);
            this.panelPlugins.Controls.Add(this.buttonBuild);
            this.panelPlugins.Name = "panelPlugins";
            this.panelPlugins.SectionHeader = null;
            // 
            // buttonSettings
            // 
            resources.ApplyResources(this.buttonSettings, "buttonSettings");
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // labelCompileSdkVersion
            // 
            resources.ApplyResources(this.labelCompileSdkVersion, "labelCompileSdkVersion");
            this.labelCompileSdkVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelCompileSdkVersion.ForeColor = System.Drawing.SystemColors.Control;
            this.labelCompileSdkVersion.Name = "labelCompileSdkVersion";
            // 
            // comboBoxCompileSdkVersion
            // 
            resources.ApplyResources(this.comboBoxCompileSdkVersion, "comboBoxCompileSdkVersion");
            this.comboBoxCompileSdkVersion.DisplayMember = " ";
            this.comboBoxCompileSdkVersion.FormattingEnabled = true;
            this.comboBoxCompileSdkVersion.Items.AddRange(new object[] {
            resources.GetString("comboBoxCompileSdkVersion.Items"),
            resources.GetString("comboBoxCompileSdkVersion.Items1"),
            resources.GetString("comboBoxCompileSdkVersion.Items2"),
            resources.GetString("comboBoxCompileSdkVersion.Items3"),
            resources.GetString("comboBoxCompileSdkVersion.Items4"),
            resources.GetString("comboBoxCompileSdkVersion.Items5"),
            resources.GetString("comboBoxCompileSdkVersion.Items6"),
            resources.GetString("comboBoxCompileSdkVersion.Items7"),
            resources.GetString("comboBoxCompileSdkVersion.Items8"),
            resources.GetString("comboBoxCompileSdkVersion.Items9"),
            resources.GetString("comboBoxCompileSdkVersion.Items10"),
            resources.GetString("comboBoxCompileSdkVersion.Items11"),
            resources.GetString("comboBoxCompileSdkVersion.Items12"),
            resources.GetString("comboBoxCompileSdkVersion.Items13"),
            resources.GetString("comboBoxCompileSdkVersion.Items14"),
            resources.GetString("comboBoxCompileSdkVersion.Items15"),
            resources.GetString("comboBoxCompileSdkVersion.Items16"),
            resources.GetString("comboBoxCompileSdkVersion.Items17"),
            resources.GetString("comboBoxCompileSdkVersion.Items18"),
            resources.GetString("comboBoxCompileSdkVersion.Items19"),
            resources.GetString("comboBoxCompileSdkVersion.Items20"),
            resources.GetString("comboBoxCompileSdkVersion.Items21")});
            this.comboBoxCompileSdkVersion.Name = "comboBoxCompileSdkVersion";
            this.comboBoxCompileSdkVersion.ValueMember = " ";
            this.comboBoxCompileSdkVersion.SelectedIndexChanged += new System.EventHandler(this.comboBoxCompileSdkVersion_SelectedIndexChanged);
            // 
            // panelBuildLog
            // 
            resources.ApplyResources(this.panelBuildLog, "panelBuildLog");
            this.panelBuildLog.BackColor = System.Drawing.Color.Transparent;
            this.panelBuildLog.Controls.Add(this.textBox1);
            this.panelBuildLog.Name = "panelBuildLog";
            this.panelBuildLog.SectionHeader = "Log";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.Controls.Add(this.label_keyC, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label_keyS, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label_keyL, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label_keyO, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label_keyOU, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelKeyPassword, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelKeyAlias, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelKeyPath, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelGameName, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelPackageName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelFolderName, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.Controls.Add(this.labelBuildMode, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.labelSettingGameFolder, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.labelCompileSdkVersion, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.labelTargetSdkVersion, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelMinSdkVersion, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelFullscreen, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelOrientation, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelBuildLog);
            this.Controls.Add(this.comboBoxCompileSdkVersion);
            this.Controls.Add(this.panelPlugins);
            this.Controls.Add(this.comboBoxBuildMode);
            this.Controls.Add(this.buttonOpenFileBrowser);
            this.Controls.Add(this.textBoxSettingGameFolder);
            this.Controls.Add(this.comboBoxTargetSdkVersion);
            this.Controls.Add(this.comboBoxMinSdkVersion);
            this.Controls.Add(this.comboBoxFullscreen);
            this.Controls.Add(this.comboBoxOrientation);
            this.Controls.Add(this.textBox_keyC);
            this.Controls.Add(this.textBox_keyS);
            this.Controls.Add(this.textBox_keyL);
            this.Controls.Add(this.textBox_keyO);
            this.Controls.Add(this.textBox_keyOU);
            this.Controls.Add(this.textBoxPassWord);
            this.Controls.Add(this.textBoxKeyAlias);
            this.Controls.Add(this.textBoxKeyPath);
            this.Controls.Add(this.textBoxGameName);
            this.Controls.Add(this.textBoxPackageName);
            this.Controls.Add(this.textBoxFolderName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panelPlugins.ResumeLayout(false);
            this.panelPlugins.PerformLayout();
            this.panelBuildLog.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.RichTextBox textBox1;
        private System.Windows.Forms.Label labelFolderName;
        private System.Windows.Forms.Label labelPackageName;
        private System.Windows.Forms.Label labelGameName;
        private System.Windows.Forms.Label labelKeyPath;
        private System.Windows.Forms.Label labelKeyAlias;
        private System.Windows.Forms.Label labelKeyPassword;
        private System.Windows.Forms.Label label_keyOU;
        private System.Windows.Forms.Label label_keyO;
        private System.Windows.Forms.Label label_keyL;
        private System.Windows.Forms.Label label_keyS;
        private DarkUI.Controls.DarkTextBox textBoxFolderName;
        private System.Windows.Forms.TextBox textBoxPackageName;
        private System.Windows.Forms.TextBox textBoxGameName;
        private System.Windows.Forms.TextBox textBoxKeyPath;
        private System.Windows.Forms.TextBox textBoxKeyAlias;
        private System.Windows.Forms.TextBox textBoxPassWord;
        private System.Windows.Forms.TextBox textBox_keyOU;
        private System.Windows.Forms.TextBox textBox_keyO;
        private System.Windows.Forms.TextBox textBox_keyL;
        private System.Windows.Forms.TextBox textBox_keyS;
        private System.Windows.Forms.TextBox textBox_keyC;
        private System.Windows.Forms.Label label_keyC;
        private System.Windows.Forms.Label labelOrientation;
        private System.Windows.Forms.Label labelFullscreen;
        private System.Windows.Forms.Label labelMinSdkVersion;
        private System.Windows.Forms.Label labelTargetSdkVersion;
        private System.Windows.Forms.ComboBox comboBoxOrientation;
        private System.Windows.Forms.ComboBox comboBoxFullscreen;
        private System.Windows.Forms.ComboBox comboBoxMinSdkVersion;
        private System.Windows.Forms.ComboBox comboBoxTargetSdkVersion;
        private System.Windows.Forms.Label labelSettingGameFolder;
        private System.Windows.Forms.TextBox textBoxSettingGameFolder;
        private System.Windows.Forms.Button buttonOpenFileBrowser;
        private System.Windows.Forms.Label labelBuildMode;
        private System.Windows.Forms.ComboBox comboBoxBuildMode;
        private System.Windows.Forms.Label labelPluginName;
        private System.Windows.Forms.TextBox textBoxPluginName;
        private System.Windows.Forms.ListBox listBoxPlugins;
        private System.Windows.Forms.Timer timerBackground;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCompileSdkVersion;
        private System.Windows.Forms.ComboBox comboBoxCompileSdkVersion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private DarkUI.Controls.DarkButton buttonBuild;
        private DarkUI.Controls.DarkButton buttonSettings;
        private DarkUI.Controls.DarkSectionPanel panelPlugins;
        private DarkUI.Controls.DarkSectionPanel panelBuildLog;
        private DarkUI.Controls.DarkButton buttonAddPlugin;
        private DarkUI.Controls.DarkButton buttonDeletePlugin;
    }
}

