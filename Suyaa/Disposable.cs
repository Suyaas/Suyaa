using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// 可主动释放对象
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        // 是否已经释放
        private bool _disposed;

        /// <summary>
        /// 可主动释放对象
        /// </summary>
        public Disposable()
        {
            _disposed = false;
        }

        /// <summary>
        /// 托管资源释放
        /// </summary>
        protected virtual void OnManagedDispose() { }

        /// <summary>
        /// 托管资源释放
        /// </summary>
        protected virtual void OnUnmanagedDispose() { }


        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            // 过滤已经释放的对象
            if (_disposed) return;
            // 设置
            _disposed = true;
            // 释放托管资源
            OnManagedDispose();
        }

        /// <summary>
        /// 构析函数
        /// </summary>
        ~Disposable()
        {
            this.Dispose();
            this.OnUnmanagedDispose();
        }
    }
}
