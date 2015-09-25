using System;
using System.Collections.Generic;
using GeoDataAPI.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GeoDataAPI.Domain.ViewModels.Update
{
    public class LanguageCode : IVersionable
    {
        [Required(ErrorMessageResourceName = "LanguageCode_ISO6393_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(24, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string ISO6393 { get; set; }

        [StringLength(24, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string ISO6392 { get; set; }

        [StringLength(24, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string ISO6391 { get; set; }

        [Required(ErrorMessageResourceName = "LanguageCode_Language_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(128, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string Language { get; set; }

        [Required(ErrorMessageResourceName = "LanguageCode_RowId_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public byte[] RowId { get; set; }
    }
}
