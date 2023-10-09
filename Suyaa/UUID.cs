using System;
using System.Text;

namespace Suyaa
{
    /*
     * 自定义32位UUID算法，可分布式唯一并支持排序
     * 8位时间码
     * 8位序列码
     * 4位机器码
     * 4位应用码
     * 8位随机码
     */

    /// <summary>
    /// 全球唯一码
    /// </summary>
    public struct UUID
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp;
        /// <summary>
        /// 顺序码
        /// </summary>
        public int Sequence;
        /// <summary>
        /// 机器码
        /// </summary>
        public int Machine;
        /// <summary>
        /// 应用码
        /// </summary>
        public int Appliction;
        /// <summary>
        /// 随机码
        /// </summary>
        public int Code;

        /// <summary>
        /// 全球唯一码
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="sequence"></param>
        /// <param name="machine"></param>
        /// <param name="appliction"></param>
        /// <param name="code"></param>
        public UUID(long timestamp, int sequence, int machine, int appliction, int code)
        {
            Timestamp = timestamp;
            Sequence = sequence;
            Machine = machine;
            Appliction = appliction;
            Code = code;
        }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <param name="isFormat"></param>
        /// <returns></returns>
        public string ToString(bool isFormat)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Timestamp.ToString("x").PadLeft(12, '0'));
            if (isFormat) sb.Append('-');
            sb.Append(Sequence.ToString("x").PadLeft(6, '0'));
            if (isFormat) sb.Append('-');
            sb.Append(Machine.ToString("x").PadLeft(4, '0'));
            if (isFormat) sb.Append('-');
            sb.Append(Appliction.ToString("x").PadLeft(4, '0'));
            if (isFormat) sb.Append('-');
            sb.Append(Code.ToString("x").PadLeft(6, '0'));
            return sb.ToString();
        }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToString(true);
        }

        /// <summary>
        /// 尝试分析
        /// </summary>
        /// <param name="str"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public static bool TryParse(string str, out UUID uuid)
        {
            string[] strs;
            switch (str.Length)
            {
                // 无'-'UUID
                case 32:
                    strs = new string[5];
                    strs[0] = str.Substring(0, 12);
                    strs[1] = str.Substring(12, 6);
                    strs[2] = str.Substring(18, 4);
                    strs[3] = str.Substring(22, 4);
                    strs[4] = str.Substring(26);
                    break;
                // 带'-'UUID
                case 36:
                    strs = str.Split('-');
                    break;
                default:
                    uuid = new UUID();
                    return false;
            }
            try
            {
                uuid = new UUID(
                    Convert.ToInt64(strs[0], 16),
                    Convert.ToInt32(strs[1], 16),
                    Convert.ToInt32(strs[2], 16),
                    Convert.ToInt32(strs[3], 16),
                    Convert.ToInt32(strs[4], 16)
                    );
                return true;
            }
            catch
            {
                uuid = new UUID();
                return false;
            }
        }
    }

    /// <summary>
    /// 自定义32位UUID算法，可分布式唯一并支持排序
    /// </summary>
    public class UUIDGenerator
    {
        // 支持的最大机器id
        private const int MAX_MACHINE_ID = 0xffff;
        // 支持的最大应用器id
        private const int MAX_APP_ID = 0xffff;
        // 支持的最大应用器id
        private const int MAX_SEQUENCE = 0xffffff;
        // 随机生成器
        private Random _random = new Random();

        // 互斥锁对象
        private static object _lock = new object();

        /// <summary>
        /// 机器ID(0~0xffff)
        /// </summary>
        public int MachineId { get; private set; }

        /// <summary>
        /// 应用ID(0~0xffff)
        /// </summary>
        public int AppId { get; private set; }

        /// <summary>
        /// 生成序列(0~0xffffff)
        /// </summary>
        public int Sequence { get; private set; }

        /// <summary>
        /// 上次生成ID的时间截
        /// </summary>
        public long LastTimestamp { get; private set; }

        // 获取随机码
        private int CreateRandomLong6()
        {
            return _random.Next(0x1000000);
        }

        // 获取随机码
        private long CreateRandomLong8()
        {
            return (_random.Next(0x10000) * (long)0x10000) + _random.Next(0x10000);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            Sequence = 0;
            this.LastTimestamp = -1;
        }

        /// <summary>
        /// 自定义32位UUID算法，可分布式唯一并支持排序, 机器Id和AppId均为0
        /// </summary>
        public UUIDGenerator()
        {
            this.MachineId = 0;
            this.AppId = 0;
            this.Initialize();
        }

        /// <summary>
        /// 自定义32位UUID算法，可分布式唯一并支持排序,AppId均为0
        /// </summary>
        /// <param name="machineId">机器ID(0~1023)</param>
        /// <exception cref="Exception"></exception>
        public UUIDGenerator(int machineId)
        {
            if (machineId < 0 || machineId > MAX_MACHINE_ID) throw new NonStandardException($"The range of machine ID is 0-{MAX_MACHINE_ID}, now {machineId}.");
            this.MachineId = machineId;
            this.AppId = 0;
            this.Initialize();
        }

        /// <summary>
        /// 自定义32位UUID算法，可分布式唯一并支持排序
        /// </summary>
        /// <param name="machineId">机器ID(0~1023)</param>
        /// <param name="appId">应用ID(0~1023)</param>
        public UUIDGenerator(int machineId, int appId)
        {
            if (machineId < 0 || machineId > MAX_MACHINE_ID) throw new NonStandardException($"The range of machine id is 0-{MAX_MACHINE_ID}, now {machineId}.");
            if (appId < 0 || appId > MAX_APP_ID) throw new NonStandardException($"The range of app id is 0-{MAX_APP_ID}, now {appId}.");
            this.MachineId = machineId;
            this.AppId = appId;
            this.Initialize();
        }

        #region 核心方法

        /// <summary>
        /// 获取下一个UUID
        /// </summary>
        /// <returns></returns>
        public UUID Next()
        {
            lock (_lock)
            {
                long timestamp = GetCurrentTimestamp();
                // 如果时钟回调，则使用上一时钟
                if (timestamp > this.LastTimestamp)
                {
                    // 记录最后的时间截
                    this.LastTimestamp = timestamp;
                    this.Sequence = 0;
                }
                // 序列码+1
                this.Sequence++;
                if (this.Sequence > MAX_SEQUENCE) this.Sequence = 0;
                var code = CreateRandomLong6();
                return new UUID(this.LastTimestamp, this.Sequence, MachineId, AppId, code);
            }
        }

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        private long GetCurrentTimestamp()
        {
            return sy.Time.Now.ToUnixTimeMilliseconds();
        }
        #endregion
    }
}
