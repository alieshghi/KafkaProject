using System.Text;
using Confluent.Kafka;
using Kafka.Public.Loggers;
using Kafka.Public;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Kafka._2ndConsumer
{
    

    class Program
    {
        
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, collection) =>
                {
                    collection.AddHostedService<ConsumerHostedService>();
                });
        static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();


            //// Load configuration
            //var config = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .Build();

            //// Kafka Consumer configuration
            //var consumerConfig = new ConsumerConfig
            //{
            //    BootstrapServers = config["Kafka:BootstrapServers"],
            //    GroupId = "console-consumer-group", // Consumer group ID
            //    AutoOffsetReset = AutoOffsetReset.Earliest, // Read messages from the beginning
            //    EnableAutoCommit = true
            //};

            //using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
            //consumer.Subscribe(config["Kafka:TopicName"]);

            //Console.WriteLine("Listening for messages...");

            //try
            //{
            //    while (true)
            //    {
            //        try
            //        {
            //            // Poll for new messages
            //            var consumeResult = consumer.Consume(CancellationToken.None);
            //            Console.WriteLine($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Value}");
            //        }
            //        catch (ConsumeException e)
            //        {
            //            Console.WriteLine($"Error consuming message: {e.Error.Reason}");
            //        }
            //    }
            //}
            //catch (OperationCanceledException)
            //{
            //    // Handle graceful shutdown
            //    Console.WriteLine("Closing consumer...");
            //}
            //finally
            //{
            //    consumer.Close();
            //}
        }
        public class ConsumerHostedService : IHostedService
        {
            private readonly ILogger<ConsumerHostedService> _logger;
            private ClusterClient _clusterClient;
            public ConsumerHostedService(ILogger<ConsumerHostedService> logger)
            {
                _logger = logger;
                _clusterClient = new ClusterClient(new Configuration
                {

                    Seeds = "localhost:9092"
                }, new ConsoleLogger());
            }
            public Task StartAsync(CancellationToken cancellationToken)
            {
                //// Set the consumer to consume from the latest offset.
                _clusterClient.ConsumeFromLatest("sida-update-data");
                _clusterClient.MessageReceived += record =>
                {
                    if (record.Value != null)
                    {
                        _logger.LogInformation($"received from serve 2: {Encoding.UTF8.GetString(record.Value as byte[])} ");
                    }
                      // Manually commit the offset
                };
                return Task.CompletedTask;

            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }
    }
}
