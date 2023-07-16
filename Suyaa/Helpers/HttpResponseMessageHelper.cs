using Suyaa.Net.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// Http助手
    /// </summary>
    public static class HttpResponseMessageHelper
    {
        /// <summary>
        /// 获取Cookies
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static HttpCookies GetCookies(this HttpResponseMessage response)
        {
            HttpCookies cookies = new HttpCookies();
            var values = response.Headers.GetValues("Set-Cookie");
            foreach (var value in values)
            {
                cookies.SetCookies(value);
            }
            return cookies;
        }
    }
}
