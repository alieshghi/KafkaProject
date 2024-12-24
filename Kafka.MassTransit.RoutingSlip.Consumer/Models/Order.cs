using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.MassTransit.RoutingSlip.Consumer.Models
{
    public record OrderArguments
    {
        public Guid OrderId { get; init; } = Guid.NewGuid();
    }
    public record OrderLog
    {
        public Guid OrderId { get; init; }
    }
}
