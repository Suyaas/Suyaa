﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Net.Http;
using Suyaa;

namespace sy
{
    /* Http Upload */
    public static partial class Http
    {
        #region 异步接口

        /// <summary>
        /// 以Post方式上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paths"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> UploadResponseAsync(string url, List<string> paths, HttpOption option)
        {
            var client = GetClient();
            // 建立传输内容
            MultipartFormDataContent content = new MultipartFormDataContent();
            foreach (string path in paths)
            {
                content.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(path)), "file", System.IO.Path.GetFileName(path));
            }
            // 设置头
            option.Headers.SetCookies(option.Cookies);
            client.SetHeaders(option.Headers);
            content.SetHeaders(option.Headers);
            return await client.PostAsync(url, content);
        }

        /// <summary>
        /// 以Post方式上传多个文件并获取结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paths"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<string> UploadAsync(string url, List<string> paths, HttpOption option)
        {
            // 应答器
            using HttpResponseMessage response = await UploadResponseAsync(url, paths, option);
            // 触发应答事件
            if (!option.RaiseResponseEvent(response)) return string.Empty;
            // 判断状态并抛出异常
            if (option.IsEnsureStatus) response.EnsureSuccessStatusCode();
            // 返回数据结果
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 以Post方式上传多个文件并获取结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paths"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> UploadAsync(string url, List<string> paths, Action<HttpOption>? action = null)
        {
            // 创建选项
            using HttpOption option = new HttpOption();
            action?.Invoke(option);
            // 提交返回数据结果
            return await UploadAsync(url, paths, option);
        }

        /// <summary>
        /// 以Post方式上传单个文件并获取结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> UploadAsync(string url, string path, Action<HttpOption>? action = null)
            => await UploadAsync(url, new List<string> { path }, action);

        #endregion

        #region 同步接口

        /// <summary>
        /// 以Post方式上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paths"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static HttpResponseMessage UploadResponse(string url, List<string> paths, HttpOption option)
        {
            var client = GetClient();
            // 建立传输内容
            MultipartFormDataContent content = new MultipartFormDataContent();
            foreach (string path in paths)
            {
                content.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(path)), "file", System.IO.Path.GetFileName(path));
            }
            // 设置头
            option.Headers.SetCookies(option.Cookies);
            client.SetHeaders(option.Headers);
            content.SetHeaders(option.Headers);
            return client.PostAsync(url, content).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 以Post方式上传多个文件并获取结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paths"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static string Upload(string url, List<string> paths, HttpOption option)
        {
            // 应答器
            using HttpResponseMessage response = UploadResponse(url, paths, option);
            // 触发应答事件
            if (!option.RaiseResponseEvent(response)) return string.Empty;
            // 判断状态并抛出异常
            if (option.IsEnsureStatus) response.EnsureSuccessStatusCode();
            // 返回数据结果
            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 以Post方式上传多个文件并获取结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paths"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string Upload(string url, List<string> paths, Action<HttpOption>? action = null)
        {
            // 创建选项
            using HttpOption option = new HttpOption();
            action?.Invoke(option);
            // 提交返回数据结果
            return Upload(url, paths, option);
        }

        /// <summary>
        /// 以Post方式上传多个文件并获取结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string Upload(string url, string path, Action<HttpOption>? action = null)
            => Upload(url, new List<string> { path }, action);

        #endregion
    }
}
