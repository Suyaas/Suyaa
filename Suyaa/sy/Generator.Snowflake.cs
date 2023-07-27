using Suyaa;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace sy
{
    /* 生成器 - 雪花Id */
    public static partial class Generator
    {

        // 定义雪花算法对象
        private static Suyaa.Snowflake? _snowflake;

        /// <summary>
        /// 雪花算法
        /// </summary>
        public static Snowflake Snowflake => _snowflake ??= new Snowflake(StartTime, MachineId);

        /// <summary>
        /// 创建一个新的UUID生成器
        /// </summary>
        /// <returns></returns>
        public static Snowflake CreateSnowflakeGenerator() => _snowflake = new Snowflake(StartTime, MachineId);

        /// <summary>
        /// 获取一个新的雪花算法Id
        /// </summary>
        /// <returns></returns>
        public static long GetSnowflakeId() => Generator.Snowflake.Next();
    }
}
