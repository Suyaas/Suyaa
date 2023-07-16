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
        /// content_type
        /// </summary>
        private const string CONTENT_TYPE = "content-type";

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
            this["Cookie"] = cookies.ToString();
            return this;
        }

        /// <summary>
        /// ContentType
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
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Clear();
            GC.SuppressFinalize(this);
        }
    }
}
