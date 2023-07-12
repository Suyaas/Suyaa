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
        /// 获取DELETE方式的应答结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> DeleteResponseAsync(string url, HttpOption option)
        {
            using var client = GetClient();
            // 设置头
            client.SetHeaders(option.Headers);
            return await client.DeleteAsync(url);
        }

        /// <summary>
        /// 以DELETE方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<string> DeleteAsync(string url, HttpOption option)
        {
            // 应答器
            using HttpResponseMessage response = await DeleteResponseAsync(url, option);
            // 触发应答事件
            if (!option.RaiseResponseEvent(response)) return string.Empty;
            // 判断状态并抛出异常
            response.EnsureSuccessStatusCode();
            // 返回数据结果
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 以DELETE方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> DeleteAsync(string url, Action<HttpOption>? action = null)
        {
            using HttpOption option = new HttpOption();
            action?.Invoke(option);
            // 返回数据结果
            return await DeleteAsync(url, option);
        }

        /// <summary>
        /// 以DELETE方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string Delete(string url, Action<HttpOption>? action = null)
            => DeleteAsync(url, action).GetAwaiter().GetResult();
    }
}
