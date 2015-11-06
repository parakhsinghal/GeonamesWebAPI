using System;
using System.Collections.Generic;
using GeoDataAPI.Domain;
using Upd_VM = GeoDataAPI.Domain.ViewModels.Update;
using Ins_VM = GeoDataAPI.Domain.ViewModels.Insert;

namespace GeoDataAPI.Domain.Interfaces
{
    public interface ILanguageCodeRepository
    {
        IEnumerable<LanguageCode> GetLanguageInfo(string iso6393Code = null, string language = null, int? pageNumber = null, int? pageSize = null);

        IEnumerable<LanguageCode> UpdateLanguages(IEnumerable<Upd_VM.LanguageCode> languageCodes);

        IEnumerable<LanguageCode> InsertLanguages (IEnumerable<Ins_VM.LanguageCode> languageCodes);

        int DeleteLanguageCode(string iso6393Code);
    }
}
