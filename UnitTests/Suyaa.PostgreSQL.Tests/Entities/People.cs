using Suyaa.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Sqlite.Tests.Entities
{
    /// <summary>
    /// 人员
    /// </summary>
    [Table("people", Schema = "ts")]
    public class People : GuidKeyEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Column("name")]
        public virtual string Name { get; set; } = string.Empty;

        /// <summary>
        /// 年龄
        /// </summary>
        [Column("age")]
        public virtual int Age { get; set; } = 0;
    }
}
