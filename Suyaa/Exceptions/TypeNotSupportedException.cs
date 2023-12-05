using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// 不支持异常
    /// </summary>
    public class TypeNotSupportedException : KeyException
    {
        /// <summary>
        /// 类型不支持
        /// </summary>
        public const string KEY_TYPE_NOT_SUPPORTED = "TypeNotSupported";
        /// <summary>
        /// 不支持异常
        /// </summary>
        public TypeNotSupportedException(Type type) : base(KEY_TYPE_NOT_SUPPORTED, "Type '{0}' is not supported.", type.FullName)
        {
            Type = type;
        }

        /// <summary>
        /// 不支持异常
        /// </summary>
        public TypeNotSupportedException(string key, string message, params string[] parameters) : base(KEY_TYPE_NOT_SUPPORTED + "." + key, message, parameters) { }

        /// <summary>
        /// 关联类型
        /// </summary>
        public Type? Type { get; }
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
