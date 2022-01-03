using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WeatherApp.Data.WebApi
{
    public class WeatherApi
    {
        public HttpClient WeatherClient { get; set; }

        public void InitializeClient()
        {
            WeatherClient = new HttpClient();
            WeatherClient.BaseAddress = new Uri("https://goweather.herokuapp.com/weather/");
            WeatherClient.DefaultRequestHeaders.Accept.Clear();
            WeatherClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}
