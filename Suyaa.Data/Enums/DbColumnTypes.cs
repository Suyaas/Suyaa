using Suyaa.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Suyaa.Data.Enums
{

    /// <summary>
    /// 数据字段类型
    /// </summary>
    [Description("数据字段类型")]
    public enum DbColumnTypes : int
    {
        /// <summary>
        /// 未知类型
        /// </summary>
        [Description("未知类型")]
        Unknow = 0,

        #region 字符串
        /// <summary>
        /// 变长文本，需指定长度
        /// </summary>
        [Description("变长文本")]
        Text = 0x10,
        /// <summary>
        /// 变长字符串，需指定长度
        /// </summary>
        [Description("变长字符串")]
        [DbNeedSize]
        Varchar = 0x11,
        /// <summary>
        /// 定长字符串
        /// </summary>
        [Description("定长字符串")]
        [DbNeedSize]
        Char = 0x12,
        #endregion

        #region 整型
        /// <summary>
        /// 布尔/位类型
        /// </summary>
        [Description("布尔/位类型")]
        Bool = 0x20,
        /// <summary>
        /// 极小整型/字节型
        /// </summary>
        [Description("极小整型/字节型")]
        TinyInt = 0x21,
        /// <summary>
        /// 短整型
        /// </summary>
        [Description("短整型")]
        SmallInt = 0x22,
        /// <summary>
        /// 整型
        /// </summary>
        [Description("整型")]
        Int = 0x23,
        /// <summary>
        /// 长整型
        /// </summary>
        [Description("长整型")]
        BigInt = 0x24,
        #endregion

        #region 浮点型
        /// <summary>
        /// 单精度类型
        /// </summary>
        [Description("单精度类型")]
        Single = 0x30,
        /// <summary>
        /// 双精度类型
        /// </summary>
        [Description("双精度类型")]
        Double = 0x31,
        /// <summary>
        /// 十进制浮点型，需指定长度和精度
        /// </summary>
        [Description("十进制浮点型")]
        [DbNeedSize]
        Decimal = 0x32,
        #endregion

        #region 日期类型
        /// <summary>
        /// 日期类型
        /// </summary>
        [Description("日期类型")]
        Datetime = 0x40,
        /// <summary>
        /// 时间戳类型
        /// </summary>
        [Description("时间戳类型")]
        Timestamp = 0x41,
        #endregion

        #region 二进制类型
        /// <summary>
        /// 定长二进制类型，需指定长度
        /// </summary>
        [Description("定长二进制类型")]
        [DbNeedSize]
        Binary = 0x50,
        /// <summary>
        /// 变长二进制类型，需指定长度
        /// </summary>
        [Description("变长二进制类型")]
        [DbNeedSize]
        Vbinary = 0x51,
        /// <summary>
        /// 变长二进制数据
        /// </summary>
        [Description("变长二进制数据")]
        Data = 0x52,
        #endregion
    }
}
