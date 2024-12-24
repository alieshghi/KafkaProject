using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.MassTransit.RoutingSlip.Consumer.Service
{
    public class RoutingSlipService : IHostedService
    {
        private readonly IBusControl _bus;

        public RoutingSlipService(IBusControl bus)
        {
            _bus = bus;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting application...");

            var builder = new RoutingSlipBuilder(NewId.NextGuid());

            // Add activities with Kafka topic URIs
            builder.AddActivity("ProcessOrder", new Uri("topic:process-order-topic"));
            builder.AddActivity("SendNotification", new Uri("topic:send-notification-topic"));

            var routingSlip = builder.Build();

            // Execute the routing slip
            Console.WriteLine("Executing routing slip...");
            await _bus.Execute(routingSlip);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
