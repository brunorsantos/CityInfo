using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

            var cityEntities =_cityInfoRepository.GetCities();
            var results = Mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cityEntities);
                
            return Ok(results);


        }
        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointOfInterest = false)
        {
            var cityEntity = _cityInfoRepository.GetCity(id, includePointOfInterest);

            if (cityEntity == null)
            {
                return NotFound();
            }

            if (includePointOfInterest)
            {
                var result = Mapper.Map<CityDto>(cityEntity);
                return Ok(result);

            }
            var resultCityWithoutPointOfInterestDto = Mapper.Map<CityWithoutPointOfInterestDto>(cityEntity);

            return Ok(resultCityWithoutPointOfInterestDto);

        }
    }
}