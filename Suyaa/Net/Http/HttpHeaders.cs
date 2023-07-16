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
    public class HttpHeaders : System.Net.Http.Headers.HttpHeaders, IDisposable
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
            return string.Join(";", GetValues(name));
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HttpHeaders Set(string name, string value)
        {
            // 清理已有的值
            if (Contains(name)) Remove(name);
            // 添加新的值
            Add(name, value);
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
        /// 获取或设置头信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get => Get(name);
            set => Set(name, value);
        }

        /// <summary>
        /// ContentType
        /// </summary>
        public string ContentType
        {
            get
            {
                if (!Contains(CONTENT_TYPE)) return X_WWW_FORM_URLENCODED;
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
