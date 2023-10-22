using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using Suyaa.Data.PostgreSQL.Helpers;
using Suyaa.EFCore.Helpers;
using Suyaa.EFCore.Dependency;

namespace Suyaa.EFCore
{
    /// <summary>
    /// PostgreSQL数据库创建器
    /// </summary>
    public class NpgsqlCreater : IDbCreater
    {

        /// <summary>
        /// 确保数据库创建
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<bool> EnsureCreated(DbDescriptorContext context)
        {
            try
            {
                await context.ExecuteNonQueryAsync(GetEnsureCreatedSql(context));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取数据列创建语句
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public string GetAddColumnSql(IEntityType table, IProperty column)
        {
            return $"ALTER TABLE {table.GetSchemaQualifiedTableName()} ADD {GetColumnSql(table, column)};";
        }

        /// <summary>
        /// 获取数据列修改语句
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public string GetAlterColumnSql(IEntityType table, IProperty column)
        {
            StringBuilder sb = new StringBuilder();
            if (column.IsPrimaryKey())
            {
                bool isAutoIncrement = column.IsAutoIncrement();
                sb.AppendLine($"ALTER TABLE {table.GetSchemaQualifiedTableName()} ALTER COLUMN \"{column.GetColumnBaseName()}\" TYPE {(isAutoIncrement ? "serial" : column.GetColumnType())};");
                sb.AppendLine($"ALTER TABLE {table.GetSchemaQualifiedTableName()} ALTER \"{column.GetColumnBaseName()}\" SET NOT NULL;");
            }
            else
            {
                sb.AppendLine($"ALTER TABLE {table.GetSchemaQualifiedTableName()} ALTER COLUMN \"{column.GetColumnBaseName()}\" TYPE {column.GetColumnType()};");
                sb.AppendLine($"ALTER TABLE {table.GetSchemaQualifiedTableName()} ALTER \"{column.GetColumnBaseName()}\" {(column.IsColumnNullable() ? "DROP" : "SET")} NOT NULL;");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取数据列创建语句
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetColumnSql(IEntityType table, IProperty column)
        {
            if (column.IsPrimaryKey())
            {
                bool isAutoIncrement = column.IsAutoIncrement();
                return $"\"{column.GetColumnBaseName()}\" {(isAutoIncrement ? "serial" : column.GetColumnType())} NOT NULL";
            }
            else
            {
                return $"\"{column.GetColumnBaseName()}\" {column.GetColumnType()} {(column.IsColumnNullable() ? "NULL" : "NOT NULL")}";
            }
        }

        /// <summary>
        /// 获取创建表的语句
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public string GetCreateTableSql(IEntityType table)
        {
            StringBuilder sb = new StringBuilder();
            string schema = table.GetSchema().Fixed();
            string tableName = table.GetTableName().Fixed();
            string tableFullName = table.GetSchemaQualifiedTableName().Fixed();
            string? primaryKey = null;
            bool isFirst = true;
            // 拼接构架
            if (!string.IsNullOrWhiteSpace(schema)) sb.AppendLine($"CREATE SCHEMA IF NOT EXISTS \"{schema}\";");
            // 拼接语句
            sb.Append($"CREATE TABLE IF NOT EXISTS {tableFullName}(\n");
            foreach (IProperty property in table.GetProperties())
            {
                if (property.IsPrimaryKey()) primaryKey = property.GetColumnBaseName();
                if (isFirst) { isFirst = false; } else { sb.Append(','); sb.AppendLine(); }
                sb.Append("    ");
                sb.Append(GetColumnSql(table, property));
            }
            if (!string.IsNullOrWhiteSpace(primaryKey))
            {
                sb.Append(',');
                sb.AppendLine();
                sb.Append("    ");
                sb.Append($"CONSTRAINT \"PK_{tableName}\" PRIMARY KEY (\"{primaryKey}\")");
            }
            // 拼接语句
            sb.AppendLine();
            sb.Append($");");
            sb.AppendLine();
            return sb.ToString();
        }

        /// <summary>
        /// 获取创建表和字段更新的语句
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public string GetCreateOrUpdateTableSql(IEntityType table)
        {
            StringBuilder sb = new StringBuilder();
            // 添加表
            string schmaName = table.GetSchema().Fixed();
            if (string.IsNullOrWhiteSpace(schmaName)) schmaName = "public";
            string tableName = table.GetTableName().Fixed();
            sb.Append(GetCreateTableSql(table));
            // 添加所有字段
            foreach (IProperty property in table.GetProperties())
            {
                if (!property.IsPrimaryKey())
                {
                    string columnName = property.GetColumnBaseName();
                    sb.AppendLine($"if not exists(select dtd_identifier from information_schema.columns WHERE table_schema = '{schmaName}' and table_name = '{tableName.ToLower()}' and column_name = '{columnName}') then");
                    sb.AppendLine(GetAddColumnSql(table, property));
                    sb.AppendLine("else");
                    sb.Append(GetAlterColumnSql(table, property));
                    sb.AppendLine("end if;");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取Sql语句
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>        
        public string GetEnsureCreatedSql(DbDescriptorContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("do $$");
            sb.AppendLine("begin");
            // 获取根类型
            var tp = context.GetType();
            var entities = context.Model.GetEntityTypes();
            foreach (var entity in entities)
            {
                sb.Append(GetCreateOrUpdateTableSql(entity));
            }
            sb.AppendLine("end; $$");
            return sb.ToString();
        }
    }
}
