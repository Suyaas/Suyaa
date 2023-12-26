using Suyaa.IocContainer.InjectModels.Dependency;
using Suyaa.IocContainer.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.IocContainer.InjectModels
{
    /// <summary>
    /// 依赖建模工厂
    /// </summary>
    public sealed class InjectModellFactory : IInjectModellFactory
    {
        private readonly IEnumerable<IInjectModellProvider> _providers;
        private readonly List<InjectModel> _models;
        private static object _locker = new object();

        /// <summary>
        /// 依赖建模工厂
        /// </summary>
        public InjectModellFactory(IEnumerable<IInjectModellProvider> providers)
        {
            _providers = providers;
            _models = new List<InjectModel>();
        }

        /// <summary>
        /// 依赖建模工厂
        /// </summary>
        public InjectModellFactory()
        {
            _providers = Enumerable.Empty<IInjectModellProvider>();
            _models = new List<InjectModel>();
        }

        /// <summary>
        /// 获取建模集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<InjectModel> GetModels() => _models;

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetime"></param>
        public void Add(Type serviceType, Type implementationType, Lifetime lifetime)
        {
            lock (_locker)
            {
                _models.Add(new InjectModel(_providers, serviceType, implementationType, lifetime));
            }
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="type"></param>
        public void Remove(Type serviceType, Type implementationType)
        {
            lock (_locker)
            {
                for (int i = _models.Count - 1; i >= 0; i++)
                {
                    var model = _models[i];
                    if (model.ServiceType == serviceType && model.ImplementationType == implementationType)
                    {
                        _models.RemoveAt(i);
                    }
                }
            }
        }
    }
}
