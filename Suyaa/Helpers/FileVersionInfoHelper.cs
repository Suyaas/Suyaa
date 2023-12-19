using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Suyaa
{
    /// <summary>
    /// 文件版本信息助手
    /// </summary>
    public static class FileVersionInfoHelper
    {
        /// <summary>
        /// 获取产品版本
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string GetProductVersion(this FileVersionInfo info)
            => info.ProductMajorPart + "." + info.ProductMinorPart + "." + info.ProductBuildPart + "." + info.ProductPrivatePart;
    }
}
