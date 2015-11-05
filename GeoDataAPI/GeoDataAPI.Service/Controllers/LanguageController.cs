using GeoDataAPI.Domain;
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
    [RoutePrefix("api/v2/languages")]
    public class LanguageController : ApiController
    {
        private ILanguageCodeRepository repository;

        public LanguageController(ILanguageCodeRepository _repository)
        {
            this.repository = _repository;
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<LanguageCode>))]
        public IHttpActionResult GetAllLanguageCodes(int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) || (pageSize == null && pageNumber == null))
                {
                    try
                    {
                        var result = repository.GetLanguageInfo(iso6393Code: null, language: null, pageNumber: pageNumber, pageSize: pageSize);

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

        [HttpGet]
        [Route("{iso6393Code:length(3):alpha}")]
        [Route("{language:minlength(4):regex([a-zA-Z]+[ a-zA-Z-_]*)}")]
        [ResponseType(typeof(LanguageCode))]
        public IHttpActionResult GetLanguageCode(string iso6393Code = null, string language = null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(iso6393Code) || !string.IsNullOrWhiteSpace(language))
                {
                    try
                    {
                        LanguageCode result = repository.GetLanguageInfo(iso6393Code: iso6393Code, language: language, pageNumber: null, pageSize: null)
                                                     .FirstOrDefault<LanguageCode>();
                        if (result.RowId != null)
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
                    return BadRequest("Please provide a valid value of ISO6393 code or language name.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
                throw;
            }

        }

        [HttpGet]
        [Route("keyvalue")]
        [ResponseType(typeof(KeyValuePair<string, string>))]
        public IHttpActionResult GetLanguageCodesAsDictionary()
        {
            try
            {
                var result = repository.GetLanguageInfo(iso6393Code: null, language: null, pageNumber: null, pageSize: null)
                                            .ToDictionary(item => item.ISO6393, item => item.Language).OrderBy(item => item.Value);
                if (result != null && result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
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
        [ResponseType(typeof(List<Upd_VM.LanguageCode>))]
        public IHttpActionResult UpdateLanguageCodes(IEnumerable<Upd_VM.LanguageCode> languageCodes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<LanguageCode> result = repository.UpdateLanguages(languageCodes);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = languageCodes
                                        .Select(inputLanguageCode => inputLanguageCode.ISO6393)
                                        .FirstOrDefault();

                        byte[] inputRowId = languageCodes
                                        .Where(inputLanguageCode => inputLanguageCode.ISO6393 == primaryKey)
                                        .Select(inputLanguageCode => inputLanguageCode.RowId)
                                        .FirstOrDefault();

                        byte[] outputRowId = result
                                        .Where(outputLanguageCode => outputLanguageCode.ISO6393 == primaryKey)
                                        .Select(outputLanguageCode => outputLanguageCode.RowId)
                                        .FirstOrDefault();

                        bool rowIdsAreEqual = inputRowId.SequenceEqual(outputRowId);

                        if (rowIdsAreEqual)
                        {
                            return BadRequest(Err_Msgs.ErrorMessages_US_en.NotUpdated_MultipleEntries);

                        }
                        else
                        {
                            return Ok<IEnumerable<LanguageCode>>(result);
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
        [Route("{iso6393Code:length(3):alpha}")]
        [Route("{language:minlength(4):regex([a-zA-Z]+[ a-zA-Z-_]*)}")]
        [ResponseType(typeof(Upd_VM.LanguageCode))]
        public IHttpActionResult UpdateSingleLanguageCode(Upd_VM.LanguageCode languageCode)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Upd_VM.LanguageCode> inputLanguageCodes = new List<Upd_VM.LanguageCode>();
                    inputLanguageCodes.Add(languageCode);
                    IEnumerable<LanguageCode> result = repository.UpdateLanguages(inputLanguageCodes);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = languageCode.ISO6393;

                        byte[] inputRowId = languageCode.RowId;

                        byte[] outputRowId = result
                                            .Where(outputLanguageCode => outputLanguageCode.ISO6393 == primaryKey)
                                            .Select(outputLanguageCode => outputLanguageCode.RowId)
                                            .FirstOrDefault();

                        bool rowIdsAreEqual = inputRowId.SequenceEqual(outputRowId);

                        if (rowIdsAreEqual)
                        {
                            return BadRequest(Err_Msgs.ErrorMessages_US_en.NotUpdated_SingleEntry);

                        }
                        else
                        {
                            LanguageCode outputLanguageCode = new LanguageCode();
                            outputLanguageCode = result.FirstOrDefault();
                            return Ok<LanguageCode>(outputLanguageCode);
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
        [ResponseType(typeof(List<LanguageCode>))]
        public IHttpActionResult InsertLanguageCodes(List<Ins_VM.LanguageCode> languageCodes)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("languageCode")]
        [ResponseType(typeof(LanguageCode))]
        public IHttpActionResult InsertSingleLanguageCode(Ins_VM.LanguageCode languageCode)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{iso6393Code:length(3):alpha}")]
        public IHttpActionResult DeleteLanguageCode(string iso6393Code)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }
    }
}
