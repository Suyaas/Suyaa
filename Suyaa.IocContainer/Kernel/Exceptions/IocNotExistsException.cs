using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.IocContainer.Kernel.Exceptions
{
    /// <summary>
    /// Ioc构造函数异常
    /// </summary>
    public class IocConstructorException : KeyException
    {
        /// <summary>
        /// 不存在
        /// </summary>
        public const string KEY_IOC_CONSTRUCTOR = "IocConstructor";

        /// <summary>
        /// Ioc构造函数异常
        /// </summary>
        /// <param name="type"></param>
        public IocConstructorException(Type type) : base(KEY_IOC_CONSTRUCTOR, "Implementation type {0} too many constructors.", type.FullName)
        {
            Type = type;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }
    }

    /// <summary>
    /// Ioc构造函数异常
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class IocConstructorException<T> : IocConstructorException
    {
        /// <summary>
        /// Ioc构造函数异常
        /// </summary>
        public IocConstructorException() : base(typeof(T))
        {
        }
    }
}
