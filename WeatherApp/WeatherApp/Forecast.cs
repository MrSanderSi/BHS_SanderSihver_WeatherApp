using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Forecast
    {
        public int day { get; set; }
        public DayOfWeek dayOfWeek { get; set; }
        public string Temperature { get; set; }
        public string Wind { get; set; }
    }
}
