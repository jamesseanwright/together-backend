using Moq;
using System.Net.Http;
using Xunit;
using System.Threading.Tasks;
using System;
using System.Threading;
using Moq.Protected;

namespace WebApplication.Services.HttpService
{
    public class HttpServiceTests
    {
        const string Url = "http://some-url";
        const string PostBody = "post body";
        const string ResponseBody = "response body";

        [Fact]
        public async void PostAsyncShouldSendAPostRequest()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(ResponseBody);

            Mock<HttpClientHandler> mockHandler = new Mock<HttpClientHandler>();

            mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                Moq.Protected.ItExpr.Is<HttpRequestMessage>(m => m.RequestUri == new Uri(Url) && m.Method == HttpMethod.Post),
                Moq.Protected.ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(new HttpResponseMessage() { Content = new StringContent(ResponseBody) });

            HttpService httpService = new HttpService(new HttpClient(mockHandler.Object));

            string actualResponse = await httpService.PostAsync(Url, PostBody);

            Assert.Equal(ResponseBody, actualResponse);
        }
    }
}