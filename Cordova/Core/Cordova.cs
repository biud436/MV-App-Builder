﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using System.Xml;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Net.Http;
using Cordova.Forms;
using Cordova.Models.FormData;

namespace Cordova.Core
{
    using Common.Http;
    using Common.IO;
    using Common.Json;
    using Common.Localization;
    using Models;
    using Entities;
    using Common.Versions;

    public class Cordova
    {

        // 메인 폼
        private Form1 _mainForm;

        // 지역화를 위한 리소스 관리자
        private ResourceManagerAdapter _rm = ResourceManagerFactory.Create();

        // 플러그인 목록
        private Dictionary<string, bool> _plugins = new Dictionary<string, bool>();

        // 버전
        private Version _version = new Version("0.7.0");

        private Version _cordovaVersion = new Version("0.0.0");

        private string _titleText;

        private Config _config;

        private VersionManager _versionManager;

        /// <summary>
        /// 생성자
        /// </summary>
        public Cordova()
        {
            InitializeDataRepository();
            InitializeVersionManager();
        }

        private void InitializeVersionManager()
        {
            _versionManager = new VersionManager(this);
        }

        public void InitializeDataRepository()
        {
            DataRepository.Instance.SetCordovaObject(this);
            DataService data = DataService.Instance;

            // 출력 폴더를 바꾼 적이 없다면 기본적으로는 내문서로 설정됩니다.
            if (!DataRepository.Instance.IsValidCustomOutputPath) 
            {
                DataRepository.Instance.OutputPath = DataService.Instance.GetRootDirectory();
            } else
            {
                // 출력 폴더를 바꾼 적이 있다면 파일에서 불러오게 됩니다.
                DataService.Instance.Type = DataService.DataFolderType.CUSTOM;
            }
        }

        public ResourceManagerAdapter GetResourceManager()
        {
            return _rm;
        }

        /// <summary>
        /// 메인 폼을 설정합니다.
        /// </summary>
        /// <param name="form"></param>
        public void SetMainForm(Form1 form)
        {
            _mainForm = form;
            _titleText = _mainForm.Text;
        }

        /// <summary>
        /// Create the build.json file for Android
        /// </summary>
        /// <param name="filename"></param>
        public void ExportBuildJson(string filename)
        {
            BuildConfig config = new BuildConfig();
            KeystoreConfig data = new KeystoreConfig()
            {
                keystore = _config.keyPath,
                storePassword = _config.passWord,
                alias = _config.keyAlias,
                password = _config.passWord,
                keystoreType = "",
                packageType = "apk", // or bundle
            };

            AndroidBuildConfig androidConfig = new AndroidBuildConfig()
            {
                debug = data,
                release = data
            };

            config.android = androidConfig;

            string json = JsonConvert.SerializeObject(config, new JsonSerializerSettings()
            {
                Formatting = Newtonsoft.Json.Formatting.Indented
            });

            File.WriteAllText(filename, json.ToString());

        }

