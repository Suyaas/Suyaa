using Suyaa.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Suyaa
{

    /// <summary>
    /// 对象扩展类
    /// </summary>
    public static class ObjectHelper
    {

        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object? obj)
        {
            return obj is null;
        }

        /// <summary>
        /// 将可空对象转化为非空对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Fixed<T>(this T? obj) where T : class
        {
            if (obj is null) throw new NullException<T>();
            return obj;
        }

        /// <summary>
        /// 获取不为空的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultObj"></param>
        /// <returns></returns>
        public static T ToNotNull<T>(this object? obj, T defaultObj) where T : notnull
        {
            return (T)(obj ?? defaultObj);
        }

        /// <summary>
        /// 获取不为空的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Obsolete("请使用Fixed函数进行可空对象强制转化为不可空对象")]
        public static T ToNotNull<T>(this object? obj)
        {
            if (obj is null) throw new NullException();
            return (T)obj;
        }

        /// <summary>
        /// 转化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T? ConvertOrNull<T>(this object? obj) where T : class
        {
            if (obj is null) return default(T);
            return obj.ConvertTo<T>();
        }

        /// <summary>
        /// 转化对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ConvertTo(this object obj, Type type)
        {
            var typeCode = Type.GetTypeCode(type);
            switch (typeCode)
            {
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
                default: return obj;
            }
        }

        /// <summary>
        /// 转化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(this object obj)
        {
            return (T)obj.ConvertTo(typeof(T));
        }

        /// <summary>
        /// 数据填充
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objSource"></param>
        public static void Fill(this object obj, object objSource)
        {
            var type = obj.GetType();
            var typeCode = Type.GetTypeCode(type);
            if (typeCode != TypeCode.Object) throw new TypeNotSupportedException(type);
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var pro in pros)
            {
                var value = pro.GetValue(obj);
                if (value is null) continue;
                pro.SetValue(obj, value.Clone(pro.PropertyType));
            }
        }

        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Clone(this object obj, Type type)
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
                    var objNew = sy.Assembly.Create(type).Fixed();
                    var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var pro in pros)
                    {
                        var value = pro.GetValue(obj);
                        if (value is null) continue;
                        pro.SetValue(objNew, value.Clone(pro.PropertyType));
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
        public static T Clone<T>(this T obj) where T : notnull
            => (T)obj.Clone(typeof(T));

    }
}
