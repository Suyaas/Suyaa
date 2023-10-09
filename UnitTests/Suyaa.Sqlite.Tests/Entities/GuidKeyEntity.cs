using Suyaa.Data.Attributes;
using Suyaa.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuyaaTest.Sqlite.Entities
{
    /// <summary>
    /// Guid主键
    /// </summary>
    public abstract class GuidKeyEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [DbColumn(Convert = DbNameConvertTypes.UnderlineLower)]
        [DbColumnType(DbColumnTypes.Varchar, 32)]
        [StringLength(32)]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// 部门
        /// </summary>
        public GuidKeyEntity()
        {
            this.Id = Guid.NewGuid().ToString("N");
        }
    }
}
