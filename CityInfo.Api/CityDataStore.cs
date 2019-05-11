using CityInfo.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api
{
    public class CityDataStore
    {
        public static CityDataStore current = new CityDataStore();
        public List<CityDto> Cities;

        public CityDataStore()
        {
            Cities = new List<CityDto>(){new CityDto()
            {
                Id = 1,
                Name = "New York",
                Description = "One with Central park",
                pointsOfInterests = new List<PointOfInterestsDto>()
                {
                    new PointOfInterestsDto()
                    {
                        id = 1,
                        Name = "Central Park",
                        Description = "Park.."
                    },
                    new PointOfInterestsDto()
                    {
                        id = 1,
                        Name = "Empire State",
                        Description = "Buildind.."
                    }

                }
            },

            new CityDto()
            {
                Id = 2,
                Name = "Antwerp",
                Description = "One with half tower",
                pointsOfInterests = new List<PointOfInterestsDto>()
                {
                    new PointOfInterestsDto()
                    {
                        id = 1,
                        Name = "Central Park",
                        Description = "Park.."
                    },
                    new PointOfInterestsDto()
                    {
                        id = 1,
                        Name = "Empire State",
                        Description = "Buildind.."
                    }

                }
            },

            new CityDto()
            {
                Id = 3,
                Name = "Paris",
                Description = "Big tower",
                pointsOfInterests = new List<PointOfInterestsDto>()
                {
                    new PointOfInterestsDto()
                    {
                        id = 1,
                        Name = "Central Park",
                        Description = "Park.."
                    },
                    new PointOfInterestsDto()
                    {
                        id = 1,
                        Name = "Empire State",
                        Description = "Buildind.."
                    }

                }
            }};

        }
    }
   
}
