using System;
using System.Collections.Generic;
using GeoDataAPI.Domain.Interfaces;

namespace GeoDataAPI.Domain
{
    public class FeatureCategory
    {
        public string FeatureCategoryId { get; set; }
        public string FeatureCategoryName { get; set; }
        public byte[] RowId { get; set; }
    }
}
