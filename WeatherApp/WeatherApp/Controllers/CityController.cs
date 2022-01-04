using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using WeatherApp.Models;


namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public List<City> Get()
        {
            string query = @"
                    select ID, CityName, Country from dbo.City";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader reader;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand customCommand = new SqlCommand(query, con))
                {
                    reader = customCommand.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                    con.Close();
                }
            }
            List<City> result = new List<City>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                int Id = (int)row["ID"];
                string CityName = row["CityName"].ToString();
                string CountryName = row["Country"].ToString();
                City city = new City()
                {
                    ID = Id,
                    CityName = CityName,
                    CountryName = CountryName
                };
                result.Add(city);

            }
            return result;
        }
    }
}
