using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cordova_Builder
{
    public class Redistribution
    {

        /// <summary>
        /// LOW : 데이터 폴더 파일에서 리소스 파일이 검출될 경우, 모두 제외한다.
        /// NORMAL : 
        /// HIGH : 
        /// </summary>
        enum Reliability : int
        {
            LOW,
            NORMAL,
            HIGH,
        };

        private Reliability _reliability;

        public Redistribution()
        {
            _reliability = Reliability.NORMAL;
        }

        public void CreateDatabase()
        {

        }

    }

    //class DataInfo
    //{
    //    public ObjectID { get; set; }
    //    public string Name { get; set; }
    //    public bool Used { get; set; }
    //}
}
