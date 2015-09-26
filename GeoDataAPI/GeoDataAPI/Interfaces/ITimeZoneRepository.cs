using System.Collections.Generic;
using System;
using Upd_VM = GeoDataAPI.Domain.ViewModels.Update;
using Ins_VM = GeoDataAPI.Domain.ViewModels.Insert;

namespace GeoDataAPI.Domain.Interfaces
{
    public interface ITimeZoneRepository
    {
        IEnumerable<string> GetDistinctTimeZones();

        IEnumerable<GeoDataAPI.Domain.TimeZone> GetTimeZoneDetails(string timeZoneId = null, string isoCountryCode = null, string iso3Code = null, int? isoNumeric = null, string countryName = null, double? latitude = null, double? longitude = null, int? pageNumber = null, int? pageSize = null);

        IEnumerable<GeoDataAPI.Domain.TimeZone> GetTimeZoneDetailsByPlaceName(string placeName);

        IEnumerable<GeoDataAPI.Domain.TimeZone> UpdateTimeZones (IEnumerable<Upd_VM.TimeZone> timeZones);

        IEnumerable<GeoDataAPI.Domain.TimeZone> InsertTimeZones (IEnumerable<Ins_VM.TimeZone> timeZones);
    }
}
