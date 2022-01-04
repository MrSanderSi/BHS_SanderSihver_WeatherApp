using System;

namespace WeatherApp
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public string Temperature { get; set; }

        public string Wind { get; set; }

        public string Description { get; set; }
        public Forecast[] Forecast { get; set; }


        //{"temperature":"-4 °C","wind":"0 km/h","description":"Snow",
        //"forecast":   [{"day":"1","temperature":"-6 °C","wind":"9 km/h"},
        //              {"day":"2","temperature":"-3 °C","wind":"28 km/h"},
        //              {"day":"3","temperature":"0 °C","wind":"21 km/h"}]}
    }
}
