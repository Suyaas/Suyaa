using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Suyaa
{

    /// <summary>
    /// 时间扩展类
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 获取yyyy-MM-dd HH:mm:ss.fff格式时间表示字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToFullDateTimeString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// 获取yyyy-MM-dd HH:mm:ss格式时间表示字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 获取yyyy-MM-dd格式时间表示字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 获取HH:mm:ss格式时间表示字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToTimeString(this DateTime dt)
        {
            return dt.ToString("HH:mm:ss");
        }

    }
}
