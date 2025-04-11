using Cordova.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cordova.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class BuildConfig
    {
        public AndroidBuildConfig android;
    }
}
