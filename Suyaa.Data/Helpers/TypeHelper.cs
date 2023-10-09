using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Suyaa.Data.Attributes;
using Suyaa.Data.Enums;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 类型扩展
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// 获取所有元数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<object> GetMetaDatas(this Type type)
        {
            return type.GetCustomAttributes(false).ToList();
        }

        /// <summary>
        /// 获取表名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTableName(this Type type)
        {
            #region 兼容 DbTable 特性
            var dbTableAttr = type.GetCustomAttribute<DbTableAttribute>();
            if (dbTableAttr != null)
            {
                string name = dbTableAttr.Name;
                if (name.IsNullOrWhiteSpace()) name = type.Name;
                switch (dbTableAttr.Convert)
                {
                    case DbNameConvertTypes.UnderlineLower: return name.ToLowerDbName();
                    case DbNameConvertTypes.UnderlineUpper: return name.ToUpperDbName();
                    default: return name;
                }
            }
            #endregion
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            if (tableAttr is null) return type.Name;
            if (tableAttr.Name.IsNullOrWhiteSpace()) return type.Name;
            return tableAttr.Name;
        }

        /// <summary>
        /// 获取表名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string? GetSchemaName(this Type type)
        {
            #region 兼容 DbTable 特性
            var dbTableAttr = type.GetCustomAttribute<DbTableAttribute>();
            if (dbTableAttr != null)
            {
                string schema = dbTableAttr.Schema;
                if (!schema.IsNullOrWhiteSpace()) return schema;
            }
            #endregion
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            if (tableAttr is null) return null;
            if (tableAttr.Schema.IsNullOrWhiteSpace()) return null;
            return tableAttr.Schema;
        }

    }
}
