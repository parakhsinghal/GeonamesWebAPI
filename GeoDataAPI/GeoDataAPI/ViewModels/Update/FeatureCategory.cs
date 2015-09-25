using System;
using System.Collections.Generic;
using GeoDataAPI.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GeoDataAPI.Domain.ViewModels.Update
{
    public class FeatureCategory
    {
         [Required(ErrorMessageResourceName = "FeatureCategory_FeatureCategoryId_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
         [StringLength(1, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string FeatureCategoryId { get; set; }

         [Required(ErrorMessageResourceName = "FeatureCategory_FeatureCategoryName_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
         [StringLength(128, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string FeatureCategoryName { get; set; }

         [Required(ErrorMessageResourceName = "FeatureCategory_RowId_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public byte[] RowId { get; set; }
    }
}
