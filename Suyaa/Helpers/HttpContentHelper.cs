using Suyaa.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// Http助手
    /// </summary>
    public static class HttpContentHelper
    {
        // 获取内容类型
        private static MediaTypeHeaderValue GetMedia(string content)
        {
            if (content.IsNullOrWhiteSpace()) return new MediaTypeHeaderValue(Net.Http.HttpHeaders.X_WWW_FORM_URLENCODED);
            string[] parts = content.Split(';');
            var media = new MediaTypeHeaderValue(parts[0].Trim());
            for (int i = 1; i < parts.Length; i++)
            {
                string part = parts[i].Trim();
                if (part.IsNullOrWhiteSpace()) continue;
                int index = part.IndexOf('=');
                if (index > 0)
                {
                    media.Parameters.Add(new NameValueHeaderValue(part.Substring(0, index), part.Substring(index + 1)));
                }
                else
                {
                    media.Parameters.Add(new NameValueHeaderValue(part));
                }
            }
            return media;
        }

        /// <summary>
        /// 设置头
        /// </summary>
        /// <param name="content"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static HttpContent SetHeaders(this HttpContent content, Net.Http.HttpHeaders headers)
        {
            // 设置内容类型
            content.Headers.ContentType = GetMedia(headers.ContentType);
            // 设置字符类型
            if (headers.ContainsKey(Net.Http.HttpHeaders.CONTENT_ENCODING))
            {
                var encodings = headers.ContentEncoding.Split(',');
                foreach (var encoding in encodings)
                {
                    content.Headers.ContentEncoding.Add(encoding.Trim());
                }
            }
            // 设置内容长度
            if (headers.ContainsKey(Net.Http.HttpHeaders.CONTENT_LENGTH))
            {
                content.Headers.ContentLength = headers.ContentLength;
            }
            // 设置内容长度
            if (headers.ContainsKey(Net.Http.HttpHeaders.CONTENT_DISPOSITION))
            {
                if (content.Headers.Contains(Net.Http.HttpHeaders.CONTENT_DISPOSITION))
                {
                    content.Headers.Remove(Net.Http.HttpHeaders.CONTENT_DISPOSITION);
                }
                content.Headers.Add(Net.Http.HttpHeaders.CONTENT_DISPOSITION, headers.ContentDisposition);
            }
            return content;
        }
    }
}
