using Cordova.Models.FormData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cordova.Core
{
    internal interface BaseBuilder
    {
        void ExportBuildJson(string filename);

        void Make(Action<string> callback);

        void Build(Config config, Action successCallback);

        void CreateKeyStore();

        bool IsValidPlugin(string pluginName);
    }
}
