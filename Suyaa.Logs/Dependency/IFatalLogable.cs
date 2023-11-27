using Suyaa.Logs.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs
{

    /// <summary>
    /// 致命信息可记录对象
    /// </summary>
    public interface IFatalLogable
    {

        /// <summary>
        /// 记录致命信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        void Fatal(string message, string? evt = null);

    }
}
