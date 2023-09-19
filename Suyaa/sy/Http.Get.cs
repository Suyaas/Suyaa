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
        #region 异步接口

        /// <summary>
        /// 获取Get方式的应答结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> GetResponseAsync(string url, HttpOption option)
        {
            var client = GetClient();
            option.Headers.SetCookies(option.Cookies);
            client.SetHeaders(option.Headers);
            return await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        }

        /// <summary>
        /// 以Get方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, HttpOption option)
        {
            // 应答器
            using HttpResponseMessage response = await GetResponseAsync(url, option);
            // 触发应答事件
            if (!option.RaiseResponseEvent(response)) return string.Empty;
            // 判断状态并抛出异常
            if (option.IsEnsureStatus) response.EnsureSuccessStatusCode();
            // 返回数据结果
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 以Get方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, Action<HttpOption>? action = null)
        {
            using HttpOption option = new HttpOption();
            action?.Invoke(option);
            // 返回数据结果
            return await GetAsync(url, option);
        }

        #endregion

        #region 同步接口

        /// <summary>
        /// 获取Get方式的应答结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static HttpResponseMessage GetResponse(string url, HttpOption option)
        {
            var client = GetClient();
            option.Headers.SetCookies(option.Cookies);
            client.SetHeaders(option.Headers);
            return client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 以Get方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static string Get(string url, HttpOption option)
        {
            // 应答器
            using HttpResponseMessage response = GetResponse(url, option);
            // 触发应答事件
            if (!option.RaiseResponseEvent(response)) return string.Empty;
            // 判断状态并抛出异常
            if (option.IsEnsureStatus) response.EnsureSuccessStatusCode();
            // 返回数据结果
            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 以Get方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string Get(string url, Action<HttpOption>? action = null)
        {
            using HttpOption option = new HttpOption();
            action?.Invoke(option);
            // 返回数据结果
            return Get(url, option);
        }

        #endregion

    }
}
