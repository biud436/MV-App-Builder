﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cordova.Models
{
    public class PackageJson
    {
        [JsonProperty("RootDirectory")]
        public string RootDirectory;
    }
}
