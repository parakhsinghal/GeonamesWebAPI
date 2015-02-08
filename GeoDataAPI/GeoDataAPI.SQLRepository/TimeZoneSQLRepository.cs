using GeoDataAPI.DALHelper;
using GeoDataAPI.Domain.Interfaces;
using GeoDataAPI.SQLRepository.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GeoDataAPI.SQLRepository
{
    public class TimeZoneSQLRepository : ITimeZoneRepository
    {
        public TimeZoneSQLRepository()
        {
            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["Geonames"].ConnectionString;
        }

        public IEnumerable<string> GetDistinctTimeZones()
        {
            try
            {
                string sql = SQLRepositoryHelper.GetDistinctTimeZones;
                List<string> result = new List<string>();

                using (DBDataHelper helper = new DBDataHelper())
                {
                    using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection: null))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                result.Add(dr.Field<string>("TimeZoneId"));
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<Domain.TimeZone> GetTimeZoneDetails(string timeZoneId = null, string isoCountryCode = null, string iso3Code = null, int? isoNumeric = null, string countryName = null, double? latitude = null, double? longitude = null, int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                string sql = SQLRepositoryHelper.GetTimeZoneDetails;
                List<SqlParameter> parameterCollection = new List<SqlParameter>();
                parameterCollection.Add(new SqlParameter("TimeZoneId", timeZoneId));
                parameterCollection.Add(new SqlParameter("ISOCountryCode", isoCountryCode));
                parameterCollection.Add(new SqlParameter("ISO3Code", iso3Code));
                parameterCollection.Add(new SqlParameter("ISONumeric", isoNumeric));
                parameterCollection.Add(new SqlParameter("CountryName", countryName));
                parameterCollection.Add(new SqlParameter("Latitude", latitude));
                parameterCollection.Add(new SqlParameter("Longitude", longitude));
                parameterCollection.Add(new SqlParameter("PageNumber", pageNumber));
                parameterCollection.Add(new SqlParameter("PageSize", pageSize));

                List<Domain.TimeZone> result = new List<Domain.TimeZone>();
                using (DBDataHelper helper = new DBDataHelper())
                {
                    using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                result.Add(new Domain.TimeZone()
                                {
                                    TimeZoneId = dr["TimeZoneId"] == DBNull.Value ? string.Empty : dr.Field<string>("TimeZoneId"),
                                    ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr.Field<string>("ISOCountryCode"),
                                    GMT = dr["GMT"] == DBNull.Value ? null : dr.Field<decimal?>("GMT"),
                                    DST = dr["DST"] == DBNull.Value ? null : dr.Field<decimal?>("DST"),
                                    RawOffset = dr["RawOffset"] == DBNull.Value ? null : dr.Field<decimal?>("RawOffset"),
                                    RowId = dr.Field<byte[]>("RowId")
                                });
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<Domain.TimeZone> GetTimeZoneDetailsByPlaceName(string placeName)
        {

            try
            {
                string sql = SQLRepositoryHelper.GetTimeZoneDetailsByPlaceName;
                List<SqlParameter> parameterCollection = new List<SqlParameter>();
                parameterCollection.Add(new SqlParameter("PlaceName", placeName));

                List<Domain.TimeZone> result = new List<Domain.TimeZone>();
                using (DBDataHelper helper = new DBDataHelper())
                {
                    using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                result.Add(new Domain.TimeZone()
                                {
                                    TimeZoneId = dr["TimeZoneId"] == DBNull.Value ? string.Empty : dr.Field<string>("TimeZoneId"),
                                    ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr.Field<string>("ISOCountryCode"),
                                    GMT = dr["GMT"] == DBNull.Value ? null : dr.Field<decimal?>("GMT"),
                                    DST = dr["DST"] == DBNull.Value ? null : dr.Field<decimal?>("DST"),
                                    RawOffset = dr["RawOffset"] == DBNull.Value ? null : dr.Field<decimal?>("RawOffset"),
                                    RowId = dr.Field<byte[]>("RowId")
                                });
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}