        /// <summary>
        /// 메이크 함수는 작업 폴더를 프로젝트 폴더로 옮기는 역할을 합니다.
        /// </summary>
        /// <param name="callback"></param>
        public void Make(Action<string> callback)
        {
            try
            {
                string myDocumentsPath = DataService.Instance.GetRootDirectory();
                string mkdir = Path.Combine(myDocumentsPath, _config.folderName);

                if (!Directory.Exists(mkdir))
                {
                    Directory.CreateDirectory(mkdir);
                }

                string tempDir = Directory.GetCurrentDirectory();
                string filename = "build.json";

                string targetPath = mkdir;

                Directory.SetCurrentDirectory(targetPath);

                callback(filename);

                Directory.SetCurrentDirectory(tempDir);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 빌드를 진행합니다.
        /// </summary>
        /// <param name="successCallback"></param>
        public void Build(Config config, Action successCallback)
        {

            _config = config;

            Thread worker = new Thread(new ThreadStart(() =>
            {
                // 출력 폴더에 APK 파일을 저장합니다.
                string targetFolder = DataService.Instance.GetRootDirectory();
                string tempDir = Directory.GetCurrentDirectory();

                // 타겟 폴더를 현재 경로로 설정합니다.
                Directory.SetCurrentDirectory(targetFolder);

                // 폴더를 생성합니다
                bool isValid = Create();
                isValid = DataService.Instance.IsValidPath(targetFolder);

                // 다시 되돌립니다.
                Directory.SetCurrentDirectory(tempDir);

                if (isValid)
                {

                    Make(filename =>
                    {
                        AddAndroidPlatform();
                        AddPlugins();
                        WriteConfig();
                        CreateKeyStore();
                        Requirements();

                        if (DataRepository.Instance.Use)
                        {
                            ExcludeUnusedFiles();
                        } else
                        {
                            CopyProjectFiles();
                        }

                        ModifyHtmlFiles(); // HTML 파일에 cordova 바인드 용 스크립트 문을 추가합니다.
                        ExportBuildJson(filename);
                        Flush(); // 빌드를 시작합니다.
                        successCallback();

                    });

                } else
                {
                    throw new FileNotFoundException("There contains non-ASCII characters in the File Path!");
                }
            }));

            worker.Start();
        }

        /// <summary>
        /// 메인 폼의 빌드 로그에 새로운 로그를 한 줄 추가합니다.
        /// </summary>
        /// <param name="output"></param>
        public void AppendText(string output)
        {
            this._mainForm.AppendText(output);
        }

        /// <summary>
        /// <see cref="https://stackoverflow.com/a/2766718"/>
        /// </summary>
        /// <param name="FolderName"></param>
        private void clearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                clearFolder(di.FullName);
                di.Delete();
            }
        }

        /// <summary>
        /// 코르도바 프로젝트를 생성합니다.
        /// 8.1.2 버전에서는 이미 생성되어있을 경우, 시간 절약을 위해 다시 생성하지 않습니다.
        /// 하지만 9.0.0 버전에서는 오류로 인해 프로젝트를 지웠다가 다시 생성합니다.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            string folderName = _config.folderName;
            string packageName = _config.packageName;
            string gameName = _config.gameName;

            try
            {
                // TODO:
                // 코르도바 프로젝트가 이미 생성되었다 => true이면 생략 
                //      - folderName가 존재한다 => true이면 아래를 또 확인
                //          - folderName/config.xml 파일이 존재한다
                //          - folderName/res 폴더가 있다.
                //
                // 가장 간단한 처리는 폴더가 존재하면 삭제하고 재생성하는 방법
                // 
                if (Directory.Exists(folderName) )
                {
                    var configPath = Path.Combine(folderName, "config.xml");

                    if(File.Exists(configPath))
                    {

                        return true;
                    }

                }

            }
            catch (System.Exception ex)
            {
                AppendText(ex.Message);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("cordova create {0} {1} {2}", folderName, packageName, gameName);
            var ret = false;

            using (HostProcessRunner process = new HostProcessRunner(sb.ToString(), true, "",
                _rm.GetString(ResourceTokens.Create1),
                _rm.GetString(ResourceTokens.Create2)))
            {
                Append append = AppendText;
                ret = process.Run(append);
            }

            return ret;
        }

        /// <summary>
        /// Adds the Android platform
        /// </summary>
        private bool AddAndroidPlatform()
        {
            // TODO:
            // 안드로이드 플랫폼이 이미 추가되었다 => true이면 생략
            //      - JSON 필요의 경우, folderName/package.json 파일에 cordova.platforms에 "android"가 존재할 때
            //      - JSON이 필요하지 않은 경우, folderName/platforms/android/android.json 파일이 있다

            var ret = false;

            if(!File.Exists(@"platforms/android/android.json"))
            {
                using (HostProcessRunner process = new HostProcessRunner
                    (
                        "cordova platform add android", 
                        true, 
                        "", 
                        _rm.GetString(ResourceTokens.AddAndroidPlatform1), 
                        _rm.GetString(ResourceTokens.AddAndroidPlatform2))
                    )
                {
                    Append append = AppendText;
                    ret = process.Run(append);
                }
            }

            return ret;

        }

