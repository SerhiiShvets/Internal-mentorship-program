using System;
using System.Collections.Generic;
using System.Text;

namespace Airports.Models
{
    public class RetrievedAirportData
    {
        public int AirportId { get; set; }
        public string AirportName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string IATACode { get; set; }
        public string ICAOCode { get; set; }
        public Location Location { get; set; }
        public string TimeZoneInfoId { get; set; }
        public RetrievedAirportData()
        {

        }

        public RetrievedAirportData(int id, string airportName, string cityName, string countrytName, string iataCode, 
                                    string icaoCode, double latitude, double longtitude, double altitude, 
                                    string timeZoneInfoId)
        {
            AirportId = id;
            AirportName = airportName;
            CityName = cityName;
            CountryName = countrytName;
            IATACode = iataCode;
            ICAOCode = icaoCode;
            Location = new Location
            {
                Latitude = latitude,
                Longtitude = longtitude,
                Altitude = altitude
            };
            TimeZoneInfoId = timeZoneInfoId;
        }
    }
}
