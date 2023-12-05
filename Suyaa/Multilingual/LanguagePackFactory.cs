using Suyaa.Multilingual.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Multilingual
{
    /// <summary>
    /// 语言包工厂
    /// </summary>
    public sealed class LanguagePackFactory : ILanguagePackFactory
    {
        private readonly IEnumerable<ILanguagePack> _packs;

        /// <summary>
        /// 语言包工厂
        /// </summary>
        /// <param name="packs"></param>
        public LanguagePackFactory(IEnumerable<ILanguagePack> packs)
        {
            _packs = packs;
        }

        /// <summary>
        /// 获取语言包
        /// </summary>
        /// <param name="languageName"></param>
        /// <returns></returns>
        public ILanguagePack? GetLanguagePack(string languageName)
        {
            return _packs.Where(d => d.LanguageName == languageName).FirstOrDefault();
        }
    }
}
