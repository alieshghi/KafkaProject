using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

//CreateHostBuilder(args).Build().Run();


// Load configuration
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Kafka Consumer configuration
var consumerConfig = new ConsumerConfig
{
    BootstrapServers = config["Kafka:BootstrapServers"],
    GroupId = "test2-consumer-group", // Consumer group ID
    AutoOffsetReset = AutoOffsetReset.Earliest, // Read messages from the beginning
                                                //EnableAutoCommit = true
};

using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
consumer.Subscribe(config["Kafka:TopicName"]);

Console.WriteLine("Listening for messages...");

try
{
    while (true)
    {
        try
        {
            // Poll for new messages
            var consumeResult = consumer.Consume(CancellationToken.None);
            Console.WriteLine($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Value}");
        }
        catch (ConsumeException e)
        {
            Console.WriteLine($"Error consuming message: {e.Error.Reason}");
        }
    }
}
catch (OperationCanceledException)
{
    // Handle graceful shutdown
    Console.WriteLine("Closing consumer...");
}
finally
{
    consumer.Close();
}
