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
    public struct Ioc<T> where T : class
    {
        /// <summary>
        /// 引入
        /// </summary>
        /// <param name="lifetime"></param>
        public static void Include(Lifetime lifetime)
        {
            var type = typeof(T);
            if (Ioc.Container.Models.Where(d => d.ServiceType == type && d.ImplementationType == type).Any()) return;
            Ioc.Container.Add(type, type, lifetime);
        }

        /// <summary>
        /// 引入
        /// </summary>
        /// <param name="lifetime"></param>
        public static void Include(Type ImplementationType, Lifetime lifetime)
        {
            var serviceType = typeof(T);
            if (Ioc.Container.Models.Where(d => d.ServiceType == serviceType && d.ImplementationType == ImplementationType).Any()) return;
            Ioc.Container.Add(serviceType, ImplementationType, lifetime);
        }

        /// <summary>
        /// 排除
        /// </summary>
        public static void Exclude(Type implementationType)
        {
            var serviceType = typeof(T);
            Ioc.Container.Remove(serviceType, implementationType);
        }

        /// <summary>
        /// 排除
        /// </summary>
        public static void Exclude()
        {
            var serviceType = typeof(T);
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
        public static T? Resolve()
        {
            var serviceType = typeof(T);
            return (T?)Ioc.Container.Resolve(serviceType);
        }

        /// <summary>
        /// 决议对象
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IocNotExistsException{T}"></exception>
        public static T ResolveRequired()
        {
            var obj = Resolve();
            if (obj is null) throw new IocNotExistsException<T>();
            return obj;
        }
    }
}
