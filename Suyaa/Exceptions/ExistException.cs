using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// 已存在异常
    /// </summary>
    public class ExistException : KeyException
    {
        /// <summary>
        /// 存在
        /// </summary>
        public const string KEY_EXIST = "Exist";

        /// <summary>
        /// 已存在异常
        /// </summary>
        public ExistException(Type type) : base(KEY_EXIST + ".Type", "Type '{0}' is already exist.", type.FullName)
        {
            Type = type;
            Name = type.FullName;
        }

        /// <summary>
        /// 已存在异常
        /// </summary>
        public ExistException(string name) : base(KEY_EXIST + ".Name", "{0} is already exist.", name)
        {
            Name = name;
        }

        /// <summary>
        /// 已存在异常
        /// </summary>
        public ExistException(string key, string message, params string[] parameters) : base(KEY_EXIST + "." + key, message, parameters) { }

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
    /// 已存在异常
    /// </summary>
    public class ExistException<T> : ExistException
    {
        /// <summary>
        /// 已存在异常
        /// </summary>
        public ExistException() : base(typeof(T)) { }
    }
}
