using AspNetReactSignalR.WebApi.Hubs.Interfaces;
using AspNetReactSignalR.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetReactSignalR.WebApi.Controllers
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
        //private readonly IChat _chat;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
            //, IChat chat
            )
        {
            _logger = logger;
            //_chat = chat;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpPost(nameof(EnviarMensaje))]
        //public async Task EnviarMensaje(Mensaje mensaje)
        //{
        //    await _chat.EnviarMensaje(mensaje).ConfigureAwait(false);
        //}
    }
}