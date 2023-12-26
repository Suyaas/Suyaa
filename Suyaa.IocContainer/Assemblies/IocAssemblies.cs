using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Suyaa.IocContainer.Assemblies
{
    /// <summary>
    /// Ioc程序集集合
    /// </summary>
    public class IocAssemblies
    {
        // 程序集集合
        private readonly List<Assembly> _assemblies;

        /// <summary>
        /// Ioc程序集集合
        /// </summary>
        public IocAssemblies()
        {
            _assemblies = new List<Assembly>();
            AddDomainAssemblies();
        }

        /// <summary>
        /// 添加应用默认程序集
        /// </summary>
        private void AddDomainAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }

        /// <summary>
        /// 添加程序集
        /// </summary>
        /// <param name="assembly"></param>
        public void AddAssembly(Assembly assembly)
        {
            if (_assemblies.Contains(assembly)) return;
            _assemblies.Add(assembly);
        }

        // 查找实现类
        private IEnumerable<Type> FindImplementationTypes(Type serviceType, Assembly assembly)
        {
            try
            {
                List<Type> implementationTypes = new List<Type>();
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    // 跳过接口
                    if (serviceType.IsInterface) continue;
                    if (serviceType.IsAssignableFrom(type)) implementationTypes.Add(type);
                }
                return implementationTypes;
            }
            catch
            {
                return Enumerable.Empty<Type>();
            }
        }

        /// <summary>
        /// 根据服务类型获取所有的实现类型
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<Type> FindImplementationTypes(Type serviceType)
        {
            // 非接口，则直接返回服务类型
            if (!serviceType.IsInterface) return new List<Type> { serviceType };
            List<Type> implementationTypes = new List<Type>();
            foreach (var assembly in _assemblies)
            {
                implementationTypes.AddRange(FindImplementationTypes(serviceType, assembly));
            }
            return implementationTypes;
        }
    }
}
