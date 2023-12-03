using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Safety.Dependency
{
    /// <summary>
    /// 异常处理工厂
    /// </summary>
    public interface IExceptionHandlerFactory
    {
        /// <summary>
        /// 调用器集合
        /// </summary>
        IList<IExceptionHandler> Handlers { get; }
    }
}
