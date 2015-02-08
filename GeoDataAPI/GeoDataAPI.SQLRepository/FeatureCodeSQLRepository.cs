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
    public class FeatureCodeSQLRepository : IFeatureCode
    {
        public FeatureCodeSQLRepository()
        {
            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["Geonames"].ConnectionString;
        }

        public IEnumerable<FeatureCode> GetFeatureCodes(string featureCodeId, int? pageNumber, int? pageSize)
        {
            string sql = SQLRepositoryHelper.GetFeatureCodeInfo;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("FeatureCodeId", featureCodeId));
            parameterCollection.Add(new SqlParameter("PageNumber", pageNumber));
            parameterCollection.Add(new SqlParameter("PageSize", pageSize));

            List<FeatureCode> result = new List<FeatureCode>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result.Add(new FeatureCode()
                            {
                                FeatureCodeId = dr["FeatureCodeId"] != null ? dr.Field<string>("FeatureCodeId") : string.Empty,
                                FeatureCodeName = dr["FeatureCodeId"] != null ? dr.Field<string>("FeatureCodeId") : string.Empty,
                                Description = dr["Description"] != null ? dr.Field<string>("Description") : string.Empty,
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