using Suyaa.Data.Expressions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Queries
{
    /// <summary>
    /// 可查询对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityQueryable<T> : IQueryable<T>, IQueryable
    {
        /// <summary>
        /// 元素类型
        /// </summary>
        public Type ElementType { get; }

        /// <summary>
        /// 表达式
        /// </summary>
        public Expression Expression { get; }

        /// <summary>
        /// 查询供应商
        /// </summary>
        public IQueryProvider Provider { get; }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.Provider.Execute<IEnumerable<T>>(Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 可查询对象
        /// </summary>
        /// <param name="provider"></param>
        public EntityQueryable(IQueryProvider provider)
        {
            Provider = provider;
            ElementType = typeof(T);
            Expression = new QueryRootExpression(ElementType);
        }

        /// <summary>
        /// 可查询对象
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="expression"></param>
        public EntityQueryable(IQueryProvider provider, Expression expression)
        {
            Provider = provider;
            ElementType = typeof(T);
            Expression = expression;
        }
    }
}
