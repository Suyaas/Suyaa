using Suyaa.IocContainer.Kernel.Dependency;
using Suyaa.IocContainer.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.IocContainer.Kernel.Helpers
{
    /// <summary>
    /// Ioc容器助手
    /// </summary>
    public static class IocContainerHelper
    {
        /// <summary>
        /// 批量注册
        /// </summary>
        /// <param name="iocContainer"></param>
        /// <param name="serviceType"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IIocContainer Registers(this IIocContainer iocContainer, Type serviceType, Lifetime lifetime)
        {
            var implementationTypes = Ioc.Assemblies.FindImplementationTypes(serviceType);
            // 注册所有的服务实现
            foreach (var implementationType in implementationTypes)
            {
                if (Ioc.Container.Models.Where(d => d.ServiceType == serviceType && d.ImplementationType == implementationType).Any()) continue;
                iocContainer.Add(serviceType, implementationType, lifetime);
            }
            // 注册所有的单实现
            foreach (var implementationType in implementationTypes)
            {
                if (Ioc.Container.Models.Where(d => d.ServiceType == implementationType && d.ImplementationType == implementationType).Any()) continue;
                iocContainer.Add(implementationType, implementationType, lifetime);
            }
            return iocContainer;
        }

        /// <summary>
        /// 批量注册
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="iocContainer"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IIocContainer Registers<TService>(this IIocContainer iocContainer, Lifetime lifetime)
        {
            var serviceType = typeof(TService);
            return iocContainer.Registers(serviceType, lifetime);
        }

        /// <summary>
        /// 批量注册
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="iocContainer"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IIocContainer Register<TService, TImplementation>(this IIocContainer iocContainer, Lifetime lifetime)
        {
            iocContainer.Add(typeof(TService), typeof(TImplementation), lifetime);
            return iocContainer;
        }

        /// <summary>
        /// 决议对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public static object ResolveRequired(this IIocContainer iocContainer, Type serviceType)
        {
            var obj = iocContainer.Resolve(serviceType);
            if (obj is null) throw new IocNotExistsException(serviceType);
            return obj;
        }

        /// <summary>
        /// 决议对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public static TService? Resolve<TService>(this IIocContainer iocContainer)
            where TService : class
        {
            return (TService?)iocContainer.Resolve(typeof(TService));
        }

        /// <summary>
        /// 决议对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public static TService ResolveRequired<TService>(this IIocContainer iocContainer)
            where TService : class
        {
            return (TService)iocContainer.ResolveRequired(typeof(TService));
        }

        /// <summary>
        /// 决议对象集合
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public static IEnumerable<TService> Resolves<TService>(this IIocContainer iocContainer)
            where TService : class
        {
            var serviceType = typeof(TService);
            var models = iocContainer.Models.Where(d => d.ServiceType == serviceType).ToList();
            List<TService> instances = new List<TService>();
            foreach (var model in models)
            {
                instances.Add((TService)iocContainer.ResolveRequired(model.ImplementationType));
            }
            return instances;
        }
    }
}
