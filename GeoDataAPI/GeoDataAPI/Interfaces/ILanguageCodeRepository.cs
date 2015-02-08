using System;
using System.Collections.Generic;
using GeoDataAPI.Domain;

namespace GeoDataAPI.Domain.Interfaces
{
    public interface ILanguageCodeRepository
    {
        IEnumerable<LanguageCode> GetLanguageInfo(string iso6393Code = null, string language = null, int? pageNumber = null, int? pageSize = null);
    }
}
