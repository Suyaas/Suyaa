using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data
{
    /// <summary>
    /// 数据库处理异常
    /// </summary>
    public class DbException : Exception
    {
        /// <summary>
        /// 数据库处理异常
        /// </summary>
        /// <param name="message">异常信息</param>
        public DbException(string message) : base(message) { }

        /// <summary>
        /// 数据库处理异常
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="innerException">包含的异常</param>
        public DbException(string message, Exception innerException) : base(message, innerException) { }
    }
}
