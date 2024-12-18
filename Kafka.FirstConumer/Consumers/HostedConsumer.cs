using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kafka.FirstConsumer.Model;
using Kafka.FirstConsumer.Services;
using Kafka.Public.Loggers;
using Kafka.Public;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kafka.FirstConsumer.Consumers
{
    public class ConsumerHostedService : IHostedService
    {
        private readonly ILogger<ConsumerHostedService> _logger;
        private ClusterClient _clusterClient;
        private readonly ICallEltService _callEltService;
        private readonly ConfigModel _config;
        public ConsumerHostedService(ILogger<ConsumerHostedService> logger,
            ICallEltService callEltService,
            IOptions<ConfigModel> config)
        {
            _logger = logger;
            _callEltService = callEltService;
            _config = config.Value;
            _clusterClient = new ClusterClient(new Configuration
            {
                Seeds = _config.BootstrapServers //"localhost:9092"
            }, new ConsoleLogger());
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            
            _clusterClient.ConsumeFromLatest(_config.TopicName); //"sida-update-data"
            //_clusterClient.ConsumeFromEarliest(_config.Value.TopicName); //"sida-update-data"
            _clusterClient.MessageReceived += record =>
            {
                if (record.Value != null)
                {
                    var message = $"received: {Encoding.UTF8.GetString(record.Value as byte[])} ";
                    _callEltService.SendRequest(message);
                    _logger.LogInformation(message);
                    // call service you want
                }

            };
            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
