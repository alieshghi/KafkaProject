using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Domain
{
    public record ProducerConstConfig
    {
        public string BootstrapServers { get; set; }
        public string TopicName { get; set; }
    }
}
