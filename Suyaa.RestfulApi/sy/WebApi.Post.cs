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
    /* WebApi - Post */
    public static partial class WebApi
    {
        /// <summary>
        /// Post方式获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<T> PostAsync<T>(string url, object data, Action<HttpOption>? action = null)
            where T : notnull
        {
            using var option = new HttpOption();
            option.Headers.ContentType = CONTENT_TYPE_JSON;
            action?.Invoke(option);
            var content = await sy.Http.PostAsync(url, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)), option);
            return JsonSerializer.Deserialize<T>(content).Fixed<T>();
        }

        /// <summary>
        /// Post方式获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Post<T>(string url, object data, Action<HttpOption>? action = null)
            where T : notnull
            => PostAsync<T>(url, data, action).GetAwaiter().GetResult();
    }
}
