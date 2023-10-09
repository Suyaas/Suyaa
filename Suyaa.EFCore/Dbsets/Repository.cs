using Suyaa.Data;
using Microsoft.EntityFrameworkCore;
using Suyaa.EFCore.Helpers;
using Suyaa.EFCore.Dependency;

namespace Suyaa.EFCore.Dbsets
{
    ///// <summary>
    ///// 数据仓库
    ///// </summary>
    //public class Repository<TClass, TId> : Data.Repositories.Repository<TClass, TId>, IRepository<TClass, TId>
    //    where TClass : class, IEntity<TId>
    //    where TId : notnull
    //{

    //    /// <summary>
    //    /// DB上下文
    //    /// </summary>
    //    public DbContextBase? Context { get; }

    //    /// <summary>
    //    /// 实体集合
    //    /// </summary>
    //    public DbSet<TClass>? DbSet { get; }

    //    /// <summary>
    //    /// 对象实例化
    //    /// </summary>
    //    /// <param name="context"></param>
    //    public Repository(DbContextBase context) : base(context.GetDatabaseConnection())
    //    {
    //        Context = context;
    //        DbSet = context.Set<TClass>();
    //    }

    //    /// <summary>
    //    /// 获取查询器
    //    /// </summary>
    //    /// <returns></returns>
    //    public IQueryable<TClass> Query()
    //    {
    //        if (DbSet is null) throw new DatabaseException($"'DbSet'尚未定义");
    //        return DbSet;
    //    }
    //}
}
