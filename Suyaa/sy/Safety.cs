using Suyaa.Safety;
using Suyaa.Safety.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using Suyaa;

namespace sy
{
    /// <summary>
    /// 安全执行
    /// </summary>
    public static class Safety
    {
        // 异常调用工厂
        private static IExceptionHandlerFactory? _factory;

        /// <summary>
        /// 获取当前异常调用工厂
        /// </summary>
        /// <returns></returns>
        public static IExceptionHandlerFactory GetCurrentFactory()
        {
            if (_factory is null) throw new NullException<IExceptionHandlerFactory>();
            return _factory;
        }

        /// <summary>
        /// 设置当前异常调用工厂
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static void SetCurrentFactory(IExceptionHandlerFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// 以安全的的方式调用
        /// </summary>
        /// <returns></returns>
        public static SafetyInvoker GetInvoker()
        {
            return new SafetyInvoker(GetCurrentFactory());
        }

        /// <summary>
        /// 以安全的的方式调用
        /// </summary>
        /// <returns></returns>
        public static SafetyInvoker GetInvoker(IExceptionHandlerFactory factory)
        {
            return new SafetyInvoker(factory);
        }

        /// <summary>
        /// 以安全的的方式调用
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool Invoke(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 以安全的的方式调用
        /// </summary>
        /// <param name="action"></param>
        /// <param name="actionException"></param>
        /// <returns></returns>
        public static bool Invoke(Action action, Action<Exception> actionException)
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
                return false;
            }
        }
    }
}
