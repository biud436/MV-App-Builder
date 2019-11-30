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

    public class HostData : IDisposable
    {
        public string _name;
        public bool _isValid;
        public string _dir;
        public string _success;
        public string _fail;

        public bool disposed = false;

        enum ExitCode : int
        {
            Success = 0,
            InvalidLogin = 1,
            InvalidFilename = 2,
            UnknownError = 10
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isValid"></param>
        /// <param name="dir"></param>
        /// <param name="success"></param>
        /// <param name="fail"></param>
        public HostData(string name, bool isValid = true, string dir = "", string success = "", string fail = "")
        {
            this._name = name;
            this._isValid = isValid;
            this._dir = dir;
            this._success = success;
            this._fail = fail;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if(disposing)
            {
                // Free any other managed objects here.

            }

            // Free any unmanaged objects here.
            disposed = true;

        }

        ~HostData()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AppendText"></param>
        /// <returns></returns>
        public bool Output(Append AppendText)
        {
            var isNull = false;

            using (var process = new Process())
            {
                ProcessStartInfo info = new ProcessStartInfo()
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    FileName = @"cmd.exe",
                    CreateNoWindow = true,
                    Arguments = "/C " + _name
                };

                if (!String.IsNullOrEmpty(_dir))
                {
                    info.WorkingDirectory = _dir;
                }

                process.StartInfo = info;
                process.Start();

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
            }

            return (isNull != false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="callback"></param>
        public void outputLine(string command, Action<string> callback)
        {
            using (var process = new Process())
            {
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

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void EvalScript(Action<System.IO.StreamWriter, System.IO.StreamReader, System.IO.StreamReader> action)
        {
            using (var process = new Process())
            {
                ProcessStartInfo info = new ProcessStartInfo()
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    FileName = @"node.exe",
                    CreateNoWindow = true,
                };

                action(process.StandardInput, process.StandardOutput, process.StandardError);

                process.StartInfo = info;

                if (process.Start())
                {
                    process.WaitForExit();
                    process.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Append"></param>
        /// <returns></returns>
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

    }
}
