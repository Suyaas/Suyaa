using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据表特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DbColumnAttribute : System.Attribute
    {
        // 名称
        private readonly string _name;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// 转换类型
        /// </summary>
        public DbNameConvertTypes Convert { get; set; }

        /// <summary>
        /// 数据表特性
        /// </summary>
        /// <param name="name"></param>
        public DbColumnAttribute(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 数据表特性
        /// </summary>
        public DbColumnAttribute()
        {
            _name = string.Empty;
        }
    }
}
