using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GeoDataAPI.Service.Integrated.Tests.Controllers
{
    [TestClass]
    public class TimeZoneControllerTests
    {
        #region Local fields, test initialization and test clean up setup

        private string serverURL = string.Empty;
        private string requestURL = string.Empty;
        private string jsonMediaType = string.Empty;
        private string xmlMediaType = string.Empty;
        private string jsonFormatParameter = string.Empty;
        private string xmlFormatParameter = string.Empty;
        private string keyValueSegment = string.Empty;
        private string pageSizeSegment = string.Empty;
        private string pageNumberSegment = string.Empty;
        private int expectedResultCount = int.MinValue;
        private HttpClientHandler clientHandler;
        private HttpClient client;

        private string expectedCountryName = string.Empty;
        private long? expectedCountryGeonameId = null;
        private string expectedISOCountryCode = string.Empty;
        private string expectedISO3CountryCode = string.Empty;
        private string expectedISONumericCountryCode = string.Empty;
        private double expectedLatitude = 0;
        private double expectedLongitude = 0;
        private string expectedTimeZone = string.Empty;
        private string expectedStateName = string.Empty;
        private string expectedCityName = string.Empty;
        private string timeZoneControllerSegment = string.Empty;
        private string timeZonePlaceSegment = string.Empty;
        private string timeZoneDetailsSegment = string.Empty;

        [TestInitialize]
        public void InitializelocalFields()
        {
            serverURL = ConfigurationManager.AppSettings["ServerURL"].ToString();
            jsonMediaType = ConfigurationManager.AppSettings["JSONMediaType"].ToString();
            xmlMediaType = ConfigurationManager.AppSettings["XMLMediaType"].ToString();
            jsonFormatParameter = ConfigurationManager.AppSettings["JSONFormatParameter"].ToString();
            xmlFormatParameter = ConfigurationManager.AppSettings["XMLFormatParameter"].ToString();
            pageSizeSegment = ConfigurationManager.AppSettings["PageSizeSegment"].ToString();
            pageNumberSegment = ConfigurationManager.AppSettings["PageNumberSegment"].ToString();
            expectedResultCount = int.Parse(ConfigurationManager.AppSettings["ExpectedResultCount"].ToString());
            keyValueSegment = ConfigurationManager.AppSettings["KeyValueSegment"].ToString();
            expectedCountryName = ConfigurationManager.AppSettings["CountryName"].ToString();
            expectedCountryGeonameId = Convert.ToInt64(ConfigurationManager.AppSettings["CountryGeonameId"].ToString());
            expectedISOCountryCode = ConfigurationManager.AppSettings["ISOCountryCode"].ToString();
            expectedISO3CountryCode = ConfigurationManager.AppSettings["ISO3Code"].ToString();
            expectedISONumericCountryCode = ConfigurationManager.AppSettings["ISONumeric"].ToString();
            expectedLatitude = double.Parse(ConfigurationManager.AppSettings["Latitude"].ToString());
            expectedLongitude = double.Parse(ConfigurationManager.AppSettings["Longitude"].ToString());
            expectedTimeZone = ConfigurationManager.AppSettings["TimeZoneId"].ToString();
            expectedStateName = ConfigurationManager.AppSettings["StateName"].ToString();
            expectedCityName = ConfigurationManager.AppSettings["CityName"].ToString();
            clientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
            client = new HttpClient(clientHandler);

            timeZoneControllerSegment = ConfigurationManager.AppSettings["TimeZoneControllerSegment"].ToString();
            timeZonePlaceSegment = ConfigurationManager.AppSettings["TimeZonePlaceSegment"].ToString();
            timeZoneDetailsSegment = ConfigurationManager.AppSettings["TimeZoneDetailsSegment"].ToString();
        }

        [TestCleanup]
        public void CleanUpLocalFields()
        {
            serverURL = string.Empty;
            jsonMediaType = string.Empty;
            xmlMediaType = string.Empty;
            jsonFormatParameter = string.Empty;
            xmlFormatParameter = string.Empty;
            keyValueSegment = string.Empty;
            pageNumberSegment = string.Empty;
            pageSizeSegment = string.Empty;
            expectedResultCount = int.MinValue;
            expectedCountryName = string.Empty;
            expectedISOCountryCode = string.Empty;
            expectedISO3CountryCode = string.Empty;
            expectedISONumericCountryCode = string.Empty;
            expectedLatitude = 0;
            expectedLongitude = 0;
            expectedTimeZone = string.Empty;
            expectedCountryGeonameId = null;
            expectedStateName = string.Empty;
            expectedCityName = string.Empty;
            clientHandler = null;
            client = null;

            timeZoneControllerSegment = string.Empty;
            timeZonePlaceSegment = string.Empty;
            timeZoneDetailsSegment = string.Empty;
        }

        #endregion

        #region Tests for GetDistinctTimeZones method

        [TestMethod]
        public void GetDistinctTimeZones_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment;

            //Act
            HttpContent result = client.GetAsync(requestURL).Result.Content;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDistinctTimeZones_Executed_ReturnsAValidListOfTimeZones()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<string> result = JsonConvert.DeserializeObject<List<string>>(response);
            string expected = expectedTimeZone;
            string actual = result.Find(timeZone => timeZone.Equals(expectedTimeZone));

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetDistinctTimeZones_Executed_ReturnsAValidHttpStatus()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetDistinctTimeZones_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetDistinctTimeZones_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetDistinctTimeZones_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetDistinctTimeZones_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetDistinctTimeZones_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        #endregion

        #region Tests for GetTimeZoneDetailsByPlaceName method

        [TestMethod]
        public void GetTimeZoneDetailsByPlaceName_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + timeZonePlaceSegment + @"/" + expectedCountryName;

            //Act
            HttpContent result = client.GetAsync(requestURL).Result.Content;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTimeZoneDetailsByPlaceName_Executed_ReturnsAValidListOfTimeZones()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + timeZonePlaceSegment + @"/" + expectedStateName;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Domain.TimeZone> result = JsonConvert.DeserializeObject<List<Domain.TimeZone>>(response);
            string expected = expectedTimeZone;
            string actual = result.Find(timeZone => timeZone.TimeZoneId == expectedTimeZone).TimeZoneId;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetailsByPlaceName_Executed_ReturnsAValidHttpStatus()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + timeZonePlaceSegment + @"/" + expectedCountryName;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetailsByPlaceName_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + timeZonePlaceSegment + @"/" + expectedCountryName;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetailsByPlaceName_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + timeZonePlaceSegment + @"/" + expectedCountryName;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetailsByPlaceName_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + timeZonePlaceSegment + @"/" + expectedCountryName;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetailsByPlaceName_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + timeZonePlaceSegment + @"/" + expectedCountryName + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetailsByPlaceName_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + timeZonePlaceSegment + @"/" + expectedCountryName + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetailsByPlaceName_PassedAValidStateName_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + timeZonePlaceSegment + @"/" + expectedStateName;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Domain.TimeZone> result = JsonConvert.DeserializeObject<List<Domain.TimeZone>>(response);
            string expected = expectedTimeZone;
            string actual = result.Find(timeZone => timeZone.TimeZoneId == expectedTimeZone).TimeZoneId;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetailsByPlaceName_PassedValidCityName_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + timeZonePlaceSegment + @"/" + expectedCityName;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Domain.TimeZone> result = JsonConvert.DeserializeObject<List<Domain.TimeZone>>(response);
            string expected = expectedTimeZone;
            string actual = result.Find(timeZone => timeZone.TimeZoneId == expectedTimeZone).TimeZoneId;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        #endregion

        #region Tests for GetTimeZoneDetails method

        [TestMethod]
        public void GetTimeZoneDetails_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + @"/" + expectedCountryName;

            //Act
            HttpContent result = client.GetAsync(requestURL).Result.Content;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTimeZoneDetails_Executed_ReturnsAValidListOfTimeZones()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + timeZoneDetailsSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Domain.TimeZone> result = JsonConvert.DeserializeObject<List<Domain.TimeZone>>(response);
            string expected = expectedTimeZone;
            string actual = result.Find(timeZone => timeZone.TimeZoneId == expectedTimeZone).TimeZoneId;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetails_Executed_ReturnsAValidHttpStatus()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + @"/" + expectedCountryName;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetails_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + @"/" + expectedCountryName;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetails_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + @"/" + expectedCountryName;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetails_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + @"/" + expectedCountryName;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetails_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + @"/" + expectedCountryName + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetails_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + @"/" + expectedCountryName + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetails_PassedAValidCountryName_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + @"/" + expectedCountryName;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Domain.TimeZone> result = JsonConvert.DeserializeObject<List<Domain.TimeZone>>(response);
            string expected = expectedTimeZone;
            string actual = result.Find(timeZone => timeZone.TimeZoneId == expectedTimeZone).TimeZoneId;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetails_PassedValidISOCountryCode_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + @"/" + expectedISOCountryCode;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Domain.TimeZone> result = JsonConvert.DeserializeObject<List<Domain.TimeZone>>(response);
            string expected = expectedTimeZone;
            string actual = result.Find(timeZone => timeZone.TimeZoneId == expectedTimeZone).TimeZoneId;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetails_PassedValidISO3CountryCode_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + @"/" + expectedISO3CountryCode;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Domain.TimeZone> result = JsonConvert.DeserializeObject<List<Domain.TimeZone>>(response);
            string expected = expectedTimeZone;
            string actual = result.Find(timeZone => timeZone.TimeZoneId == expectedTimeZone).TimeZoneId;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetails_PassedValidISONumericCountryCode_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + @"/" + expectedISONumericCountryCode;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Domain.TimeZone> result = JsonConvert.DeserializeObject<List<Domain.TimeZone>>(response);
            string expected = expectedTimeZone;
            string actual = result.Find(timeZone => timeZone.TimeZoneId == expectedTimeZone).TimeZoneId;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetails_PassedValidLatitudeAndLongitude_ReturnsValidResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + @"/" + expectedLatitude + @"/" + expectedLongitude;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Domain.TimeZone> result = JsonConvert.DeserializeObject<List<Domain.TimeZone>>(response);
            string expected = expectedTimeZone;
            string actual = result.Find(timeZone => timeZone.TimeZoneId == expectedTimeZone).TimeZoneId;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetTimeZoneDetails_PassedAPageNumberAndPageSize_ReturnsValidPaginatedResponse()
        {
            //Arrange
            requestURL = serverURL + timeZoneControllerSegment + timeZoneDetailsSegment + @"?" + pageNumberSegment + @"&" + pageSizeSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<Domain.TimeZone> result = JsonConvert.DeserializeObject<List<Domain.TimeZone>>(response);
            int actualResultCount = result.Count;

            //Assert
            Assert.AreEqual<int>(expectedResultCount, actualResultCount);
        }
        #endregion
    }
}
