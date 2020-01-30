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
        private bool[] _status = new bool[4];

        // 준비해야 할 프로그램이 모두 깔려있으면 true
        private bool _isValid = false;

        // 화면 상의 모든 UI 컨트롤을 자식으로 지니는 컬렉션
        private TextBoxList _textBoxList = new TextBoxList();

        // 코르도바의 CLI 명령을 처리하는 클래스
        private Cordova _cordova = new Cordova();

        // 콜백 함수 처리를 위한 선언
        private delegate void AppendTextCallback(string output);

        // 다국어 처리를 위한 리소스 관리자
        private ResourceManager _rm;

        // 호스트 데이터 목록;
        public List<HostData> _hostList = new List<HostData>();

        // 빌드 작업 중임을 판별하는 변수, 처음에는 false
        public bool _isWorking = false;

        public SortedSet<int> platformTools = new SortedSet<int>();

        /// <summary>
        /// Form1's constructor.
        /// </summary>
        public Form1()
        {

            InitializeComponent();
            InitWithResourceManager();
            InitWithHostData();
            InitWithContextMenu();

        }

        public void InitWithResourceManager()
        {
            _rm = new ResourceManager("Cordova_Builder.locale", Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// 호스트 데이터는 새로운 프로세스를 생성하여 콘솔 리다이렉션을 수행한다.
        /// 자바, 키스토어, 코르도바가 설치 되어있는 지를 확인한다.
        /// </summary>
        public void InitWithHostData()
        {
            _hostList.Add(new HostData("where java.exe > nul", false, "", _rm.GetString("FOUND_JAVA"), _rm.GetString("NOT_FOUND_JAVA")));
            _hostList.Add(new HostData("where keytool.exe > nul", false, "", _rm.GetString("FOUND_KEYTOOL"), _rm.GetString("NOT_FOUND_KEYTOOL")));
            _hostList.Add(new HostData("where cordova > nul", false, "", _rm.GetString("FOUND_CORDOVA"), _rm.GetString("NOT_FOUND_CORDOVA")));
            _hostList.Add(new HostData("where mv-resource-cleaner > nul", false, "", "", ""));
        }

        /// <summary>
        /// 메뉴 아이템 추가
        /// </summary>
        public void InitWithContextMenu()
        {
            ToolStripItem item = contextMenuStrip1.Items.Add(_rm.GetString("CLEAR_BUILD_LOG"));
            item.Click += ClearBuildLog;

            contextMenuStrip1.Items.Add(_rm.GetString("SAVE_BUILD_LOG")).Click += SaveBuildLog;
        }

        /// <summary>
        /// Get the list for all text box
        /// </summary>
        /// <returns></returns>
        public TextBoxList GetTextBoxList()
        {
            return this._textBoxList;
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

        public void DebugText(string output)
        {
            textBox1.SelectionColor = Color.Red;

            AppendText(output);

            textBox1.SelectionColor = Color.White;
        }

        /// <summary>
        /// 설치되어있는 SDK 레벨을 나열합니다.
        /// </summary>
        public void GetPlatformsFolders()
        {
            string ANDROID_HOME = System.Environment.GetEnvironmentVariable("ANDROID_HOME");
            string ANDROID_SDK_ROOT = System.Environment.GetEnvironmentVariable("ANDROID_SDK_ROOT");
            string defaultPath = "";
            bool isValid = false;

            if (!String.IsNullOrEmpty(ANDROID_SDK_ROOT))
            {
                isValid = true;
                defaultPath = ANDROID_SDK_ROOT;
            }
            else if (!String.IsNullOrEmpty(ANDROID_HOME))
            {
                isValid = true;
                defaultPath = ANDROID_HOME;
            }

            // sdkmanager "platform-tools" "platforms;android-29"
            if (System.IO.Directory.Exists(defaultPath) && isValid )
            {
                var platformsPath = System.IO.Path.Combine(defaultPath, "platforms");

                if(System.IO.Directory.Exists(platformsPath))
                {
                    var sdkIds = System.IO.Directory.GetDirectories(platformsPath);

                    if(sdkIds.Count() == 0)
                    {
                        throw new Exception($"The Android SDK is not installed.");
                    }

                    textBox1.SelectionColor = Color.Red;
                    AppendText("[SDK]==========================================");

                    foreach (var sdk in sdkIds)
                    {
                        textBox1.SelectionColor = Color.YellowGreen;
                        AppendText(System.IO.Path.GetFileNameWithoutExtension(sdk));
                    }

                    textBox1.SelectionColor = Color.Red;
                    AppendText("==============================================");
                    textBox1.SelectionColor = Color.White;
                }

            } else
            {
                throw new Exception("Cannot find ANDROID_SDK_ROOT or ANDROID_HOME in the Windows Environment Variables");
            }
        }

        /// <summary>
        /// 안드로이드 API 레벨을 구합니다.
        /// </summary>
        private void GetAndroidAPILevels()
        {

            string ANDROID_HOME = System.Environment.GetEnvironmentVariable("ANDROID_HOME");
            string ANDROID_SDK_ROOT = System.Environment.GetEnvironmentVariable("ANDROID_SDK_ROOT");
            string defaultPath = "";
            bool isValid = false;

            if (!String.IsNullOrEmpty(ANDROID_SDK_ROOT))
            {
                isValid = true;
                defaultPath = ANDROID_SDK_ROOT;
            }
            else if (!String.IsNullOrEmpty(ANDROID_HOME))
            {
                isValid = true;
                defaultPath = ANDROID_HOME;
            }

            string sdkaMangerPath = String.Format("{0} --list | find \"platforms;\"", System.IO.Path.Combine(defaultPath, "tools", "bin", "sdkmanager.bat"));

            if (System.IO.Directory.Exists(defaultPath) && isValid)
            {
                using (HostData process = new HostData(sdkaMangerPath, false, "", "echo ", "echo "))
                {
                    Append append = (output) =>
                    {
                        var ret = output.Split('|')[0];
                        var regex = new System.Text.RegularExpressions.Regex("(?:platforms;android-)(\\d+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        var match = regex.Match(ret);

                        if (match.Success)
                        {
                            platformTools.Add(Int32.Parse(match.Groups[1].Value));
                        }
                    };

                    process.Run(append);

                }

                comboBoxMinSdkVersion.Items.Clear();
                comboBoxTargetSdkVersion.Items.Clear();
                comboBoxCompileSdkVersion.Items.Clear();

                foreach (var value in platformTools)
                {
                    comboBoxMinSdkVersion.Items.Add(value);
                    comboBoxTargetSdkVersion.Items.Add(value);
                    comboBoxCompileSdkVersion.Items.Add(value);
                }
            } else
            {
                throw new Exception("Cannot find ANDROID_SDK_ROOT or ANDROID_HOME in the Windows Environment Variables");
            }

        }

        /// <summary>
        /// 프로그램 초기화 함수입니다.
        /// </summary>
        private void Prepare()
        {
            CenterToScreen();
            _cordova.SetMainForm(this);

            // 준비 메시지
            AppendText(_rm.GetString("Ready"));

            if(CheckRequirements())
            {
                textBox1.SelectionColor = Color.LightSteelBlue;

                // 필요한 프로그램이 모두 있음
                AppendText(_rm.GetString("Done"));

                textBox1.SelectionColor = Color.White;
            } else
            {
                AppendText(_rm.GetString("NotInstalled"));

                if(!_status[0] || !_status[1])
                {
                    // 자바 미설치
                    AppendText(_rm.GetString("NotInstalledJava1"));
                    AppendText(_rm.GetString("NotInstalledJava2"));
                }

                if (!_status[2])
                {
                    // 코르도바 미설치
                    AppendText(_rm.GetString("NotInstalledCordova1"));
                    AppendText(_rm.GetString("NotInstalledCordova2"));
                    AppendText(_rm.GetString("NotInstalledCordova3"));
                }

                if (!_status[3])
                {
                    // 안드로이드 SDK
                    AppendText(_rm.GetString("NotInstalledAndroidSDK1"));
                    AppendText(_rm.GetString("NotInstalledAndroidSDK2"));
                    AppendText(_rm.GetString("NotInstalledAndroidSDK3"));
                }

                if (!_status[4])
                {
                    _cordova.InstallResourceCleaner();
                }


            }

        }

        /// <summary>
        /// 키스토어 생성 버튼을 활성화해도 되는 지를 확인합니다.
        /// </summary>
        /// <returns></returns>
        public bool isValidCreateKeyStoreButton()
        {
            return _textBoxList.All(data => data.Text.Length > 0);
        }

        /// <summary>
        /// 빌드에 필요한 프로그램이 컴퓨터에 설치되어있지 확인합니다.
        /// 이 작업은 약간 걸릴 수 있습니다.
        /// </summary>
        /// <returns></returns>
        public bool CheckRequirements()
        {

            Append append = AppendText;

            for (var i = 0; i < _hostList.Count; i++)
            {
                _status[i] = _hostList[i].Run(append);
            }

            // 결과에 상관 없이 메모리를 명시적으로 해제합니다.
            _hostList.ForEach((i) => {
                i.Dispose();
            });

            string ANDROID_HOME = System.Environment.GetEnvironmentVariable("ANDROID_HOME");
            string ANDROID_SDK_ROOT = System.Environment.GetEnvironmentVariable("ANDROID_SDK_ROOT");

            if (!String.IsNullOrEmpty(ANDROID_HOME))
            {
                AppendText(String.Format(_rm.GetString("FOUND_ANDROID_HOME"), ANDROID_HOME));
                _status[3] = true;
            }
            else if (!String.IsNullOrEmpty(ANDROID_SDK_ROOT)) // (Recommended)
            {
                AppendText(String.Format(_rm.GetString("FOUND_ANDROID_SDK_ROOT"), ANDROID_SDK_ROOT));
                _status[3] = true;
            }

            GetPlatformsFolders();

            _isValid = _status.All(i => true);

            return _isValid;
        }

        /// <summary>
        /// 텍스트 박스 리스트에 모든 텍스트 박스를 추가합니다.
        /// </summary>
        public void InitWIthTextBoxList()
        {
            _textBoxList.Add(textBoxFolderName);
            _textBoxList.Add(textBoxKeyPath);
            _textBoxList.Add(textBoxGameName);
            _textBoxList.Add(textBoxKeyAlias);
            _textBoxList.Add(textBoxPassWord);
            _textBoxList.Add(textBoxPackageName);
            _textBoxList.Add(textBox_keyOU);
            _textBoxList.Add(textBox_keyO);
            _textBoxList.Add(textBox_keyL);
            _textBoxList.Add(textBox_keyS);
            _textBoxList.Add(textBox_keyC);
            _textBoxList.Add(comboBoxOrientation);
            _textBoxList.Add(comboBoxFullscreen);
            _textBoxList.Add(comboBoxMinSdkVersion);
            _textBoxList.Add(comboBoxTargetSdkVersion);
            _textBoxList.Add(textBoxSettingGameFolder);
            _textBoxList.Add(comboBoxBuildMode);
            _textBoxList.Add(comboBoxCompileSdkVersion);

            _textBoxList.plugins = listBoxPlugins;
        }

        /// <summary>
        /// UI의 배경 색상과 글자 색상을 설정합니다.
        /// </summary>
        public void InitWithUIBackground()
        {
            var backColor = Color.FromArgb(255, 26, 41, 62);
            var foreColor = Color.White;

            List<Control> list = new List<Control>();
            list.AddRange(_textBoxList);
            list.Add(textBoxSettingGameFolder);

            foreach (var tb in list)
            {
                tb.BackColor = backColor;
                tb.ForeColor = foreColor;
            }

            textBoxPluginName.BackColor = Color.FromArgb(255, 57, 60, 62);
            textBoxPluginName.ForeColor = foreColor;
            listBoxPlugins.BackColor = Color.FromArgb(255, 57, 60, 62);
            listBoxPlugins.ForeColor = foreColor;
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

            if (_isValid)
            {
                GetAndroidAPILevels();
            }

            InitWithComboBox();
            DataMan.Instance.Import();
        }

        public void InitWithComboBox()
        {
            comboBoxOrientation.SelectedIndex = 2;
            comboBoxFullscreen.SelectedIndex = 0;
            comboBoxMinSdkVersion.SelectedIndex = comboBoxMinSdkVersion.Items.IndexOf(19);
            comboBoxTargetSdkVersion.SelectedIndex = comboBoxTargetSdkVersion.Items.IndexOf(29);
            comboBoxCompileSdkVersion.SelectedIndex = comboBoxCompileSdkVersion.Items.IndexOf(29);
            comboBoxBuildMode.SelectedIndex = 0;

            timerBackground.Start();
        }

        /// <summary>
        /// 코르도바 플러그인을 자동으로 취득합니다.
        /// </summary>
        /// <param name="mainPath"></param>
        public void ReadCordovaPlugins(string mainPath)
        {
            // Read the file called "www/js/plugins.js"
            _cordova.ReadProjectPluginsJson(mainPath);

            // UI 컨트롤이 멈추지 않도록 새로운 쓰레드에서 작업 수행
            System.Threading.Thread worker = new System.Threading.Thread(() =>
            {
                string path = System.IO.Path.Combine(mainPath, "js", "plugins");

                // plugins.json 파일에 플러그인이 ON이라면 해당 플러그인에서 특정 코멘트를 찾는다 (50줄까지만 읽음)
                if (System.IO.Directory.Exists(path))
                {
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);

                    foreach (System.IO.FileInfo fi in dir.GetFiles())
                    {
                        string filename = System.IO.Path.GetFileNameWithoutExtension(fi.FullName);

                        if (_cordova.IsValidPlugin(filename))
                        {
                            #region 플러그인명 추출
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
                            #endregion
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
            textBox1.BackColor = Color.FromArgb(255, 57, 60, 62);
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
            if (buttonBuild.Enabled && !_isWorking)
            {

                _isWorking = true;
                buttonBuild.Enabled = false;

                var config = new FormData.Config()
                {
                    folderName = textBoxFolderName.Text,
                    keyPath = textBoxKeyPath.Text,
                    gameName = textBoxGameName.Text,
                    keyAlias = textBoxKeyAlias.Text,
                    passWord = textBoxPassWord.Text,
                    packageName = textBoxPackageName.Text,
                    keyOU = textBox_keyOU.Text,
                    keyO = textBox_keyO.Text,
                    keyL = textBox_keyL.Text,
                    keyS = textBox_keyS.Text,
                    keyC = textBox_keyC.Text,
                    orientation = comboBoxOrientation.SelectedItem.ToString(),
                    fullscreen = comboBoxFullscreen.SelectedItem.ToString(),
                    minSdkVersion = comboBoxMinSdkVersion.SelectedItem.ToString(),
                    targetSdkVersion = comboBoxTargetSdkVersion.SelectedItem.ToString(),
                    settingGameFolder = textBoxSettingGameFolder.Text,
                    buildMode = comboBoxBuildMode.SelectedIndex,
                    compileSdkVersion = comboBoxCompileSdkVersion.SelectedItem.ToString(),
                    plugins = listBoxPlugins.Items.Cast<String>().ToList(),
                 };

                // 빌드 후 일부 프로세스가 정리되지 않는 경우가 있다 (JDK 등)
                // 그때 워커가 종료되고 다시 버튼이 활성화되는 지 여부를 확인하기 위해 토글 처리를 하였다
                _cordova.Build(config, () =>
                {

                    Action completeBuild = () =>
                    {
                        // 빌드가 끝이 났으므로 빌드 버튼을 다시 활성화 한다.
                        buttonBuild.Enabled = true;
                        _isWorking = false;

                        // 폴더가 있다면 탐색기로 연다.
                        string myDocumentsPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), this.Text);
                        string targetPath = System.IO.Path.Combine(myDocumentsPath, textBoxFolderName.Text, "platforms", "android", "app", "build", "outputs", "apk");

                        if(System.IO.Directory.Exists(targetPath))
                        {
                            System.Diagnostics.Process.Start(targetPath);
                        }

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
            if(!_isWorking)
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

        /// <summary>
        /// <see cref="http://www.csharpstudy.com/Tip/Tip-network-connectivity.aspx"/>
        /// <seealso cref="https://stackoverflow.com/questions/2031824/what-is-the-best-way-to-check-for-internet-connectivity-using-net"/>
        /// </summary>
        /// <returns></returns>
        public bool IsInternetConnected()
        {
            const string NCSI_TEST_URL = "http://www.msftncsi.com/ncsi.txt";
            const string NCSI_TEST_RESULT = "Microsoft NCSI";
            const string NCSI_DNS = "dns.msftncsi.com";
            const string NCSI_DNS_IP_ADDRESS = "131.107.255.255";

            try
            {
                // Check NCSI test link
                var webClient = new System.Net.WebClient();
                string result = webClient.DownloadString(NCSI_TEST_URL);
                if (result != NCSI_TEST_RESULT)
                {
                    return false;
                }

                // Check NCSI DNS IP
                var dnsHost = System.Net.Dns.GetHostEntry(NCSI_DNS);
                if (dnsHost.AddressList.Count() < 0 || dnsHost.AddressList[0].ToString() != NCSI_DNS_IP_ADDRESS)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            bool connected = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

            if(connected && IsInternetConnected())
            {
                textBox1.SelectionColor = Color.Yellow;
                AppendText(_rm.GetString("PENDING_CHECK_PROGRAM_VER"));

                textBox1.SelectionColor = Color.White;

                _cordova.CheckVersion();

                textBox1.SelectionColor = Color.Yellow;

                AppendText(_rm.GetString("PENDING_CHECK_CORDOVA_VER"));

                textBox1.SelectionColor = Color.White;

                System.Threading.Thread worker = new System.Threading.Thread(() =>
                {
                    Action action = () =>
                    {
                        _cordova.CheckLatestCordovaVersion();
                    };

                    if (InvokeRequired)
                    {
                        action();
                    }
                    else
                    {
                        _cordova.CheckLatestCordovaVersion();
                    }
                });

                worker.Start();
            } else
            {
                textBox1.SelectionColor = Color.Red;
                AppendText(_rm.GetString("NOT_CONNECTED_INTERNET"));
                textBox1.SelectionColor = Color.White;
            }
                
        }

        /// <summary>
        /// 빌드 로드를 초기화합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearBuildLog(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        /// <summary>
        /// 빌드 로드를 저장합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBuildLog(object sender, EventArgs e)
        {
            string filename = "log_" + System.IO.Path.GetRandomFileName() + ".log";
            string filepath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename);

            SaveFileDialog sfg = new SaveFileDialog();
            sfg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sfg.DefaultExt = "*.log";
            sfg.FileName = filename;

            if (sfg.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(sfg.FileName, textBox1.Text, Encoding.UTF8);
            }
        }

        /// <summary>
        /// 오른쪽 단축 메뉴 표시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.ContextMenuStrip = contextMenuStrip1;
            if(e.Button == MouseButtons.Right)
            {
                Point pt = textBox1.PointToScreen(e.Location);
                contextMenuStrip1.Show(pt);
            }
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if(!this.InvokeRequired)
            {
                SettingsDialog settingDialog = new SettingsDialog();
                settingDialog.Show();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataMan.Instance.Save();
        }
    }
}
