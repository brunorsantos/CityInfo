using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controller
{
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetCities()
        {
            return new JsonResult(CityDataStore.current.Cities);

        }
        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            return new JsonResult(CityDataStore.current.Cities.FirstOrDefault(c => c.Id == id));

        }
    }
}