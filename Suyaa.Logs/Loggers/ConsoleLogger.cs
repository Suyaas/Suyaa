using Suyaa;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Suyaa.Logs.Loggers
{

    /// <summary>
    /// 控制台输出
    /// </summary>
    public class ConsoleLogger : ILogable
    {

        /// <summary>
        /// 输出内容
        /// </summary>
        /// <param name="entity"></param>
        public void Log(LogInfo entity)
        {
            // 获取默认
            //if (entity.Source.IsNullOrWhiteSpace()) entity.Source = sy.Logger.GetDefaultSoucre();
            var color = Console.ForegroundColor;
            //Console.ForegroundColor = ConsoleColor.Blue;
            switch (entity.Level)
            {
                case LogLevel.Fatal: Console.ForegroundColor = ConsoleColor.DarkRed; break;
                case LogLevel.Error: Console.ForegroundColor = ConsoleColor.Red; break;
                case LogLevel.Warn: Console.ForegroundColor = ConsoleColor.Yellow; break;
                case LogLevel.Info: Console.ForegroundColor = ConsoleColor.Green; break;
                default: Console.ForegroundColor = color; break;
            }
            // 输出内容
            Console.Write(ActionLogger.GetLogString(entity));
            if (entity.Level == LogLevel.Info)
            {
                Console.ForegroundColor = color;
            }
            Console.WriteLine();
            Console.ForegroundColor = color;
        }
    }
}
