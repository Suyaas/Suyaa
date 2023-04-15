﻿using Suyaa.Data.Dependency;
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
    [Table("people")]
    public class People : GuidKeyEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Column("name")]
        [DbColumnType(DbColumnTypes.Varchar, 128)]
        public virtual string Name { get; set; } = string.Empty;

        /// <summary>
        /// 年龄
        /// </summary>
        [Column("age")]
        public virtual int Age { get; set; } = 0;
    }
}
