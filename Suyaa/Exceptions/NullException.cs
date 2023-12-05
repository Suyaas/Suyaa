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
        /// 为空
        /// </summary>
        public const string KEY_NULL = "NonStandard";
        /// <summary>
        /// 为空错误
        /// </summary>
        public NullException() : base(KEY_NULL, "Object is null") { }

        /// <summary>
        /// 为空错误
        /// </summary>
        public NullException(Type type) : base(KEY_NULL + ".Type", $"Type '{0}' not instantiated", type.FullName)
        {
            Type = type;
        }

        /// <summary>
        /// 为空错误
        /// </summary>
        public NullException(string key, string message, params string[] parameters) : base(KEY_NULL + "." + key, message, parameters)
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
