using System.Collections.Generic;

namespace GeoDataAPI.Domain.Interfaces
{
    public interface ITimeZoneRepository
    {
        IEnumerable<string> GetDistinctTimeZones();

        IEnumerable<GeoDataAPI.Domain.TimeZone> GetTimeZoneDetails(string timeZoneId = null, string isoCountryCode = null, string iso3Code = null, int? isoNumeric = null, string countryName = null, double? latitude = null, double? longitude = null, int? pageNumber = null, int? pageSize = null);

        IEnumerable<GeoDataAPI.Domain.TimeZone> GetTimeZoneDetailsByPlaceName(string placeName);
    }
}
