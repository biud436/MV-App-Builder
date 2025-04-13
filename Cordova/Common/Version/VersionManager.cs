using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cordova.Common.Version
{
    using Cordova.Core;

    public class VersionManager
    {
        private readonly Cordova _cordova;

        public VersionManager(Cordova cordova)
        {
            _cordova = cordova;
        }
    }
}
