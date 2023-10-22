using Suyaa.Logs.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs.Providers
{
    /// <summary>
    /// 匿名函数方式日志供应商
    /// </summary>
    public class DescriptorActionLogProvider : ICommonLogProvider
    {
        private readonly Action<LogDescriptor> _logDescriptorAction;

        /// <summary>
        /// 匿名函数方式日志供应商
        /// </summary>
        /// <param name="logDescriptorAction"></param>
        public DescriptorActionLogProvider(Action<LogDescriptor> logDescriptorAction)
        {
            _logDescriptorAction = logDescriptorAction;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log"></param>
        public void Log(LogDescriptor log)
        {
            _logDescriptorAction.Invoke(log);
        }
    }
}
