using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TravelApp1.Services;
using TravelApp1;

namespace ApiTesting
{
    [TestClass]
    public class NewsUnitTest
    {
        private Mock<RestClient> _mockRestClient;

        [TestInitialize]
        public void Setup()
        {
            // Initialize the mock RestClient before each test
            _mockRestClient = new Mock<RestClient>();
        }

        [TestMethod]
        public async Task GetNewsAsync_ValidLocation_ReturnsNewsResponse()
        {
            // Arrange
            string location = "Dundalk";
            var fakeJsonResponse = JsonConvert.SerializeObject(new NewsResponse
            {
                Articles = new[]
                {
                    new Article
                    {
                        Title = "Local Weather Update in Dundalk",
                        Description = "Stay updated with the latest weather news in Dundalk.",
                        Url = "https://newsapi.org/article/1"
                    }
                }
            });

            var mockResponse = new RestResponse
            {
                Content = fakeJsonResponse,
                StatusCode = System.Net.HttpStatusCode.OK
            };

            _mockRestClient
                .Setup(client => client.ExecuteAsync(It.IsAny<RestRequest>(), Method.Get, default))
                .ReturnsAsync(mockResponse);

            var newsService = new NewsService(_mockRestClient.Object);

            // Act
            var result = await newsService.GetNewsAsync(location);

            // Assert
            Assert.IsNotNull(result, "Expected non-null NewsResponse");
            Assert.AreEqual(1, result.Articles.Length);
            Assert.AreEqual("Local Weather Update in Dundalk", result.Articles[0].Title);
            Assert.AreEqual("Stay updated with the latest weather news in Dundalk.", result.Articles[0].Description);
            Assert.AreEqual("https://newsapi.org/article/1", result.Articles[0].Url);
        }

        [TestMethod]
        public async Task GetNewsAsync_InvalidLocation_ReturnsNull()
        {
            // Arrange
            string location = "InvalidLocation";
            var mockResponse = new RestResponse
            {
                Content = null,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };

            _mockRestClient
                .Setup(client => client.ExecuteAsync(It.IsAny<RestRequest>(), Method.Get, default))
                .ReturnsAsync(mockResponse);

            var newsService = new NewsService(_mockRestClient.Object);

            // Act
            var result = await newsService.GetNewsAsync(location);

            // Assert
            Assert.IsNull(result, "Expected null NewsResponse for invalid location");
        }

        [TestMethod]
        public async Task GetNewsAsync_ApiError_ThrowsException()
        {
            // Arrange
            string location = "Dundalk";
            _mockRestClient
                .Setup(client => client.ExecuteAsync(It.IsAny<RestRequest>(), Method.Get, default))
                .ThrowsAsync(new HttpRequestException("API Error"));

            var newsService = new NewsService(_mockRestClient.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => newsService.GetNewsAsync(location));
        }
    }
}
