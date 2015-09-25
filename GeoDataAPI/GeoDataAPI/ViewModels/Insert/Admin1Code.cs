using System;
using System.Collections.Generic;
using GeoDataAPI.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GeoDataAPI.Domain.ViewModels.Insert
{
    public class Admin1Code
    {
        [Required(ErrorMessageResourceName = "Requried_Error",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(32, ErrorMessageResourceName = "Length_Error",
                        ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string Admin1CodeId { get; set; }

        [StringLength(128, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string Admin1CodeName { get; set; }

        [Required(ErrorMessageResourceName = "Requried_Error",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(128, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string ASCIIName { get; set; }
    }
}
