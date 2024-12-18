using Kafka.Domain;
using Kafka.Service.ProducerService;
using Microsoft.AspNetCore.Mvc;

namespace Kafka.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IProducerService _producerService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IProducerService producerService)
        {
            _logger = logger;
            _producerService = producerService;
        }

        [HttpPost(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get(WeatherForecast model)
        {
            await _producerService.Produce(new KafkaProducerModel()
            {
                Message =  model.Summary + "/IsIran first task"
            });
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
