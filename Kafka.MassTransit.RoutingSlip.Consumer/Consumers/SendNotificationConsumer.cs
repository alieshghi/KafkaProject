using Kafka.MassTransit.RoutingSlip.Consumer.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.MassTransit.RoutingSlip.Consumer.Consumers
{
    public class SendNotificationConsumer : IConsumer<NotificationArguments>
    {
        public Task Consume(ConsumeContext<NotificationArguments> context)
        {
            Console.WriteLine($"Sending notification for order: {context.Message.OrderId}");
            return Task.CompletedTask;
        }
    }
}
