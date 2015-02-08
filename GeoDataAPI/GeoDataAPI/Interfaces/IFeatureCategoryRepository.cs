using System;
using GeoDataAPI.Domain;
using System.Collections.Generic;

namespace GeoDataAPI.Domain.Interfaces
{
    public interface IFeatureCategoryRepository
    {
        IEnumerable<FeatureCategory> GetFeatureCategories(string featureCategoryId);
    }
}
