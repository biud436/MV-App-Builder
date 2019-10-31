using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cordova_Builder
{
    public class Requirements
    {

        private Form1 mainForm;

        public HostData[] hostList = new HostData[]
        {
            new HostData("where java.exe", false, "", @"java.exe -version 2>&1", "echo -- 자바를 찾지 못했습니다"),
            new HostData("where keytool.exe", false, "", "echo -- keytool을 찾았습니다.", "echo -- keytool을 찾지 못했습니다"),
            new HostData("where cordova", false, "", "cordova -v", "echo -- cordova를 찾지 못했습니다")
        };

        Requirements()
        {

        }

        public void SetMainForm(Form1 form)
        {
            this.mainForm = form;
        }


    }
}
