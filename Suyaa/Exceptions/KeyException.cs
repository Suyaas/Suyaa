using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// 带关键字的异常
    /// </summary>
    public class KeyException : Exception
    {
        /// <summary>
        /// 带关键字的异常
        /// </summary>
        public KeyException(string key, string message, params string[] parameters) : base(key + " occurred: " + string.Format(message, parameters))
        {
            Key = key;
            OriginalMessage = message;
            OriginalParameters = parameters;
        }

        /// <summary>
        /// 带关键字的异常
        /// </summary>
        public KeyException(string key, Exception innerException, string message, params string[] parameters) : base(key + " occurred: " + string.Format(message, parameters), innerException)
        {
            Key = key;
            OriginalMessage = message;
            OriginalParameters = parameters;
        }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 原始消息
        /// </summary>
        public string OriginalMessage { get; }

        /// <summary>
        /// 原始参数
        /// </summary>
        public string[] OriginalParameters { get; }
    }
}
