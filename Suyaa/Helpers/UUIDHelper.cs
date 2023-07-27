using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// UUID助手类
    /// </summary>
    public static class UUIDHelper
    {
        /// <summary>
        /// 获取时间对象
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public static DateTimeOffset GetTime(this UUID uuid)
        {
            return sy.Time.Parse(uuid.Timestamp, true);
        }
    }
}
