using System;
using GeoDataAPI.Domain;
using System.Collections.Generic;

namespace GeoDataAPI.Domain.Interfaces
{
    public interface IContinentRepository
    {
        IEnumerable<Continent> GetContinentInfo(string continentCodeId = null, long? geonameId = null, string continentName = null);

        IEnumerable<Country> GetCountriesInAContinent(string continentName = null, string continentCodeId = null, long? geonameId = null, int? pageNumber = null, int? pageSize = null);
    }
}
