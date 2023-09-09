using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Net.Http;
using Suyaa;
using System.Runtime.CompilerServices;

namespace sy
{
    /// <summary>
    /// Http
    /// </summary>
    public static partial class Http
    {
        // 私有变量
        private static HttpClientHandler? _handler;

        /// <summary>
        /// Http Client Handler
        /// </summary>
        public static HttpClientHandler DefaultHandler
            => _handler ??= new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.None,
                AllowAutoRedirect = true,
                UseProxy = false,
                //Proxy = null,
                ClientCertificateOptions = ClientCertificateOption.Automatic,
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            };

        #region 创建客户端

        /// <summary>
        /// 获取一个新的客户端
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static HttpClient GetClient(HttpClientHandler handler)
            => new HttpClient(handler);

        /// <summary>
        /// 获取一个新的客户端
        /// </summary>
        /// <returns></returns>
        public static HttpClient GetClient()
            => new HttpClient(DefaultHandler);

        #endregion

        #region 发送数据

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsync(HttpClientHandler handler, HttpRequestMessage request)
        {
            // 新建客户端
            var client = new HttpClient(handler);
            return await client.SendAsync(request);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            // 新建一个Handler
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.None,
                AllowAutoRedirect = true,
                UseProxy = false,
                //Proxy = null,
                ClientCertificateOptions = ClientCertificateOption.Automatic
            };
            return await SendAsync(handler, request);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static HttpResponseMessage Send(HttpClientHandler handler, HttpRequestMessage request)
        {
            return SendAsync(handler, request).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static HttpResponseMessage Send(HttpRequestMessage request)
        {
            return SendAsync(request).GetAwaiter().GetResult();
        }

        #endregion
    }
}
