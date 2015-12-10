using GeoDataAPI.Domain;
using GeoDataAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace GeoDataAPI.Service.Controllers
{
    [RoutePrefix("api/v2/countries")]
    public class CountryController : ApiController
    {
        private ICountryRepository repository;

        public CountryController(ICountryRepository _repository)
        {
            this.repository = _repository;
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<Country>))]
        public IHttpActionResult GetCountries(int? pageSize = null, int? pageNumber = null)
        {
            try
            {
                if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) ||
                                (pageSize == null && pageNumber == null))
                {
                    try
                    {
                        var result = repository.GetAllCountries(pageSize, pageNumber);
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
        [Route("{isoCountryCode:length(2):alpha}")]
        [Route("{countryName:minlength(3)}")]
        [ResponseType(typeof(Country))]
        public IHttpActionResult GetCountryInfo(string isoCountryCode = null, string countryName = null)
        {
            try
            {
                if ((!string.IsNullOrEmpty(isoCountryCode) && !string.IsNullOrWhiteSpace(isoCountryCode)) ||
                                (!string.IsNullOrEmpty(countryName) && !string.IsNullOrWhiteSpace(countryName)))
                {
                    try
                    {
                        var result = repository.GetCountryInfo(isoCountryCode, countryName);
                        if (result != null)
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
                    return BadRequest("Please provide a valid value of ISO country code or country name");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        [Route("{isoCountryCode:length(2):alpha}/{featureCategoryId:length(1):alpha}/{featureCodeId?}")]
        [Route("{countryName:minlength(3)}/{featureCategoryId:length(1):alpha}/{featureCodeId?}")]
        [ResponseType(typeof(List<RawData>))]
        public IHttpActionResult GetCountryFeatureCategoryFeatureCode(string featureCategoryId, string isoCountryCode = null, string countryName = null, string featureCodeId = null, int? pageSize = null, int? pageNumber = null)
        {
            try
            {
                if ((!string.IsNullOrEmpty(isoCountryCode) && !string.IsNullOrWhiteSpace(isoCountryCode)) ||
                               (!string.IsNullOrEmpty(countryName) && !string.IsNullOrWhiteSpace(countryName)))
                {
                    if ((!string.IsNullOrEmpty(featureCategoryId) && !string.IsNullOrWhiteSpace(featureCategoryId)) ||
                        ((!string.IsNullOrEmpty(featureCodeId) && !string.IsNullOrWhiteSpace(featureCodeId))))
                    {
                        if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) ||
                        (pageSize == null && pageNumber == null))
                        {
                            try
                            {
                                var result = repository.GetCountryFeatureCategoryFeatureCode(featureCategoryId, isoCountryCode, countryName, featureCodeId, pageSize, pageNumber);
                                if (result != null)
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
                    else
                    {
                        return BadRequest("Please provide a valid value of feature category id and/or feature code.");
                    }
                }
                else
                {
                    return BadRequest("Please provide a valid value of ISO country code or country name.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        [Route("{countryName:minlength(3)}/states")]
        [Route("{isoCountryCode:alpha:length(2)}/states")]
        [ResponseType(typeof(List<RawData>))]
        public IHttpActionResult GetStates(string countryName = null, string isoCountryCode = null, int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                if ((!string.IsNullOrEmpty(countryName) && !string.IsNullOrWhiteSpace(countryName) ||
                                (!string.IsNullOrWhiteSpace(isoCountryCode) && !string.IsNullOrEmpty(isoCountryCode))))
                {
                    if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) ||
                        (pageSize == null && pageNumber == null))
                    {
                        try
                        {
                            var result = repository.GetStates(countryName, isoCountryCode, pageNumber, pageSize);
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
                else
                {
                    return BadRequest("Country name or ISO country code need to have valid values.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        [Route("{countryName:minlength(3)}/states/{stateName:minlength(3):regex([a-zA-Z]+[ a-zA-Z-_]*)}")]
        [Route("{countryName:minlength(3)}/states/{stateGeonameId:long}")]
        [Route("{isoCountryCode:alpha:length(2)}/states/{stateName:minlength(3):regex([a-zA-Z]+[ a-zA-Z-_]*)}")]
        [Route("{isoCountryCode:alpha:length(2)}/states/{stateGeonameId:long}")]
        [ResponseType(typeof(RawData))]
        public IHttpActionResult GetStateInfo(string countryName = null, string isoCountryCode = null, string stateName = null, int? stateGeonameId = null)
        {
            try
            {
                if ((!string.IsNullOrEmpty(countryName) && !string.IsNullOrWhiteSpace(countryName)) ||
                               (!string.IsNullOrEmpty(isoCountryCode) && !string.IsNullOrWhiteSpace(isoCountryCode)))
                {
                    if ((!string.IsNullOrEmpty(stateName) && !string.IsNullOrWhiteSpace(stateName)) ||
                        (stateGeonameId.HasValue && stateGeonameId.Value > 0))
                    {
                        var result = repository.GetStateInfo(countryName, isoCountryCode, stateName, stateGeonameId);
                        if (result.RowId != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return BadRequest("State name or state geoname id need to have valid values.");
                    }
                }
                else
                {
                    return BadRequest("Country name or ISO country code need to have valid values.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        [Route("{countryName:minlength(3)}/states/{stateName:minlength(3):regex([a-zA-Z]+[ a-zA-Z-_]*)}/cities")]
        [Route("{countryName:minlength(3)}/states/{stateGeonameId:long}/cities")]
        [Route("{isoCountryCode:alpha:length(2)}/states/{stateName:minlength(3):regex([a-zA-Z]+[ a-zA-Z-_]*)}/cities")]
        [Route("{isoCountryCode:alpha:length(2)}/states/{stateGeonameId:long}/cities")]
        [ResponseType(typeof(List<RawData>))]
        public IHttpActionResult GetCitiesInState(string countryName = null, string isoCountryCode = null, string stateName = null, int? stateGeonameId = null, int? cityGeonameId = null, string cityName = null, int? pageSize = null, int? pageNumber = null)
        {
            try
            {
                if ((!string.IsNullOrEmpty(countryName) && !string.IsNullOrWhiteSpace(countryName)) ||
                                !string.IsNullOrEmpty(isoCountryCode) && !string.IsNullOrWhiteSpace(isoCountryCode))
                {
                    if ((!string.IsNullOrWhiteSpace(stateName) && !string.IsNullOrEmpty(stateName)) ||
                    (stateGeonameId.HasValue && stateGeonameId.Value > 0))
                    {
                        if ((string.IsNullOrEmpty(cityName) && string.IsNullOrWhiteSpace(cityName)) ||
                            (cityGeonameId == null) ||
                            (!string.IsNullOrEmpty(cityName) && !string.IsNullOrWhiteSpace(cityName)) ||
                            (cityGeonameId.HasValue && cityGeonameId.Value > 0))
                        {
                            if (((pageNumber != null && pageSize != null) && (pageNumber > 0 && pageSize > 0)) ||
                        (pageSize == null && pageNumber == null))
                            {
                                try
                                {
                                    var result = repository.GetCitiesInState(countryName, isoCountryCode, stateName, stateGeonameId, cityGeonameId, cityName, pageSize, pageNumber);
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
                            return BadRequest("City name or city geoname id need to have valid values.");
                        }
                    }
                    else
                    {
                        return BadRequest("State name or state geoname id need to have valid values.");
                    }
                }
                else
                {
                    return BadRequest("Country name or ISO country code need to have valid values.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        [Route("{countryName:minlength(3)}/states/{stateName:minlength(3):regex([a-zA-Z]+[ a-zA-Z-_]*)}/cities/{cityName:minlength(3):regex([a-zA-Z]+[ a-zA-Z-_]*)}")]
        [Route("{countryName:minlength(3)}/states/{stateName:minlength(3):regex([a-zA-Z]+[ a-zA-Z-_]*)}/cities/{cityGeonameId:long}")]
        [Route("{countryName:minlength(3)}/states/{stateGeonameId:long}/cities/{cityName:minlength(3):regex([a-zA-Z]+[ a-zA-Z-_]*)}")]
        [Route("{countryName:minlength(3)}/states/{stateGeonameId:long}/cities/{cityGeonameId:long}")]
        [Route("{isoCountryCode:alpha:length(2)}/states/{stateName:minlength(3):regex([a-zA-Z]+[ a-zA-Z-_]*)}/cities/{cityName:minlength(3):regex([a-zA-Z]+[ a-zA-Z-_]*)}")]
        [Route("{isoCountryCode:alpha:length(2)}/states/{stateName:minlength(3):regex([a-zA-Z]+[ a-zA-Z-_]*)}/cities/{cityGeonameId:long}")]
        [Route("{isoCountryCode:alpha:length(2)}/states/{stateGeonameId:long}/cities/{cityName:minlength(3):regex([a-zA-Z]+[ a-zA-Z-_]*)}")]
        [Route("{isoCountryCode:alpha:length(2)}/states/{stateGeonameId:long}/cities/{cityGeonameId:long}")]
        [ResponseType(typeof(RawData))]
        public IHttpActionResult GetCityInState(string countryName = null, string isoCountryCode = null, string stateName = null, int? stateGeonameId = null, int? cityGeonameId = null, string cityName = null)
        {
            try
            {
                if ((!string.IsNullOrEmpty(countryName) && !string.IsNullOrWhiteSpace(countryName)) ||
                               !string.IsNullOrEmpty(isoCountryCode) && !string.IsNullOrWhiteSpace(isoCountryCode))
                {
                    if ((!string.IsNullOrWhiteSpace(stateName) && !string.IsNullOrEmpty(stateName)) ||
                    (stateGeonameId.HasValue && stateGeonameId.Value > 0))
                    {
                        if ((!string.IsNullOrEmpty(cityName) && !string.IsNullOrWhiteSpace(cityName)) ||
                            (cityGeonameId.HasValue && cityGeonameId.Value > 0))
                        {

                            try
                            {
                                var result = repository.GetCityInState(countryName, isoCountryCode, stateName, stateGeonameId, cityGeonameId, cityName);
                                if (result.RowId != null)
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
                            return BadRequest("City name or city geoname id need to have valid values.");
                        }
                    }
                    else
                    {
                        return BadRequest("State name or state geoname id need to have valid values.");
                    }
                }
                else
                {
                    return BadRequest("Country name or ISO country code need to have valid values.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
