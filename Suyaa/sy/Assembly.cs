﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;
using Suyaa;

namespace sy
{

    /// <summary>
    /// 程序集
    /// </summary>
    public static partial class Assembly
    {

        // 私有变量
        private static string? _executionFilePath = null;
        private static string? _executionDirectory = null;
        private static string? _workingDirectory = null;
        private static FileVersionInfo? _info = null;

        /// <summary>
        /// 获取执行程序信息
        /// </summary>
        public static FileVersionInfo ExecutionInfo
        {
            get
            {
                if (_info == null)
                {
                    _info = FileVersionInfo.GetVersionInfo(ExecutionFilePath);
                }
                return _info;
            }
        }

        /// <summary>
        /// 获取程序版本
        /// </summary>
        public static string Version { get { return ExecutionInfo.ProductVersion; } }

        /// <summary>
        /// 获取程序名称
        /// </summary>
        public static string Name { get { return ExecutionInfo.ProductName; } }

        /// <summary>
        /// 获取程序全称
        /// </summary>
        public static string FullName { get { return ExecutionInfo.ProductName + " Ver:" + ExecutionInfo.ProductVersion; } }

        /// <summary>
        /// 获取程序版本
        /// </summary>
        public static string ExecutionFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(_executionFilePath))
                {
                    _executionFilePath = Process.GetCurrentProcess().MainModule.FileName;
                }
                return _executionFilePath;
            }
        }

        /// <summary>
        /// 获取程序目录
        /// </summary>
        public static string ExecutionDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_executionDirectory))
                {
                    _executionDirectory = IO.GetClosedPath(Path.GetDirectoryName(ExecutionFilePath));
                }
                return _executionDirectory;
            }
        }

        /// <summary>
        /// 获取或设置工作目录
        /// </summary>
        public static string WorkingDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_workingDirectory))
                {
                    _workingDirectory = IO.GetClosedPath(Environment.CurrentDirectory);
                }
                return _workingDirectory;
            }
            set { _workingDirectory = value; }
        }

        /// <summary>
        /// 获取模块基目录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetModulePath<T>()
            where T : class
        {
            return GetModulePath(typeof(T));
        }

        /// <summary>
        /// 获取模块基目录
        /// </summary>
        /// <returns></returns>
        public static string GetModulePath(Type type)
        {
            using var processModule = Process.GetCurrentProcess().MainModule;
            var filePath = type.Assembly.Location;
            var folder = sy.IO.GetFolderPath(filePath);
            return folder;
        }

        /// <summary>
        /// 获取模块基目录
        /// </summary>
        /// <returns></returns>
        public static string GetModulePath()
        {
            return GetModulePath<Disposable>();
        }

        /// <summary>
        /// 获取程序参数集
        /// </summary>
        public static Suyaa.Arguments.IArguments? Arguments { get; private set; }

        /// <summary>s
        /// 设置参数集合
        /// </summary>
        /// <returns></returns>
        public static void SetArguments<T>(string[] args) where T : Suyaa.Arguments.IArguments, new()
        {
            Arguments = CreateArguments<T>(args);
        }

        /// <summary>
        /// 获取参数集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T CreateArguments<T>(string[] args) where T : Suyaa.Arguments.IArguments, new()
        {
            var obj = new T();
            obj.SetParams(args);
            return obj;
        }

    }
}
