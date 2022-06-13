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


        IMongoCollection<ShipWreck> _shipwreckCollection;

        public WeatherForecastController(IMongoClient client)
        {
            var database = client.GetDatabase("sample_geospatial");
            this._shipwreckCollection = database.GetCollection<ShipWreck>("shipwrecks");
        }

        [HttpGet]
        public IEnumerable<ShipWreck> Get()
        {
            return this._shipwreckCollection.Find(s=>s.FeatureType=="Wrecks - Visible").ToList();
        }

        [HttpGet("{index:int}")]
        public ShipWreck Get(int index)
        {
            return this._shipwreckCollection.Find(s => s.FeatureType == "Wrecks - Visible").ToList()[index];
        }
    }
}
