using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace sy
{
    /// <summary>
    /// 模拟终端
    /// </summary>
    public static class Terminal
    {
        /// <summary>
        /// 执行命令行程序
        /// </summary>
        /// <param name="processStartInfo"></param>
        /// <returns></returns>
        public static string Execute(ProcessStartInfo processStartInfo)
        {
            using (Suyaa.Terminal term = new Suyaa.Terminal())
            {
                return term.Execute(processStartInfo);
            }
        }

        /// <summary>
        /// 执行命令行程序
        /// </summary>
        /// <param name="processStartInfo"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Execute(ProcessStartInfo processStartInfo, Encoding encoding)
        {
            using (Suyaa.Terminal term = new Suyaa.Terminal(sy.Assembly.WorkingDirectory, encoding))
            {
                return term.Execute(processStartInfo);
            }
        }

        /// <summary>
        /// 执行命令行程序
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Execute(string filePath, string? args)
        {
            using (Suyaa.Terminal term = new Suyaa.Terminal())
            {
                return term.Execute(filePath, args);
            }
        }

        /// <summary>
        /// 执行命令行程序
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="args"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Execute(string filePath, string? args, Encoding encoding)
        {
            using (Suyaa.Terminal term = new Suyaa.Terminal(sy.Assembly.WorkingDirectory, encoding))
            {
                return term.Execute(filePath, args);
            }
        }
    }
}
