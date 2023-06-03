using Suyaa.Configure.Exceptions;
using Suyaa.Configure.ProfileConfigs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Helpers
{
    /// <summary>
    /// 配置对象助手
    /// </summary>
    public static class ProfileHelper
    {
        // 填充到对象
        private static object? CreateObject(Profile profile, string name, Type type)
        {
            // 获取所有属性
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // 获取处理结点
            Section? section;
            if (name.IsNullOrWhiteSpace())
            {
                section = profile.DefaultSection;
            }
            else
            {
                section = profile[name];
                if (section is null) return null;
            }
            // 创建对象
            var obj = type.Create();
            // 遍历属性
            foreach (var pro in pros)
            {
                // 设置对象属性
                if (pro.PropertyType.GetTypeCode() == TypeCode.Object)
                {
                    // 单独处理顶层对象
                    if (name.IsNullOrWhiteSpace())
                    {
                        var value = CreateObject(profile, pro.Name, pro.PropertyType);
                        if (value is null) continue;
                        pro.SetValue(obj, value);
                        continue;
                    }
                    else
                    {
                        var value = CreateObject(profile, name + "." + pro.Name, pro.PropertyType);
                        if (value is null) continue;
                        pro.SetValue(obj, value);
                        continue;
                    }
                }
                // 设置属性内容
                if (!section.ContainsKey(pro.Name)) continue;
                pro.SetValue(obj, section[pro.Name].ConvertTo(pro.PropertyType));
            }
            return obj;
        }

        /// <summary>
        /// 配置对象转为普通对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="profile"></param>
        /// <returns></returns>
        public static T ConvertToConfig<T>(this Profile profile)
            where T : IConfig
        {
            var type = typeof(T);
            var obj = CreateObject(profile, string.Empty, type);
            if (obj is null) throw new ConfigException($"Convert to '{type.FullName}' fail.");
            return (T)obj;
        }

        /// <summary>
        /// 配置对象转为普通对象
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ConvertToConfig(this Profile profile, Type type)
        {
            var obj = CreateObject(profile, string.Empty, type);
            if (obj is null) throw new ConfigException($"Convert to '{type.FullName}' fail.");
            return obj;
        }
    }
}
