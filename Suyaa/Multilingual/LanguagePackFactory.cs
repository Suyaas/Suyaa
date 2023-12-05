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
        private readonly List<ILanguagePack> _packs;

        /// <summary>
        /// 标准语言包工厂
        /// </summary>
        /// <param name="providers"></param>
        public LanguagePackFactory(IEnumerable<ILanguagePackProvider> providers)
        {
            _packs = new List<ILanguagePack>();
            // 注册所有的语言包
            foreach (var provider in providers)
            {
                var packs = provider.GetLanguagePacks();
                foreach (var pack in packs)
                {
                    // 跳过已经存在的语言包
                    if (_packs.Where(d => d.LanguageName == pack.LanguageName).Any()) continue;
                    _packs.Add(pack);
                }
            }
        }

        /// <summary>
        /// 语言包工厂
        /// </summary>
        /// <param name="provider"></param>
        public LanguagePackFactory(ILanguagePackProvider provider)
        {
            _packs = provider.GetLanguagePacks().ToList();
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
