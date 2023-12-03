using Suyaa.Safety.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Safety
{
    /// <summary>
    /// 匿名异常处理器
    /// </summary>
    public sealed class ActionExceptionHandler : IExceptionHandler
    {
        private readonly Action<Exception> _action;

        /// <summary>
        /// 匿名异常处理器
        /// </summary>
        /// <param name="action"></param>
        public ActionExceptionHandler(Action<Exception> action)
        {
            _action = action;
        }

        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="ex"></param>
        public void Handling(Exception ex)
        {
            _action(ex);
        }
    }
}
