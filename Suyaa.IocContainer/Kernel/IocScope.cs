using Suyaa.IocContainer.InjectModels;
using Suyaa.IocContainer.InjectModels.Dependency;
using Suyaa.IocContainer.Kernel.Dependency;
using Suyaa.IocContainer.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Suyaa.IocContainer.Kernel
{
    /// <summary>
    /// Ioc工作范围
    /// </summary>
    public sealed class IocScope : Disposable, IIocScope
    {
        // 所有的工作域对象
        private readonly Dictionary<Type, object> _scope;
        private readonly IocContainer _iocContainer;

        /// <summary>
        /// Ioc工作范围
        /// </summary>
        /// <param name="iocContainer"></param>
        public IocScope(IocContainer iocContainer)
        {
            _scope = new Dictionary<Type, object>();
            _iocContainer = iocContainer;
        }

        /// <summary>
        /// Ioc容器
        /// </summary>
        public IIocContainer IocContainer => _iocContainer;

        // 获取构造函数参数集合
        private List<object> GetConstructorParameters(InjectModel model)
        {
            // 处理构造依赖
            List<object> parameters = new List<object>();
            foreach (var constructorType in model.ConstructorTypes)
            {
                var arg = Resolve(constructorType);
                if (arg is null) throw new IocNotExistsException(constructorType);
                parameters.Add(arg);
            }
            return parameters;
        }

        // 设置属性依赖
        private void SetProperties(object obj, IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                var value = Resolve(property.PropertyType);
                if (value is null) continue;
                property.SetValue(obj, value);
            }
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        /// <exception cref="IocNotExistsException"></exception>
        internal object CreateInstance(Type serviceType)
        {
            // 判断是否为泛型
            if (serviceType.IsGenericType) return CreateGenericInstance(serviceType);
            // 判断是否注册
            var model = IocContainer.Models.Where(d => d.ServiceType == serviceType).FirstOrDefault();
            if (model is null) throw new IocNotExistsException(serviceType);
            return CreateInstance(model);
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        /// <exception cref="IocNotExistsException"></exception>
        internal object CreateGenericInstance(Type serviceType)
        {
            // 获取泛型定义对象
            var serviceGenericType = serviceType.GetGenericTypeDefinition();
            // 判断是否注册
            var model = IocContainer.Models.Where(d => d.ServiceType == serviceGenericType).FirstOrDefault();
            if (model is null) throw new IocNotExistsException(serviceGenericType);
            return CreateGenericInstance(model, serviceType.GenericTypeArguments);
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        /// <exception cref="IocNotExistsException"></exception>
        internal object CreateGenericInstance(InjectModel model, IEnumerable<Type> genericTypes)
        {
            // 获取依赖参数对象集合
            var parameters = GetConstructorParameters(model);
            // 创建对象
            var genericType = model.ImplementationType.MakeGenericType(genericTypes.ToArray());
            var obj = Activator.CreateInstance(genericType, parameters.ToArray());
            // 设置属性
            SetProperties(obj, model.Properties);
            return obj;
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        /// <exception cref="IocNotExistsException"></exception>
        internal object CreateInstance(InjectModel model)
        {
            // 获取依赖参数对象集合
            var parameters = GetConstructorParameters(model);
            // 创建对象
            var obj = Activator.CreateInstance(model.ImplementationType, parameters.ToArray());
            // 设置属性
            SetProperties(obj, model.Properties);
            return obj;
        }

        /// <summary>
        /// 获取单例对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object GetScopeInstance(Type serviceType)
        {
            // 工作域集合中有，则返回工作域对象
            if (_scope.ContainsKey(serviceType)) return _scope[serviceType];
            // 工作域集合中没有，则中先创建一个对象，并添加到工作域集合中
            var obj = CreateInstance(serviceType);
            _scope[serviceType] = obj;
            return obj;
        }

        // 决议泛型对象
        private object? ResolveGeneric(Type serviceType)
        {
            // 获取泛型定义对象
            var serviceGenericType = serviceType.GetGenericTypeDefinition();
            // 判断是否注册
            var model = IocContainer.Models.Where(d => d.ServiceType == serviceGenericType).FirstOrDefault();
            if (model is null) return null;
            return model.Lifetime switch
            {
                // 单例
                Lifetime.Singleton => _iocContainer.GetSingletonInstance(serviceType),
                // 工作域
                Lifetime.Scope => GetScopeInstance(serviceType),
                // 瞬态
                _ => CreateGenericInstance(model, serviceType.GenericTypeArguments),
            };
        }

        /// <summary>
        /// 添加单例对象
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetime"></param>
        public void AddScope(Type serviceType, object instance)
        {
            var implementationType = instance.GetType();
            if (!IocContainer.Models.Where(d => d.ServiceType == serviceType && d.ImplementationType == implementationType).Any())
            {
                IocContainer.Add(serviceType, implementationType, Lifetime.Scope);
            }
            _scope[serviceType] = instance;
        }

        /// <summary>
        /// 决定对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public object? Resolve(Type serviceType)
        {
            // 判断是否为泛型
            if (serviceType.IsGenericType) return ResolveGeneric(serviceType);
            // 判断是否注册
            var model = IocContainer.Models.Where(d => d.ServiceType == serviceType).FirstOrDefault();
            if (model is null) return null;
            return model.Lifetime switch
            {
                // 单例
                Lifetime.Singleton => _iocContainer.GetSingletonInstance(serviceType),
                // 工作域
                Lifetime.Scope => GetScopeInstance(serviceType),
                // 瞬态
                _ => CreateInstance(model),
            };
        }

        #region 释放对象
        /// <summary>
        /// 释放托管对象
        /// </summary>
        protected override void OnManagedDispose()
        {
            base.OnManagedDispose();
            _scope.Clear();
        }
        #endregion
    }
}
