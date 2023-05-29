using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency.Attributes
{
    /// <summary>
    /// 自增长字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DbAutoIncrementAttribute : Attribute
    {
    }
}
