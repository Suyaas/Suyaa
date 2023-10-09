using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// 不规范异常
    /// </summary>
    public sealed class NonStandardException : Exception
    {
        /// <summary>
        /// 不规范异常
        /// </summary>
        public NonStandardException(string message) : base("Non Standard: " + message) { }
    }
}
