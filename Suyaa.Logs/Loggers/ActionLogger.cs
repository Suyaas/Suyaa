using Suyaa;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Suyaa.Logs.Loggers
{

    /// <summary>
    /// 可注册方法日志管理器
    /// </summary>
    public class ActionLogger : ILogable
    {
        // 方法
        private readonly Action<string> _action;

        /// <summary>
        /// 可注册方法日志管理器
        /// </summary>
        /// <param name="action"></param>
        public ActionLogger(Action<string> action)
        {
            _action = action;
        }

        /// <summary>
        /// 输出内容
        /// </summary>
        /// <param name="info"></param>
        public void Log(LogInfo info)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"*");
            sb.Append($"{sy.Time.Now.ToFullDateTimeString()}");
            sb.Append($"*");
            sb.Append($" [{info.Level.ToString().ToUpper()}]");
            sb.Append($" {info.Event} {info.Message}");
            sb.AppendLine();
            _action(sb.ToString());
        }
    }
}
