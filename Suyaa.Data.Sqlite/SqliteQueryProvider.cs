using Suyaa.Data.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Sqlite
{
    /// <summary>
    /// Sqlite查询供应商
    /// </summary>
    public class SqliteQueryProvider : IDatabaseQueryProvider
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TResult Query<TResult>(Expression expression)
        {
            var type = typeof(TResult);
            if (type.IsInterface)
            {
                var typeEnumerable = typeof(IEnumerable<>);
                var typeName = type.GetTopName();
                var typeEnumerableName = typeEnumerable.GetTopName();
                // 返回列表
                if (typeName == typeEnumerableName)
                {
                    var typeList = typeof(List<>);
                    var typeInstance = typeList.MakeGenericType(type.GenericTypeArguments);
                    var obj = Activator.CreateInstance(typeInstance);
                    if (obj is null) throw new TypeNotSupportedException(type);
                    return (TResult)obj;
                }
            }
            return Activator.CreateInstance<TResult>();
            //throw new NotImplementedException();
        }
    }
}
