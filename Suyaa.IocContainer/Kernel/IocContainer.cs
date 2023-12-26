using Suyaa.IocContainer.InjectModels;
using Suyaa.IocContainer.InjectModels.Dependency;
using Suyaa.IocContainer.Kernel.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.IocContainer.Kernel
{
    /// <summary>
    /// Ioc容器
    /// </summary>
    public sealed class IocContainer : Disposable, IIocContainer
    {
        // 建模集合
        private readonly InjectModellFactory _injectModellFactory;
        // 所有的单例对象
        private readonly Dictionary<Type, object> _singleton;
        // 默认工作域
        private readonly IocScope _iocScope;

        /// <summary>
        /// Ioc容器
        /// </summary>
        public IocContainer()
        {
            _injectModellFactory = new InjectModellFactory();
            _singleton = new Dictionary<Type, object>();
            _iocScope = new IocScope(this);
        }

        /// <summary>
        /// 建模集合
        /// </summary>
        public IEnumerable<InjectModel> Models => _injectModellFactory.GetModels();

        /// <summary>
        /// 获取单例对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal object GetSingletonInstance(Type serviceType)
        {
            // 单例集合中有，则返回单例对象
            if (_singleton.ContainsKey(serviceType)) return _singleton[serviceType];
            // 单例集合中没有，则从默认工作域中先创建一个对象，并添加到单例集合中
            var obj = _iocScope.CreateInstance(serviceType);
            _singleton[serviceType] = obj;
            return obj;
        }

        /// <summary>
        /// 添加单例对象
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetime"></param>
        public void AddSingleton(Type serviceType, object instance)
        {
            var implementationType = instance.GetType();
            if (!_injectModellFactory.GetModels().Where(d => d.ServiceType == serviceType && d.ImplementationType == implementationType).Any())
            {
                _injectModellFactory.Add(serviceType, implementationType, Lifetime.Singleton);
            }
            _singleton[serviceType] = instance;
        }

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
        public void Remove(Type serviceType, Type implementationType)
        {
            _injectModellFactory.Remove(serviceType, implementationType);
        }

        /// <summary>
        /// 创建工作域
        /// </summary>
        /// <returns></returns>
        public IocScope CreateScope()
        {
            return new IocScope(this);
        }

        /// <summary>
        /// 决定对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public object? Resolve(Type serviceType)
        {
            return _iocScope.Resolve(serviceType);
        }

        #region 释放对象
        /// <summary>
        /// 释放托管对象
        /// </summary>
        protected override void OnManagedDispose()
        {
            base.OnManagedDispose();
            _singleton.Clear();
            _iocScope.Dispose();
        }
        #endregion
    }
}
