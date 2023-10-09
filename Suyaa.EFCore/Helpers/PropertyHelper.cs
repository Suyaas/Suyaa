using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Suyaa.Data;
using Suyaa.Data.Attributes;

namespace Suyaa.EFCore.Helpers
{
    /// <summary>
    /// 属性扩展
    /// </summary>
    public static class PropertyHelper
    {
        /// <summary>
        /// 是否拥有自动增长特性
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static bool IsAutoIncrement(this IProperty pro)
        {
            if (pro.PropertyInfo is null) return false;
            return pro.PropertyInfo.GetCustomAttributes<DbAutoIncrementAttribute>().Any();
        }
    }
}
