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
    /// EFCore重写上下文
    /// </summary>
    public abstract class DbContextBase : DbContext, IDbContext
    {

        /// <summary>
        /// EFCore重写上下文
        /// </summary>
        /// <param name="options"></param>
        /// <param name="connectionString"></param>
        public DbContextBase(DbConnectionDescriptor descriptor, DbContextOptions options) : base(options)
        {
            ConnectionDescriptor = descriptor;
        }

        /// <summary>
        /// 数据库连接描述
        /// </summary>
        public DbConnectionDescriptor ConnectionDescriptor { get; }
    }
}
