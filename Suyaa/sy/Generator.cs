using Suyaa;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace sy
{
    /// <summary>
    /// 生成器
    /// </summary>
    public static partial class Generator
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public static DateTime StartTime { get; set; } = new DateTime(2020, 1, 1);

        /// <summary>
        /// 全局机器ID
        /// </summary>
        public static int MachineId { get; set; } = 0;

        /// <summary>
        /// 全局应用ID
        /// </summary>
        public static int AppId { get; set; } = 0;

        /// <summary>
        /// 获取一个新的带'-'的Guid
        /// </summary>
        /// <returns></returns>
        public static string GetNewGuid() => Guid.NewGuid().ToString();

        /// <summary>
        /// 获取一个新的不带'-'的Guid
        /// </summary>
        /// <returns></returns>
        public static string GetNewNGuid() => Guid.NewGuid().ToString("N");
    }
}
