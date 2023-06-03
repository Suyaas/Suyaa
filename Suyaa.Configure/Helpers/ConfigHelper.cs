using Suyaa.Configure.ProfileConfigs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Helpers
{
    /// <summary>
    /// 配置助手
    /// </summary>
    public static class ConfigHelper
    {
        private static string GetProfileString(object obj, string name)
        {
            StringBuilder sb = new StringBuilder();
            // 获取类型
            var type = obj.GetType();
            // 获取所有属性
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // 输出描述
            var objDescription = type.GetCustomAttribute<DescriptionAttribute>();
            if (objDescription != null)
            {
                sb.AppendLine($"# {objDescription.Description}");
            }
            // 输出对象结点
            if (!name.IsNullOrWhiteSpace()) sb.AppendLine($"[{name}]");
            // 单独输非对象值
            foreach (var pro in pros)
            {
                // 输出所有非对象属性
                if (pro.PropertyType.GetTypeCode() != TypeCode.Object)
                {
                    // 输出描述
                    var proDescription = pro.GetCustomAttribute<DescriptionAttribute>();
                    if (proDescription != null)
                    {
                        sb.AppendLine($"# {proDescription.Description}");
                    }
                    // 判断是否为字符串
                    if (pro.PropertyType.GetTypeCode() == TypeCode.String)
                    {
                        var strValue = pro.GetValue(obj);
                        if (strValue != null)
                        {
                            sb.AppendLine($"{pro.Name}=\"{strValue.ToString()}\"");
                        }
                        continue;
                    }
                    // 处理标准逻辑
                    var value = pro.GetValue(obj);
                    if (value != null)
                    {
                        sb.AppendLine($"{pro.Name}={value.ToString()}");
                    }
                }
            }
            // 单独输出对象
            foreach (var pro in pros)
            {
                // 输出所有非对象属性
                if (pro.PropertyType.GetTypeCode() == TypeCode.Object)
                {
                    var proObject = pro.GetValue(obj);
                    if (proObject != null)
                    {
                        if (sb.Length > 0) sb.AppendLine();
                        // 输出描述
                        var proDescription = pro.GetCustomAttribute<DescriptionAttribute>();
                        if (proDescription != null)
                        {
                            sb.AppendLine($"# {proDescription.Description}");
                        }
                        sb.Append(GetProfileString(proObject, name.IsNullOrWhiteSpace() ? pro.Name : name + "." + pro.Name));
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取配置对象字符串
        /// </summary>
        /// <returns></returns>
        public static string GetProfileString(this object obj)
        {
            return GetProfileString(obj, string.Empty);
        }
    }
}
