using Suyaa.Logs.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs
{

    /// <summary>
    /// 错误信息可记录对象
    /// </summary>
    public interface IErrorLogable
    {

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        void Error(string message, string? evt = null);

    }
}
