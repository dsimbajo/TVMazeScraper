using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TVMaze.Core.DTO;
using TVMaze.Infrastructure.Data;
using TVMaze.Infrastructure.Services;
using TVMaze.Infrastructure.Services.Clients;

namespace TVMaze.Infrastructure.Tests
{
    [TestClass]
    public class ShowsClient_Tests
    {
        private string _showsData;

        [TestInitialize]
        public void Init()
        {

            string showsJsonFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resources\shows.json");
            _showsData = File.ReadAllText(showsJsonFilePath);

        }


        [TestMethod]
        public async Task ShowsClient_Return_Cast_Json()
        {
            var showsHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            showsHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(_showsData)
                })
                .Verifiable();

            var httpClient = new HttpClient(showsHandlerMock.Object)
            {
                BaseAddress = new System.Uri("https://api.tvmaze.com")
            };

            var showsClient = new ShowsClient(httpClient);

            var content = await showsClient.GetShowsPerPage(1);

            Assert.IsNotNull(content);
            
        }
    }
}
