using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// 为空错误
    /// </summary>
    public class NullException : Exception
    {
        /// <summary>
        /// 为空错误
        /// </summary>
        public NullException() : base("Object is null") { }

        /// <summary>
        /// 为空错误
        /// </summary>
        public NullException(Type type) : base($"Type '{type.FullName}' not instantiated")
        {
            Type = type;
        }

        /// <summary>
        /// 为空错误
        /// </summary>
        public NullException(string message) : base(message) { }

        /// <summary>
        /// 关联类型
        /// </summary>
        public Type? Type { get; }
    }

    /// <summary>
    /// 为空错误
    /// </summary>
    public class NullException<T> : NullException
    {
        /// <summary>
        /// 为空错误
        /// </summary>
        public NullException() : base(typeof(T)) { }
    }
}
