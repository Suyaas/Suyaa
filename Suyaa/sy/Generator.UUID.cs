using Suyaa;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace sy
{
    /* 生成器 - UUID */
    public static partial class Generator
    {
        // 定义UUID生成器对象
        private static UUIDGenerator? _uuid;

        /// <summary>
        /// UUID生成器
        /// </summary>
        public static UUIDGenerator UUID => _uuid ??= new UUIDGenerator(MachineId, AppId);

        /// <summary>
        /// 创建一个新的UUID生成器
        /// </summary>
        /// <returns></returns>
        public static UUIDGenerator CreateUUIDGenerator() => _uuid = new UUIDGenerator(MachineId, AppId);

        /// <summary>
        /// 获取一个新的UUID
        /// </summary>
        /// <returns></returns>
        public static UUID GetNewUUID() => UUID.Next();
    }
}
