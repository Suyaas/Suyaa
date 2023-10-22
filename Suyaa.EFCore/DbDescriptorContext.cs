using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Suyaa.EFCore
{
    /// <summary>
    /// 带描述的数据库上下文
    /// </summary>
    public abstract class DbDescriptorContext : DbContext, IDbDescriptorContext
    {

        /// <summary>
        /// EFCore重写上下文
        /// </summary>
        /// <param name="options"></param>
        /// <param name="connectionString"></param>
        public DbDescriptorContext(DbConnectionDescriptor descriptor, DbContextOptions options) : base(options)
        {
            ConnectionDescriptor = descriptor;
            Options = options;
        }

        /// <summary>
        /// 数据库连接描述
        /// </summary>
        public DbConnectionDescriptor ConnectionDescriptor { get; }

        /// <summary>
        /// 数据库上下文配置
        /// </summary>
        public DbContextOptions Options { get; }
    }
}
