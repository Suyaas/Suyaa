using Suyaa.Net.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// Http助手
    /// </summary>
    public static class HttpClientHelper
    {
        /// <summary>
        /// 设置头
        /// </summary>
        /// <param name="client"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static HttpClient SetHeader(this HttpClient client, string key, string value)
        {
            // 清理原来的数据
            if (client.DefaultRequestHeaders.Contains(key)) client.DefaultRequestHeaders.Remove(key);
            // 设置新的数据
            client.DefaultRequestHeaders.Add(key, value);
            return client;
        }

        /// <summary>
        /// 设置头
        /// </summary>
        /// <param name="client"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static HttpClient SetHeaders(this HttpClient client, HttpHeaders headers)
        {
            foreach (var header in headers)
            {
                client.SetHeader(header.Key, header.Value);
            }
            return client;
        }
    }
}
