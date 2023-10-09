using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Suyaa.Data.Enums
{
    /// <summary>
    /// 名称转换类型
    /// </summary>
    [Description("名称转换类型")]
    public enum DbNameConvertTypes : int
    {
        /// <summary>
        /// 不转换
        /// </summary>
        [Description("不转换")]
        None = 0,
        /// <summary>
        /// 下划线小写转换
        /// </summary>
        [Description("下划线小写转换")]
        UnderlineLower = 0x10,
        /// <summary>
        /// 下划线大写转换
        /// </summary>
        [Description("下划线大写转换")]
        UnderlineUpper = 0x20,
    }
}
