using System;
using GeoDataAPI.Domain;
using Upd_VM = GeoDataAPI.Domain.ViewModels.Update;
using Ins_VM = GeoDataAPI.Domain.ViewModels.Insert;
using System.Collections.Generic;

namespace GeoDataAPI.Domain.Interfaces
{
    public interface IContinentRepository
    {
        IEnumerable<Continent> GetContinentInfo(string continentCodeId = null, long? geonameId = null, string continentName = null);

        IEnumerable<Country> GetCountriesInAContinent(string continentName = null, string continentCodeId = null, long? geonameId = null, int? pageNumber = null, int? pageSize = null);

        IEnumerable<Continent> UpdateContinents(IEnumerable<Upd_VM.Continent> continents);

        IEnumerable<Continent> InsertContinents(IEnumerable<Ins_VM.Continent> continents);
    }
}
