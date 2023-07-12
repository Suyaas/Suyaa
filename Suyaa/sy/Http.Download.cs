using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Net.Http;
using Suyaa;
using System.IO;

namespace sy
{
    /* Http Download */
    public static partial class Http
    {

        /// <summary>
        /// 以Get方式下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task DownloadAsync(string url, string path, Action<HttpOption>? action = null)
        {
            using HttpOption option = new HttpOption();
            if (action != null) action(option);
            // 应答器
            using HttpResponseMessage response = await GetResponseAsync(url, option);
            // 触发应答事件
            option.RaiseResponseEvent(response);
            // 判断状态并抛出异常
            if (option.IsEnsureStatus) response.EnsureSuccessStatusCode();
            // 建立缓冲区
            byte[] buffer = new byte[4096];
            long receiveLen = 0;
            int len = 0;
            // 创建文件操作流
            using FileStream fs = System.IO.File.Open(path, FileMode.CreateNew);
            using var stream = await response.Content.ReadAsStreamAsync();
            var contentLength = response.Content.Headers.ContentLength.GetValueOrDefault();
            do
            {
                // 读取数据
                len = stream.Read(buffer, 0, buffer.Length);
                if (len > 0)
                {
                    // 统计并写入数据
                    receiveLen += len;
                    fs.Write(buffer, 0, len);
                    // 触发下载变更
                    option.RaiseDownloadEvent(new HttpDownloadInfo(contentLength, receiveLen, false));
                }
            } while (len > 0);
            // 触发下载完成
            option.RaiseDownloadEvent(new HttpDownloadInfo(contentLength, receiveLen, true));
            // 清理数据
            fs.Close();
            buffer = new byte[0];
        }


        /// <summary>
        /// 以Post方式上传多个文件并获取结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static void Download(string url, string path, Action<HttpOption>? action = null)
            => DownloadAsync(url, path, action).GetAwaiter().GetResult();
    }
}
