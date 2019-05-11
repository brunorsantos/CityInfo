using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controller
{
    [Route("api/cities")]
    [ApiController]
    public class PointsOfInterestsController : ControllerBase
    {
        [Route("{cityId}/pointsofinterests")]
        public IActionResult getPointsOfInterests(int cityId)
        {
            var city = CityDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.pointsOfInterests);
        }

        [Route("{cityId}/pointsofinterests/{id}")]
        public IActionResult getPointOfInterest(int cityId, int id)
        {
            var city = CityDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.pointsOfInterests.FirstOrDefault(p => p.id == id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

    }

}
