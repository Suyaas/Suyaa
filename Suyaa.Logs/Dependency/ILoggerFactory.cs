using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs.Dependency
{
    /// <summary>
    /// 日志工厂
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// 获取通用日志供应商
        /// </summary>
        /// <returns></returns>
        IList<ICommonLogProvider> GetCommonProviders();

        /// <summary>
        /// 获取调试信息供应商
        /// </summary>
        /// <returns></returns>
        IList<IDebugLogProvider> GetDebugProviders();

        /// <summary>
        /// 获取普通信息供应商
        /// </summary>
        /// <returns></returns>
        IList<IInformationLogProvider> GetInfoProviders();

        /// <summary>
        /// 获取警告信息供应商
        /// </summary>
        /// <returns></returns>
        IList<IWarningLogProvider> GetWarnProviders();

        /// <summary>
        /// 获取错误信息供应商
        /// </summary>
        /// <returns></returns>
        IList<IErrorLogProvider> GetErrorProviders();

        /// <summary>
        /// 获取致命信息供应商
        /// </summary>
        /// <returns></returns>
        IList<IFatalLogProvider> GetFatalProviders();

    }
}
