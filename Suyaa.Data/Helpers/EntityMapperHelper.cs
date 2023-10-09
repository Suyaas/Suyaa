using System.Data.Common;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 数据映射器扩展
    /// </summary>
    public static class EntityMapperHelper
    {

        /// <summary>
        /// 从数据库查询结果映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mapper"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T MapReaderToEntity<T>(this EntityMapper<T> mapper, DbDataReader reader)
        {
            return mapper.Map(pro =>
            {
                int idx = reader.GetOrdinal(pro.GetColumnName());
                if (idx >= 0) return reader.GetValue(idx);
                return null;
            });
        }

        /// <summary>
        /// 从数据库查询结果映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mapper"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T ToEntity<T>(this EntityMapper<T> mapper, DbDataReader reader)
        {
            return mapper.MapReaderToEntity(reader);
        }

    }
}
