using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Usables.Exceptions
{
    /// <summary>
    /// 未使用异常
    /// </summary>
    public class NotUsedException : KeyException
    {
        /// <summary>
        /// 存在
        /// </summary>
        public const string KEY_NOT_USED = "NotUsed";

        /// <summary>
        /// 未使用异常
        /// </summary>
        /// <param name="type"></param>
        public NotUsedException(Type type) : base(KEY_NOT_USED, "{0} not used.", type.FullName) { }
    }

    /// <summary>
    /// 未使用异常
    /// </summary>
    public class NotUsedException<T> : NotUsedException
    {
        /// <summary>
        /// 未使用异常
        /// </summary>
        public NotUsedException() : base(typeof(T)) { }
    }
}
