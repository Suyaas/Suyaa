using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs.Dependency
{

    /// <summary>
    /// 通用可日志对象
    /// </summary>
    public interface ICommonLogable
    {

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log"></param>
        void Log(LogDescriptor log);

    }
}
