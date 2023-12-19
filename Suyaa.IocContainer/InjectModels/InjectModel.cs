using Suyaa.IocContainer.InjectModels.Dependency;
using Suyaa.IocContainer.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.IocContainer.InjectModels
{
    /// <summary>
    /// 注入模型
    /// </summary>
    public sealed class InjectModel
    {
        private readonly IEnumerable<IInjectModellProvider> _providers;

        /// <summary>
        /// 注入模型
        /// </summary>
        public InjectModel(IEnumerable<IInjectModellProvider> providers, Type serviceType, Type implementationType, Lifetime lifetime)
        {
            _providers = providers;
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Lifetime = lifetime;
        }

        /// <summary>
        /// 注入模型
        /// </summary>
        public InjectModel(Type serviceType, Type implementationType, Lifetime lifetime)
        {
            _providers = Enumerable.Empty<IInjectModellProvider>();
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Lifetime = lifetime;
        }

        /// <summary>
        /// 服务类型
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// 实现类型
        /// </summary>
        public Type ImplementationType { get; }

        /// <summary>
        /// 生命周期
        /// </summary>
        public Lifetime Lifetime { get; }
    }
}
