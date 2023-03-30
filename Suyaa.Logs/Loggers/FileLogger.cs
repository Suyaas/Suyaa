using Suyaa.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Suyaa.Logs.Loggers
{

    /// <summary>
    /// 文件日志
    /// </summary>
    public class FileLogger : ILogable
    {

        // 路径
        private string _path;

        /// <summary>
        /// 对象实例化
        /// </summary>
        /// <param name="path"></param>
        public FileLogger(string path)
        {
            _path = sy.IO.GetClosedPath(path);
        }

        /// <summary>
        /// 输出内容
        /// </summary>
        /// <param name="entity"></param>
        public void Log(LogInfo entity)
        {
            string content = $"*{sy.Time.Now.ToFullDateTimeString()}* [{entity.Level.ToString().ToUpper()}] ({entity.Event}) {entity.Message}";
            var t = sy.Time.Now;
            string path = sy.IO.GetClosedPath($"{_path}{t.Year}-{t.Month.ToString().PadLeft(2, '0')}");
            sy.IO.CreateFolder(path);
            string filePath = $"{path}{entity.Level.ToString().ToLower()}-{t.ToDateString()}.log";
            using (var f = sy.IO.OpenFile(filePath, System.IO.FileMode.OpenOrCreate))
            {
                f.Position = f.Length;
                f.WriteUtf8Line(content);
            }
        }
    }
}
