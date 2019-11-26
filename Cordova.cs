using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Xml;
using Newtonsoft.Json;
using System.Data;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Net;
using System.IO;
using System.Net.Http;

namespace Cordova_Builder
{
    public class Cordova
    {

        // 메인 폼
        private Form1 _mainForm;

        // 현재 경로
        private string _currentDirectory;

        // UI 컨트롤 목록
        private TextBoxList _list;

        // 지역화를 위한 리소스 관리자
        private ResourceManager _rm;

        // 플러그인 목록
        private Dictionary<string, bool> _plugins = new Dictionary<string, bool>();

        // 버전
        private Version _version = new Version("0.1.36");

        private Version _cordovaVersion = new Version("0.0.0");

        private string _titleText;

        /// <summary>
        /// 생성자
        /// </summary>
        public Cordova()
        {
            _currentDirectory = System.IO.Directory.GetCurrentDirectory();
            _rm = new ResourceManager("Cordova_Builder.locale", Assembly.GetExecutingAssembly());
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
        /// 텍스트 박스 목록을 가져옵니다.
        /// </summary>
        /// <param name="list"></param>
        public void Bind(TextBoxList list)
        {
            this._list = list;
        }

        /// <summary>
        /// Create the build.json file for Android
        /// </summary>
        /// <param name="filename"></param>
        public void ExportBuildJson(string filename)
        {
            BuildConfig config = new BuildConfig();
            ConfigData data = new ConfigData()
            {
                keystore = _list.keyPath.Text,
                storePassword = _list.passWord.Text,
                alias = _list.keyAlias.Text,
                password = _list.passWord.Text,
                keystoreType = "",
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

            System.IO.File.WriteAllText(filename, json.ToString());

        }

        /// <summary>
        /// 메이크 함수는 작업 폴더를 프로젝트 폴더로 옮기는 역할을 합니다.
        /// </summary>
        /// <param name="callback"></param>
        public void Make(Action<string> callback)
        {
            try
            {
                string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string programName = _titleText;
                string mkdir = System.IO.Path.Combine(myDocumentsPath, programName, _list.folderName.Text);

                if (!System.IO.Directory.Exists(mkdir))
                {
                    System.IO.Directory.CreateDirectory(mkdir);
                }

                string tempDir = System.IO.Directory.GetCurrentDirectory();
                string filename = "build.json";

                string targetPath = mkdir;

                System.IO.Directory.SetCurrentDirectory(targetPath);

                callback(filename);

                System.IO.Directory.SetCurrentDirectory(tempDir);

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 빌드를 진행합니다.
        /// </summary>
        /// <param name="successCallback"></param>
        public void Build(Action successCallback)
        {

            Thread worker = new Thread(new ThreadStart(() =>
            {

                // 내문서에 APK 파일을 저장합니다.
                string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string programName = _titleText;
                string targetFolder = System.IO.Path.Combine(myDocumentsPath, programName);
                string tempDir = System.IO.Directory.GetCurrentDirectory();

                // 내문서에 프로그램 폴더를 생성합니다.
                if(!System.IO.Directory.Exists(targetFolder)) {
                    System.IO.Directory.CreateDirectory(targetFolder);
                }

                // 타겟 폴더를 현재 경로로 설정합니다.
                System.IO.Directory.SetCurrentDirectory(targetFolder);

                // 폴더를 생성합니다
                bool isValid = Create();

                // 다시 되돌립니다.
                System.IO.Directory.SetCurrentDirectory(tempDir);

                if (isValid)
                {
                    Make(filename =>
                    {
                        AddAndroidPlatform();
                        AddPlugins();
                        WriteConfig(); // config.xml 파일을 수정합니다
                        _mainForm.CreateKeyStore();
                        CopyProjectFiles();
                        ModifyHtmlFiles(); // HTML 파일에 cordova 바인드 용 스크립트 문을 추가합니다.
                        ExportBuildJson(filename); 
                        Flush(); // 빌드를 시작합니다.
                        successCallback();
                    });
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
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(FolderName);

            foreach (System.IO.FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (System.IO.DirectoryInfo di in dir.GetDirectories())
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
            string folderName = _list.folderName.Text;
            string packageName = _list.packageName.Text;
            string gameName = _list.gameName.Text;

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
                if (System.IO.Directory.Exists(folderName) )
                {
                    var configPath = System.IO.Path.Combine(folderName, "config.xml");

                    if(System.IO.File.Exists(configPath))
                    {

                        var uniqueVersion = new Version("9.0.0");
                        var result = _cordovaVersion.CompareTo(uniqueVersion);

                        if (result >= 0)
                        {
                            
                            //HostData cleanProcess = new HostData("cordova clean android", true, "", "echo Clean Android Project for rebuilding...", "echo Clean Android Project failed...");
                            //Append DoAppend = AppendText;
                            //cleanProcess.Run(DoAppend);

                            clearFolder(folderName);
                        }
                        else {
                            return true;
                        }

                    }

                }

            }
            catch (System.Exception ex)
            {
                AppendText(ex.Message);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("cordova create {0} {1} {2}", folderName, packageName, gameName);
            HostData process = new HostData(sb.ToString(), true, "",
                _rm.GetString("Create1"),
                _rm.GetString("Create2"));
            Append append = AppendText;
            
            return process.Run(append);
        }

        /// <summary>
        /// Adds the Android platform
        /// </summary>
        private void AddAndroidPlatform()
        {
            // TODO:
            // 안드로이드 플랫폼이 이미 추가되었다 => true이면 생략
            //      - JSON 필요의 경우, folderName/package.json 파일에 cordova.platforms에 "android"가 존재할 때
            //      - JSON이 필요하지 않은 경우, folderName/platforms/android/android.json 파일이 있다

            if(!System.IO.File.Exists(@"platforms/android/android.json"))
            {
                HostData process = new HostData("cordova platform add android", true, "", _rm.GetString("AddAndroidPlatform1"), _rm.GetString("AddAndroidPlatform2"));

                Append append = AppendText;

                process.Run(append);
            }

        }

        /// <summary>
        /// Shows up the requiements information to Build Log
        /// </summary>
        /// <returns></returns>
        private bool Requirements()
        {
            var process = new HostData("cordova requirements", true, "", _rm.GetString("Requirements1"), _rm.GetString("Requirements2"));

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
                if (System.IO.File.Exists("config.xml"))
                {
                    XmlDocument xmlDoc = new XmlDocument();

                    xmlDoc.Load("config.xml");

                    this
                        .SetPreference(xmlDoc, "Orientation", _list.orientation.SelectedItem.ToString())
                        .SetPreference(xmlDoc, "FullScreen", _list.fullscreen.SelectedItem.ToString())
                        .SetPreference(xmlDoc, "android-minSdkVersion", _list.minSdkVersion.SelectedItem.ToString())
                        .SetPreference(xmlDoc, "android-targetSdkVersion", _list.targetSdkVersion.SelectedItem.ToString())
                        .SetPreference(xmlDoc, "android-compileSdkVersion", _list.compileSdkVersion.SelectedItem.ToString()) // it didn't exist property.
                        .SetPreference(xmlDoc, "BackgroundColor", "0xff000000") // black color
                        .SetPreference(xmlDoc, "ShowTitle", "false")
                        .SetIcon(xmlDoc);

                    xmlDoc.Save("config.xml");

                    AppendText(_rm.GetString("WriteConfig"));
                }

            }
            catch (System.Exception ex)
            {
                AppendText(ex.Message);
            }


        }

        /// <summary>
        /// index.html 파일을 수정합니다.
        /// </summary>
        private void ModifyHtmlFiles()
        {
            var filename = @"www/index.html";
            var lines = System.IO.File.ReadAllLines(filename, Encoding.UTF8).ToList();
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

                    System.IO.File.WriteAllLines(filename, lines);

                    AppendText(_rm.GetString("ModifyHtmlFiles"));
                }
            }

        }

        /// <summary>
        /// 프로젝트 파일을 복사합니다.
        /// </summary>
        private void CopyProjectFiles()
        {
            string srcPath = _list.settingGameFolder.Text.ToString();
            string dstPath = ".\\www";

            AppendText(_rm.GetString("CopyProjectFiles1"));

            if (System.IO.Directory.Exists(srcPath))
            {
                string robocopy = String.Format("robocopy \"{0}\" \"{1}\" /E /R:1 /W:1", srcPath, dstPath);

                HostData process = new HostData(robocopy, true, "",
                    _rm.GetString("CopyProjectFiles2"),
                    _rm.GetString("CopyProjectFiles3"));

                Append append = AppendText;

                process.Run(append);
            } else
            {
                AppendText(_rm.GetString("CopyProjectFiles4"));
            }

        }

        /// <summary>
        /// Add new plugin.
        /// </summary>
        private void AddPlugins()
        {

            foreach(string pluginName in _list.plugins.Items)
            {
                string command = String.Format("cordova plugin add {0}", pluginName);
                string success = String.Format(_rm.GetString("AddPlugins1"), pluginName);
                string fail = String.Format(_rm.GetString("AddPlugins2"), pluginName);
                HostData process = new HostData(command, true, "", success, fail);

                Append append = AppendText;

                process.Run(append);
            }

        }

        /// <summary>
        /// Build current project after adding plugins
        /// </summary>
        private void Flush()
        {
            if(System.IO.File.Exists("build.json"))
            {
                AppendText(_rm.GetString("Flush1"));
            }

            string mode = (_list.biuldMode.SelectedIndex == 0) ? "--release" : "--debug";
            string cmd = String.Format("cordova build android {0} --buildConfig=build.json", mode);

            // 코르도바는 빌드가 완료되면, 정상 출력 루트가 아닌 오류 쪽으로 빌드 완료 메시지를 보낸다.
            // 따라서, 오류 메시지를 정상인 것처럼 처리할 경우, 프로세스가 종료되지 않고 영원히 남게 된다.
            // TODO: 
            // 따라서 빌드 실패 메시지에 빌드 성공 메시지를 넣어야 한다.
            // 다만, 이 경우 빌드 실패 시에도 빌드가 성공했다고 메시지가 뜨게 된다.
            // 구분할 수 있는 방법은 아직까지 없다. 리치 텍스트 박스에서 fail 글자를 추출하지 않는한 불가능하다.
            HostData process = new HostData(cmd, true, "",
                _rm.GetString("Flush2"),
                _rm.GetString("Flush2"));

            Append append = AppendText;

            //process.Run(append);
            process.SafeRun(append);

        }

        /// <summary>
        /// Install cordova to user computer on Windows
        /// </summary>
        private void InstallCordova()
        {
            AppendText(_rm.GetString("INSTALLING_CORDOVA"));

            HostData process = new HostData("npm install -g cordova", true, "", _rm.GetString("SUCCESS_INSTALLED_CORDOVA"), _rm.GetString("FAIL_INSTALLED_CORDOVA"));
            Append append = AppendText;
            process.Run(append);

        }

        /// <summary>
        /// Reads the plugins.js file and Checks whether the plugin's status is to true in the project folder.
        /// </summary>
        /// <param name="projectPath">The project path that contains the www folder.</param>
        public void ReadProjectPluginsJson(string projectPath)
        {

            if(!System.IO.File.Exists(System.IO.Path.Combine(projectPath, "index.html"))) 
            {
                return;
            }

            string realPath = System.IO.Path.Combine(projectPath, "js", "plugins.js");
            var fakeLines = System.IO.File.ReadLines(realPath).Skip(4);
            var length = fakeLines.Count() - 1;
            var lines = fakeLines.Take(length);

            foreach (var json in lines)
            {
                string goodJson = Regex.Replace(json, @"}}\s*,\s*$", "}}");

                PluginConfigImpl impl = JsonHelper.ToClass<PluginConfigImpl>(goodJson);
                _plugins[impl.name] = impl.status;
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
        public void StartDownloadAndRun(Version targetVersion)
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36");
            client.DefaultRequestHeaders.Accept.TryParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            client.DefaultRequestHeaders.AcceptEncoding.TryParseAdd("gzip, deflate, br");
            client.DefaultRequestHeaders.AcceptLanguage.TryParseAdd("ko-KR,ko;q=0.9,en;q=0.8,ja;q=0.7");
            client.DefaultRequestHeaders.Host = "github.com";
            client.DefaultRequestHeaders.Referrer = new Uri("https://github.com/biud436/MV-App-Builder/releases");
            client.DefaultRequestHeaders.TransferEncodingChunked = true;
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.Timeout = TimeSpan.FromMinutes(5);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls12
                | SecurityProtocolType.Ssl3;

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var url = String.Format("https://github.com/biud436/MV-App-Builder/releases/download/v{0}/MVAppBuilder.exe", targetVersion);
            var bytes = client.GetByteArrayAsync(url).Result;

            AppendText(_rm.GetString("SUCCESSED_SETUP_FILE"));

            try
            {
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RPG Maker MV Cordova Builder");
                string targetPath = Path.Combine(folderPath, "MVAppBuilder.exe");

                if (Directory.Exists(folderPath))
                {
                    if (File.Exists(targetPath))
                    {
                        File.Delete(targetPath);
                    }

                    File.WriteAllBytes(targetPath, bytes);
                    System.Diagnostics.Process.Start(targetPath);

                }

            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void CheckVersion()
        {
            if(_version == null)
            {
                return;
            }

            using (WebClient wc = new WebClient())
            {
                string targetJson = wc.DownloadString("https://raw.githubusercontent.com/biud436/MV-App-Builder/master/version.json");
                var targetObject = JsonHelper.ToClass<Dictionary<string, string>>(targetJson);

                if (targetObject.ContainsKey("version"))
                {
                    var targetVersion = new Version(targetObject["version"]);
                    var result = _version.CompareTo(targetVersion);

                    if (result > 0)
                    {
                        // 알파 버전
                        AppendText(_rm.GetString("CHECK_VERSION_ALPHA"));
                    }
                    else if (result < 0)
                    {
                        // 구 버전을 사용하고 있습니다.
                        MessageBox.Show(_rm.GetString("CHECK_VERSION_OLD"), _mainForm.Text);

                        if(MessageBox.Show(_rm.GetString("CHECK_VERSION_OLD_ASK"), _mainForm.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            StartDownloadAndRun(targetVersion);
                        }
                    }
                    else
                    {
                        // 최신 버전을 사용하고 있습니다.
                        AppendText(_rm.GetString("CHECK_VERSION_LATEST"));
                    }

                }
            }
        }

        /// <summary>
        /// Checks the latest cordova version from cordova-android.git
        /// </summary>
        /// <returns></returns>
        public void CheckLatestCordovaVersion()
        {
            Version cordovaVersion = new Version("0.0.0");

            try
            {

                using (WebClient wc = new WebClient())
                {
                    string releaseNote = wc.DownloadString(new Uri("https://raw.githubusercontent.com/apache/cordova-cli/master/RELEASENOTES.md"));

                    var regex = new Regex("[\r\n]+", RegexOptions.IgnoreCase);
                    var lines = regex.Split(releaseNote);
                    var matchedData = "# Cordova-cli Release Notes";
                    var findIndex = 0;

                    // 타겟 인덱스 근처를 유연하게 읽도록 한다.
                    for (var i = 19; i < 28; i++)
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

                        // 버전 문자열을 뽑아낸다.
                        regex = new Regex(@"[#]{3}[ ]*(\d+\.\d+\.\d+)", RegexOptions.IgnoreCase);
                        Match m = regex.Match(lines[findIndex]);

                        if (m.Success)
                        {
                            cordovaVersion = new Version(m.Groups[1].Value);
                        }

                    }

                }

                // 한 줄만 처리하는 경량 명령 프롬프트를 생성한다.
                var tempCmdProcess = new HostData("", true, "", "");
                var localCordovaVersion = new Version("0.0.0");

                // 로컬에 설치된 코르도바의 버전을 확인한다. 
                tempCmdProcess.outputLine("cordova --version", (string output) => {

                    // 버전 문자열을 뽑아낸다.
                    Regex regex = new Regex(@"(\d+\.\d+\.\d+)[ ]+", RegexOptions.IgnoreCase);
                    Match m = regex.Match(output);

                    if (m.Success)
                    {
                        localCordovaVersion = new Version(m.Groups[1].Value);
                        var result = cordovaVersion.CompareTo(localCordovaVersion);

                        if (result > 0)
                        {
                            // 오래된 버전을 사용 중인 경우, 업데이트 처리를 한다.
                            AppendText(_rm.GetString("NOT_LATEST_CORDOV_VER"));
                            AppendText(String.Format(_rm.GetString("REQUEST_NPM_INSTALL"), cordovaVersion.ToString()));

                            DialogResult dialogResult = MessageBox.Show(_rm.GetString("REQUEST_NPM_INSTALL_ASK"), _mainForm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

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
                            // 이미 최신 버전을 쓰고 있는 경우
                            AppendText(_rm.GetString("LATEST_CORDOVA_READY"));
                        }

                    }
                });


            } catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            } finally
            {
                _cordovaVersion = cordovaVersion;
            }

        }

    }

}