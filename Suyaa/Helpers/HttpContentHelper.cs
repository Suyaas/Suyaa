using Suyaa.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="headers"></param>
        /// <returns></returns>
        public static HttpContent SetHeaders(this HttpContent content, HttpHeaders headers)
        {
            // 设置内容类型
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(headers.ContentType);
            // 设置字符类型
            if (headers.ContainsKey(HttpHeaders.CONTENT_ENCODING))
            {
                var encodings = headers.ContentEncoding.Split(',');
                foreach (var encoding in encodings)
                {
                    content.Headers.ContentEncoding.Add(encoding.Trim());
                }
            }
            // 设置内容长度
            if (headers.ContainsKey(HttpHeaders.CONTENT_LENGTH))
            {
                content.Headers.ContentLength = headers.ContentLength;
            }
            return content;
        }
    }
}
