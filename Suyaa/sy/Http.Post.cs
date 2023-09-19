using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Net.Http;
using Suyaa;
using System.IO;

namespace sy
{
    /// <summary>
    /// Http
    /// </summary>
    public static partial class Http
    {
        #region 异步接口

        /// <summary>
        /// 获取Post方式的应答结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bytes"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostResponseAsync(string url, byte[] bytes, HttpOption option)
        {
            var client = GetClient();
            // 建立传输内容
            var content = new ByteArrayContent(bytes);
            // 设置头
            option.Headers.SetCookies(option.Cookies);
            content.SetHeaders(option.Headers);
            client.SetHeaders(option.Headers);
            return await client.PostAsync(url, content);
        }

        /// <summary>
        /// 以Post方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bytes"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, byte[] bytes, HttpOption option)
        {
            // 应答器
            using HttpResponseMessage response = await PostResponseAsync(url, bytes, option);
            // 触发应答事件
            if (!option.RaiseResponseEvent(response)) return string.Empty;
            // 判断状态并抛出异常
            if (option.IsEnsureStatus) response.EnsureSuccessStatusCode();
            // 返回数据结果
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 以Post方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bytes"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, byte[] bytes, Action<HttpOption>? action = null)
        {
            using HttpOption option = new HttpOption();
            action?.Invoke(option);
            // 执行并返回数据结果
            return await PostAsync(url, bytes, option);
        }

        /// <summary>
        /// 以Post方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, string data, Action<HttpOption>? action = null)
            => await PostAsync(url, Encoding.UTF8.GetBytes(data), action);

        /// <summary>
        /// 以Post方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, string data, Encoding encoding, Action<HttpOption>? action = null)
            => await PostAsync(url, encoding.GetBytes(data), action);

        #endregion

        #region 同步接口

        /// <summary>
        /// 获取Post方式的应答结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bytes"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static HttpResponseMessage PostResponse(string url, byte[] bytes, HttpOption option)
        {
            var client = GetClient();
            // 建立传输内容
            var content = new ByteArrayContent(bytes);
            // 设置头
            option.Headers.SetCookies(option.Cookies);
            content.SetHeaders(option.Headers);
            client.SetHeaders(option.Headers);
            return client.PostAsync(url, content).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 以Post方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bytes"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static string Post(string url, byte[] bytes, HttpOption option)
        {
            // 应答器
            using HttpResponseMessage response = PostResponse(url, bytes, option);
            // 触发应答事件
            if (!option.RaiseResponseEvent(response)) return string.Empty;
            // 判断状态并抛出异常
            if (option.IsEnsureStatus) response.EnsureSuccessStatusCode();
            // 返回数据结果
            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 以Post方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bytes"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string Post(string url, byte[] bytes, Action<HttpOption>? action = null)
        {
            using HttpOption option = new HttpOption();
            action?.Invoke(option);
            // 执行并返回数据结果
            return Post(url, bytes, option);
        }

        /// <summary>
        /// 以Post方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string Post(string url, string data, Action<HttpOption>? action = null)
            => Post(url, Encoding.UTF8.GetBytes(data), action);

        /// <summary>
        /// 以Post方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string Post(string url, string data, Encoding encoding, Action<HttpOption>? action = null)
            => Post(url, encoding.GetBytes(data), action);

        #endregion
    }
}
