using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace GeoDataAPI.SQLRepository.Helper
{
    public static class SQLRepositoryHelper
    {
        #region Continent

        public static readonly string GetContinentInfo = ConfigurationManager.AppSettings["GetContinentInfo"];
        public static readonly string GetCountriesInAContinent = ConfigurationManager.AppSettings["GetCountriesInAContinent"];

        #endregion

        #region Country

        public static readonly string GetCountryInfo = ConfigurationManager.AppSettings["GetCountryInfo"];
        public static readonly string GetCountryFeatureCategoryFeatureCode = ConfigurationManager.AppSettings["GetCountryFeatureCategoryFeatureCode"];

        #endregion

        #region State

        public static readonly string GetStateInfo = ConfigurationManager.AppSettings["GetStateInfo"];
        public static readonly string GetCitiesInAState = ConfigurationManager.AppSettings["GetCitiesInAState"];

        #endregion

        #region TimeZone

        public static readonly string GetTimeZoneDetails = ConfigurationManager.AppSettings["GetTimeZoneDetails"];
        public static readonly string GetTimeZoneDetailsByPlaceName = ConfigurationManager.AppSettings["GetTimeZoneDetailsByPlaceName"];
        public static readonly string GetDistinctTimeZones = ConfigurationManager.AppSettings["GetDistinctTimeZones"];

        #endregion

        #region FeatureCategory

        public static readonly string GetFeatureCategoryInfo = ConfigurationManager.AppSettings["GetFeatureCategoryInfo"];

        #endregion

        #region FeatureCode

        public static readonly string GetFeatureCodeInfo = ConfigurationManager.AppSettings["GetFeatureCodeInfo"];

        #endregion

        #region LanguageSQLRepository

        public static readonly string GetLanguageInfo = ConfigurationManager.AppSettings["GetLanguageInfo"];

        #endregion

        #region RawPostalRepository

        public static readonly string GetPostalCodeInfo = ConfigurationManager.AppSettings["GetPostalCodeInfo"];

        #endregion
    }
}