using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using System.Reflection;
using Suyaa.Data.Helpers;
using Suyaa.Data.Dependency;

namespace Suyaa.Data
{
    /// <summary>
    /// Sql表达式
    /// </summary>
    public class SqlExpression<T> : IDisposable
    {
        // 数据库供应商
        private readonly IDatabaseProvider _provider;
        // 字段集合
        private readonly List<PropertyInfo> _properties;

        /// <summary>
        /// 数据库供应商
        /// </summary>
        public IDatabaseProvider Provider { get { return _provider; } }

        /// <summary>
        /// Sql表达式
        /// </summary>
        /// <param name="provider"></param>
        public SqlExpression(IDatabaseProvider provider)
        {
            _provider = provider;
            _properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
        }

        // 从对象中获取子项
        private object? GetValueInObject(object obj, string name)
        {
            Type type = obj.GetType();
            // 尝试从字段获取
            var field = type.GetField(name);
            if (field != null) return field.GetValue(obj);
            // 尝试从属性获取
            var pro = type.GetProperty(name);
            if (pro != null) return pro.GetValue(obj);
            return null;
        }

        // 获取列名称
        private string GetColumnName(string propertyName)
        {
            var pro = _properties.Where(d => d.Name == propertyName).FirstOrDefault();
            if (pro != null) return _provider.GetNameString(pro.GetColumnName());
            return _provider.GetNameString(propertyName);
        }

        // 获取Contains函数兼容的sql语句
        private string GetContainsSql(MethodCallExpression call)
        {
            var callObj = (MemberExpression)call.Object;
            var callObjValues = GetSqlExpressionValue(callObj.Expression) ?? throw new Exception($"容器无数据");
            var listInfo = callObjValues.GetType().GetField(callObj.Member.Name);
            ICollection list = (ICollection)listInfo.GetValue(callObjValues);
            StringBuilder sbList = new StringBuilder();
            sbList.Append('(');
            foreach (var item in list)
            {
                if (sbList.Length > 1) sbList.Append(", ");
                if (item is null) { sbList.Append("NULL"); continue; }
                if (item.GetType().IsNumeric()) { sbList.Append(item); continue; }
                if (item is string str) { sbList.Append(_provider.GetValueString(str)); continue; }
                throw new Exception($"不支持的数据类型'{item.GetType().FullName}'");
            }
            sbList.Append(')');
            var arg = GetSqlExpressionValue(call.Arguments[0]);
            return $"{arg} IN {sbList}";
        }

        // 获取Contains函数兼容的sql语句
        private string GetEqualsSqlString(MethodCallExpression call)
        {
            var callObj = (MemberExpression)call.Object;
            object? value;
            string name;
            switch (callObj.Expression)
            {
                case ParameterExpression _:
                    name = GetColumnName(callObj.Member.Name);
                    value = GetSqlExpressionValue(call.Arguments[0]);
                    break;
                default:
                    value = GetSqlExpressionValue(callObj.Expression);
                    name = call.Arguments[0].NodeType.ToString();
                    break;
            }
            if (value is null) return $"{name} IS NULL";
            return $"{name} = {value}";
        }

        // 获取Convert函数兼容的sql语句
        private string GetConvertSql(UnaryExpression unary)
        {
            object? value = null;
            if (unary.Operand is UnaryExpression) value = GetSqlExpressionValue(unary.Operand);
            if (unary.Operand is ConstantExpression) value = GetSqlExpressionValue(unary.Operand);
            if (value is null)
            {
                var operand = (MemberExpression)unary.Operand;
                var operandValues = GetSqlExpressionValue(operand.Expression) ?? throw new Exception($"容器无数据");
                value = GetValueInObject(operandValues, operand.Member.Name);
            }

            return value switch
            {
                null => "NULL",
                string _ => _provider.GetValueString((string)value),
                _ => Convert.ToString(value),
            };
        }

