using Suyaa.Data.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Dependency
{
    /// <summary>
    /// 仓库接口
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IRepository<TClass, TId> : Data.Dependency.IRepository<TClass, TId>
        where TClass : class, IEntity<TId>
        where TId : notnull
    {

        /// <summary>
        /// 创建查询
        /// </summary>
        /// <returns></returns>
        IQueryable<TClass> Query();

        /// <summary>
        /// 修改
        /// </summary>
        void Update(TClass entity);

        /// <summary>
        /// 修改
        /// </summary>
        Task UpdateAsync(TClass entity);

    }
}
