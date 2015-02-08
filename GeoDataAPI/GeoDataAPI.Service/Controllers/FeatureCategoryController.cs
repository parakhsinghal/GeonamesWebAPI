using GeoDataAPI.Domain;
using GeoDataAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace GeoDataAPI.Service.Controllers
{
    [RoutePrefix("api/featurecategories")]
    public class FeatureCategoryController : ApiController
    {
        private IFeatureCategoryRepository repository;

        public FeatureCategoryController(IFeatureCategoryRepository _repository)
        {
            this.repository = _repository;
        }

        [Route("")]
        [Route("{featureCategoryId:alpha:length(1)}")]
        [ResponseType(typeof(List<FeatureCategory>))]
        public IHttpActionResult GetFeatureCategories(string featureCategoryId = null)
        {
            if ((!string.IsNullOrEmpty(featureCategoryId) && featureCategoryId.Length == 1) || string.IsNullOrEmpty(featureCategoryId))
            {
                try
                {
                    IEnumerable<FeatureCategory> result = repository.GetFeatureCategories(featureCategoryId);
                    if (result != null && result.Count() > 0)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return InternalServerError();
                    throw;
                }
            }
            else
            {
                return BadRequest("If you are passing a featureCategoryId the ensure that the length of the string parameter featureCategoryId does not exceed one english alphabet");
            }
        }
    }
}
