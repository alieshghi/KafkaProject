using Kafka.MassTransit.RoutingSlip.Consumer.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.MassTransit.RoutingSlip.Consumer.Activities
{
    // Process Order Activity
    public class ProcessOrderActivity : IActivity<OrderArguments, OrderLog>
    {
        public Task<ExecutionResult> Execute(ExecuteContext<OrderArguments> context)
        {
            Console.WriteLine($"Executing ProcessOrder Activity for Order: {context.Arguments.OrderId}");
            return Task.FromResult(context.Completed(new OrderLog { OrderId = context.Arguments.OrderId }));
        }

        public Task<CompensationResult> Compensate(CompensateContext<OrderLog> context)
        {
            Console.WriteLine($"Compensating ProcessOrder Activity for Order: {context.Log.OrderId}");
            return Task.FromResult(context.Compensated());
        }
    }

}
