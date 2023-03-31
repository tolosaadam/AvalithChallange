using Microsoft.AspNetCore.Http;
using N5Company.Business;
using N5Company.Entities.DTOS;
using System.Threading.Tasks;

namespace N5CompanyAPI.Middlewares
{
    public class KafkaMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly KafkaProducerBusiness _producer;

        public KafkaMiddleware(RequestDelegate next, KafkaProducerBusiness producer)
        {
            _next = next;
            _producer = producer;
        }

        public async Task Invoke(HttpContext context)
        {
            var operationName = context.Request.Path.Value;

            if (operationName.Contains("/swagger")) // No envía mensajes de Kafka si es una solicitud para Swagger
            {
                await _next(context);
                return;
            }


            // Produces a message in Kafka with the operation name
            await _producer.ProduceMessageAsync("Permission", new KafkaLogDTO { OperationName = operationName });

            await _next(context);
        }
    }
}
