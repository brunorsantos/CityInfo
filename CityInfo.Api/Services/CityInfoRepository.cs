using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Api.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        CityInfoContext _context;

        public bool CityExists(int id)
        {
            return _context.Cities.Any(c => c.Id == id);
        }
        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return _context.Cities.Include(c => c.pointsOfInterests).Where(c => c.Id == cityId).FirstOrDefault();
            }



            return _context.Cities.Where(c => c.Id == cityId).FirstOrDefault();
        }

        public PointOfInterestsEntity GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            return _context.PointsOfInterests.Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefault();

        }

        public IEnumerable<PointOfInterestsEntity> GetPointsOfInterests(int cityId)
        {
            return _context.PointsOfInterests.Where(p => p.CityId == cityId).ToList();
        }

        public void AddPointOfInterestForCity(int cityId, PointOfInterestsEntity pointOfInterest)
        {
            var city = this.GetCity(cityId, false);
            city.pointsOfInterests.Add(pointOfInterest);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public void DeletePointOfInterest(PointOfInterestsEntity pointOfInterest)
        {
            _context.PointsOfInterests.Remove(pointOfInterest);
        }
    }
}
