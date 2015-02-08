using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoDataAPI.Domain;

namespace GeoDataAPI.Domain.Interfaces
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetAllCountries(int? pageSize = null, int? pageNumber = null);

        Country GetCountryInfo(string isoCountryCode = null, string countryName = null);

        IEnumerable<RawData> GetCountryFeatureCategoryFeatureCode(string featureCategoryId, string isoCountryCode = null, string countryName = null, string featureCode = null, int? pageSize = null, int? pageNumber = null);

        IEnumerable<RawData> GetStates(string countryName = null, string isoCountryCode = null, int? pageNumber = null, int? pageSize = null);

        RawData GetStateInfo(string countryName = null, string isoCountryCode = null, string stateName = null, long? stateGeonameId = null);

        RawData GetCityInState(string countryName = null, string isoCountryCode = null, string stateName = null, long? stateGeonameId = null, long? cityGeonameId = null, string cityName = null);

        IEnumerable<RawData> GetCitiesInState(string countryName = null, string isoCountryCode = null, string stateName = null, long? stateGeonameId = null, long? cityGeonameId = null, string cityName = null, int? pageSize = null, int? pageNumber = null);
    }
}
