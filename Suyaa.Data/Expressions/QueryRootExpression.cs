using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Expressions
{
    /// <summary>
    /// 查询根表达式
    /// </summary>
    public class QueryRootExpression : Expression
    {
        /// <summary>
        /// 表达式类型
        /// </summary>
        public override ExpressionType NodeType => ExpressionType.Extension;

        /// <summary>
        /// 类型
        /// </summary>
        public override Type Type { get; }

        /// <summary>
        /// 查询根表达式
        /// </summary>
        /// <param name="entityType"></param>
        public QueryRootExpression(Type entityType)
        {
            Type = typeof(IQueryable<>).MakeGenericType(entityType);
        }
    }
}
