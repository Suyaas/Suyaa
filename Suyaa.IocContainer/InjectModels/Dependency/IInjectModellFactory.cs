using Suyaa.IocContainer.Kernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.IocContainer.InjectModels.Dependency
{
    /// <summary>
    /// 依赖建模工厂
    /// </summary>
    public interface IInjectModellFactory
    {
        /// <summary>
        /// 建模集合
        /// </summary>
        IEnumerable<InjectModel> GetModels();
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
    }
}
