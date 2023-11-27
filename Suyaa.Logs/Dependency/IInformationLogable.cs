using Suyaa.Logs.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs
{

    /// <summary>
    /// 普通信息记录供应商
    /// </summary>
    public interface IInformationLogable
    {

        /// <summary>
        /// 记录普通信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="evt">事件源</param>
        void Info(string message, string? evt = null);

    }
}
