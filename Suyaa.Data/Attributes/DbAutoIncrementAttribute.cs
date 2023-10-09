using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Attributes
{
    /// <summary>
    /// 自增长字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DbAutoIncrementAttribute : Attribute
    {
    }
}
