using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Cordova_Builder
{

    public partial class Form1 : Form
    {

        private bool[] status = new bool[4];

        private bool isValid = false;

        private TextBoxList textBoxList = new TextBoxList();

        private Cordova cordova = new Cordova();

        public HostData[] hostList = new HostData[]
        {
            new HostData("where java.exe", false, "", @"java.exe -version 2>&1", "echo -- 자바를 찾지 못했습니다"),
            new HostData("where keytool.exe", false, "", "echo -- keytool을 찾았습니다.", "echo -- keytool을 찾지 못했습니다"),
            new HostData("where cordova", false, "", "cordova -v", "echo -- cordova를 찾지 못했습니다")
        };

        /// <summary>
        /// Form1's constructor.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get the list for all text box
        /// </summary>
        /// <returns></returns>
        public TextBoxList GetTextBoxList()
        {
            return this.textBoxList;
        }

        /// <summary>
        /// This method adds a text to text box.
        /// </summary>
        /// <param name="output"></param>
        public void AppendText(string output)
        {
            textBox1.AppendText(output);
            if (!output.EndsWith(Environment.NewLine))
            {
                textBox1.AppendText(Environment.NewLine);
            }
        }

        /// <summary>
        /// 프로그램 초기화 함수입니다.
        /// </summary>
        private void Prepare()
        {

            cordova.SetMainForm(this);

            AppendText("--- 준비 ---");

            if(CheckRequirements())
            {
                AppendText("--- 필요한 모든 프로그램이 설치되어있습니다.");
            } else
            {
                AppendText("--- [ERROR] 필요한 프로그램이 설치되어있지 않습니다.");

                if(!status[0] || !status[1])
                {
                    AppendText("자바를 설치해주세요.");
                    AppendText("https://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html");
                }

                if (!status[2])
                {
                    AppendText("Node.js와 cordova를 설치해야 합니다.");
                    AppendText("https://nodejs.org/ko/download/");
                    AppendText("https://www.npmjs.com/package/cordova");
                }

                if (!status[3])
                {
                    AppendText("안드로이드 SDK가 설치되어있지 않습니다");
                    AppendText("안드로이드 SDK를 설치해주십시오.");
                    AppendText("https://developer.android.com/studio/?hl=ko");
                }


            }

        }

        /// <summary>
        /// 키스토어 생성 버튼을 활성화해도 되는 지를 확인합니다.
        /// </summary>
        /// <returns></returns>
        public bool isValidCreateKeyStoreButton()
        {
            return textBoxList.All(data => data.Text.Length > 0);
        }

        /// <summary>
        /// 빌드에 필요한 프로그램이 컴퓨터에 설치되어있지 확인합니다.
        /// 이 작업은 약간 걸릴 수 있습니다.
        /// </summary>
        /// <returns></returns>
        public bool CheckRequirements()
        {

            for (var i = 0; i < hostList.Length; i++)
            {
                Append append = AppendText;

                status[i] = hostList[i].Run(append);
            }

            string ANDROID_HOME = System.Environment.GetEnvironmentVariable("ANDROID_HOME");
            string ANDROID_SDK_ROOT = System.Environment.GetEnvironmentVariable("ANDROID_SDK_ROOT");

            if (!String.IsNullOrEmpty(ANDROID_HOME))
            {
                AppendText(String.Format("안드로이드 SDK / ANDROID_HOME를 찾았습니다 : {0}", ANDROID_HOME));
                status[3] = true;
            }
            else if (!String.IsNullOrEmpty(ANDROID_SDK_ROOT))
            {
                AppendText(String.Format("안드로이드 SDK / ANDROID_SDK_ROOT를 찾았습니다 : {0}", ANDROID_SDK_ROOT));
                status[3] = true;
            }

            isValid = status.All(i => true);

            return isValid;
        }

        /// <summary>
        /// 텍스트 박스 리스트에 모든 텍스트 박스를 추가합니다.
        /// </summary>
        public void InitWIthTextBoxList()
        {
            textBoxList.Add(textBoxFolderName);
            textBoxList.Add(textBoxKeyPath);
            textBoxList.Add(textBoxGameName);
            textBoxList.Add(textBoxKeyAlias);
            textBoxList.Add(textBoxPassWord);
            textBoxList.Add(textBoxPackageName);
            textBoxList.Add(textBox_keyOU);
            textBoxList.Add(textBox_keyO);
            textBoxList.Add(textBox_keyL);
            textBoxList.Add(textBox_keyS);
            textBoxList.Add(textBox_keyC);
            textBoxList.Add(comboBoxOrientation);
            textBoxList.Add(comboBoxFullscreen);
            textBoxList.Add(comboBoxMinSdkVersion);
            textBoxList.Add(comboBoxTargetSdkVersion);
            textBoxList.Add(textBoxSettingGameFolder);
        }

        /// <summary>
        /// UI의 배경 색상과 글자 색상을 설정합니다.
        /// </summary>
        public void InitWithUIBackground()
        {
            var backColor = Color.FromArgb(255, 26, 41, 62);
            var foreColor = Color.White;

            foreach (var tb in textBoxList)
            {
                tb.BackColor = backColor;
                tb.ForeColor = foreColor;
            }
        }

        /// <summary>
        /// 키스토어 파일을 생성합니다.
        /// </summary>
        public void CreateKeyStore()
        {
            bool isOK = textBoxList.All(text => !String.IsNullOrEmpty(text.Text));

            cordova.Make(filename =>
            {
                if (isOK)
                {
                    string cmd = String.Format("keytool -genkey -v -keystore {0} -alias {1} -keyalg RSA -keysize 2048 -validity 10000 -keypass {2} -storepass {3} -dname \"CN={4},OU={5},O={6},L={7},S={8},C={9}\" 2>&1",
                            textBoxKeyPath.Text,
                            textBoxKeyAlias.Text,
                            textBoxPassWord.Text,
                            textBoxPassWord.Text,
                            textBoxPackageName.Text,
                            textBox_keyOU.Text,
                            textBox_keyO.Text,
                            textBox_keyL.Text,
                            textBox_keyS.Text,
                            textBox_keyC.Text
                        );

                    Append append = AppendText;

                    Debug.WriteLine(cmd);

                    HostData hostData = new HostData(cmd, false, "", "키스토어 파일이 생성되었습니다", "키스토어 파일 생성에 실패하였습니다");

                    bool status = hostData.Run(append);

                    if (status)
                    {
                        AppendText("키스토어 파일이 생성되었습니다.");
                    }
                    else
                    {
                        AppendText("이미 파일이 존재하거나. 유효하지 않은 매개변수를 입력하였습니다. 키스토어 파일 생성에 실패하였습니다.");
                    }

                }
                else
                {
                    AppendText("비어있는 칸이 있어서 키스토어 파일을 생성할 수 없습니다.");
                }
            });

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Prepare();
            InitWIthTextBoxList();
            InitWithUIBackground();
            InitWithComboBox();

            cordova.Bind(this.textBoxList);

        }

        public void InitWithComboBox()
        {
            comboBoxOrientation.SelectedIndex = 2;
            comboBoxFullscreen.SelectedIndex = 0;
            comboBoxMinSdkVersion.SelectedIndex = comboBoxMinSdkVersion.Items.IndexOf("19");
            comboBoxTargetSdkVersion.SelectedIndex = comboBoxTargetSdkVersion.Items.IndexOf("29");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            textBox1.ScrollToCaret();
            textBox1.BackColor = Color.FromArgb(255, 26, 41, 62);
            textBox1.ForeColor = Color.White;
        }

        private void customRichTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonCreateKeyStore_Click(object sender, EventArgs e)
        {
            CreateKeyStore();
        }

        private void textBoxPassWord_TextChanged(object sender, EventArgs e)
        {
            bool isValid = isValidCreateKeyStoreButton();
            buttonCreateKeyStore.Enabled = isValid;
            buttonBuild.Enabled = isValid;
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            cordova.Build();
        }

        private void comboBoxMinSdkVersion_TextChanged(object sender, EventArgs e)
        {
            int n = 0;

            if(!int.TryParse(comboBoxMinSdkVersion.Text, out n) && !String.IsNullOrEmpty(comboBoxMinSdkVersion.Text))
            {
                comboBoxMinSdkVersion.SelectedIndex = comboBoxMinSdkVersion.Items.IndexOf("19");
            }
        }

        private void buttonFolderBrowser_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();

            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxSettingGameFolder.Text = folderBrowserDialog.SelectedPath;
            }

        }
    }
}
