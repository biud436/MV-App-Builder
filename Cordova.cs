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

namespace Cordova_Builder
{
    public class Cordova
    {

        // 메인 폼
        private Form1 mainForm;

        // 현재 경로
        private string currentDirectory;

        // UI 컨트롤 목록
        private TextBoxList list;

        // 지역화를 위한 리소스 관리자
        private ResourceManager rm;

        // 플러그인 목록
        private Dictionary<string, bool> plugins = new Dictionary<string, bool>();

        // 버전
        private Version version;

        public Cordova()
        {
            currentDirectory = System.IO.Directory.GetCurrentDirectory();
            rm = new ResourceManager("Cordova_Builder.locale", Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// 메인 폼을 설정합니다.
        /// </summary>
        /// <param name="form"></param>
        public void SetMainForm(Form1 form)
        {
            this.mainForm = form;
        }

        /// <summary>
        /// 텍스트 박스 목록을 가져옵니다.
        /// </summary>
        /// <param name="list"></param>
        public void Bind(TextBoxList list)
        {
            this.list = list;
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
                keystore = list.keyPath.Text,
                storePassword = list.passWord.Text,
                alias = list.keyAlias.Text,
                password = list.passWord.Text,
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

        public void Make(Action<string> callback)
        {
            try
            {
                string mkdir = list.folderName.Text;

                if (!System.IO.Directory.Exists(mkdir))
                {
                    System.IO.Directory.CreateDirectory(mkdir);
                }

                string tempDir = System.IO.Directory.GetCurrentDirectory();
                string filename = "build.json";

                string targetPath = System.IO.Path.Combine(tempDir, mkdir);

                System.IO.Directory.SetCurrentDirectory(targetPath);

                callback(filename);

                System.IO.Directory.SetCurrentDirectory(tempDir);

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void Build(Action successCallback)
        {

            Thread worker = new Thread(new ThreadStart(() =>
            {
                // 폴더를 생성합니다
                bool isValid = Create();

                if (isValid)
                {
                    Make(filename =>
                    {
                        AddAndroidPlatform();
                        AddPlugins();
                        WriteConfig(); // config.xml 파일을 수정합니다
                        mainForm.CreateKeyStore();
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

        public void AppendText(string output)
        {
            this.mainForm.AppendText(output);
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

        private bool Create()
        {
            string folderName = list.folderName.Text;
            string packageName = list.packageName.Text;
            string gameName = list.gameName.Text;

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
                if (System.IO.Directory.Exists(folderName))
                {
                    var configPath = System.IO.Path.Combine(folderName, "config.xml");
                    var resFolderPath = System.IO.Path.Combine(folderName, "res");

                    if(System.IO.File.Exists(configPath) && System.IO.Directory.Exists(resFolderPath))
                    {
                        return true;
                    }

                    //clearFolder(folderName);

                }

            }
            catch (System.Exception ex)
            {
                AppendText(ex.Message);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("cordova create {0} {1} {2}", folderName, packageName, gameName);
            HostData process = new HostData(sb.ToString(), true, "",
                rm.GetString("Create1"),
                rm.GetString("Create2"));
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
                HostData process = new HostData("cordova platform add android", true, "", rm.GetString("AddAndroidPlatform1"), rm.GetString("AddAndroidPlatform2"));

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
            var process = new HostData("cordova requirements", true, "", rm.GetString("Requirements1"), rm.GetString("Requirements2"));

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
                        .SetPreference(xmlDoc, "Orientation", list.orientation.SelectedItem.ToString())
                        .SetPreference(xmlDoc, "FullScreen", list.fullscreen.SelectedItem.ToString())
                        .SetPreference(xmlDoc, "android-minSdkVersion", list.minSdkVersion.SelectedItem.ToString())
                        .SetPreference(xmlDoc, "android-targetSdkVersion", list.targetSdkVersion.SelectedItem.ToString())
                        .SetPreference(xmlDoc, "android-compileSdkVersion", list.compileSdkVersion.SelectedItem.ToString()) // it didn't exist property.
                        .SetPreference(xmlDoc, "BackgroundColor", "0xff000000") // black color
                        .SetPreference(xmlDoc, "ShowTitle", "false")
                        .SetIcon(xmlDoc);

                    xmlDoc.Save("config.xml");

                    AppendText(rm.GetString("WriteConfig"));
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

                    AppendText(rm.GetString("ModifyHtmlFiles"));
                }
            }

        }

        /// <summary>
        /// 프로젝트 파일을 복사합니다.
        /// </summary>
        private void CopyProjectFiles()
        {
            string srcPath = list.settingGameFolder.Text.ToString();
            string dstPath = ".\\www";

            AppendText(rm.GetString("CopyProjectFiles1"));

            if (System.IO.Directory.Exists(srcPath))
            {
                string robocopy = String.Format("robocopy \"{0}\" \"{1}\" /E /R:1 /W:1", srcPath, dstPath);

                HostData process = new HostData(robocopy, true, "",
                    rm.GetString("CopyProjectFiles2"),
                    rm.GetString("CopyProjectFiles3"));

                Append append = AppendText;

                process.Run(append);
            } else
            {
                AppendText(rm.GetString("CopyProjectFiles4"));
            }

        }

        /// <summary>
        /// Add new plugin.
        /// </summary>
        private void AddPlugins()
        {

            foreach(string pluginName in list.plugins.Items)
            {
                string command = String.Format("cordova plugin add {0}", pluginName);
                string success = String.Format(rm.GetString("AddPlugins1"), pluginName);
                string fail = String.Format(rm.GetString("AddPlugins2"), pluginName);
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
                AppendText(rm.GetString("Flush1"));
            }

            string mode = (list.biuldMode.SelectedIndex == 0) ? "--release" : "--debug";
            string cmd = String.Format("cordova build android {0} --buildConfig=build.json", mode);

            // 코르도바는 빌드가 완료되면, 정상 출력 루트가 아닌 오류 쪽으로 빌드 완료 메시지를 보낸다.
            // 따라서, 오류 메시지를 정상인 것처럼 처리할 경우, 프로세스가 종료되지 않고 영원히 남게 된다.
            // TODO: 
            // 따라서 빌드 실패 메시지에 빌드 성공 메시지를 넣어야 한다.
            // 다만, 이 경우 빌드 실패 시에도 빌드가 성공했다고 메시지가 뜨게 된다.
            // 구분할 수 있는 방법은 아직까지 없다. 리치 텍스트 박스에서 fail 글자를 추출하지 않는한 불가능하다.
            HostData process = new HostData(cmd, true, "",
                rm.GetString("Flush2"),
                rm.GetString("Flush2"));

            Append append = AppendText;

            process.Run(append);

        }

        /// <summary>
        /// Install cordova to user computer on Windows
        /// </summary>
        private void Install()
        {
            AppendText("Installing cordova...");

            HostData process = new HostData("npm install -g cordova", true, "", "echo ...", "echo ...");
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
                plugins[impl.name] = impl.status;
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

            if (plugins.ContainsKey(pluginName))
            {
                isValid = plugins[pluginName];
            }

            return isValid;
        }

        public void InitWithVersion()
        {
            // 현재 버전
            version = new Version("0.1.17");

            using (WebClient wc = new WebClient())
            {
                string targetJson = wc.DownloadString("https://raw.githubusercontent.com/biud436/MV-App-Builder/master/version.json");
                var targetObject = JsonHelper.ToClass<Dictionary<string, string>>(targetJson);
                    
                if(targetObject.ContainsKey("version"))
                {
                    var targetVersion = new Version(targetObject["version"]);
                    var result = version.CompareTo(targetVersion);

                    if(result > 0)
                    {
                        AppendText("현재 버전은 최신 버전입니다.");
                    } else if(result < 0)
                    {
                        AppendText("새로운 버전이 있습니다.");

                        //TODO : 업데이터 프로그램이 따로 있어야 한다.
                        // 빌더 프로그램을 강제 종료시킨 후, 인스톨러를 따로 실행해야 한다.

                        //var worker = new BackgroundWorker();
                        //worker.DoWork += (object sender, DoWorkEventArgs e) =>
                        //{
                        //    wc.DownloadFile(new Uri("https://github-production-release-asset-2e65be.s3.amazonaws.com/218708137/09b1cd80-0317-11ea-8d0e-1059dbd4a116?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAIWNJYAX4CSVEH53A%2F20191110%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20191110T094646Z&X-Amz-Expires=300&X-Amz-Signature=ec70b0adad9787d42a78feee5daab4751bc2a2f47c3e73ccce89ec9aebb0e91d&X-Amz-SignedHeaders=host&actor_id=13586185&response-content-disposition=attachment%3B%20filename%3DMVAppBuilder.exe&response-content-type=application%2Foctet-stream"), "MVAppBuilder.exe");
                        //};
                        //worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
                        //{
                        //    MessageBox.Show("새로운 버전 다운로드가 완료되었습니다");
                        //};
                        //worker.RunWorkerAsync();

                    } else
                    {
                        AppendText("최신 버전을 사용 중입니다.");
                    }

                }

            }

        }
    }

}