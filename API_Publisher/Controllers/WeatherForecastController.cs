using API_Publisher.bus;
using Microsoft.AspNetCore.Mvc;

namespace API_Publisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEventBus _eventBus;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken=default)
        {
            var message = new MessageModel("hello world");
            await _eventBus.PublishAsync(message, cancellationToken);
            return Ok();
        }
    }
}
