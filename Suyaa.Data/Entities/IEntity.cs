using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Entities
{
    /// <summary>
    /// 数据实例
    /// </summary>
    public interface IEntity
    {
    }

    /// <summary>
    /// 带主键的实例
    /// </summary>
    public interface IEntity<TId> : IEntity where TId : notnull
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        TId Id { get; set; }
    }
}
