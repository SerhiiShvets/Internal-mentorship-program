using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Airports.Models
{
    [DataContract]
    public class City
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int CountryId { get; set; }
        [DataMember]
        public string TimeZoneInfoId { get; set; }
    }
}
