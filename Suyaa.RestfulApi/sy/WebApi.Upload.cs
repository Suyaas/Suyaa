using Suyaa.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Suyaa;

namespace sy
{
    /* WebApi - Put */
    public static partial class WebApi
    {
        /// <summary>
        /// Put方式获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<T> PutAsync<T>(string url, object data, Action<HttpOption>? action = null)
            where T : notnull
        {
            using var option = new HttpOption();
            option.Headers.ContentType = CONTENT_TYPE_JSON;
            action?.Invoke(option);
            var content = await sy.Http.PutAsync(url, JsonSerializer.Serialize(data), option);
            return JsonSerializer.Deserialize<T>(content).Fixed<T>();
        }

        /// <summary>
        /// Put方式获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Put<T>(string url, object data, Action<HttpOption>? action = null)
            where T : notnull
            => PutAsync<T>(url, data, action).GetAwaiter().GetResult();
    }
}
