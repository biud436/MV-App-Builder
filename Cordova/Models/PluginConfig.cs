using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Cordova.Models
{    
    public class PluginConfig
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("status")]
        public bool status { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("parameters")]
        [JsonExtensionData]
        public IDictionary<string, JToken> parameters { get; set; }
    }

}
