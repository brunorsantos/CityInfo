using CityInfo.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api
{
    public static class CityInfoContextExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {

            if (context.Cities.Any())
            {
                return;
            }

            var cities = new List<City>()
            {
                new City()
                {
                    Name = "Test city",
                    Description = "DEscription test1",
                    pointsOfInterests = new List<PointOfInterests>()
                    {
                        new PointOfInterests()
                        {
                            Name = "Point of interest 1",
                            Description = "Point of interest description 1"
                        }
                    }

                },
                  new City()
                {
                    Name = "Test city2",
                    Description = "DEscription test2",
                    pointsOfInterests = new List<PointOfInterests>()
                    {
                        new PointOfInterests()
                        {
                            Name = "Point of interest 2",
                            Description = "Point of interest description 2"
                        }
                    }

                }
            };

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }
    }
}
