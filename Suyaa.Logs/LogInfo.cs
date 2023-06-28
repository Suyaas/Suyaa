using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs
{

    /// <summary>
    /// 记录实体
    /// </summary>
    public class LogInfo
    {

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
