using Suyaa.IocContainer.InjectModels;
using Suyaa.IocContainer.Kernel.Dependency;
using Suyaa.IocContainer.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        /// <summary>
        /// Ioc工作范围
        /// </summary>
        /// <param name="iocContainer"></param>
        public IocScope(IocContainer iocContainer)
        {
            IocContainer = iocContainer;
            _scope = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Ioc容器
        /// </summary>
        public IocContainer IocContainer { get; }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        /// <exception cref="IocNotExistsException"></exception>
        internal object CreateInstance(Type serviceType)
        {
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
        internal object CreateInstance(InjectModel model)
        {
            // 处理构造依赖
            List<object> args = new List<object>();
            foreach (var constructorType in model.ConstructorTypes)
            {
                var arg = Resolve(constructorType);
                if (arg is null) throw new IocNotExistsException(constructorType);
                args.Add(arg);
            }
            // 创建对象
            var obj = Activator.CreateInstance(model.ImplementationType, args.ToArray());
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

        /// <summary>
        /// 决定对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public object? Resolve(Type serviceType)
        {
            // 判断是否注册
            var model = IocContainer.Models.Where(d => d.ServiceType == serviceType).FirstOrDefault();
            if (model is null) return null;
            return model.Lifetime switch
            {
                // 单例
                Lifetime.Singleton => IocContainer.GetSingletonInstance(serviceType),
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
