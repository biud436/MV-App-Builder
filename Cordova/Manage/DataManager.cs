using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Cordova.Manage
{
    public class DataManager
    {

        private static readonly Lazy<DataManager> instance = new Lazy<DataManager>(() => new DataManager()
        {
            PackageFileName = "package.json",
            DataFolderName = "RPG Maker MV Cordova Builder",
            Type = DataManager.DataFolderType.MY_DOCUMENTS,
        });

        public static DataManager Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public enum DataFolderType : int
        {
            MY_DOCUMENTS,
            APP_DATA,
            CUSTOM,
        }

        public string PackageFileName { get; set; }
        public string DataFolderName { get; set; }
        public DataFolderType Type { get; set; }
        public string AndroidSDKPath { get; set; }
        public string JDKPath { get; set; }

        private DataManager()
        {

        }

        /// <summary>
        /// 루트 디렉토리를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public string GetRootDirectory()
        {
            var root = "";

            switch (Type)
            {
                default:
                case DataFolderType.MY_DOCUMENTS:
                    root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    break;
                case DataFolderType.APP_DATA:
                    root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    break;
                case DataFolderType.CUSTOM:
                    root = DataMan.Instance.OutputPath;
                    break;
            }

            string rootDirectory = Path.Combine(root, DataFolderName);

            if (!Directory.Exists(rootDirectory))
                Directory.CreateDirectory(rootDirectory);

            return rootDirectory;
        }

        /// <summary>
        /// 패키지 파일을 내보냅니다.
        /// </summary>
        public void ExportPackageJsonFile()
        {
            var rootDirectory = GetRootDirectory();
            var filePath = Path.Combine(rootDirectory, PackageFileName);

            var packageJson = new Data.PackageJson()
            {
                RootDirectory = rootDirectory
            };

            File.WriteAllText(filePath, JsonConvert.SerializeObject(packageJson));
        }

        /// <summary>
        /// 패키지 파일을 로드합니다.
        /// </summary>
        /// <returns></returns>
        public string ImportPackageJsonFile()
        {
            var rootDirectory = GetRootDirectory();

            var filePath = Path.Combine(rootDirectory, PackageFileName);

            if (File.Exists(filePath))
            {
                var contents = File.ReadAllText(rootDirectory, Encoding.UTF8);
                var ret = JsonConvert.DeserializeObject<Data.PackageJson>(contents);

                return ret.RootDirectory;
            }
            else
            {
                return rootDirectory;
            }
        }


        /// <summary>
        /// 경로에 영어가 있으면 true, 아니면 false
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool IsValidPath(string filePath)
        {
            // 문자가 비어있으면 false
            if (String.IsNullOrEmpty(filePath))
                return false;

            // 사용할 수 없는 문자가 있으면 false
            if (Path.GetInvalidPathChars().Any(c => filePath.Contains(c)))
                return false;

            // 한글, 한자, 카타카나 등이 있으면 false
            return filePath.Any(c => c <= 127);
        }

    }
}
