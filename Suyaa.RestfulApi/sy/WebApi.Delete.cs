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
    /* WebApi - Get */
    public static partial class WebApi
    {
        /// <summary>
        /// Delete方式获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<T> DeleteAsync<T>(string url, Action<HttpOption>? action = null)
            where T : class
        {
            using var option = new HttpOption();
            option.Headers.ContentType = CONTENT_TYPE_JSON;
            action?.Invoke(option);
            var content = await sy.Http.DeleteAsync(url, option);
            return JsonSerializer.Deserialize<T>(content).Fixed();
        }

        /// <summary>
        /// Delete方式获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Delete<T>(string url, Action<HttpOption>? action = null)
            where T : class
        {
            using var option = new HttpOption();
            option.Headers.ContentType = CONTENT_TYPE_JSON;
            action?.Invoke(option);
            var content = sy.Http.Delete(url, option);
            return JsonSerializer.Deserialize<T>(content).Fixed();
        }
    }
}
