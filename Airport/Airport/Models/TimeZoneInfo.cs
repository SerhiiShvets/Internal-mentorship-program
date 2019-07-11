using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Airports.Models
{
    [DataContract]
    public class TimeZoneInfo
    {
        [DataMember]
        public int AirportId { get; set; }
        [DataMember]
        public string TimeZoneInfoId { get; set; }
    }
}
