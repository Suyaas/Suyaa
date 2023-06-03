using Suyaa.Configure.Helpers;
using Suyaa.Configure.ProfileConfigs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure
{
    /// <summary>
    /// INI/Conf配置文件管理
    /// </summary>
    public class ProfileConfigManager<T> : IConfigManager<T>
        where T : IConfig, new()
    {
        /// <summary>
        /// 配置路径
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// 配置内容
        /// </summary>
        public T Config { get; set; }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            // 保存内容
            sy.IO.WriteUtf8FileContent(this.Path, this.Config.GetProfileString());
        }

        /// <summary>
        /// 基于Json的设置对象
        /// </summary>
        /// <param name="path"></param>
        public ProfileConfigManager(string path)
        {
            this.Path = path;
            if (!sy.IO.FileExists(this.Path))
            {
                // 创建文件夹
                string folder = sy.IO.GetFolderPath(this.Path);
                sy.IO.CreateFolder(folder);
                // 填充默认配置
                this.Config = new T();
                this.Config.Default();
                // 保存
                this.Save();
            }
            else
            {
                // 读取文件并加载
                string content = sy.IO.ReadUtf8FileContent(path);
                var profile = Profile.Parse(content);
                var type = typeof(T);
                this.Config = profile.ConvertToConfig<T>();
            }
        }
    }
}
