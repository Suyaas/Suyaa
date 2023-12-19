using Suyaa.IocContainer.Kernel;
using Suyaa.IocContainer.Kernel.Dependency;
using Suyaa.IocContainer.Kernel.Helpers;
using System;

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

        }

        /// <summary>
        /// 引入
        /// </summary>
        /// <param name="lifetime"></param>
        public static void Include(Type serviceType, Lifetime lifetime)
        {

        }

        /// <summary>
        /// 排除
        /// </summary>
        public static void Exclude()
        {

        }
    }
}
