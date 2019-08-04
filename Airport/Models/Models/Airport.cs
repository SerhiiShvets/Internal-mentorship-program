using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Airports.Models
{
    [DataContract]
    public class Airport
    {
        [DataMember]
        public int CityId { get; set; }
        [DataMember]
        public int CountryyId { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string IATACode { get; set; }
        [DataMember]
        public string ICAOCode { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string TimeZoneInfoId { get; set; }
        [DataMember]
        public Location Location { get; set; }

        //public Airport(int id, string airportName, string cityName, string countryName, )
    }
}
