using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs.Helpers
{
    /// <summary>
    /// 日志描述助手
    /// </summary>
    public static class LogDescriptorHelper
    {
        /// <summary>
        /// 获取日志字符串
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string GetLogString(this LogDescriptor info)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            sb.Append(info.Level.ToString().ToUpper());
            sb.Append(']');
            sb.Append('[');
            sb.Append(sy.Time.Now.ToFullDateTimeString());
            sb.Append(']');
            sb.Append($" {info.Event}@{info.Source} - {info.Message}");
            return sb.ToString();
        }
    }
}
