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
    [RoutePrefix("api/timezones")]
    public class TimeZoneController : ApiController
    {
        private ITimeZoneRepository repository;

        public TimeZoneController(ITimeZoneRepository _repository)
        {
            this.repository = _repository;
        }

        [HttpGet]
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

        [HttpGet]
        [Route("timezone/{continent:regex([a-z][a-z0-9_])}/{country:regex([a-z][a-z0-9_])}/{state:regex([a-z][a-z0-9_])?}")]
        [Route("{isoCountryCode:alpha:length(2)}")]
        [Route("{iso3Code:alpha:length(3)}")]
        [Route("{isoNumeric:int}")]
        [Route("{latitude:double}/{longitude:double}")]
        [Route("{countryName:alpha:length(4,50)}")]
        [Route("details")]
        [ResponseType(typeof(List<GeoDataAPI.Domain.TimeZone>))]
        public IHttpActionResult GetTimeZoneDetails(string continent = null, string country = null, string state = null, string isoCountryCode = null, string iso3Code = null, int? isoNumeric = null, string countryName = null, double? latitude = null, double? longitude = null, int? pageNumber = null, int? pageSize = null)
        {
            if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) || (pageSize == null && pageNumber == null))
            {
                try
                {
                    string timeZoneId = null;

                    if ((!string.IsNullOrEmpty(continent)&& !string.IsNullOrWhiteSpace(continent))&&
                        (!string.IsNullOrEmpty(country) && !string.IsNullOrWhiteSpace(country)))
                    {
                        timeZoneId = continent + "/" + country;
                        if (!string.IsNullOrEmpty(state)&& !string.IsNullOrWhiteSpace(state))
                        {
                            timeZoneId += "/" + state;
                        }
                    }
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

        [HttpGet]
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

        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateTimeZones(IEnumerable<Upd_VM.TimeZone> timeZones)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("timezone/{continent:regex([a-z][a-z0-9_])}/{country:regex([a-z][a-z0-9_])}/{state:regex([a-z][a-z0-9_])?}")]
        public IHttpActionResult UpdateTimeZone(Upd_VM.TimeZone timeZone)
        {
            throw new NotImplementedException();
        }
    }
}