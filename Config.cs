using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cordova_Builder
{
    namespace FormData {

        public class Config
        {

            public string folderName { get; set; }
            public string keyPath { get; set; }
            public string gameName { get; set; }
            public string keyAlias { get; set; }
            public string passWord { get; set; }
            public string packageName { get; set; }
            public string keyOU { get; set; }
            public string keyO { get; set; }
            public string keyL { get; set; }
            public string keyS { get; set; }
            public string keyC { get; set; }
            public string orientation { get; set; }
            public string fullscreen { get; set; }
            public string minSdkVersion { get; set; }
            public string targetSdkVersion { get; set; }
            public string settingGameFolder { get; set; }
            public int buildMode { get; set; }
            public string compileSdkVersion { get; set; }
            public List<String> plugins { get; set; }

            public Config()
            {

            }
        }
    }
}
