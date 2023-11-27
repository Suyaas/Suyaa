using Suyaa.Data.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Suyaa.Data.PostgreSQL.Helpers
{
    /// <summary>
    /// 属性扩展
    /// </summary>
    public static class PropertyInfoHelper
    {
        /// <summary>
        /// 获取列属性类型
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static string GetColumnAttributeType(this PropertyInfo pro)
        {
            var columnAttr = pro.GetCustomAttribute<ColumnAttribute>();
            var stringLengthAttr = pro.GetCustomAttribute<StringLengthAttribute>();
            if (columnAttr != null)
                if (!columnAttr.TypeName.IsNullOrWhiteSpace())
                    return columnAttr.TypeName ?? "";
            var proType = pro.PropertyType;
            var proTypeCode = Type.GetTypeCode(proType);
            switch (proTypeCode)
            {
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                    if (pro.IsAutoIncrement()) return "samllserial";
                    return "smallint";
                case TypeCode.Int32:
                case TypeCode.UInt32:
                    if (pro.IsAutoIncrement()) return "serial";
                    return "integer";
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    if (pro.IsAutoIncrement()) return "bigserial";
                    return "bigint";
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return "decimal";
                case TypeCode.String:
                    if (stringLengthAttr is null) return "varchar";
                    return $"varchar({stringLengthAttr.MaximumLength})";
                case TypeCode.Boolean:
                    return "bool";
                case TypeCode.DateTime:
                    return "timestamp without time zone";
                default:
                    throw new DbException($"不支持的数据格式'{proTypeCode}'");
            }
        }
    }
}
