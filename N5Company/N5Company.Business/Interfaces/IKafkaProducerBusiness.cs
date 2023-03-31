using N5Company.Entities.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.Business.Interfaces
{
    public interface IKafkaProducerBusiness
    {
        Task ProduceMessageAsync(string topic, KafkaLogDTO model);
    }
}
