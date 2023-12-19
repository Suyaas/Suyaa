using Suyaa.Usables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// 使用入口
    /// </summary>
    public struct Use<T> where T : class
    {
        // 单例全局变量
        private static T? _single;

        /// <summary>
        /// 获取一个单例对象
        /// </summary>
        /// <returns></returns>
        public static T Single()
        {
            return _single ??= Create();
        }

        /// <summary>
        /// 获取单例或默认
        /// </summary>
        /// <returns></returns>
        public static T? SingleOrDefault() => _single;

        /// <summary>
        /// 设置单例对象
        /// </summary>
        /// <param name="single"></param>
        public static void Single(T single)
        {
            if (_single != null)
            {
                if (_single is IDisposable disposable) disposable.Dispose();
                _single = null;
            }
            _single = single;
            if (_single is IUsable usable) usable.OnUsed();
        }

        /// <summary>
        /// 创建一个新对象
        /// </summary>
        /// <returns></returns>
        public static T Create()
        {
            return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// 创建一个新对象
        /// </summary>
        /// <returns></returns>
        public static T Create(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        /// <summary>
        /// 获取可使用对象
        /// </summary>
        public static Usable<T> Usable => Use<Usable<T>>.Single();

        /// <summary>
        /// 获取轻量化对象
        /// </summary>
        public static Toy<T> Toy => Use<Toy<T>>.Single();
    }
}





