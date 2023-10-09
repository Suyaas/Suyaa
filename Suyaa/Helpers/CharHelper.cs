namespace Suyaa
{

    /// <summary>
    /// 字符扩展类
    /// </summary>
    public static class CharHelper
    {
        /// <summary>
        /// 大小写差值
        /// </summary>
        private const byte CASE_DROP = 'a' - 'A';

        /// <summary>
        /// 判断是否为大写字符
        /// </summary>
        /// <param name="chr">类型编码</param>
        /// <returns></returns>
        public static bool IsUpper(this char chr)
            => chr >= 'A' && chr <= 'Z';

        /// <summary>
        /// 判断是否为小写字符
        /// </summary>
        /// <param name="chr">类型编码</param>
        /// <returns></returns>
        public static bool IsLower(this char chr)
            => chr >= 'a' && chr <= 'z';

        /// <summary>
        /// 转化为小写字符
        /// </summary>
        /// <param name="chr">类型编码</param>
        /// <returns></returns>
        public static char ToLower(this char chr)
        {
            if (chr >= 'A' && chr <= 'Z') return (char)(chr + CASE_DROP);
            return chr;
        }

        /// <summary>
        /// 转化为大写字符
        /// </summary>
        /// <param name="chr">类型编码</param>
        /// <returns></returns>
        public static char ToUpper(this char chr)
        {
            if (chr >= 'a' && chr <= 'z') return (char)(chr - CASE_DROP);
            return chr;
        }
    }
}
