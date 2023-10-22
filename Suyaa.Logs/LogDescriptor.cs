using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs
{

    /// <summary>
    /// 日志信息
    /// </summary>
    public class LogDescriptor
    {
        // 记录索引
        private static long _recordIndexer = 0;

        // 获取新的记录索引
        internal static long GetNewRecordId() => ++_recordIndexer;

        /// <summary>
        /// 索引号
        /// </summary>
        public virtual long RecordId { get; internal set; }

        /// <summary>
        /// 级别
        /// </summary>
        public virtual LogLevel Level { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public virtual string? Source { get; set; }

        /// <summary>
        /// 事件
        /// </summary>
        public virtual string? Event { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public virtual string? Message { get; set; }
    }
}
