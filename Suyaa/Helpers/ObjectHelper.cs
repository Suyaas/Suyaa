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
        public static T Fixed<T>(this object? obj) where T : notnull
        {
            if (obj is null) throw new NullException<T>();
            return (T)obj;
        }

        /// <summary>
        /// 获取不为空的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultObj"></param>
        /// <returns></returns>
        public static T Fixed<T>(this object? obj, T defaultObj) where T : notnull
        {
            return (T)(obj ?? defaultObj);
        }

        /// <summary>
        /// 转化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T? ConvertOrNull<T>(this object? obj) where T : class
        {
            if (obj is null) return default;
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
            return typeCode switch
            {
                TypeCode.Boolean => Convert.ToBoolean(obj),
                TypeCode.Byte => Convert.ToByte(obj),
                TypeCode.Int16 => Convert.ToInt16(obj),
                TypeCode.Int32 => Convert.ToInt32(obj),
                TypeCode.Int64 => Convert.ToInt64(obj),
                TypeCode.UInt16 => Convert.ToUInt16(obj),
                TypeCode.UInt32 => Convert.ToUInt32(obj),
                TypeCode.UInt64 => Convert.ToUInt64(obj),
                TypeCode.Single => Convert.ToSingle(obj),
                TypeCode.Double => Convert.ToDouble(obj),
                TypeCode.Char => Convert.ToChar(obj),
                TypeCode.Decimal => Convert.ToDecimal(obj),
                TypeCode.DateTime => Convert.ToDateTime(obj),
                _ => obj,
            };
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

    }
}
