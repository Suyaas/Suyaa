using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Helpers;
using System;

namespace Suyaa.EFCore.Helpers
{
    /// <summary>
    /// 建模助手
    /// </summary>
    public static class ModelBuilderHelper
    {
        public static void BuildToLowerName<T>(this ModelBuilder modelBuilder) where T : DbContextBase
        {
            var list = typeof(T).GetRepositoryInfos();
            foreach (var item in list)
            {
                Type entityType = item.ObjectType.GenericTypeArguments[0];
                modelBuilder.Entity(entityType).ToTable(entityType.Name.ToLowerDbName());
            }
        }
    }
}
