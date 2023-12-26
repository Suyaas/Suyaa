using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.IocContainer.Kernel.Exceptions
{
    /// <summary>
    /// Ioc异常
    /// </summary>
    public class IocNotExistsException : KeyException
    {
        /// <summary>
        /// 不存在
        /// </summary>
        public const string KEY_IOC_NOT_EXIST = "IocNotExist";

        /// <summary>
        /// Ioc异常
        /// </summary>
        /// <param name="type"></param>
        public IocNotExistsException(Type type) : base(KEY_IOC_NOT_EXIST, "Service type {0} not exists.", type.FullName)
        {
            Type = type;
        }

        /// <summary>
        /// 对象类型
        /// </summary>
        public Type Type { get; }
    }

    /// <summary>
    /// Ioc异常
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class IocNotExistsException<T> : IocNotExistsException
    {
        /// <summary>
        /// Ioc异常
        /// </summary>
        public IocNotExistsException() : base(typeof(T))
        {
        }
    }
}
