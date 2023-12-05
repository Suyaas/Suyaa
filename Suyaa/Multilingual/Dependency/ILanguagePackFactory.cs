using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Multilingual.Dependency
{
    /// <summary>
    /// 语言工厂
    /// </summary>
    public interface ILanguagePackFactory
    {
        /// <summary>
        /// 获取语言包
        /// </summary>
        /// <param name="languageName"></param>
        /// <returns></returns>
        ILanguagePack? GetLanguagePack(string languageName);
    }
}
