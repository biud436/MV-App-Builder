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

namespace Cordova_Builder
{

    public class Cordova
    {

        private Form1 mainForm;
        private string currentDirectory;
        private List<string> buildConfig = new List<string>();
        private TextBoxList list;
        private ResourceManager rm;

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
        /// 빌드 구성 문자열에 새로운 cordova command를 추가합니다.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public Cordova Add(string config)
        {
            buildConfig.Add(config);

            return this;
        }

        /// <summary>
        /// 텍스트 박스 목록을 가져옵니다.
        /// </summary>
        /// <param name="list"></param>
        public void Bind(TextBoxList list)
        {
            this.list = list;
        }

        public void ExportBuildJson(string filename)
        {
            StringBuilder output = new StringBuilder();

            string filePath = String.Format("          \"keystore\": \"{0}\",\r\n", list.keyPath.Text);
            string storePassword = String.Format("          \"storePassword\": \"{0}\",\r\n", list.passWord.Text);
            string alias = String.Format("          \"alias\": \"{0}\",\r\n", list.keyAlias.Text);
            string password = String.Format("          \"password\" : \"{0}\",\r\n", list.passWord.Text);

            output.Append("{ \r\n");
            output.Append("  \"android\": {\r\n");
            output.Append("      \"debug\": {\r\n");
            output.Append(filePath);
            output.Append(storePassword);
            output.Append(alias);
            output.Append(password);
            output.Append("          \"keystoreType\": \"\"\r\n");
            output.Append("      },\r\n");
            output.Append("      \"release\": {\r\n");
            output.Append(filePath);
            output.Append(storePassword);
            output.Append(alias);
            output.Append(password);
            output.Append("          \"keystoreType\": \"\"\r\n");
            output.Append("      }\r\n");
            output.Append("  }\r\n");
            output.Append("}\r\n");

            System.IO.File.WriteAllText(filename, output.ToString());

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
                        AddAndroidPlatform(); // 안드로이드 플랫폼 추가합니다
                        AddPlugins(); // 플러그인을 추가합니다.
                        WriteConfig(); // config.xml 파일을 수정합니다
                        mainForm.CreateKeyStore(); // 키스토어 파일을 생성합니다
                        CopyProjectFiles(); // 파일을 복사합니다.
                        ModifyHtmlFiles(); // HTML 파일에 cordova 바인드 용 스크립트 문을 추가합니다.
                        ExportBuildJson(filename); // build.config 파일을 배포합니다.
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
                //      - 현재 사용 중인 방법
                if (System.IO.Directory.Exists(folderName))
                {
                    clearFolder(folderName);
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
            string cmd = String.Format("cordova build android {0} --buildConfig=build.json 2>&1", mode);

            HostData process = new HostData(cmd, true, "",
                rm.GetString("Flush2"),
                rm.GetString("Flush3"));

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

    }

}