using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var client = new MongoClient("mongodb+srv://liveOm:Omkar29hs-owl@cluster0.ew0x3.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");

            var database = client.GetDatabase("sample_geospatial");
            var collection = database.GetCollection<BsonDocument>("shipwrecks"); 
            

            return null;
        }
    }
}
