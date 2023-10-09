using System;
using System.Collections.Generic;
using System.Text;
using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;

namespace Suyaa.Data.Attributes
{
    /// <summary>
    /// 数据表特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DbTableAttribute : Attribute
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 架构名称
        /// </summary>
        public string Schema { get; set; } = string.Empty;

        /// <summary>
        /// 转换类型
        /// </summary>
        public DbNameConvertTypes Convert { get; set; } = DbNameConvertTypes.None;

        /// <summary>
        /// 数据表特性
        /// </summary>
        /// <param name="name"></param>
        public DbTableAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 数据表特性
        /// </summary>
        public DbTableAttribute()
        {
            Name = string.Empty;
        }
    }
}
