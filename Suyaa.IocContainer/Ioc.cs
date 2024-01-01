using Suyaa.IocContainer.Assemblies;
using Suyaa.IocContainer.Kernel;
using Suyaa.IocContainer.Kernel.Dependency;
using Suyaa.IocContainer.Kernel.Exceptions;
using Suyaa.IocContainer.Kernel.Helpers;
using System;
using System.Linq;
using System.Reflection;

namespace Suyaa.IocContainer
{
    /// <summary>
    /// 控制反转
    /// </summary>
    public struct Ioc
    {
        /// <summary>
        /// 使用全局容器
        /// </summary>
        /// <typeparam name="TContainer"></typeparam>
        /// <returns></returns>
        public static IIocContainer UseContainer<TContainer>()
            where TContainer : class, IIocContainer
        {
            // 获取静态容器
            var container = Use<IIocContainer>.SingleOrDefault();
            if (container is null)
            {
                // 创建静态容器
                container = Use<TContainer>.Create();
                Use<IIocContainer>.Single(container);
            }
            // 返回静态容器
            return container;
        }

        /// <summary>
        /// 使用全局容器
        /// </summary>
        /// <typeparam name="TContainer"></typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        public static IIocContainer UseContainer<TContainer>(TContainer container)
            where TContainer : class, IIocContainer
        {
            Use<IIocContainer>.Single(container);
            return container;
        }

        /// <summary>
        /// 引入程序集
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IocAssemblies Include(Assembly assembly)
        {
            var iocAssemblies = Use<IocAssemblies>.Single();
            iocAssemblies.AddAssembly(assembly);
            return iocAssemblies;
        }

        /// <summary>
        /// 程序集
        /// </summary>
        public static IocAssemblies Assemblies => Use<IocAssemblies>.Single();

        /// <summary>
        /// 获取当前容器
        /// </summary>
        public static IIocContainer Container => Use<IIocContainer>.Toy.GetOrCreateContainer();

    }
    /// <summary>
    /// 控制反转
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Ioc<TService> where TService : class
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="lifetime"></param>
        public static void Register(Lifetime lifetime)
        {
            var type = typeof(TService);
            if (Ioc.Container.Models.Where(d => d.ServiceType == type && d.ImplementationType == type).Any()) return;
            Ioc.Container.Add(type, type, lifetime);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="lifetime"></param>
        public static void Register(Type ImplementationType, Lifetime lifetime)
        {
            var serviceType = typeof(TService);
            if (Ioc.Container.Models.Where(d => d.ServiceType == serviceType && d.ImplementationType == ImplementationType).Any()) return;
            Ioc.Container.Add(serviceType, ImplementationType, lifetime);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="lifetime"></param>
        public static void Register<TImplementation>(Lifetime lifetime)
        {
            Register(typeof(TImplementation), lifetime);
        }

        /// <summary>
        /// 批量注册
        /// </summary>
        /// <param name="lifetime"></param>
        public static void Registers(Lifetime lifetime)
        {
            var serviceType = typeof(TService);
            var implementationTypes = Ioc.Assemblies.FindImplementationTypes(serviceType);
            // 注册所有的服务实现
            foreach (var implementationType in implementationTypes)
            {
                if (Ioc.Container.Models.Where(d => d.ServiceType == serviceType && d.ImplementationType == implementationType).Any()) continue;
                Ioc.Container.Add(serviceType, implementationType, lifetime);
            }
            // 注册所有的单实现
            foreach (var implementationType in implementationTypes)
            {
                if (Ioc.Container.Models.Where(d => d.ServiceType == implementationType && d.ImplementationType == implementationType).Any()) continue;
                Ioc.Container.Add(implementationType, implementationType, lifetime);
            }
        }

        /// <summary>
        /// 添加单例
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public static void AddSingleton(object instance)
        {
            Ioc.Container.AddSingleton(typeof(TService), instance);
        }

        /// <summary>
        /// 移除
        /// </summary>
        public static void Remove(Type implementationType)
        {
            var serviceType = typeof(TService);
            Ioc.Container.Remove(serviceType, implementationType);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="lifetime"></param>
        public static void Remove<TImplementation>()
        {
            Remove(typeof(TImplementation));
        }

        /// <summary>
        /// 移除
        /// </summary>
        public static void Remove()
        {
            var serviceType = typeof(TService);
            var models = Ioc.Container.Models.Where(d => d.ServiceType == serviceType).ToList();
            foreach (var model in models)
            {
                Ioc.Container.Remove(serviceType, model.ImplementationType);
            }
        }

        /// <summary>
        /// 决议对象
        /// </summary>
        /// <returns></returns>
        public static TService? Resolve()
        {
            var serviceType = typeof(TService);
            return (TService?)Ioc.Container.Resolve(serviceType);
        }

        /// <summary>
        /// 决议对象
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IocNotExistsException{TService}"></exception>
        public static TService ResolveRequired()
        {
            var obj = Resolve();
            if (obj is null) throw new IocNotExistsException<TService>();
            return obj;
        }
    }
}
