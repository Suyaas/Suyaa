using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.EFCore.Dbsets
{
    /// <summary>
    /// 存储库信息
    /// </summary>
    public class RepositoryInfo
    {
        /// <summary>
        /// 接口类型
        /// </summary>
        public Type InterfaceType { get;  }

        /// <summary>
        /// 对象类型
        /// </summary>
        public Type ObjectType { get;  }

        /// <summary>
        /// 存储库信息
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="objectType"></param>
        public RepositoryInfo(Type interfaceType, Type objectType)
        {
            InterfaceType = interfaceType;
            ObjectType = objectType;
        }
    }
}
