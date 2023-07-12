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
        private Action<HttpResponseMessage>? _response;

        /// <summary>
        /// 头信息
        /// </summary>
        public HttpHeaders Headers { get; }

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
            // 判断是否已经注册事件
            if (_download is null) return;
            // 执行事件
            _download(info);
        }

        /// <summary>
        /// 触发下载事件
        /// </summary>
        /// <param name="response"></param>
        internal void RaiseResponseEvent(HttpResponseMessage response)
        {
            // 判断是否已经注册事件
            if (_response is null) return;
            // 执行事件
            _response(response);
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
        /// <param name="action"></param>
        public void OnResponse(Action<HttpResponseMessage> action)
        {
            _response = action;
        }

        /// <summary>
        /// Http选项
        /// </summary>
        public HttpOption()
        {
            this.Headers = new HttpHeaders();
            this.IsEnsureStatus = true;
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
                this.Headers.Dispose();
            }
            #endregion
            #region 非托管释放
            _download = null;
            _download = null;
            #endregion
            _disposed = true;
        }

        /// <summary>
        /// 释放托管资源
        /// </summary>
        public void Dispose()
        {
            this.OnDispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 构析函数
        /// </summary>
        ~HttpOption()
        {
            this.OnDispose(false);
        }

        #endregion
    }
}
