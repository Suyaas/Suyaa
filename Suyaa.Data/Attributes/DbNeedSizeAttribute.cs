using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Attributes
{
    /// <summary>
    /// 数据表特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class DbNeedSizeAttribute : Attribute
    {

    }
}
