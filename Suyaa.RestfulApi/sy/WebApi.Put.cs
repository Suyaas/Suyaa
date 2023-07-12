using Suyaa.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Suyaa;
using System.IO;

namespace sy
{
    /* WebApi - Put */
    public static partial class WebApi
    {
        /// <summary>
        /// Post方式上传文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="paths"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<T> UploadAsync<T>(string url, List<string> paths, Action<HttpOption>? action = null)
            where T : notnull
        {
            using var option = new HttpOption();
            action?.Invoke(option);
            var content = await sy.Http.UploadAsync(url, paths, option);
            return JsonSerializer.Deserialize<T>(content).Fixed<T>();
        }

        /// <summary>
        /// Put方式获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="paths"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Put<T>(string url, List<string> paths, Action<HttpOption>? action = null)
            where T : notnull
            => UploadAsync<T>(url, paths, action).GetAwaiter().GetResult();

        /// <summary>
        /// Post方式上传文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<T> UploadAsync<T>(string url, string path, Action<HttpOption>? action = null)
            where T : notnull
            => await UploadAsync<T>(url, new List<string> { path }, action);

        /// <summary>
        /// Put方式获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Put<T>(string url, string path, Action<HttpOption>? action = null)
            where T : notnull
            => UploadAsync<T>(url, new List<string> { path }, action).GetAwaiter().GetResult();
    }
}
