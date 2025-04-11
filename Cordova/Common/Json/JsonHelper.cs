using Newtonsoft.Json;

using System.Collections.Generic;

namespace Cordova.Common.Json
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

            if (!EqualityComparer<T>.Default.Equals(data, default(T)))
            {
                ret = JsonConvert.SerializeObject(data, jsonSettings);
            }

            return isEmptyToNull ? (ret == "{}" ? "null" : ret) : ret;
        }

        /// <summary>
        /// JSON 문자열을 T로 변환합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="jsonSettings"></param>
        /// <returns></returns>
        public static T ToClass<T>(string data, JsonSerializerSettings jsonSettings = null)
        {
            var ret = default(T);

            if (!string.IsNullOrEmpty(data))
            {
                ret = jsonSettings == null ? JsonConvert.DeserializeObject<T>(data, jsonSettings)
                    : JsonConvert.DeserializeObject<T>(data, jsonSettings);
            }

            return ret;
        }
    }
}
