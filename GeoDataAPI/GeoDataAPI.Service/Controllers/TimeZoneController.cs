using GeoDataAPI.Domain;
using GeoDataAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace GeoDataAPI.Service.Controllers
{
    [RoutePrefix("api/timezones")]
    public class TimeZoneController : ApiController
    {
        private ITimeZoneRepository repository;

        public TimeZoneController(ITimeZoneRepository _repository)
        {
            this.repository = _repository;
        }

        [Route("")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult GetDistinctTimeZones()
        {
            IEnumerable<string> result = repository.GetDistinctTimeZones();

            if (result != null && result.Count() > 0)
            {
                return Ok(result);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Route("{timeZoneId:regex(.*\\/.*)}")]
        [Route("{isoCountryCode:alpha:length(2)}")]
        [Route("{iso3Code:alpha:length(3)}")]
        [Route("{isoNumeric:int}")]
        [Route("{latitude:double}/{longitude:double}")]
        [Route("{countryName:alpha:length(4,50)}")]
        [Route("details")]
        [ResponseType(typeof(List<GeoDataAPI.Domain.TimeZone>))]
        public IHttpActionResult GetTimeZoneDetails(string timeZoneId = null, string isoCountryCode = null, string iso3Code = null, int? isoNumeric = null, string countryName = null, double? latitude = null, double? longitude = null, int? pageNumber = null, int? pageSize = null)
        {
            if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) || (pageSize == null && pageNumber == null))
            {
                try
                {
                    IEnumerable<GeoDataAPI.Domain.TimeZone> result = repository.GetTimeZoneDetails(timeZoneId, isoCountryCode, iso3Code, isoNumeric, countryName, latitude, longitude, pageNumber, pageSize);
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

        [Route("place/{placeName:regex([a-zA-Z]+[ a-zA-Z-_]*)}")]
        [ResponseType(typeof(List<GeoDataAPI.Domain.TimeZone>))]
        public IHttpActionResult GetTimeZoneDetailsByPlaceName(string placeName)
        {
            if (!string.IsNullOrWhiteSpace(placeName) && !string.IsNullOrEmpty(placeName))
            {
                try
                {
                    IEnumerable<GeoDataAPI.Domain.TimeZone> result = repository.GetTimeZoneDetailsByPlaceName(placeName);
                    if (result != null || result.Count() > 0)
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
                return BadRequest("Please provide a valid value for name of the place of which the time zone is required.");
            }
        }
    }
}