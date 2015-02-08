using GeoDataAPI.Domain;
using GeoDataAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace GeoDataAPI.Service.Controllers
{
    [RoutePrefix("api/featurecodes")]
    public class FeatureCodeController : ApiController
    {
        private IFeatureCode repository;

        public FeatureCodeController(IFeatureCode _repository)
        {
            this.repository = _repository;
        }

        [Route("")]
        [ResponseType(typeof(List<FeatureCode>))]
        public IHttpActionResult GetFeatureCodes(int? pageNumber = null, int? pageSize = null)
        {

            if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) ||
                (pageSize == null && pageNumber == null))
            {
                try
                {
                    IEnumerable<FeatureCode> result = repository.GetFeatureCodes(featureCodeId:null, pageNumber:pageNumber, pageSize:pageSize);
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
                return BadRequest("Both pageSize and pageNumber properties need to have valid values.");
            }
        }
    }
}
