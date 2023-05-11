using Suyaa;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 字符串助手类
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 将C#名称转化为数据库小写名称，如 AbcDe -> abc_de
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToLowerDbName(this string? str)
        {
            if (str is null) return string.Empty;
            if (str.IsNullOrWhiteSpace()) return string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append(str[0].ToLower());
            for (int i = 1; i < str.Length; i++)
            {
                char chr = str[i];
                if (chr.IsUpper())
                {
                    sb.Append('_');
                    sb.Append(chr.ToLower());
                }
                else
                {
                    sb.Append(chr);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将C#名称转化为数据库大写名称，如 AbcDe -> ABC_DE
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUpperDbName(this string? str)
        {
            if (str is null) return string.Empty;
            if (str.IsNullOrWhiteSpace()) return string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append(str[0].ToUpper());
            for (int i = 1; i < str.Length; i++)
            {
                char chr = str[i];
                if (chr.IsUpper())
                {
                    sb.Append('_');
                    sb.Append(chr);
                }
                else
                {
                    sb.Append(chr.ToUpper());
                }
            }
            return sb.ToString();
        }
    }
}
