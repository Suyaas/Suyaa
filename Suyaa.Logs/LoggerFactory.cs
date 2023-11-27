using Suyaa.Logs.Dependency;
using Suyaa.Logs.Loggers;
using Suyaa.Logs.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs
{
    /// <summary>
    /// 日志记录器工厂
    /// </summary>
    public class LoggerFactory : ILoggerFactory
    {

        #region 标准实现

        /// <summary>
        /// 使用匿名函数
        /// </summary>
        /// <param name="logDescriptorAction"></param>
        public LoggerFactory UseDescriptorAction(Action<LogDescriptor> logDescriptorAction)
        {
            AddCommonProvider(new DescriptorActionLogProvider(logDescriptorAction));
            return this;
        }

        /// <summary>
        /// 使用匿名函数
        /// </summary>
        /// <param name="stringAction"></param>
        public LoggerFactory UseStringAction(Action<string> stringAction)
        {
            AddCommonProvider(new StringActionLogProvider(stringAction));
            return this;
        }

        /// <summary>
        /// 使用控制台
        /// </summary>
        public LoggerFactory UseConsole()
        {
            AddCommonProvider(new ConsoleLogProvider());
            return this;
        }

        /// <summary>
        /// 使用文件
        /// </summary>
        public LoggerFactory UseFile(string path)
        {
            AddCommonProvider(new FileLogProvider(path));
            return this;
        }

        #endregion

        #region 通用日志

        private readonly IList<ICommonLogProvider> _commonLogProviders;

        /// <summary>
        /// 获取所有的通用日志供应商
        /// </summary>
        /// <returns></returns>
        public IList<ICommonLogProvider> GetCommonProviders() => _commonLogProviders;

        /// <summary>
        /// 添加通用日志供应商
        /// </summary>
        /// <param name="provider"></param>
        public LoggerFactory AddCommonProvider(ICommonLogProvider provider)
        {
            _commonLogProviders.Add(provider);
            return this;
        }

        /// <summary>
        /// 添加通用日志供应商
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public LoggerFactory AddCommonProvider<T>()
            where T : class, ICommonLogProvider, new()
        {
            AddCommonProvider(sy.Assembly.Create<T>());
            return this;
        }

        #endregion

        #region 普通日志

        private readonly IList<IInformationLogProvider> _informationLogProviders;

        /// <summary>
        /// 获取所有的通用日志供应商
        /// </summary>
        /// <returns></returns>
        public IList<IInformationLogProvider> GetInfoProviders() => _informationLogProviders;

        /// <summary>
        /// 添加通用日志供应商
        /// </summary>
        /// <param name="provider"></param>
        public LoggerFactory AddInfoProvider(IInformationLogProvider provider)
        {
            _informationLogProviders.Add(provider);
            return this;
        }

        /// <summary>
        /// 添加通用日志供应商
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public LoggerFactory AddInfoProvider<T>()
            where T : class, IInformationLogProvider, new()
        {
            AddInfoProvider(sy.Assembly.Create<T>());
            return this;
        }

        #endregion

        #region 调试日志

        private readonly IList<IDebugLogProvider> _debugLogProviders;

        /// <summary>
        /// 获取所有的通用日志供应商
        /// </summary>
        /// <returns></returns>
        public IList<IDebugLogProvider> GetDebugProviders() => _debugLogProviders;

        /// <summary>
        /// 添加通用日志供应商
        /// </summary>
        /// <param name="provider"></param>
        public LoggerFactory AddDebugProvider(IDebugLogProvider provider)
        {
            _debugLogProviders.Add(provider);
            return this;
        }

        /// <summary>
        /// 添加通用日志供应商
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public LoggerFactory AddDebugProvider<T>()
            where T : class, IDebugLogProvider, new()
        {
            AddDebugProvider(sy.Assembly.Create<T>());
            return this;
        }

        #endregion

        #region 警告日志

        private readonly IList<IWarningLogProvider> _warningLogProviders;

        /// <summary>
        /// 获取所有的通用日志供应商
        /// </summary>
        /// <returns></returns>
        public IList<IWarningLogProvider> GetWarnProviders() => _warningLogProviders;

        /// <summary>
        /// 添加通用日志供应商
        /// </summary>
        /// <param name="provider"></param>
        public LoggerFactory AddWarnProvider(IWarningLogProvider provider)
        {
            _warningLogProviders.Add(provider);
            return this;
        }

        /// <summary>
        /// 添加通用日志供应商
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public LoggerFactory AddWarnProvider<T>()
            where T : class, IWarningLogProvider, new()
        {
            AddWarnProvider(sy.Assembly.Create<T>());
            return this;
        }

        #endregion

        #region 错误日志

        private readonly IList<IErrorLogProvider> _errorLogProviders;

        /// <summary>
        /// 获取所有的通用日志供应商
        /// </summary>
        /// <returns></returns>
        public IList<IErrorLogProvider> GetErrorProviders() => _errorLogProviders;

        /// <summary>
        /// 添加通用日志供应商
        /// </summary>
        /// <param name="provider"></param>
        public LoggerFactory AddErrorProvider(IErrorLogProvider provider)
        {
            _errorLogProviders.Add(provider);
            return this;
        }

        /// <summary>
        /// 添加通用日志供应商
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public LoggerFactory AddErrorProvider<T>()
            where T : class, IErrorLogProvider, new()
        {
            AddErrorProvider(sy.Assembly.Create<T>());
            return this;
        }

        #endregion

        #region 致命日志

        private readonly IList<IFatalLogProvider> _fatalLogProviders;

        /// <summary>
        /// 获取所有的通用日志供应商
        /// </summary>
        /// <returns></returns>
        public IList<IFatalLogProvider> GetFatalProviders() => _fatalLogProviders;

        /// <summary>
        /// 添加通用日志供应商
        /// </summary>
        /// <param name="provider"></param>
        public LoggerFactory AddFatalProvider(IFatalLogProvider provider)
        {
            _fatalLogProviders.Add(provider);
            return this;
        }

        /// <summary>
        /// 添加通用日志供应商
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public LoggerFactory AddFatalProvider<T>()
            where T : class, IFatalLogProvider, new()
        {
            AddFatalProvider(sy.Assembly.Create<T>());
            return this;
        }

        #endregion

        /// <summary>
        /// 日志记录器工厂
        /// </summary>
        public LoggerFactory()
        {
            _commonLogProviders = new List<ICommonLogProvider>();
            _informationLogProviders = new List<IInformationLogProvider>();
            _debugLogProviders = new List<IDebugLogProvider>();
            _warningLogProviders = new List<IWarningLogProvider>();
            _errorLogProviders = new List<IErrorLogProvider>();
            _fatalLogProviders = new List<IFatalLogProvider>();
        }

    }
}
