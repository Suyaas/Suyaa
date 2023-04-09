using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Exceptions
{
    /// <summary>
    /// 为空错误
    /// </summary>
    public class NullException : Exception
    {
        /// <summary>
        /// 为空错误
        /// </summary>
        public NullException() : base("对象为空") { }

        /// <summary>
        /// 为空错误
        /// </summary>
        public NullException(Type? type) : base(type is null ? $"对象类型为空" : $"对象'{type.FullName}'为空") { }

        /// <summary>
        /// 为空错误
        /// </summary>
        public NullException(string message) : base(message) { }
    }
}
