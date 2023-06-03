using Suyaa.Configure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.ProfileConfigs
{
    /// <summary>
    /// 配置对象
    /// </summary>
    public class Profile : List<Section>
    {
        /// <summary>
        /// 默认结点
        /// </summary>
        public Section DefaultSection { get; }

        // 解析单行
        private static Section ParseLine(Profile profile, Section section, string line)
        {
            // 跳过空行
            if (line.Trim().IsNullOrWhiteSpace()) return section;
            // 跳过注释
            if (line.StartsWith(";")) return section;
            if (line.StartsWith("#")) return section;
            if (line.StartsWith("["))
            {
                line = line.Trim();
                if (!line.EndsWith("]")) throw new ConfigException($"Section definition must end with ']'.");
                string sectionName = line.Substring(1, line.Length - 2);
                return profile.Add(sectionName);
            }
            // 解析 = 定义
            int index = line.IndexOf('=');
            if (index <= 0) throw new ConfigException("Missing key-value definition");
            string key = line.Substring(0, index);
            string value = line.Substring(index + 1);
            // 单独处理字符串
            string strValue = value.Trim();
            if (strValue.StartsWith("\"") && strValue.EndsWith("\""))
            {
                value = strValue.Substring(1, strValue.Length - 2);
            }
            section[key] = value;
            return section;
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static Profile Parse(string txt)
        {
            Profile profile = new Profile();
            Section currentSection = profile.DefaultSection;
            StringBuilder sb = new StringBuilder();
            char chr;
            for (int i = 0; i < txt.Length; i++)
            {
                chr = txt[i];
                switch (chr)
                {
                    // 跳过回车符
                    case '\r': break;
                    // 换行
                    case '\n':
                        // 解析单行数据
                        currentSection = ParseLine(profile, currentSection, sb.ToString());
                        sb.Clear();
                        break;
                    // 默认添加字符
                    default:
                        sb.Append(chr);
                        break;
                }
            }
            return profile;
        }

        /// <summary>
        /// 添加结点
        /// </summary>
        /// <param name="name"></param>
        public Section Add(string name)
        {
            Section section = new Section(this, name);
            this.Add(section);
            return section;
        }

        /// <summary>
        /// 名称校验
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal bool NameVerify(string name)
        {
            return !this.Where(d => d.Name == name).Any();
        }

        /// <summary>
        /// 获取结点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ConfigException"></exception>
        public Section? this[string name]
        {
            get
            {
                if (name.IsNullOrWhiteSpace()) throw new ConfigException($"Name is empty.");
                return this.Where(d => d.Name == name).FirstOrDefault();
            }
        }

        /// <summary>
        /// 配置对象
        /// </summary>
        public Profile()
        {
            this.DefaultSection = new Section(this);
            this.Add(this.DefaultSection);
        }
    }
}
