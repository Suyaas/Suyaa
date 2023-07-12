using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Net.Http;
using Suyaa;

namespace sy
{
    /// <summary>
    /// Http
    /// </summary>
    public static partial class Http
    {
        /// <summary>
        /// 获取Get方式的应答结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> GetResponseAsync(string url, Action<HttpOption>? action = null)
        {
            //using (var client = GetClient())
            //{
            var client = GetClient();
            if (action != null)
            {
                using HttpOption option = new HttpOption();
                action(option);
                client.SetHeaders(option.Headers);
            }
            return await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            //}
        }

        /// <summary>
        /// 以Get方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, Action<HttpOption>? action = null)
        {
            // 应答器
            using (HttpResponseMessage response = await GetResponseAsync(url, action))
            {
                // 判断状态并抛出异常
                response.EnsureSuccessStatusCode();
                // 返回数据结果
                return await response.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// 以Get方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string Get(string url, Action<HttpOption>? action = null)
            => GetAsync(url, action).GetAwaiter().GetResult();
    }
}
