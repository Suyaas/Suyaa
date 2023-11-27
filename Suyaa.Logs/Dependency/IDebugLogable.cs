using Suyaa.Logs.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs
{

    /// <summary>
    /// 调试信息可记录对象
    /// </summary>
    public interface IDebugLogable
    {

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="evt">事件源</param>
        void Debug(string message, string? evt = null);

    }
}
