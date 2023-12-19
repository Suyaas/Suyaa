using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Suyaa.IocContainer.Kernel
{
    /// <summary>
    /// 生命周期
    /// </summary>
    [Description("生命周期")]
    public enum Lifetime
    {
        /// <summary>
        /// 瞬态
        /// </summary>
        [Description("瞬态")]
        Transient = 0,
        /// <summary>
        /// 范围
        /// </summary>
        [Description("范围")]
        Scope = 1,
        /// <summary>
        /// 单例
        /// </summary>
        [Description("单例")]
        Singleton = 2,
    }
}
