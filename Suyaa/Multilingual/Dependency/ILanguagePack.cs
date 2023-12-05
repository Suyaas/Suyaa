using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Multilingual.Dependency
{
    /// <summary>
    /// 多语言字典
    /// </summary>
    public interface ILanguagePack
    {
        /// <summary>
        /// 语言名称
        /// </summary>
        string LanguageName { get; }
        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="key"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        string GetContent(string key, params string[] args);
    }
}
