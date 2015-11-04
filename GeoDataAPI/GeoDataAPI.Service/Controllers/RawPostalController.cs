using GeoDataAPI.Domain;
using GeoDataAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace GeoDataAPI.Service.Controllers
{
    [RoutePrefix("api/postalcodes")]
    public class RawPostalController : ApiController
    {
        private IRawPostalRepository repository;

        public RawPostalController(IRawPostalRepository _repository)
        {
            this.repository = _repository;
        }

        [Route("{isoCountryCode:alpha:length(2)}/{postalCode?}")]
        [Route("{countryName:alpha:minlength(3)}/{postalCode?}")]
        [ResponseType(typeof(List<RawPostal>))]
        public IHttpActionResult GetPostalCodesInfo(string isoCountryCode = null, string countryName = null, string postalCode = null, int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if ((!string.IsNullOrEmpty(isoCountryCode) && !string.IsNullOrWhiteSpace(isoCountryCode)) ||
                               (!string.IsNullOrEmpty(countryName) && (!string.IsNullOrWhiteSpace(countryName))))
                {
                    if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) ||
                        (pageSize == null && pageNumber == null))
                    {
                        try
                        {
                            var result = repository.GetPostalInfo(isoCountryCode, countryName, postalCode, pageNumber, pageSize);

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
                else
                {
                    return BadRequest("Please provide a valid value of ISO country code or country name.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
                throw;
            }

        }
    }
}