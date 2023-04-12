using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Suyaa.Exceptions;
using Suyaa.Helpers;

namespace sy
{
    /// <summary>
    /// Json
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// 从Json字符串中反序列化对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static T Deserialize<T>(string? json) where T : notnull
        {
            if (json is null) throw new NullException();
            if (json.IsNullOrWhiteSpace()) throw new NullException();
            var obj = JsonSerializer.Deserialize<T>(json);
            if (obj is null) throw new NullException();
            return obj;
        }

        /// <summary>
        /// 将对象序列化为Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object? obj)
        {
            if (obj is null) return string.Empty;
            return JsonSerializer.Serialize(obj);
        }
    }
}
