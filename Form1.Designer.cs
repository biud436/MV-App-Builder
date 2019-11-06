namespace Cordova_Builder
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
            this.textBoxFolderName = new System.Windows.Forms.TextBox();
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
            this.buttonBuild = new System.Windows.Forms.Button();
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
            this.buttonAddPlugin = new System.Windows.Forms.Button();
            this.textBoxPluginName = new System.Windows.Forms.TextBox();
            this.listBoxPlugins = new System.Windows.Forms.ListBox();
            this.buttonDeletePlugin = new System.Windows.Forms.Button();
            this.timerBackground = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panelPlugins = new System.Windows.Forms.Panel();
            this.labelCompileSdkVersion = new System.Windows.Forms.Label();
            this.comboBoxCompileSdkVersion = new System.Windows.Forms.ComboBox();
            this.panelBuildLog = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelPlugins.SuspendLayout();
            this.panelBuildLog.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
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
            this.textBoxFolderName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBoxFolderName, "textBoxFolderName");
            this.textBoxFolderName.Name = "textBoxFolderName";
            this.textBoxFolderName.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxPackageName
            // 
            this.textBoxPackageName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBoxPackageName, "textBoxPackageName");
            this.textBoxPackageName.Name = "textBoxPackageName";
            this.textBoxPackageName.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxGameName
            // 
            this.textBoxGameName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBoxGameName, "textBoxGameName");
            this.textBoxGameName.Name = "textBoxGameName";
            this.textBoxGameName.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxKeyPath
            // 
            this.textBoxKeyPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBoxKeyPath, "textBoxKeyPath");
            this.textBoxKeyPath.Name = "textBoxKeyPath";
            this.textBoxKeyPath.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxKeyAlias
            // 
            this.textBoxKeyAlias.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBoxKeyAlias, "textBoxKeyAlias");
            this.textBoxKeyAlias.Name = "textBoxKeyAlias";
            this.textBoxKeyAlias.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxPassWord
            // 
            this.textBoxPassWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBoxPassWord, "textBoxPassWord");
            this.textBoxPassWord.Name = "textBoxPassWord";
            this.textBoxPassWord.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyOU
            // 
            this.textBox_keyOU.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox_keyOU, "textBox_keyOU");
            this.textBox_keyOU.Name = "textBox_keyOU";
            this.textBox_keyOU.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyO
            // 
            this.textBox_keyO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox_keyO, "textBox_keyO");
            this.textBox_keyO.Name = "textBox_keyO";
            this.textBox_keyO.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyL
            // 
            this.textBox_keyL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox_keyL, "textBox_keyL");
            this.textBox_keyL.Name = "textBox_keyL";
            this.textBox_keyL.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyS
            // 
            this.textBox_keyS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox_keyS, "textBox_keyS");
            this.textBox_keyS.Name = "textBox_keyS";
            this.textBox_keyS.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyC
            // 
            this.textBox_keyC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox_keyC, "textBox_keyC");
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
            this.buttonBuild.UseVisualStyleBackColor = true;
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
            this.comboBoxOrientation.DisplayMember = " ";
            this.comboBoxOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOrientation.FormattingEnabled = true;
            this.comboBoxOrientation.Items.AddRange(new object[] {
            resources.GetString("comboBoxOrientation.Items"),
            resources.GetString("comboBoxOrientation.Items1"),
            resources.GetString("comboBoxOrientation.Items2")});
            resources.ApplyResources(this.comboBoxOrientation, "comboBoxOrientation");
            this.comboBoxOrientation.Name = "comboBoxOrientation";
            this.comboBoxOrientation.ValueMember = " ";
            this.comboBoxOrientation.SelectedIndexChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // comboBoxFullscreen
            // 
            this.comboBoxFullscreen.DisplayMember = " ";
            this.comboBoxFullscreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFullscreen.FormattingEnabled = true;
            this.comboBoxFullscreen.Items.AddRange(new object[] {
            resources.GetString("comboBoxFullscreen.Items"),
            resources.GetString("comboBoxFullscreen.Items1")});
            resources.ApplyResources(this.comboBoxFullscreen, "comboBoxFullscreen");
            this.comboBoxFullscreen.Name = "comboBoxFullscreen";
            this.comboBoxFullscreen.ValueMember = " ";
            this.comboBoxFullscreen.SelectedIndexChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // comboBoxMinSdkVersion
            // 
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
            resources.ApplyResources(this.comboBoxMinSdkVersion, "comboBoxMinSdkVersion");
            this.comboBoxMinSdkVersion.Name = "comboBoxMinSdkVersion";
            this.comboBoxMinSdkVersion.ValueMember = " ";
            this.comboBoxMinSdkVersion.SelectedIndexChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            this.comboBoxMinSdkVersion.TextChanged += new System.EventHandler(this.comboBoxMinSdkVersion_TextChanged);
            // 
            // comboBoxTargetSdkVersion
            // 
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
            resources.ApplyResources(this.comboBoxTargetSdkVersion, "comboBoxTargetSdkVersion");
            this.comboBoxTargetSdkVersion.Name = "comboBoxTargetSdkVersion";
            this.comboBoxTargetSdkVersion.ValueMember = " ";
            this.comboBoxTargetSdkVersion.SelectedIndexChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
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
            this.textBoxSettingGameFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxSettingGameFolder, "textBoxSettingGameFolder");
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
            this.comboBoxBuildMode.DisplayMember = " ";
            this.comboBoxBuildMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBuildMode.FormattingEnabled = true;
            this.comboBoxBuildMode.Items.AddRange(new object[] {
            resources.GetString("comboBoxBuildMode.Items"),
            resources.GetString("comboBoxBuildMode.Items1")});
            resources.ApplyResources(this.comboBoxBuildMode, "comboBoxBuildMode");
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
            this.buttonAddPlugin.UseVisualStyleBackColor = true;
            this.buttonAddPlugin.Click += new System.EventHandler(this.buttonAddPlugin_Click);
            // 
            // textBoxPluginName
            // 
            this.textBoxPluginName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxPluginName, "textBoxPluginName");
            this.textBoxPluginName.Name = "textBoxPluginName";
            // 
            // listBoxPlugins
            // 
            this.listBoxPlugins.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxPlugins.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxPlugins, "listBoxPlugins");
            this.listBoxPlugins.Items.AddRange(new object[] {
            resources.GetString("listBoxPlugins.Items")});
            this.listBoxPlugins.Name = "listBoxPlugins";
            this.listBoxPlugins.SelectedIndexChanged += new System.EventHandler(this.listBoxPlugins_SelectedIndexChanged);
            // 
            // buttonDeletePlugin
            // 
            resources.ApplyResources(this.buttonDeletePlugin, "buttonDeletePlugin");
            this.buttonDeletePlugin.Name = "buttonDeletePlugin";
            this.buttonDeletePlugin.UseVisualStyleBackColor = true;
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
            this.panelPlugins.BackColor = System.Drawing.Color.Transparent;
            this.panelPlugins.Controls.Add(this.listBoxPlugins);
            this.panelPlugins.Controls.Add(this.label1);
            this.panelPlugins.Controls.Add(this.labelPluginName);
            this.panelPlugins.Controls.Add(this.buttonDeletePlugin);
            this.panelPlugins.Controls.Add(this.textBoxPluginName);
            this.panelPlugins.Controls.Add(this.buttonAddPlugin);
            this.panelPlugins.Controls.Add(this.buttonBuild);
            resources.ApplyResources(this.panelPlugins, "panelPlugins");
            this.panelPlugins.Name = "panelPlugins";
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
            resources.ApplyResources(this.comboBoxCompileSdkVersion, "comboBoxCompileSdkVersion");
            this.comboBoxCompileSdkVersion.Name = "comboBoxCompileSdkVersion";
            this.comboBoxCompileSdkVersion.ValueMember = " ";
            // 
            // panelBuildLog
            // 
            this.panelBuildLog.BackColor = System.Drawing.Color.Transparent;
            this.panelBuildLog.Controls.Add(this.textBox1);
            resources.ApplyResources(this.panelBuildLog, "panelBuildLog");
            this.panelBuildLog.Name = "panelBuildLog";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.labelFolderName);
            this.panel1.Controls.Add(this.labelPackageName);
            this.panel1.Controls.Add(this.labelGameName);
            this.panel1.Controls.Add(this.labelKeyPath);
            this.panel1.Controls.Add(this.labelKeyAlias);
            this.panel1.Controls.Add(this.labelKeyPassword);
            this.panel1.Controls.Add(this.label_keyOU);
            this.panel1.Controls.Add(this.label_keyO);
            this.panel1.Controls.Add(this.label_keyL);
            this.panel1.Controls.Add(this.label_keyS);
            this.panel1.Controls.Add(this.label_keyC);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.labelOrientation);
            this.panel2.Controls.Add(this.labelFullscreen);
            this.panel2.Controls.Add(this.labelMinSdkVersion);
            this.panel2.Controls.Add(this.labelTargetSdkVersion);
            this.panel2.Controls.Add(this.labelCompileSdkVersion);
            this.panel2.Controls.Add(this.labelSettingGameFolder);
            this.panel2.Controls.Add(this.labelBuildMode);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
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
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelPlugins.ResumeLayout(false);
            this.panelPlugins.PerformLayout();
            this.panelBuildLog.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.TextBox textBoxFolderName;
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
        private System.Windows.Forms.Button buttonBuild;
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
        private System.Windows.Forms.Button buttonAddPlugin;
        private System.Windows.Forms.TextBox textBoxPluginName;
        private System.Windows.Forms.ListBox listBoxPlugins;
        private System.Windows.Forms.Button buttonDeletePlugin;
        private System.Windows.Forms.Timer timerBackground;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelPlugins;
        private System.Windows.Forms.Label labelCompileSdkVersion;
        private System.Windows.Forms.ComboBox comboBoxCompileSdkVersion;
        private System.Windows.Forms.Panel panelBuildLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

