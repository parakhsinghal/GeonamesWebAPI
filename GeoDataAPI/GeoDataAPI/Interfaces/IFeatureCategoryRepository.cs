using System;
using GeoDataAPI.Domain;
using System.Collections.Generic;
using Upd_VM = GeoDataAPI.Domain.ViewModels.Update;
using Ins_VM = GeoDataAPI.Domain.ViewModels.Insert;

namespace GeoDataAPI.Domain.Interfaces
{
    public interface IFeatureCategoryRepository
    {
        IEnumerable<FeatureCategory> GetFeatureCategories(string featureCategoryId);

        IEnumerable<FeatureCategory> UpdateFeatureCategories(IEnumerable<Upd_VM.FeatureCategory> featureCategories);

        IEnumerable<FeatureCategory> InsertFeatureCategories(IEnumerable<Ins_VM.FeatureCategory> featureCategories);
    }
}
