using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Ranges
{
    /// <summary>
    /// 范围
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRange<T>
        where T : struct
    {
        /// <summary>
        /// 开始值
        /// </summary>
        public T Start { get; }

        /// <summary>
        /// 结束值
        /// </summary>
        public T End { get; }

        /// <summary>
        /// 步长值
        /// </summary>
        public T Step { get; }
    }
}
