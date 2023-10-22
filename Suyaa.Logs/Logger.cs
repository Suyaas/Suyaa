using Suyaa.Logs.Dependency;
using Suyaa.Logs.Loggers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Suyaa.Logs
{

    /// <summary>
    /// 日志器集合
    /// </summary>
    public class Logger : Disposable, ILogger
    {
        /// <summary>
        /// 日志输出接口
        /// </summary>
        /// <param name="message"></param>
        public delegate void LogMessageHandle(string? message);

        /// <summary>
        /// 日志对象输出接口
        /// </summary>
        /// <param name="message"></param>
        public delegate void LogInfoHandle(LogDescriptor message);

        // 所有日志输出接口
        //private readonly IList<Action<string>> _logMessages;
        //private readonly IList<Action<LogDescriptor>> _logInfos;
        //private readonly IList<ICommonLogable> _loggers;

        // 支持多线程写入
        private static readonly object _lock = new object();
        private readonly Queue<LogDescriptor> _logQueue;
        //private ManualResetEvent _manualReset;
        private readonly CancellationTokenSource _tokenSource;
        private readonly CancellationToken _token;
        private readonly ILoggerFactory _loggerFactory;

        private void LogWrite()
        {
            while (!_token.IsCancellationRequested)
            {
                // 判断队列是否为空
                if (!_logQueue.Any())
                {
                    Thread.Sleep(10);
                    continue;
                }
                var info = _logQueue.Dequeue();
                //string content = ActionLogger.GetLogString(info);
                //// 输出到委托
                //for (int i = 0; i < _logMessages.Count; i++)
                //{
                //    try { _logMessages[i](content); } catch { }
                //}
                //// 输出到委托
                //for (int i = 0; i < _logInfos.Count; i++)
                //{
                //    try { _logInfos[i](info); } catch { }
                //}
                // 输出到所有记录器
                var loggers = _loggerFactory.GetCommonProviders();
                for (int i = 0; i < loggers.Count; i++)
                {
                    try { loggers[i].Log(info); } catch { }
                }
                //Thread.Sleep(1);
            }
        }

        /// <summary>
        /// 对象实例化
        /// </summary>
        public Logger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            //_logMessages = new List<Action<string>>();
            //_logInfos = new List<Action<LogDescriptor>>();
            //_loggers = new List<ICommonLogable>();
            _logQueue = new Queue<LogDescriptor>();
            // 建立日志线程
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            Task.Run(LogWrite, _token);
        }

        ///// <summary>
        ///// 注册使用日志器
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <returns></returns>
        //public Logger Use<T>() where T : ICommonLogable, new()
        //{
        //    _loggers.Add(new T());
        //    return this;
        //}

        ///// <summary>
        ///// 使用日志器
        ///// </summary>
        ///// <param name="logger"></param>
        ///// <returns></returns>
        //public Logger Use(ICommonLogable logger)
        //{
        //    _loggers.Add(logger);
        //    return this;
        //}

        ///// <summary>
        ///// 注册委托
        ///// </summary>
        ///// <param name="logger"></param>
        ///// <returns></returns>
        //public Logger Use(Action<string> logger)
        //{
        //    _logMessages.Add(logger);
        //    return this;
        //}

        ///// <summary>
        ///// 注册委托
        ///// </summary>
        ///// <param name="logger"></param>
        ///// <returns></returns>
        //public Logger UseInfo(Action<LogDescriptor> logger)
        //{
        //    _logInfos.Add(logger);
        //    return this;
        //}

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="info"></param>
        public void Log(LogDescriptor info)
        {
            if (info.Source.IsNullOrWhiteSpace()) info.Source = sy.Logger.GetDefaultSoucre();
            var task = new Task(() =>
            {
                lock (_lock)
                {
                    info.RecordId = LogDescriptor.GetNewRecordId();
                    _logQueue.Enqueue(info);
                }
            });
            task.Start();
        }

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        public void Debug(string message, string? evt = null)
        {
            // 输出到个性化记录器
            var loggers = _loggerFactory.GetDebugProviders();
            for (int i = 0; i < loggers.Count; i++)
            {
                try { loggers[i].Debug(message, evt); } catch { }
            }
            // 输出标准日志
            Log(new LogDescriptor() { Event = evt, Level = LogLevel.Debug, Message = message });
        }

        /// <summary>
        /// 记录普通信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        public void Info(string message, string? evt = null)
        {
            // 输出到个性化记录器
            var loggers = _loggerFactory.GetInfoProviders();
            for (int i = 0; i < loggers.Count; i++)
            {
                try { loggers[i].Info(message, evt); } catch { }
            }
            Log(new LogDescriptor() { Event = evt, Level = LogLevel.Info, Message = message });
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        public void Warn(string message, string? evt = null)
        {
            // 输出到个性化记录器
            var loggers = _loggerFactory.GetWarnProviders();
            for (int i = 0; i < loggers.Count; i++)
            {
                try { loggers[i].Warn(message, evt); } catch { }
            }
            // 输出标准日志
            Log(new LogDescriptor() { Event = evt, Level = LogLevel.Warn, Message = message });
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        public void Error(string message, string? evt = null)
        {
            // 输出到个性化记录器
            var loggers = _loggerFactory.GetErrorProviders();
            for (int i = 0; i < loggers.Count; i++)
            {
                try { loggers[i].Error(message, evt); } catch { }
            }
            // 输出标准日志
            Log(new LogDescriptor() { Event = evt, Level = LogLevel.Error, Message = message });
        }

        /// <summary>
        /// 记录致命信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        public void Fatal(string message, string? evt = null)
        {
            // 输出到个性化记录器
            var loggers = _loggerFactory.GetFatalProviders();
            for (int i = 0; i < loggers.Count; i++)
            {
                try { loggers[i].Fatal(message, evt); } catch { }
            }
            // 输出标准日志
            Log(new LogDescriptor() { Event = evt, Level = LogLevel.Fatal, Message = message });
        }

        #region 释放资源

        /// <summary>
        /// 托管资源释放
        /// </summary>
        protected override void OnManagedDispose()
        {
            _logQueue.Clear();
            // 线程取消
            _tokenSource.Cancel();
            base.OnManagedDispose();
        }

        #endregion
    }
}
