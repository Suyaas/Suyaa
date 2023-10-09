using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库查询供应商
    /// </summary>
    public interface IDatabaseQueryProvider
    {
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TResult Query<TResult>(Expression expression);
    }
}
