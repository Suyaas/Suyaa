using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Attributes
{
    /// <summary>
    /// 索引字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DbIndexAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 唯一性
        /// </summary>
        public bool Unique { get; set; } = false;

        /// <summary>
        /// 索引字段
        /// </summary>
        public DbIndexAttribute() { Name = string.Empty; }

        /// <summary>
        /// 索引字段
        /// </summary>
        public DbIndexAttribute(string name) { Name = name; }
    }
}
