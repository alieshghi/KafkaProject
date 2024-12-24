using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Kafka.Domain;
using Microsoft.Extensions.Options;

namespace Kafka.Service.ProducerService
{
    public interface IProducerService
    {
        public Task Produce(KafkaProducerModel model);
    }
    public class ProducerService : IProducerService
    {
        private readonly IProducer<Null, string> _producer;
        private readonly IConfiguration _configuration;
        private readonly ProducerConstConfig _config;
        public ProducerService( IConfiguration configuration
        ,IOptions<ProducerConstConfig> kafkaConfig)
        {
            _configuration = configuration;
            _config = kafkaConfig.Value;
            var config = new ProducerConfig()
            {
                
                BootstrapServers = _config.BootstrapServers

            };
            
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task Produce(KafkaProducerModel model)
        {
            try
            {
                //process-order-topic
                //await _producer.ProduceAsync(_config.TopicName, new Message<Null, string>()
                //{
                //    Value = model.Message
                //});
                await _producer.ProduceAsync("process-order-topic", new Message<Null, string>()
                {
                    Value = model.Message
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
          
           
        }


    }
}
