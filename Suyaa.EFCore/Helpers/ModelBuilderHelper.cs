using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Helpers;
using System;
using System.Reflection;

namespace Suyaa.EFCore.Helpers
{
    /// <summary>
    /// 建模助手
    /// </summary>
    public static class ModelBuilderHelper
    {
        public static void BuildToLowerName<T>(this ModelBuilder modelBuilder) where T : DbDescriptorContext
        {
            var list = typeof(T).GetRepositoryInfos();
            foreach (var item in list)
            {
                // 设置表名
                Type entityType = item.ObjectType.GenericTypeArguments[0];
                string tableName = entityType.GetTableName();
                modelBuilder.Entity(entityType).ToTable(tableName);
                // 设置字段名称
                var pros = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var property in pros)
                {
                    modelBuilder.Entity(entityType).Property(property.Name).HasColumnName(property.GetColumnName());
                }
            }
        }
    }
}
