using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text;
using Suyaa.IocContainer.Kernel.Dependency;
using Suyaa.Usables;

namespace Suyaa.IocContainer.Kernel.Helpers
{
    /// <summary>
    /// 轻量化容器助手
    /// </summary>
    public static class ToyContainerHelper
    {
        /// <summary>
        /// 获取程序执行文件路径
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public static IIocContainer GetOrCreateContainer(this Toy<IIocContainer> use)
        {
            var container = Use<IIocContainer>.SingleOrDefault();
            if (container is null)
            {
                container = Ioc.UseContainer<IocContainer>();
            }
            return container;
        }
    }
}
