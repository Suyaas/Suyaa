using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Net.Http;
using Suyaa;
using static System.Collections.Specialized.BitVector32;

namespace sy
{
    /// <summary>
    /// Http
    /// </summary>
    public static partial class Http
    {

        #region 异步接口

        /// <summary>
        /// 获取Put方式的应答结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bytes"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PutResponseAsync(string url, byte[] bytes, HttpOption option)
        {
            var client = GetClient();
            // 建立传输内容
            var content = new ByteArrayContent(bytes);
            // 设置头
            option.Headers.SetCookies(option.Cookies);
            client.SetHeaders(option.Headers);
            content.SetHeaders(option.Headers);
            return await client.PutAsync(url, content);
        }

        /// <summary>
        /// 以Post方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bytes"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<string> PutAsync(string url, byte[] bytes, HttpOption option)
        {
            // 应答器
            using HttpResponseMessage response = await PutResponseAsync(url, bytes, option);
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
        public static async Task<string> PutAsync(string url, byte[] bytes, Action<HttpOption>? action = null)
        {
            using HttpOption option = new HttpOption();
            action?.Invoke(option);
            // 执行并返回数据结果
            return await PutAsync(url, bytes, option);
        }

        /// <summary>
        /// 以Put方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> PutAsync(string url, string data, Action<HttpOption>? action = null)
            => await PutAsync(url, Encoding.UTF8.GetBytes(data), action);

        /// <summary>
        /// 以Put方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> PutAsync(string url, string data, Encoding encoding, Action<HttpOption>? action = null)
            => await PutAsync(url, encoding.GetBytes(data), action);

        #endregion

        #region 同步接口

        /// <summary>
        /// 获取Put方式的应答结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bytes"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static HttpResponseMessage PutResponse(string url, byte[] bytes, HttpOption option)
        {
            var client = GetClient();
            // 建立传输内容
            var content = new ByteArrayContent(bytes);
            // 设置头
            option.Headers.SetCookies(option.Cookies);
            client.SetHeaders(option.Headers);
            content.SetHeaders(option.Headers);
            return client.PutAsync(url, content).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 以Post方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bytes"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static string Put(string url, byte[] bytes, HttpOption option)
        {
            // 应答器
            using HttpResponseMessage response = PutResponse(url, bytes, option);
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
        public static string Put(string url, byte[] bytes, Action<HttpOption>? action = null)
        {
            using HttpOption option = new HttpOption();
            action?.Invoke(option);
            // 执行并返回数据结果
            return Put(url, bytes, option);
        }

        /// <summary>
        /// 以Put方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string Put(string url, string data, Action<HttpOption>? action = null)
            => Put(url, Encoding.UTF8.GetBytes(data), action);

        /// <summary>
        /// 以Put方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string Put(string url, string data, Encoding encoding, Action<HttpOption>? action = null)
            => Put(url, encoding.GetBytes(data), action);

        #endregion
    }
}
