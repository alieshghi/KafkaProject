using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Kafka.FirstConsumer.Consumers;
using Kafka.FirstConsumer.Model;
using Kafka.FirstConsumer.Services;
using Kafka.Public;
using Kafka.Public.Loggers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kafka.FirstConsumer
{
    internal class Program
    {
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, collection) =>
                {
                    collection.Configure<ConfigModel>(context.Configuration.GetSection("Kafka"));
                    collection.AddHostedService<ConsumerHostedService>();
                    collection.AddSingleton<ICallEltService, CallEltService>(); 
                });

        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
    
   


   
}
