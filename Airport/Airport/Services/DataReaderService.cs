using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Airports.Models;
using Newtonsoft.Json;

namespace Airports.Services
{
    public class DataReaderService
    {
        public DataReaderService()
        {

        }

        private bool CheckIfFilesPresent(string dataFileDirectoryPath)
        {
            IEnumerable<string> dirs = Directory.EnumerateDirectories(dataFileDirectoryPath, "*.*", SearchOption.TopDirectoryOnly);

            foreach (string d in dirs)
            {
                IEnumerable<string> files = Directory.EnumerateFiles(d, "*.*", SearchOption.AllDirectories);
                if (files.Any()) { return true; }
            }
            return false;
        }

        public string[] ReadAirportsDat()
        {
            string[] readAirportsData = File.ReadAllLines(@"C:\Users\Сергій\Desktop\EPAM\Internal\Lesson 4 Common .NET Techniques\airports.dat");
            return readAirportsData;
        }

        public List<Models.TimeZoneInfo> ReadTimeZoneInfoJson()
        {
            return JsonConvert.DeserializeObject<List<Models.TimeZoneInfo>>
                (File.ReadAllText(@"C:\Users\Сергій\Desktop\EPAM\Internal\Lesson 4 Common .NET Techniques\Airport\Airport\bin\Debug\netcoreapp2.1\timezoneinfo.json"));
        }

        public IEnumerable<string[]> SplitAirportsData(string[] readAirportsData)
        {
            var splittedAirportsData = readAirportsData
                        .Select(x => Regex.Replace(x, "['\"]", string.Empty).Split(new char[] { ',' }));
            return splittedAirportsData;
        }

        public List<RetrievedAirportData> GetAirportInfo(IEnumerable<string[]> splittedAirportsData)
        {
            var allAirportInfo = splittedAirportsData
                                              .Select(x => new RetrievedAirportData
                                              {
                                                  AirportId = Convert.ToInt32(x[0]),
                                                  AirportName = x[1],
                                                  CityName = x[2],
                                                  CountryName = x[3],
                                                  IATACode = x[4],
                                                  ICAOCode = x[5],
                                                  Location =  new Location
                                                  {
                                                      Latitude = Convert.ToDouble(x[6]),
                                                      Longtitude = Convert.ToDouble(x[7]),
                                                      Altitude = Convert.ToDouble(x[8])
                                                  }
                                              }).ToList();
            return allAirportInfo;
        }
    }
}
