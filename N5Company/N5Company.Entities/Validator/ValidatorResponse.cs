using System;
using System.Collections.Generic;

namespace N5Company.Entities.Validator
{
    public class ValidatorResponse
    {
        public ValidatorResponse()
        {
            IsError = false;
            Errors = new List<string>();
        }
        public bool? IsError { get; set; }

        public List<string> Errors { get; set; }
    }
}
