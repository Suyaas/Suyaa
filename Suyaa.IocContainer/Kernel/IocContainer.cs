using Suyaa.IocContainer.InjectModels;
using Suyaa.IocContainer.InjectModels.Dependency;
using Suyaa.IocContainer.Kernel.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.IocContainer.Kernel
{
    /// <summary>
    /// Ioc容器
    /// </summary>
    public sealed class IocContainer : IIocContainer
    {
        // 建模集合
        private readonly InjectModellFactory _injectModellFactory;

        /// <summary>
        /// Ioc容器
        /// </summary>
        public IocContainer()
        {
            _injectModellFactory = new InjectModellFactory();
        }

        /// <summary>
        /// 建模集合
        /// </summary>
        public IEnumerable<InjectModel> Models => _injectModellFactory.GetModels();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetime"></param>
        public void Add(Type serviceType, Type implementationType, Lifetime lifetime)
        {
            _injectModellFactory.Add(serviceType, implementationType, lifetime);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="type"></param>
        public void Remove(Type type)
        {
            _injectModellFactory.Remove(type);
        }
    }
}
