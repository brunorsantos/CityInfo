﻿using CityInfo.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Services
{
    public interface ICityInfoRepository
    {
        bool CityExists(int id);
        IEnumerable<City> GetCities();

        City GetCity(int cityId, bool includePointsOfInterest);

        IEnumerable<PointOfInterests> GetPointsOfInterests(int cityId);

        PointOfInterests GetPointOfInterest(int cityId, int pointOfInterestId);

        void AddPointOfInterestForCity(int cityId, PointOfInterests pointOfInterest);

        bool Save();
    }
}
