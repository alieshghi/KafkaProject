using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.MassTransit.RoutingSlip.Consumer.Models
{
    public record NotificationArguments
    {
        public Guid OrderId { get; init; }
    }

    public record NotificationLog
    {
        public Guid OrderId { get; init; }
    }
}
