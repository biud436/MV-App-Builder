using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cordova.Common.Versions
{
    using Cordova.Common.Localization;
    using Cordova.Core;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class VersionManager
    {
        private readonly Cordova _cordova;

        public VersionManager(Cordova cordova)
        {
            _cordova = cordova;
        }

        public void CheckLatestCordovaVersion()
        {
            Version cordovaVersion = new Version("0.0.0");

            Action<string> AppendText = (string text) =>
            {
                if (_cordova != null)
                {
                    _cordova.AppendText(text);
                }
            };
            ResourceManagerAdapter _rm = _cordova.GetResourceManager();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            try
            {
                // npm을 이용한 버전 체크
                using (var tempCmdProcess = new HostProcessRunner("", true, "", ""))
                {
                    tempCmdProcess.outputLine("npm show cordova version", (string output) =>
                    {
                        cordovaVersion = new Version(output);
                    });
                }

                // Create the lightweight command process and check the version of installed Cordova in the local system.
                using (var tempCmdProcess = new HostProcessRunner("", true, "", ""))
                {
                    // Create the lightweight command process
                    var localCordovaVersion = new Version("0.0.0");

                    // check the version of installed Cordova in the local system.
                    tempCmdProcess.outputLine("cordova --version", (string output) =>
                    {
                        Regex regex = new Regex(@"(\d+\.\d+\.\d+)", RegexOptions.IgnoreCase);
                        Match m = regex.Match(output);

                        if (m.Success)
                        {
                            localCordovaVersion = new Version(m.Groups[1].Value);
                            var result = cordovaVersion.CompareTo(localCordovaVersion);

                            if (result > 0)
                            {
                                AppendText(_rm.GetString(ResourceTokens.NOT_LATEST_CORDOV_VER));
                                AppendText(String.Format(_rm.GetString(ResourceTokens.REQUEST_NPM_INSTALL), cordovaVersion.ToString()));

                                DialogResult dialogResult = MessageBox.Show
                                    (
                                        _rm.GetString(ResourceTokens.REQUEST_NPM_INSTALL_ASK), 
                                        _cordova.GetMainFormText(), 
                                        MessageBoxButtons.YesNo, 
                                        MessageBoxIcon.Information
                                    ); 

                                if (dialogResult == DialogResult.Yes)
                                {
                                    _cordova.InstallCordova();
                                }

                            }
                            else if (result < 0)
                            {
                                // Why you use dev version? I don't understand.
                            }
                            else
                            {
                                sw.Stop();
                                AppendText(_rm.GetString(ResourceTokens.LATEST_CORDOVA_READY) + "(" + sw.Elapsed.ToString() + ")");
                            }

                        }
                        else
                        {
                            sw.Stop();
                            AppendText($"코르도바를 설치해주세요. 정규표현식에 매칭되는 버전 텍스트가 없습니다. 출력 텍스트 : {output}");
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                _cordova.SetCordovaVersion(cordovaVersion);
            }
        }
    }
}
