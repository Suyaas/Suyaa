using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Suyaa.Data.Dependency;
using Suyaa.Data;
using System.Linq.Expressions;

namespace Suyaa.EFCore.Helpers
{
    /// <summary>
    /// 可查询对象助手
    /// </summary>
    public static class QueryableHelper
    {
        /// <summary>
        /// 满足条件时增加查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="condition"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>>? expression)
        {
            if (expression is null) return query;
            return query.Where(expression);
        }
    }
}
