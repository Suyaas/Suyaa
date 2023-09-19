using System;
using System.Collections.Generic;
using System.Text;

namespace sy
{
    /// <summary>
    /// 控制台
    /// </summary>
    public static class Console
    {
        // 互斥锁
        private static readonly object _lock = new object();

        /// <summary>
        /// 信息颜色
        /// </summary>
        public static ConsoleColor SignColor { get; set; } = ConsoleColor.Yellow;

        /// <summary>
        /// 信息颜色
        /// </summary>
        public static ConsoleColor InfoColor { get; set; } = ConsoleColor.White;

        /// <summary>
        /// 警告颜色
        /// </summary>
        public static ConsoleColor WarnColor { get; set; } = ConsoleColor.Cyan;

        /// <summary>
        /// 异常颜色
        /// </summary>
        public static ConsoleColor ErrorColor { get; set; } = ConsoleColor.DarkRed;

        /// <summary>
        /// 输出普通内容
        /// </summary>
        /// <param name="content"></param>
        public static void Write(string content)
        {
            lock (_lock)
            {
                System.Console.Write(content);
            }
        }

        /// <summary>
        /// 输出带着色的内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="color"></param>
        public static void Write(string content, ConsoleColor color)
        {
            lock (_lock)
            {
                ConsoleColor currentColor = System.Console.ForegroundColor;
                System.Console.ForegroundColor = color;
                System.Console.Write(content);
                System.Console.ForegroundColor = currentColor;
            }
        }

        /// <summary>
        /// 输出标志
        /// </summary>
        /// <param name="sign"></param>
        public static void SignWrite(string sign)
        {
            Write("[" + sign + "] ", SignColor);
        }

        /// <summary>
        /// 输出换行
        /// </summary>
        public static void WriteLine()
        {
            lock (_lock)
            {
                System.Console.WriteLine();
            }
        }

        /// <summary>
        /// 输出普通内容
        /// </summary>
        /// <param name="content"></param>
        public static void WriteLine(string content)
        {
            lock (_lock)
            {
                System.Console.WriteLine(content);
            }
        }

        /// <summary>
        /// 输出带着色的换行内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="color"></param>
        public static void WriteLine(string content, ConsoleColor color)
        {
            lock (_lock)
            {
                ConsoleColor currentColor = System.Console.ForegroundColor;
                System.Console.ForegroundColor = color;
                System.Console.WriteLine(content);
                System.Console.ForegroundColor = currentColor;
            }
        }

        /// <summary>
        /// 输出标志的信息
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="content"></param>
        public static void SignInfo(string sign, string content)
        {
            SignWrite(sign);
            WriteLine(content, InfoColor);
        }

        /// <summary>
        /// 输出信息
        /// </summary>
        /// <param name="content"></param>
        public static void Info(string content)
        {
            WriteLine(content, InfoColor);
        }

        /// <summary>
        /// 输出标志的警告
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="content"></param>
        public static void SignWarn(string sign, string content)
        {
            SignWrite(sign);
            WriteLine(content, WarnColor);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="content"></param>
        public static void Warn(string content)
        {
            WriteLine(content, WarnColor);
        }

        /// <summary>
        /// 输出标志的异常
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="content"></param>
        public static void SignError(string sign, string content)
        {
            SignWrite(sign);
            WriteLine(content, ErrorColor);
        }

        /// <summary>
        /// 输出异常
        /// </summary>
        /// <param name="content"></param>
        public static void Error(string content)
        {
            WriteLine(content, ErrorColor);
        }
    }
}
