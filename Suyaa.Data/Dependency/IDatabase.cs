using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        DbTypes Type { get; }

        /// <summary>
        /// 供应类名称
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// 转化为连接字符串
        /// </summary>
        /// <returns></returns>
        string ToConnectionString();
    }
}
