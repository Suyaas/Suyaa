using Suyaa.Data;
using Suyaa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Suyaa.EFCore.Helpers;

namespace Suyaa.EFCore.Dbsets
{
    /// <summary>
    /// 数据仓库
    /// </summary>
    public class EfRepository<TClass, TId> : IEfRepository<TClass, TId> where TClass : class, IEntity<TId> where TId : notnull
    {
        // 私有变量
        private Updater<TClass, TId> _updater;

        /// <summary>
        /// DB上下文
        /// </summary>
        public DbContextBase? Context { get; }

        /// <summary>
        /// 实体集合
        /// </summary>
        public DbSet<TClass>? DbSet { get; }

        /// <summary>
        /// 对象实例化
        /// </summary>
        /// <param name="context"></param>
        public EfRepository(DbContextBase context)
        {
            Context = context;
            DbSet = context.Set<TClass>();
            _updater = new Updater<TClass, TId>(new DatabaseConnection(context.GetDatabaseConnectionInfo()));
        }

        /// <summary>
        /// 删除数
        /// </summary>
        /// <param name="id"></param>
        public void Delete(TId id)
        {
            DeleteAsync(id).Wait();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TId id)
        {
            if (DbSet is null) throw new DatabaseException($"'DbSet'尚未定义");
            var entity = await DbSet.FindAsync(id);
            if (entity is null) return;
            DbSet.Remove(entity);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TClass entity)
        {
            InsertAsync(entity).Wait();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(TClass entity)
        {
            if (DbSet is null) throw new DatabaseException($"'DbSet'尚未定义");
            await DbSet.AddAsync(entity);
        }

        /// <summary>
        /// 添加数据列表
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void InsertList(IEnumerable<TClass> entity)
        {
            InsertListAsync(entity).Wait();
        }

        /// <summary>
        /// 添加数据列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertListAsync(IEnumerable<TClass> entity)
        {
            if (DbSet is null) throw new DatabaseException($"'DbSet'尚未定义");
            await DbSet.AddRangeAsync(entity);
        }

        /// <summary>
        /// 获取查询器
        /// </summary>
        /// <returns></returns>
        public IQueryable<TClass> Query()
        {
            if (DbSet is null) throw new DatabaseException($"'DbSet'尚未定义");
            return DbSet;
        }

        /// <summary>
        /// 获取更新器
        /// </summary>
        /// <returns></returns>
        public Updater<TClass, TId> Update() => _updater.UseAll();

        /// <summary>
        /// 获取更新器
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Updater<TClass, TId> Update(Expression<Func<TClass, object?>> selector) => _updater.Use(selector);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TClass entity, Expression<Func<TClass, bool>> predicate, Expression<Func<TClass, object?>>? selector = null)
        {
            if (Context is null) throw new DatabaseException($"'Context'尚未定义");
            var updater = new Updater<TClass, TId>(this.Context.GetDatabaseConnection());
            if (selector is null)
            {
                updater.UseAll();
            }
            else
            {
                updater.Use(selector);
            }
            updater.Set(entity, predicate);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(TClass entity)
        {
            UpdateAsync(entity).Wait();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TClass entity)
        {
            if (Context is null) throw new DatabaseException($"'Context'尚未定义");
            Context.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TClass? Get(TId id)
        {
            return GetAsync(id).Result;
        }

        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TClass?> GetAsync(TId id)
        {
            if (DbSet is null) throw new DatabaseException($"'DbSet'尚未定义");
            return await DbSet.FindAsync(id);
        }
    }
}
