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

namespace Cordova.Forms
{
    using Core;
    using Data;
    using Manage;
    using System.IO;
    using System.Threading;

    public partial class Form1 : Form
    {
        private bool[] _status              = new bool[5];                  // 필요 프로그램 4개가 컴퓨터에 깔려있는 지 확인하는 bool[]형 변수
        private bool _isValid               = false;                        // 준비해야 할 프로그램이 모두 깔려있으면 true
        private TextBoxList _textBoxList    = new TextBoxList();            // 화면 상의 모든 UI 컨트롤을 자식으로 지니는 컬렉션
        private Cordova _cordova            = new Cordova();                // 코르도바의 CLI 명령을 처리하는 클래스
        private delegate void AppendTextCallback(string output);            // 콜백 함수 처리를 위한 선언

        private ResourceManager _rm;                                        // 다국어 처리를 위한 리소스 관리자

        public List<HostData> _hostList     = new List<HostData>();         // 호스트 데이터 목록;

        public bool _isWorking              = false;                        // 빌드 작업 중임을 판별하는 변수, 처음에는 false

        public SortedSet<int> platformTools = new SortedSet<int>();         // 플랫폼 정렬

        public SortedSet<int> installedSDKs = new SortedSet<int>();         // 설치된 SDK 목록

        public const int DEFAULT_MINIMUM_SDK_VERSION = 28;
        public const int DEFAULT_TARGET_SDK_VERSION = 33;
        public const int DEFAULT_COMPILE_SDK_VERSION = 33;

        sealed class SDK
        {
            public static int JDK = 0;
            public static int KEYTOOL = 1;
            public static int CORDOVA = 2;
            public static int ANDROID_SDK = 3;
            public static int CLEANER = 4;
            public static int GRADLE = 5;
        };

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
            // JDK 버전이 15이상이면 sdkmanager를 사용할 수 없다.
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
            string ANDROID_HOME = null;
            string ANDROID_SDK_ROOT = null;

            try
            {
                ANDROID_HOME = System.Environment.GetEnvironmentVariable("ANDROID_HOME");
                ANDROID_SDK_ROOT = System.Environment.GetEnvironmentVariable("ANDROID_SDK_ROOT");

            } catch
            {
                AppendText("ANDROID_HOME or ANDROID_SDK_ROOT can't find in your system environment");
            }

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

            DataManager.Instance.AndroidSDKPath = defaultPath;

            // 안드로이드 SDK가 설치되어있는지 확인합니다.
            // sdkmanager "platform-tools" "platforms;android-29"
            if (Directory.Exists(defaultPath) && isValid )
            {
                var platformsPath = Path.Combine(defaultPath, "platforms");

                // 안드로이드 SDK 폴더에 어떤 API가 설치되어있는지 확인합니다.
                if(Directory.Exists(platformsPath))
                {
                    var sdkIds = Directory.GetDirectories(platformsPath);

                    if(sdkIds.Count() == 0)
                    {
                        throw new Exception(_rm.GetString("NotInstalledAndroidSDK1"));
                    }

                    textBox1.SelectionColor = Color.Red;
                    AppendText("[SDK]==========================================");

                    foreach (var sdk in sdkIds)
                    {
                        textBox1.SelectionColor = Color.YellowGreen;
                        AppendText(System.IO.Path.GetFileNameWithoutExtension(sdk));

                        string[] pack = sdk.Split('-');
                        int sdkLevel = Int32.Parse(pack[1]);

                        installedSDKs.Add(sdkLevel);
                        
                    }

                    textBox1.SelectionColor = Color.Red;
                    AppendText("==============================================");
                    textBox1.SelectionColor = Color.White;
                }

            } else
            {
                throw new Exception(_rm.GetString("ENVIRONMENT_VAR_NOT_FOUND"));
            }

            if(installedSDKs.Count == 0)
            {
                DebugText(_rm.GetString("ANDROID_API_LEVEL_NOT_FOUND"));
            }
        }

