using RubiconTest.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace RubiconTest.Tests
{
    public class HttpServiceTest
    {
        private readonly IHttpService _httpService;
        private readonly Mock<IHttpService> _mockHttpService = new Mock<IHttpService>();
        private readonly string _baseUrl;
        private readonly string auth;
        private readonly Dictionary<string, string> headers = new Dictionary<string, string>();

        public HttpServiceTest(IHttpService httpService)
        {
            _baseUrl = "https://intakeopdracht-apim-euw-p.azure-api.net/api/movies";
            _httpService = httpService;
            auth = "ukxCtsHaVniPXdwhMjIGvfCMNlJynLHcogKGgJW%2FAMo%3D";
            headers.Add("Accept", "application/xml");
        }

        [Fact]
        public async Task GetAsync()
        {
            // Define response to test against

            var mockResult = Helper.Helper.ReadFileData("genreCrimeIn0.xml");
            Uri uri = new UriBuilder($"{_baseUrl}/genres/crime?index=0").Uri;

            HttpResponseMessage httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(mockResult, Encoding.UTF8, "application/xml")
            };

            // Mock httpService response
            _mockHttpService
                .Setup(s => s.GetAsync(uri, null, null))
                .ReturnsAsync(httpResponse);

            // Simulate API call
            var resultToTest = await _httpService.GetAsync(uri, headers, auth);

            // Assert test
            Assert.Equal(resultToTest.Content.ToString(), mockResult);
        }
    }
}
