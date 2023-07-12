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
    public static class HttpContentHelper
    {
        /// <summary>
        /// 设置头
        /// </summary>
        /// <param name="content"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static HttpContent SetHeader(this HttpContent content, string key, string value)
        {
            // 清理原来的数据
            if (content.Headers.Contains(key)) content.Headers.Remove(key);
            // 设置新的数据
            content.Headers.Add(key, value);
            return content;
        }

        /// <summary>
        /// 设置头
        /// </summary>
        /// <param name="content"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static HttpContent SetHeaders(this HttpContent content, HttpHeaders headers)
        {
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(headers.ContentType);
            foreach (var header in headers)
            {
                content.SetHeader(header.Key, string.Join(";", header.Value));
            }
            return content;
        }
    }
}
