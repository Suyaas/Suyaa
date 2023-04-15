using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据表特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DbTableAttribute : System.Attribute
    {
        // 名称
        private readonly string _name;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// 架构名称
        /// </summary>
        public string Sechma { get; set; } = string.Empty;

        /// <summary>
        /// 转换类型
        /// </summary>
        public DbNameConvertTypes Convert { get; set; }

        /// <summary>
        /// 数据表特性
        /// </summary>
        /// <param name="name"></param>
        public DbTableAttribute(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 数据表特性
        /// </summary>
        public DbTableAttribute()
        {
            _name = string.Empty;
        }
    }
}
