using Suyaa.Configure.Exceptions;
using System.Text.Json;

namespace Suyaa.Configure
{
    /// <summary>
    /// 基于Json的配置管理器
    /// </summary>
    public class JsonConfigManager<T> : IConfigManager<T>
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
        /// 基于Json的设置对象
        /// </summary>
        /// <param name="path"></param>
        public JsonConfigManager(string path)
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
                string json = sy.IO.ReadUtf8FileContent(path);
                this.Config = sy.Json.Deserialize<T>(json);
            }
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        public void Save()
        {
            // 保存文件
            sy.IO.WriteUtf8FileContent(this.Path, sy.Json.Serialize(this.Config));
        }
    }
}