using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Multilingual.Dependency
{
    /// <summary>
    /// 语言包供应商
    /// </summary>
    public interface ILanguagePackProvider
    {
        /// <summary>
        /// 获取语言包集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<ILanguagePack> GetLanguagePacks();
    }
}
