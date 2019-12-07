using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Cordova_Builder
{
    public class DataManagerImpl
    {

        public enum DataFolderType : int
        {
            MY_DOCUMENTS,
            APP_DATA,
        }

        public string PackageFileName { get; set; }
        public string DataFolderName { get; set; }
        public DataFolderType Type { get; set; }

        public DataManagerImpl()
        {

        }

        /// <summary>
        /// 
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
            }

            string rootDirectory = Path.Combine(root, DataFolderName);

            if (!Directory.Exists(rootDirectory))
                Directory.CreateDirectory(rootDirectory);

            return rootDirectory;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ExportPackageJsonFile()
        {
            var rootDirectory = GetRootDirectory();
            var filePath = Path.Combine(rootDirectory, PackageFileName);

            var packageJson = new PackageJson()
            {
                RootDirectory = rootDirectory
            };

            File.WriteAllText(filePath, JsonConvert.SerializeObject(packageJson));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ImportPackageJsonFile()
        {
            var rootDirectory = GetRootDirectory();

            var filePath = Path.Combine(rootDirectory, PackageFileName);

            if (File.Exists(filePath))
            {
                var contents = File.ReadAllText(rootDirectory, Encoding.UTF8);
                var ret = JsonConvert.DeserializeObject<PackageJson>(contents);

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