        /// <summary>
        /// Shows up the requiements information to Build Log
        /// </summary>
        /// <returns></returns>
        private bool Requirements()
        {
            var process = new HostProcessRunner("cordova requirements", true, "", _rm.GetString(ResourceTokens.Requirements1), _rm.GetString(ResourceTokens.Requirements2));

            Append append = AppendText;

            return process.Run(append);
        }

        private Cordova SetPreference(XmlDocument xmlDoc, string name, string value)
        {
            var element = xmlDoc.CreateNode(XmlNodeType.Element, "preference", null);

            var nameAttr = xmlDoc.CreateAttribute("name");
            nameAttr.Value = name;

            var valueAttr = xmlDoc.CreateAttribute("value");
            valueAttr.Value = value;

            element.Attributes.Append(nameAttr);
            element.Attributes.Append(valueAttr);

            xmlDoc.DocumentElement.AppendChild(element);

            return this;
        }

        /// <summary>
        /// 아이콘 이미지를 설정합니다.
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        private Cordova SetIcon(XmlDocument xmlDoc)
        {
            if (!File.Exists(Path.Combine(DataService.Instance.GetRootDirectory(), "www", "icon", "icon.png"))) {
                AppendText("The www/icon/icon.png file could not found.");
                return this;
            }

            var element = xmlDoc.CreateNode(XmlNodeType.Element, "icon", null);

            var srcAttr = xmlDoc.CreateAttribute("src");

            srcAttr.Value = "www/icon/icon.png";

            element.Attributes.Append(srcAttr);

            xmlDoc.DocumentElement.AppendChild(element);

            return this;
        }

        /// <summary>
        /// config.xml 파일에 화면 방향과 같은 추가 사항을 기록합니다.
        /// <see cref="https://cordova.apache.org/docs/en/latest/config_ref/#preference"/>
        /// </summary>
        private void WriteConfig()
        {
            try
            {
                if (File.Exists("config.xml"))
                {
                    XmlDocument xmlDoc = new XmlDocument();

                    xmlDoc.Load("config.xml");

                    this
                        .SetPreference(xmlDoc, "Orientation", _config.orientation)
                        .SetPreference(xmlDoc, "FullScreen", _config.fullscreen)
                        .SetPreference(xmlDoc, "android-minSdkVersion", _config.minSdkVersion)
                        .SetPreference(xmlDoc, "android-targetSdkVersion", _config.targetSdkVersion)
                        .SetPreference(xmlDoc, "android-compileSdkVersion", _config.compileSdkVersion) // it didn't exist property.
                        .SetPreference(xmlDoc, "BackgroundColor", "0xff000000") // black color
                        .SetPreference(xmlDoc, "ShowTitle", "false")
                        .SetIcon(xmlDoc);

                    xmlDoc.Save("config.xml");

                    AppendText(_rm.GetString(ResourceTokens.WriteConfig));
                }

            }
            catch (System.Exception ex)
            {
                AppendText(ex.Message);
            }


        }

        /// <summary>
        /// 키스토어 파일을 생성합니다.
        /// </summary>
        public void CreateKeyStore()
        {

            string keystorePath = _config.keyPath;

            // 키스토어 파일이 이미 있다면
            if (File.Exists(keystorePath))
            {
                AppendText(_rm.GetString(ResourceTokens.ALREADY_EXISTED_KEYSTORE));
                return;
            }

            // TODO: 이 코드는 보기 좋지 않은 듯 하다.
            // 텍스트 목록을 한 번에 가져올 수 있는 섹시한 함수가 있을까?
            // 있다면 그때 변경하자.
            List<string> validData = new List<string>()
            {
                        keystorePath,
                        _config.keyAlias,
                        _config.passWord,
                        _config.passWord,
                        _config.packageName,
                        _config.keyOU,
                        _config.keyO,
                        _config.keyL,
                        _config.keyS,
                        _config.keyC
            };

            bool isOK = validData.All(text => !String.IsNullOrEmpty(text));

            if (isOK)
            {
                // TODO: 전달되는 인수가 너무 많다.
                // 더 간단한 처리 방법이 없을까?
                string cmd = String.Format("keytool -genkeypair -v -keystore {0} -alias {1} -keyalg RSA -keysize 2048 -validity 10000 -keypass {2} -storepass {3} -dname \"cn={4}, ou={5}, o={6}, l={7}, s={8}, c={9}\" 2>&1",
                        keystorePath,
                        _config.keyAlias,
                        _config.passWord,
                        _config.passWord,
                        _config.packageName,
                        _config.keyOU,
                        _config.keyO,
                        _config.keyL,
                        _config.keyS,
                        _config.keyC
                    );

                Append append = AppendText;

                bool status = false;

                using (HostProcessRunner hostData = new HostProcessRunner
                    (
                        cmd, 
                        false, 
                        "", 
                        _rm.GetString(ResourceTokens.CreateKeyStore1), 
                        _rm.GetString(ResourceTokens.CreateKeyStore2))
                    )
                {
                    status = hostData.Run(append);
                }

                // 키스토어가 정상적으로 생성되었다면 status가 true이다.
                if (status)
                {
                    AppendText(_rm.GetString(ResourceTokens.CreateKeyStore3));
                }
                else
                {
                    AppendText(_rm.GetString(ResourceTokens.CreateKeyStore4));
                }

            }
            else
            {
                // if the text is empty, try to show the error message.

                // 텍스트가 비어있을 때의 처리
                // 정상적으로 실행하였다면 이 부분은 절대 실행될 일이 없다.
                AppendText(_rm.GetString(ResourceTokens.CreateKeyStore5));
            }

        }

