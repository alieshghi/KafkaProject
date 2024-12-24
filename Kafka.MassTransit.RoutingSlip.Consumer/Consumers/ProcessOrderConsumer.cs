using Kafka.MassTransit.RoutingSlip.Consumer.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.MassTransit.RoutingSlip.Consumer.Consumers
{
    public class ProcessOrderConsumer : IConsumer<OrderArguments>
    {
        public Task Consume(ConsumeContext<OrderArguments> context)
        {
            Console.WriteLine($"Processing order: {context.Message.OrderId}");
            return Task.CompletedTask;
        }
    }
}
