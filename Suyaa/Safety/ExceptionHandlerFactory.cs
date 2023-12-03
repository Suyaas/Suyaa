using Suyaa.Safety.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Safety
{
    /// <summary>
    /// 匿名函数异常调用工厂
    /// </summary>
    public sealed class ActionExceptionHandlerFactory : IExceptionHandlerFactory
    {
        private readonly List<IExceptionHandler> _handlers;

        /// <summary>
        /// 匿名函数异常调用工厂
        /// </summary>
        public ActionExceptionHandlerFactory()
        {
            _handlers = new List<IExceptionHandler>();
        }

        /// <summary>
        /// 调用器集合
        /// </summary>
        public IList<IExceptionHandler> Handlers => _handlers;

        /// <summary>
        /// 注册调用函数
        /// </summary>
        /// <param name="action"></param>
        public void Reg(Action<Exception> action)
        {
            _handlers.Add(new ActionExceptionHandler(action));
        }

        /// <summary>
        /// 清理调用函数
        /// </summary>
        public void Clear() { _handlers.Clear(); }
    }
}
