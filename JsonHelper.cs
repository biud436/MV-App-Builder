using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Cordova_Builder
{
    /// <summary>
    /// https://www.codeproject.com/Articles/1201466/Working-with-JSON-in-Csharp-VB
    /// </summary>
    public static class JsonHelper
    {
        public static string FromClass<T>(T data, bool isEmptyToNull = false, 
            JsonSerializerSettings jsonSettings = null)
        {
            string ret = string.Empty;

            if(!EqualityComparer<T>.Default.Equals(data, default(T)))
            {
                ret = JsonConvert.SerializeObject(data, jsonSettings);
            }

            return isEmptyToNull ? (ret == "{}" ? "null" : ret) : ret;
        }

        public static T ToClass<T>(string data, JsonSerializerSettings jsonSettings = null)
        {
            var ret = default(T);

            if(!string.IsNullOrEmpty(data))
            {
                ret = jsonSettings == null ? JsonConvert.DeserializeObject<T>(data, jsonSettings)
                    : JsonConvert.DeserializeObject<T>(data, jsonSettings);
            }

            return ret;
        }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class ConfigData
    {

        public ConfigData()
        {

        }

        public string keystore { get; set; }
        public string storePassword { get; set; }
        public string alias { get; set; }
        public string password { get; set; }
        public string keystoreType { get; set; }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class AndroidBuildConfig
    {
        public ConfigData debug;
        public ConfigData release;
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class BuildConfig
    {
        public AndroidBuildConfig android;
    }
    
    public class PluginConfigImpl
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
