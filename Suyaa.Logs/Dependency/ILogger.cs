using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs.Dependency
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public interface ILogger : ICommonLogable, IDebugLogable, IInformationLogable, IWarningLogable, IErrorLogable, IFatalLogable
    {
    }
}
