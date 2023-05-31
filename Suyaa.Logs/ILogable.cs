using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Logs
{

    /// <summary>
    /// 记录器接口
    /// </summary>
    public interface ILogable
    {

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="entity"></param>
        void Log(LogInfo entity);

    }
}
