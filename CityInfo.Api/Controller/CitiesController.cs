using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Api.Model;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        ICityInfoRepository _cityInfoRepository;
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }
        [HttpGet()]
        public IActionResult GetCities()
        {
            //return Ok(CityDataStore.current.Cities);

            var cityEntities =_cityInfoRepository.GetCities();

            var results = new List<CityWithoutPointOfInterestDto>();

            foreach (var cityEntity in cityEntities)
            {
                results.Add(new CityWithoutPointOfInterestDto()
                {
                    Id = cityEntity.Id,
                    Name = cityEntity.Name,
                    Description = cityEntity.Description
                });
            }

            return Ok(results);


        }
        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointOfInterest = false)
        {
            //var citiesToReturn = CityDataStore.current.Cities.FirstOrDefault(c => c.Id == id);
            //if (citiesToReturn == null)
            //{
            //    return NotFound();
            //}

            //return Ok(citiesToReturn);


            var cityEntity = _cityInfoRepository.GetCity(id, includePointOfInterest);

            if (cityEntity == null)
            {
                return NotFound();
            }

            if (includePointOfInterest)
            {
                var result = new CityDto() {
                    Id = cityEntity.Id,
                    Description = cityEntity.Description,
                    Name = cityEntity.Name,
                };

                foreach (var pointOfInterestEntity in cityEntity.pointsOfInterests)
                {
                    result.pointsOfInterests.Add(new PointOfInterestsDto() {
                        id = pointOfInterestEntity.Id,
                        Name = pointOfInterestEntity.Name,
                        Description = pointOfInterestEntity.Description
                    });
                }


                return Ok(result);

            }

            var resultCityWithoutPointOfInterestDto = new CityWithoutPointOfInterestDto()
            {
                Id = cityEntity.Id,
                Description = cityEntity.Description,
                Name = cityEntity.Name,
            };

            return Ok(resultCityWithoutPointOfInterestDto);

        }
    }
}