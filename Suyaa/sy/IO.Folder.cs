using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Suyaa;

namespace sy
{

    /// <summary>
    /// 输入输出相关函数
    /// </summary>
    public static partial class IO
    {

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="existsException">存在时是否抛出错误</param>
        public static void CreateFolder(string path, bool existsException = false)
        {
            if (System.IO.Directory.Exists(path))
                if (existsException) throw new Exception("文件夹已经存在");
            System.IO.Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 检测文件夹是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool FolderExists(string path) { return System.IO.Directory.Exists(path); }

        /// <summary>
        /// 获取所属子文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetFolders(string path)
        {
            return System.IO.Directory.GetDirectories(path);
        }

        /// <summary>
        /// 获取所属文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string[] GetFiles(string path, [Optional] string pattern)
        {
            if (pattern.IsNullOrWhiteSpace())
            {
                return System.IO.Directory.GetFiles(path);
            }
            else
            {
                return System.IO.Directory.GetFiles(path, pattern);
            }
        }

        /// <summary>
        /// 删除文件夹及子文件夹和文件
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFolder(string path)
        {
            // 跳过不存在的目录
            if (!sy.IO.FolderExists(path)) return;
            // 获取所有文件
            var files = sy.IO.GetFiles(path);
            // 删除所有文件
            foreach (var file in files)
            {
                sy.IO.DeleteFile(file);
            }
            // 获取所有子目录
            var folders = sy.IO.GetFolders(path);
            // 删除所有子目录
            foreach (var folder in folders)
            {
                DeleteFolder(folder);
            }
            // 删除本级目录
            System.IO.Directory.Delete(path);
        }

    }

}
