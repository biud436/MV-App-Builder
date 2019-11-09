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

        // 필요 프로그램 4개가 컴퓨터에 깔려있는 지 확인하는 bool[]형 변수
        private bool[] status = new bool[4];

        // 준비해야 할 프로그램이 모두 깔려있으면 true
        private bool isValid = false;

        // 화면 상의 모든 UI 컨트롤을 자식으로 지니는 컬렉션
        private TextBoxList textBoxList = new TextBoxList();

        // 코르도바의 CLI 명령을 처리하는 클래스
        private Cordova cordova = new Cordova();

        // 콜백 함수 처리를 위한 선언
        private delegate void AppendTextCallback(string output);

        // 다국어 처리를 위한 리소스 관리자
        private ResourceManager rm;

        // 호스트 데이터 목록;
        public List<HostData> hostList = new List<HostData>();

        // 빌드 작업 중임을 판별하는 변수, 처음에는 false
        public bool isWorking = false;

        /// <summary>
        /// Form1's constructor.
        /// </summary>
        public Form1()
        {

            InitializeComponent();

            rm = new ResourceManager("Cordova_Builder.locale", Assembly.GetExecutingAssembly());

            // 호스트 데이터는 새로운 프로세스를 생성하여 콘솔 리다이렉션을 수행한다.
            // 자바, 키스토어, 코르도바가 설치 되어있는 지를 확인한다.
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
            // TODO : 이렇게 해야만 하는가?
            // 더 간단한 방법은 없을까?
            // 빌더 패턴? 
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
        }

        /// <summary>
        /// 키스토어 파일을 생성합니다.
        /// </summary>
        public void CreateKeyStore()
        {

            string keystorePath = textBoxKeyPath.Text;

            if (System.IO.File.Exists(keystorePath))
            {
                return;
            }

            // TODO: 이 코드는 보기 좋지 않은 듯 하다.
            // 텍스트 목록을 한 번에 가져올 수 있는 섹시한 함수가 있을까?
            // 있다면 그때 변경하자.
            List<string> validData = new List<string>()
            {
                        keystorePath,
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
                // TODO: 전달되는 인수가 너무 많다.
                // 더 간단한 처리 방법이 없을까?
                string cmd = String.Format("keytool -genkey -v -keystore {0} -alias {1} -keyalg RSA -keysize 2048 -validity 10000 -keypass {2} -storepass {3} -dname \"CN={4},OU={5},O={6},L={7},S={8},C={9}\" 2>&1",
                        keystorePath,
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

                HostData hostData = new HostData(cmd, false, "", rm.GetString("CreateKeyStore1"), rm.GetString("CreateKeyStore2"));

                bool status = hostData.Run(append);

                // 키스토어가 정상적으로 생성되었다면 status가 true이다.
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
                // 텍스트가 비어있을 때의 처리
                // 정상적으로 실행하였다면 이 부분은 절대 실행될 일이 없다.
                AppendText(rm.GetString("CreateKeyStore5"));
            }

        }

        /// <summary>
        /// Form1이 로드되었을 때의 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            // UI 컨트롤이 멈추지 않도록 새로운 쓰레드에서 작업 수행
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                string path = System.IO.Path.Combine(mainPath, "js", "plugins");

                // TODO : 이 로직은 불필요한 플러그인을 모두 읽기 때문에 성능에 불리하다.
                // good 방법은 plugins.json 파일을 읽고 활성화된 플러그인의 주석을 처리해야 한다.
                // 이 경우, JSON 처리가 필요함. 

                // 플러그인 폴더가 존재하면 특정 코멘트를 찾는다 (50줄까지만 읽음)
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

        /// <summary>
        /// 빌드 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBuild_Click(object sender, EventArgs e)
        {
            // 빌드 중이 아닐 때에만 빌드가 가능함
            if (buttonBuild.Enabled && !isWorking)
            {

                isWorking = true;
                buttonBuild.Enabled = false;

                // 빌드 후 일부 프로세스가 정리되지 않는 경우가 있다 (JDK 등)
                // 그때 워커가 종료되고 다시 버튼이 활성화되는 지 여부를 확인하기 위해 토글 처리를 하였다
                cordova.Build(() =>
                {

                    Action completeBuild = () =>
                    {
                        // 빌드가 끝이 났으므로 빌드 버튼을 다시 활성화 한다.
                        buttonBuild.Enabled = true;
                        isWorking = false;
                    };

                    if(InvokeRequired)
                    {
                        Invoke(completeBuild);
                    } else
                    {
                        completeBuild();
                    }

                });
            }
        }

        /// <summary>
        /// 최소 SDK 레벨을 읽고 정수형으로 파싱합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxMinSdkVersion_TextChanged(object sender, EventArgs e)
        {
            int n = 0;

            if(!int.TryParse(comboBoxMinSdkVersion.Text, out n) && !String.IsNullOrEmpty(comboBoxMinSdkVersion.Text))
            {
                comboBoxMinSdkVersion.SelectedIndex = comboBoxMinSdkVersion.Items.IndexOf("19");
            }
        }

        /// <summary>
        /// Read the content from the game folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFolderBrowser_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();

            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxSettingGameFolder.Text = folderBrowserDialog.SelectedPath;
                ReadCordovaPlugins(folderBrowserDialog.SelectedPath);
            }

        }

        /// <summary>
        /// 리스트 박스에 새로운 플러그인을 추가합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddPlugin_Click(object sender, EventArgs e)
        {
            // 텍스트 박스에 아무것도 적지 않았으면 추가를 못하게 한다.
            if(textBoxPluginName.TextLength > 0)
            {
                listBoxPlugins.Items.Add(textBoxPluginName.Text);
                textBoxPluginName.Text = "";
            }
        }

        /// <summary>
        /// 매 틱마다 플러그인 추가, 삭제, 빌드 버튼 활성화 여부를 설정합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerBackground_Tick(object sender, EventArgs e)
        {
            buttonAddPlugin.Enabled = (textBoxPluginName.TextLength > 0);
            buttonDeletePlugin.Enabled = (listBoxPlugins.SelectedIndex != -1) && (listBoxPlugins.SelectedIndex != 0);

            bool isValid = isValidCreateKeyStoreButton();

            // 빌드 중이 아닐 때에만 동작
            if(!isWorking)
            {
                buttonBuild.Enabled = isValid;
            }

        }

        /// <summary>
        /// 플러그인을 리스트 박스에서 삭제 처리합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeletePlugin_Click(object sender, EventArgs e)
        {
            int index = listBoxPlugins.SelectedIndex;

            // 화면 꺼짐 방지 플러그인은 삭제할 수 없게 한다.
            if(index >= 1 && listBoxPlugins.Items.Count > 1)
            {
                listBoxPlugins.Items.RemoveAt(index);
            }
            
        }

        /// <summary>
        /// 플러그인 목록의 인덱스가 변경되었을 때 텍스트 박스 업데이트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
