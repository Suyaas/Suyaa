using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text.Json;
using Suyaa.Configure;
using Suyaa;

namespace sy
{

    /// <summary>
    /// 配置
    /// </summary>
    public static class Configure
    {
        /// <summary>
        /// 从文件中加载Json配置
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public static JsonConfigManager<T> LoadJsonSetting<T>(string path) where T : IConfig, new()
            => new JsonConfigManager<T>(path);

        /// <summary>
        /// 从文件中加载ini/conf配置
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public static ProfileConfigManager<T> LoadProfileSetting<T>(string path) where T : IConfig, new()
            => new ProfileConfigManager<T>(path);
    }
}
