using Suyaa.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace sy
{
    /// <summary>
    /// 类型处理
    /// </summary>
    public static class Typer
    {

        /// <summary>
        /// 复制填充数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="objSource"></param>
        /// <param name="objTarget">目标对象</param>
        public static void Copy(Type type, object objSource, object objTarget)
        {
            var typeCode = Type.GetTypeCode(type);
            if (typeCode != TypeCode.Object) throw new TypeNotSupportedException(type);
            // 填充属性
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var pro in pros)
            {
                var value = pro.GetValue(objSource);
                if (value is null) continue;
                pro.SetValue(objTarget, Clone(pro.PropertyType, value));
            }
            // 填充字段
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                var value = field.GetValue(objSource);
                if (value is null) continue;
                field.SetValue(objTarget, Clone(field.FieldType, value));
            }
        }

        /// <summary>
        /// 复制填充数据
        /// </summary>
        /// <param name="objSource"></param>
        /// <param name="objTarget">目标对象</param>
        public static void Copy<T>(T objSource, T objTarget)
            where T : notnull
        {
            var type = typeof(T);
            Copy(type, objSource, objTarget);
        }

        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Clone(Type type, object obj)
        {
            var typeCode = Type.GetTypeCode(type);
            switch (typeCode)
            {
                // 常规类型
                case TypeCode.Boolean: return Convert.ToBoolean(obj);
                case TypeCode.Byte: return Convert.ToByte(obj);
                case TypeCode.Int16: return Convert.ToInt16(obj);
                case TypeCode.Int32: return Convert.ToInt32(obj);
                case TypeCode.Int64: return Convert.ToInt64(obj);
                case TypeCode.UInt16: return Convert.ToUInt16(obj);
                case TypeCode.UInt32: return Convert.ToUInt32(obj);
                case TypeCode.UInt64: return Convert.ToUInt64(obj);
                case TypeCode.Single: return Convert.ToSingle(obj);
                case TypeCode.Double: return Convert.ToDouble(obj);
                case TypeCode.Char: return Convert.ToChar(obj);
                case TypeCode.Decimal: return Convert.ToDecimal(obj);
                case TypeCode.DateTime: return Convert.ToDateTime(obj);
                case TypeCode.String: return Convert.ToString(obj);
                case TypeCode.Object:
                    // 创建新对象
                    var objNew = sy.Assembly.Create(type) ?? throw new NullException(type);
                    // 复制所有的属性
                    var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var pro in pros)
                    {
                        var value = pro.GetValue(obj);
                        if (value is null) continue;
                        pro.SetValue(objNew, Clone(pro.PropertyType, value));
                    }
                    // 复制所有的字段
                    var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var field in fields)
                    {
                        var value = field.GetValue(obj);
                        if (value is null) continue;
                        field.SetValue(objNew, Clone(field.FieldType, value));
                    }
                    return objNew;
                default: throw new TypeNotSupportedException(type);
            }
        }

        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Clone<T>(T obj) where T : notnull
            => (T)Clone(typeof(T), obj);
    }
}
