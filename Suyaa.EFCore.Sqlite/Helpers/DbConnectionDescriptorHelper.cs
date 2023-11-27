using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.SqlServer.Helpers
{
    /// <summary>
    /// 主机数据库上下文配置助手
    /// </summary>
    public static class DbConnectionDescriptorHelper
    {
        /// <summary>
        /// 获取Sqlite
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static DbContextOptions GetSqliteContextOptions(this DbConnectionDescriptor descriptor)
        {
            if (descriptor.DatabaseType != DbTypes.MicrosoftSqlServer) throw new DbException($"DatabaseType '{descriptor.DatabaseType}' not supported.");
            // 添加数据库上下文配置
            var optionsBuilder = new DbContextOptionsBuilder<DbDescriptorContext>();
            optionsBuilder.UseSqlite(descriptor.ToConnectionString());
            return optionsBuilder.Options;
        }
    }
}
