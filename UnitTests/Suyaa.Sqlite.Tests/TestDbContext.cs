using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.EFCore;
using Suyaa.EFCore.Helpers;
using Suyaa.EFCore.SqlServer;
using Suyaa.Sqlite.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Sqlite.Tests
{
    /// <summary>
    /// 测试连接
    /// </summary>
    public class TestDbContext : SqlServerContext
    {

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<People> Peoples { get; set; }

#nullable disable
        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="options"></param>
        public TestDbContext(DbConnectionDescriptor descriptor) : base(descriptor)
        {
        }
#nullable enable

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildToLowerName<TestDbContext>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
