﻿using GeoDataAPI.Domain;
using GeoDataAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Upd_VM = GeoDataAPI.Domain.ViewModels.Update;
using Ins_VM = GeoDataAPI.Domain.ViewModels.Insert;
using Err_Msgs = GeoDataAPI.Service.ErrorMessagaes;

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
        [ResponseType(typeof(List<FeatureCategory>))]
        public IHttpActionResult UpdateFeatureCategories(IEnumerable<Upd_VM.FeatureCategory> featureCategories)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<FeatureCategory> result = repository.UpdateFeatureCategories(featureCategories);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = featureCategories
                                        .Select(inputFeatureCategory => inputFeatureCategory.FeatureCategoryId)
                                        .FirstOrDefault();

                        byte[] inputRowId = featureCategories
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
                            return BadRequest(Err_Msgs.ErrorMessages_US_en.NotUpdated_MultipleEntries);

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

        [HttpPut]
        [Route("{featureCategoryId:alpha:length(1)}")]
        [ResponseType(typeof(FeatureCategory))]
        public IHttpActionResult UpdateFeatureCategory(Upd_VM.FeatureCategory featureCategory)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    List<Upd_VM.FeatureCategory> inputFeatureCategories = new List<Upd_VM.FeatureCategory>();
                    inputFeatureCategories.Add(featureCategory);
                    IEnumerable<FeatureCategory> result = repository.UpdateFeatureCategories(inputFeatureCategories);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = featureCategory.FeatureCategoryId;

                        byte[] inputRowId = featureCategory.RowId;

                        byte[] outputRowId = result
                                        .Where(outputFeatureCategory => outputFeatureCategory.FeatureCategoryId == primaryKey)
                                        .Select(outputFeatureCategory => outputFeatureCategory.RowId)
                                        .FirstOrDefault();

                        bool rowIdsAreEqual = inputRowId.SequenceEqual(outputRowId);

                        if (rowIdsAreEqual)
                        {
                            return BadRequest(Err_Msgs.ErrorMessages_US_en.NotUpdated_SingleEntry);

                        }
                        else
                        {
                            FeatureCategory outputFeatureCategory = new FeatureCategory();
                            outputFeatureCategory = result.FirstOrDefault();
                            return Ok<FeatureCategory>(outputFeatureCategory);
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
