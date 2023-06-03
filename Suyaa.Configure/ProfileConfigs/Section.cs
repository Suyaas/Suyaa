using Suyaa.Configure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.ProfileConfigs
{
    /// <summary>
    /// 配置结点
    /// </summary>
    public class Section : StringKeyDictionary<string>
    {
        // 配置对象
        private readonly Profile _profile;
        // 名称
        private string _name;

        /// <summary>
        /// 结点名称
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                if (value.IsNullOrWhiteSpace()) throw new ConfigException($"Name is empty.");
                if (!_profile.NameVerify(value)) throw new ConfigException($"Name '{value}' existed.");
                _name = value;
            }
        }

        /// <summary>
        /// 配置结点
        /// </summary>
        public Section(Profile profile, string? name = null)
        {
            _profile = profile;
            _name = name ?? string.Empty;
            if (!_profile.NameVerify(_name)) throw new ConfigException($"Name '{_name}' existed.");
        }
    }
}