        /// <summary>
        /// index.html 파일을 수정합니다.
        /// </summary>
        private void ModifyHtmlFiles()
        {
            var filename = @"www/index.html";

            if(!File.Exists(filename))
            {
                AppendText("Cannot find www/index.html. Note that this tool require an index.html file. Please check that");
                return;
            }

            var lines = File.ReadAllLines(filename, Encoding.UTF8).ToList();
            string matchLine = null;
            bool isValid = false;

            foreach (var line in lines)
            {
                if (line.Contains("<script "))
                {
                    matchLine = line;
                    isValid = true;
                    break;
                }
            }

            if (isValid)
            {
                var index = lines.IndexOf(matchLine);
                if (index != -1)
                {
                    lines.Insert(index, "        <script type=\"text/javascript\" src=\"cordova.js\"></script>");
                    lines.Insert(index + 1, "        <script type=\"text/javascript\">(function(){document.addEventListener(\"deviceready\",function(){var mainloopid = setInterval(mainloop,1000); function mainloop(){ window.plugins.insomnia.keepAwake();} },false);})();</script>");

                    File.WriteAllLines(filename, lines);

                    AppendText(_rm.GetString(ResourceTokens.ModifyHtmlFiles));
                }
            }

        }

        /// <summary>
        /// 프로젝트 파일을 복사합니다.
        /// </summary>
        private void CopyProjectFiles()
        {
            string srcPath = _config.settingGameFolder;
            string dstPath = ".\\www";

            AppendText(_rm.GetString(ResourceTokens.CopyProjectFiles1));

            if (Directory.Exists(srcPath))
            {
                // 다중 쓰레드 (기본값 : 8 쓰레드)
                // 원본과 동일한 트리로 유지
                string robocopy = String.Format("chcp {0} | robocopy \"{1}\" \"{2}\" /MIR /E /R:1 /W:1", Encoding.UTF8.CodePage, srcPath, dstPath);

                using (var process = new HostProcessRunner(robocopy, true, "",
                    _rm.GetString(ResourceTokens.CopyProjectFiles2),
                    _rm.GetString(ResourceTokens.CopyProjectFiles3)))
                {

                    Append append = AppendText;

                    process.Run(append);
                }

            } else
            {
                AppendText(_rm.GetString(ResourceTokens.CopyProjectFiles4));
            }

        }

        /// <summary>
        /// Add new plugin.
        /// </summary>
        private void AddPlugins()
        {

            foreach(string pluginName in _config.plugins)
            {
                string command = String.Format("cordova plugin add {0}", pluginName);
                string success = String.Format(_rm.GetString(ResourceTokens.AddPlugins1), pluginName);
                string fail = String.Format(_rm.GetString(ResourceTokens.AddPlugins2), pluginName);

                using (var process = new HostProcessRunner(command, true, "", success, fail))
                {
                    Append append = AppendText;
                    process.Run(append);
                }
            }

        }

