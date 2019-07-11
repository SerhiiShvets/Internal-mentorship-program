using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using Airports.Models;
using System.Globalization;
using ISO3166;
using Newtonsoft.Json;
using System.Threading;
using NLog;


namespace Airports
{
    class Program
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

      

        static void Main(string[] args)
        {

            CultureInfo ciEn = new CultureInfo("en-US");
            CultureInfo ciHu = new CultureInfo("hu-HU");
            Thread.CurrentThread.CurrentCulture = ciEn;
            Thread.CurrentThread.CurrentUICulture = ciEn;

            string[] readFile = File.ReadAllLines(@"C:\Users\Сергій\Desktop\EPAM\Internal\Lesson 4 Common .NET Techniques\airports.dat");

            //var correctedReadFile = readFile
            //                        .Select(x => x.Replace("\"", string.Empty));
            
            var correctedReadFile = readFile
                                    .Select(x => Regex.Replace(x, "['\"]", string.Empty));

            var splittedCorrectedReadFile = correctedReadFile
                                            .Select(x => x.Split(new char[] { ',' }).ToArray());           
           
            var dictOfLocations = new Dictionary<int, Location>();

            var countries = splittedCorrectedReadFile
                            .OrderBy(x => x[3])
                            .Select(x => x[3])
                            .Distinct()
                            .ToArray();

            var cities = splittedCorrectedReadFile
                         .OrderBy(x => x[2])
                         .Select(x => x[2])
                         .Distinct()
                         .ToArray();

            //Parsing of the countries
            var dictOfCountries = CreateDictionaryOfCountries(countries, _logger);

            var distinctCitiesWithCountries = (from everyString in splittedCorrectedReadFile
                                               orderby everyString[2]
                                               //At the very beginning it was an anonymous type but I wasnt sure it was a good idea
                                               //to pass an anonymous type into a method as a parameter. 
                                               //So I created CityWithCountry class. But I still have doubts about it
                                               select new CityWithCountry
                                               {
                                                   CityName = everyString[2],
                                                   CountryName = everyString[3],
                                                   CountryId = (from country in dictOfCountries
                                                                where country.Value.Name == everyString[3]
                                                                select country.Value.Id)
                                                                .FirstOrDefault()
                                               })
                                               .Distinct()
                                               .ToArray();
            string type = distinctCitiesWithCountries.GetType().ToString();
            //var count = distinctCitiesWithCountries.Count();

            //Parcing of the cities
            var dictOfCities = CreateDictionaryOfCities(distinctCitiesWithCountries, _logger);

            //retrieving data from timezones.json
            List<Models.TimeZoneInfo> allTimeZoneInfo = JsonConvert.DeserializeObject<List<Models.TimeZoneInfo>>
                (File.ReadAllText(@"C:\Users\Сергій\Desktop\EPAM\Internal\Lesson 4 Common .NET Techniques\Airport\Airport\bin\Debug\netcoreapp2.1\timezoneinfo.json"));

            //Parsing of the airports
            var dictOfAirports = CreateDictionaryOfAirports
                (splittedCorrectedReadFile, dictOfCountries, allTimeZoneInfo, dictOfCities, _logger);

            //An assigning of timezones to cities
            AssignTimeZonesToCities(dictOfCities, dictOfAirports, _logger);
                   
            //Serialization
            Serialize(dictOfCities, "cities.json");
            Serialize(dictOfCountries, "countries.json");
            Serialize(dictOfAirports, "airports.json");

            var listOfCountriesByNameWithNumberOfAirports = from data in splittedCorrectedReadFile
                                                            orderby data[3]
                                                            select new { Country = data[3], };
           




            //var countriesWithoutCode = dictOfCountries.Select(x => x.Value)
            //                                          .Where(x => x.TwoLetterISOCode == null)
            //                                          .Select(x =>x);

            //var twoLetterCodesOfCultures = (from cltrInf in  CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            //                                select cltrInf.TwoLetterISOLanguageName.ToUpper()).Distinct();

            //for (int i = 0; i < twoLetterCodesOfCultures.Count(); i++)
            //{
            //    string regionInfoEnglishName = twoLetterCodesOfCultures.ElementAt(i);
                
            //    CultureInfo cultureInfo = new CultureInfo(regionInfoEnglishName);
         

            //    try
            //    {
            //        RegionInfo regionInfo = new RegionInfo(cultureInfo.LCID);
            //        dictOfCountries.Add(i, new Models.Country
            //        {
            //            Id = i,
            //            Name = countries.ElementAt(i),
            //            TwoLetterISOCode = regionInfo.TwoLetterISORegionName,
            //            ThreeLetterISOCode = regionInfo.ThreeLetterISORegionName
            //        });
            //    }
            //    catch { }    
            //}

            //var query = dictOfCountries.Select(x => x.Value)
            //                                .Select(x => x.TwoLetterISOCode);
            //var anonymousObjects = new Dictionary<int, object>();

           

            

          

            //foreach (KeyValuePair<int, object> anObject in anonymousObjects)
            //{
            //    airports.Add(new Airport { Id = anObject. });
            //}


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

        public static List<RegionInfo> GetCountriesByIso3166()
        {
            List<RegionInfo> countries = new List<RegionInfo>();
            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo country = new RegionInfo(culture.LCID);
                if (countries.Where(p => p.Name == country.Name).Count() == 0)
                    countries.Add(country);
            }
            return countries.OrderBy(p => p.EnglishName).ToList();
        }

