using System.Data.Common;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 数据库阅读器
    /// </summary>
    public static class DbDataReaderHelper
    {

        /// <summary>
        /// 读取单个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public static T ToValue<T>(this DbDataReader reader)
        {
            return reader[0].ConvertTo<T>();
        }

    }
}
