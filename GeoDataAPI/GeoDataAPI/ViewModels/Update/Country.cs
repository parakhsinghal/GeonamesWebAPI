using System;
using System.Collections.Generic;
using GeoDataAPI.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GeoDataAPI.Domain.ViewModels.Update
{
    public class Country : IVersionable
    {
        [Required(ErrorMessageResourceName = "Country_ISOCountryCode_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(2, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string ISOCountryCode { get; set; }

        [Required(ErrorMessageResourceName = "Country_ISO3Code_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(3, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string ISO3Code { get; set; }

        [Required(ErrorMessageResourceName = "Country_ISONumeric_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public int? ISONumeric { get; set; }

        [Required(ErrorMessageResourceName = "Country_CountryName_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(200, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string CountryName { get; set; }

        [Required(ErrorMessageResourceName = "Country_ContinentCodeId_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        [StringLength(2, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
               ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string ContinentCodeId { get; set; }

        [Required(ErrorMessageResourceName = "Country_GeonameId_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public long? GeonameId { get; set; }

         [StringLength(2, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string FIPSCode { get; set; }

         [StringLength(200, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string Capital { get; set; }
        public double? SqKmArea { get; set; }
        public long? TotalPopulation { get; set; }

         [StringLength(10, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string TopLevelDomain { get; set; }

         [StringLength(4, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string CurrencyCode { get; set; }

         [StringLength(128, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string CurrencyName { get; set; }

         [StringLength(32, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string Phone { get; set; }

         [StringLength(64, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string PostalFormat { get; set; }

         [StringLength(256, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string PostalRegex { get; set; }

         [StringLength(256, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string Languages { get; set; }

         [StringLength(256, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string Neighbors { get; set; }

         [StringLength(64, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string EquivalentFipsCode { get; set; }

        [Required(ErrorMessageResourceName = "Country_RowId_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public byte[] RowId { get; set; }
    }
}
