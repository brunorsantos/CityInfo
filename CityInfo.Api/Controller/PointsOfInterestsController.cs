using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controller
{
    [Route("api/cities")]
    [ApiController]
    public class PointsOfInterestsController : ControllerBase
    {
        [HttpGet("{cityId}/pointsofinterests")]
        public IActionResult getPointsOfInterests(int cityId)
        {
            var city = CityDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.pointsOfInterests);
        }

        [HttpGet("{cityId}/pointsofinterests/{id}", Name = "GetPointOfInterest")]
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


        [HttpPost("{cityId}/pointsofinterests")]
        public IActionResult CreatePointOfIneterest(int cityId, 
            [FromBody] PointOfInterestsForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            var city = CityDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var maxPointofInterestId = CityDataStore.current.Cities
                .SelectMany(c => c.pointsOfInterests).Max(p => p.id);

            var finalPointOfInterest = new PointOfInterestsDto()
            {
                id = ++maxPointofInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };
            city.pointsOfInterests.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", 
                new {cityId = cityId, id = finalPointOfInterest.id }, finalPointOfInterest);
        }

    }

}
