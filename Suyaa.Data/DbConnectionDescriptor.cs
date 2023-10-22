using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Suyaa.Data
{
    /// <summary>
    /// 数据库连接描述
    /// </summary>
    public class DbConnectionDescriptor : Dictionary<string, string>
    {

        /// <summary>
        /// 解析连接字符串
        /// </summary>
        /// <param name="connectionString"></param>
        private void ParseConnectionString(string connectionString)
        {
            string[] strings = connectionString.Split(';');
            foreach (var str in strings)
            {
                if (str.IsNullOrWhiteSpace()) continue;
                int idx = str.IndexOf("=");
                if (idx < 0)
                {
                    this[str] = "";
                }
                else
                {
                    this[str.Substring(0, idx)] = str.Substring(idx + 1);
                }
            }
        }

        /// <summary>
        /// 数据库描述
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connectionDefine">以[dbtype]connectionString形式定义的连接描述</param>
        public DbConnectionDescriptor(string name, string connectionDefine)
        {
            if (connectionDefine.IsNullOrWhiteSpace()) throw new DbException("connectionDefine not found.");
            if (connectionDefine[0] != '[') throw new DbException(string.Format("connectionDefine must start with '[dbtype]'."));
            int idx = connectionDefine.IndexOf(']');
            if (idx < 0) throw new DbException(string.Format("ConnectionString must start with '[dbtype]'."));
            string dbType = connectionDefine.Substring(1, idx - 1);
            // 获取连接字符串
            ParseConnectionString(connectionDefine.Substring(idx + 1));
            // 获取数据库类型
            this.DatabaseType = dbType.ToLower() switch
            {
                "sqlite" => DbTypes.Sqlite,
                "sqlite3" => DbTypes.Sqlite3,
                "mssql" => DbTypes.MicrosoftSqlServer,
                "sqlserver" => DbTypes.MicrosoftSqlServer,
                "pqsql" => DbTypes.PostgreSQL,
                "postgresql" => DbTypes.PostgreSQL,
                "postgres" => DbTypes.PostgreSQL,
                "mysql" => DbTypes.MySQL,
                "access" => DbTypes.MicrosoftOfficeAccess,
                "access12" => DbTypes.MicrosoftOfficeAccessV12,
                _ => throw new DbException(string.Format("Unsupported database type '{0}'.", dbType)),
            };
            Name = name;
        }

        /// <summary>
        /// 数据库描述
        /// </summary>
        /// <param name="name"></param>
        /// <param name="databaseType"></param>
        public DbConnectionDescriptor(string name, DbTypes databaseType)
        {
            Name = name;
            DatabaseType = databaseType;
        }

        /// <summary>
        /// 数据库描述
        /// </summary>
        /// <param name="name"></param>
        /// <param name="databaseType"></param>
        /// <param name="connectionString"></param>
        public DbConnectionDescriptor(string name, DbTypes databaseType, string connectionString)
        {
            // 解析连接字符串
            ParseConnectionString(connectionString);
            Name = name;
            DatabaseType = databaseType;
        }

        /// <summary>
        /// 连接名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DbTypes DatabaseType { get; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ToConnectionString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this)
            {
                sb.Append(item.Key);
                sb.Append('=');
                sb.Append(item.Value);
                sb.Append(';');
            }
            return sb.ToString();
        }
    }
}
