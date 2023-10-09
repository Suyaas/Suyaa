using Suyaa.Data.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库工厂
    /// </summary>
    public interface IDatabaseFactory
    {
        /// <summary>
        /// 实例描述集合
        /// </summary>
        IEnumerable<EntityDescriptor> Entities { get; }

        /// <summary>
        /// 数据库集合
        /// </summary>
        IDictionary<string, IDatabase> Databases { get; }
    }
}
