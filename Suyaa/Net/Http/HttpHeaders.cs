using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace Suyaa.Net.Http
{
    /// <summary>
    /// Http头
    /// </summary>
    public class HttpHeaders : Dictionary<string, string>, IDisposable
    {
        /// <summary>
        /// application/x-www-form-urlencoded
        /// </summary>
        public const string X_WWW_FORM_URLENCODED = "application/x-www-form-urlencoded";

        /// <summary>
        /// 内容类型
        /// </summary>
        public const string CONTENT_TYPE = "Content-Type";

        /// <summary>
        /// 内容字符集
        /// </summary>
        public const string CONTENT_ENCODING = "Content-Encoding";

        /// <summary>
        /// 内容长度
        /// </summary>
        public const string CONTENT_LENGTH = "Content-Length";

        /// <summary>
        /// 内容处置方式
        /// </summary>
        public const string CONTENT_DISPOSITION = "Content-Disposition";

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string Get(string name)
        {
            if (this.ContainsKey(name)) return this[name];
            return string.Empty;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HttpHeaders Set(string name, string value)
        {
            this[name] = value;
            return this;
        }

        /// <summary>
        /// 设置 Cookies
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public HttpHeaders SetCookies(HttpCookies cookies)
        {
            if (!cookies.Any()) return this;
            this["Cookie"] = cookies.ToString();
            return this;
        }

        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType
        {
            get
            {
                if (!this.ContainsKey(CONTENT_TYPE)) return X_WWW_FORM_URLENCODED;
                return Get(CONTENT_TYPE);
            }
            set => Set(CONTENT_TYPE, value);
        }

        /// <summary>
        /// 内容字符集
        /// </summary>
        public string ContentEncoding
        {
            get
            {
                if (!this.ContainsKey(CONTENT_ENCODING)) return string.Empty;
                return Get(CONTENT_ENCODING);
            }
            set => Set(CONTENT_ENCODING, value);
        }

        /// <summary>
        /// 内容字符集
        /// </summary>
        public long ContentLength
        {
            get
            {
                if (!this.ContainsKey(CONTENT_LENGTH)) return 0;
                return Get(CONTENT_LENGTH).ToLong();
            }
            set => Set(CONTENT_LENGTH, value.ToString());
        }

        /// <summary>
        /// 内容处置方式
        /// </summary>
        public string ContentDisposition
        {
            get
            {
                if (!this.ContainsKey(CONTENT_DISPOSITION)) return string.Empty;
                return Get(CONTENT_DISPOSITION);
            }
            set => Set(CONTENT_DISPOSITION, value);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Clear();
            GC.SuppressFinalize(this);
        }
    }
}
