using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure
{
    /// <summary>
    /// 配置管理器
    /// </summary>
    public interface IConfigManager<T> where T : IConfig, new()
    {
        /// <summary>
        /// 配置项
        /// </summary>
        T Config { get; }

        /// <summary>
        /// 保存
        /// </summary>
        void Save();
    }
}
