using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Net.Http
{

    /// <summary>
    /// Http下载信息
    /// </summary>
    public sealed class HttpDownloadInfo
    {
        /// <summary>
        /// 总数据量
        /// </summary>
        public long TotalSize { get; }

        /// <summary>
        /// 已接收数据量
        /// </summary>
        public long ReceiveSize { get; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsCompleted { get; }

        /// <summary>
        /// Http下载信息
        /// </summary>
        /// <param name="totalSize"></param>
        /// <param name="receiveSize"></param>
        /// <param name="isCompleted"></param>
        public HttpDownloadInfo(long totalSize, long receiveSize, bool isCompleted)
        {
            TotalSize = totalSize;
            ReceiveSize = receiveSize;
            IsCompleted = isCompleted;
        }

        /// <summary>
        /// Http下载信息
        /// </summary>
        /// <param name="totalSize"></param>
        /// <param name="receiveSize"></param>
        public HttpDownloadInfo(long totalSize, long receiveSize)
        {
            TotalSize = totalSize;
            ReceiveSize = receiveSize;
            IsCompleted = receiveSize >= totalSize;
        }
    }
}
