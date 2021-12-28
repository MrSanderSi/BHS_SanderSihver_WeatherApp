using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Data.WebApi;

namespace WeatherApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly Dictionary<int, string> Summaries = new()
        {
            { -20, "Freezing" },
            { -15, "Bracing" },
            { -10, "Chilly" },
            { -5, "Cool" },
            { 0, "Mild" },
            { 10, "Warm" },
            { 20, "Balmy" },
            { 25, "Hot" },
            { 30, "Sweltering" },
            { 40, "Scorching" },
        };
        //API call https://goweather.herokuapp.com/weather/{city}

        /*
            >40 == Scorching
            >30 == Sweltering
            >25 == Hot
            >20 == Balmy
            >10 == Warm
            >=0 == Mild
            <=-0 == Cool
            <-5 == Chilly
            <-10 == Bracing
            <-20 == Freezing
        */

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<WeatherForecast> GetAsync(string City)
        {
            WeatherApi weatherApi = new();
            weatherApi.InitializeClient();

            //var rng = new Random();
            //int[] genTemp = {
            //    rng.Next(-20, 55),
            //    rng.Next(-20, 55),
            //    rng.Next(-20, 55),
            //    rng.Next(-20, 55),
            //    rng.Next(-20, 55),
            //};
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    Temperature = genTemp[index-1].ToString(),
            //    Summary = Summaries.Where(w => genTemp[index-1] >= w.Key).LastOrDefault().Value,
            //})
            //.ToArray();
            ///*
            string url = $"https://goweather.herokuapp.com/weather/London";
            using (HttpResponseMessage response = await weatherApi.WeatherClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    WeatherForecast forecast = await response.Content.ReadAsAsync<WeatherForecast>();
                    foreach (Forecast line in forecast.Forecast)
                    {
                        line.dayOfWeek = DateTime.Now.AddDays(line.day).DayOfWeek;
                    }
                    return forecast;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
