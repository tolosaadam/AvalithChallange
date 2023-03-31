using System;
using System.Collections.Generic;
using System.Text;

namespace N5Company.Entities.Responses
{
    public class ErrorResponse
    {
        public bool Success => false;
        public List<string> Messages { get; private set; }

        public ErrorResponse(List<string> messages)
        {
            this.Messages = messages ?? new List<string>();
        }
    }
}
