using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Cordova_Builder
{
    public class Cordova
    {

        private Form1 mainForm;
        private string currentDirectory;
        private List<string> buildConfig = new List<string>();
        private TextBoxList list;

        public Cordova()
        {
            currentDirectory = System.IO.Directory.GetCurrentDirectory();
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

        public void ExportBuildJson()
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

            Make(filename => {
                System.IO.File.WriteAllText(filename, output.ToString());
            });

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

        public void Build()
        {
            bool isValid = Create();

            if(isValid)
            {
                Make(filename =>
                {
                    AddAndroidPlatform();
                    WriteConfig();
                    mainForm.CreateKeyStore();
                    ModifyHtmlFiles();
                });
            }
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
                if(System.IO.Directory.Exists(folderName))
                {
                    clearFolder(folderName);
                }
                
            } catch(System.Exception ex)
            {
                AppendText(ex.Message);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("cordova create {0} {1} {2}", folderName, packageName, gameName);

            HostData process = new HostData(sb.ToString(), true, "", "echo 코르도바 프로젝트를 생성하였습니다", "echo 코르도바 프로젝트 생성에 실패하였습니다");
            Append append = AppendText;

            return process.Run(append);
        }

        private void AddAndroidPlatform()
        {
            HostData process = new HostData("cordova platform add android", true, "", "echo 안드로이드 프로젝트가 추가되었습니다", "echo 안드로이드 프로젝트 추가에 실패하였습니다");

            Append append = AppendText;

            process.Run(append);

        }

        // cordova plugin add cordova-plugin-insomnia
        // cordova build android --release --buildConfig=build.json

        private bool Requirements()
        {
            var process = new HostData("cordova requirements", true, "", "echo ...", "echo ...");

            Append append = AppendText;

            return process.Run(append);
        }

        private Cordova CreateElement(XmlDocument xmlDoc, string name, string value)
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

        private void WriteConfig()
        {
            try
            {

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("config.xml");

                this
                    .CreateElement(xmlDoc, "Orientation", list.orientation.SelectedItem.ToString())
                    .CreateElement(xmlDoc, "Fullscreen", list.fullscreen.SelectedItem.ToString())
                    .CreateElement(xmlDoc, "AllowInlineMediaPlayback", "true")
                    .CreateElement(xmlDoc, "android-minSdkVersion", list.minSdkVersion.SelectedItem.ToString())
                    .CreateElement(xmlDoc, "android-targetSdkVersion", list.targetSdkVersion.SelectedItem.ToString());

                xmlDoc.Save("config.xml");

                AppendText("config.xml 파일을 수정하였습니다");

            } catch (System.Exception ex)
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
                if(line.Contains("<script "))
                {
                    matchLine = line;
                    isValid = true;
                    break;
                }
            }

            if(isValid)
            {
                var index = lines.IndexOf(matchLine);
                if(index != -1)
                {
                    lines.Insert(index, "        <script type=\"text/javascript\" src=\"cordova.js\"></script>");
                    System.IO.File.WriteAllLines(filename, lines);

                    AppendText("index.html 파일을 수정하였습니다.");
                }
            }

        }

    }
}
