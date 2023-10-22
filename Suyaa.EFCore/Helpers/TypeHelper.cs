using Suyaa.Data;
using Suyaa.EFCore.Dbsets;

namespace Suyaa.EFCore.Helpers
{
    /// <summary>
    /// 类型助手
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// 获取实例基类定义类型
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public static Type? GetEntityType(this Type tp)
        {
            string tpName = tp.GetTopName();
            if (tpName == Consts.TYPE_NAME_ENTITY) return tp;
            if (tp.BaseType is null) return null;
            return GetEntityType(tp.BaseType);
        }

        /// <summary>
        /// 生成数据仓库集合
        /// </summary>
        /// <param name="type"></param>
        public static List<RepositoryInfo> GetRepositoryInfos(this Type type)
        {
            if (!type.IsBased<DbDescriptorContext>()) throw new DbException($"类型 '{type.FullName}' 不是继承 'DbContextBase' 类型。");
            List<RepositoryInfo> list = new List<RepositoryInfo>();
            var pros = type.GetProperties();
            foreach (var pro in pros)
            {
                var tp = pro.PropertyType;
                string tpName = tp.Namespace + "." + tp.Name;
                if (tpName != Consts.TYPE_NAME_DBSET) continue;
                // 获取实例类型
                var tpEntity = tp.GenericTypeArguments[0];
                var tpEntityType = tpEntity.GetEntityType();
                if (tpEntityType is null) continue;
                // 获取主键类型
                var tpEntityKeyType = tpEntityType.GenericTypeArguments[0];
                //// 创建接口类型
                //var interfaceType = typeof(IRepository<,>);
                //interfaceType = interfaceType.MakeGenericType(new Type[] { tpEntity, tpEntityKeyType });
                //// 创建对象类型
                //var objectType = typeof(Dbsets.Repository<,>);
                //objectType = objectType.MakeGenericType(new Type[] { tpEntity, tpEntityKeyType });
                // 添加到信息列表
                //list.Add(new RepositoryInfo(interfaceType, objectType));
            }
            return list;
        }

    }
}
