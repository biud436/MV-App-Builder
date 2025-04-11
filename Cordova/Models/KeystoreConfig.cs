using Newtonsoft.Json;

namespace Cordova.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class KeystoreConfig
    {

        public KeystoreConfig()
        {

        }

        public string keystore { get; set; }
        public string storePassword { get; set; }
        public string alias { get; set; }
        public string password { get; set; }
        public string keystoreType { get; set; }
        public string packageType { get; set; }
    }

}
