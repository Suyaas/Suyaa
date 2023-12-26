using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.IocContainer.Kernel.Dependency
{
    /// <summary>
    /// Ioc工作域
    /// </summary>
    public interface IIocScope
    {
        /// <summary>
        /// Ioc容器
        /// </summary>
        IIocContainer IocContainer { get; }
        /// <summary>
        /// 决议对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        object? Resolve(Type serviceType);
    }
}
