// WeatherUnitTest.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Moq;
using RestSharp;
using TravelApp1.Services;
using TravelApp1;
using Microsoft.Extensions.Configuration;
using System;

namespace ApiTesting
{
    [TestClass]
    public class WeatherUnitTest
    {
        private Mock<RestClient> _restClientMock;
        private Mock<IConfiguration> _configurationMock;
        private WeatherService _weatherService;

        [TestInitialize]
        public void Setup()
        {
            // Mock IConfiguration
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(config => config["WeatherService:ApiKey"])
                .Returns("TEST_API_KEY");

            // Mock RestClient
            _restClientMock = new Mock<RestClient>("https://fakeurl.com") { CallBase = true };

            // Initialize WeatherService with mocks
            _weatherService = new WeatherService(_configurationMock.Object, _restClientMock.Object);
        }

        [TestMethod]
        public async Task GetWeatherAsync_ValidLocation_ReturnsWeatherResponse()
        {
            // Arrange
            string location = "New York";
            var mockResponse = new RestResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = "{\"resolvedAddress\":\"New York, NY\",\"currentConditions\":{\"temp\":25.0,\"conditions\":\"Clear\",\"humidity\":50.0,\"windSpeed\":10.0}}"
            };

            _restClientMock
                .Setup(client => client.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockResponse);

            // Act
            var result = await _weatherService.GetWeatherAsync(location);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("New York, NY", result.ResolvedAddress);
            Assert.AreEqual(25.0f, result.CurrentConditions.Temp);
        }

        [TestMethod]
        public async Task GetWeatherAsync_InvalidLocation_ReturnsNull()
        {
            // Arrange
            string location = "InvalidLocation";
            var mockResponse = new RestResponse
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Content = string.Empty
            };

            _restClientMock
                .Setup(client => client.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockResponse);

            // Act
            var result = await _weatherService.GetWeatherAsync(location);

            // Assert
            Assert.IsNull(result);
        }
    }
}
