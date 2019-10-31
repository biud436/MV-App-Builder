﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;



namespace Cordova_Builder
{
    public delegate void Append(string output);

    public class HostData
    {
        public string name;
        public bool isValid;
        public string dir;
        public string success;
        public string fail;

        public HostData(string name, bool isValid = true, string dir = "", string success = "", string fail = "")
        {
            this.name = name;
            this.isValid = isValid;
            this.dir = dir;
            this.success = success;
            this.fail = fail;
        }

        public bool Output(Append AppendText)
        {
            
            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo();

            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;

            info.FileName = @"cmd.exe";
            info.CreateNoWindow = true;
            info.Arguments = "/C " + name;

            if (!String.IsNullOrEmpty(dir))
            {
                info.WorkingDirectory = dir;
            }

            process.StartInfo = info;
            process.Start();

            bool isNull = false;

            //ret.Add(process.StandardOutput.ReadToEnd());

            var reader = process.StandardOutput;
            while (!reader.EndOfStream)
            {
                var nextLine = reader.ReadLine();

                if (nextLine == null)
                {
                    break;
                }

                AppendText(nextLine);            

            }

            isNull = String.IsNullOrEmpty(process.StandardError.ReadToEnd() ?? "");

            process.WaitForExit();
            process.Close();

            return (isNull != false);
        }

        public void outputLine(string command, Action<string> callback)
        {
            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo()
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                FileName = @"cmd.exe",
                CreateNoWindow = true,
                Arguments = "/C " + command
            };

            process.StartInfo = info;
            process.Start();

            callback(process.StandardOutput.ReadToEnd());

            process.WaitForExit();
            process.Close();

        }

        public bool Run(Append Append)
        {

            bool isSuccessMessage = Output(Append);

            //bool isSuccessMessage = true;

            if (isSuccessMessage)
            {
                outputLine(success, (msg) => { Append(msg); });
                return true;
            } else
            {
                outputLine(fail, (msg) => { Append(msg); });
                return false;
            }

            //var ret = Output();
            //string temp = "";

            //// 실패 시 출력할 메시지
            //if (!String.IsNullOrEmpty(ret[1]))
            //{
            //    temp = name;

            //    name = fail;

            //    Append(Output()[0]);

            //    name = temp;

            //    return false;
            //}

            //if (isValid)
            //{
            //    Append(ret[0]);
            //}

            //// 성공 시 출력할 메시지
            //temp = name;
            //name = success;

            //string routput = Output()[0];

            //Append(routput);

            //name = temp;

            //return true;

        }

    }
}
