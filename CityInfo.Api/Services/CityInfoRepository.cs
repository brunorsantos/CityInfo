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

        public PointOfInterests GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            return _context.PointsOfInterests.Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefault();

        }

        public IEnumerable<PointOfInterests> GetPointsOfInterests(int cityId)
        {
            return _context.PointsOfInterests.Where(p => p.CityId == p.CityId).ToList();
        }
    }
}
