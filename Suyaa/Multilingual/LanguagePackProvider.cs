using Suyaa.Multilingual.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Multilingual
{
    /// <summary>
    /// 标准语言包供应商
    /// </summary>
    public sealed class LanguagePackProvider : ILanguagePackProvider
    {
        private readonly List<LanguagePack> _packs;

        /// <summary>
        /// 标准语言包供应商
        /// </summary>
        public LanguagePackProvider()
        {
            _packs = new List<LanguagePack>();
        }

        /// <summary>
        /// 获取语言包集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ILanguagePack> GetLanguagePacks()
        {
            return _packs;
        }

        /// <summary>
        /// 添加内容
        /// </summary>
        /// <param name="languageName"></param>
        /// <param name="key"></param>
        /// <param name="content"></param>
        public void SetContent(string languageName, string key, string content)
        {
            var pack = _packs.Where(d => d.LanguageName == languageName).FirstOrDefault();
            // 为空自动添加语言包
            if (pack is null)
            {
                pack = new LanguagePack(languageName, new Dictionary<string, string>());
                _packs.Add(pack);
            }
            // 设置内容
            pack.SetContent(key, content);
        }

        /// <summary>
        /// 添加内容
        /// </summary>
        /// <param name="languageName"></param>
        /// <param name="keys"></param>
        public void SetContent(string languageName, IDictionary<string, string> keys)
        {
            var pack = _packs.Where(d => d.LanguageName == languageName).FirstOrDefault();
            // 已存在则移除
            if (pack != null) _packs.Remove(pack);
            pack = new LanguagePack(languageName, new Dictionary<string, string>());
            _packs.Add(pack);
        }
    }
}
