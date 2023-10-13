using SuyaaTest.Datas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Tests.Datas
{
    /// <summary>
    /// 人员
    /// </summary>
    public class People : Entity<string>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; } = string.Empty;

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int Age { get; set; } = 0;

        /// <summary>
        /// 是否男性
        /// </summary>
        public bool IsMan = true;
    }
}
