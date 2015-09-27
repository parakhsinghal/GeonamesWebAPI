using GeoDataAPI.Domain;
using GeoDataAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Upd_VM = GeoDataAPI.Domain.ViewModels.Update;
using Ins_VM = GeoDataAPI.Domain.ViewModels.Insert;

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

        [HttpGet]
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

        [HttpPut]
        [Route("")]
        //[Route("{featureCategoryId:alpha:length(1)}")]
        [ResponseType(typeof(List<FeatureCategory>))]
        public IHttpActionResult UpdateFeatureCategories(IEnumerable<Upd_VM.FeatureCategory> featurecategories)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<FeatureCategory> result = repository.UpdateFeatureCategories(featurecategories);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = featurecategories
                                        .Select(inputFeatureCategory => inputFeatureCategory.FeatureCategoryId)
                                        .FirstOrDefault();

                        byte[] inputRowId = featurecategories
                                        .Where(inputFeatureCategory => inputFeatureCategory.FeatureCategoryId == primaryKey)
                                        .Select(inputFeatureCategory => inputFeatureCategory.RowId)
                                        .FirstOrDefault();

                        byte[] outputRowId = result
                                        .Where(outputFeatureCategory => outputFeatureCategory.FeatureCategoryId == primaryKey)
                                        .Select(outputFeatureCategory => outputFeatureCategory.RowId)
                                        .FirstOrDefault();

                        bool rowIdsAreEqual = inputRowId.SequenceEqual(outputRowId);

                        if (rowIdsAreEqual)
                        {
                            return BadRequest(GeoDataAPI.Service.ErrorMessagaes.ErrorMessages_US_en.NotUpdated);

                        }
                        else
                        {
                            return Ok<IEnumerable<FeatureCategory>>(result);
                        }

                    }
                    else
                    {
                        return InternalServerError();
                    }

                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError();
                throw;
            }

        }
    }
}
