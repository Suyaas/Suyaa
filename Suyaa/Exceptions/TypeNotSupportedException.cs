using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Exceptions
{
    /// <summary>
    /// 不支持异常
    /// </summary>
    public class TypeNotSupportedException : Exception
    {
        /// <summary>
        /// 不支持异常
        /// </summary>
        public TypeNotSupportedException(Type type) : base($"Type '{type.FullName}' is not supported") { }

        /// <summary>
        /// 不支持异常
        /// </summary>
        public TypeNotSupportedException(string message) : base(message) { }
    }

    /// <summary>
    /// 不支持异常
    /// </summary>
    public class NotSupportedException<T> : TypeNotSupportedException
    {
        /// <summary>
        /// 不支持异常
        /// </summary>
        public NotSupportedException() : base(typeof(T)) { }
    }
}
