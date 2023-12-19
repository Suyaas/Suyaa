using Suyaa.Usables.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Usables
{
    /// <summary>
    /// 可使用描述
    /// </summary>
    public class Usable<T> : IUsable where T : class
    {
        // 类型
        private Type? _type;

        /// <summary>
        /// 被使用
        /// </summary>
        public virtual void OnUsed()
        {
            _type = typeof(T);
        }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type => _type ?? throw new NotUsedException<T>();
    }
}
