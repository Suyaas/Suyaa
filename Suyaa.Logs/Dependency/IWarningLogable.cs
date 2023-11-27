using Suyaa.Logs.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs
{

    /// <summary>
    /// 警告信息可记录对象
    /// </summary>
    public interface IWarningLogable
    {

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="evt"></param>
        void Warn(string message, string? evt = null);

    }
}
