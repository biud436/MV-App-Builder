using System;
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
        public string _name;
        public bool _isValid;
        public string _dir;
        public string _success;
        public string _fail;

        enum ExitCode : int
        {
            Success = 0,
            InvalidLogin = 1,
            InvalidFilename = 2,
            UnknownError = 10
        }

        public HostData(string name, bool isValid = true, string dir = "", string success = "", string fail = "")
        {
            this._name = name;
            this._isValid = isValid;
            this._dir = dir;
            this._success = success;
            this._fail = fail;
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
            info.Arguments = "/C " + _name;

            if (!String.IsNullOrEmpty(_dir))
            {
                info.WorkingDirectory = _dir;
            }

            process.StartInfo = info;
            process.Start();

            bool isNull = false;

            var reader = process.StandardOutput;
            while (!reader.EndOfStream)
            {
                var nextLine = reader.ReadLine();

                if (nextLine == null)
                {
                    break;
                }

                Task.Delay(10);
                AppendText(nextLine);

            }

            isNull = String.IsNullOrEmpty(process.StandardError.ReadToEnd() ?? "");

            process.WaitForExit();
            process.Close();

            return (isNull != false);
        }

        public void SafeOutput(Append AppendText, EventHandler eventHandler)
        {
            
            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo();

            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;

            info.FileName = @"cmd.exe";
            info.CreateNoWindow = true;
            info.Arguments = "/C " + _name;

            if (!String.IsNullOrEmpty(_dir))
            {
                info.WorkingDirectory = _dir;
            }

            process.StartInfo = info;

            bool isNull = false;

            var reader = process.StandardOutput;

            // 이벤트 방식으로 데이터 수신
            process.OutputDataReceived += new DataReceivedEventHandler((sender, e) => {
                if(!String.IsNullOrEmpty(e.Data))
                {
                    AppendText(e.Data);
                }
            });

            // 이벤트 방식으로 오류 수신
            process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if(!String.IsNullOrEmpty(e.Data))
                {
                    AppendText(e.Data);
                }
            });

            process.Exited += eventHandler;

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();

            process.Close();
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

            if (isSuccessMessage)
            {
                outputLine(_success, (msg) => { Append(msg); });
                return true;
            } else
            {
                outputLine(_fail, (msg) => { Append(msg); });
                return false;
            }

        }

        public void SafeRun(Append Append)
        {
            SafeOutput(Append, new EventHandler((sender, e) =>
            {
                Process process = (Process)sender;

                if(process.ExitCode == (int)ExitCode.Success)
                {
                    outputLine(_success, (msg) => { Append(msg); });
                }
                else
                {
                    outputLine(_fail, (msg) => { Append(msg); });
                }
            }));
        }

    }
}
