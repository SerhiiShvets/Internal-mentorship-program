using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Device.Location;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using Airports.Models;
using System.Globalization;
using Newtonsoft.Json;
using NLog;


namespace Airports
{
    class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {

            CultureInfo ciEn = new CultureInfo("en-US");
            CultureInfo ciHu = new CultureInfo("hu-HU");
            CultureInfo.CurrentCulture = ciEn;
            CultureInfo.CurrentUICulture = ciEn;

            string[] readFile = File.ReadAllLines(@"C:\Users\Сергій\Desktop\EPAM\Internal\Lesson 4 Common .NET Techniques\airports.dat");

            //retrieving data from timezones.json
            List<Models.TimeZoneInfo> allTimeZoneInfo = JsonConvert.DeserializeObject<List<Models.TimeZoneInfo>>
                (File.ReadAllText(@"C:\Users\Сергій\Desktop\EPAM\Internal\Lesson 4 Common .NET Techniques\Airport\Airport\bin\Debug\netcoreapp2.1\timezoneinfo.json"));

            //var correctedReadFile = readFile
            //                        .Select(x => x.Replace("\"", string.Empty));

            var splittedCorrectedReadFile = readFile
                                    .Select(x => Regex.Replace(x, "['\"]", string.Empty).Split(new char[] { ',' }));

            var allAirportInfo = splittedCorrectedReadFile
                                              .Select(x => new
                                              {
                                                  AirportId = Convert.ToInt32(x[0]),
                                                  AirportName = x[1],
                                                  CityName = x[2],
                                                  CountryName = x[3],
                                                  IATACode = x[4],
                                                  ICAOCode = x[5],
                                                  Latitude = Convert.ToDouble(x[6]),
                                                  Longtitude = Convert.ToDouble(x[7]),
                                                  Altitude = Convert.ToDouble(x[8])
                                              }).ToList();

            //var splittedCorrectedReadFile = correctedReadFile
            //                                .Select(x => x.Split(new char[] { ',' }).ToArray());


            var airportsSelectedData = new List<RetrievedAirportData>();
            
            try
            {
                airportsSelectedData = allAirportInfo.Join(allTimeZoneInfo,
                                       s => Convert.ToInt32(s.AirportId),
                                       a => a.AirportId,
                                       (s, a) => new RetrievedAirportData
                                       (s.AirportId,
                                       s.AirportName,
                                       s.CityName,
                                       s.CountryName,
                                       s.IATACode,
                                       s.ICAOCode,
                                       Convert.ToDouble(s.Latitude),
                                       Convert.ToDouble(s.Longtitude),
                                       Convert.ToDouble(s.Altitude),
                                       a.TimeZoneInfoId))
                                      .ToList();
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);
            }

            AddWordAirportToAirportNamesIfNeeded(airportsSelectedData);

            var countries = GetCountries(airportsSelectedData);

            var cities = GetCities(airportsSelectedData, countries);

            var airports = GetAirports(airportsSelectedData, cities);

            
            //Serialization
            Serialize(cities, "cities.json");
            Serialize(countries, "countries.json");
            Serialize(airports, "airports.json");

            var listOfCountriesByNameWithNumberOfAirports = airportsSelectedData
                                                            .OrderBy(x => x.CountryName)
                                                            .GroupBy(x => x.CountryName)
                                                            .Select(x => new {
                                                                                CountryName = x.Key,
                                                                                NumberOfAirports = x.Count()
                                                                             });

            var citiesWhichHaveTheMostAirports = (airportsSelectedData.GroupBy(x => new { CityName = x.CityName, CountryName = x.CountryName }))
                                                 .OrderByDescending(x => x.Count());




