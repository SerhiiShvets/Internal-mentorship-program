using System;
using System.Collections.Generic;
using System.Text;

namespace Airports.Models
{
    public class AirportInfo
    {
        public int AirportId { get; set; }
        public string AirportName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string IATACode { get; set; }
        public string ICAOCode { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public double Altitude { get; set; }

        public AirportInfo()
        {

        }

        public AirportInfo(int id, string airportName, string cityName, string countrytName, string iataCode,
                                    string icaoCode, double latitude, double longtitude, double altitude)
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
