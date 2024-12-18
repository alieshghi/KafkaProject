using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.FirstConsumer.Services
{
    public interface ICallEltService
    {
        public Task SendRequest(string message);
    }
    public class CallEltService:ICallEltService
    {
        public Task SendRequest(string message)
        {
            // do stuff u want.
            return Task.CompletedTask;
        }
    }
}
