using System;
using GeoDataAPI.Domain;
using System.Collections.Generic;
using Upd_VM = GeoDataAPI.Domain.ViewModels.Update;
using Ins_VM = GeoDataAPI.Domain.ViewModels.Insert;

namespace GeoDataAPI.Domain.Interfaces
{
    public interface IFeatureCode
    {
        IEnumerable<FeatureCode> GetFeatureCodes(string featureCodeId, int? pageNumber, int? pageSize);

        IEnumerable<FeatureCode> UpdateFeatureCodes(IEnumerable<Upd_VM.FeatureCode> featureCodes);

        IEnumerable<FeatureCode> InsertFeatureCodes(IEnumerable<Ins_VM.FeatureCode> featureCodes);

        int DeleteFeatureCode(string featureCodeId);
    }
}
