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
    public class Logger : ILogger, IDisposable
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
        public delegate void LogInfoHandle(LogInfo message);

        // 所有日志输出接口
        private readonly IList<Action<string>> _logMessages;
        private readonly IList<Action<LogInfo>> _logInfos;
        private readonly IList<ILogable> _loggers;

        // 支持多线程写入
        private static readonly object _lock = new object();
        private Queue<LogInfo> _logQueue;
        private Task _logTask;
        //private ManualResetEvent _manualReset;
        private CancellationTokenSource _tokenSource;
        private CancellationToken _token;

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
                string content = ActionLogger.GetLogString(info);
                // 输出到委托
                for (int i = 0; i < _logMessages.Count; i++)
                {
                    try { _logMessages[i](content); } catch { }
                }
                // 输出到委托
                for (int i = 0; i < _logInfos.Count; i++)
                {
                    try { _logInfos[i](info); } catch { }
                }
                // 输出到所有记录器
                for (int i = 0; i < _loggers.Count; i++)
                {
                    try { _loggers[i].Log(info); } catch { }
                }
                //Thread.Sleep(1);
            }
        }

        /// <summary>
        /// 对象实例化
        /// </summary>
        public Logger()
        {
            _logMessages = new List<Action<string>>();
            _logInfos = new List<Action<LogInfo>>();
            _loggers = new List<ILogable>();
            _logQueue = new Queue<LogInfo>();
            // 建立日志线程
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            _logTask = Task.Run(LogWrite, _token);
        }

        /// <summary>
        /// 注册使用日志器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Logger Use<T>() where T : ILogable, new()
        {
            _loggers.Add(new T());
            return this;
        }

        /// <summary>
        /// 使用日志器
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public Logger Use(ILogable logger)
        {
            _loggers.Add(logger);
            return this;
        }

        /// <summary>
        /// 注册委托
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public Logger Use(Action<string> logger)
        {
            _logMessages.Add(logger);
            return this;
        }

        /// <summary>
        /// 注册委托
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public Logger UseInfo(Action<LogInfo> logger)
        {
            _logInfos.Add(logger);
            return this;
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="info"></param>
        public void Log(LogInfo info)
        {
            if (info.Source.IsNullOrWhiteSpace()) info.Source = sy.Logger.GetDefaultSoucre();
            var task = new Task(() =>
            {
                lock (_lock)
                {
                    info.RecordId = LogInfo.GetNewRecordId();
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
            => Log(new LogInfo() { Event = evt, Level = LogLevel.Debug, Message = message });

        /// <summary>
        /// 记录普通信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        public void Info(string message, string? evt = null)
            => Log(new LogInfo() { Event = evt, Level = LogLevel.Info, Message = message });

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        public void Warn(string message, string? evt = null)
            => Log(new LogInfo() { Event = evt, Level = LogLevel.Warn, Message = message });

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        public void Error(string message, string? evt = null)
            => Log(new LogInfo() { Event = evt, Level = LogLevel.Error, Message = message });

        /// <summary>
        /// 记录致命信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        public void Fatal(string message, string? evt = null)
            => Log(new LogInfo() { Event = evt, Level = LogLevel.Fatal, Message = message });

        #region 释放资源

        // 是否释放
        private bool _disposed;

        /// <summary>
        /// 释放事件
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void OnDispose(bool disposing)
        {
            if (_disposed) return;
            #region 托管释放
            if (disposing)
            {
                _loggers.Clear();
                _logInfos.Clear();
                _logMessages.Clear();
                _logQueue.Clear();
                // 线程取消
                _tokenSource.Cancel();
                //_logTask.Dispose();
            }
            #endregion
            #region 非托管释放
            #endregion
            _disposed = true;
        }

        /// <summary>
        /// GC释放
        /// </summary>
        public void Dispose()
        {
            this.OnDispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 构析函数
        /// </summary>
        ~Logger()
        {
            this.OnDispose(false);
        }

        #endregion
    }
}
