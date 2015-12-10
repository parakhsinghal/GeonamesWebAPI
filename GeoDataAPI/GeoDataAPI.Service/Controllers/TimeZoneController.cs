using GeoDataAPI.Domain.Interfaces;
using GeoDataAPI.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Upd_VM = GeoDataAPI.Domain.ViewModels.Update;
using Ins_VM = GeoDataAPI.Domain.ViewModels.Insert;
using Err_Msgs = GeoDataAPI.Service.ErrorMessagaes;


namespace GeoDataAPI.Service.Controllers
{
    [RoutePrefix("api/v2/timezones")]
    public class TimeZoneController : ApiController
    {
        private ITimeZoneRepository repository;

        public TimeZoneController(ITimeZoneRepository _repository)
        {
            this.repository = _repository;
        }

        [HttpGet]
        [Route("list")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult GetDistinctTimeZones()
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

        }

        [HttpGet]
        [Route("{isoCountryCode:alpha:length(2)}")]
        [Route("{iso3Code:alpha:length(3)}")]
        [Route("{isoNumeric:int}")]
        [Route("{latitude:double}/{longitude:double}")]
        [Route("{countryName:alpha:length(4,50)}")]
        [Route("")]
        [ResponseType(typeof(List<GeoDataAPI.Domain.TimeZone>))]
        public IHttpActionResult GetTimeZoneDetails(string continent = null, string country = null, string state = null, string isoCountryCode = null, string iso3Code = null, int? isoNumeric = null, string countryName = null, double? latitude = null, double? longitude = null, int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) || (pageSize == null && pageNumber == null))
                {
                    try
                    {
                        string timeZoneId = null;

                        if ((!string.IsNullOrEmpty(continent) && !string.IsNullOrWhiteSpace(continent)) &&
                            (!string.IsNullOrEmpty(country) && !string.IsNullOrWhiteSpace(country)))
                        {
                            timeZoneId = continent + "/" + country;
                            if (!string.IsNullOrEmpty(state) && !string.IsNullOrWhiteSpace(state))
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
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
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
                Debug.WriteLine(ex);
                throw;
            }

        }


        [HttpGet]
        [Route("timezone/{continent:regex([a-z][a-z0-9_])}/{country:regex([a-z][a-z0-9_])}/{state:regex([a-z][a-z0-9_])?}")]
        [ResponseType(typeof(GeoDataAPI.Domain.TimeZone))]
        public IHttpActionResult GetSingleTimeZoneDetails(string continent = null, string country = null, string state = null)
        {
            try
            {
                string timeZoneId = null;

                if ((!string.IsNullOrEmpty(continent) && !string.IsNullOrWhiteSpace(continent)) &&
                    (!string.IsNullOrEmpty(country) && !string.IsNullOrWhiteSpace(country)))
                {
                    timeZoneId = continent + "/" + country;
                    if (!string.IsNullOrEmpty(state) && !string.IsNullOrWhiteSpace(state))
                    {
                        timeZoneId += "/" + state;
                    }
                }
                IEnumerable<GeoDataAPI.Domain.TimeZone> result = repository.GetTimeZoneDetails(timeZoneId, null, null, null, null, null, null, null, null);
                if (result != null && result.Count() > 0)
                {
                    return Ok(result.FirstOrDefault<GeoDataAPI.Domain.TimeZone>());
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

        [HttpGet]
        [Route("place/{placeName:regex([a-zA-Z]+[ a-zA-Z-_]*)}")]
        [ResponseType(typeof(List<GeoDataAPI.Domain.TimeZone>))]
        public IHttpActionResult GetTimeZoneDetailsByPlaceName(string placeName)
        {
            try
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
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        throw;
                    }
                }
                else
                {
                    return BadRequest("Please provide a valid value for name of the place of which the time zone is required.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpPut]
        [Route("")]
        [ResponseType(typeof(List<GeoDataAPI.Domain.TimeZone>))]
        public IHttpActionResult UpdateTimeZones(IEnumerable<Upd_VM.TimeZone> timeZones)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<GeoDataAPI.Domain.TimeZone> result = repository.UpdateTimeZones(timeZones);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = timeZones
                                        .Select(inputTimeZone => inputTimeZone.TimeZoneId)
                                        .FirstOrDefault();

                        byte[] inputRowId = timeZones
                                        .Where(inputTimeZone => inputTimeZone.TimeZoneId == primaryKey)
                                        .Select(inputTimeZone => inputTimeZone.RowId)
                                        .FirstOrDefault();

                        byte[] outputRowId = result
                                        .Where(outputTimeZone => outputTimeZone.TimeZoneId == primaryKey)
                                        .Select(outputTimeZone => outputTimeZone.RowId)
                                        .FirstOrDefault();

                        bool rowIdsAreEqual = inputRowId.SequenceEqual(outputRowId);

                        if (rowIdsAreEqual)
                        {
                            return BadRequest(Err_Msgs.ErrorMessages_US_en.NotUpdated_MultipleEntries);

                        }
                        else
                        {
                            return Ok<IEnumerable<GeoDataAPI.Domain.TimeZone>>(result);
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
                Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpPut]
        [Route("timezone/{continent:regex([a-z][a-z0-9_])}/{country:regex([a-z][a-z0-9_])}/{state:regex([a-z][a-z0-9_])?}")]
        [ResponseType(typeof(GeoDataAPI.Domain.TimeZone))]
        public IHttpActionResult UpdateSingleTimeZone(Upd_VM.TimeZone timeZone)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Upd_VM.TimeZone> inputTimeZones = new List<Upd_VM.TimeZone>();
                    inputTimeZones.Add(timeZone);
                    IEnumerable<GeoDataAPI.Domain.TimeZone> result = repository.UpdateTimeZones(inputTimeZones);

                    if (result != null && result.Count() > 0)
                    {
                        var primaryKey = timeZone.TimeZoneId;

                        byte[] inputRowId = timeZone.RowId;

                        byte[] outputRowId = result
                                        .Where(outputTimeZone => outputTimeZone.TimeZoneId == primaryKey)
                                        .Select(outputTimeZone => outputTimeZone.RowId)
                                        .FirstOrDefault();

                        bool rowIdsAreEqual = inputRowId.SequenceEqual(outputRowId);

                        if (rowIdsAreEqual)
                        {
                            return BadRequest(Err_Msgs.ErrorMessages_US_en.NotUpdated_SingleEntry);

                        }
                        else
                        {
                            GeoDataAPI.Domain.TimeZone outputTimeZone = new GeoDataAPI.Domain.TimeZone();
                            outputTimeZone = result.FirstOrDefault();
                            return Ok<GeoDataAPI.Domain.TimeZone>(outputTimeZone);
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
                Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(List<GeoDataAPI.Domain.TimeZone>))]
        public IHttpActionResult InsertTimeZones(List<Ins_VM.TimeZone> timeZones)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("timeZone")]
        [ResponseType(typeof(GeoDataAPI.Domain.TimeZone))]
        public IHttpActionResult InsertSingleTimeZone(Ins_VM.TimeZone timeZone)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("timezone/{continent:regex([a-z][a-z0-9_])}/{country:regex([a-z][a-z0-9_])}/{state:regex([a-z][a-z0-9_])?}")]
        public IHttpActionResult DeleteTimeZone(string timeZoneId)
        {
            // refer to http://www.restapitutorial.com/lessons/httpmethods.html
            // for http status codes that need to be used.
            throw new NotImplementedException();
        }
    }
}