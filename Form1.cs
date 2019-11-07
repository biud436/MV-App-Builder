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
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Cordova_Builder
{

    public partial class Form1 : Form
    {

        private bool[] status = new bool[4];
        private bool isValid = false;
        private TextBoxList textBoxList = new TextBoxList();
        private Cordova cordova = new Cordova();
        private delegate void AppendTextCallback(string output);
        private ResourceManager rm;

        public List<HostData> hostList = new List<HostData>();

        /// <summary>
        /// Form1's constructor.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            rm = new ResourceManager("Cordova_Builder.locale", Assembly.GetExecutingAssembly());
            hostList.Add(new HostData("where java.exe", false, "", rm.GetString("FOUND_JAVA"), rm.GetString("NOT_FOUND_JAVA")));
            hostList.Add(new HostData("where keytool.exe", false, "", rm.GetString("FOUND_KEYTOOL"), rm.GetString("NOT_FOUND_KEYTOOL")));
            hostList.Add(new HostData("where cordova", false, "", rm.GetString("FOUND_CORDOVA"), rm.GetString("NOT_FOUND_CORDOVA")));

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
        /// 
        public void AppendText(string output)
        {
            if(InvokeRequired)
            {
                AppendTextCallback callback = new AppendTextCallback(AppendText);
                Invoke(callback, output);
            } else
            {
                textBox1.AppendText(output);
                if (!output.EndsWith(Environment.NewLine))
                {
                    textBox1.AppendText(Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// 프로그램 초기화 함수입니다.
        /// </summary>
        private void Prepare()
        {
            CenterToScreen();
            cordova.SetMainForm(this);

            // 준비 메시지
            AppendText(rm.GetString("Ready"));

            if(CheckRequirements())
            {
                // 필요한 프로그램이 모두 있음
                AppendText(rm.GetString("Done"));
            } else
            {
                AppendText(rm.GetString("NotInstalled"));

                if(!status[0] || !status[1])
                {
                    // 자바 미설치
                    AppendText(rm.GetString("NotInstalledJava1"));
                    AppendText(rm.GetString("NotInstalledJava2"));
                }

                if (!status[2])
                {
                    // 코르도바 미설치
                    AppendText(rm.GetString("NotInstalledCordova1"));
                    AppendText(rm.GetString("NotInstalledCordova2"));
                    AppendText(rm.GetString("NotInstalledCordova3"));
                }

                if (!status[3])
                {
                    // 안드로이드 SDK
                    AppendText(rm.GetString("NotInstalledAndroidSDK1"));
                    AppendText(rm.GetString("NotInstalledAndroidSDK2"));
                    AppendText(rm.GetString("NotInstalledAndroidSDK3"));
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

            for (var i = 0; i < hostList.Count; i++)
            {
                Append append = AppendText;

                status[i] = hostList[i].Run(append);
            }

            string ANDROID_HOME = System.Environment.GetEnvironmentVariable("ANDROID_HOME");
            string ANDROID_SDK_ROOT = System.Environment.GetEnvironmentVariable("ANDROID_SDK_ROOT");

            if (!String.IsNullOrEmpty(ANDROID_HOME))
            {
                AppendText(String.Format(rm.GetString("FOUND_ANDROID_HOME"), ANDROID_HOME));
                status[3] = true;
            }
            else if (!String.IsNullOrEmpty(ANDROID_SDK_ROOT))
            {
                AppendText(String.Format(rm.GetString("FOUND_ANDROID_SDK_ROOT"), ANDROID_SDK_ROOT));
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
            textBoxList.Add(comboBoxBuildMode);
            textBoxList.Add(comboBoxCompileSdkVersion);

            textBoxList.plugins = listBoxPlugins;
        }

        /// <summary>
        /// UI의 배경 색상과 글자 색상을 설정합니다.
        /// </summary>
        public void InitWithUIBackground()
        {
            var backColor = Color.FromArgb(255, 26, 41, 62);
            var foreColor = Color.White;

            List<Control> list = new List<Control>();
            list.AddRange(textBoxList);
            list.Add(listBoxPlugins);
            list.Add(textBoxSettingGameFolder);
            list.Add(textBoxPluginName);

            foreach (var tb in list)
            {
                tb.BackColor = backColor;
                tb.ForeColor = foreColor;
            }

            //textBoxList.Remove(listBoxPlugins);
            //textBoxList.Remove(textBoxSettingGameFolder);
            //textBoxList.Remove(textBoxPluginName);
        }

        /// <summary>
        /// 키스토어 파일을 생성합니다.
        /// </summary>
        public void CreateKeyStore()
        {

            List<string> validData = new List<string>()
            {
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
            };

            bool isOK = validData.All(text => !String.IsNullOrEmpty(text));

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

                

                HostData hostData = new HostData(cmd, false, "", rm.GetString("CreateKeyStore1"), rm.GetString("CreateKeyStore2"));

                bool status = hostData.Run(append);

                if (status)
                {
                    AppendText(rm.GetString("CreateKeyStore3"));
                }
                else
                {
                    AppendText(rm.GetString("CreateKeyStore4"));
                }

            }
            else
            {
                AppendText("비어있는 칸이 있어서 키스토어 파일을 생성할 수 없습니다.");
            }

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
            comboBoxCompileSdkVersion.SelectedIndex = comboBoxCompileSdkVersion.Items.IndexOf("29");
            comboBoxBuildMode.SelectedIndex = 0;

            timerBackground.Start();
        }

        /// <summary>
        /// 코르도바 플러그인을 자동으로 취득합니다.
        /// </summary>
        /// <param name="mainPath"></param>
        public void ReadCordovaPlugins(string mainPath)
        {
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                string path = System.IO.Path.Combine(mainPath, "js", "plugins");

                if (System.IO.Directory.Exists(path))
                {
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);

                    foreach (System.IO.FileInfo fi in dir.GetFiles())
                    {
                        using (System.IO.StreamReader sr = fi.OpenText())
                        {
                            var s = "";
                            int lineCount = 0;
                            const int maxLine = 50;
                            string matchLine = "";
                            string matchString = "@cordova_plugin ";

                            while ((s = sr.ReadLine()) != null)
                            {
                                lineCount++;

                                if (s.Contains(matchString))
                                {
                                    matchLine = s;
                                    break;
                                }

                                if (lineCount >= maxLine)
                                {
                                    break;
                                }
                            }

                            if (!String.IsNullOrEmpty(matchLine))
                            {
                                int index = matchLine.IndexOf(matchString);

                                string pluginName = matchLine.Substring(index + matchString.Length).Trim();

                                if (InvokeRequired)
                                {
                                    Action action = () => {
                                        listBoxPlugins.Items.Add(pluginName);
                                    };

                                    Invoke(action);
                                }
                                else
                                {
                                    listBoxPlugins.Items.Add(pluginName);
                                }

                            }
                        }
                    }
                }


            });

            worker.Start();

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
            
        }

        private void textBoxPassWord_TextChanged(object sender, EventArgs e)
        {
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
                ReadCordovaPlugins(folderBrowserDialog.SelectedPath);
            }

        }

        private void buttonAddPlugin_Click(object sender, EventArgs e)
        {
            if(textBoxPluginName.TextLength > 0)
            {
                listBoxPlugins.Items.Add(textBoxPluginName.Text);
                textBoxPluginName.Text = "";
            }
        }

        private void timerBackground_Tick(object sender, EventArgs e)
        {
            buttonAddPlugin.Enabled = (textBoxPluginName.TextLength > 0);
            buttonDeletePlugin.Enabled = (listBoxPlugins.SelectedIndex != -1) && (listBoxPlugins.SelectedIndex != 0);

            bool isValid = isValidCreateKeyStoreButton();
            buttonBuild.Enabled = isValid;

        }

        private void buttonDeletePlugin_Click(object sender, EventArgs e)
        {
            int index = listBoxPlugins.SelectedIndex;

            if(index >= 1 && listBoxPlugins.Items.Count > 1)
            {
                listBoxPlugins.Items.RemoveAt(index);
            }
            
        }

        private void listBoxPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBoxPlugins.SelectedIndex;

            if (index >= 0 && listBoxPlugins.Items.Count > 0)
            {
                textBoxPluginName.Text = listBoxPlugins.SelectedItem.ToString();
            }
        }
    }
}
