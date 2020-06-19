using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;

namespace Cordova.Manage
{
    using Core;

    public sealed class DataMan
    {
        private static volatile DataMan instance;
        private static object syncRoot = new Object();

        public bool Use { get; set; }
        public string AudioFileFormat { get; set; }
        public bool RemainTree { get; set; }
        public bool IsValidCustomOutputPath { get; set; }
        public string OutputPath { get; set; }

        private Cordova cordovaMain;

        private DataMan()
        {
        }

        public static DataMan Instance
        {
            get
            {
                if (instance == null)
                {
                    // 하나의 쓰레드만 실행할 수 있도록 해주는 키워드
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DataMan()
                            {
                                AudioFileFormat = "ogg"
                            };
                            instance.Import();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// 데이터를 레지스트리로부터 불러옵니다.
        /// </summary>
        public void Import()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "settings.json");
            if(File.Exists(path))
            {
                string contents = File.ReadAllText(path, Encoding.UTF8);

                Dictionary<string, string> option = JsonConvert.DeserializeObject<Dictionary<string, string>>(contents);
                AudioFileFormat = option["audioFileFormat"];
                RemainTree = option["remainTree"] == "True";
                Use = option["excludeUnusedFiles"] == "True";

                if(!option.ContainsKey("IsValidCustomOutputPath"))
                {
                    IsValidCustomOutputPath = false;
                } else
                {
                    IsValidCustomOutputPath = option["IsValidCustomOutputPath"] == "True";
                }

                if (!option.ContainsKey("OutputPath"))
                {
                    OutputPath = DataManager.Instance.GetRootDirectory();
                }
                else
                {
                    OutputPath = option["OutputPath"];
                }

            } else
            {
                AudioFileFormat = "ogg";
                RemainTree = true;
                Use = false;
                IsValidCustomOutputPath = false;
            }
            
        }

        /// <summary>
        /// 데이터를 레지스트리에 저장합니다.
        /// </summary>
        public void Save()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "settings.json");

            Dictionary<string, string> option = new Dictionary<string, string>();
            option.Add("audioFileFormat", AudioFileFormat.ToString());
            option.Add("remainTree", RemainTree.ToString());
            option.Add("excludeUnusedFiles", Use.ToString());
            option.Add("IsValidCustomOutputPath", IsValidCustomOutputPath.ToString());

            // 출력 폴더를 저장합니다.
            option.Add("OutputPath", OutputPath.ToString());

            string contents = JsonConvert.SerializeObject(option);

            File.WriteAllText(path, contents);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cordova"></param>
        public void SetCordovaObject(Cordova cordova)
        {
            cordovaMain = cordova;
        }

    }
}
