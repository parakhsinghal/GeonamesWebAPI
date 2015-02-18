using GeoDataAPI.Domain;
using GeoDataAPI.Service.Controllers;
using GeoDataAPI.SQLRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GeoDataAPI.Service.Integrated.Tests.Controllers
{
    [TestClass]
    public class FeatureCodeContollerTests
    {
        #region Local fields, test initialization and test clean up setup

        private string serverURL = string.Empty;
        private string requestURL = string.Empty;
        private string jsonMediaType = string.Empty;
        private string xmlMediaType = string.Empty;
        private string jsonFormatParameter = string.Empty;
        private string xmlFormatParameter = string.Empty;
        private string pageSizeSegment = string.Empty;
        private string pageNumberSegment = string.Empty;
        private int expectedResultCount = int.MinValue;
        private HttpClientHandler clientHandler;
        private HttpClient client;

        private string featureCodeControllerSegment = string.Empty;
        private string expectedFeatureCodeId = string.Empty;
        private FeatureCodeController featureCodeControllerObject;

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
            clientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
            client = new HttpClient(clientHandler);

            featureCodeControllerSegment = ConfigurationManager.AppSettings["FeatureCodeControllerSegment"].ToString();
            expectedFeatureCodeId = ConfigurationManager.AppSettings["FeatureCode"].ToString();
            featureCodeControllerObject = new FeatureCodeController(new FeatureCodeSQLRepository());
        }

        [TestCleanup]
        public void CleanUpLocalFields()
        {
            serverURL = string.Empty;
            jsonMediaType = string.Empty;
            xmlMediaType = string.Empty;
            jsonFormatParameter = string.Empty;
            xmlFormatParameter = string.Empty;
            pageSizeSegment = string.Empty;
            pageNumberSegment = string.Empty;
            expectedResultCount = int.MinValue;
            clientHandler = null;
            client = null;

            featureCodeControllerSegment = string.Empty;
            expectedFeatureCodeId = string.Empty;
            featureCodeControllerObject = null;
        }

        #endregion

        #region Integrated tests

        #region Tests for construction of FeatureCodeController class

        [TestMethod]
        public void Constructor_Instantiation_ShouldReturnNonNullControllerObject()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(featureCodeControllerObject);
        }

        [TestMethod]
        public void Constructor_Instantiation_ShouldReturnAnInstanceOfFeatureCodeController()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsInstanceOfType(featureCodeControllerObject, typeof(FeatureCodeController));
        }

        #endregion

        #region Tests for GetFeatureCodes method

        [TestMethod]
        public void GetFeatureCodes_Executed_ReturnsANonNullResult()
        {
            //Arrange
            requestURL = serverURL + featureCodeControllerSegment;

            //Act
            string result = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetFeatureCodes_Executed_ReturnsAValidListOfFeatureCodes()
        {
            //Arrange
            requestURL = serverURL + featureCodeControllerSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<FeatureCode> featureCodeCollection = JsonConvert.DeserializeObject<List<FeatureCode>>(response);
            string actual = featureCodeCollection.Find(featureCode => featureCode.FeatureCodeId == expectedFeatureCodeId).FeatureCodeId;
            string expected = expectedFeatureCodeId;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetFeatureCodes_Executed_ReturnsValidHttpStatusCode()
        {
            //Arrange
            requestURL = serverURL + featureCodeControllerSegment;

            //Act
            HttpStatusCode actual = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expected = HttpStatusCode.OK;

            //Assert
            Assert.AreEqual<HttpStatusCode>(expected, actual);
        }

        [TestMethod]
        public void GetFeatureCodes_Executed_ReturnsValidJSONResponseObjectByDefault()
        {
            //Arrange
            requestURL = serverURL + featureCodeControllerSegment;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetFeatureCodes_ChangedAcceptHeader_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + featureCodeControllerSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetFeatureCodes_ChangedAcceptHeader_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + featureCodeControllerSegment;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetFeatureCodes_PassedQueryStringParameter_ReturnsValidJSONResponse()
        {
            //Arrange
            requestURL = serverURL + featureCodeControllerSegment + jsonFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = jsonMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetFeatureCodes_PassedQueryStringParameter_ReturnsValidXMLResponse()
        {
            //Arrange
            requestURL = serverURL + featureCodeControllerSegment + xmlFormatParameter;

            //Act
            string actual = client.GetAsync(requestURL).Result.Content.Headers.ContentType.MediaType;
            string expected = xmlMediaType;

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void GetFeatureCodes_PassedAPageNumberAndPageSize_ReturnsValidPaginatedResponse()
        {
            //Arrange
            requestURL = serverURL + featureCodeControllerSegment + @"?" + pageSizeSegment + @"&" + pageNumberSegment;

            //Act
            string response = client.GetAsync(requestURL).Result.Content.ReadAsStringAsync().Result;
            List<FeatureCode> result = JsonConvert.DeserializeObject<List<FeatureCode>>(response);
            int actualResultCount = result.Count;

            //Assert
            Assert.AreEqual(expectedResultCount, actualResultCount);
        }

        [TestMethod]
        public void GetFeatureCodes_PassedAnInvalidPageNumber_ReturnsBadRequestResponse()
        {
            //Arrange
            pageNumberSegment = "pageNumber=-1";
            requestURL = serverURL + featureCodeControllerSegment + @"?" + pageSizeSegment + @"&" + pageNumberSegment;

            //Act
            HttpStatusCode actualStatusCode = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expectedStatusCode = HttpStatusCode.BadRequest;

            //Assert
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        [TestMethod]
        public void GetFeatureCodes_PassedAnInvalidAndPageSize_ReturnsBadRequestResponse()
        {
            //Arrange
            pageSizeSegment = "pageSize=0";
            requestURL = serverURL + featureCodeControllerSegment + @"?" + pageSizeSegment + @"&" + pageNumberSegment;

            //Act
            HttpStatusCode actualStatusCode = client.GetAsync(requestURL).Result.StatusCode;
            HttpStatusCode expectedStatusCode = HttpStatusCode.BadRequest;

            //Assert
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        #endregion

        #endregion
    }
}
