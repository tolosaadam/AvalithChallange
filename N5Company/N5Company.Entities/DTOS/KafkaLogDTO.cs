using System;
using System.Collections.Generic;
using System.Text;

namespace N5Company.Entities.DTOS
{
    public class KafkaLogDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string OperationName { get; set; }
    }
}
