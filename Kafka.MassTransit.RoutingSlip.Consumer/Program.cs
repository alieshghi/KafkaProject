

using Kafka.MassTransit.RoutingSlip.Consumer.Activities;
using Kafka.MassTransit.RoutingSlip.Consumer.Consumers;
using Kafka.MassTransit.RoutingSlip.Consumer.Models;
using Kafka.MassTransit.RoutingSlip.Consumer.Service;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kafka.MassTransit.RoutingSlip.Consumer
{

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    // Configure MassTransit with Kafka Rider
                    services.AddMassTransit(cfg =>
                    {
                        // Add Kafka Rider
                        cfg.AddRider(rider =>
                        {
                            rider.UsingKafka((context, k) =>
                            {
                                k.Host("localhost:9092"); // Kafka broker

                                // Configure topic endpoints
                                k.TopicEndpoint<OrderArguments>("process-order-topic", "consumer-group", e =>
                                {
                                    e.ConfigureConsumer<ProcessOrderConsumer>(context);
                                });

                                k.TopicEndpoint<NotificationArguments>("send-notification-topic", "consumer-group", e =>
                                {
                                    e.ConfigureConsumer<SendNotificationConsumer>(context);
                                });
                            });

                            // Add consumers
                            rider.AddConsumer<ProcessOrderConsumer>();
                            rider.AddConsumer<SendNotificationConsumer>();
                        });

                        // Add routing slip activities
                        cfg.AddActivity<ProcessOrderActivity, OrderArguments, OrderLog>();
                    });

                    services.AddHostedService<RoutingSlipService>();
                });

            await builder.RunConsoleAsync();
        }
    }
}

