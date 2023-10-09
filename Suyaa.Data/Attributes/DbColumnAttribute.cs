using System;
using System.Collections.Generic;
using System.Text;
using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;

namespace Suyaa.Data.Attributes
{
    /// <summary>
    /// 数据字段特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DbColumnAttribute : Attribute
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 转换类型
        /// </summary>
        public DbNameConvertTypes Convert { get; set; } = DbNameConvertTypes.None;

        /// <summary>
        /// 数据字段特性
        /// </summary>
        /// <param name="name"></param>
        public DbColumnAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 数据字段特性
        /// </summary>
        public DbColumnAttribute()
        {
            Name = string.Empty;
        }
    }
}
