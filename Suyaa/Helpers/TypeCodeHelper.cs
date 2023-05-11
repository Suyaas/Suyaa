using Suyaa.Exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Suyaa
{

    /// <summary>
    /// 类型编码扩展类
    /// </summary>
    public static class TypeCodeHelper
    {
        /// <summary>
        /// 获取是否为空
        /// </summary>
        /// <param name="code">类型编码</param>
        /// <returns></returns>
        public static bool IsNull(this TypeCode code)
            => code == TypeCode.Empty;

        /// <summary>
        /// 获取是否为数值
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsNumeric(this TypeCode code)
        {
            List<TypeCode> list = new List<TypeCode>() {
                TypeCode.Boolean,
                TypeCode.Byte,
                TypeCode.SByte,
                TypeCode.Char,
                TypeCode.Double,
                TypeCode.Int16,
                TypeCode.Int32,
                TypeCode.Int64,
                TypeCode.UInt16,
                TypeCode.UInt32,
                TypeCode.UInt64,
                TypeCode.Single,
                TypeCode.Double,
                TypeCode.Decimal,
            };
            return list.Contains(code);
        }
    }
}
