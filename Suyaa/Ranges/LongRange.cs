using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Ranges
{
    /// <summary>
    /// 整数范围
    /// </summary>
    public class LongRange : IEnumerable<long>, IRange<long>
    {
        /// <summary>
        /// 开始值
        /// </summary>
        public long Start { get; }

        /// <summary>
        /// 结束值
        /// </summary>
        public long End { get; }

        /// <summary>
        /// 步长值
        /// </summary>
        public long Step { get; }

        /// <summary>
        /// 获取索引器对象
        /// </summary>
        /// <returns></returns>
        public IEnumerator<long> GetEnumerator()
        {
            return new LongRangeEnumerable(Start, End, Step);
        }

        // 获取索引器对象
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>
        /// 整数范围
        /// </summary>
        /// <param name="start">开始值</param>
        /// <param name="end">结束值(不包含)</param>
        /// <param name="step">步长值</param>
        public LongRange(long start, long end, long step = 1)
        {
            Start = start;
            End = end;
            Step = step;
        }
    }

    /// <summary>
    /// 整数范围
    /// </summary>
    public class LongRangeEnumerable : IEnumerator<long>, IRange<long>
    {
        // 是否新队列
        private bool isNewEnumer = true;

        /// <summary>
        /// 开始值
        /// </summary>
        public long Start { get; }

        /// <summary>
        /// 结束值
        /// </summary>
        public long End { get; }

        /// <summary>
        /// 步长值
        /// </summary>
        public long Step { get; }

        /// <summary>
        /// 当前值
        /// </summary>
        public long Current { get; private set; }

        // 获取当前
        object IEnumerator.Current => Current;

        /// <summary>
        /// 整数范围
        /// </summary>
        /// <param name="start">开始值</param>
        /// <param name="end">结束值(不包含)</param>
        /// <param name="step">步长值</param>
        public LongRangeEnumerable(long start, long end, long step = 1)
        {
            if (step == 0) throw new Exception($"Step cannot be 0.");
            Start = start;
            End = end;
            Step = step;
            Current = start;
        }

        /// <summary>
        /// 读取下一个
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool MoveNext()
        {
            if (isNewEnumer)
            {
                isNewEnumer = false;
            }
            else
            {
                Current += Step;
            }
            if (Step > 0) return Current < End;
            return Current > End;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            isNewEnumer = true;
            Current = Start;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
