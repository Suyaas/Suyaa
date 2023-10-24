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
        /// <param name="condition">判断条件</param>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            if (!condition) return query;
            return query.Where(predicate);
        }

        /// <summary>
        /// 设置分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="page">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public static IQueryable<T> Paged<T>(this IQueryable<T> query, int page, int pageSize)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
