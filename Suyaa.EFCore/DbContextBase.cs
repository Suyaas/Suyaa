using Microsoft.EntityFrameworkCore;
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
    public abstract class DbContextBase : DbContext
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// EFCore重写上下文
        /// </summary>
        /// <param name="options"></param>
        /// <param name="connectionString"></param>
        public DbContextBase(DbContextOptions options, string connectionString) : base(options)
        {
            ConnectionString = connectionString;
        }

    }
}