        public static List<RegionInfo> GetCountriesByCode(List<string> codes)
        {
            List<RegionInfo> countries = new List<RegionInfo>();
            if (codes != null && codes.Count > 0)
            {
                foreach (string code in codes)
                {
                    try
                    {
                        countries.Add(new RegionInfo(code));
                    }
                    catch
                    {
                       
                    }
                }
            }
            return countries.OrderBy(p => p.EnglishName).ToList();
        }

        public static void Serialize<T>(Dictionary<int, T> dictionary, string nameOfFileToCreate)
        {
            var array = dictionary.Values.ToArray();
            DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(T[]));
            using (FileStream fs = new FileStream(nameOfFileToCreate, FileMode.OpenOrCreate))
            {
                formatter.WriteObject(fs, array);
            }

        }

        public static Dictionary<int, Airport> CreateDictionaryOfAirports
            (IEnumerable<string[]> readFile, Dictionary<int, Models.Country> countries,
            List<Models.TimeZoneInfo> tzInfo, Dictionary<int, City> cities, Logger logger)
        {
            var dictOfAirports = new Dictionary<int, Airport>();

            foreach (string[] s in readFile)
            {
                try
                {
                    var ap = new
                    {
                        ID = s[0],
                        AirportName = s[1] + "Airport",
                        CityName = s[2],
                        CountryName = s[3],
                        IATACode = s[4],
                        ICAOCode = s[5],
                        Latitude = s[6],
                        Longtitude = s[7],
                        Altitude = s[8],
                        CountryyId = countries
                                     .Where(x => x.Value.Name == s[3])
                                     .Select(x => x.Value.Id)
                                     .First(),
                        TimeZoneName = tzInfo
                                       .Where(x => x.AirportId == Convert.ToInt32(s[0]))
                                       .Select(x => x.TimeZoneInfoId)
                                       .FirstOrDefault(),
                    };
                    dictOfAirports.Add(Convert.ToInt32(ap.ID), new Airport
                    {
                        Id = Convert.ToInt32(ap.ID),
                        CountryyId = ap.CountryyId,
                        CityId = cities
                                .Where(x => x.Value.Name == ap.CityName)
                                .Select(x => x.Value.Id)
                                .FirstOrDefault(),
                        Name = s[1],
                        IATACode = ap.IATACode,
                        ICAOCode = ap.ICAOCode,
                        FullName = ap.AirportName,
                        TimeZoneName = ap.TimeZoneName,
                        Location = new Location
                        {
                            Latitude = Convert.ToDouble(ap.Latitude),
                            Longtitude = Convert.ToDouble(ap.Longtitude),
                            Altitude = Convert.ToDouble(ap.Altitude),
                        }
                    });
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }

            }
            return dictOfAirports;
        }
        public static void AssignTimeZonesToCities(Dictionary<int, City> cities, Dictionary<int, Airport> airports, Logger logger)
        {
            foreach (var kv in cities.Values)
            {
                try
                {
                    kv.TimeZoneName = airports
                                      .Where(x => x.Value.CityId == kv.Id)
                                      .Select(x => x.Value.TimeZoneName)
                                      .First();
                }
                catch(Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static Dictionary<int, City> CreateDictionaryOfCities(CityWithCountry[] distinctCitiesWithCountries, Logger logger)
        {
            var dictOfCities = new Dictionary<int, City>();

            try
            {
                for (int i = 0; i < distinctCitiesWithCountries.Length; i++)
                {
                    dictOfCities.Add(i, new City
                    {
                        Id = i,
                        Name = distinctCitiesWithCountries[i].CityName,
                        CounryId = distinctCitiesWithCountries[i].CountryId,
                    });
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);
            }
            return dictOfCities;
        }

        public static Dictionary<int, Models.Country> CreateDictionaryOfCountries(string[] countries, Logger logger)
        {
            var allCountriesISO3166Data = ISO3166.Country.List;
            var dictOfCountries = new Dictionary<int, Models.Country>();
            try
            {
                for (int i = 0; i < countries.Length; i++)
                {
                    if (countries.Contains(allCountriesISO3166Data.Select(x => x.Name).ElementAt(i)))
                    {
                        dictOfCountries.Add(i, new Models.Country
                        {
                            Id = i,
                            Name = allCountriesISO3166Data.Select(x => x.Name).ElementAt(i),
                            TwoLetterISOCode = allCountriesISO3166Data.Select(x => x.TwoLetterCode).ElementAt(i),
                            ThreeLetterISOCode = allCountriesISO3166Data.Select(x => x.ThreeLetterCode).ElementAt(i)
                        });
                    }
                    else
                    {
                        dictOfCountries.Add(i, new Models.Country
                        {
                            Id = i,
                            Name = countries[i],

                        });
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);
            }
            return dictOfCountries;
        }

    }
}
