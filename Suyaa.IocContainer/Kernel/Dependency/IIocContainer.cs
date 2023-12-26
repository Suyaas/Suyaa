using Suyaa.IocContainer.InjectModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.IocContainer.Kernel.Dependency
{
    /// <summary>
    /// IoC容器
    /// </summary>
    public interface IIocContainer
    {
        /// <summary>
        /// 建模集合
        /// </summary>
        IEnumerable<InjectModel> Models { get; }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetime"></param>
        void Add(Type serviceType, Type implementationType, Lifetime lifetime);
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="type"></param>
        void Remove(Type serviceType, Type implementationType);
        /// <summary>
        /// 决议对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        object? Resolve(Type serviceType);
    }
}
