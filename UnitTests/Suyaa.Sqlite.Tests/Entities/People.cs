using Suyaa.Data.Attributes;
using Suyaa.Data.Enums;
using SuyaaTest.Sqlite.Entities;
using System.ComponentModel.DataAnnotations;

namespace Suyaa.Sqlite.Tests.Entities
{
    /// <summary>
    /// 人员
    /// </summary>
    [DbTable(Convert = DbNameConvertTypes.UnderlineLower)]
    public class People : GuidKeyEntity
    {

        /// <summary>
        /// 名称
        /// </summary>
        [DbColumn(Convert = DbNameConvertTypes.UnderlineLower)]
        [DbColumnType(DbColumnTypes.Varchar, 128)]
        [StringLength(128)]
        public virtual string Name { get; set; } = string.Empty;

        /// <summary>
        /// 年龄
        /// </summary>
        [DbColumn(Convert = DbNameConvertTypes.UnderlineLower)]
        public virtual int Age { get; set; } = 0;

        /// <summary>
        /// 部门Id
        /// </summary>
        [DbColumn(Convert = DbNameConvertTypes.UnderlineLower)]
        [DbColumnType(DbColumnTypes.Varchar, 50)]
        [StringLength(50)]
        public virtual string DepartmentId { get; set; } = string.Empty;
    }
}
