using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Suyaa.Data.Descriptors
{
    /// <summary>
    /// 表描述
    /// </summary>
    public sealed class ColumnDescriptor : BaseDescriptor
    {
        /// <summary>
        /// 表描述
        /// </summary>
        public ColumnDescriptor(PropertyInfo property) : base(property.GetMetaDatas())
        {
            PropertyInfo = property;
        }

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; }
    }
}