        /// <summary>
        /// 안드로이드 API 레벨을 구합니다.
        /// </summary>
        private void GetAndroidAPILevels()
        {

            string ANDROID_HOME = null;
            string ANDROID_SDK_ROOT = null;

            try
            {
                ANDROID_HOME = System.Environment.GetEnvironmentVariable("ANDROID_HOME");
                ANDROID_SDK_ROOT = System.Environment.GetEnvironmentVariable("ANDROID_SDK_ROOT");

            }
            catch
            {
                AppendText("ANDROID_HOME or ANDROID_SDK_ROOT can't find in your system environment");
            }
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

            // 2024.02.03 기준으로는 아래와 같이 해야 한다.
            if (!File.Exists(sdkaMangerPath))
            {
                sdkaMangerPath = String.Format("{0} --list | find \"platforms;\"", System.IO.Path.Combine(defaultPath, "cmdline-tools", "8.0", "bin", "sdkmanager.bat"));
            }

            if (Directory.Exists(defaultPath) && isValid)
            {

                using (HostData process = new HostData(sdkaMangerPath, false, "", "echo ", ""))
                {
                    Append append = (output) =>
                    {
                        //if(String.IsNullOrEmpty(output))
                        //{
                        //    string sdkaMangerPath2 = String.Format("{0} --verbose --list", System.IO.Path.Combine(defaultPath, "tools", "bin", "sdkmanager.bat"));

                        //    using (HostData subProcess = new HostData(sdkaMangerPath2 + " >result.txt 2>&1", false, "", "echo ", ""))
                        //    {
                        //        subProcess.Run((str) => {
                        //            if (File.Exists("result.txt"))
                        //            {
                        //                output = File.ReadAllText("result.txt");
                        //                AppendText(output);
                        //                // File.Delete("redirect.txt");
                        //            }
                        //        });
                        //    }
                        //}

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
                throw new Exception(_rm.GetString("ENVIRONMENT_VAR_NOT_FOUND"));
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

                if(!_status[SDK.JDK] || !_status[SDK.KEYTOOL])
                {
                    // 자바 미설치
                    AppendText(_rm.GetString("NotInstalledJava1"));
                    AppendText(_rm.GetString("NotInstalledJava2"));
                }

                if (!_status[SDK.CORDOVA])
                {
                    // 코르도바 미설치
                    AppendText(_rm.GetString("NotInstalledCordova1"));
                    AppendText(_rm.GetString("NotInstalledCordova2"));
                    AppendText(_rm.GetString("NotInstalledCordova3"));
                }

                if (!_status[SDK.ANDROID_SDK])
                {
                    // 안드로이드 SDK
                    AppendText(_rm.GetString("NotInstalledAndroidSDK1"));
                    AppendText(_rm.GetString("NotInstalledAndroidSDK2"));
                    AppendText(_rm.GetString("NotInstalledAndroidSDK3"));
                }

                if (!_status[SDK.CLEANER])
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

            string JAVA_HOME = Environment.GetEnvironmentVariable("JAVA_HOME");
            string ANDROID_HOME = Environment.GetEnvironmentVariable("ANDROID_HOME");
            string ANDROID_SDK_ROOT = Environment.GetEnvironmentVariable("ANDROID_SDK_ROOT");

            DataManager.Instance.JDKPath = String.IsNullOrEmpty(JAVA_HOME) ? "" : JAVA_HOME;

            if (!String.IsNullOrEmpty(ANDROID_HOME))
            {
                AppendText(String.Format(_rm.GetString("FOUND_ANDROID_HOME"), ANDROID_HOME));
                _status[SDK.ANDROID_SDK] = true;
            }
            else if (!String.IsNullOrEmpty(ANDROID_SDK_ROOT)) // (Recommended)
            {
                AppendText(String.Format(_rm.GetString("FOUND_ANDROID_SDK_ROOT"), ANDROID_SDK_ROOT));
                _status[SDK.ANDROID_SDK] = true;
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
        /// 키스토어 파일이 없을 때, 키스토어 별칭을 매번 램덤하게 변경한다.
        /// </summary>
        public void InitWithDefaultTexts()
        {
            byte[] aliasText = new UTF8Encoding().GetBytes(DateTime.Now.ToString());

            // Base64로 뽑아낸 key alias을 8자만 남긴다.
            textBoxKeyAlias.Text = String.Join("", Convert.ToBase64String(aliasText).Take(8));
        }

        /// <summary>
        /// Form1이 로드되었을 때의 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Prepare();
            InitWithDefaultTexts();
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
            comboBoxMinSdkVersion.SelectedIndex = comboBoxMinSdkVersion.Items.IndexOf(DEFAULT_MINIMUM_SDK_VERSION); // Android 5.1
            comboBoxTargetSdkVersion.SelectedIndex = comboBoxTargetSdkVersion.Items.IndexOf(DEFAULT_TARGET_SDK_VERSION);
            comboBoxCompileSdkVersion.SelectedIndex = comboBoxCompileSdkVersion.Items.IndexOf(DEFAULT_COMPILE_SDK_VERSION); // Android 11
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
            Thread worker = new Thread(() =>
            {
                if (File.Exists(Path.Combine(Path.Combine(mainPath, "js", "plugins"))))
                {
                    AppendText("Cannot find a file from js/plugins.js");
                    return;
                }

                string path = Path.Combine(mainPath, "js", "plugins");

                // plugins.json 파일에 플러그인이 ON이라면 해당 플러그인에서 특정 코멘트를 찾는다 (50줄까지만 읽음)
                if (System.IO.Directory.Exists(path))
                {
                    System.IO.DirectoryInfo dir = new DirectoryInfo(path);

                    foreach (FileInfo fi in dir.GetFiles())
                    {
                        string filename = Path.GetFileNameWithoutExtension(fi.FullName);

                        if (_cordova.IsValidPlugin(filename))
                        {
                            #region 플러그인명 추출
                            using (StreamReader sr = fi.OpenText())
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
            if (buttonBuild.Enabled && textBoxPassWord.Text.Length < 6 && !_isWorking)
            {
                MessageBox.Show("The password value must enter at least 6 digits.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 빌드 중이 아닐 때에만 빌드가 가능함
            if (buttonBuild.Enabled && !_isWorking)
            {

                _isWorking = true;
                buttonBuild.Enabled = false;

                var config = new Data.FormData.Config()
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

                        // TODO: "RPG Maker MV Cordova Builder"/YourApplication/android/app/build/outputs/apk/release/
                        string buildMode = (config.buildMode == 0) ? "release" : "debug";
                        string targetPath = System.IO.Path.Combine(DataManager.Instance.GetRootDirectory(), textBoxFolderName.Text, "platforms", "android", "app", "build", "outputs", "apk", buildMode);

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
            bool isValid = false;

            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string targetFolder = folderBrowserDialog.SelectedPath;

                // 대상 폴더에 HTML 파일이 있는지 확인합니다.
                if (File.Exists(Path.Combine(targetFolder, "index.html")))
                {
                    isValid = true;
                }

                // www 폴더에 HTML 파일이 있는지 확인합니다.
                string wwwFolder = Path.Combine(targetFolder, "www", "index.html");    
                if(File.Exists(wwwFolder))
                {
                    targetFolder = Path.Combine(targetFolder, "www");
                    isValid = true;
                }

                if(!isValid)
                {
                    StringBuilder message = new StringBuilder();

                    if(Thread.CurrentThread.CurrentUICulture.Name == "ko-KR")
                    {
                        message.Append("선택된 게임 폴더에 index.html 파일이 없습니다.");
                    } 
                    else
                    {
                        message.Append("index.html File Not Found");
                    }
                        
                    MessageBox.Show(message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                textBoxSettingGameFolder.Text = targetFolder;
                   
                ReadCordovaPlugins(targetFolder);
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
        /// 빌드 로그를 초기화합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearBuildLog(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        /// <summary>
        /// 빌드 로그를 저장합니다.
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

        private void comboBoxMinSdkVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckWithAndroidSDK(comboBoxMinSdkVersion.Text);
        }

        private void CheckWithAndroidSDK(string text)
        {
            int value = Int32.Parse(text);
            if (!installedSDKs.Contains(value))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "ko-KR")
                    AppendText($"안드로이드 SDK {value}{GetHangulPost(value)} 설치되어있지 않습니다.");
                else
                    AppendText($"Android SDK {value} is not installed.");
            }
        }

        /// <summary>
        /// 한국어 조사를 처리합니다.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetHangulPost(int value)
        {
            string[] items = { "영", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구" };
            int target = value.ToString().ToCharArray().Last();

            int range = target - 48;
            if(range > items.Count())
            {
                return "이";
            }

            string targetStr = items[target - 48];

            int code = targetStr.ToCharArray().First();
            int offset = code - 44032;

            int first = offset / 588;
            int middle = (offset % 588) / 28;
            int final = offset % 28;

            return (final == 0) ? "가" : "이";

        }

        private void comboBoxTargetSdkVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckWithAndroidSDK(comboBoxTargetSdkVersion.Text);
        }

        private void comboBoxCompileSdkVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckWithAndroidSDK(comboBoxCompileSdkVersion.Text);
        }
    }
}
