using System;

namespace Suyaa
{

    /// <summary>
    /// 类型扩展类
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// 获取外层类型名称
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static string GetTopName(this Type? type)
        {
            if (type is null) return "System.Null";
            return type.Namespace + "." + type.Name;
        }

        /// <summary>
        /// 获取是否为可空类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool IsNullable(this Type? type)
        {
            if (type is null) return true;
            return type.GetTopName() == "System.Nullable";
        }

        /// <summary>
        /// 获取类型编码
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TypeCode GetTypeCode(this Type? type)
            => Type.GetTypeCode(type);

        /// <summary>
        /// 判断是否为数值类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNumeric(this Type? type)
            => type.GetTypeCode().IsNumeric();

        /// <summary>
        /// 判断是否为泛型实现
        /// </summary>
        /// <param name="type"></param>
        /// <param name="typeDefinition"></param>
        /// <returns></returns>
        public static bool IsGenericImplementation(this Type? type, Type typeDefinition)
        {
            if (type is null) return false;
            if (!typeDefinition.IsGenericTypeDefinition) return false;
            if (!type.IsGenericType) return false;
            if (type.GetGenericTypeDefinition().Equals(typeDefinition)) return true;
            return false;
        }

        /// <summary>
        /// 判断是否继承类
        /// </summary>
        /// <param name="type"></param>
        /// <param name="typeBase"></param>
        /// <returns></returns>

        public static bool IsBased(this Type? type, Type typeBase)
        {
            if (type is null) return false;
            if (typeBase is null) return false;
            if (type.Equals(typeBase)) return true;
            // 兼容泛型实现
            if (type.IsGenericImplementation(typeBase)) return true;
            if (type.BaseType is null) return false;
            return type.BaseType.IsBased(typeBase);
        }

        /// <summary>
        /// 判断是否继承类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>

        public static bool IsBased<T>(this Type? type)
            => type.IsBased(typeof(T));

        /// <summary>
        /// 判断是否包含接口
        /// </summary>
        /// <param name="type"></param>
        /// <param name="typeInterface"></param>
        /// <returns></returns>
        public static bool HasInterface(this Type? type, Type typeInterface)
        {
            if (type is null) return false;
            if (!typeInterface.IsInterface) throw new Exception($"'{typeInterface.Name}'不是一个有效的接口");
            var ifs = type.GetInterfaces();
            foreach (var ifc in ifs)
            {
                if (ifc.Equals(typeInterface)) return true;
                // 兼容泛型实现
                if (ifc.IsGenericImplementation(typeInterface)) return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否包含接口
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool HasInterface<T>(this Type? type)
            => type.HasInterface(typeof(T));

        /// <summary>
        /// 创建一个实例对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T Create<T>(this Type? type, object[]? args = null) where T : class
            => ((T?)sy.Assembly.Create(type, args)).Fixed();

        /// <summary>
        /// 创建一个实例对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object Create(this Type type, object[]? args = null)
            => sy.Assembly.Create(type, args) ?? throw new NullException(type);
    }
}
