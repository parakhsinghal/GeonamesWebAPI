using GeoDataAPI.Domain;
using GeoDataAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace GeoDataAPI.Service.Controllers
{
    [RoutePrefix("api/languages")]
    public class LanguageController : ApiController
    {
        private ILanguageCodeRepository repository;

        public LanguageController(ILanguageCodeRepository _repository)
        {
            this.repository = _repository;
        }

        [Route("")]
        [ResponseType(typeof(IEnumerable<LanguageCode>))]
        public IHttpActionResult GetAllLanguageCodes(int? pageNumber = null, int? pageSize = null)
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

        [Route("{iso6393Code:length(3):alpha}")]
        [Route("{language:minlength(4):regex([a-zA-Z]+[ a-zA-Z-_]*)}")]
        [ResponseType(typeof(LanguageCode))]
        public IHttpActionResult GetLanguageCode(string iso6393Code = null, string language = null)
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
            catch (Exception)
            {
                return InternalServerError();
                throw;
            }
        }
    }
}
