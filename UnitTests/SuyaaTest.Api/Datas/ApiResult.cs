using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SuyaaTest.Api.Datas
{
    public class ApiResult
    {
        /// <summary>
        /// Success
        /// </summary>
        public virtual bool Success { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        [JsonPropertyName("error")]
        public virtual ApiResultError? Error { get; set; }
    }

    public class ApiResultError
    {
        /// <summary>
        /// Message
        /// </summary>
        [JsonPropertyName("message")]
        public virtual string Message { get; set; } = string.Empty;
    }
}
