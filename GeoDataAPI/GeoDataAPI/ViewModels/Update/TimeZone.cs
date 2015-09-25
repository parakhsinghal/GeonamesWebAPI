using System;
using System.Collections.Generic;
using GeoDataAPI.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GeoDataAPI.Domain.ViewModels.Update
{

    public class TimeZone : IVersionable
    {
        [Required(ErrorMessageResourceName = "TimeZone_TimeZoneId_Required",
                 ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(128, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string TimeZoneId { get; set; }

        [Required(ErrorMessageResourceName = "TimeZone_ISOCountryCode_Required",
                 ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(2, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string ISOCountryCode { get; set; }

        [Required(ErrorMessageResourceName = "TimeZone_GMT_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public decimal? GMT { get; set; }

        [Required(ErrorMessageResourceName = "TimeZone_DST_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public decimal? DST { get; set; }

        [Required(ErrorMessageResourceName = "TimeZone_RawOffset_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public decimal? RawOffset { get; set; }

        [Required(ErrorMessageResourceName = "TimeZone_RowId_Required",
                 ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public byte[] RowId { get; set; }
    }
}
