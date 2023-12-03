using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// 不规范异常
    /// </summary>
    public sealed class NonStandardException : KeyException
    {
        /// <summary>
        /// 不规范异常
        /// </summary>
        public NonStandardException(string key, string message, params string[] parameters) : base("Exception.NonStandard." + key, message, parameters) { }
    }
}
