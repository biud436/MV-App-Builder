using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cordova.Common.Localization
{
    public class ResourceToken
    {
        private string _token;

        public ResourceToken(string token)
        {
            _token = token;
        }

        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}
