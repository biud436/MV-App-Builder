using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;

namespace Cordova_Builder
{
    public sealed class DataMan
    {
        private static volatile DataMan instance;
        private static object syncRoot = new Object();

        public bool Use { get; set; }
        public string AudioFileFormat { get; set; }
        public bool RemainTree { get; set; }

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
                DataMan.Instance.AudioFileFormat = option["audioFileFormat"];
                DataMan.Instance.RemainTree = option["remainTree"] == "True";
                DataMan.Instance.Use = option["excludeUnusedFiles"] == "True";
            } else
            {
                DataMan.Instance.AudioFileFormat = "ogg";
                DataMan.Instance.RemainTree = true;
                DataMan.Instance.Use = false;
            }
            
        }

        /// <summary>
        /// 데이터를 레지스트리에 저장합니다.
        /// </summary>
        public void Save()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "settings.json");

            Dictionary<string, string> option = new Dictionary<string, string>();
            option.Add("audioFileFormat", DataMan.Instance.AudioFileFormat.ToString());
            option.Add("remainTree", DataMan.Instance.RemainTree.ToString());
            option.Add("excludeUnusedFiles", DataMan.Instance.Use.ToString());

            string contents = JsonConvert.SerializeObject(option);

            File.WriteAllText(path, contents);
        }

    }
}
