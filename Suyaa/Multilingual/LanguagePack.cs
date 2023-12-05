using Suyaa.Multilingual.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Multilingual
{
    /// <summary>
    /// 语言包
    /// </summary>
    public sealed class LanguagePack : ILanguagePack
    {
        private readonly IDictionary<string, string> _keys;

        /// <summary>
        /// 语言包
        /// </summary>
        /// <param name="languageName"></param>
        /// <param name="keys"></param>
        public LanguagePack(
            string languageName,
            IDictionary<string, string> keys
            )
        {
            _keys = keys;
            LanguageName = languageName;
        }

        /// <summary>
        /// 语言名称
        /// </summary>
        public string LanguageName { get; }

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="key"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="NotExistException"></exception>
        public string GetContent(string key, params string[] args)
        {
            if (!_keys.ContainsKey(key)) throw new NotExistException($"Key '{key}'");
            return string.Format(_keys[key], args);
        }
    }
}
