using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Safety.Dependency
{
    /// <summary>
    /// 异常处理器
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="ex"></param>
        void Handling(Exception ex);
    }
}
