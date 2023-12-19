using Suyaa.IocContainer.InjectModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.IocContainer.Kernel.Dependency
{
    /// <summary>
    /// IoC容器
    /// </summary>
    public interface IIocContainer
    {
        /// <summary>
        /// 建模集合
        /// </summary>
        IEnumerable<InjectModel> Models { get; }
    }
}
