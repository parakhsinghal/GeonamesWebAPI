using GeoDataAPI.DALHelper;
using GeoDataAPI.Domain;
using GeoDataAPI.Domain.Interfaces;
using GeoDataAPI.SQLRepository.Helper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace GeoDataAPI.SQLRepository
{
    public class LanguageCodeSQLRepository : ILanguageCodeRepository
    {
        public LanguageCodeSQLRepository()
        {
            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["Geonames"].ConnectionString;
        }

        public IEnumerable<LanguageCode> GetLanguageInfo(string iso6393Code = null, string language = null, int? pageNumber = null, int? pageSize = null)
        {
            string sql = SQLRepositoryHelper.GetLanguageInfo;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("ISO6393Code", iso6393Code));
            parameterCollection.Add(new SqlParameter("Language", language));
            parameterCollection.Add(new SqlParameter("PageNumber", pageNumber));
            parameterCollection.Add(new SqlParameter("PageSize", pageSize));

            List<LanguageCode> result = new List<LanguageCode>();
            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result.Add(new LanguageCode()
                            {
                                ISO6391 = dr.Field<string>("ISO6391"),
                                ISO6392 = dr.Field<string>("ISO6392"),
                                ISO6393 = dr.Field<string>("ISO6393"),
                                Language = dr.Field<string>("Language"),
                                RowId = dr.Field<byte[]>("RowId")
                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}