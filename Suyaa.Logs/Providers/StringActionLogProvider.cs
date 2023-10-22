using Suyaa.Logs.Dependency;
using Suyaa.Logs.Helpers;
using Suyaa.Logs.Loggers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs
{
    /// <summary>
    /// 匿名函数方式日志供应商
    /// </summary>
    public class StringActionLogProvider : ICommonLogProvider
    {
        private readonly Action<string> _stringAction;

        /// <summary>
        /// 匿名函数方式日志供应商
        /// </summary>
        /// <param name="stringAction"></param>
        public StringActionLogProvider(Action<string> stringAction)
        {
            _stringAction = stringAction;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log"></param>
        public void Log(LogDescriptor log)
        {
            _stringAction.Invoke(log.GetLogString());
        }
    }
}
