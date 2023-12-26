using Suyaa.IocContainer.Kernel.Dependency;
using Suyaa.IocContainer.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.IocContainer.Kernel.Helpers
{
    /// <summary>
    /// Ioc工作域助手
    /// </summary>
    public static class IocScopeHelper
    {

        /// <summary>
        /// 决议对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public static object ResolveRequired(this IIocScope iocScope, Type serviceType)
        {
            var obj = iocScope.Resolve(serviceType);
            if (obj is null) throw new IocNotExistsException(serviceType);
            return obj;
        }

        /// <summary>
        /// 决议对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public static TService? Resolve<TService>(this IIocScope iocScope)
            where TService : class
        {
            return (TService?)iocScope.Resolve(typeof(TService));
        }

        /// <summary>
        /// 决议对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public static TService ResolveRequired<TService>(this IIocScope iocScope)
            where TService : class
        {
            return (TService)iocScope.ResolveRequired(typeof(TService));
        }

        /// <summary>
        /// 决议对象集合
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public static IEnumerable<TService> Resolves<TService>(this IIocScope iocScope)
            where TService : class
        {
            var serviceType = typeof(TService);
            var models = iocScope.IocContainer.Models.Where(d => d.ServiceType == serviceType).ToList();
            List<TService> instances = new List<TService>();
            foreach (var model in models)
            {
                instances.Add((TService)iocScope.ResolveRequired(model.ImplementationType));
            }
            return instances;
        }
    }
}
