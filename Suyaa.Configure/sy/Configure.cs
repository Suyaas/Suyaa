﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text.Json;
using Suyaa.Configure;
using Suyaa.Helpers;

namespace sy
{

    /// <summary>
    /// 配置
    /// </summary>
    public static class Configure
    {
        /// <summary>
        /// 从字符串中加载Json配置
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static T? LoadJsonSettingFromString<T>(string? json) where T : JsonSetting
        {
            if (json is null) return null;
            if (json.IsNullOrWhiteSpace()) return null;
            return JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// 从文件路径中加载Json配置
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static T? LoadJsonSettingFromFile<T>(string path) where T : JsonSetting
        {
            string json = sy.IO.ReadUtf8FileContent(path);
            return LoadJsonSettingFromString<T>(json);
        }
    }
}
