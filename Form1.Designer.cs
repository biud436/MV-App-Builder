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
            this.panelPlugins.SuspendLayout();
            this.panelBuildLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(960, 123);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // labelFolderName
            // 
            this.labelFolderName.AutoSize = true;
            this.labelFolderName.BackColor = System.Drawing.Color.Transparent;
            this.labelFolderName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelFolderName.Location = new System.Drawing.Point(70, 24);
            this.labelFolderName.Name = "labelFolderName";
            this.labelFolderName.Size = new System.Drawing.Size(41, 12);
            this.labelFolderName.TabIndex = 1;
            this.labelFolderName.Text = "폴더명";
            // 
            // labelPackageName
            // 
            this.labelPackageName.AutoSize = true;
            this.labelPackageName.BackColor = System.Drawing.Color.Transparent;
            this.labelPackageName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelPackageName.Location = new System.Drawing.Point(54, 65);
            this.labelPackageName.Name = "labelPackageName";
            this.labelPackageName.Size = new System.Drawing.Size(57, 12);
            this.labelPackageName.TabIndex = 2;
            this.labelPackageName.Text = "패키지 명";
            // 
            // labelGameName
            // 
            this.labelGameName.AutoSize = true;
            this.labelGameName.BackColor = System.Drawing.Color.Transparent;
            this.labelGameName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelGameName.Location = new System.Drawing.Point(70, 106);
            this.labelGameName.Name = "labelGameName";
            this.labelGameName.Size = new System.Drawing.Size(41, 12);
            this.labelGameName.TabIndex = 3;
            this.labelGameName.Text = "게임명";
            // 
            // labelKeyPath
            // 
            this.labelKeyPath.AutoSize = true;
            this.labelKeyPath.BackColor = System.Drawing.Color.Transparent;
            this.labelKeyPath.ForeColor = System.Drawing.SystemColors.Control;
            this.labelKeyPath.Location = new System.Drawing.Point(15, 149);
            this.labelKeyPath.Name = "labelKeyPath";
            this.labelKeyPath.Size = new System.Drawing.Size(97, 12);
            this.labelKeyPath.TabIndex = 4;
            this.labelKeyPath.Text = "키스토어 파일 명";
            // 
            // labelKeyAlias
            // 
            this.labelKeyAlias.AutoSize = true;
            this.labelKeyAlias.BackColor = System.Drawing.Color.Transparent;
            this.labelKeyAlias.ForeColor = System.Drawing.SystemColors.Control;
            this.labelKeyAlias.Location = new System.Drawing.Point(30, 190);
            this.labelKeyAlias.Name = "labelKeyAlias";
            this.labelKeyAlias.Size = new System.Drawing.Size(81, 12);
            this.labelKeyAlias.TabIndex = 5;
            this.labelKeyAlias.Text = "키스토어 별칭";
            // 
            // labelKeyPassword
            // 
            this.labelKeyPassword.AutoSize = true;
            this.labelKeyPassword.BackColor = System.Drawing.Color.Transparent;
            this.labelKeyPassword.ForeColor = System.Drawing.SystemColors.Control;
            this.labelKeyPassword.Location = new System.Drawing.Point(9, 234);
            this.labelKeyPassword.Name = "labelKeyPassword";
            this.labelKeyPassword.Size = new System.Drawing.Size(105, 12);
            this.labelKeyPassword.TabIndex = 6;
            this.labelKeyPassword.Text = "키스토어 비밀번호";
            // 
            // label_keyOU
            // 
            this.label_keyOU.AutoSize = true;
            this.label_keyOU.BackColor = System.Drawing.Color.Transparent;
            this.label_keyOU.ForeColor = System.Drawing.SystemColors.Control;
            this.label_keyOU.Location = new System.Drawing.Point(38, 276);
            this.label_keyOU.Name = "label_keyOU";
            this.label_keyOU.Size = new System.Drawing.Size(73, 12);
            this.label_keyOU.TabIndex = 7;
            this.label_keyOU.Text = "조직 단위 명";
            // 
            // label_keyO
            // 
            this.label_keyO.AutoSize = true;
            this.label_keyO.BackColor = System.Drawing.Color.Transparent;
            this.label_keyO.ForeColor = System.Drawing.SystemColors.Control;
            this.label_keyO.Location = new System.Drawing.Point(54, 318);
            this.label_keyO.Name = "label_keyO";
            this.label_keyO.Size = new System.Drawing.Size(57, 12);
            this.label_keyO.TabIndex = 8;
            this.label_keyO.Text = "조직 이름";
            // 
            // label_keyL
            // 
            this.label_keyL.AutoSize = true;
            this.label_keyL.BackColor = System.Drawing.Color.Transparent;
            this.label_keyL.ForeColor = System.Drawing.SystemColors.Control;
            this.label_keyL.Location = new System.Drawing.Point(24, 362);
            this.label_keyL.Name = "label_keyL";
            this.label_keyL.Size = new System.Drawing.Size(87, 12);
            this.label_keyL.TabIndex = 9;
            this.label_keyL.Text = "구/군/시/ 이름";
            // 
            // label_keyS
            // 
            this.label_keyS.AutoSize = true;
            this.label_keyS.BackColor = System.Drawing.Color.Transparent;
            this.label_keyS.ForeColor = System.Drawing.SystemColors.Control;
            this.label_keyS.Location = new System.Drawing.Point(48, 403);
            this.label_keyS.Name = "label_keyS";
            this.label_keyS.Size = new System.Drawing.Size(63, 12);
            this.label_keyS.TabIndex = 10;
            this.label_keyS.Text = "시/도 이름";
            // 
            // textBoxFolderName
            // 
            this.textBoxFolderName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFolderName.Location = new System.Drawing.Point(119, 22);
            this.textBoxFolderName.Name = "textBoxFolderName";
            this.textBoxFolderName.Size = new System.Drawing.Size(342, 14);
            this.textBoxFolderName.TabIndex = 11;
            this.textBoxFolderName.Text = "test";
            this.textBoxFolderName.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxPackageName
            // 
            this.textBoxPackageName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPackageName.Location = new System.Drawing.Point(119, 63);
            this.textBoxPackageName.Name = "textBoxPackageName";
            this.textBoxPackageName.Size = new System.Drawing.Size(342, 14);
            this.textBoxPackageName.TabIndex = 12;
            this.textBoxPackageName.Text = "me.biud436.testgame";
            this.textBoxPackageName.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxGameName
            // 
            this.textBoxGameName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxGameName.Location = new System.Drawing.Point(118, 105);
            this.textBoxGameName.Name = "textBoxGameName";
            this.textBoxGameName.Size = new System.Drawing.Size(342, 14);
            this.textBoxGameName.TabIndex = 13;
            this.textBoxGameName.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxKeyPath
            // 
            this.textBoxKeyPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxKeyPath.Location = new System.Drawing.Point(117, 146);
            this.textBoxKeyPath.Name = "textBoxKeyPath";
            this.textBoxKeyPath.Size = new System.Drawing.Size(342, 14);
            this.textBoxKeyPath.TabIndex = 14;
            this.textBoxKeyPath.Text = "android.keystore";
            this.textBoxKeyPath.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxKeyAlias
            // 
            this.textBoxKeyAlias.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxKeyAlias.Location = new System.Drawing.Point(117, 189);
            this.textBoxKeyAlias.Name = "textBoxKeyAlias";
            this.textBoxKeyAlias.Size = new System.Drawing.Size(342, 14);
            this.textBoxKeyAlias.TabIndex = 15;
            this.textBoxKeyAlias.Text = "biud436";
            this.textBoxKeyAlias.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBoxPassWord
            // 
            this.textBoxPassWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPassWord.Location = new System.Drawing.Point(117, 232);
            this.textBoxPassWord.Name = "textBoxPassWord";
            this.textBoxPassWord.PasswordChar = '*';
            this.textBoxPassWord.Size = new System.Drawing.Size(342, 14);
            this.textBoxPassWord.TabIndex = 16;
            this.textBoxPassWord.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyOU
            // 
            this.textBox_keyOU.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_keyOU.Location = new System.Drawing.Point(117, 275);
            this.textBox_keyOU.Name = "textBox_keyOU";
            this.textBox_keyOU.Size = new System.Drawing.Size(342, 14);
            this.textBox_keyOU.TabIndex = 17;
            this.textBox_keyOU.Text = "biud436";
            this.textBox_keyOU.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyO
            // 
            this.textBox_keyO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_keyO.Location = new System.Drawing.Point(117, 318);
            this.textBox_keyO.Name = "textBox_keyO";
            this.textBox_keyO.Size = new System.Drawing.Size(342, 14);
            this.textBox_keyO.TabIndex = 18;
            this.textBox_keyO.Text = "biud436";
            this.textBox_keyO.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyL
            // 
            this.textBox_keyL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_keyL.Location = new System.Drawing.Point(117, 360);
            this.textBox_keyL.Name = "textBox_keyL";
            this.textBox_keyL.Size = new System.Drawing.Size(342, 14);
            this.textBox_keyL.TabIndex = 19;
            this.textBox_keyL.Text = "Siheung";
            this.textBox_keyL.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyS
            // 
            this.textBox_keyS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_keyS.Location = new System.Drawing.Point(116, 402);
            this.textBox_keyS.Name = "textBox_keyS";
            this.textBox_keyS.Size = new System.Drawing.Size(342, 14);
            this.textBox_keyS.TabIndex = 20;
            this.textBox_keyS.Text = "Gyeonggi";
            this.textBox_keyS.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // textBox_keyC
            // 
            this.textBox_keyC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_keyC.Location = new System.Drawing.Point(116, 445);
            this.textBox_keyC.Name = "textBox_keyC";
            this.textBox_keyC.Size = new System.Drawing.Size(342, 14);
            this.textBox_keyC.TabIndex = 22;
            this.textBox_keyC.Text = "ko";
            this.textBox_keyC.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // label_keyC
            // 
            this.label_keyC.AutoSize = true;
            this.label_keyC.BackColor = System.Drawing.Color.Transparent;
            this.label_keyC.ForeColor = System.Drawing.SystemColors.Control;
            this.label_keyC.Location = new System.Drawing.Point(24, 445);
            this.label_keyC.Name = "label_keyC";
            this.label_keyC.Size = new System.Drawing.Size(87, 12);
            this.label_keyC.TabIndex = 21;
            this.label_keyC.Text = "국가/지역 코드";
            // 
            // buttonBuild
            // 
            this.buttonBuild.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonBuild.Enabled = false;
            this.buttonBuild.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonBuild.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonBuild.Location = new System.Drawing.Point(0, 116);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(440, 30);
            this.buttonBuild.TabIndex = 24;
            this.buttonBuild.Text = "빌드";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.buttonBuild_Click);
            // 
            // labelOrientation
            // 
            this.labelOrientation.AutoSize = true;
            this.labelOrientation.BackColor = System.Drawing.Color.Transparent;
            this.labelOrientation.ForeColor = System.Drawing.SystemColors.Control;
            this.labelOrientation.Location = new System.Drawing.Point(527, 22);
            this.labelOrientation.Name = "labelOrientation";
            this.labelOrientation.Size = new System.Drawing.Size(57, 12);
            this.labelOrientation.TabIndex = 25;
            this.labelOrientation.Text = "화면 방향";
            // 
            // labelFullscreen
            // 
            this.labelFullscreen.AutoSize = true;
            this.labelFullscreen.BackColor = System.Drawing.Color.Transparent;
            this.labelFullscreen.ForeColor = System.Drawing.SystemColors.Control;
            this.labelFullscreen.Location = new System.Drawing.Point(527, 63);
            this.labelFullscreen.Name = "labelFullscreen";
            this.labelFullscreen.Size = new System.Drawing.Size(57, 12);
            this.labelFullscreen.TabIndex = 26;
            this.labelFullscreen.Text = "전체 화면";
            // 
            // labelMinSdkVersion
            // 
            this.labelMinSdkVersion.AutoSize = true;
            this.labelMinSdkVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelMinSdkVersion.ForeColor = System.Drawing.SystemColors.Control;
            this.labelMinSdkVersion.Location = new System.Drawing.Point(504, 107);
            this.labelMinSdkVersion.Name = "labelMinSdkVersion";
            this.labelMinSdkVersion.Size = new System.Drawing.Size(80, 12);
            this.labelMinSdkVersion.TabIndex = 27;
            this.labelMinSdkVersion.Text = "최소 API 버전";
            // 
            // labelTargetSdkVersion
            // 
            this.labelTargetSdkVersion.AutoSize = true;
            this.labelTargetSdkVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelTargetSdkVersion.ForeColor = System.Drawing.SystemColors.Control;
            this.labelTargetSdkVersion.Location = new System.Drawing.Point(504, 148);
            this.labelTargetSdkVersion.Name = "labelTargetSdkVersion";
            this.labelTargetSdkVersion.Size = new System.Drawing.Size(80, 12);
            this.labelTargetSdkVersion.TabIndex = 28;
            this.labelTargetSdkVersion.Text = "타겟 API 버전";
            // 
            // comboBoxOrientation
            // 
            this.comboBoxOrientation.DisplayMember = " ";
            this.comboBoxOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOrientation.FormattingEnabled = true;
            this.comboBoxOrientation.Items.AddRange(new object[] {
            "any",
            "portrait",
            "landscape"});
            this.comboBoxOrientation.Location = new System.Drawing.Point(598, 19);
            this.comboBoxOrientation.Name = "comboBoxOrientation";
            this.comboBoxOrientation.Size = new System.Drawing.Size(331, 20);
            this.comboBoxOrientation.TabIndex = 29;
            this.comboBoxOrientation.ValueMember = " ";
            this.comboBoxOrientation.SelectedIndexChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // comboBoxFullscreen
            // 
            this.comboBoxFullscreen.DisplayMember = " ";
            this.comboBoxFullscreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFullscreen.FormattingEnabled = true;
            this.comboBoxFullscreen.Items.AddRange(new object[] {
            "true",
            "false"});
            this.comboBoxFullscreen.Location = new System.Drawing.Point(598, 60);
            this.comboBoxFullscreen.Name = "comboBoxFullscreen";
            this.comboBoxFullscreen.Size = new System.Drawing.Size(331, 20);
            this.comboBoxFullscreen.TabIndex = 30;
            this.comboBoxFullscreen.ValueMember = " ";
            this.comboBoxFullscreen.SelectedIndexChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // comboBoxMinSdkVersion
            // 
            this.comboBoxMinSdkVersion.DisplayMember = " ";
            this.comboBoxMinSdkVersion.FormattingEnabled = true;
            this.comboBoxMinSdkVersion.Items.AddRange(new object[] {
            "29",
            "28",
            "27",
            "26",
            "25",
            "24",
            "23",
            "22",
            "21",
            "20",
            "19",
            "18",
            "17",
            "16",
            "15",
            "14",
            "13",
            "12",
            "11",
            "10",
            "9",
            "8"});
            this.comboBoxMinSdkVersion.Location = new System.Drawing.Point(598, 103);
            this.comboBoxMinSdkVersion.Name = "comboBoxMinSdkVersion";
            this.comboBoxMinSdkVersion.Size = new System.Drawing.Size(331, 20);
            this.comboBoxMinSdkVersion.TabIndex = 31;
            this.comboBoxMinSdkVersion.ValueMember = " ";
            this.comboBoxMinSdkVersion.SelectedIndexChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            this.comboBoxMinSdkVersion.TextChanged += new System.EventHandler(this.comboBoxMinSdkVersion_TextChanged);
            // 
            // comboBoxTargetSdkVersion
            // 
            this.comboBoxTargetSdkVersion.DisplayMember = " ";
            this.comboBoxTargetSdkVersion.FormattingEnabled = true;
            this.comboBoxTargetSdkVersion.Items.AddRange(new object[] {
            "29",
            "28",
            "27",
            "26",
            "25",
            "24",
            "23",
            "22",
            "21",
            "20",
            "19",
            "18",
            "17",
            "16",
            "15",
            "14",
            "13",
            "12",
            "11",
            "10",
            "9",
            "8"});
            this.comboBoxTargetSdkVersion.Location = new System.Drawing.Point(598, 143);
            this.comboBoxTargetSdkVersion.Name = "comboBoxTargetSdkVersion";
            this.comboBoxTargetSdkVersion.Size = new System.Drawing.Size(331, 20);
            this.comboBoxTargetSdkVersion.TabIndex = 32;
            this.comboBoxTargetSdkVersion.ValueMember = " ";
            this.comboBoxTargetSdkVersion.SelectedIndexChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // labelSettingGameFolder
            // 
            this.labelSettingGameFolder.AutoSize = true;
            this.labelSettingGameFolder.BackColor = System.Drawing.Color.Transparent;
            this.labelSettingGameFolder.ForeColor = System.Drawing.SystemColors.Control;
            this.labelSettingGameFolder.Location = new System.Drawing.Point(499, 234);
            this.labelSettingGameFolder.Name = "labelSettingGameFolder";
            this.labelSettingGameFolder.Size = new System.Drawing.Size(85, 12);
            this.labelSettingGameFolder.TabIndex = 33;
            this.labelSettingGameFolder.Text = "게임 폴더 설정";
            // 
            // textBoxSettingGameFolder
            // 
            this.textBoxSettingGameFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSettingGameFolder.Location = new System.Drawing.Point(599, 229);
            this.textBoxSettingGameFolder.Name = "textBoxSettingGameFolder";
            this.textBoxSettingGameFolder.Size = new System.Drawing.Size(260, 21);
            this.textBoxSettingGameFolder.TabIndex = 34;
            this.textBoxSettingGameFolder.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // buttonOpenFileBrowser
            // 
            this.buttonOpenFileBrowser.Location = new System.Drawing.Point(863, 229);
            this.buttonOpenFileBrowser.Name = "buttonOpenFileBrowser";
            this.buttonOpenFileBrowser.Size = new System.Drawing.Size(65, 21);
            this.buttonOpenFileBrowser.TabIndex = 35;
            this.buttonOpenFileBrowser.Text = "Browse";
            this.buttonOpenFileBrowser.UseVisualStyleBackColor = true;
            this.buttonOpenFileBrowser.Click += new System.EventHandler(this.buttonFolderBrowser_Click);
            // 
            // labelBuildMode
            // 
            this.labelBuildMode.AutoSize = true;
            this.labelBuildMode.BackColor = System.Drawing.Color.Transparent;
            this.labelBuildMode.ForeColor = System.Drawing.SystemColors.Control;
            this.labelBuildMode.Location = new System.Drawing.Point(526, 277);
            this.labelBuildMode.Name = "labelBuildMode";
            this.labelBuildMode.Size = new System.Drawing.Size(57, 12);
            this.labelBuildMode.TabIndex = 36;
            this.labelBuildMode.Text = "빌드 모드";
            // 
            // comboBoxBuildMode
            // 
            this.comboBoxBuildMode.DisplayMember = " ";
            this.comboBoxBuildMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBuildMode.FormattingEnabled = true;
            this.comboBoxBuildMode.Items.AddRange(new object[] {
            "release",
            "debug"});
            this.comboBoxBuildMode.Location = new System.Drawing.Point(597, 274);
            this.comboBoxBuildMode.Name = "comboBoxBuildMode";
            this.comboBoxBuildMode.Size = new System.Drawing.Size(331, 20);
            this.comboBoxBuildMode.TabIndex = 37;
            this.comboBoxBuildMode.ValueMember = " ";
            this.comboBoxBuildMode.SelectedIndexChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // labelPluginName
            // 
            this.labelPluginName.AutoSize = true;
            this.labelPluginName.BackColor = System.Drawing.Color.Transparent;
            this.labelPluginName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelPluginName.Location = new System.Drawing.Point(12, 94);
            this.labelPluginName.Name = "labelPluginName";
            this.labelPluginName.Size = new System.Drawing.Size(53, 12);
            this.labelPluginName.TabIndex = 38;
            this.labelPluginName.Text = "플러그인";
            // 
            // buttonAddPlugin
            // 
            this.buttonAddPlugin.Location = new System.Drawing.Point(328, 90);
            this.buttonAddPlugin.Name = "buttonAddPlugin";
            this.buttonAddPlugin.Size = new System.Drawing.Size(50, 21);
            this.buttonAddPlugin.TabIndex = 40;
            this.buttonAddPlugin.Text = "+";
            this.buttonAddPlugin.UseVisualStyleBackColor = true;
            this.buttonAddPlugin.Click += new System.EventHandler(this.buttonAddPlugin_Click);
            // 
            // textBoxPluginName
            // 
            this.textBoxPluginName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPluginName.Location = new System.Drawing.Point(72, 90);
            this.textBoxPluginName.Name = "textBoxPluginName";
            this.textBoxPluginName.Size = new System.Drawing.Size(250, 21);
            this.textBoxPluginName.TabIndex = 39;
            // 
            // listBoxPlugins
            // 
            this.listBoxPlugins.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxPlugins.FormattingEnabled = true;
            this.listBoxPlugins.ItemHeight = 12;
            this.listBoxPlugins.Items.AddRange(new object[] {
            "cordova-plugin-insomnia"});
            this.listBoxPlugins.Location = new System.Drawing.Point(7, 26);
            this.listBoxPlugins.Name = "listBoxPlugins";
            this.listBoxPlugins.Size = new System.Drawing.Size(424, 60);
            this.listBoxPlugins.TabIndex = 41;
            this.listBoxPlugins.SelectedIndexChanged += new System.EventHandler(this.listBoxPlugins_SelectedIndexChanged);
            // 
            // buttonDeletePlugin
            // 
            this.buttonDeletePlugin.Location = new System.Drawing.Point(382, 90);
            this.buttonDeletePlugin.Name = "buttonDeletePlugin";
            this.buttonDeletePlugin.Size = new System.Drawing.Size(50, 21);
            this.buttonDeletePlugin.TabIndex = 42;
            this.buttonDeletePlugin.Text = "-";
            this.buttonDeletePlugin.UseVisualStyleBackColor = true;
            this.buttonDeletePlugin.Click += new System.EventHandler(this.buttonDeletePlugin_Click);
            // 
            // timerBackground
            // 
            this.timerBackground.Tick += new System.EventHandler(this.timerBackground_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 12);
            this.label1.TabIndex = 43;
            this.label1.Text = "플러그인 목록";
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
            this.panelPlugins.Location = new System.Drawing.Point(487, 316);
            this.panelPlugins.Name = "panelPlugins";
            this.panelPlugins.Size = new System.Drawing.Size(440, 146);
            this.panelPlugins.TabIndex = 44;
            // 
            // labelCompileSdkVersion
            // 
            this.labelCompileSdkVersion.AutoSize = true;
            this.labelCompileSdkVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelCompileSdkVersion.ForeColor = System.Drawing.SystemColors.Control;
            this.labelCompileSdkVersion.Location = new System.Drawing.Point(492, 191);
            this.labelCompileSdkVersion.Name = "labelCompileSdkVersion";
            this.labelCompileSdkVersion.Size = new System.Drawing.Size(92, 12);
            this.labelCompileSdkVersion.TabIndex = 45;
            this.labelCompileSdkVersion.Text = "컴파일 API 버전";
            // 
            // comboBoxCompileSdkVersion
            // 
            this.comboBoxCompileSdkVersion.DisplayMember = " ";
            this.comboBoxCompileSdkVersion.FormattingEnabled = true;
            this.comboBoxCompileSdkVersion.Items.AddRange(new object[] {
            "29",
            "28",
            "27",
            "26",
            "25",
            "24",
            "23",
            "22",
            "21",
            "20",
            "19",
            "18",
            "17",
            "16",
            "15",
            "14",
            "13",
            "12",
            "11",
            "10",
            "9",
            "8"});
            this.comboBoxCompileSdkVersion.Location = new System.Drawing.Point(599, 187);
            this.comboBoxCompileSdkVersion.Name = "comboBoxCompileSdkVersion";
            this.comboBoxCompileSdkVersion.Size = new System.Drawing.Size(331, 20);
            this.comboBoxCompileSdkVersion.TabIndex = 46;
            this.comboBoxCompileSdkVersion.ValueMember = " ";
            // 
            // panelBuildLog
            // 
            this.panelBuildLog.BackColor = System.Drawing.Color.Transparent;
            this.panelBuildLog.Controls.Add(this.textBox1);
            this.panelBuildLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBuildLog.Location = new System.Drawing.Point(0, 478);
            this.panelBuildLog.Name = "panelBuildLog";
            this.panelBuildLog.Size = new System.Drawing.Size(960, 123);
            this.panelBuildLog.TabIndex = 47;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(960, 601);
            this.Controls.Add(this.panelBuildLog);
            this.Controls.Add(this.comboBoxCompileSdkVersion);
            this.Controls.Add(this.labelCompileSdkVersion);
            this.Controls.Add(this.panelPlugins);
            this.Controls.Add(this.comboBoxBuildMode);
            this.Controls.Add(this.labelBuildMode);
            this.Controls.Add(this.buttonOpenFileBrowser);
            this.Controls.Add(this.textBoxSettingGameFolder);
            this.Controls.Add(this.labelSettingGameFolder);
            this.Controls.Add(this.comboBoxTargetSdkVersion);
            this.Controls.Add(this.comboBoxMinSdkVersion);
            this.Controls.Add(this.comboBoxFullscreen);
            this.Controls.Add(this.comboBoxOrientation);
            this.Controls.Add(this.labelTargetSdkVersion);
            this.Controls.Add(this.labelMinSdkVersion);
            this.Controls.Add(this.labelFullscreen);
            this.Controls.Add(this.labelOrientation);
            this.Controls.Add(this.textBox_keyC);
            this.Controls.Add(this.label_keyC);
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
            this.Controls.Add(this.label_keyS);
            this.Controls.Add(this.label_keyL);
            this.Controls.Add(this.label_keyO);
            this.Controls.Add(this.label_keyOU);
            this.Controls.Add(this.labelKeyPassword);
            this.Controls.Add(this.labelKeyAlias);
            this.Controls.Add(this.labelKeyPath);
            this.Controls.Add(this.labelGameName);
            this.Controls.Add(this.labelPackageName);
            this.Controls.Add(this.labelFolderName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "RPG Maker MV Cordova Builder ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelPlugins.ResumeLayout(false);
            this.panelPlugins.PerformLayout();
            this.panelBuildLog.ResumeLayout(false);
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
    }
}

