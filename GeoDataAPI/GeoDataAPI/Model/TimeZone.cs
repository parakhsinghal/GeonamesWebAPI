using System;
using System.Collections.Generic;
using GeoDataAPI.Domain.Interfaces;

namespace GeoDataAPI.Domain
{

    public class TimeZone : IVersionable
    {
        public string TimeZoneId { get; set; }
        public string ISOCountryCode { get; set; }        
        public decimal? GMT { get; set; }
        public decimal? DST { get; set; }
        public decimal? RawOffset { get; set; }
        public byte[] RowId { get; set; }
    }
}
