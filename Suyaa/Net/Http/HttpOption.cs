using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace Suyaa.Net.Http
{
    /// <summary>
    /// Http选项
    /// </summary>
    public class HttpOption : IDisposable
    {
        /// <summary>
        /// 头信息
        /// </summary>
        public HttpHeaders Headers { get; }

        /// <summary>
        /// 是否响应异常
        /// </summary>
        public bool IsEnsureStatus { get; set; }

        /// <summary>
        /// Http选项
        /// </summary>
        public HttpOption()
        {
            this.Headers = new HttpHeaders();
            this.IsEnsureStatus = true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.Headers.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
