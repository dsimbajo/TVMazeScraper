using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVMaze.Infrastructure.Services.Clients;
using TVMaze.Core.Contracts;
using TVMaze.Infrastructure.Data;
using TVMaze.Infrastructure.Services;
using Polly;
using System.Net.Http;
using Polly.Extensions.Http;
using AutoMapper;
using TVMaze.Infrastructure.Mapping;

namespace TVMaze.DataImporter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient<ShowsClient>()
                    .AddPolicyHandler(GetRetryPolicy());

                    services.AddHttpClient<CastClient>()
                    .AddPolicyHandler(GetRetryPolicy());

                    var mapperConfig = new MapperConfiguration(mapperConfig => {
                        mapperConfig.AddProfile(new MappingProfile()); 
                    });

                    IMapper mapper = mapperConfig.CreateMapper();
                    services.AddSingleton(mapper);

                    services.AddTransient<IShowRepository, ShowRepository>();
                    services.AddTransient<ICastRepository, CastRepository>();
                    services.AddTransient<IImportTransactionRepository, ImportTransactionRepository>();
                    services.AddTransient<IUnitOfWork, UnitOfWork>();
                    services.AddSingleton<IDataImporterService, DataImporterService>();
                    services.AddHostedService<Worker>();
                 
                });

        /// <summary>
        /// Retry Mechanism to handle too many requests error
        /// </summary>
        /// <returns></returns>
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
               .HandleTransientHttpError()
               .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
               .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(10));
        }
    }
}
