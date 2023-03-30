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
    /// 带主键的实例
    /// </summary>
    public class Entity<TId> : IEntity<TId>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Key]
        [Column("id")]
        public virtual TId Id { get; set; }

    }
}
