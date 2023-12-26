using Suyaa.IocContainer.InjectModels.Dependency;
using Suyaa.IocContainer.Kernel;
using Suyaa.IocContainer.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Suyaa.IocContainer.InjectModels
{
    /// <summary>
    /// 注入模型
    /// </summary>
    public sealed class InjectModel
    {
        // 供应商集合
        private readonly IEnumerable<IInjectModellProvider> _providers;
        // 构建依赖
        private readonly List<Type> _constructorTypes;

        /// <summary>
        /// 注入模型
        /// </summary>
        public InjectModel(IEnumerable<IInjectModellProvider> providers, Type serviceType, Type implementationType, Lifetime lifetime)
        {
            _providers = providers;
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Lifetime = lifetime;
            _constructorTypes = GetConstructorTypes();
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
            _constructorTypes = GetConstructorTypes();
        }

        /// <summary>
        /// 构建依赖
        /// </summary>
        /// <returns></returns>
        private List<Type> GetConstructorTypes()
        {
            var constructors = ImplementationType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (constructors.Length > 1) throw new IocConstructorException(ImplementationType);
            var constructor = constructors.First()!;
            var parameters = constructor.GetParameters();
            List<Type> types = new List<Type>();
            foreach (var parameter in parameters)
            {
                types.Add(parameter.ParameterType);
            }
            return types;
        }

        /// <summary>
        /// 构造依赖类型集合
        /// </summary>
        public IEnumerable<Type> ConstructorTypes => _constructorTypes;

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
