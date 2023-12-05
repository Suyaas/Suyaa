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
        /// 不规范
        /// </summary>
        public const string KEY_NON_STANDARD = "NonStandard";
        /// <summary>
        /// 不规范异常
        /// </summary>
        public NonStandardException(string key, string message, params string[] parameters) : base(KEY_NON_STANDARD + "." + key, message, parameters) { }
    }
}
