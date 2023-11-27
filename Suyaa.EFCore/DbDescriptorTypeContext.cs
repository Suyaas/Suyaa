using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore
{
    /// <summary>
    /// 类型数据库上下文
    /// </summary>
    public sealed class DbDescriptorTypeContext : DbDescriptorContext
    {
        // 所有类型
        private readonly IEnumerable<Type> _types;

        /// <summary>
        /// 动态数据库上下文
        /// </summary>
        /// <param name="descriptor"></param>
        /// <param name="options"></param>
        public DbDescriptorTypeContext(DbConnectionDescriptor descriptor, DbContextOptions options, IEnumerable<Type> types) : base(descriptor, options)
        {
            _types = types;
        }

        // 创建模型构建器
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var type in _types)
            {
                modelBuilder.Entity(type);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
