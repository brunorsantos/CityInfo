using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CityInfo.Api.Entities;
using CityInfo.Api.Model;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CityInfo.Api.Controller
{
    [Route("api/cities")]
    [ApiController]
    public class PointsOfInterestsController : ControllerBase
    {
        private ILogger<PointsOfInterestsController> _logger;
        private IMailService _mailService;
        ICityInfoRepository _cityInfoRepository;
        public PointsOfInterestsController(ILogger<PointsOfInterestsController> logger, IMailService mailService, ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _cityInfoRepository = cityInfoRepository;
        }
        [HttpGet("{cityId}/pointsofinterests")]
        public IActionResult getPointsOfInterests(int cityId)
        {


            try
            {

                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City {cityId} was not found");
                    return NotFound();
                }

                var pointsOfInterestEntity = _cityInfoRepository.GetPointsOfInterests(cityId);
                var result = Mapper.Map<IEnumerable<PointOfInterestsDto>>(pointsOfInterestEntity);



                return Ok(result);

            }
            catch (Exception)
            {
                _logger.LogCritical($"A problem while processing {cityId}");
                return StatusCode(500,"A problem occoured");
            }

        }

        [HttpGet("{cityId}/pointsofinterests/{id}", Name = "GetPointOfInterest")]
        public IActionResult getPointOfInterest(int cityId, int id)
        {

            if (!_cityInfoRepository.CityExists(cityId))
            {
                _logger.LogInformation($"City {cityId} was not found");
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterest(cityId, id);

            if (pointOfInterestEntity == null)
            {
                _logger.LogInformation($"Point of interest {id} was not found");
                return NotFound();
            }

            var result = Mapper.Map<PointOfInterestsDto>(pointOfInterestEntity);

            return Ok(result);
        }


        [HttpPost("{cityId}/pointsofinterests")]
        public IActionResult CreatePointOfIneterest(int cityId,
            [FromBody] PointOfInterestsForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "Fields Should be different");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = Mapper.Map<PointOfInterests>(pointOfInterest);

            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);

            _cityInfoRepository.Save();

            var createdPointOfInterest = Mapper.Map<PointOfInterestsDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new { cityId = cityId, id = createdPointOfInterest.id }, createdPointOfInterest);
        }

        [HttpPut("{cityId}/pointsofinterests/{id}")]
        public IActionResult UpdatePointOfIneterest(int cityId, int id,
           [FromBody] PointOfInterestsForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "Fields Should be different");
            }


            //Para valicoes em projetos mais complexos usar fluentValidation
            // https://github.com/JeremySkinner/FluentValidation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CityDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.pointsOfInterests.FirstOrDefault(p => p.id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterests/{id}")]
        public IActionResult PartiallyUpdatePointOfIneterest(int cityId, int id,
           [FromBody] JsonPatchDocument<PointOfInterestsForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var city = CityDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.pointsOfInterests.FirstOrDefault(p => p.id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestsForUpdateDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Name == pointOfInterestToPatch.Description)
            {
                ModelState.AddModelError("Description", "Fields Should be different");
            }


            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();

        }

        [HttpDelete("{cityId}/pointsofinterests/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            var city = CityDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return BadRequest();
            }

            var pointOfInterestDataStore = city.pointsOfInterests.FirstOrDefault(p => p.id == id);

            if (pointOfInterestDataStore == null)
            {
                return BadRequest();
            }

            city.pointsOfInterests.Remove(pointOfInterestDataStore);

            _mailService.Send($"Deteleting point of insterest", $"Point of interest {pointOfInterestDataStore.Name} was deleted");
            return NoContent();

        }
    }

}
