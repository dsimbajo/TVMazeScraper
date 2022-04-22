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
    public class CastClient_Tests
    {

        private string _castData;

        [TestInitialize]
        public void Init()
        {

            string castJsonFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resources\casts.json");
            _castData = File.ReadAllText(castJsonFilePath);

        }


        [TestMethod]
        public async Task CastClient_Json_Return_Not_Null()
        {
            var castHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            castHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(_castData)
                })
                .Verifiable();

            var httpClient = new HttpClient(castHandlerMock.Object)
            {
                BaseAddress = new System.Uri("https://api.tvmaze.com")
            };

            var castClient = new CastClient(httpClient);

            var content = await castClient.GetCastByShowId(1);

            Assert.IsNotNull(content);
            
        }
    }
}
