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
    public class FeatureCategorySQLRepository : IFeatureCategoryRepository
    {
        public FeatureCategorySQLRepository()
        {
            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["Geonames"].ConnectionString;
        }

        public IEnumerable<FeatureCategory> GetFeatureCategories(string featureCategoryId)
        {
            string sql = SQLRepositoryHelper.GetFeatureCategoryInfo;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("FeatureCategoryId", featureCategoryId));

            List<FeatureCategory> result = new List<FeatureCategory>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result.Add(new FeatureCategory()
                            {
                                FeatureCategoryId = dr["FeatureCategoryId"] != null ? dr.Field<string>("Featurecategoryid") : string.Empty,
                                FeatureCategoryName = dr["FeatureCategoryName"] != null ? dr.Field<string>("FeatureCategoryName") : string.Empty,
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