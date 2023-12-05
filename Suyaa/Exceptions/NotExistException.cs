using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// 不存在异常
    /// </summary>
    public class NotExistException : KeyException
    {
        /// <summary>
        /// 不存在
        /// </summary>
        public const string KEY_NOT_EXIST = "NotExist";

        /// <summary>
        /// 不存在异常
        /// </summary>
        public NotExistException(Type type) : base(KEY_NOT_EXIST + ".Type", "Type '{0}' does not exist.", type.FullName)
        {
            Type = type;
            Name = type.FullName;
        }

        /// <summary>
        /// 不存在异常
        /// </summary>
        public NotExistException(string name) : base(KEY_NOT_EXIST + ".Name", "{0} does not exist.", name)
        {
            Name = name;
        }

        /// <summary>
        /// 不存在异常
        /// </summary>
        public NotExistException(string key, string message, params string[] parameters) : base(KEY_NOT_EXIST + "." + key, message, parameters) { }

        /// <summary>
        /// 关联类型
        /// </summary>
        public Type? Type { get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = string.Empty;
    }

    /// <summary>
    /// 不存在异常
    /// </summary>
    public class NotExistException<T> : ExistException
    {
        /// <summary>
        /// 不存在异常
        /// </summary>
        public NotExistException() : base(typeof(T)) { }
    }
}
