using GeoDataAPI.Domain;
using GeoDataAPI.Domain.Interfaces;
using Upd_VM = GeoDataAPI.Domain.ViewModels.Update;
using Ins_VM = GeoDataAPI.Domain.ViewModels.Insert;
using Err_Msgs = GeoDataAPI.Service.ErrorMessagaes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace GeoDataAPI.Service.Controllers
{
    [RoutePrefix("api/v2/featurecodes")]
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
            try
            {
                if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) ||
                                (pageSize == null && pageNumber == null))
                {
                    try
                    {
                        IEnumerable<FeatureCode> result = repository.GetFeatureCodes(featureCodeId: null, pageNumber: pageNumber, pageSize: pageSize);
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
                throw;
            }

        }

        [HttpPut]
        [Route("")]
        [ResponseType(typeof(List<FeatureCode>))]
        public IHttpActionResult UpdateFeatureCodes(IEnumerable<Upd_VM.FeatureCode> featureCodes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<FeatureCode> result = repository.UpdateFeatureCodes(featureCodes);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = featureCodes
                                        .Select(inputFeatureCode => inputFeatureCode.FeatureCodeId)
                                        .FirstOrDefault();

                        byte[] inputRowId = featureCodes
                                        .Where(inputFeatureCode => inputFeatureCode.FeatureCodeId == primaryKey)
                                        .Select(inputFeatureCode => inputFeatureCode.RowId)
                                        .FirstOrDefault();

                        byte[] outputRowId = result
                                        .Where(outputFeatureCode => outputFeatureCode.FeatureCodeId == primaryKey)
                                        .Select(outputFeatureCode => outputFeatureCode.RowId)
                                        .FirstOrDefault();

                        bool rowIdsAreEqual = inputRowId.SequenceEqual(outputRowId);

                        if (rowIdsAreEqual)
                        {
                            return BadRequest(Err_Msgs.ErrorMessages_US_en.NotUpdated_MultipleEntries);

                        }
                        else
                        {
                            return Ok<IEnumerable<FeatureCode>>(result);
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
                return InternalServerError(ex);
                throw;
            }
        }

        [HttpPut]
        [Route("{featureCode:minlength(4)}")]
        [ResponseType(typeof(FeatureCode))]
        public IHttpActionResult UpdateSingleFeatureCode(Upd_VM.FeatureCode featureCode)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Upd_VM.FeatureCode> inputFeatureCodes = new List<Upd_VM.FeatureCode>();
                    inputFeatureCodes.Add(featureCode);
                    IEnumerable<FeatureCode> result = repository.UpdateFeatureCodes(inputFeatureCodes);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = featureCode.FeatureCodeId;

                        byte[] inputRowId = featureCode.RowId;

                        byte[] outputRowId = result
                                        .Where(outputFeatureCode => outputFeatureCode.FeatureCodeId == primaryKey)
                                        .Select(outputFeatureCode => outputFeatureCode.RowId)
                                        .FirstOrDefault();

                        bool rowIdsAreEqual = inputRowId.SequenceEqual(outputRowId);

                        if (rowIdsAreEqual)
                        {
                            return BadRequest(Err_Msgs.ErrorMessages_US_en.NotUpdated_SingleEntry);

                        }
                        else
                        {
                            FeatureCode outputFeatureCode = new FeatureCode();
                            outputFeatureCode = result.FirstOrDefault();
                            return Ok<FeatureCode>(outputFeatureCode);
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
                return InternalServerError(ex);
                throw;
            }
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(List<FeatureCode>))]
        public IHttpActionResult InsertFeatureCodes(List<Ins_VM.FeatureCode >featureCodes)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("featureCode")]
        [ResponseType(typeof(FeatureCode))]
        public IHttpActionResult InsertSingleFeatureCode(Ins_VM.FeatureCode featureCode)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{featureCode:minlength(4)}")]
        public IHttpActionResult DeleteFeatureCode(string featureCodeId)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }
    }
}
