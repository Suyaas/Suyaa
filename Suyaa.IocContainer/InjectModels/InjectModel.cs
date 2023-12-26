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
        // 字符串类型
        private static readonly Type _stringType = typeof(string);
        // 供应商集合
        private readonly IEnumerable<IInjectModellProvider> _providers;
        // 构建依赖
        private readonly List<Type> _constructorTypes;
        // 属性依赖
        private readonly List<PropertyInfo> _properties;

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
            _properties = GetProperties();
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
            _properties = GetProperties();
        }

        // 获取构造函数依赖
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

        // 获取属性依赖
        private List<PropertyInfo> GetProperties()
        {
            return ImplementationType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(d => d.CanWrite && !d.PropertyType.IsValueType && d.PropertyType != _stringType).ToList();
        }

        /// <summary>
        /// 构造依赖类型集合
        /// </summary>
        public IEnumerable<Type> ConstructorTypes => _constructorTypes;

        /// <summary>
        /// 属性依赖集合
        /// </summary>
        public IEnumerable<PropertyInfo> Properties => _properties;

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
