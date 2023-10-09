using Suyaa.Data;
using Suyaa.EFCore.Dbsets;
using Suyaa.Data.Dependency;

namespace Suyaa.EFCore.Helpers
{
    ///// <summary>
    ///// Egg专用数据库上下文
    ///// </summary>
    //public static class DbContextHelper
    //{

    //    /// <summary>
    //    /// 创建数据仓库
    //    /// </summary>
    //    /// <param name="context"></param>
    //    /// <param name="entity"></param>
    //    /// <param name="key"></param>
    //    /// <returns></returns>
    //    private static object CreateRepository(DbContextBase context, Type entity, Type key)
    //    {
    //        Type tp = typeof(Dbsets.Repository<,>);
    //        //指定泛型的具体类型
    //        Type tpNew = tp.MakeGenericType(new Type[] { entity, key });
    //        //创建一个list返回
    //        var obj = sy.Assembly.Create(tpNew, new object[] { context });
    //        if (obj is null) throw new DatabaseException($"类型'{tpNew.FullName}'实例化失败");
    //        return obj;
    //    }

    //    /// <summary>
    //    /// 生成数据仓库集合
    //    /// </summary>
    //    /// <param name="context"></param>
    //    public static List<object> GetRepositories(this DbContextBase context)
    //    {
    //        List<object> list = new List<object>();
    //        var pros = context.GetType().GetProperties();
    //        foreach (var pro in pros)
    //        {
    //            var tp = pro.PropertyType;
    //            string tpName = tp.Namespace + "." + tp.Name;
    //            if (tpName != Consts.TYPE_NAME_DBSET) continue;
    //            var tpEntity = tp.GenericTypeArguments[0];
    //            var tpEntityType = tpEntity.GetEntityType();
    //            if (tpEntityType is null) continue;
    //            var tpEntityKeyType = tpEntityType.GenericTypeArguments[0];
    //            var obj = CreateRepository(context, tpEntity, tpEntityKeyType);
    //            list.Add(obj);
    //        }
    //        return list;
    //    }

    //    /// <summary>
    //    /// 生成数据仓库集合
    //    /// </summary>
    //    /// <param name="context"></param>
    //    public static List<RepositoryInfo> GetRepositoryInfos(this DbContextBase context)
    //    {
    //        return context.GetType().GetRepositoryInfos();
    //    }

    //    /// <summary>
    //    /// 创建
    //    /// </summary>
    //    /// <param name="context"></param>
    //    public static bool EnsureCreated<T>(this DbContextBase context) where T : IDbCreater, new()
    //    {
    //        T creater = new T();
    //        return creater.EnsureCreated(context).Result;
    //    }

    //    /// <summary>
    //    /// 获取数据库类型
    //    /// </summary>
    //    /// <param name="context"></param>
    //    /// <returns></returns>
    //    public static DatabaseTypes GetDbType(this DbContextBase context) => context.Database.ProviderName switch
    //    {
    //        var h when h == "Microsoft.EntityFrameworkCore.Sqlite" => DatabaseTypes.Sqlite,
    //        _ => throw new Exception($"不支持的数据库提供商'{context.Database.ProviderName}'")
    //    };

    //    /// <summary>
    //    /// 获取数据库连接信息
    //    /// </summary>
    //    /// <param name="context"></param>
    //    /// <returns></returns>
    //    /// <exception cref="Exception"></exception>
    //    public static IDatabase GetDatabaseConnectionInfo(this DbContextBase context)
    //    {
    //        var type = context.GetDbType();
    //        var infoType = type switch
    //        {
    //            var h when h == DatabaseTypes.PostgreSQL => sy.Assembly.FindType("Suyaa.Data.PostgreSQL.NpgsqlConnectionInfo", sy.Assembly.ExecutionDirectory),
    //            var h when h == DatabaseTypes.Sqlite => sy.Assembly.FindType("Suyaa.Data.Sqlite.SqliteConnectionInfo", sy.Assembly.ExecutionDirectory),
    //            _ => throw new Exception($"Unsupported database '{type}'.")
    //        };
    //        if (infoType is null) throw new DatabaseException($"Database info '{type}' type not found.");
    //        var info = Activator.CreateInstance(infoType, new object[] { context.ConnectionString });
    //        if (info is null) throw new DatabaseException($"Database info '{type}' object create fail.");
    //        return (IDatabase)info;
    //    }

    //    /// <summary>
    //    /// 获取数据库连接
    //    /// </summary>
    //    /// <param name="context"></param>
    //    /// <returns></returns>
    //    /// <exception cref="Exception"></exception>
    //    public static DatabaseConnection GetDatabaseConnection(this DbContextBase context)
    //    {
    //        return new DatabaseConnection(context.GetDatabaseConnectionInfo());
    //    }

    //    /// <summary>
    //    /// 获取Sql语句供应器
    //    /// </summary>
    //    /// <param name="context"></param>
    //    public static IDatabaseProvider GetSqlProvider(this DbContextBase context)
    //    {
    //        IDatabase info = context.GetDatabaseConnectionInfo();
    //        Type? type = Type.GetType(info.ProviderName);
    //        if (type is null) throw new DatabaseException($"Provider '{info.ProviderName}' not found.");
    //        return type.Create<IDatabaseProvider>();
    //    }
    //}
}
