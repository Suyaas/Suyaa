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
        /// 获取DELETE方式的应答结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> DeleteResponseAsync(string url, HttpOption option)
        {
            var client = GetClient();
            // 设置头
            option.Headers.SetCookies(option.Cookies);
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

        #endregion

        #region 同步接口

        /// <summary>
        /// 获取DELETE方式的应答结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static HttpResponseMessage DeleteResponse(string url, HttpOption option)
        {
            var client = GetClient();
            // 设置头
            option.Headers.SetCookies(option.Cookies);
            client.SetHeaders(option.Headers);
            return client.DeleteAsync(url).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 以DELETE方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static string Delete(string url, HttpOption option)
        {
            // 应答器
            using HttpResponseMessage response = DeleteResponse(url, option);
            // 触发应答事件
            if (!option.RaiseResponseEvent(response)) return string.Empty;
            // 判断状态并抛出异常
            response.EnsureSuccessStatusCode();
            // 返回数据结果
            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 以DELETE方式获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string Delete(string url, Action<HttpOption>? action = null)
        {
            using HttpOption option = new HttpOption();
            action?.Invoke(option);
            // 返回数据结果
            return Delete(url, option);
        }

        #endregion
    }
}
