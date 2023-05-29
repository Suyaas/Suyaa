using Suyaa.Data.Dependency;
using Suyaa.Data.Dependency.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Entities
{
    /// <summary>
    /// 数据实例
    /// </summary>
    public abstract class Entity : IEntity
    {

    }

    /// <summary>
    /// 带主键的实例
    /// </summary>
    public abstract class Entity<TId> : Entity, IEntity<TId> where TId : notnull
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Key]
        [DbColumn(Convert = DbNameConvertTypes.UnderlineLower)]
        public virtual TId Id { get; set; }

        /// <summary>
        /// 带主键的实例
        /// </summary>
        /// <param name="id"></param>
        public Entity(TId id)
        {
            this.Id = id;
        }

    }
}
