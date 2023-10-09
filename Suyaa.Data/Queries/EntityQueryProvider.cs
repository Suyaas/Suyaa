using Suyaa.Data.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Queries
{
    /// <summary>
    /// 实例查询供应商
    /// </summary>
    public class EntityQueryProvider : IQueryProvider
    {
        // 数据库查询供应商
        private readonly IDatabaseQueryProvider _provider;

        /// <summary>
        /// 创建查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 创建查询
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new EntityQueryable<TElement>(this, expression);
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TResult Execute<TResult>(Expression expression)
        {
            return _provider.Query<TResult>(expression);
        }

        /// <summary>
        /// 实例查询供应商
        /// </summary>
        /// <param name="provider"></param>
        public EntityQueryProvider(IDatabaseQueryProvider provider)
        {
            _provider = provider;
        }
    }
}
