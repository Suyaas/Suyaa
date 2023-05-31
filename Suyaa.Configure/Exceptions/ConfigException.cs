using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Exceptions
{
    /// <summary>
    /// 配置文件异常
    /// </summary>
    public class ConfigException : Exception
    {
        /// <summary>
        /// 配置文件异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">内联异常</param>
        public ConfigException(string message, Exception? innerException = null) : base(message, innerException) { }
    }
}
