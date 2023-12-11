using Suyaa;
using System;

namespace sy
{

    /// <summary>
    /// 程序集
    /// </summary>
    public static partial class Assembly
    {

        /// <summary>
        /// 根据名称查找类型
        /// </summary>
        /// <param name="name">类型名称</param>
        /// <returns></returns>
        public static Type? FindType(string name)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                        if (type.FullName == name) return type;
                }
                catch { }
            }
            return null;
        }

        /// <summary>
        /// 根据名称查找类型，如不存在，则从目标目录中加载引用程序集后重新查找
        /// </summary>
        /// <param name="name">类型名称</param>
        /// <param name="path">类库文件或文件夹路径</param>
        /// <returns></returns>
        public static Type? FindType(string name, string path)
        {
            // 先进行名称查找类型
            Type? type = FindType(name);
            // 未找到则先加载关联的dll文件，再重新查找
            if (type is null)
            {
                // 目标地址是一个文件
                if (sy.IO.FileExists(path)) LoadAssemblyFromFile(path);
                // 目标地址是一个文件夹
                if (sy.IO.FolderExists(path)) LoadAssemblyFromFolder(path);
                // 重新查找类型
                type = FindType(name);
            }
            // 处理后仍未找到类型，则重新加载程序目录下的dll文件
            if (type is null)
            {
                // 重新加载程序目录下的所有dll文件
                LoadAssemblyFromFolder(ExecutionDirectory);
                // 重新加载工作目录下的所有dll文件
                LoadAssemblyFromFolder(WorkingDirectory);
                // 重新加载Suyaa.dll所在目录下的所有dll文件
                LoadAssemblyFromFolder(GetModulePath());
                // 重新查找类型
                type = FindType(name);
            }
            return type;
        }

        /// <summary>
        /// 从文件中加载程序集
        /// </summary>
        /// <param name="path"></param>
        public static void LoadAssemblyFromFile(string path)
            => System.Reflection.Assembly.LoadFrom(path);

        /// <summary>
        /// 从文件夹中加载所有程序集
        /// </summary>
        /// <param name="path"></param>
        public static void LoadAssemblyFromFolder(string path)
        {
            var files = sy.IO.GetFiles(path, "*.dll");
            foreach (var file in files) LoadAssemblyFromFile(file);
        }

        /// <summary>
        /// 创建一个对象实例
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        public static object? Create(Type? type, object[]? args = null)
        {
            if (type is null) return null;
            if (args is null) return Activator.CreateInstance(type);
            return Activator.CreateInstance(type, args);
        }

        /// <summary>
        /// 创建一个对象实例
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? Create(string name, object[]? args = null)
            => Create(FindType(name), args);

        /// <summary>
        /// 创建一个对象实例
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T Create<T>(string name, object[]? args = null) where T : class
            => (T)(Create(FindType(name), args) ?? throw new KeyException("Exception.Type.Create.Fail", "Type '{0}' create fail.", name));

        /// <summary>
        /// 创建一个对象实例
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T Create<T>(object[]? args = null) where T : class
            => (T)(Create(typeof(T), args) ?? throw new NullException(typeof(T)));
    }
}
