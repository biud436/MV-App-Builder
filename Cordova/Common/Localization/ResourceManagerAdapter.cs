using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Cordova.Common.Localization
{
    interface IResourceManagerAdapter
    {
        /// <summary>
        /// 리소스 문자열을 가져옵니다.
        /// </summary>
        string GetString(ResourceToken token);
    }

    /// <summary>
    /// ResourceManagerAdapter는 리소스 문자열을 가져오는 데 사용되는 어댑터 클래스입니다.
    /// </summary>
    public class ResourceManagerAdapter: IResourceManagerAdapter
    {
        private ResourceManager _resourceManager;

        public ResourceManagerAdapter()
        {
            _resourceManager = new ResourceManager("Cordova_Builder.locale", Assembly.GetExecutingAssembly());
        }

        public string GetString(ResourceToken token)
        {
            return _resourceManager.GetString(token.Token);
        }
    }
}
