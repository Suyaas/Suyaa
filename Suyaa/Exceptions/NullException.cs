using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// 为空错误
    /// </summary>
    public class NullException : KeyException

    {
        /// <summary>
        /// 为空错误
        /// </summary>
        public NullException() : base("Exception.Null", "Object is null") { }

        /// <summary>
        /// 为空错误
        /// </summary>
        public NullException(Type type) : base("Exception.Null.Type", $"Type '{0}' not instantiated", type.FullName)
        {
            Type = type;
        }

        /// <summary>
        /// 为空错误
        /// </summary>
        public NullException(string key, string message, params string[] parameters) : base("Exception.Null." + key, message, parameters)
        {
            Type = null;
        }

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
