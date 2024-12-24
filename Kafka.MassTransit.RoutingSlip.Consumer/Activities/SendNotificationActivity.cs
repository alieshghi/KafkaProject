using Kafka.MassTransit.RoutingSlip.Consumer.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.MassTransit.RoutingSlip.Consumer.Activities
{
    // Send Notification Activity
    public class SendNotificationActivity : IActivity<NotificationArguments, NotificationLog>
    {
        public Task<ExecutionResult> Execute(ExecuteContext<NotificationArguments> context)
        {
            Console.WriteLine($"Executing SendNotification Activity for Order: {context.Arguments.OrderId}");
            return Task.FromResult(context.Completed(new NotificationLog { OrderId = context.Arguments.OrderId }));
        }

        public Task<CompensationResult> Compensate(CompensateContext<NotificationLog> context)
        {
            Console.WriteLine($"Compensating SendNotification Activity for Order: {context.Log.OrderId}");
            return Task.FromResult(context.Compensated());
        }
    }
}
