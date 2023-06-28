﻿using Suyaa;
using Suyaa.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace sy
{

    /// <summary>
    /// 日志管理器
    /// </summary>
    public static class Logger
    {

        private static Suyaa.Logs.Logger? _loggers;

        /// <summary>
        /// 获取默认来源
        /// </summary>
        /// <returns></returns>
        internal static string GetDefaultSoucre()
        {
            var trace = new StackTrace();
            StackFrame? frame = null;
            MethodBase? method = null;
            int index = 0;
            while (index < trace.FrameCount)
            {
                frame = trace.GetFrame(index);
                method = frame.GetMethod();
                if (method.DeclaringType.Equals(typeof(sy.Logger)))
                {
                    index++;
                    continue;
                }
                if (method.DeclaringType.HasInterface<ILogger>())
                {
                    index++;
                    continue;
                }
                if (method.DeclaringType.HasInterface<ILogable>())
                {
                    index++;
                    continue;
                }
                break;
            }
            if (method is null) return string.Empty;
            return method.DeclaringType.FullName + "." + method.Name;
        }

        /// <summary>
        /// 获取当前日志记录器
        /// </summary>
        /// <returns></returns>
        public static Suyaa.Logs.Logger GetCurrentLogger()
        {
            if (_loggers is null) _loggers = new Suyaa.Logs.Logger();
            return _loggers;
        }

        /// <summary>
        /// 使用一个新的日志记录器
        /// </summary>
        /// <returns></returns>
        public static Suyaa.Logs.Logger Create()
        {
            _loggers = new Suyaa.Logs.Logger();
            return _loggers;
        }

        /// <summary>
        /// 添加一条调试信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        public static void Debug(string message, string evt = "default")
        {
            _loggers?.Log(new LogInfo()
            {
                Event = evt,
                Level = LogLevel.Debug,
                Message = message
            });
        }

        /// <summary>
        /// 添加一条普通信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        public static void Info(string message, string evt = "default")
        {
            _loggers?.Log(new LogInfo()
            {
                Event = evt,
                Level = LogLevel.Info,
                Message = message
            });
        }

        /// <summary>
        /// 添加一条警告信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        public static void Warn(string message, string evt = "default")
        {
            _loggers?.Log(new LogInfo()
            {
                Event = evt,
                Level = LogLevel.Warn,
                Message = message
            });
        }

        /// <summary>
        /// 添加一条错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        public static void Error(string message, string evt = "default")
        {
            _loggers?.Log(new LogInfo()
            {
                Event = evt,
                Level = LogLevel.Error,
                Message = message
            });
        }

        /// <summary>
        /// 添加一条致命错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        public static void Fatal(string message, string evt = "default")
        {
            _loggers?.Log(new LogInfo()
            {
                Event = evt,
                Level = LogLevel.Fatal,
                Message = message
            });
        }
    }
}