            Console.ReadKey();
            
        }

        private static bool FilesPresent(string dataFileDirectoryPath)
        {
            IEnumerable<string> dirs = Directory.EnumerateDirectories(dataFileDirectoryPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string d in dirs)
            {
                IEnumerable<string> files = Directory.EnumerateFiles(d, "*.*", SearchOption.AllDirectories);
                if (files.Any()) { return true; }
            }
            return false;
        }

        public static void AddWordAirportToAirportNamesIfNeeded(List<RetrievedAirportData> retrievedAirportData)
        {
            foreach (var s in retrievedAirportData)
            {
                if (!s.AirportName.EndsWith("Airport"))
                {
                    s.AirportName = s.AirportName + " Airport";
                }
            }
        }

        public static List<Models.Country> GetCountries(List<RetrievedAirportData> data)
        {
            var allCountriesISO3166Data = ISO3166.Country.List;
            List<string> countries = data
                            .OrderBy(x => x.CountryName)
                            .Select(x => x.CountryName)
                            .Distinct()
                            .ToList();

            var listOfCountries = countries
                                  .Select(x => new Models.Country
                                  {
                                      Id = countries.IndexOf(x),
                                      Name = x,
                                      TwoLetterISOCode = allCountriesISO3166Data.Select(y => y.TwoLetterCode).ElementAt(countries.IndexOf(x)),
                                      ThreeLetterISOCode = allCountriesISO3166Data.Select(y => y.ThreeLetterCode).ElementAt(countries.IndexOf(x))
                                  })
                                  .ToList();
            return listOfCountries;
        }

        public static List<City> GetCities(IEnumerable<RetrievedAirportData> data, List<Models.Country> listOfCountries)
        {
            var cities = data
                         .OrderBy(x => x.CityName)
                         .Select(x => x.CityName)
                         .Distinct()
                         .ToList();

            var countries = data
                            .OrderBy(x => x.CountryName)
                            .Select(x => x.CountryName)
                            .Distinct()
                            .ToList();

            var query = data.Join(countries,
                                  d => d.CountryName,
                                  c => c,
                                  (d, c) => new {
                                      CountryName = d.CountryName,
                                      CityName = d.CityName,
                                      CountryId = countries.IndexOf(d.CountryName),
                                      TimeZoneInfoId = d.TimeZoneInfoId
                                  });

            var listOfCities = cities.Join(query,
                                      c => c,
                                      q => q.CityName,
                                      (c, q) => new City
                                      {
                                          Id = cities.IndexOf(c),
                                          Name = c,
                                          CountryId = q.CountryId,
                                          TimeZoneInfoId = q.TimeZoneInfoId
                                      })
                                     .ToList();
            return listOfCities;
        }

        public static List<Airport> GetAirports(IEnumerable<RetrievedAirportData> data, List<City> cities)
        {
            var listOfAirports = new List<Airport>();

            listOfAirports = data.Join(cities,
                                    d => d.CityName,
                                    c => c.Name,
                                    (d, c) => new Airport
                                    {
                                        Id = d.AirportId,
                                        CityId = c.Id,
                                        CountryyId = c.CountryId,
                                        FullName = d.AirportName,
                                        IATACode = d.IATACode,
                                        ICAOCode = d.ICAOCode,
                                        Location = d.Location,
                                        TimeZoneInfoId = d.TimeZoneInfoId,
                                        Name = d.AirportName
                                    })
                                    .ToList();

            return listOfAirports;
        }

        public static void Serialize<T>(List<T> list, string nameOfFileToCreate)
        {
            DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(List<T>));
            using (FileStream fs = new FileStream(nameOfFileToCreate, FileMode.OpenOrCreate))
            {
                formatter.WriteObject(fs, list);
            }
        }

        public static string GetClosestAirport(IEnumerable<RetrievedAirportData> data)
        {
            Console.WriteLine("Input Latitude, please (between -90 and 90)");
            string latitude = Console.ReadLine();

            Console.WriteLine("Input Longtitude, please (between -180 and 180)");
            string longtitude = Console.ReadLine();

            string result = "";

            if (Double.TryParse(latitude, out double checkedLatitude) && Double.TryParse(longtitude, out double checkedLongtitude))
            {
                var firstCoordinate = new GeoCoordinate(checkedLatitude, checkedLongtitude);
                var locationsAndDistancesToInputPoint = data
                                .Select(x => new
                                {
                                    Name = x.AirportName,
                                    City = x.CityName,
                                    DistanceToInputPoint = firstCoordinate.GetDistanceTo(new GeoCoordinate(x.Location.Latitude, x.Location.Longtitude))
                                });

                var closestDistance = locationsAndDistancesToInputPoint
                                     .OrderBy(x => x.DistanceToInputPoint)
                                     .Select(x => x)
                                     .First();
                result = $"The closest airpot is {closestDistance.Name} in {closestDistance.City}, {closestDistance.DistanceToInputPoint}km";
                return result;
            }
            else
            {
                Console.WriteLine("Input data is not valid");
                return result;
            }
        }
    }
}


//airportsSelectedData = from s in splittedCorrectedReadFile
//                                           join a in allTimeZoneInfo on Convert.ToInt32(s[0]) equals a.AirportId
//                                           select new RetrievedAirportData{ AirportId = Convert.ToInt32(s[0]), AirportName = s[1] + " Airport", CityName = s[2], CountryName = s[3], IATACode = s[4], ICAOCode = s[5], Latitude = Convert.ToDouble(s[6]), Longtitude = Convert.ToDouble(s[7]), Altitude = Convert.ToInt32(s[8]), a.TimeZoneInfoId };