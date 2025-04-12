using Cordova.Common.Localization;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cordova.Common.Localization
{
    public static class ResourceManagerFactory
    {
        public static ResourceManagerAdapter Create()
        {
            return new ResourceManagerAdapter();
        }
    }
}
