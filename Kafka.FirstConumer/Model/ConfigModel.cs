﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.FirstConsumer.Model
{
    public record ConfigModel
    {
        public string BootstrapServers { get; set; }
        public string TopicName { get; set; }
    }
}
