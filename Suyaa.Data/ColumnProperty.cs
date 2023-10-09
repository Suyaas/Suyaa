using Suyaa;
using Suyaa.Data.Dependency;
using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace Suyaa.Data
{
    /// <summary>
    /// 字段属性
    /// </summary>
    public class ColumnProperty
    {
        // 数据库供应商
        private readonly IDatabaseProvider _provider;

        /// <summary>
        /// 变量名称
        /// </summary>
        public string VarName { get; }

        /// <summary>
        /// 列名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 是否更新
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// 获取是否为数字
        /// </summary>
        public bool IsNumeric { get; }

        /// <summary>
        /// 获取Sql中的值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetSqlValue(object obj)
        {
            var value = this.PropertyInfo.GetValue(obj);
            if (value is null) return "NULL";
            if (IsNumeric) return value.ToString().ToUpper();
            return _provider.GetValueString(value.ToString());
        }

        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTypeName(Type type)
        {
            string name = type.Namespace + "." + type.Name;
            // 兼容可空类型
            if (name == "System.Nullable`1") return GetTypeName(type.GenericTypeArguments[0]);
            return name;
        }

        /// <summary>
        /// 更新器属性
        /// </summary>
        public ColumnProperty(IDatabaseProvider provider, PropertyInfo property)
        {
            _provider = provider;
            this.PropertyInfo = property;
            this.VarName = property.Name;
            this.ColumnName = property.GetColumnName();
            //var column = property.GetCustomAttribute<ColumnAttribute>();
            //if (column != null)
            //{
            //    if (!string.IsNullOrWhiteSpace(column.Name)) this.ColumnName = column.Name;
            //}
            this.IsModified = false;
            this.IsNumeric = property.PropertyType.IsNumeric();
            string typeFullName = GetTypeName(property.PropertyType);
        }
    }
}