        // 获取函数调用处理sql语句
        private string GetMethodCallSqlString(MethodCallExpression methodCall)
        {
            var callMethod = methodCall.Method;
            return callMethod.Name switch
            {
                "Contains" => GetContainsSql(methodCall),
                "Equals" => GetEqualsSqlString(methodCall),
                _ => throw new Exception($"SqlExpressionValue不支持的Call类型'{callMethod.Name}'"),
            };
        }

        // 获取sql语句值
        private object? GetSqlExpressionValue(Expression exp)
        {
            switch (exp)
            {
                // 表达式
                case BinaryExpression binaryExpression:
                    return "(" + GetBinarySqlString(binaryExpression) + ")";
                // Call函数
                case MethodCallExpression methodCallExpression:
                    return GetMethodCallSqlString(methodCallExpression);
                // 变量
                case MemberExpression member:
                    switch (member.Expression)
                    {
                        // 如果包含表达式，则先解析表达式
                        case ConstantExpression constant:
                            var parent = GetSqlExpressionValue(constant) ?? throw new NullException(typeof(ConstantExpression));
                            return GetValueInObject(parent, member.Member.Name);
                        default: return GetColumnName(member.Member.Name);
                    }
            }
            switch (exp.NodeType)
            {
                case ExpressionType.Constant: // 获取Constant函数
                    var constant = (ConstantExpression)exp;
                    if (constant.Value is null) return "NULL";
                    if (constant.Value is string str) return _provider.GetValueString(str);
                    var valueType = constant.Value.GetType();
                    if (valueType.IsNumeric()) return constant.Value.ToString();
                    return constant.Value;
                case ExpressionType.Call: // 获取call函数
                    var call = (MethodCallExpression)exp;
                    var callMethod = call.Method;
                    if (callMethod.Name == "Contains") return GetContainsSql(call);
                    throw new Exception($"SqlExpressionValue不支持的Call类型'{callMethod.Name}'");
                case ExpressionType.Convert: // 获取Convert函数
                    return GetConvertSql((UnaryExpression)exp);
                default:
                    throw new Exception($"SqlExpressionValue不支持的'{exp.NodeType}'节点类型");
            }
        }

        /// <summary>
        /// 获取sql语句
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public string GetBinarySqlString(BinaryExpression exp)
        {
            StringBuilder sb = new StringBuilder();
            string expLeft = (string)(GetSqlExpressionValue(exp.Left) ?? "");
            string expRight = (string)(GetSqlExpressionValue(exp.Right) ?? "");
            sb.Append(expLeft);
            switch (exp.NodeType)
            {
                case ExpressionType.AndAlso:
                    sb.Append(" AND ");
                    break;
                case ExpressionType.OrElse:
                    sb.Append(" OR ");
                    break;
                case ExpressionType.Equal:
                    if (expRight == "NULL")
                    {
                        sb.Append(" IS ");
                    }
                    else
                    {
                        sb.Append(" = ");
                    }
                    break;
                case ExpressionType.NotEqual:
                    if (expRight == "NULL")
                    {
                        sb.Append(" IS NOT ");
                    }
                    else
                    {
                        sb.Append(" <> ");
                    }
                    break;
                case ExpressionType.GreaterThan:
                    sb.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    sb.Append(" >= ");
                    break;
                case ExpressionType.LessThan:
                    sb.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    sb.Append(" <= ");
                    break;
                case ExpressionType.Coalesce:
                    return $"COALESCE({expLeft}, {expRight})";
                default: throw new DbException($"SqlExpression不支持的'{exp.NodeType}'节点类型");
            }
            sb.Append(expRight);
            return sb.ToString();
        }

        /// <summary>
        /// 获取sql语句
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public string GetSqlString(Expression exp)
        {
            return exp switch
            {
                BinaryExpression binaryExpression => GetBinarySqlString(binaryExpression),
                MethodCallExpression methodCallExpression => GetMethodCallSqlString(methodCallExpression),
                _ => throw new DbException($"SqlExpression不支持的'{exp.NodeType}'节点类型"),
            };
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
