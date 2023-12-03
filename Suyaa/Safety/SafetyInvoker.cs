using Suyaa.Safety.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Safety
{
    /// <summary>
    /// 安全调用器
    /// </summary>
    public sealed class SafetyInvoker
    {
        private readonly IExceptionHandlerFactory _factory;

        /// <summary>
        /// 安全调用器
        /// </summary>
        /// <param name="factory"></param>
        public SafetyInvoker(IExceptionHandlerFactory factory)
        {
            _factory = factory;
        }

        // 异常处理执行
        private void ExceptionHandling(Exception ex)
        {
            if (_factory is null) return;
            foreach (var handler in _factory.Handlers)
            {
                handler.Handling(ex);
            }
        }

        /// <summary>
        /// 以安全的的方式调用
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Invoke(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                // 从工厂执行处理
                ExceptionHandling(ex);
                return false;
            }
        }

        /// <summary>
        /// 以安全的的方式调用
        /// </summary>
        /// <param name="action"></param>
        /// <param name="actionException"></param>
        /// <returns></returns>
        public bool Invoke(Action action, Action<Exception> actionException)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                // 执行自定义调用
                actionException(ex);
                // 从工厂执行处理
                ExceptionHandling(ex);
                return false;
            }
        }
    }
}
