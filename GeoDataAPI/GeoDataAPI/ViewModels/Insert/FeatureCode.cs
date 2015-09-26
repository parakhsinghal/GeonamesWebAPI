using System;
using System.Collections.Generic;
using GeoDataAPI.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GeoDataAPI.Domain.ViewModels.Insert
{
    public class FeatureCode 
    {
        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(16, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string FeatureCodeId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Error",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(128, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string FeatureCodeName { get; set; }

        [StringLength(512, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string Description { get; set; }
    }
}
