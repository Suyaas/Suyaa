using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace Suyaa.Net.Http
{
    /// <summary>
    /// Http Cookies
    /// </summary>
    public class HttpCookies : Dictionary<string, string>, IDisposable
    {

        /// <summary>
        /// 创建 Http Cookies
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static HttpCookies Create(string cookies)
        {
            var httpCookies = new HttpCookies();
            httpCookies.SetCookies(cookies);
            return httpCookies;
        }

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
        public HttpCookies Set(string name, string value)
        {
            this[name] = value;
            return this;
        }

        /// <summary>
        /// 设置Cookie字符串
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public HttpCookies SetCookies(string cookies)
        {
            string[] strs = cookies.Split(';');
            foreach (string item in strs)
            {
                var str = item.Trim();
                if (str.IsNullOrWhiteSpace()) continue;
                int index = str.IndexOf('=');
                if (index <= 0)
                {
                    this[str] = string.Empty;
                    continue;
                }
                this[str.Substring(0, index)] = str.Substring(index + 1);
            }
            return this;
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this)
            {
                sb.Append(item.Key);
                sb.Append('=');
                sb.Append(item.Value);
                sb.Append(';');
            }
            return sb.ToString();
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
