using System;
using GeoDataAPI.Domain;
using System.Collections.Generic;

namespace GeoDataAPI.Domain.Interfaces
{
    public interface IFeatureCode
    {
        IEnumerable<FeatureCode> GetFeatureCodes(string featureCodeId, int? pageNumber, int? pageSize);
    }
}
