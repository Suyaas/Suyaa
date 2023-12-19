using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Usables
{
    /// <summary>
    /// 可使用对象
    /// </summary>
    public interface IUsable
    {
        /// <summary>
        /// 被使用
        /// </summary>
        void OnUsed();
    }
}
