using System;
using System.Collections.Generic;
using System.Text;

namespace Airports.Models
{
    public class AirportInfo
    {
        string AirportId { get; set; }
        string AirportName { get; set; }
        string CityName { get; set; }
        string CountryName { get; set; }
        string IATACode { get; set; }
        string ICAOCode { get; set; }
        string Latitude { get; set; }
        string Longtitude { get; set; }
        string Altitude { get; set; }

        public AirportInfo()
        {

        }

        public AirportInfo(string id, string airportName, string cityName, string countrytName, string iataCode,
                                    string icaoCode, string latitude, string longtitude, string altitude)
        {
            AirportId = id;
            AirportName = airportName;
            CityName = cityName;
            CountryName = countrytName;
            IATACode = iataCode;
            ICAOCode = icaoCode;
            Latitude = latitude;
            Longtitude = longtitude;
            Altitude = altitude;
        }
    }
}
