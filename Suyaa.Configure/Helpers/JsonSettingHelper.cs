using Suyaa.Configure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Suyaa.Configure.Helpers
{
    /// <summary>
    /// 基于Json的设置对象助手
    /// </summary>
    public static class JsonSettingHelper
    {
        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="overwrite"></param>
        public static void SaveToFile(this JsonSetting setting, string path, bool overwrite = false)
        {
            // 判断文件是否存在
            if (sy.IO.FileExists(path))
            {
                if (!overwrite) throw new ConfigException($"文件已经存在");
            }
            string json = JsonSerializer.Serialize(setting);
            sy.IO.WriteUtf8FileContent(path, json);
        }
    }
}