        /// <summary>
        /// 다음은 cordova-android v8.1.0 버전에서 발생하는 빌드 오류를 수정합니다.
        /// </summary>
        private void CreateBuildExtrasFile()
        {
            if (_cordovaVersion.Major >= 9)
            {
                // 오류 막기...
                if (!File.Exists("platforms/android/build-extras.gradle"))
                {
                    
                    string contents = @"
android {
    lintOptions {
        checkReleaseBuilds false
        abortOnError false
    }
}
                ";

                    File.WriteAllText("platforms/android/build-extras.gradle", contents);

                }
            }
        }

        /// <summary>
        /// Build current project after adding plugins
        /// </summary>
        private bool Flush()
        {
            if(File.Exists("build.json"))
            {
                AppendText(_rm.GetString(ResourceTokens.Flush1));
            } else
            {
                AppendText("Cannot find the build.json file to your project folder");
            }

            CreateBuildExtrasFile();

            string mode = (_config.buildMode == 0) ? "--release" : "--debug";
            string cmd = String.Format("cordova build android {0} --buildConfig=build.json", mode);

            // 코르도바는 빌드가 완료되면, 정상 출력 루트가 아닌 오류 쪽으로 빌드 완료 메시지를 보낸다.
            // 따라서, 오류 메시지를 정상인 것처럼 처리할 경우, 프로세스가 종료되지 않고 영원히 남게 된다.
            // TODO: 
            // 따라서 빌드 실패 메시지에 빌드 성공 메시지를 넣어야 한다.
            // 다만, 이 경우 빌드 실패 시에도 빌드가 성공했다고 메시지가 뜨게 된다.
            // 구분할 수 있는 방법은 아직까지 없다. 리치 텍스트 박스에서 fail 글자를 추출하지 않는한 불가능하다.
            bool ret = false;

            using(HostProcessRunner process = new HostProcessRunner(cmd, true, "", _rm.GetString(ResourceTokens.Flush2), _rm.GetString(ResourceTokens.Flush3)))
            {
                Append append = AppendText;
                ret = process.Run(append);

                if(!ret)
                {
                    AppendText("Unknown error. Please run a command called 'cordova build android' manually in your command line.");
                }
            }

            return ret;

        }

        /// <summary>
        /// Install cordova to user computer on Windows
        /// </summary>
        public void InstallCordova()
        {

            AppendText(_rm.GetString(ResourceTokens.INSTALLING_CORDOVA));

            using (var process = new HostProcessRunner("npm install -g cordova", true, "", _rm.GetString(ResourceTokens.SUCCESS_INSTALLED_CORDOVA), _rm.GetString(ResourceTokens.FAIL_INSTALLED_CORDOVA)))
            {
                Append append = AppendText;
                process.Run(append);
            }

        }

        /// <summary>
        /// Reads the plugins.js file and Checks whether the plugin's status is to true in the project folder.
        /// </summary>
        /// <param name="projectPath">The project path that contains the www folder.</param>
        public void ReadProjectPluginsJson(string projectPath)
        {
            string rootPluginFile = Path.Combine(projectPath, "js", "plugins.js");
            bool isEmptyHTMLFile = !File.Exists(Path.Combine(projectPath, "index.html"));
            bool isEmptyPluginFile = !File.Exists(rootPluginFile);

            if (isEmptyHTMLFile) 
            {
                return;
            }

            if(isEmptyPluginFile) {
                AppendText("Cannot find a file from js/plugins.js");
                return;
            }

            string realPath = rootPluginFile;
            IEnumerable<string> fakeLines = File.ReadLines(realPath).Skip(4);
            int length = fakeLines.Count() - 1;
            IEnumerable<string> lines = fakeLines.Take(length);

            foreach (string json in lines)
            {
                string goodJson = Regex.Replace(json, @"}}\s*,\s*$", "}}");

                PluginConfig pluginConfig = JsonHelper.ToClass<PluginConfig>(goodJson);
                _plugins[pluginConfig.name] = pluginConfig.status;
            }

        }

