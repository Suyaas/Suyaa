using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text;

namespace Suyaa.Usables.Helpers
{
    /// <summary>
    /// 程序集助手
    /// </summary>
    public static class ToyAssemblyHelper
    {
        // 执行文件
        private static string? _executionFilePath;

        /// <summary>
        /// 获取程序执行文件路径
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public static string GetExecutionFile(this Toy<Assembly> use)
            => _executionFilePath ??= Process.GetCurrentProcess().MainModule.FileName;

        /// <summary>
        /// 获取程序执行文件版本信息
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public static FileVersionInfo GetExecutionFileInfo(this Toy<Assembly> use)
            => use.GetFileVersionInfo(use.GetExecutionFile());

        /// <summary>
        /// 获取产品名称
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public static string GetProductName(this Toy<Assembly> use)
            => use.GetExecutionFileInfo().ProductName;

        /// <summary>
        /// 获取产品版本
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public static string GetProductVersion(this Toy<Assembly> use)
            => use.GetExecutionFileInfo().GetProductVersion();

        /// <summary>
        /// 获取产品完整名称
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public static string GetProductFullName(this Toy<Assembly> use)
            => use.GetProductName() + " Ver:" + use.GetProductVersion();

        /// <summary>
        /// 获取程序执行文件目录
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public static string GetExecutionDirectory(this Toy<Assembly> use)
            => Path.GetDirectoryName(use.GetExecutionFile());

        /// <summary>
        /// 获取工作目录
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public static string GetWorkingDirectory(this Toy<Assembly> use)
        {
            return Environment.CurrentDirectory;
        }

        /// <summary>
        /// 获取模块基文件
        /// </summary>
        /// <returns></returns>
        public static string GetModuleFile(this Toy<Assembly> use, Type type)
            => type.Assembly.Location;

        /// <summary>
        /// 获取模块基文件
        /// </summary>
        /// <returns></returns>
        public static string GetModuleFile<T>(this Toy<Assembly> use)
            => use.GetModuleFile(typeof(T));

        /// <summary>
        /// 获取模块基文件
        /// </summary>
        /// <returns></returns>
        public static string GetModuleFile(this Toy<Assembly> use)
            => use.GetModuleFile<Disposable>();

        /// <summary>
        /// 获取模块基目录
        /// </summary>
        /// <returns></returns>
        public static string GetModuleDirectory(this Toy<Assembly> use, Type type)
            => Path.GetDirectoryName(use.GetModuleFile(type));

        /// <summary>
        /// 获取模块基目录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetModuleDirectory<T>(this Toy<Assembly> use)
            where T : class
            => use.GetModuleDirectory(typeof(T));

        /// <summary>
        /// 获取模块基目录
        /// </summary>
        /// <returns></returns>
        public static string GetModuleDirectory(this Toy<Assembly> use)
            => use.GetModuleDirectory<Disposable>();

        /// <summary>
        /// 获取文件版本信息
        /// </summary>
        /// <param name="use"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static FileVersionInfo GetFileVersionInfo(this Toy<Assembly> use, string file)
            => FileVersionInfo.GetVersionInfo(file);
    }
}
