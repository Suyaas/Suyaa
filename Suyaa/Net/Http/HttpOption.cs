using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;

namespace Suyaa.Net.Http
{

    /// <summary>
    /// Http选项
    /// </summary>
    public class HttpOption : IDisposable
    {
        // 下载
        private Action<HttpDownloadInfo>? _download;
        private Func<HttpResponseMessage, bool>? _response;

        /// <summary>
        /// 头信息
        /// </summary>
        public HttpHeaders Headers { get; }

        /// <summary>
        /// Cookies 信息
        /// </summary>
        public HttpCookies Cookies { get; }

        /// <summary>
        /// 是否响应异常
        /// </summary>
        public bool IsEnsureStatus { get; set; }

        /// <summary>
        /// 触发下载事件
        /// </summary>
        /// <param name="info"></param>
        internal void RaiseDownloadEvent(HttpDownloadInfo info)
        {
            // 执行事件
            _download?.Invoke(info);
        }

        /// <summary>
        /// 触发下载事件
        /// </summary>
        /// <param name="response"></param>
        internal bool RaiseResponseEvent(HttpResponseMessage response)
        {
            // 执行事件
            return _response?.Invoke(response) ?? true;
        }

        /// <summary>
        /// 注册下载事件
        /// </summary>
        /// <param name="action"></param>
        public void OnDownload(Action<HttpDownloadInfo> action)
        {
            _download = action;
        }

        /// <summary>
        /// 注册应答事件
        /// </summary>
        /// <param name="func"></param>
        public void OnResponse(Func<HttpResponseMessage, bool> func)
        {
            _response = func;
        }

        /// <summary>
        /// Http选项
        /// </summary>
        public HttpOption()
        {
            Headers = new HttpHeaders();
            Cookies = new HttpCookies();
            IsEnsureStatus = true;
        }

        #region 释放资源

        // 是否释放
        private bool _disposed;

        /// <summary>
        /// 释放事件
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void OnDispose(bool disposing)
        {
            if (_disposed) return;
            #region 托管释放
            if (disposing)
            {
                Headers.Dispose();
                Cookies.Dispose();
            }
            #endregion
            #region 非托管释放
            _download = null;
            _response = null;
            #endregion
            _disposed = true;
        }

        /// <summary>
        /// 释放托管资源
        /// </summary>
        public void Dispose()
        {
            OnDispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 构析函数
        /// </summary>
        ~HttpOption()
        {
            OnDispose(false);
        }

        #endregion
    }
}
