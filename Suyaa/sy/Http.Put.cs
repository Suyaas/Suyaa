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
        /// 获取Put方式的应答结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PutResponseAsync(string url, string data, HttpOption option)
        {
            using var client = GetClient();
            // 建立传输内容
            HttpContent content = new StringContent(data);
            // 设置头
            content.SetHeaders(option.Headers);
            return await client.PutAsync(url, content);
        }

        /// <summary>
        /// 以Put方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> PutAsync(string url, string data, Action<HttpOption>? action = null)
        {
            using HttpOption option = new HttpOption();
            action?.Invoke(option);
            // 应答器
            using HttpResponseMessage response = await PutResponseAsync(url, data, option);
            // 触发应答事件
            option.RaiseResponseEvent(response);
            // 判断状态并抛出异常
            response.EnsureSuccessStatusCode();
            // 返回数据结果
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 以Put方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string Put(string url, string data, Action<HttpOption>? action = null)
            => PutAsync(url, data, action).GetAwaiter().GetResult();
    }
}
