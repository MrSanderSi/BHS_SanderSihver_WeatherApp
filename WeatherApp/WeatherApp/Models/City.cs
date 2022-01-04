using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class City
    {
        public int ID { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }

        //public string FullInfo
        //{
        //    get
        //    {
        //        return $"{CityName } { CountryName }";
        //    }
        //}
    }
}
