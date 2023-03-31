using Confluent.Kafka;
using N5Company.Business.Interfaces;
using N5Company.Entities.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.Business
{
    public class KafkaProducerBusiness : IKafkaProducerBusiness
    {
        private readonly ProducerConfig _config;
        private readonly IProducer<string, string> _producer;

        public KafkaProducerBusiness(string bootstrapServers)
        {
            _config = new ProducerConfig { BootstrapServers = bootstrapServers };
            _producer = new ProducerBuilder<string, string>(_config).Build();
        }

        public async Task ProduceMessageAsync(string topic, KafkaLogDTO model)
        {
            try
            {
                var message = new Message<string, string> { Key = model.Id.ToString(), Value = model.OperationName };
                var result = await _producer.ProduceAsync(topic, message);
                Console.WriteLine($"Produced message '{result.Value}' to topic '{result.TopicPartition}'");
            }
            catch (ProduceException<string, string> e)
            {
                Console.WriteLine($"Failed to produce message: {e.Error.Reason}");
            }
        }
    }
}