        /// <summary>
        /// Check whether certain plugin is valid.
        /// </summary>
        /// <param name="pluginName">Filename that omitted the file extension</param>
        /// <returns></returns>
        public bool IsValidPlugin(string pluginName)
        {
            bool isValid = false;

            if (_plugins.ContainsKey(pluginName))
            {
                isValid = _plugins[pluginName];
            }

            return isValid;
        }

        /// <summary>
        /// Update program after downloading from the server.
        /// However, it doesn't have a progress bar.
        /// </summary>
        public async Task StartDownloadAndRunAsync(Version targetVersion)
        {
            try
            {
                var downloadClient = new AppDownloadClient();
                var fileManager = new FileManager();

                byte[] bytes = await downloadClient.DownloadAppAsync(targetVersion.ToString());
                AppendText(_rm.GetString(ResourceTokens.SUCCESSED_SETUP_FILE));

                string folderPath = DataService.Instance.GetRootDirectory();
                string fileName = "MVAppBuilder.exe";
                string savedFilePath = fileManager.SaveFile(folderPath, fileName, bytes);

                fileManager.RunApplication(savedFilePath);
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task CheckVersionAsync()
        {
            if (_version == null)
            {
                return;
            }

            using (WebClient wc = new WebClient())
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                string targetJson = wc.DownloadString("https://raw.githubusercontent.com/biud436/MV-App-Builder/master/version.json");
                var targetObject = JsonHelper.ToClass<Dictionary<string, string>>(targetJson);

                if (targetObject.ContainsKey("version"))
                {
                    var targetVersion = new Version(targetObject["version"]);
                    var result = _version.CompareTo(targetVersion);

                    if (result > 0)
                    {
                        // You are using the alpha version that has not been released yet.
                        AppendText(_rm.GetString(ResourceTokens.CHECK_VERSION_ALPHA));
                    }
                    else if (result < 0)
                    {
                        // You are using the older version
                        MessageBox.Show(_rm.GetString(ResourceTokens.CHECK_VERSION_OLD), _mainForm.Text);

                        if (MessageBox.Show(_rm.GetString(ResourceTokens.CHECK_VERSION_OLD_ASK), _mainForm.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            await StartDownloadAndRunAsync(targetVersion);
                        }
                    }
                    else
                    {
                        // You are using the latest version
                        AppendText(_rm.GetString(ResourceTokens.CHECK_VERSION_LATEST));
                    }

                }
            }
        }

        public string GetMainFormText()
        {
            return _mainForm.Text;
        }

        /// <summary>
        /// Checks the latest cordova version using npm
        /// </summary>
        /// <returns></returns>
        public void CheckLatestCordovaVersion()
        {
            _versionManager.CheckLatestCordovaVersion();
        }

        public void SetCordovaVersion(Version version)
        {
            _cordovaVersion = version;
        }

        /// <summary>
        /// Checks the latest cordova version from cordova-android.git
        /// </summary>
        /// <returns></returns>
        public void CheckLastestCordovaVersionFromGit()
        {
            Version cordovaVersion = new Version("0.0.0");

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            try
            {

                using (WebClient wc = new WebClient())
                {
                    string releaseNote = wc.DownloadString(new Uri("https://raw.githubusercontent.com/apache/cordova-cli/master/RELEASENOTES.md"));

                    var regex = new Regex("[\r\n]+", RegexOptions.IgnoreCase);
                    var lines = regex.Split(releaseNote);
                    var matchedData = "# Cordova-cli Release Notes";
                    var findIndex = 0;

                    // Try to read around the target index.
                    int startLineNumber = 19;
                    int endLineNumber = 28;

                    for (var i = startLineNumber; i < endLineNumber; i++)
                    {
                        if (lines[i] == matchedData)
                        {
                            findIndex = i;
                            break;
                        }
                    }

                    if (findIndex > 0)
                    {
                        findIndex += 1;

                        // Try to extract the string that includes version
                        regex = new Regex(@"[#]{3}[ ]*(\d+\.\d+\.\d+)", RegexOptions.IgnoreCase);
                        Match m = regex.Match(lines[findIndex]);

                        if (m.Success)
                        {
                            cordovaVersion = new Version(m.Groups[1].Value);
                        }

                    }
                    else
                    {
                        // Cannot find the version text in the github.
                        return;
                    }

                }

                // Create the lightweight command process and check the version of installed Cordova in the local system.
                using (var tempCmdProcess = new HostProcessRunner("", true, "", ""))
                {
                    // Create the lightweight command process
                    var localCordovaVersion = new Version("0.0.0");

                    // check the version of installed Cordova in the local system.
                    tempCmdProcess.outputLine("cordova --version", (string output) => {

                        // Extract the text that contains the version using the regular expression.
                        Regex regex = new Regex(@"(\d+\.\d+\.\d+)[ ]+", RegexOptions.IgnoreCase);
                        Match m = regex.Match(output);

                        if (m.Success)
                        {
                            localCordovaVersion = new Version(m.Groups[1].Value);
                            var result = cordovaVersion.CompareTo(localCordovaVersion);

                            if (result > 0)
                            {
                                // it will be updated the cordova automatically if you are using older version of it.
                                AppendText(_rm.GetString(ResourceTokens.NOT_LATEST_CORDOV_VER));
                                AppendText(String.Format(_rm.GetString(ResourceTokens.REQUEST_NPM_INSTALL), cordovaVersion.ToString()));

                                DialogResult dialogResult = MessageBox
                                    .Show(
                                        _rm.GetString(ResourceTokens.REQUEST_NPM_INSTALL_ASK), 
                                        _mainForm.Text, 
                                        MessageBoxButtons.YesNo, 
                                        MessageBoxIcon.Information
                                    );

                                if (dialogResult == DialogResult.Yes)
                                {
                                    InstallCordova();
                                }

                            }
                            else if (result < 0)
                            {
                                // Why you use dev version? I don't understand.
                            }
                            else
                            {
                                // You are using the latest version of Cordova already.
                                sw.Stop();
                                AppendText(_rm.GetString(ResourceTokens.LATEST_CORDOVA_READY) + "(" + sw.Elapsed.ToString() + ")");
                            }

                        }
                    });
                }


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                _cordovaVersion = cordovaVersion;
            }
        }


        /// <summary>
        /// Installing the tool for excluding unused resources.
        /// </summary>
        public void InstallResourceCleaner()
        {
            string success_installed_cleaner = "MV 미사용 리소스 제거기를 성공적으로 내려 받았습니다.";
            string fail_installed_cleaner = "MV 미사용 리소스 제거기 설치에 실패하였습니다";
            Append append = AppendText;

            using (var process = new HostProcessRunner("npm install -g mv-exclude-unused-files", true, "", success_installed_cleaner, fail_installed_cleaner))
            {
                process.Run(append);
            }
               
        }

        /// <summary>
        /// 미사용 리소스를 제거하는 옵션입니다.
        /// </summary>
        public void ExcludeUnusedFiles()
        {
            string src = _config.settingGameFolder;
            src = src.Replace(@"\", "/");

            string myDocumentsPath = DataService.Instance.GetRootDirectory();
            string dst = Path.Combine(myDocumentsPath, _config.folderName, "www");
            dst = dst.Replace(@"\", "/");

            Dictionary<string, string> option = new Dictionary<string, string>();

            Append append = AppendText;

            option["--audioFileFormat="] = DataRepository.Instance.AudioFileFormat;
            option["--remainTree="] = DataRepository.Instance.RemainTree ? "true" : "false";

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("mv-resource-cleaner \"{0}\" \"{1}\"", src, dst);

            foreach (KeyValuePair<string, string> pair in option)
            {
                sb.AppendFormat(" {0}{1}", pair.Key, pair.Value);
            }

            System.Diagnostics.Debug.WriteLine(sb.ToString());

            using (var process = new HostProcessRunner(sb.ToString(), true, "", "Unused resource removal succeeded.", "Failed to remove unused resources."))
            {
                process.Run(append);
            }

        }

        public int GetTotalSize()
        {
            string path = _config.settingGameFolder;

            var files = Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
            var totalSizeBytes = files.Sum(file => {
                FileInfo fi = new FileInfo(file);
                return fi.Length;
            });

            var totalSize = Convert.ToInt32(totalSizeBytes / (1024.0 * 1024.0));

            return totalSize;
        }

    }

}