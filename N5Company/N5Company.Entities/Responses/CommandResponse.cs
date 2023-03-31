using System;
using System.Collections.Generic;
using System.Text;

namespace N5Company.Entities.Responses
{
    public class CommandResponse<T>
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public T Data { get; protected set; }

        public CommandResponse(bool success, string message, T data)
        {
            this.Success = success;
            this.Message = message ?? string.Empty;
            this.Data = data;
        }

        /// <summary>
        /// Produces a failure response.
        /// </summary>
        /// <param name="message">Error message.</param>
        public CommandResponse(string message) : this(false, message, default(T))
        {
        }

        /// <summary>
        /// Producess a successful response.
        /// </summary>
        /// <param name="data">Returned data.</param>
        public CommandResponse(T data) : this(true, string.Empty, data)
        {
        }
    }
}
