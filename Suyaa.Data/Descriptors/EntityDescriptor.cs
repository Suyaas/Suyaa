using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Descriptors
{
    /// <summary>
    /// 表描述
    /// </summary>
    public sealed class EntityDescriptor : BaseDescriptor
    {

        /// <summary>
        /// 创建表描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static EntityDescriptor Instance<T>()
            where T : class
        {
            return new EntityDescriptor(typeof(T));
        }

        /// <summary>
        /// 表描述
        /// </summary>
        public EntityDescriptor(Type type) : base(type.GetMetaDatas())
        {
            Type = type;
            this.Name = string.Empty;
            this.Schema = string.Empty;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属架构
        /// </summary>
        public string Schema { get; set; }
    }
}
