using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.IocContainer.InjectModels.Dependency
{
    /// <summary>
    /// 依赖建模工厂
    /// </summary>
    public interface IInjectModellFactory
    {
        /// <summary>
        /// 建模集合
        /// </summary>
        IEnumerable<InjectModel> GetModels();
    }
}
